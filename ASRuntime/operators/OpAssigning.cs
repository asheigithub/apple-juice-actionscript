using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAssigning
    {
        public static void execAssigning(Player player, ASBinCode.OpStep step ,IList<ASBinCode.IEAX> eaxs,ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v = step.arg1.getValue(eaxs,scope);
            step.reg.setValue(v, eaxs, scope);

        }

    }
}
