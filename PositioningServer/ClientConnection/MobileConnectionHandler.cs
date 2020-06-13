using PositioningServer.Common.Data;
using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PositioningServer.ConnectionHandler
{
    public class MobileConnectionHandler : IConnectionHandler
    {
        private UDPConnection server = new UDPConnection();

        public void update(List<Client> clients, SetupFacade setupFacade)
        {
            server.update(clients, setupFacade);
        }
    }
}
