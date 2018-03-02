using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public sealed class HostedObject : Object,IHostedObject
    {
        public object hosted_object;
        public HostedObject(Class _class):base(_class)
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
