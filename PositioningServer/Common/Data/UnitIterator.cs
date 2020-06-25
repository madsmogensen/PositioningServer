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
        private class UnitIterator : IUnitIterator
        {
            private List<IUnit> units;
            //private int index = Int32.MaxValue;
            private int index = 0;

            public UnitIterator(List<IUnit> list)
            {
                units = list;
            }

            public List<IUnit> getUnits()
            {
                return units;
            }

            public void next()
            {
                //if (index + 1 > units.Count)
                //{
                    index++;
                //}
            }

            public int getIndex()
            {
                return index;
            }

            public IUnit getNext()
            {
                if (index + 1 > units.Count)
                {
                    index++;
                }
                return units[index];
            }

            public void addUnit(IUnit unit)
            {
                units.Add(unit);
            }
        }
    }
}
