using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 运行时异常基类
    /// </summary>
    public class ASRunTimeException :Exception
    {
        public ASRunTimeException() { }
        public ASRunTimeException(string msg) : base(msg) { }

        public ASRunTimeException(string msg, Exception innerException) : base(msg, innerException) { }

    }
}
