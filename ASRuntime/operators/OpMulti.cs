using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpMulti
    {
        public static void execMulti(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);
            
            double n1 = TypeConverter.ConvertToNumber(v1, player, step.token);
            
            double n2 = TypeConverter.ConvertToNumber(v2, player, step.token);
            
            {
                step.reg.getISlot(scope).setValue(n1 * n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }

        }

        public static void execDiv(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            double n1 = TypeConverter.ConvertToNumber(v1, player, step.token);

            double n2 = TypeConverter.ConvertToNumber(v2, player, step.token);

            {
                step.reg.getISlot(scope).setValue(n1 / n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }

        }
        public static void execMod(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            double n1 = TypeConverter.ConvertToNumber(v1, player, step.token);

            double n2 = TypeConverter.ConvertToNumber(v2, player, step.token);

            {
                step.reg.getISlot(scope).setValue(n1 % n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }

        }
    }

}
