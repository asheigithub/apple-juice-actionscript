using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtNull : IRunTimeValue
    {
        public static rtNull nullptr = new rtNull();

        private rtNull() { }

        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_null;
            }
        }

        public object Clone()
        {
            return nullptr;
        }

        public override string ToString()
        {
            return "rtNull";
        }

    }
}
