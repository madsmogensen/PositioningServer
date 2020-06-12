using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Interface
{
    public interface ISetup
    {
        void lastUsed(DateTime newTime);
        DateTime getLastUsed();
        string id();
        void addRawNode(IUnit newNode);
        void addCleanNode(IUnit newNode);
        void addAnchor(IUnit newNode);
        IUnitIterator getIterator(string list);


    }
}
