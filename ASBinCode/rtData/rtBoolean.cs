using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtBoolean : IRunTimeValue
    {
        public static readonly rtBoolean False = new rtBoolean(false);
        public static readonly rtBoolean True = new rtBoolean(true);

        public bool value;
        private rtBoolean(bool v)
        {
            value = v;
        }

        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_boolean;
            }
        }

        public object Clone()
        {
            if (value)
            {
                return True;
            }
            else
            {
                return False;
            }
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
