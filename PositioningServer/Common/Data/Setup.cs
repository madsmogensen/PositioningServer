using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                        IUnitIterator clean = new UnitIterator(nodesClean);
                        //Console.WriteLine("clean iterator count in setup class: " + clean.getUnits().Count);
                        return clean;
                    case "anchor":
                    case "anchors":
                        IUnitIterator itAnchors = new UnitIterator(anchors);
                        //Console.WriteLine("anchors iterator count in setup class: " + itAnchors.getUnits().Count);
                        return itAnchors;
                    case "raw":
                    default:
                        IUnitIterator raw = new UnitIterator(nodesRaw);
                        Console.WriteLine("raw iterator count in setup class: " + raw.getUnits().Count);
                        return raw;
                }
            }

            public void addRawNode(IUnit unit)
            {
                foreach (IUnit currentUnit in nodesRaw)
                {
                    if (currentUnit.getId().Equals(unit.getId()))
                    {
                        currentUnit.addCoordinate(unit.getCoordinate(0));
                        return;
                    }
                }
                nodesRaw.Add((Unit)unit);
                Console.WriteLine("rawNodes list is now size: " + nodesRaw.Count);
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
