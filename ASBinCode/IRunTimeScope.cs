using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 定义运行时的内存数据
    /// </summary>
    public interface IRunTimeScope
    {
        IRunTimeScope parent { get; }

        IEAX[] memberData { get; }
    }
}
