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

        ISLOT[] memberData { get; }

        /// <summary>
        /// 程序执行栈
        /// </summary>
        IList<ISLOT> stack { get; }
        /// <summary>
        /// 程序执行栈偏移量
        /// </summary>
        int offset { get; }

    }
}
