using PositioningServer.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.ConnectionHandler
{
    public class UDPConnection
    {

        private UDPIncoming incoming = new UDPIncoming();
        private UDPOutgoing outgoing = new UDPOutgoing();

        public void update(List<Client> clients, SetupFacade setupFacade)
        {
            incoming.update(clients, setupFacade);
            outgoing.update(clients);
        }

    }
}
