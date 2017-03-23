using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpNeg
    {
        public static void execNeg(StackFrame frame, ASBinCode.OpStep step,ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v = step.arg1.getValue(scope);
            step.reg.getISlot(scope).setValue(-((ASBinCode.rtData.rtNumber)v).value);//new ASBinCode.rtData.rtNumber( -((ASBinCode.rtData.rtNumber)v).value));
            frame.endStep(step);
        }
    }
}
