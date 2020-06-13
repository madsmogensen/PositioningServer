using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PositioningServer;
using Moq;
using PositioningServer.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel;

namespace PositioningServerTests
{
    [TestClass]
    public class UnitCore
    {
        [TestMethod]
        public void TestUpdate()
        {
            //Arrange
            Program.update(); //Instantiate static Program class
            Mock<IConnectionHandler> connectionHandler = new Mock<IConnectionHandler>();
            IConnectionHandler[] connectionHandlers = new IConnectionHandler[] { connectionHandler.Object };
            Program.setConnectionHandlers(connectionHandlers);
            //Act
            Program.update();
            //Assert
            Assert.IsTrue(Program.getConnectionHandlers().Length == 1);
            Assert.IsTrue(Program.getConnectionHandlers()[0].Equals(connectionHandler.Object));
            connectionHandler.Verify(m => m.update(Program.getClients(), Program.getSetupFacade()), Times.Once);
        }
    }
}
