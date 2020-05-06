using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public class Setup
    {
        public List<Node> nodesRaw { get; set; } = new List<Node>();
        public List<Node> nodesClean { get; set; } = new List<Node>();
        public int cleanIndex { get; set; } = 0; //if cleanIndex = nodesRaw.Count-1, then all the data has been processed
        public List<Anchor> anchors { get; set; } = new List<Anchor>();
        public string id; //name?
        public DateTime lastUsed { get; set; }

    public Setup(string name)
        {
            this.id = name;
        }
    }
}
