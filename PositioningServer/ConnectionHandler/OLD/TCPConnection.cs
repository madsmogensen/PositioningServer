using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PositioningServer.ConnectionHandler
{
    class TCPConnection
    {
        Thread listenerThread = null;

        public TCPConnection()
        {
            listenerThread = new Thread(new ThreadStart(StartServer));
            listenerThread.Start();
        }

        private void StartServer()
        {
            IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
            //IPAddress ipAddress = host.AddressList[0];
            IPAddress ipAddress = host.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            Socket listener = null;
            try
            {
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(100);

                Console.WriteLine("Waiting for a connection on ip:port");
                Console.WriteLine(ipAddress.ToString() + ":" + localEndPoint.Port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            while (true)
            {
                Socket handler = listener.Accept();
                Console.WriteLine("connection accepted");

                string data = null;
                byte[] bytes = null;

                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                data.TrimEnd();

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes("HamneHabibi");
                Console.WriteLine("Sending msg");
                handler.Send(msg);
                //handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                //Console.WriteLine("\n Press any key to continue...");
                //Console.ReadKey();
            }
        }
    }
}
