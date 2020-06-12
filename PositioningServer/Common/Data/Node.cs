using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public partial class SetupFacade
    {
        protected class Node : Unit
        {
            private List<Coordinate> coordinates { get; set; } = new List<Coordinate>();

            public Node(string id)
            {
                this.id = id;
            }

            public override void addCoordinate(Coordinate coordinate)
            {
                coordinates.Add(coordinate);
            }

            internal override int size()
            {
                return coordinates.Count;
            }

            internal override Coordinate getCoordinate(int i)
            {
                return coordinates[i];
            }

        }
    }
}
