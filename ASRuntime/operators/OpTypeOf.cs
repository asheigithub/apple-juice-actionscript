using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpTypeOf
    {
        private static RunTimeValueBase OBJECT = new rtString("object");
        private static RunTimeValueBase BOOLEAN = new rtString("boolean");
        private static RunTimeValueBase NUMBER = new rtString("number");
        private static RunTimeValueBase STRING = new rtString("string");
        private static RunTimeValueBase FUNCTION = new rtString("function");
        private static RunTimeValueBase UNDEFINED = new rtString("undefined");

        public static void exec_TypeOf(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v1 = step.arg1.getValue(scope);
            if (v1.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(((rtObject)v1).value._class, out ot))
                {
                    v1 = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)v1);
                }
            }


            if (v1.rtType == RunTimeDataType.rt_null
                ||
                v1.rtType == RunTimeDataType.rt_array
                ||
                v1.rtType > RunTimeDataType.unknown
                )
            {
                step.reg.getISlot(scope).directSet(OBJECT);
            }
            else if (v1.rtType == RunTimeDataType.rt_boolean)
            {
                step.reg.getISlot(scope).directSet(BOOLEAN);
            }
            else if (v1.rtType == RunTimeDataType.rt_int ||
                    v1.rtType == RunTimeDataType.rt_uint ||
                    v1.rtType == RunTimeDataType.rt_number
                )
            {
                step.reg.getISlot(scope).directSet(NUMBER);
            }
            else if (v1.rtType == RunTimeDataType.rt_string
                )
            {
                step.reg.getISlot(scope).directSet(STRING);
            }
            else if (v1.rtType == RunTimeDataType.rt_function)
            {
                step.reg.getISlot(scope).directSet(FUNCTION);
            }
            else if (v1.rtType == RunTimeDataType.rt_void)
            {
                step.reg.getISlot(scope).directSet(UNDEFINED);
            }
            else if (v1.rtType == RunTimeDataType.unknown
                ||
                v1.rtType == RunTimeDataType.fun_void
                )
            {
                frame.throwCastException(step.token, v1.rtType, RunTimeDataType.fun_void);
            }
            

            frame.endStep(step);
        }
    }
}
