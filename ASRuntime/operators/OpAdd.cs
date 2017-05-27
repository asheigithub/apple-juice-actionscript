using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAdd
    {
        internal static readonly ASBinCode.rtData.rtString nullStr = new ASBinCode.rtData.rtString(null);

        public static void execAdd_Number(StackFrame frame ,ASBinCode.OpStep step,ASBinCode.RunTimeScope scope)
        {
            double a1 = step.arg1.getValue(scope, frame).toNumber();
            double a2 = step.arg2.getValue(scope, frame).toNumber();

            step.reg.getSlot(scope, frame).setValue(a1 + a2);//new ASBinCode.rtData.rtNumber(a1.value +a2.value ));
            frame.endStep(step);
        }

        public static void execAdd_String( StackFrame frame,ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.rtData.rtString a1;
            if (step.arg1.getValue(scope, frame).rtType == ASBinCode.RunTimeDataType.rt_null)
            {
                a1 = nullStr;
            }
            else
            {
                a1 = (ASBinCode.rtData.rtString)step.arg1.getValue(scope, frame);
            }


            ASBinCode.rtData.rtString a2;
                if (step.arg2.getValue(scope, frame).rtType == ASBinCode.RunTimeDataType.rt_null)
            {
                a2 = nullStr;
            }
            else
            {
                a2= (ASBinCode.rtData.rtString)step.arg2.getValue(scope, frame);
            }


            step.reg.getSlot(scope, frame).setValue(a1.valueString() + a2.valueString());// new ASBinCode.rtData.rtString(a1.valueString() + a2.valueString()));
            frame.endStep(step);
        }

        public static void execAdd(StackFrame frame, ASBinCode.OpStep step,  ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue( scope, frame);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue( scope, frame);

            var f = frame.player.swc.operatorOverrides.getOperatorFunction(OverrideableOperator.addition,
                v1.rtType, v2.rtType);
            if (f != null)
            {
                FunctionCaller fc =  FunctionCaller.create(frame.player, frame, step.token);
                fc.function = f;
                fc.loadDefineFromFunction();
                //fc.releaseAfterCall = true;

                bool success;
                fc.pushParameter(v1, 0, out success);
                fc.pushParameter(v2, 1, out success);
                fc.returnSlot = step.reg.getSlot(scope, frame);
                fc.callbacker = fc;
                fc.call();
                
            }
            else if (
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
            ASBinCode.RunTimeValueBase v1,ASBinCode.RunTimeValueBase v2 ,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
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
            ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
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
                step.reg.getSlot(scope, frame).setValue(
                    TypeConverter.ConvertToNumber(v1)
                    +
                    TypeConverter.ConvertToNumber(v2)

                    );
            }
            else if (finalType == ASBinCode.RunTimeDataType.rt_string)
            {

                BlockCallBackBase cb = BlockCallBackBase.create();
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
                step.reg.getSlot(scope, frame).setValue(ASBinCode.rtData.rtUndefined.undefined);
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

            sender.step.reg.getSlot(sender.scope,(StackFrame)sender.args).setValue(v1 + v2);
            ((StackFrame)sender.args).endStep(sender.step);

        }

    }
}
