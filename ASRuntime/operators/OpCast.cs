using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpCast
    {
        public static void execCast( Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            
            ASBinCode.IRunTimeValue output = CastValue(
                step.arg1.getValue(eaxs, scope), step.regType , player, step.token, eaxs,scope);

            if (output == null)
            {
                player.throwCastException(step.token, step.arg1.valueType, step.regType);
            }
            else
            {
                step.reg.setValue(output, eaxs, scope);
            }

        }

        public static ASBinCode.IRunTimeValue CastValue(
            ASBinCode.IRunTimeValue srcValue,ASBinCode.RunTimeDataType targetType,
            Player player, ASBinCode.SourceToken token, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope

            )
        {
            switch (targetType)
            {
                case ASBinCode.RunTimeDataType.rt_boolean:
                    return TypeConverter.ConvertToBoolean(
                        srcValue, player, token, true
                    );
                case ASBinCode.RunTimeDataType.rt_int:


                    return TypeConverter.ConvertToInt(
                        srcValue, player, token, true
                    );
                     
                case ASBinCode.RunTimeDataType.rt_uint:


                    return TypeConverter.ConvertToUInt(
                     srcValue, player, token, true
                    );
                    
                case ASBinCode.RunTimeDataType.rt_number:

                    return TypeConverter.ConvertToNumber(
                     srcValue, player, token, true
                    );

                case ASBinCode.RunTimeDataType.rt_string:

                    return TypeConverter.ConvertToString(
                     srcValue, player, token, true
                    );

                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:

                    return srcValue;
                    
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    return null;
            }
        }


    }
}
