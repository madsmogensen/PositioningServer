using PositioningServer.Common.Data;
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
    class UDPOutgoing
    {

        long updateStarted;
        long timeElapsed;

        public UDPOutgoing()
        {
            IPEndPoint test = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
            Client testClient = new Client(test);
            testClient.setup = new Setup("test");
            testClient.request = "CLIENT;REQUEST:From File";
            SenderThread sender = new SenderThread(testClient);
            Thread t = new Thread(new ThreadStart(sender.send));
            t.Start();
        }


        public void update(List<Client> clients)
        {
            updateStarted = DateTime.Now.Ticks/10000; //ticks to millis
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
                for (int i = threadPool.Count-1; i >= 0; i--)
                {
                    if (threadPool[i].ThreadState.Equals(ThreadState.Stopped))
                    {
                        threadPool.RemoveAt(i);
                    }
                }
                timeElapsed = (DateTime.Now.Ticks/10000) - updateStarted;
                if (timeElapsed > 2000) //if blocked for more than 5 seconds, break
                {
                    foreach (Thread t in threadPool)
                    {
                        t.Abort();
                    }
                    Console.WriteLine("Time up, breaking while " +  timeElapsed);
                    break;
                }
            }
        }
    }

    public class SenderThread
    {
        Client client;
        Socket sendingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public SenderThread(Client client)
        {
            this.client = client;
        }

        public void send()
        {
            IPEndPoint endPoint = client.connection;
            if (endPoint == null) { /*printDebug(client);*/ return; } //skip debugging clients where endPoint is null
            if (client.setup == null) { return; } //skip clients without a setup

            List<byte[]> dataToSend = new List<byte[]>();
            foreach (Node node in client.setup.nodesClean)
            {
                for (int i = node.indexSent; i < node.coordinates.Count; i++ )
                {
                    Coordinate nextCoordinate = node.coordinates[i];
                    StringBuilder sb = new StringBuilder();
                    sb.Append(node.id + ";");
                    sb.Append(nextCoordinate.x + ";");
                    sb.Append(nextCoordinate.y + ";");
                    sb.Append(nextCoordinate.z + ";");
                    sb.Append(nextCoordinate.dateTime.ToString().Replace(".", ":") + ".");
                    for (int j = nextCoordinate.dateTime.Millisecond.ToString().Length; j < 3; j++) { sb.Append("0"); }
                    sb.Append(nextCoordinate.dateTime.Millisecond);
                    //Console.WriteLine(i + " - " + sb.ToString());
                    byte[] coordinateBuffer = Encoding.ASCII.GetBytes(sb.ToString());
                    dataToSend.Add(coordinateBuffer);
                    node.indexSent = i+1;
                }
            }   

            foreach (byte[] sendBuffer in dataToSend)
            {
                try
                {
                    //ONLY FOR DEBUGGNING; DELETE LINE LATER
                    //endPoint.Port = 11000;
                    sendingSocket.SendTo(sendBuffer, endPoint);
                    //Console.WriteLine("Sent something to " + endPoint);
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
