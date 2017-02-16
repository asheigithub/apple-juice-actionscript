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
        public static readonly rtNumber zero = new rtNumber(0);


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

        public override string ToString()
        {
            return value.ToString();
        }

    }
}
