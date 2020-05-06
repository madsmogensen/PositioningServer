using PositioningServer.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.ConnectionHandler
{
    class UDPConnection
    {

        UDPIncoming incoming = new UDPIncoming();
        UDPOutgoing outgoing = new UDPOutgoing();

        public void update(List<Client> clients, List<Setup> setups)
        {
            incoming.update(clients, setups);
            outgoing.update(clients);
        }

    }
}
