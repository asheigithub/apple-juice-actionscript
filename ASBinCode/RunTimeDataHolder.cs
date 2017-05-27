using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public abstract class RunTimeDataHolder
    {
        public Dictionary<int, ASBinCode.rtData.rtObject> static_objects;
        public int offset;
        public SLOT[] stack;
    }
}
