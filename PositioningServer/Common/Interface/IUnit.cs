using PositioningServer.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Interface
{
    public interface IUnit
    {
        void addCoordinate(Coordinate coordinate);
        int size();
        Coordinate getCoordinate(int i);
        string getId();
    }
}
