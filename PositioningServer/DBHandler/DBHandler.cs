using PositioningServer.Common.Data;
using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.DBHandler
{
    class DBHandler : IDatabaseHandler
    {
        public DBHandler()
        {
            //instantiate the prototypeDatabase, in order to load the track from a file
            new PrototypeDatabase();
        }
            
        public void update(List<Client> clients, SetupFacade setupFacade)
        {
            foreach (Client client in clients)
            {
                if (client.setup == null || client.request != client.setup)
                {
                    lookupClient(client, setupFacade);
                }
            }

            //if setup not used/updated in x time, consume?
            List<ISetup> setupsToRemove = new List<ISetup>(); 
            foreach (ISetup setup in setupFacade.getSetups())
            {
                if (DateTime.Now.Subtract(setup.getLastUsed()).TotalHours >= 1)
                {
                    setupsToRemove.Add(setup);
                }
            }
            foreach (ISetup setup in setupsToRemove)
            {
                setupFacade.removeSetup(setup.id());
            }
        }

        private void lookupClient(Client client, SetupFacade setupFacade)
        {
            //simple lookup; setup already loaded in memory
            client.setup = setupFacade.getSetup(client.request).id();
            setupFacade.getSetup(client.setup).lastUsed(DateTime.Now);
        }
    }
}
