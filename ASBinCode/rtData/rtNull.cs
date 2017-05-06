using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtNull : RunTimeValueBase
    {
        public static rtNull nullptr = new rtNull();

        private rtNull():base(RunTimeDataType.rt_null) { }

        public override double toNumber()
        {
            return 0;
        }

        public sealed override  object Clone()
        {
            return nullptr;
        }

        public override string ToString()
        {
            return "rtNull";
        }

    }
}
