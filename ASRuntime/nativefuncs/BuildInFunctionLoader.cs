using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    public class BuildInFunctionLoader
    {
        public static void loadBuildInFunctions(ASBinCode.CSWC bin)
        {
            bin.regNativeFunction(new Int_toPrecision());
            bin.regNativeFunction(new Int_toExponential());
            bin.regNativeFunction(new Int_toFixed());
            bin.regNativeFunction(new Int_toString());

            bin.regNativeFunction(new UInt_toPrecision());
            bin.regNativeFunction(new UInt_toExponential());
            bin.regNativeFunction(new UInt_toFixed());
            bin.regNativeFunction(new UInt_toString());

            bin.regNativeFunction(new Number_toPrecision());
            bin.regNativeFunction(new Number_toExponential());
            bin.regNativeFunction(new Number_toFixed());
            bin.regNativeFunction(new Number_toString());

            bin.regNativeFunction(new Number_pow());
            bin.regNativeFunction(new Number_random());
            bin.regNativeFunction(new Number_round());
            bin.regNativeFunction(new Number_sin());
            bin.regNativeFunction(new Number_sqrt());
            bin.regNativeFunction(new Number_tan());
            bin.regNativeFunction(new Number_abs());
            bin.regNativeFunction(new Number_acos());
            bin.regNativeFunction(new Number_asin());
            bin.regNativeFunction(new Number_atan());
            bin.regNativeFunction(new Number_atan2());
            bin.regNativeFunction(new Number_ceil());
            bin.regNativeFunction(new Number_cos());
            bin.regNativeFunction(new Number_exp());
            bin.regNativeFunction(new Number_floor());
            bin.regNativeFunction(new Number_log());

            bin.regNativeFunction(new Function_fill());
            bin.regNativeFunction(new Function_load());
            bin.regNativeFunction(new Function_apply());

            bin.regNativeFunction(new Array_constructor());
            bin.regNativeFunction(new Array_fill());
            bin.regNativeFunction(new Array_load());
            bin.regNativeFunction(new Array_getLength());
            bin.regNativeFunction(new Array_setLength());
            bin.regNativeFunction(new Array_insertAt());
            bin.regNativeFunction(new Array_join());
            bin.regNativeFunction(new Array_pop());
            bin.regNativeFunction(new Array_push());
            bin.regNativeFunction(new Array_removeAt());
        }

    }
}
