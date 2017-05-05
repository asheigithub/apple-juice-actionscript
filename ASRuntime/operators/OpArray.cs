using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpArray
    {
        public static void exec_create(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            //ASBinCode.rtData.rtArray array = (ASBinCode.rtData.rtArray)step.reg.getValue(scope);

            ////防止引用了StackSlot中的值类型，因此这里需要Clone()
            //array.innerArray.Add((ASBinCode.IRunTimeValue)step.arg1.getValue(scope).Clone());

            //frame.endStep(step);


            step.reg.getSlot(scope).directSet(new ASBinCode.rtData.rtArray());

            frame.endStep(step);

        }


        public static void exec_Push(Player player,StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.rtData.rtArray array = (ASBinCode.rtData.rtArray)step.reg.getValue(scope);

            //防止引用了StackSlot中的值类型，因此这里需要Clone()
            array.innerArray.Add((ASBinCode.RunTimeValueBase)step.arg1.getValue(scope).Clone());

            frame.endStep(step);
        }
    }
}
