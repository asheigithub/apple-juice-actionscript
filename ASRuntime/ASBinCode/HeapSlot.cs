using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtData;

namespace ASBinCode
{
    /// <summary>
    /// 堆区的存储结构
    /// </summary>
    public class HeapSlot : ASBinCode.SLOT
    {
        
        public HeapSlot()
        {
            rtType = RunTimeDataType.unknown;
            value = null;
        }

        private RunTimeValueBase value;
        private RunTimeDataType rtType;

        //public sealed override bool isPropGetterSetter
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        public void setDefaultType(RunTimeDataType type,RunTimeValueBase v)
        {
            rtType = type;
            value = v; //TypeConverter.getDefaultValue(rtType).getValue(null);
        }

		public override SLOT assign(RunTimeValueBase value, out bool success)
		{
			//throw new NotImplementedException();
			success = directSet(value);
			return this;
		}

		public override bool directSet(RunTimeValueBase value)
        {
            //value只会在内部new出来，因此，如果value不为null,也肯定是自己new出来的
            rtType = value.rtType;

            if (this.value == null || this.value.rtType != rtType)
            {
                //new 一个
                this.value = (RunTimeValueBase)value.Clone();
            }
            else
            {
                switch (value.rtType)
                {
                    case RunTimeDataType.rt_boolean:
                        this.value = value;
                        break;
                    case RunTimeDataType.rt_int:
                        ((rtInt)this.value).value = ((rtInt)value).value;
                        break;
                    case RunTimeDataType.rt_uint:
                        ((rtUInt)this.value).value = ((rtUInt)value).value;
                        break;
                    case RunTimeDataType.rt_number:
                        ((rtNumber)this.value).value = ((rtNumber)value).value;
                        break;
                    case RunTimeDataType.rt_string:
                        ((rtString)this.value).value = ((rtString)value).value;
                        break;
                    case RunTimeDataType.rt_void:
                        this.value = value;
                        break;
                    case RunTimeDataType.rt_null:
                        this.value = value;
                        break;
                    case RunTimeDataType.rt_function:
                        ((rtFunction)this.value).CopyFrom((rtFunction)value);
                        
                        break;
                    case RunTimeDataType.fun_void:
                        this.value = value;
                        break;
                    case RunTimeDataType.rt_array:
                        //((rtArray)this.value).CopyFrom((rtArray)value);
                        this.value = value;
                        break;
                    case RunTimeDataType.unknown:
                        this.value = null;
                        break;
                    default:
                        if (!ReferenceEquals(this.value, value))
                        {
                            rtObjectBase ro = (rtObjectBase)value;
                            var _class = ro.value._class;

                            //if (this.value.rtType == value.rtType)//前面已有判断这里肯定相同
                            {
                                
                                if (_class.isLink_System)
                                {
                                    if (_class.isStruct)
                                    {
                                        ((rtti.LinkSystemObject)((rtObjectBase)this.value).value)
                                            .CopyStructData((rtti.LinkSystemObject)ro.value);
                                    }
                                    else
                                    {
										var dst = ((rtObjectBase)this.value).value;
										var src = ro.value;

										((rtti.LinkObj<object>)dst).SetLinkData(((rtti.LinkObj<object>)src).value);

										//((rtObjectBase)this.value).value = ro.value; 
									}
								}
                                else
                                {
                                    this.value = value;
                                }
                            }
                            //else
                            //{
                            //    this.value = (RunTimeValueBase)value.Clone(); //考虑处理结构类型struct
                            //}
                        }
                        //this.value = (rtObject)value.Clone();
                        //if (((rtObject)this.value).value.objectid != ((rtObject)value).value.objectid)
                        //{

                        //}


                        //((rtObject)this.value).CopyFrom((rtObject)value);
                        break;
                }
            }
            return true;
        }

        public sealed override RunTimeValueBase getValue()
        {
            return value;
        }

        public sealed override void setValue(string value)
        {
            throw new NotImplementedException();
        }

        public sealed override void setValue(rtUndefined value)
        {
            throw new NotImplementedException();
        }

        public sealed override void setValue(rtNull value)
        {
            throw new NotImplementedException();
        }

        public sealed override void setValue(uint value)
        {
            throw new NotImplementedException();
        }

        public sealed override void setValue(int value)
        {
            throw new NotImplementedException();
            
        }

        public sealed override void setValue(double value)
        {
            throw new NotImplementedException();
        }

        public sealed override void setValue(rtBoolean value)
        {
            throw new NotImplementedException();
        }

        public sealed override void clear()
        {
            throw new NotImplementedException();
        }
    }
}
