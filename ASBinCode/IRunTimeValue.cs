using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 标记是一个运行时的值
    /// </summary>
    public interface IRunTimeValue:ICloneable
    {
        RunTimeDataType rtType { get; }

        

    }
}
