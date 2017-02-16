using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 操作符
    /// </summary>
    public enum OpCode
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        cast=0,
        /// <summary>
        /// 赋值 =
        /// </summary>
        assigning=1,

        /// <summary>
        /// 加法+
        /// </summary>
        add =2,
        add_number=3,
        add_string=4,

        /// <summary>
        /// 减法-
        /// </summary>
        sub =5,
        sub_number=6,

        /// <summary>
        /// 取反-
        /// </summary>
        neg =7,
        /// <summary>
        /// 大于 >
        /// </summary>
        gt =8,
        /// <summary>
        /// 小于 &lt;
        /// </summary>
        lt=9,
        /// <summary>
        /// 大于等于 >=
        /// </summary>
        ge =10,
        /// <summary>
        /// 小于等于 &lt;=
        /// </summary>
        le =11,
    }
}
