using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpSub
    {
        public static void execSub_Number(StackFrame frame,ASBinCode.OpStep step,  ASBinCode.RunTimeScope scope)
        {
            //ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            //ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);
            double a1 = step.arg1.getValue(scope).toNumber();
            double a2 = step.arg2.getValue(scope).toNumber();

            step.reg.getSlot(scope).setValue(a1 - a2);//new ASBinCode.rtData.rtNumber(a1.value - a2.value));
            frame.endStep(step);
        }

        public static void execSub(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
                frame._tempSlot1, frame._tempSlot2, step, _execSub_CallBacker);

        }

        private static void _execSub_CallBacker(
            ASBinCode.RunTimeValueBase v1,ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            double n1 = TypeConverter.ConvertToNumber(v1);
            double n2 = TypeConverter.ConvertToNumber(v2);

            {
                step.reg.getSlot(scope).setValue(n1 - n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }
    }
}
