using PositioningServer.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PositioningServer.ConnectionHandler
{
    public class UDPIncoming
    {
        private List<Client> clientRequests = new List<Client>();

        public UDPIncoming()
        {
            ListenerThread listener = new ListenerThread(clientRequests);
            Thread t = new Thread(new ThreadStart(listener.listen));
            t.Start();
        }

        //Currently setupFacade is unused, because getting new setup data from clients is unsupported
        public void update(List<Client> clients, SetupFacade setupFacade)
        {
            //Handle client requests
            foreach (Client request in clientRequests)
            {
                bool consumed = false;
                //Compare if client request is already connected in clients
                foreach (Client client in clients)
                {
                    if (request.connection.Equals(client.connection))
                    {
                        client.request = request.request;
                        client.lastUpdate = request.lastUpdate;
                        consumed = true;
                        break;
                    }
                }
                if (!consumed) { clients.Add(request); Console.WriteLine("New client connected " + request.connection); }
            }
            clientRequests.Clear();
        }
    }

    internal class ListenerThread
    {

        private List<Client> clientRequests;

        private const int listenPort = 11000;
        private UdpClient listener = new UdpClient(listenPort);
        private IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        private string receivedData;
        private byte[] receiveByteArray;

        public ListenerThread(List<Client> clientRequests)
        {
            this.clientRequests = clientRequests;
        }

        internal void listen()
        {
            try
            {
                Console.WriteLine("Startet listening on " + groupEP.Address);
                while (true)
                {
                    receiveByteArray = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    receivedData = Encoding.ASCII.GetString(receiveByteArray, 0, receiveByteArray.Length);
                    Console.WriteLine(receivedData);
                    handleIncoming(groupEP, receivedData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            listener.Close();
        }

        private void handleIncoming(IPEndPoint clientEndPoint, string receivedData)
        {
            string[] keyValuePairs = receivedData.Split(';');
            string requestType = keyValuePairs[0];
            switch (requestType)
            {
                case "CLIENT":
                    handleClientRequest(clientEndPoint, keyValuePairs);
                    break;
                case "DATA":
                    //handleData(keyValuePairs);
                    break;
                default:
                    Console.WriteLine("Couldn't recognize the request from " + clientEndPoint + ": " + requestType);
                    break;
            }
        }

        private void handleClientRequest(IPEndPoint clientEndPoint, string[] clientRequest)
        {
            bool first = true;
            Client newClient = new Client(clientEndPoint);
            foreach (string keyValue in clientRequest)
            {
                if (first) { first = false; continue; } //skip first
                string key = keyValue.Split(':')[0];
                string value = keyValue.Split(':')[1];

                switch (key)
                {
                    case "REQUEST":
                        newClient.request = value;
                        
                        break;
                    default:
                        Console.WriteLine("Couldn't recognize the request from " + clientEndPoint + ": " + key);
                        break;
                }
            }
            newClient.lastUpdate = DateTime.Now;
            clientRequests.Add(newClient);
        }
    }


}
