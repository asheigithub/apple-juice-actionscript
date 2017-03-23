using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpSub
    {
        public static void execSub_Number(Player player, ASBinCode.OpStep step, StackFrame frame, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);

            step.reg.getISlot(scope).setValue(a1.value - a2.value);//new ASBinCode.rtData.rtNumber(a1.value - a2.value));
            frame.endStep(step);
        }

        public static void execSub(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

           
            double    n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);          
            double    n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);
            
            {
                step.reg.getISlot(scope).setValue(n1-n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }


    }
}
