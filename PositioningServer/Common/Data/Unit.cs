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
        public abstract class Unit : IUnit
        {
            protected string id;

            internal abstract int size();
            
            internal abstract Coordinate getCoordinate(int i);

            public abstract void addCoordinate(Coordinate coordinate);
            
            internal string getId()
            {
                return id;
            }

            int IUnit.size()
            {
                return size();
            }

            Coordinate IUnit.getCoordinate(int i)
            {
                return getCoordinate(i);
            }

            string IUnit.getId()
            {
                return getId();
            }
        }
    }
}
