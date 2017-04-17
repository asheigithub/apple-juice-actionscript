using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public sealed class HostedObject : Object
    {
        public object hosted_object;
        public HostedObject(Class _class):base(_class)
        {
        }
    }
}
