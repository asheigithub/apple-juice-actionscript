using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtData;

namespace ASRuntime
{
    /// <summary>
    /// 程序执行栈的存储结构
    /// </summary>
    class StackSlot : ISLOT
    {
        public StackSlot()
        {
            store = new IRunTimeValue[(int)RunTimeDataType.unknown+1];
            index = (int)RunTimeDataType.unknown;

            //存储器设置初始值
            clear();

        }

        

        private int index;
        private IRunTimeValue[] store;

        

        public void directSet(IRunTimeValue value)
        {
            index = (int)value.rtType;
            //store[index] = value;

            //必须拷贝!!否则值可能被其他引用而导致错误
            //私有构造函数的数据可以直接传引用，否则必须拷贝赋值。
            switch (value.rtType)
            {
                case RunTimeDataType.rt_boolean:
                    store[index] = value;       
                    break;
                case RunTimeDataType.rt_int:
                    setValue( ((rtInt)value).value);
                    break;
                case RunTimeDataType.rt_uint:
                    setValue(((rtUInt)value).value);
                    break;
                case RunTimeDataType.rt_number:
                    setValue(((rtNumber)value).value);
                    break;
                case RunTimeDataType.rt_string:
                    setValue(((rtString)value).value);
                    break;
                case RunTimeDataType.rt_void:
                    store[index] = value;
                    break;
                case RunTimeDataType.rt_null:
                    store[index] = value;
                    break;
                case RunTimeDataType.rt_function:
                    {
                        if (store[index].rtType == RunTimeDataType.rt_null)
                        {
                            store[index] = (rtFunction)value.Clone();
                        }
                        else
                        {
                            ((rtFunction)store[index]).CopyFrom((rtFunction)value);
                        }
                    }
                    break;
                case RunTimeDataType.fun_void:
                    store[index] = value;
                    break;
                case RunTimeDataType.unknown:
                    store[index] = null;
                    break;
                default:
                    break;
            }

        }

        public IRunTimeValue getValue()
        {
            return store[index];
            //throw new NotImplementedException();
        }

        public void setValue(string value)
        {
            //throw new NotImplementedException();
            index = (int)RunTimeDataType.rt_string;


            if (value == null)
            {
                store[(int)RunTimeDataType.rt_string] = rtNull.nullptr;
            }
            else
            {
                if (store[(int)RunTimeDataType.rt_string].rtType == RunTimeDataType.rt_null)
                {
                    store[(int)RunTimeDataType.rt_string] = new rtString(value);
                }
                else
                {
                    ((rtString)store[(int)RunTimeDataType.rt_string]).value = value;
                }
            }
        }

        public void setValue(rtUndefined value)
        {
            //throw new NotImplementedException();
            index = (int)RunTimeDataType.rt_void;
            store[(int)RunTimeDataType.rt_void] = value;
        }

        public void setValue(rtNull value)
        {
            index = (int)RunTimeDataType.rt_null;
            store[(int)RunTimeDataType.rt_null] = value;
        }

        public void setValue(uint value)
        {
            index = (int)RunTimeDataType.rt_uint;
            ((rtUInt)store[(int)RunTimeDataType.rt_uint]).value = value;
        }

        public void setValue(int value)
        {
            index = (int)RunTimeDataType.rt_int;
            ((rtInt)store[(int)RunTimeDataType.rt_int]).value = value;
        }

        public void setValue(double value)
        {
            index = (int)RunTimeDataType.rt_number;
            ((rtNumber)store[(int)RunTimeDataType.rt_number]).value = value;
        }

        public void setValue(rtBoolean value)
        {
            index = (int)RunTimeDataType.rt_boolean;
            store[(int)RunTimeDataType.rt_boolean] = value;
        }

        public void clear()
        {
            for (int i = 0; i < RunTimeDataType.unknown ; i++)
            {
                RunTimeDataType t = (RunTimeDataType)i;

                {
                    store[i] = TypeConverter.getDefaultValue(t).getValue(null);
                }
            }
        }
    }
}
