using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Data
{
    public partial class SetupFacade
    {
        private class SetupList
        {
            
            private Dictionary<string, Setup> setups = new Dictionary<String, Setup>();

            private static readonly SetupList instance = new SetupList();

            private SetupList() { }
            static SetupList() { }

            //Threadsafe lazy without locks
            //Static constructors in C# are specified to execute only when an instance of the class is created or a static member is referenced, and to execute only once per AppDomain.
            public static SetupList Instance()
            {
                return instance;
            }

            public Setup getSetup(String id)
            {
                if (!setups.ContainsKey(id))
                {
                    Setup newSetup = new Setup(id);
                    setups.Add(id, newSetup);
                }
                else
                {
                    if (setups[id] == null)
                    {
                        Setup newSetup = new Setup(id);
                        setups.Add(id, newSetup);
                    }
                }
                return setups[id];
            }
            
            public void remove(string id)
            {
                setups.Remove(id);
            }

            public List<Setup> asList()
            {
                return setups.Values.ToList();
            }
        }
    }
}
