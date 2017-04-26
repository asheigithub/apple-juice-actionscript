using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpLogic
    {
        public static void execNOT(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = TypeConverter.ConvertToBoolean(step.arg1.getValue(scope),frame,step.token);

            if (object.ReferenceEquals(v, ASBinCode.rtData.rtBoolean.True))
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False );
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            frame.endStep(step);
        }

        


        public static void execGT_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            
            
            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);

            if (a1.value > a2.value)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }

            frame.endStep(step);
            
        }

        public static void execGE_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {


            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);

            if (a1.value >= a2.value)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }


            frame.endStep(step);
        }

        public static void execGT_Void(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _GTVoid_ValueOf_CallBacker);
        }
        private static void _GTVoid_ValueOf_CallBacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (
                (
                v1.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
                )
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)
                ||
                (
                needInvokeToString(v1,v2,frame.player)
                //v1.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
                //    )
                //||
                //(v2.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
                //)
                )
                )
            {

                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_GTVoid_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, cb
                    );

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 > n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }

        private static void _readTwoStringFromCallBacker(BlockCallBackBase sender,out string s1,out string s2)
        {
            
            {
                var rv = ((StackFrame)sender.args)._tempSlot1.getValue();
                if (rv.rtType == ASBinCode.RunTimeDataType.rt_null)
                {
                    s1 = null;
                }
                else
                {
                    s1 = (((ASBinCode.rtData.rtString)rv).valueString());
                }
            }
            
            {
                var rv = ((StackFrame)sender.args)._tempSlot2.getValue();
                if (rv.rtType == ASBinCode.RunTimeDataType.rt_null)
                {
                    s2 = null;
                }
                else
                {
                    s2 = (((ASBinCode.rtData.rtString)rv).valueString());
                }
            }
        }
        private static void _GTVoid_TwoString_Callbacker(BlockCallBackBase sender, object args)
        {
            string s1;
            string s2;
            _readTwoStringFromCallBacker(sender,out s1,out s2);

            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            else if (string.CompareOrdinal(s1, s2) > 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }




        public static void execGE_Void(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _GEVoid_ValueOf_Callbacker);
        }
        private static void _GEVoid_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            if (
                (
                v1.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
                )
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)
                ||
                //(
                needInvokeToString(v1,v2,frame.player)
                //v1.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
                //    )
                //||
                //(v2.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
                //)
                )
            {
                
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_GEVoid_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, cb);

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 >= n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }

        private static void _GEVoid_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1, s2;
            _readTwoStringFromCallBacker(sender, out s1, out s2);
            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else if (string.CompareOrdinal(s1, s2) >= 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }

            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void execLT_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {


            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);

            if (a1.value < a2.value)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }


            frame.endStep(step);
        }

        public static void execLE_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {


            ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);

            if (a1.value <= a2.value)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execLT_VOID(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _LTVoid_ValueOf_Callbacker);
        }

        private static void _LTVoid_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (
               (
               v1.rtType == ASBinCode.RunTimeDataType.rt_string
               &&
               (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
               )
               ||
               v2.rtType == ASBinCode.RunTimeDataType.rt_string
               &&
               (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)

               ||
               needInvokeToString(v1, v2, frame.player)
               //(
               //v1.rtType > ASBinCode.RunTimeDataType.unknown
               //    &&
               //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
               //    )
               //||
               //(v2.rtType > ASBinCode.RunTimeDataType.unknown
               //    &&
               //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
               //)

               )
            {
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_LTVoid_TwoString_Callbacker);
                cb.scope = scope;
                cb.args = frame;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string, frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, cb);

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 < n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }

        private static void _LTVoid_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1;
            string s2;
            _readTwoStringFromCallBacker(sender, out s1, out s2);
            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            else if (string.CompareOrdinal(s1, s2) < 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }


        public static void execLE_VOID(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _LEVoid_ValueOf_Callbacker);
        }
        private static void _LEVoid_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (
                (
                v1.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
                )
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)
                ||
                needInvokeToString(v1, v2, frame.player)
                //(
                //v1.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
                //    )
                //||
                //(v2.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
                //)
                )
            {
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_LEVoid_TwoString_Callbacker);
                cb.args = frame;
                cb.step = step;
                cb.scope = scope;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string, frame, step.token,
                    scope, frame._tempSlot1, frame._tempSlot2, cb);

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 <= n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }
        private static void _LEVoid_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1, s2;
            _readTwoStringFromCallBacker(sender, out s1, out s2);

            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else if (string.CompareOrdinal(s1, s2) <= 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }


            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void execEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v1 = step.arg1.getValue(scope);
            var v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _EQ_ValueOf_Callbacker);
        }
        private static void _EQ_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc))
            {
                v1 = new ASBinCode.rtData.rtNumber(TypeConverter.ConvertToNumber(v1, frame, step.token));
            }
            if (TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc))
            {
                v2 = new ASBinCode.rtData.rtNumber(TypeConverter.ConvertToNumber(v2, frame, step.token));
            }


            if (needInvokeToString(v1, v2, frame.player))
            {
                //***转成字符串比较***
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_EQ_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2,
                    ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, cb);


                return;
            }
            else
            {
                if (testEquals(v1, v2, frame, step, scope))
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
                frame.endStep(step);
            }
        }

        private static void _EQ_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1, s2;_readTwoStringFromCallBacker(sender, out s1, out s2);

            if (string.CompareOrdinal(s1, s2) == 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void execNotEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v1 = step.arg1.getValue(scope);
            var v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _NotEQ_ValueOf_Callbacker);
        }

        private static void _NotEQ_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc))
            {
                v1 = new ASBinCode.rtData.rtNumber(TypeConverter.ConvertToNumber(v1, frame, step.token));
            }
            if (TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc))
            {
                v2 = new ASBinCode.rtData.rtNumber(TypeConverter.ConvertToNumber(v2, frame, step.token));
            }

            if (needInvokeToString(v1, v2, frame.player))//v1.rtType > ASBinCode.RunTimeDataType.unknown || v2.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                //***转成字符串比较***
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_NOTEQ_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2,
                    ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, cb);


                return;
            }
            else
            {
                if (!testEquals(step.arg1.getValue(scope), step.arg2.getValue(scope), frame, step, scope))
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
                frame.endStep(step);
            }
        }

        private static void _NOTEQ_TwoString_Callbacker(BlockCallBackBase sender, object args)
        {
            string s1, s2; _readTwoStringFromCallBacker(sender, out s1, out s2);

            if (string.CompareOrdinal(s1, s2) != 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void execEQ_NumNum(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), frame, step.token);
            var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), frame, step.token);

            if (n1==n2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execNotEQ_NumNum(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), frame, step.token);
            var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), frame, step.token);

            if (n1 != n2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execEQ_StrStr(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var n1 = ((ASBinCode.rtData.rtString)step.arg1.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg1.getValue(scope), frame, step.token);
            var n2 = ((ASBinCode.rtData.rtString)step.arg2.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg2.getValue(scope), frame, step.token);

            if (string.CompareOrdinal(n1,n2)==0)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execNotEQ_StrStr(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var n1 = ((ASBinCode.rtData.rtString)step.arg1.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg1.getValue(scope), frame, step.token);
            var n2 = ((ASBinCode.rtData.rtString)step.arg2.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg2.getValue(scope), frame, step.token);

            if (string.CompareOrdinal(n1, n2) != 0)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        private static bool  _execStrictEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            //strict equality 运算符仅针对数字类型（Number、int 和 uint）执行自动数据转换
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            if (ReferenceEquals(v1, v2))
            {
                return true;
            }

            ASBinCode.RunTimeDataType ot;
            if (TypeConverter.Object_CanImplicit_ToPrimitive(v1.rtType, frame.player.swc, out ot))
            {
                v1 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v1);
            }
            if (TypeConverter.Object_CanImplicit_ToPrimitive(v2.rtType, frame.player.swc, out ot))
            {
                v2 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v1);
            }

            if ((
                v1.rtType == ASBinCode.RunTimeDataType.rt_number || v1.rtType == ASBinCode.RunTimeDataType.rt_int
                ||
                v1.rtType == ASBinCode.RunTimeDataType.rt_uint
                ||
                TypeConverter.ObjectImplicit_ToNumber(v1.rtType,frame.player.swc)
                )

                &&
                (
                v2.rtType == ASBinCode.RunTimeDataType.rt_number || v2.rtType == ASBinCode.RunTimeDataType.rt_int
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_uint
                ||
                TypeConverter.ObjectImplicit_ToNumber(v2.rtType,frame.player.swc)
                )
                )
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 == n2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_string && v2.rtType == ASBinCode.RunTimeDataType.rt_string)
            {
                //string s1 = TypeConverter.ConvertToString(step.arg1.getValue(scope), frame, step.token);
                //string s2 = TypeConverter.ConvertToString(step.arg2.getValue(scope), frame, step.token);
                string s1 = ((ASBinCode.rtData.rtString)step.arg1.getValue(scope)).value;
                string s2 = ((ASBinCode.rtData.rtString)step.arg2.getValue(scope)).value;

                if (string.CompareOrdinal(s1, s2) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_function
                &&
                v2.rtType == ASBinCode.RunTimeDataType.rt_function
                )
            {
                ASBinCode.rtData.rtFunction obj1 = (ASBinCode.rtData.rtFunction)v1;
                ASBinCode.rtData.rtFunction obj2 = (ASBinCode.rtData.rtFunction)v2;

                return ASBinCode.rtData.rtFunction.isTypeEqual(obj1, obj2);

            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_array
                && v2.rtType == ASBinCode.RunTimeDataType.rt_array)
            {
                return v1.Equals(v2);
            }
            else if (v1.rtType > ASBinCode.RunTimeDataType.unknown
                &&
                v2.rtType > ASBinCode.RunTimeDataType.unknown
                )
            {
                ASBinCode.rtData.rtObject obj1 = (ASBinCode.rtData.rtObject)v1;
                ASBinCode.rtData.rtObject obj2 = (ASBinCode.rtData.rtObject)v2;

                return ReferenceEquals(obj1.value, obj2.value) && ReferenceEquals(obj1.objScope, obj2.objScope);

            }
            else
            {
                if (object.ReferenceEquals(v1, v2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void execStrictEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (_execStrictEQ(frame, step, scope))
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }
        public static void execStrictNotEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (!_execStrictEQ(frame, step, scope))
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        private static bool needInvokeToString(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,Player player)
        {
            if ((v1.rtType < ASBinCode.RunTimeDataType.unknown && v2.rtType > ASBinCode.RunTimeDataType.unknown)
                ||
                (v1.rtType > ASBinCode.RunTimeDataType.unknown && v2.rtType < ASBinCode.RunTimeDataType.unknown)
                )
            {
                //***如果有任一类型为array或者是vector,则返回false***
                if (v1.rtType == ASBinCode.RunTimeDataType.rt_array
                    ||
                    v2.rtType == ASBinCode.RunTimeDataType.rt_array
                    ||
                    (v1.rtType>ASBinCode.RunTimeDataType.unknown
                        &&
                        player.swc.dict_Vector_type.ContainsKey( player.swc.getClassByRunTimeDataType(v1.rtType) )
                    )
                    ||
                    (v2.rtType > ASBinCode.RunTimeDataType.unknown
                        &&
                        player.swc.dict_Vector_type.ContainsKey(player.swc.getClassByRunTimeDataType(v2.rtType))
                    )
                    )
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        ///测试两个表达式是否相等。如果表达式相等，则结果为 true。 
        ///如果两个操作数的数据类型相匹配，则相等的定义取决于操作数的数据类型：
        ///如果 int、uint 和 Boolean 类型具有相同的值，则将其值视为相等。
        ///值相匹配的两个 Number 被视为相等，除非两个值都为 NaN。
        ///如果两个操作数的值均为 null 或 undefined，则将它们视为相等。
        ///如果字符串表达式具有相同的字符数，而且这些字符都相同，则这些字符串表达式相等。
        ///对于 XML 对象： 
        ///如果一个操作数是文本或属性节点，而另一个操作数具有简单的内容，则使用 toString() 方法可将两个操作数转换为字符串，如果生成的字符串相匹配，则将这两个操作数视为相等。 
        ///否则，仅当两个对象的限定名、特性和子属性都匹配时，才会被视为相等。
        ///如果 XMLList 对象具有相同数目的属性，并且属性的顺序和值都匹配，则可将其视为相等。
        ///对于 Namespace 对象，如果两个对象的 uri 属性相匹配，则其值被视为相等。
        ///对于 QName 对象，如果两个对象的 uri 属性相匹配，并且两个对象的 localName 属性也相匹配，则其值视为相等。
        ///表示对象、数组和函数的变量按引用进行比较。如果两个这样的变量引用同一个对象、数组或函数，则它们相等。而两个单独的数组即使具有相同数量的元素，也永远不会被视为相等。
        ///如果这两个操作数的数据类型不匹配，则结果为 false，但在以下情况下除外： 
        ///操作数的值为 undefined 和 null，在这种情况下结果为 true。
        ///自动数据类型转换将数据类型为 String、Boolean、int、uint 和 Number 的值转换为兼容的类型，并且转换后的值相等，在这种情况下，操作数被视为相等。
        ///一个操作数的类型为 XML，并且包含简单内容(hasSimpleContent() == true)，在使用 toString() 将这两个操作数均转换为字符串后，所生成的字符串相匹配。
        ///一个操作数的类型为 XMLList，并且满足以下任一条件： 
        ///XMLList 对象的 length 属性是 0，而另一个对象为 undefined。
        ///XMLList 对象的 length 属性为 1，XMLList 对象的一个元素与另一个操作数相匹配。
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool testEquals(ASBinCode.IRunTimeValue v1,ASBinCode.IRunTimeValue v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.RunTimeDataType t1 = v1.rtType;
            ASBinCode.RunTimeDataType t2 = v2.rtType;

            if (ReferenceEquals(v1, v2))
            {
                return true;
            }

            if (
                (
                t1 == ASBinCode.RunTimeDataType.rt_int
                || t1 == ASBinCode.RunTimeDataType.rt_uint || t1 == ASBinCode.RunTimeDataType.rt_boolean
                || t1 == ASBinCode.RunTimeDataType.rt_number
                || TypeConverter.ObjectImplicit_ToNumber(t1, frame.player.swc)
                )
                &&
                (t2 == ASBinCode.RunTimeDataType.rt_int
                || t2 == ASBinCode.RunTimeDataType.rt_uint || t2 == ASBinCode.RunTimeDataType.rt_boolean
                || t2 == ASBinCode.RunTimeDataType.rt_number
                || TypeConverter.ObjectImplicit_ToNumber(t2, frame.player.swc)
                )
                )
            {
                return TypeConverter.ConvertToNumber(v1, frame, step.token) == TypeConverter.ConvertToNumber(v2, frame, step.token);
            }
            else if (
                (t1 == ASBinCode.RunTimeDataType.rt_null || t1 == ASBinCode.RunTimeDataType.rt_void)
                &&
                (t2 == ASBinCode.RunTimeDataType.rt_null || t2 == ASBinCode.RunTimeDataType.rt_void)
                )
            {
                return true;
            }
            else if (t1 == ASBinCode.RunTimeDataType.rt_string && t2 == ASBinCode.RunTimeDataType.rt_string)
            {
                return string.CompareOrdinal(
                    ((ASBinCode.rtData.rtString)v1).value
                    ,
                    ((ASBinCode.rtData.rtString)v2).value
                    ) == 0;
            }
            else if (t1 == ASBinCode.RunTimeDataType.rt_string)
            {
                switch (t2)
                {
                    case ASBinCode.RunTimeDataType.rt_boolean:
                        if (v2.Equals(ASBinCode.rtData.rtBoolean.True))
                        {
                            return TypeConverter.ConvertToInt(v1, frame, step.token) == 1;
                        }
                        else
                        {
                            return TypeConverter.ConvertToInt(v1, frame, step.token) != 1;
                        }
                    case ASBinCode.RunTimeDataType.rt_int:
                        return TypeConverter.ConvertToInt(v1, frame, step.token) == TypeConverter.ConvertToInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_uint:
                        return TypeConverter.ConvertToUInt(v1, frame, step.token) == TypeConverter.ConvertToUInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_number:
                        return TypeConverter.ConvertToNumber(v1, frame, step.token) == TypeConverter.ConvertToNumber(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_string:
                        return string.CompareOrdinal(
                            ((ASBinCode.rtData.rtString)v1).value
                            ,
                            ((ASBinCode.rtData.rtString)v2).value
                            ) == 0;
                    case ASBinCode.RunTimeDataType.rt_void:
                        return false;
                    case ASBinCode.RunTimeDataType.rt_null:
                        return false;
                    case ASBinCode.RunTimeDataType.unknown:
                        return false;
                    default:
                        break;
                }
            }
            else if (t2 == ASBinCode.RunTimeDataType.rt_string)
            {
                switch (t1)
                {
                    case ASBinCode.RunTimeDataType.rt_boolean:
                        if (v1.Equals(ASBinCode.rtData.rtBoolean.True))
                        {
                            return TypeConverter.ConvertToInt(v2, frame, step.token) == 1;
                        }
                        else
                        {
                            return TypeConverter.ConvertToInt(v2, frame, step.token) != 1;
                        }
                    case ASBinCode.RunTimeDataType.rt_int:
                        return TypeConverter.ConvertToInt(v1, frame, step.token) == TypeConverter.ConvertToInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_uint:
                        return TypeConverter.ConvertToUInt(v1, frame, step.token) == TypeConverter.ConvertToUInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_number:
                        return TypeConverter.ConvertToNumber(v1, frame, step.token) == TypeConverter.ConvertToNumber(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_string:
                        return string.CompareOrdinal(
                           ((ASBinCode.rtData.rtString)v1).value
                            ,
                            ((ASBinCode.rtData.rtString)v2).value
                            ) == 0;
                    case ASBinCode.RunTimeDataType.rt_void:
                        return false;
                    case ASBinCode.RunTimeDataType.rt_null:
                        return false;
                    case ASBinCode.RunTimeDataType.unknown:
                        return false;
                    default:
                        break;
                }
            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_function
                &&
                v2.rtType == ASBinCode.RunTimeDataType.rt_function
                )
            {
                ASBinCode.rtData.rtFunction obj1 = (ASBinCode.rtData.rtFunction)v1;
                ASBinCode.rtData.rtFunction obj2 = (ASBinCode.rtData.rtFunction)v2;

                return ASBinCode.rtData.rtFunction.isTypeEqual(obj1, obj2);

            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_array 
                && v2.rtType == ASBinCode.RunTimeDataType.rt_array)
            {
                return v1.Equals(v2);
            }
            else if (v1.rtType > ASBinCode.RunTimeDataType.unknown
                &&
                v2.rtType > ASBinCode.RunTimeDataType.unknown
                )
            {
                ASBinCode.rtData.rtObject obj1 = (ASBinCode.rtData.rtObject)v1;
                ASBinCode.rtData.rtObject obj2 = (ASBinCode.rtData.rtObject)v2;

                return ReferenceEquals(obj1.value, obj2.value) && ReferenceEquals(obj1.objScope, obj2.objScope);

            }
            return false;
        }


    }
}
