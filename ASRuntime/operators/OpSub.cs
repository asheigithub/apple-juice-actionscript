using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpSub
    {
        public static void execSub_Number(Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(eaxs, scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(eaxs, scope);

            step.reg.setValue(new ASBinCode.rtData.rtNumber(a1.value - a2.value), eaxs, scope);
        }

        public static void execSub(Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(eaxs, scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(eaxs, scope);

            //if (v1.rtType == ASBinCode.RunTimeDataType.rt_void)
            {
                v1 = TypeConverter.ConvertToNumber(v1, player, step.token);
            }
            //if (v2.rtType == ASBinCode.RunTimeDataType.rt_void)
            {
                v2 = TypeConverter.ConvertToNumber(v2, player, step.token);
            }


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
                step.reg.setValue(new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value - ((ASBinCode.rtData.rtNumber)v2).value), eaxs, scope);
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
