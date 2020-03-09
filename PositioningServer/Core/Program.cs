using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer
{
    public static class Program
    {

        private static IEnumerable<IConnectionHandler> connectionHandlers = RegisterIConnectionHandlerServices();

        static void Main(string[] args)
        {

            foreach (IConnectionHandler service in connectionHandlers) { service.Instantiate(); }

            int i = 0;
            while (true)
            {
                Update();
                i++;
                //if (i > 10) { Console.ReadLine(); break; }
            }
        }

        private static void Update()
        {
            foreach (IConnectionHandler service in connectionHandlers)
            {
                service.Update();
            }
        }

        private static IEnumerable<IConnectionHandler> RegisterIConnectionHandlerServices()
        {
            var interfaceType = typeof(IConnectionHandler);
            var all = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as IConnectionHandler);
            return all;
        }
    }
}
