using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public sealed class rtInt : RunTimeValueBase
    {
        public int value;
        public rtInt(int v):base(RunTimeDataType.rt_int)
        {
            value = v;
        }

        public override double toNumber()
        {
            return value;
        }

        public sealed override  object Clone()
        {
            return new rtInt(value);
        }

        public override string ToString()
        {
            return value.ToString();
        }

    }
}
