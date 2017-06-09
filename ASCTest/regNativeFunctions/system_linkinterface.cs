using ASBinCode;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_linkinterface
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_ienumerator_creator_", default(System.Collections.IEnumerator)));

            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_ienumerable_creator_", default(System.Collections.IEnumerable)));
            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_icollectinos_creator_", default(System.Collections.ICollection)));

            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_ilist_creator_", default(System.Collections.IList)));


        }
    }
}
