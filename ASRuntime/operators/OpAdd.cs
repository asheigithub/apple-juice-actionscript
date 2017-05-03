using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAdd
    {
        internal static readonly ASBinCode.rtData.rtString nullStr = new ASBinCode.rtData.rtString(null);

        public static void execAdd_Number(Player player, ASBinCode.OpStep step,StackFrame frame ,ASBinCode.IRunTimeScope scope)
        {
            double a1 = TypeConverter.ConvertToNumber( step.arg1.getValue(scope),null,null);
            double a2 = TypeConverter.ConvertToNumber( step.arg2.getValue(scope),null,null);

            step.reg.getISlot(scope).setValue(a1 + a2);//new ASBinCode.rtData.rtNumber(a1.value +a2.value ));
            frame.endStep(step);
        }

        public static void execAdd_String(Player player, ASBinCode.OpStep step,StackFrame frame, ASBinCode.IRunTimeScope scope)
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
            frame.endStep(step);
        }

        public static void execAdd(StackFrame frame, ASBinCode.OpStep step,  ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue( scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue( scope);

            if (
                (v1.rtType > ASBinCode.RunTimeDataType.unknown && v2.rtType > ASBinCode.RunTimeDataType.unknown)
                ||
                v1.rtType == ASBinCode.RunTimeDataType.rt_int
                ||
                v1.rtType == ASBinCode.RunTimeDataType.rt_uint
                ||
                v1.rtType == ASBinCode.RunTimeDataType.rt_number
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_int
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_uint
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_number
                )
            {
                OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, step, _execAdd_CallBacker);
            }
            else //toString
            {
                OpCast.InvokeTwoToString(v1, v2, frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, step, _execAdd_InvokeToString_CallBacker);
            }
            
        }

        private static void _execAdd_CallBacker( 
            ASBinCode.IRunTimeValue v1,ASBinCode.IRunTimeValue v2 ,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
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

            if (v1type > ASBinCode.RunTimeDataType.unknown || v2type > ASBinCode.RunTimeDataType.unknown)
            {
                //***调用toString()***
                OpCast.InvokeTwoToString(v1, v2, frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, step, _execAdd_InvokeToString_CallBacker);
            }
            //else if (v1type > ASBinCode.RunTimeDataType.unknown)
            //{
            //    OpCast.InvokeTwoToString(v1, v2, frame, step.token, scope,
            //        frame._tempSlot1, frame._tempSlot2, step, _execAdd_InvokeToString_CallBacker);
            //}
            //else if (v2type > ASBinCode.RunTimeDataType.unknown)
            //{

            //}
            else
            {
                _execAdd_InvokeToString_CallBacker(v1, v2, frame, step, scope);
            }
        }
        private static void _execAdd_InvokeToString_CallBacker(
            ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
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

            ASBinCode.RunTimeDataType finalType =
                TypeConverter.getImplicitOpType(
                    v1type,
                    v2type,
                    ASBinCode.OpCode.add, frame.player.swc
                );

            if (finalType == ASBinCode.RunTimeDataType.rt_number)
            {
                step.reg.getISlot(scope).setValue(
                    TypeConverter.ConvertToNumber(v1, frame, step.token)
                    +
                    TypeConverter.ConvertToNumber(v2, frame, step.token)

                    );
            }
            else if (finalType == ASBinCode.RunTimeDataType.rt_string)
            {

                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_Cast_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2, finalType, frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, cb
                    );


                return;
            }
            else if (finalType == ASBinCode.RunTimeDataType.rt_void)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtUndefined.undefined);
            }
            else
            {
                frame.throwOpException(step.token, ASBinCode.OpCode.add);
            }

            frame.endStep(step);
        }



        private static void _Cast_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string v1;
            {
                var rv = ((StackFrame)sender.args)._tempSlot1.getValue();
                if (rv.rtType == ASBinCode.RunTimeDataType.rt_null)
                {
                    v1 = "null";
                }
                else
                {
                    v1 = (((ASBinCode.rtData.rtString)rv).valueString());
                }
            }
            string v2;
            {
                var rv = ((StackFrame)sender.args)._tempSlot2.getValue();
                if (rv.rtType == ASBinCode.RunTimeDataType.rt_null)
                {
                    v2 = "null";
                }
                else
                {
                    v2 = (((ASBinCode.rtData.rtString)rv).valueString());
                }
            }

            sender.step.reg.getISlot(sender.scope).setValue(v1 + v2);
            ((StackFrame)sender.args).endStep(sender.step);

        }

    }
}
