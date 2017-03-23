using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpMulti
    {
        public static void execMulti(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);
            
            double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
            
            double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);
            
            {
                step.reg.getISlot(scope).setValue(n1 * n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }

        public static void execDiv(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);

            double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

            {
                step.reg.getISlot(scope).setValue(n1 / n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }
        public static void execMod(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);

            double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

            {
                step.reg.getISlot(scope).setValue(n1 % n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            frame.endStep(step);
        }
    }

}
