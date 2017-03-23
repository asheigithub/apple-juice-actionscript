using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    /// <summary>
    /// 保存动态属性的插槽
    /// </summary>
    class DynamicPropertySlot : ObjectMemberSlot
    {
        public DynamicPropertySlot(ASBinCode.rtData.rtObject obj):base(obj)
        {

        }

        internal string _propname;
    }
}
