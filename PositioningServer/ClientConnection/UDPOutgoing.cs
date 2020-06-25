using PositioningServer.Common.Data;
using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PositioningServer.ConnectionHandler
{
    public class UDPOutgoing
    {

        private long updateStarted;
        private long timeElapsed;

        public UDPOutgoing()
        {
            /*IPEndPoint test = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
            Client testClient = new Client(test);
            testClient.setup = "test";
            testClient.request = "CLIENT;REQUEST:From File";
            SenderThread sender = new SenderThread(testClient);
            Thread t = new Thread(new ThreadStart(sender.send));
            t.Start();*/
        }


        public void update(List<Client> clients)
        {
            updateStarted = DateTime.Now.Ticks / 10000; //ticks to millis
            timeElapsed = 0;
            List<Thread> threadPool = new List<Thread>();
            foreach (Client client in clients)
            {
                SenderThread sender = new SenderThread(client);
                Thread t = new Thread(new ThreadStart(sender.send));
                threadPool.Add(t);
                t.Start();
            }

            //Blocking untill all threads are closed
            while (threadPool.Count > 0)
            {
                for (int i = threadPool.Count - 1; i >= 0; i--)
                {
                    if (threadPool[i].ThreadState.Equals(ThreadState.Stopped))
                    {
                        threadPool.RemoveAt(i);
                    }
                }
                timeElapsed = (DateTime.Now.Ticks / 10000) - updateStarted;
                if (timeElapsed > 5000) //if blocked for more than 5 seconds, break
                {
                    foreach (Thread t in threadPool)
                    {
                        t.Abort();
                    }
                    Console.WriteLine("Time up, breaking while " + timeElapsed);
                    break;
                }
            }
        }
    }

    internal class SenderThread
    {
        private Client client;
        private Socket sendingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public SenderThread(Client client)
        {
            this.client = client;
        }

        internal void send()
        {
            IPEndPoint endPoint = client.connection;
            if (endPoint == null) { /*printDebug(client);*/ return; } //skip debugging clients where endPoint is null

            List<byte[]> dataToSend = new List<byte[]>();

            if (client.request.Equals("GETAVAILABLESETUPS"))
            {
                Console.WriteLine("getSetups().count: " + SetupFacade.Instance.getSetups().Count);
                foreach (ISetup setup in SetupFacade.Instance.getSetups())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SETUP;");
                    sb.Append(setup.id());
                    byte[] coordinateBuffer = Encoding.ASCII.GetBytes(sb.ToString());
                    Console.WriteLine("data added to buffer " + sb.ToString());
                    dataToSend.Add(coordinateBuffer);
                }
                client.request = "";
            }
            else
            {
                //if (client.setup == "") { return; } //skip clients without a setup
                foreach (IUnitIterator iterator in client.getSetup())
                {
                    foreach (IUnit unit in iterator.getUnits())
                    {
                        for (int i = iterator.getIndex(); i < unit.size(); i++)
                        {
                            
                            iterator.next();
                            Coordinate nextCoordinate = unit.getCoordinate(i);
                            StringBuilder sb = new StringBuilder();
                            sb.Append("DATA;");
                            sb.Append(unit.getId() + ";");
                            sb.Append(nextCoordinate.x + ";");
                            sb.Append(nextCoordinate.y + ";");
                            sb.Append(nextCoordinate.z + ";");
                            sb.Append(nextCoordinate.dateTime.ToString().Replace(".", ":") + ".");
                            for (int j = nextCoordinate.dateTime.Millisecond.ToString().Length; j < 3; j++) { sb.Append("0"); }
                            sb.Append(nextCoordinate.dateTime.Millisecond);
                            byte[] coordinateBuffer = Encoding.ASCII.GetBytes(sb.ToString());
                            dataToSend.Add(coordinateBuffer);
                            Console.WriteLine("Sent data: " + sb.ToString());
                        }
                    }
                }
            }

            foreach (byte[] sendBuffer in dataToSend)
            {
                try
                {
                    endPoint.Port = 11005;
                    sendingSocket.SendTo(sendBuffer, endPoint);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Something went wrong when sending to the client");
                }
            }
            sendingSocket.Close();
        }
    }
}
