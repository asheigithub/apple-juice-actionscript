using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpCast
    {
        public static void execCast( Player player, ASBinCode.OpStep step,  ASBinCode.IRunTimeScope scope)
        {

            //ASBinCode.IRunTimeValue output = CastValue(
            //    step.arg1.getValue( scope), step.regType , player, step.token, scope);

            //if (output == null)
            //{
            //    player.throwCastException(step.token, step.arg1.valueType, step.regType);
            //}
            //else
            //{
            //    step.reg.getISlot(scope).setValue(output);
            //}

            if (!CastValue(
                step.arg1.getValue(scope), step.regType, step.reg.getISlot(scope), player, step.token, scope))
            {
                player.throwCastException(step.token, step.arg1.valueType, step.regType);
            }
            

        }

        public static bool CastValue(
            ASBinCode.IRunTimeValue srcValue,ASBinCode.RunTimeDataType targetType,
            ASBinCode.ISLOT storeto
            ,
            Player player, ASBinCode.SourceToken token,  ASBinCode.IRunTimeScope scope

            )
        {
            switch (targetType)
            {
                case ASBinCode.RunTimeDataType.rt_boolean:
                    //return TypeConverter.ConvertToBoolean(
                    //    srcValue, player, token, true
                    //);
                    storeto.setValue(
                        TypeConverter.ConvertToBoolean(
                        srcValue, player, token, true
                    )
                        );
                    return true;
                case ASBinCode.RunTimeDataType.rt_int:
                    //return TypeConverter.ConvertToInt(
                    //    srcValue, player, token, true
                    //);
                    {
                       
                        storeto.setValue(
                             TypeConverter.ConvertToInt(
                                srcValue, player, token, true
                            )
                            );

                    }
                    return true;
                case ASBinCode.RunTimeDataType.rt_uint:
                    //return TypeConverter.ConvertToUInt(
                    // srcValue, player, token, true
                    //);
                    storeto.setValue(
                        TypeConverter.ConvertToUInt(
                        srcValue, player, token, true
                    )
                        );
                    return true;
                case ASBinCode.RunTimeDataType.rt_number:

                    //return TypeConverter.ConvertToNumber(
                    // srcValue, player, token, true
                    //);
                    storeto.setValue(
                        TypeConverter.ConvertToNumber(
                        srcValue, player, token, true
                    )
                        );
                    return true;
                case ASBinCode.RunTimeDataType.rt_string:

                    //return TypeConverter.ConvertToString(
                    // srcValue, player, token, true
                    //);
                    storeto.setValue(
                        TypeConverter.ConvertToString(
                        srcValue, player, token, true
                    )
                        );
                    return true;

                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:

                    //return srcValue;
                    storeto.directSet(srcValue);
                    return true;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    return false;
            }
        }


    }
}
