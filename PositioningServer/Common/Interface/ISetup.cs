﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.Common.Interface
{
    public interface ISetup
    {
        DateTime lastUsed();
        string id();
        void addRawNode(IUnit newNode);


    }
}
