using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpIncrementDecrement
    {
        public static void execIncrement(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);

            switch (v.rtType)
            {
                
                case ASBinCode.RunTimeDataType.rt_int:
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                        iv.value++;
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                        iv.value++;
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                        iv.value++;
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
                    }
                    break;
                
                case ASBinCode.RunTimeDataType.rt_string:

                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        if (string.IsNullOrEmpty( ((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
                    }
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr, frame, step.token, 
                            scope, frame._tempSlot1, 
                            frame._tempSlot2, step, _execIncrement_ValueOf_Callbacker);
                        return;
                    }
            }

            frame.endStep(step);

        }
        private static void _execIncrement_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope
            )
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execIncrement_ToString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1, frame, step.token);
                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
                frame.endStep(step);
            }
            
        }
        private static void _execIncrement_ToString_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
           StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope
           )
        {
            double n = TypeConverter.ConvertToNumber(v1, frame, step.token);
            ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
            ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
            frame.endStep(step);
        }



        public static void execIncInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value++;
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
            }
            frame.endStep(step);
        }

        public static void execIncUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value++;
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
            }
            frame.endStep(step);
        }

        public static void execIncNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value++;
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
            }

            frame.endStep(step);
        }





        public static void execDecrement(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);

            switch (v.rtType)
            {

                case ASBinCode.RunTimeDataType.rt_int:
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                        iv.value--;
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                        iv.value--;
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                        iv.value--;
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_string:

                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        if (string.IsNullOrEmpty(((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
                    }
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr, frame, step.token,
                            scope, frame._tempSlot1,
                            frame._tempSlot2, step, _execDecrement_ValueOf_Callbacker);
                        return;
                    }
            }


            frame.endStep(step);
        }

        private static void _execDecrement_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope
            )
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execDecrement_toString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1, frame, step.token);
                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
                frame.endStep(step);
            }
        }
        private static void _execDecrement_toString_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope
            )
        {
            double n = TypeConverter.ConvertToNumber(v1, frame, step.token);
            ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
            ((ASBinCode.ILeftValue)step.reg).getISlot(scope).directSet(num);
            frame.endStep(step);
        }



        public static void execDecInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value--;
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
            }
            frame.endStep(step);
        }

        public static void execDecUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value--;
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
            }
            frame.endStep(step);
        }

        public static void execDecNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value--;
                ((ASBinCode.ILeftValue)step.reg).getISlot(scope).setValue(iv.value);
            }
            frame.endStep(step);

        }



        public static void execSuffixInc(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);

            switch (v.rtType)
            {

                case ASBinCode.RunTimeDataType.rt_int:
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                        step.reg.getISlot(scope).setValue(iv.value );

                        iv.value++;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value++;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value++;
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_string:

                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        if (string.IsNullOrEmpty(((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixInc_ValueOf_Callbacker);
                        return;
                    }
            }

            frame.endStep(step);
        }

        private static void _execSuffixInc_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixInc_toString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1, frame, step.token);

                step.reg.getISlot(scope).setValue(n);

                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                frame.endStep(step);
            }
        }

        private static void _execSuffixInc_toString_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            double n = TypeConverter.ConvertToNumber(v1, frame, step.token);

            step.reg.getISlot(scope).setValue(n);

            ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
            ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
            frame.endStep(step);
        }

        public static void execSuffixIncInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value++;
                    }

            frame.endStep(step);
        }

        public static void execSuffixIncUint(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value++;
                    }

            frame.endStep(step);
        }

        public static void execSuffixIncNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value++;
            }
            frame.endStep(step);
        }




        public static void execSuffixDec(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);

            switch (v.rtType)
            {

                case ASBinCode.RunTimeDataType.rt_int:
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value--;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value--;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value--;
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_string:

                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        if (string.IsNullOrEmpty(((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixDec_ValueOf_Callbacker);
                        return;
                    }
            }

            frame.endStep(step);
        }

        private static void _execSuffixDec_ValueOf_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixDec_toString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1, frame, step.token);

                step.reg.getISlot(scope).setValue(n);

                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                frame.endStep(step);
            }
        }

        private static void _execSuffixDec_toString_Callbacker(ASBinCode.IRunTimeValue v1, ASBinCode.IRunTimeValue v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            double n = TypeConverter.ConvertToNumber(v1, frame, step.token);

            step.reg.getISlot(scope).setValue(n);

            ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
            ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
            frame.endStep(step);
        }

        public static void execSuffixDecInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value--;
            }
            frame.endStep(step);
        }

        public static void execSuffixDecUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value--;
            }
            frame.endStep(step);
        }

        public static void execSuffixDecNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value--;
            }
            frame.endStep(step);
        }


    }


}
