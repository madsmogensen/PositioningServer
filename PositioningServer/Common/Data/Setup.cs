using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public partial class SetupFacade
    {
        public class Setup : ISetup
        {
            private string id;
            private DateTime lastUsed;

            private List<IUnit> nodesRaw = new List<IUnit>();
            private List<IUnit> nodesClean = new List<IUnit>();
            private List<IUnit> anchors = new List<IUnit>();


            public Setup(string id)
            {
                this.id = id;
            }


            public IUnitIterator getIterator(string list)
            {
                switch (list.ToLower())
                {
                    case "clean":
                        return new UnitIterator(nodesClean);
                    case "anchor":
                    case "anchors":
                        return new UnitIterator(anchors);
                    case "raw":
                    default:
                        return new UnitIterator(nodesRaw);
                }
            }

            public void addRawNode(IUnit unit)
            {
                nodesRaw.Add((Unit)unit);
            }

            public void addCleanNode(IUnit unit)
            {
                nodesClean.Add(unit);
            }

            public void addAnchor(IUnit unit)
            {
                anchors.Add(unit);
            }

            public void mergeSetup(Setup otherSetup)
            {
                this.lastUsed = otherSetup.lastUsed;
                foreach (Unit unit in otherSetup.nodesRaw)
                {
                    this.addRawNode(unit);
                }
                foreach (Unit unit in otherSetup.nodesClean)
                {
                    this.addCleanNode(unit);
                }
                foreach (Unit unit in otherSetup.anchors)
                {
                    this.addAnchor(unit);
                }
            }

            void ISetup.lastUsed(DateTime newTime)
            {
                this.lastUsed = newTime;
            }

            DateTime ISetup.getLastUsed()
            {
                return lastUsed;
            }

            string ISetup.id()
            {
                return id;
            }
        }
    }
}
