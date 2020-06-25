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
        private SetupList setups = SetupList.Instance();
        private UnitFactory unitFactory = new UnitFactory();

        private static readonly SetupFacade instance = new SetupFacade();

        //Threadsafe lazy without locks
        //Static constructors in C# are specified to execute only when an instance of the class is created or a static member is referenced, and to execute only once per AppDomain.
        private SetupFacade() { }
        static SetupFacade() { }

        public static SetupFacade Instance {
            get {
                return instance;
            }
        }

        public ISetup getSetup(string id)
        {
            return setups.getSetup(id);
        }

        public void newSetup(string id)
        {
            setups.addSetup(id);
        }

        public List<Setup> getSetups()
        {
            return setups.asList();
        }

        public  void removeSetup(string id)
        {
            setups.remove(id);
        }

        public void addRawNode(string setupId, Unit node)
        {
            getSetup(setupId).addRawNode(node);
        }
        public void addCleanNode(string setupId, Unit node)
        {
            getSetup(setupId).addCleanNode(node);
        }
        public void addAnchor(string setupId, Unit anchor)
        {
            getSetup(setupId).addAnchor(anchor);
        }

        public Unit makeUnit(string id)
        {
            return unitFactory.makeUnit(id);
        }

        public void mergeSetups(string id, Setup otherSetup)
        {
            if (otherSetup == null) { return; }
            Setup original = (Setup)getSetup(id);
            original.mergeSetup(otherSetup);
        }

    }
}
