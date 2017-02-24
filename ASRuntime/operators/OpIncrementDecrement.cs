using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpIncrementDecrement
    {
        public static void execIncrement(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }



        }


        public static void execIncInt(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value++;
            }
        }

        public static void execIncUInt(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value++;
            }
        }

        public static void execIncNumber(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value++;
            }
                   

        }





        public static void execDecrement(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }



        }


        public static void execDecInt(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value--;
            }
        }

        public static void execDecUInt(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value--;
            }
        }

        public static void execDecNumber(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value--;
            }


        }



        public static void execSuffixInc(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }


        }

        public static void execSuffixIncInt(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value++;
                    }
        }

        public static void execSuffixIncUint(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                        step.reg.getISlot(scope).setValue(iv.value);

                        iv.value++;
                    }
        }

        public static void execSuffixIncNumber(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value++;
            }
        }




        public static void execSuffixDec(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);
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
                        double n = TypeConverter.ConvertToNumber(v, player, step.token);

                        step.reg.getISlot(scope).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.ILeftValue)step.arg1).getISlot(scope).directSet(num);
                    }
                    break;
            }


        }

        public static void execSuffixDecInt(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value--;
            }
        }

        public static void execSuffixDecUInt(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value--;
            }
                    
        }

        public static void execSuffixDecNumber(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {

            var v = step.arg1.getValue(scope);
            {
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                step.reg.getISlot(scope).setValue(iv.value);

                iv.value--;
            }
                    
        }


    }


}
