using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtInt : IRunTimeValue
    {
        public static readonly rtInt zero = new rtInt(0);

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

        public override string ToString()
        {
            return value.ToString();
        }

    }
}
