using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Interface
{
    public interface IUnitIterator
    {
        List<IUnit> getUnits();
        int getIndex();
        void next();
    }
}
