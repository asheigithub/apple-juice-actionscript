using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASRuntime.nativefuncs;

namespace ASCTest
{
    partial class extFunctions : INativeFunctionRegister
    {
        public void registrationFunction(CSWC bin)
        {
			

   //         system_byte_buildin.regNativeFunctions(bin);
   //         system_char_buildin.regNativeFunctions(bin);
   //         system_sbyte_buildin.regNativeFunctions(bin);
   //         system_uint64_buildin.regNativeFunctions(bin);

   //         bin.regNativeFunction(new system_int64_explicit_from());
   //         bin.regNativeFunction(new system_int64_implicit_from());
   //         bin.regNativeFunction(new system_int64_ctor());
   //         bin.regNativeFunction(new system_int64_valueOf());

   //         system_collections_hashtable_buildin.regNativeFunctions(bin);
   //         system_collections_arraylist_buildin.regNativeFunctions(bin);
   //         system_collections_stack_buildin.regNativeFunctions(bin);
   //         system_collections_queue_buildin.regNativeFunctions(bin);

			//system_ICloneable_buildin.regNativeFunctions(bin);
			

			regAutoCreateCodes(bin);


        }


		

	}
}
