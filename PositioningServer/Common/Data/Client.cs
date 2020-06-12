using PositioningServer.Common.Interface;
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
        public string setup { get; set; } = "";
        public string request { get; set; } = "";
        public DateTime lastUpdate { get; set; } = DateTime.Now;
        private List<IUnitIterator> iterators = new List<IUnitIterator>();


        public Client(IPEndPoint connection)
        {
            this.connection = connection;
        }

        public void toStringDebug()
        {
            if (setup == null) { Console.WriteLine("setup is null?"); } else
            {
                Console.WriteLine("Setup: " + setup); //Currently toStringDebug is only called if IPEndPoint is null in UDPOutgoing
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

        public List<IUnitIterator> getSetup()
        {
            if (iterators.Count == 0)
            {
                iterators.Add(SetupFacade.Instance.getSetup(setup).getIterator("raw"));
                iterators.Add(SetupFacade.Instance.getSetup(setup).getIterator("clean"));
                iterators.Add(SetupFacade.Instance.getSetup(setup).getIterator("anchors"));
            }
            return iterators;
        }
    }
}
