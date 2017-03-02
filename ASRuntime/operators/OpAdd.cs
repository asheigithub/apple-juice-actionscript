using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAdd
    {
        internal static readonly ASBinCode.rtData.rtString nullStr = new ASBinCode.rtData.rtString(null);

        public static void execAdd_Number(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);

            step.reg.getISlot(scope).setValue(a1.value + a2.value);//new ASBinCode.rtData.rtNumber(a1.value +a2.value ));
        }

        public static void execAdd_String(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.rtData.rtString a1;
            if (step.arg1.getValue(scope).rtType == ASBinCode.RunTimeDataType.rt_null)
            {
                a1 = nullStr;
            }
            else
            {
                a1 = (ASBinCode.rtData.rtString)step.arg1.getValue(scope);
            }


            ASBinCode.rtData.rtString a2;
                if (step.arg2.getValue(scope).rtType == ASBinCode.RunTimeDataType.rt_null)
            {
                a2 = nullStr;
            }
            else
            {
                a2= (ASBinCode.rtData.rtString)step.arg2.getValue(scope);
            }


            step.reg.getISlot(scope).setValue(a1.valueString() + a2.valueString());// new ASBinCode.rtData.rtString(a1.valueString() + a2.valueString()));
        }

        public static void execAdd(StackFrame frame, ASBinCode.OpStep step,  ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue( scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue( scope);

            ASBinCode.RunTimeDataType v1type = v1.rtType;
            ASBinCode.RunTimeDataType v2type = v2.rtType;

            if (v1type == ASBinCode.RunTimeDataType.rt_void)
            {
                v1type = ASBinCode.RunTimeDataType.rt_number;
            }
            if (v2type == ASBinCode.RunTimeDataType.rt_void)
            {
                v2type = ASBinCode.RunTimeDataType.rt_number;
            }

            //if (v1.rtType == ASBinCode.RunTimeDataType.rt_void)
            //{
            //    v1 = TypeConverter.ConvertToNumber(v1, player, step.token);
            //}
            //if (v2.rtType == ASBinCode.RunTimeDataType.rt_void)
            //{
            //    v2 = TypeConverter.ConvertToNumber(v2, player, step.token);
            //}

            ASBinCode.RunTimeDataType finalType = 
                TypeConverter.getImplicitOpType(
                    v1type,
                    v2type,
                    ASBinCode.OpCode.add
                );

            //if (v1.rtType != finalType)
            //{
            //    v1 = OpCast.CastValue(v1, finalType, player, step.token,  scope);
            //}
            //if (v2.rtType != finalType)
            //{
            //    v2 = OpCast.CastValue(v2, finalType, player, step.token,  scope);
            //}

            if (finalType == ASBinCode.RunTimeDataType.rt_number)
            {
                step.reg.getISlot(scope).setValue(
                    TypeConverter.ConvertToNumber(v1,frame,step.token)
                    //((ASBinCode.rtData.rtNumber)v1).value 
                    +
                    TypeConverter.ConvertToNumber(v2, frame, step.token)
                    //((ASBinCode.rtData.rtNumber)v2).value

                    );// new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtNumber)v1).value + ((ASBinCode.rtData.rtNumber)v2).value));
            }
            else if (finalType == ASBinCode.RunTimeDataType.rt_string)
            {
                string s1 = TypeConverter.ConvertToString(v1, frame, step.token);
                string s2 = TypeConverter.ConvertToString(v2, frame, step.token);

                if (s1 == null) { s1 = "null"; }
                if (s2 == null) { s2 = "null"; }

                step.reg.getISlot(scope).setValue(s1+s2);//new ASBinCode.rtData.rtString(((ASBinCode.rtData.rtString)v1).valueString() + ((ASBinCode.rtData.rtString)v2).valueString()));
            }
            else if (finalType == ASBinCode.RunTimeDataType.rt_void)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtUndefined.undefined);
            }
            else
            {
                frame.throwOpException(step.token, ASBinCode.OpCode.add);
            }
        }


    }
}
