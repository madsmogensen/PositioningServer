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

        PrototypeDatabase database = new PrototypeDatabase();

        public DBHandler()
        {

        }


        bool firstRun = true;
        public void update(List<Client> clients, List<Setup> setups)
        {
            //load the hardcoded setup and put into setups?? //remove later, when clients ask for specific setups by name?
            if (firstRun) { setups.Add(database.getSetup("From File")); firstRun = false; }

            foreach (Client client in clients)
            {
                if (client.setup == null || client.request != client.setup.id)
                {
                    lookup(client, setups);
                }
            }

            //if setup not used/updated in x time, consume?
            List<Setup> setupsToRemove = new List<Setup>(); 
            foreach (Setup setup in setups)
            {
                if (DateTime.Now.Subtract(setup.lastUsed).TotalHours >= 1)
                {
                    database.saveSetup(setup);
                    setupsToRemove.Add(setup);
                }
            }
            foreach (Setup setup in setupsToRemove)
            {
                setups.Remove(setup);
            }
        }

        private void lookup(Client client, List<Setup> setups)
        {
            //simple lookup; setup already loaded in memory
            foreach (Setup setup in setups)
            {
                if (client.request.Equals(setup.id))
                {
                    client.setup = setup;
                    setup.lastUsed = DateTime.Now;
                    return;
                }
            }
            //database lookup: setup not in (List)setups, look in database
            Setup temp = database.getSetup(client.request);
            client.setup = temp;
            if (temp != null) { setups.Add(temp); }
        }
    }
}
