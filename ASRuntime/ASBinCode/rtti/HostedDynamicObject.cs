using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public sealed class HostedDynamicObject :DynamicObject,IHostedObject
    {
        public object hosted_object;
        public HostedDynamicObject(Class _class) : base(_class)
        {
        }

        public object hostedObject
        {
            get
            {
                return hosted_object;
            }
            set
            {
                hosted_object = value;
            }
        }
    }
}
