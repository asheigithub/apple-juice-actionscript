using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    /// <summary>
    /// 函数形参
    /// </summary>
    public class FunctionParameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string name;
        /// <summary>
        /// 参数类型
        /// </summary>
        public RunTimeDataType type;
        /// <summary>
        /// 默认值
        /// </summary>
        public RightValueBase defaultValue;
        /// <summary>
        /// 是否是参数数组
        /// </summary>
        public bool isPara;

    }
}
