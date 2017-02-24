using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtInt : IRunTimeValue
    {
        public int value;
        public rtInt(int v)
        {
            value = v;
        }

        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_int;
            }
        }

        public object Clone()
        {
            return new rtInt(value);
        }

        public override string ToString()
        {
            return value.ToString();
        }

    }
}
