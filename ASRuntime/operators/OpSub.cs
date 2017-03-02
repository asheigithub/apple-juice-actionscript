using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpSub
    {
        public static void execSub_Number(Player player, ASBinCode.OpStep step,ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);

            step.reg.getISlot(scope).setValue(a1.value - a2.value);//new ASBinCode.rtData.rtNumber(a1.value - a2.value));
        }

        public static void execSub(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            //if (v1.rtType == ASBinCode.RunTimeDataType.rt_void)
            //{
            double    n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
            //}
            //if (v2.rtType == ASBinCode.RunTimeDataType.rt_void)
            //{
            double    n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);
            //}


            //ASBinCode.RunTimeDataType finalType =
            //    TypeConverter.getImplicitOpType(
            //        v1.rtType,
            //        v2.rtType,
            //        ASBinCode.OpCode.sub
            //    );

            //if (v1.rtType != finalType)
            //{
            //    v1 = OpCast.CastValue(v1, finalType, player, step.token, eaxs, scope);
            //}
            //if (v2.rtType != finalType)
            //{
            //    v2 = OpCast.CastValue(v2, finalType, player, step.token, eaxs, scope);
            //}

            //if (finalType == ASBinCode.RunTimeDataType.rt_number)
            {
                step.reg.getISlot(scope).setValue(n1-n2);// ((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value);//new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value));
            }
            //else if (finalType == ASBinCode.RunTimeDataType.rt_void)
            //{
            //    step.reg.setValue(ASBinCode.rtData.rtUndefined.undefined, eaxs, scope);
            //}
            //else
            //{
            //    player.throwOpException(step.token, ASBinCode.OpCode.sub);
            //}
        }


    }
}
