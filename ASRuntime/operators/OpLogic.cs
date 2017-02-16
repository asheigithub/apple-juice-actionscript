using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpLogic
    {
        public static void execGT(Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(eaxs, scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(eaxs, scope);

            if (a1.value > a2.value)
            {
                step.reg.setValue(ASBinCode.rtData.rtBoolean.True, eaxs, scope);
            }
            else
            {
                step.reg.setValue(ASBinCode.rtData.rtBoolean.False, eaxs, scope);
            }
            //step.reg.setValue(new ASBinCode.rtData.rtNumber(a1.value + a2.value), eaxs, scope);
        }
    }
}
