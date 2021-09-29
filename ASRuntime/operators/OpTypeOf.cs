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
            var v1 = step.arg1.getValue(scope, frame);
            if (v1.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(((rtObjectBase)v1).value._class, out ot))
                {
                    v1 = TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)v1);
                }
            }


            if (v1.rtType == RunTimeDataType.rt_null
                ||
                v1.rtType == RunTimeDataType.rt_array
                ||
                v1.rtType > RunTimeDataType.unknown
                )
            {
                step.reg.getSlot(scope, frame).directSet(OBJECT);
            }
            else if (v1.rtType == RunTimeDataType.rt_boolean)
            {
                step.reg.getSlot(scope, frame).directSet(BOOLEAN);
            }
            else if (v1.rtType == RunTimeDataType.rt_int ||
                    v1.rtType == RunTimeDataType.rt_uint ||
                    v1.rtType == RunTimeDataType.rt_number
                )
            {
                step.reg.getSlot(scope, frame).directSet(NUMBER);
            }
            else if (v1.rtType == RunTimeDataType.rt_string
                )
            {
                step.reg.getSlot(scope, frame).directSet(STRING);
            }
            else if (v1.rtType == RunTimeDataType.rt_function)
            {
                step.reg.getSlot(scope, frame).directSet(FUNCTION);
            }
            else if (v1.rtType == RunTimeDataType.rt_void)
            {
                step.reg.getSlot(scope, frame).directSet(UNDEFINED);
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
