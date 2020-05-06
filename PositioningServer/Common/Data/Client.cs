using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public class Client
    {

        public IPEndPoint connection { get; set; }
        public Setup setup { get; set; } = null;
        public string request { get; set; } = "";
        public DateTime lastUpdate { get; set; } = DateTime.Now;


        public Client(IPEndPoint connection)
        {
            this.connection = connection;
        }

        public void toStringDebug()
        {
            if (setup == null) { Console.WriteLine("setup is null?"); } else
            {
                Console.WriteLine("Setup: " + setup.id); //Currently toStringDebug is only called if IPEndPoint is null in UDPOutgoing
            }
            Console.WriteLine("Request: " + this.request);
            /*foreach (Node node in this.setup.nodesClean)
            {
                foreach (Coordinate coordinate in node.coordinates)
                {
                    Console.WriteLine("Coordinate: " + coordinate.ToString());
                }
            }*/
        }
    }
}
