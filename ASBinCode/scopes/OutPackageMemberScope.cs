using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.scopes
{
    /// <summary>
    /// 包外代码的Scope
    /// </summary>
    public class OutPackageMemberScope :ScopeBase
    {
        public readonly rtti.Class mainclass;
        public OutPackageMemberScope(rtti.Class mainclass)
        {
            this.mainclass = mainclass;
        }
    }
}
