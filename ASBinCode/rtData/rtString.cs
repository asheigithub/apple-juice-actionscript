using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    /// <summary>
    /// 运行时基本数据类型(String)
    /// </summary>
    public class rtString : IRunTimeValue
    {
        public string value;

        public rtString(string v)
        {
            value = v;
        }

        public RunTimeDataType rtType
        {
            get
            {
                if (value == null)
                {
                    return RunTimeDataType.rt_null;
                }
                else
                {
                    return RunTimeDataType.rt_string;
                }
            }
        }

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


        public override string ToString()
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

    }
}
