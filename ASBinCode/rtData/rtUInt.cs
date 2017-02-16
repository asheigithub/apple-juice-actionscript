using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtUInt : IRunTimeValue
    {
        public static readonly rtUInt zero = new rtUInt(0);


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

        public override string ToString()
        {
            return value.ToString();
        }

    }
}
