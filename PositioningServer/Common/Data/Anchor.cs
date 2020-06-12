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

            public override void addCoordinate(Coordinate coordinate)
            {
                anchorCoordinate = coordinate;
            }
            
            internal override int size()
            {
                return 1;
            }

            internal override Coordinate getCoordinate(int i)
            {
                return anchorCoordinate;
            }
        }
    }
}
