using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    /// <summary>
    /// 运行时基本数据类型Number (64位浮点数)
    /// </summary>
    public sealed class rtNumber : RunTimeValueBase
    {
        
        public double value;
        public rtNumber(double v):base(RunTimeDataType.rt_number)
        {
            value = v;
        }

        
        public sealed override object Clone()
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
