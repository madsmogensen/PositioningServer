using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer
{
    class Program
    {

        private static IEnumerable<IConnectionHandler> ConnectionHandlers = RegisterIConnectionHandlerServices();



        static void Main(string[] args)
        {
            while (true)
            {
                Update();
                break;
            }
        }

        private static void Update()
        {
            foreach(IConnectionHandler con in ConnectionHandlers)
            {
                con.Update();
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
