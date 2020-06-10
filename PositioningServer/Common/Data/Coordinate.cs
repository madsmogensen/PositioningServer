using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public class Coordinate
    {

        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public DateTime dateTime { get; set; }

        public Coordinate(int x, int y, int z, DateTime dateTime)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.dateTime = dateTime;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("x: " + x + ", ");
            sb.Append("y: " + y + ", ");
            sb.Append("z: " + z + ", ");
            sb.Append("Time: " + dateTime);
            return sb.ToString();
        }
    }
}
