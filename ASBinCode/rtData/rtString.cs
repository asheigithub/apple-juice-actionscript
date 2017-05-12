using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    /// <summary>
    /// 运行时基本数据类型(String)
    /// </summary>
    public sealed class rtString : RunTimeValueBase
    {
        public string value;

        public rtString(string v):base(v==null? RunTimeDataType.rt_null: RunTimeDataType.rt_string)
        {
            value = v;
        }

        public override double toNumber()
        {
            double r = 0;
            if (value == null)
            {
                return 0;// new ASBinCode.rtData.rtNumber(0);
            }
            else if (double.TryParse(value, out r))
            {
                return r;//new ASBinCode.rtData.rtNumber(r);
            }
            else
            {
                return double.NaN; //new ASBinCode.rtData.rtNumber(double.NaN);
            }
        }

        //public RunTimeDataType rtType
        //{
        //    get
        //    {
        //        if (value == null)
        //        {
        //            return RunTimeDataType.rt_null;
        //        }
        //        else
        //        {
        //            return RunTimeDataType.rt_string;
        //        }
        //    }
        //}

        public string valueString()
        {
            if (value == null)
            {
                return "null";
            }
            else
            {
                return value.ToString();
            }
        }


        public sealed override string ToString()
        {
            if (value == null)
            {
                return "null";
            }
            else
            { 
                return "\"" + value.ToString() + "\"";
            }
        }

        public sealed override  object Clone()
        {
            return this;
            //return new rtString(value);
        }
    }
}
