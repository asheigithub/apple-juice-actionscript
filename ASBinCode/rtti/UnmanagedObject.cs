using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public sealed class UnmanagedObject : Object
    {
        public object unmanaged_object;
        public UnmanagedObject(Class _class):base(_class)
        {
        }
    }
}
