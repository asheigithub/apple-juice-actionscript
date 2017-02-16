using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.error
{
    /// <summary>
    /// 内部操作错误
    /// </summary>
    public class InternalError
    {
        public ASBinCode.SourceToken token;

        public string message;

        public int errorCode;

        public InternalError(ASBinCode.SourceToken token,string message)
        {
            this.token = token;
            this.message = message;
        }

    }
}
