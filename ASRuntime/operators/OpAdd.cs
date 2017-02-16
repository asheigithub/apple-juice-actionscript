using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAdd
    {
        public static void execAdd_Number(Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(eaxs,scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(eaxs,scope);

            step.reg.setValue(new ASBinCode.rtData.rtNumber(a1.value +a2.value ), eaxs, scope);
        }

        public static void execAdd_String(Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtString a1 = (ASBinCode.rtData.rtString)step.arg1.getValue(eaxs, scope);
            ASBinCode.rtData.rtString a2 = (ASBinCode.rtData.rtString)step.arg2.getValue(eaxs, scope);

            


            step.reg.setValue(new ASBinCode.rtData.rtString(a1.valueString() + a2.valueString()), eaxs, scope);
        }

        public static void execAdd(Player player, ASBinCode.OpStep step, IList<ASBinCode.IEAX> eaxs, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(eaxs, scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(eaxs, scope);

            if (v1.rtType == ASBinCode.RunTimeDataType.rt_void)
            {
                v1 = TypeConverter.ConvertToNumber(v1, player, step.token);
            }
            if (v2.rtType == ASBinCode.RunTimeDataType.rt_void)
            {
                v2 = TypeConverter.ConvertToNumber(v2, player, step.token);
            }

            ASBinCode.RunTimeDataType finalType = 
                TypeConverter.getImplicitOpType(
                    v1.rtType,
                    v2.rtType,
                    ASBinCode.OpCode.add
                );

            if (v1.rtType != finalType)
            {
                v1 = OpCast.CastValue(v1, finalType, player, step.token, eaxs, scope);
            }
            if (v2.rtType != finalType)
            {
                v2 = OpCast.CastValue(v2, finalType, player, step.token, eaxs, scope);
            }

            if (finalType == ASBinCode.RunTimeDataType.rt_number)
            {
                step.reg.setValue(new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value + ((ASBinCode.rtData.rtNumber)v2).value), eaxs, scope);
            }
            else if (finalType == ASBinCode.RunTimeDataType.rt_string)
            {
                step.reg.setValue(new ASBinCode.rtData.rtString(((ASBinCode.rtData.rtString)v1).valueString() + ((ASBinCode.rtData.rtString)v2).valueString()), eaxs, scope);
            }
            else if (finalType == ASBinCode.RunTimeDataType.rt_void)
            {
                step.reg.setValue(ASBinCode.rtData.rtUndefined.undefined,eaxs,scope);
            }
            else
            {
                player.throwOpException(step.token, ASBinCode.OpCode.add);
            }
        }


    }
}
