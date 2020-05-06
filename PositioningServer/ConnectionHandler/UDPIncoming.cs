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
        List<Client> clientRequests = new List<Client>();
        List<Setup> setupData = new List<Setup>();

        public UDPIncoming()
        {
            ListenerThread listener = new ListenerThread(clientRequests, setupData);
            Thread t = new Thread(new ThreadStart(listener.listen));
            t.Start();
        }


        public void update(List<Client> clients, List<Setup> setups)
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
            foreach (Setup data in setupData)
            {
                //Handle new input data
            }
            //put new clients to clients if absent
            //add new data to setups (create new if absent)
        }
    }

    public class ListenerThread
    {

        List<Client> clientRequests;
        List<Setup> setupData;

        private const int listenPort = 11000;
        private UdpClient listener = new UdpClient(listenPort);
        private IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        private string receivedData;
        private byte[] receiveByteArray;

        public ListenerThread(List<Client> clientRequests, List<Setup> setupData)
        {
            this.clientRequests = clientRequests;
            this.setupData = setupData;
        }

        public void listen()
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
