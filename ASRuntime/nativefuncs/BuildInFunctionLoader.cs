using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    public class BuildInFunctionLoader
    {
        public static void loadBuildInFunctions(ASBinCode.CSWC bin)
        {
            bin.regNativeFunction(new __buildin__ismethod());
            bin.regNativeFunction(new __buildin__trace());
            bin.regNativeFunction(new __buildin__isnan());

            bin.regNativeFunction(new Object_toString());
            //bin.regNativeFunction(new Object_init());

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
            bin.regNativeFunction(new Function_call());
            bin.regNativeFunction(new Function_setPrototype());

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
            bin.regNativeFunction(new Array_reverse());
            bin.regNativeFunction(new Array_shift());
            bin.regNativeFunction(new Array_slice());
            bin.regNativeFunction(new Array_splice());
            bin.regNativeFunction(new Array_unshift());
            bin.regNativeFunction(new Array_concat());
            bin.regNativeFunction(new Array_toString());

            bin.regNativeFunction(new Boolean_toString());

            bin.regNativeFunction(new String_length());
            bin.regNativeFunction(new String_charAt());
            bin.regNativeFunction(new String_charCodeAt());
            bin.regNativeFunction(new String_fromCharCode());
            bin.regNativeFunction(new String_indexOf());
            bin.regNativeFunction(new String_lastindexOf());
            bin.regNativeFunction(new String_slice());
            bin.regNativeFunction(new String_split());
            bin.regNativeFunction(new String_substr());
            bin.regNativeFunction(new String_substring());
            bin.regNativeFunction(new String_tolower());
            bin.regNativeFunction(new String_toupper());
            bin.regNativeFunction(new String_replace());

            bin.regNativeFunction(new Vector_constructor());
            bin.regNativeFunction(new Vector_getIsFixed());
            bin.regNativeFunction(new Vector_setIsFixed());
            bin.regNativeFunction(new Vector_getLength());
            bin.regNativeFunction(new Vector_setLength());
            bin.regNativeFunction(new Vector_toString());
            bin.regNativeFunction(new Vector__concat());
            bin.regNativeFunction(new Vector_insertAt());
            bin.regNativeFunction(new Vector_join());
            bin.regNativeFunction(new Vector_pop());
            bin.regNativeFunction(new Vector_removeAt());
            bin.regNativeFunction(new Vector_reverse());
            bin.regNativeFunction(new Vector_shift());
            bin.regNativeFunction(new Vector_slice());
            bin.regNativeFunction(new Vector_splice());
            bin.regNativeFunction(new Vector_push());


            bin.regNativeFunction(new Date_constructor());
            bin.regNativeFunction(new Date_getdate());
            bin.regNativeFunction(new Date_setdate());
            bin.regNativeFunction(new Date_valueof());
            bin.regNativeFunction(new Date_tostring());
            bin.regNativeFunction(new Date_totimestring());
            bin.regNativeFunction(new Date_toLocalTimeString());
            bin.regNativeFunction(new Date_toUTCstring());
            bin.regNativeFunction(new Date_toLocalString());
            bin.regNativeFunction(new Date_toLocalDateString());

            bin.regNativeFunction(new Error_getstack());

        }

    }
}
