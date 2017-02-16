using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpNeg
    {
        public static void execNeg(Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v = step.arg1.getValue(eaxs, scope);
            step.reg.setValue( new ASBinCode.rtData.rtNumber( -((ASBinCode.rtData.rtNumber)v).value), eaxs, scope);

        }
    }
}
