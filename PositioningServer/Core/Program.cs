using PositioningServer.Common.Data;
using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer
{
    public static class Program
    {
        private static IConnectionHandler[] connectionHandlers = RegisterIConnectionHandlerServices();
        private static IDatabaseHandler[] databaseHandlers = RegisterIDatabaseHandlerServices();
        private static List<Client> clients = new List<Client>();
        private static List<Setup> setups = new List<Setup>();

        static void Main(string[] args)
        {
            //Test case
            IPEndPoint testEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
            Client testClient = new Client(testEndPoint);
            testClient.request = "From File";
            clients.Add(testClient);



            while (true)
            {
                update();
            }
        }

        private static void update()
        {
            foreach (IConnectionHandler service in connectionHandlers)
            {
                service.update(clients, setups);
            }
            foreach (IDatabaseHandler service in databaseHandlers)
            {
                service.update(clients, setups);
            }
        }

        private static IConnectionHandler[] RegisterIConnectionHandlerServices()
        {
            var interfaceType = typeof(IConnectionHandler);
            var all = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as IConnectionHandler);
            return all.ToArray();
        }

        private static IDatabaseHandler[] RegisterIDatabaseHandlerServices()
        {
            var interfaceType = typeof(IDatabaseHandler);
            var all = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as IDatabaseHandler);
            return all.ToArray();
        }
    }
}
