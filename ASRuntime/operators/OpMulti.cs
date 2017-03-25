using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpMulti
    {
        public static void execMulti(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
                frame._tempSlot1, frame._tempSlot2, step, _execMulti_CallBacker);
        }

        private static void _execMulti_CallBacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);

            double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

            {
                step.reg.getISlot(scope).setValue(n1 * n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }

        public static void execDiv(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
               frame._tempSlot1, frame._tempSlot2, step, _execDiv_CallBacker);

        }

        private static void _execDiv_CallBacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);

            double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

            {
                step.reg.getISlot(scope).setValue(n1 / n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }

        public static void execMod(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
               frame._tempSlot1, frame._tempSlot2, step, _execMod_CallBacker);

        }

        private static void _execMod_CallBacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);

            double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

            {
                step.reg.getISlot(scope).setValue(n1 % n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }

    }

}
