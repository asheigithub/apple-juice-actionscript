using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
	[Serializable]
    public sealed class rtBoolean : RunTimeValueBase
    {
        public static readonly rtBoolean False = new rtBoolean(false);
        public static readonly rtBoolean True = new rtBoolean(true);

        public bool value;
        private rtBoolean(bool v):base(RunTimeDataType.rt_boolean)
        {
            value = v;
        }

        public override double toNumber()
        {
            if (value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public sealed override  object Clone()
        {
            return this;
        }

        public override string ToString()
        {
            if (value)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

    }
}
