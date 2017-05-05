using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public sealed class HostedDynamicObject :DynamicObject
    {
        public object hosted_object;
        public HostedDynamicObject(Class _class) : base(_class)
        {
        }

    }
}
