using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAssigning
    {
        public static void execAssigning(Player player, ASBinCode.OpStep step ,ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v = step.arg1.getValue(scope);
            step.reg.getISlot(scope).directSet(v);

        }

    }
}
