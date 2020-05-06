using PositioningServer.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Interface
{
    public interface IDatabaseHandler
    {
        void update(List<Client> clients, List<Setup> setups);
    }
}
