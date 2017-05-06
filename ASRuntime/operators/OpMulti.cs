using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpMulti
    {

        public static void exec_MultiNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double a1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope));
            double a2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope));

            step.reg.getSlot(scope).setValue(a1 * a2);
            frame.endStep(step);

        }

        public static void exec_DivNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double a1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope));
            double a2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope));

            step.reg.getSlot(scope).setValue(a1 / a2);
            frame.endStep(step);

        }
        public static void exec_ModNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double a1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope));
            double a2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope));

            step.reg.getSlot(scope).setValue(a1 % a2);
            frame.endStep(step);

        }

        public static void execMulti(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
                frame._tempSlot1, frame._tempSlot2, step, _execMulti_CallBacker);
        }

        private static void _execMulti_CallBacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double n1 = TypeConverter.ConvertToNumber(v1);

            double n2 = TypeConverter.ConvertToNumber(v2);

            {
                step.reg.getSlot(scope).setValue(n1 * n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }

        public static void execDiv(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
               frame._tempSlot1, frame._tempSlot2, step, _execDiv_CallBacker);

        }

        private static void _execDiv_CallBacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double n1 = TypeConverter.ConvertToNumber(v1);

            double n2 = TypeConverter.ConvertToNumber(v2);

            {
                step.reg.getSlot(scope).setValue(n1 / n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }

        public static void execMod(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
               frame._tempSlot1, frame._tempSlot2, step, _execMod_CallBacker);

        }

        private static void _execMod_CallBacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double n1 = TypeConverter.ConvertToNumber(v1);

            double n2 = TypeConverter.ConvertToNumber(v2);

            {
                step.reg.getSlot(scope).setValue(n1 % n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }

    }

}
