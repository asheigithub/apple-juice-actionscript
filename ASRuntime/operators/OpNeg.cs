using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpNeg
    {
        public static void execNeg(StackFrame frame, ASBinCode.OpStep step,ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v = step.arg1.getValue(scope);

            if (v.rtType != ASBinCode.RunTimeDataType.rt_number)
            {
                OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execNeg_ValueOf_Callbacker);
            }
            else
            {
                step.reg.getISlot(scope).setValue(-((ASBinCode.rtData.rtNumber)v).value);//new ASBinCode.rtData.rtNumber( -((ASBinCode.rtData.rtNumber)v).value));
                frame.endStep(step);
            }
        }

        private static void _execNeg_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execNeg_ToString_Callbacker);
            }
            else
            {

                step.reg.getISlot(scope).setValue(
                    -TypeConverter.ConvertToNumber(v1,frame,step.token)  
                    );
                frame.endStep(step);
            }
        }
        private static void _execNeg_ToString_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            step.reg.getISlot(scope).setValue(
                    -TypeConverter.ConvertToNumber(v1, frame, step.token)
                    //-((ASBinCode.rtData.rtNumber)v1).value
                    );//new ASBinCode.rtData.rtNumber( -((ASBinCode.rtData.rtNumber)v).value));
            frame.endStep(step);
        }
    }
}
