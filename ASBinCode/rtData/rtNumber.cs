using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    /// <summary>
    /// 运行时基本数据类型Number (64位浮点数)
    /// </summary>
    public class rtNumber : IRunTimeValue
    {
        
        public double value;
        public rtNumber(double v)
        {
            value = v;
        }

        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_number;
            }
        }

        public object Clone()
        {
            return new rtNumber(value);
        }

        public override string ToString()
        {
            if (double.IsPositiveInfinity(value))
            {
                return "Infinity";
            }
            else if (double.IsNegativeInfinity(value))
            {
                return "-Infinity";
            }
            else if (double.IsNaN(value))
            {
                return "NaN";
            }
            else
            {
                return value.ToString();
            }
        }

    }
}
