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
            store = new IRunTimeValue[(int)RunTimeDataType._OBJECT+1];
            index = (int)RunTimeDataType.unknown;

            //存储器设置初始值
            for (int i = 0; i < RunTimeDataType._OBJECT+1; i++)
            {
                RunTimeDataType t = (RunTimeDataType)i;
                if(t != RunTimeDataType.unknown)
                {
                    store[i] = TypeConverter.getDefaultValue(t).getValue(null);
                }
            }

        }
        internal ASBinCode.ClassPropertyGetter propGetSet;
        internal ASBinCode.rtData.rtObject propBindObj;

        internal rtArray fromArray;
        internal int fromArrayIndex;

        internal ISLOT linktarget;
        public void linkTo(ISLOT linktarget)
        {
            this.linktarget = linktarget;
        }


        private int index;
        private IRunTimeValue[] store;

        public bool isPropGetterSetter
        {
            get
            {
                if (linktarget != null)
                {
                    return linktarget.isPropGetterSetter;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool directSet(IRunTimeValue value)
        {
            if (linktarget != null)
            {
                return  linktarget.directSet(value);
            }
            else
            {

                index = (int)value.rtType;
                if (index > RunTimeDataType.unknown) //若大于unknown,则说明是一个对象
                {
                    index = RunTimeDataType._OBJECT;
                }
                //store[index] = value;

                //值类型必须拷贝!!否则值可能被其他引用而导致错误
                //私有构造函数的数据可以直接传引用，否则必须拷贝赋值。
                switch (value.rtType)
                {
                    case RunTimeDataType.rt_boolean:
                        store[index] = value;
                        break;
                    case RunTimeDataType.rt_int:
                        setValue(((rtInt)value).value);
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
                    case RunTimeDataType.rt_array:
                        {
                            //store[index] = value;
                            if (store[index].rtType == RunTimeDataType.rt_null)
                            {
                                store[index] = (rtArray)value.Clone();
                            }
                            else
                            {
                                ((rtArray)store[index]).CopyFrom((rtArray)value);
                            }
                        }
                        break;
                    case RunTimeDataType.unknown:
                        store[index] = null;
                        break;
                    default:
                        {
                            if (store[RunTimeDataType._OBJECT].rtType == RunTimeDataType.rt_null)
                            {
                                store[RunTimeDataType._OBJECT] = (rtObject)value.Clone();
                            }
                            else
                            {
                                ((rtObject)store[RunTimeDataType._OBJECT]).CopyFrom((rtObject)value);
                            }
                        }
                        break;
                }
                return true;
            }
            
        }

        public IRunTimeValue getValue()
        {
            if (linktarget != null)
            {
                return linktarget.getValue();
            }
            else
            {
                return store[index];
            }
            //throw new NotImplementedException();
        }

        public void setValue(string value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
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
        }

        public void setValue(rtUndefined value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = (int)RunTimeDataType.rt_void;
                store[(int)RunTimeDataType.rt_void] = value;
            }
        }

        public void setValue(rtNull value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = (int)RunTimeDataType.rt_null;
                store[(int)RunTimeDataType.rt_null] = value;
            }
        }

        public void setValue(uint value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = (int)RunTimeDataType.rt_uint;
                ((rtUInt)store[(int)RunTimeDataType.rt_uint]).value = value;
            }
        }

        public void setValue(int value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = (int)RunTimeDataType.rt_int;
                ((rtInt)store[(int)RunTimeDataType.rt_int]).value = value;
            }
        }

        public void setValue(double value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = (int)RunTimeDataType.rt_number;
                ((rtNumber)store[(int)RunTimeDataType.rt_number]).value = value;
            }
        }

        public void setValue(rtBoolean value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = (int)RunTimeDataType.rt_boolean;
                store[(int)RunTimeDataType.rt_boolean] = value;
            }
        }

        public void clear()
        {
            linktarget = null;
            propGetSet = null;
            propBindObj = null;
            fromArray = null;
            fromArrayIndex = -1;

            store[RunTimeDataType.rt_string] = rtNull.nullptr;
            store[RunTimeDataType.rt_function] = rtNull.nullptr;
            store[RunTimeDataType._OBJECT] = rtNull.nullptr;

            index = (int)RunTimeDataType.unknown;
        }
    }
}
