using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpLinkOutPackageScope
    {
        public static void exec_link(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            StackSlot l = (StackSlot)step.reg.getISlot(scope);

            int classid = ((ASBinCode.rtData.rtInt)step.arg2.getValue(scope)).value;

           

            var outscope = player.outpackage_runtimescope[classid];

            ISLOT outpackagescopeslot= ((VariableBase)step.arg1).getISlot(outscope);

            l.linkTo(outpackagescopeslot);

            frame.endStep(step);
        }
    }
}
