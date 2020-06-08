using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public partial class SetupFacade
    {
        private class UnitFactory
        {
            public UnitFactory()
            {

            }

            public Unit makeUnit(string id)
            {
                switch (id[1])
                {
                    case 'x'://Node
                        Node newNode = new Node(id);
                        return newNode;
                    case 'z'://Anchor
                        Anchor newAnchor = new Anchor(id);
                        return newAnchor;
                    default:
                        throw new Exception("Error in UnitFactory.makeUnit, neither Node or anchor");
                }
            }
        }
    }
}
