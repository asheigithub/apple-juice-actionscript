using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public enum RunTimeScopeType
    {
        function,
        objectinstance,
        outpackagemember,
        startup,
    }

    /// <summary>
    /// 定义运行时的内存数据
    /// </summary>
    public interface IRunTimeScope
    {
        IRunTimeScope parent { get; }

        RunTimeScopeType scopeType { get; }
        
        //Dictionary<ASBinCode.ClassMethodGetter, Dictionary<rtData.rtObject, ISLOT>> dictMethods { get; }

        Dictionary<int, rtData.rtObject> static_objects { get; }

        IRunTimeValue this_pointer { get; }

        ISLOT[] memberData { get; }

        /// <summary>
        /// 程序执行栈
        /// </summary>
        IList<ISLOT> stack { get; }
        /// <summary>
        /// 程序执行栈偏移量
        /// </summary>
        int offset { get; }

        /// <summary>
        /// 所属代码块的id
        /// </summary>
        int blockId { get; }
    }
}
