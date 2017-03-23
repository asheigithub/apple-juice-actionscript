using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.scopes
{
    public class FunctionScope :ScopeBase
    {
        public readonly ASBinCode.rtti.FunctionDefine function;
        public FunctionScope(rtti.FunctionDefine function)
        {
            this.function = function;
        }
    }
}
