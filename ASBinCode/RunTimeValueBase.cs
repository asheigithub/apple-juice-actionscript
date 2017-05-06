using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 标记是一个运行时的值
    /// </summary>
    public abstract class RunTimeValueBase:ICloneable
    {
        public readonly RunTimeDataType  rtType;

        public RunTimeValueBase(RunTimeDataType rtType)
        {
            this.rtType = rtType;
        }

        public abstract object Clone();



        public abstract double toNumber();

    }
}
