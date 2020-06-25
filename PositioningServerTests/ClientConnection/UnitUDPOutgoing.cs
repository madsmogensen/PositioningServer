using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PositioningServer;
using PositioningServer.Common.Data;
using PositioningServer.Common.Interface;
using PositioningServer.ConnectionHandler;

namespace PositioningServerTests.ClientConnection
{
    [TestClass]
    public class UnitUDPOutgoing
    {
        [TestMethod]
        public void TestUpdate()
        {
            //Arrange
            UDPOutgoing outgoing = new UDPOutgoing();
            IPEndPoint test = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
            Client testClient = new Client(test);
            testClient.setup = "test";
            testClient.request = "CLIENT;REQUEST:From File";
            Mock<IUnitIterator> overloadedIterator = new Mock<IUnitIterator>();
            //for (int i = 0; i <= 1000000; i++)
            for (int i = 0; i <= 1000; i++)
            {
                overloadedIterator.Object.addUnit(SetupFacade.Instance.makeUnit("0x" + i));
            }
            testClient.getSetup().Add(overloadedIterator.Object);
            List<Client> clients = new List<Client>() { testClient };
            //Act
            outgoing.update(clients);
            //Assert
            overloadedIterator.Verify(m => m.getUnits(), Times.Once);
        }
    }
}
