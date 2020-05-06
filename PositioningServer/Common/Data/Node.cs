using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public class Node
    {

        public string id { get; set; }
        public List<Coordinate> coordinates { get; set; } = new List<Coordinate>();
        public int indexSent { get; set; } = 0;

        public Node(string id)
        {
            this.id = id;
        }
    }
}
