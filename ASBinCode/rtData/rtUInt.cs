using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtUInt : IRunTimeValue
    {
        
        public uint value;
        public rtUInt(uint v)
        {
            value = v;
        }

        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_uint;
            }
        }

        public object Clone()
        {
            return new rtUInt(value);
        }

        public override string ToString()
        {
            return value.ToString();
        }

    }
}
