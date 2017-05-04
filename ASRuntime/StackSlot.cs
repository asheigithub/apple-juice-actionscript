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
    sealed class StackSlot : SLOT
    {
        public StackSlot(IClassFinder classfinder)
        {
            store = new RunTimeValueBase[(int)RunTimeDataType._OBJECT+1];
            index = (int)RunTimeDataType.unknown;

            _cache_vectorSlot = new operators.OpVector.vectorSLot(null, 0,classfinder);
            _cache_prototypeSlot = new operators.OpAccess_Dot.prototypeSlot(null, null, null);

            //存储器设置初始值
            for (int i = 0; i < RunTimeDataType._OBJECT+1; i++)
            {
                RunTimeDataType t = (RunTimeDataType)i;
                if(t != RunTimeDataType.unknown)
                {
                    store[i] = TypeConverter.getDefaultValue(t).getValue(null);
                }
            }


            _numberValue = (rtNumber)store[RunTimeDataType.rt_number];
            _intValue = (rtInt)store[RunTimeDataType.rt_int];
            _uintValue = (rtUInt)store[RunTimeDataType.rt_uint];
        }

        private rtNumber _numberValue;
        private rtInt _intValue;
        private rtUInt _uintValue;


        internal IEnumerator<RunTimeValueBase> cache_enumerator;

        internal ASBinCode.ClassPropertyGetter propGetSet;
        internal ASBinCode.rtData.rtObject propBindObj;
        internal ASBinCode.rtti.Class superPropBindClass;

        internal operators.OpVector.vectorSLot _cache_vectorSlot;
        internal operators.OpAccess_Dot.prototypeSlot _cache_prototypeSlot;

        internal SLOT linktarget;
        public void linkTo(SLOT linktarget)
        {
            this.linktarget = linktarget;
        }


        private int index;
        private RunTimeValueBase[] store;

        public sealed override  bool isPropGetterSetter
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

        public sealed override bool directSet(RunTimeValueBase value)
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
                        //setValue(((rtInt)value).value);
                        ((rtInt)store[index]).value = ((rtInt)value).value;
                        break;
                    case RunTimeDataType.rt_uint:
                        //setValue(((rtUInt)value).value);
                        ((rtUInt)store[index]).value = ((rtUInt)value).value;
                        break;
                    case RunTimeDataType.rt_number:
                        //setValue(((rtNumber)value).value);
                        _numberValue.value= ((rtNumber)value).value;
                        //((rtNumber)store[index]).value = ((rtNumber)value).value;
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
                        {//Function需要保存上下文环境。因此需要像值类型那样进行拷贝
                            
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
                            store[index] = value;
                            //if (store[index].rtType == RunTimeDataType.rt_null)
                            //{
                            //    store[index] = (rtArray)value.Clone();
                            //}
                            //else
                            //{
                            //    ((rtArray)store[index]).CopyFrom((rtArray)value);
                            //}
                        }
                        break;
                    case RunTimeDataType.unknown:
                        store[index] = null;
                        break;
                    default:
                        {
                            store[RunTimeDataType._OBJECT] = value;
                            //store[RunTimeDataType._OBJECT] = (rtObject)value.Clone();
                            //if (store[RunTimeDataType._OBJECT].rtType == RunTimeDataType.rt_null)
                            //{
                            //    store[RunTimeDataType._OBJECT] = (rtObject)value.Clone();
                            //}
                            //else if (((rtObject)store[RunTimeDataType._OBJECT]).value.objectid
                            //    != ((rtObject)value).value.objectid
                            //    )
                            //{
                            //    store[RunTimeDataType._OBJECT] = (rtObject)value.Clone();
                            //}
                            //else
                            //{
                            //    ((rtObject)store[RunTimeDataType._OBJECT]).CopyFrom((rtObject)value);
                            //}
                        }
                        break;
                }
                return true;
            }
            
        }

        public sealed override RunTimeValueBase getValue()
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

        public sealed override void setValue(string value)
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

        public sealed override void setValue(rtUndefined value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_void;
                store[index] = value;
            }
        }

        public sealed override void setValue(rtNull value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_null;
                store[index] = value;
            }
        }

        public sealed override void setValue(uint value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_uint;
                _uintValue.value = value;
                //((rtUInt)store[index]).value = value;
            }
        }

        public sealed override void setValue(int value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_int;
                _intValue.value = value;
                //((rtInt)store[index]).value = value;
            }
        }

        public sealed override void setValue(double value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_number;
                _numberValue.value = value;
                //((rtNumber)store[index]).value = value;
            }
        }

        public sealed override void setValue(rtBoolean value)
        {
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_boolean;
                store[index] = value;
            }
        }

        public sealed override void clear()
        {
            linktarget = null;
            propGetSet = null;
            propBindObj = null;
            superPropBindClass = null;
            cache_enumerator = null;
            //fromArray = null;
            //fromArrayIndex = -1;
            _cache_vectorSlot.clear();
            _cache_prototypeSlot.clear();

            store[RunTimeDataType.rt_string] = rtNull.nullptr;
            store[RunTimeDataType.rt_function] = rtNull.nullptr;
            store[RunTimeDataType.rt_array] = rtNull.nullptr;
            store[RunTimeDataType._OBJECT] = rtNull.nullptr;

            index = (int)RunTimeDataType.unknown;
        }
    }
}
