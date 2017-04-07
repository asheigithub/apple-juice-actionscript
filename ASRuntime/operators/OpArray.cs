using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpArray
    {
        public static void exec_Push(Player player,StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtArray array = (ASBinCode.rtData.rtArray)step.reg.getValue(scope);

            array.innerArray.Add(step.arg1.getValue(scope));

            frame.endStep(step);
        }
    }
}
