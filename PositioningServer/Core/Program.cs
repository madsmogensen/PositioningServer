using PositioningServer.Common.Data;
using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer
{
    public static class Program
    {
        private static IConnectionHandler[] connectionHandlers = RegisterIConnectionHandlerServices();
        private static IDatabaseHandler[] databaseHandlers = RegisterIDatabaseHandlerServices();
        private static List<Client> clients = new List<Client>();
        private static SetupFacade setupFacade = SetupFacade.Instance;
            
        static void Main(string[] args)
        {
            while (true)
            {
                update();
            }
        }

        public static void update()
        {
            foreach (IConnectionHandler service in connectionHandlers)
            {
                service.update(clients, setupFacade);
            }
            foreach (IDatabaseHandler service in databaseHandlers)
            {
                service.update(clients, setupFacade);
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
        
        public static IConnectionHandler[] getConnectionHandlers()
        {
            return connectionHandlers;
        }

        public static void setConnectionHandlers(IConnectionHandler[] connectionHandlers)
        {
            Program.connectionHandlers = connectionHandlers;
        }
        public static IDatabaseHandler[] getDatabaseHandlers()
        {
            return databaseHandlers;
        }

        public static void setDatabaseHandlers(IDatabaseHandler[] databaseHandlers)
        {
            Program.databaseHandlers = databaseHandlers;
        }

        public static List<Client> getClients()
        {
            return clients;
        }

        public static SetupFacade getSetupFacade()
        {
            return setupFacade;
        }
    }
}
