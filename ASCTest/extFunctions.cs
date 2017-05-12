using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASCTest.regNativeFunctions;
using ASRuntime.nativefuncs;

namespace ASCTest
{
    class extFunctions : INativeFunctionRegister
    {
        public void registrationFunction(CSWC bin)
        {
            bin.regNativeFunction(new enumitem_create());
            bin.regNativeFunction(new enumitem_tostring());
            bin.regNativeFunction(new enumitem_valueof());

            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_Int64_creator__",default(long)) );// new system_int64_creator());
            bin.regNativeFunction(LinkSystem_Buildin.getToString<long>("_system_Int64_toString"));

            bin.regNativeFunction(new system_int64_explicit_from());
            bin.regNativeFunction(new system_int64_ctor());
            bin.regNativeFunction(new system_int64_valueOf());


            bin.regNativeFunction(LinkSystem_Buildin.getFieldLinkObjGetter<long>("_system_Int64_MaxValue_getter", "_system_Int64_MaxValue", default(long)));//new system_int64_MaxValue_getter());
            bin.regNativeFunction(new system_int64_MinValue_getter());

        }
    }
}
