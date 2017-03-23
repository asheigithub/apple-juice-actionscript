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
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                        iv.value++;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                        iv.value++;
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
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }

            frame.endStep(step);

        }


        public static void execIncInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value++;
            }
            frame.endStep(step);
        }

        public static void execIncUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value++;
            }
            frame.endStep(step);
        }

        public static void execIncNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value++;
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
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                        iv.value--;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
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

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }


            frame.endStep(step);
        }


        public static void execDecInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value--;
            }
            frame.endStep(step);
        }

        public static void execDecUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value--;
            }
            frame.endStep(step);
        }

        public static void execDecNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value--;
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
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }

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
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        double n = TypeConverter.ConvertToNumber(v, frame, step.token);

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }

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
