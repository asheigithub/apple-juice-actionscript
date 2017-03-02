using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpCast
    {
        public static void execCast( StackFrame frame, ASBinCode.OpStep step,  ASBinCode.IRunTimeScope scope)
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
                step.arg1.getValue(scope), step.regType, step.reg.getISlot(scope), frame, step.token, scope))
            {
                frame.throwCastException(step.token, step.arg1.getValue(scope).rtType, step.regType);
            }
            

        }

        public static bool CastValue(
            ASBinCode.IRunTimeValue srcValue,ASBinCode.RunTimeDataType targetType,
            ASBinCode.ISLOT storeto
            ,
            StackFrame frame, ASBinCode.SourceToken token,  ASBinCode.IRunTimeScope scope

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
                        srcValue, frame, token, true
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
                                srcValue, frame, token, true
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
                        srcValue, frame, token, true
                    )
                        );
                    return true;
                case ASBinCode.RunTimeDataType.rt_number:

                    //return TypeConverter.ConvertToNumber(
                    // srcValue, player, token, true
                    //);
                    storeto.setValue(
                        TypeConverter.ConvertToNumber(
                        srcValue, frame, token, true
                    )
                        );
                    return true;
                case ASBinCode.RunTimeDataType.rt_string:

                    //return TypeConverter.ConvertToString(
                    // srcValue, player, token, true
                    //);
                    storeto.setValue(
                        TypeConverter.ConvertToString(
                        srcValue, frame, token, true
                    )
                        );
                    return true;

                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                case ASBinCode.RunTimeDataType.fun_void:
                    //return srcValue;
                    storeto.directSet(srcValue);
                    return true;

                case ASBinCode.RunTimeDataType.rt_function:
                    {
                        if (srcValue.rtType == ASBinCode.RunTimeDataType.rt_function
                            ||
                            srcValue.rtType == ASBinCode.RunTimeDataType.rt_null
                            ||
                            srcValue.rtType == ASBinCode.RunTimeDataType.rt_void
                            )
                        {
                            storeto.directSet(srcValue);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    return false;
            }
        }


    }
}
