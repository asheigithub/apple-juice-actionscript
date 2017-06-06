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
    public sealed class StackSlot : SLOT
    {
        public StackSlot(CSWC classfinder)
        {
            store = new RunTimeValueBase[(int)RunTimeDataType._OBJECT+1];
            index = (int)RunTimeDataType.unknown;

            _cache_vectorSlot = new operators.OpVector.vectorSLot(null, 0,classfinder);
            _cache_prototypeSlot = new operators.OpAccess_Dot.prototypeSlot(null, null, null);

            _cache_linksystemObject = rtObject.create_cache_linkObject();

            //存储器设置初始值
            for (int i = 0; i < RunTimeDataType._OBJECT+1; i++)
            {
                RunTimeDataType t = (RunTimeDataType)i;
                if(t != RunTimeDataType.unknown)
                {
                    store[i] = TypeConverter.getDefaultValue(t).getValue(null,null);
                }
            }


            _numberValue = (rtNumber)store[RunTimeDataType.rt_number];
            _intValue = (rtInt)store[RunTimeDataType.rt_int];
            _uintValue = (rtUInt)store[RunTimeDataType.rt_uint];
        }

        private rtNumber _numberValue;
        private rtInt _intValue;
        private rtUInt _uintValue;

        
        internal ASBinCode.ClassPropertyGetter propGetSet;
        internal ASBinCode.rtData.rtObject propBindObj;
        internal ASBinCode.rtti.Class superPropBindClass;

        internal operators.OpVector.vectorSLot _cache_vectorSlot;
        internal operators.OpAccess_Dot.prototypeSlot _cache_prototypeSlot;

        internal rtObject _cache_linksystemObject;


        internal SLOT linktarget;
        public void linkTo(SLOT linktarget)
        {
            this.linktarget = linktarget;
        }


        private int index;
        private RunTimeValueBase[] store;

        public sealed override bool isPropGetterSetter
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
                //值类型必须拷贝!!否则值可能被其他引用而导致错误
                //私有构造函数的数据可以直接传引用，否则必须拷贝赋值。
                switch (value.rtType)
                {
                    case RunTimeDataType.rt_boolean:
                        store[index] = value;
                        break;
                    case RunTimeDataType.rt_int:
                        //setValue(((rtInt)value).value);
                        _intValue.value = ((rtInt)value).value;
                        break;
                    case RunTimeDataType.rt_uint:
                        //setValue(((rtUInt)value).value);
                        _uintValue.value = ((rtUInt)value).value;
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
                            
                        }
                        break;
                    case RunTimeDataType.unknown:
                        store[index] = null;
                        break;
                    default:
                        {
                            rtObject obj = (rtObject)value;
                            if (obj.value._class.isLink_System) 
                            {
                                //链接到系统的对象。这里需要用到缓存的rtObject，以避免当调用链接对象的方法并返回的也是链接对象时，
                                //要重新创建rtObject,而是直接更新缓存的rtObject.
                                _cache_linksystemObject.cache_setValue((ASBinCode.rtti.LinkSystemObject)obj.value);
                                store[RunTimeDataType._OBJECT] = _cache_linksystemObject;
                            }
                            else
                            {
                                store[RunTimeDataType._OBJECT] = value;
                            }
                        }
                        break;
                }
                return true;
            }
            
        }

        //仅用于链接对象的赋值更新
        public void setLinkObjectValue<T>(ASBinCode.rtti.Class clsType, Player player ,T value)
        {
            index = RunTimeDataType._OBJECT;

            if (_cache_linksystemObject.rtType == clsType.getRtType())
            {
                ((ASBinCode.rtti.LinkObj<T>)_cache_linksystemObject.value).value = value;
            }
            else
            {
                if (clsType.isStruct 
                    )
                {
                    var lk = player.alloc_LinkObjValue(clsType);
                    
                    ((ASBinCode.rtti.LinkObj<T>)lk).value = value;
                    _cache_linksystemObject.cache_setValue(lk);
                }
                else
                {
                    if (
                        _cache_linksystemObject.rtType == RunTimeDataType.unknown
                        || 
                        _cache_linksystemObject.value._class.isStruct
                        
                        )                   
                    {
                        var lk = player.alloc_LinkObjValue(clsType);
                        ((ASBinCode.rtti.LinkObj<object>)lk).value = value;
                        _cache_linksystemObject.cache_setValue(lk);
                    }
                    else
                    {
                        _cache_linksystemObject.cache_setTypeAndLinkObject(clsType, value);
                    }

                    
                }
                
            }


            store[RunTimeDataType._OBJECT] = _cache_linksystemObject;
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
            
            _cache_vectorSlot.clear();
            _cache_prototypeSlot.clear();
            _cache_linksystemObject.cache_clear();


            store[RunTimeDataType.rt_string] = rtNull.nullptr;
            store[RunTimeDataType.rt_function] = rtNull.nullptr;
            store[RunTimeDataType.rt_array] = rtNull.nullptr;
            store[RunTimeDataType._OBJECT] = rtNull.nullptr;

            index = (int)RunTimeDataType.unknown;
        }
    }
}
