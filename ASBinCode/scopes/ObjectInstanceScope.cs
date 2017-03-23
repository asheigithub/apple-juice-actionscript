using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.scopes
{
    /// <summary>
    /// 实例对象Scope
    /// </summary>
    public class ObjectInstanceScope :ScopeBase
    {
        public ASBinCode.rtti.Class _class;
        public ObjectInstanceScope( rtti.Class _class )
        {
            this._class = _class;
        }
    }
}
