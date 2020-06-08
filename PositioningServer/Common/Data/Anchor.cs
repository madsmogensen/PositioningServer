using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public partial class SetupFacade
    {
        protected class Anchor : Unit
        {

            private Coordinate anchorCoordinate;

            internal Anchor(string id)
            {
                this.id = id;
            }

            public override void coordinate(Coordinate coordinate)
            {
                anchorCoordinate = coordinate;
            }

            internal override Coordinate getCoordinate(int i)
            {
                return anchorCoordinate;
            }

            internal override int size()
            {
                return 1;
            }
        }
    }
}
