using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtData;

namespace ASRuntime
{
    /// <summary>
    /// 堆区的存储结构
    /// </summary>
    class HeapSlot : ASBinCode.ISLOT
    {
        public HeapSlot()
        {
            rtType = RunTimeDataType.unknown;
            value = null;
        }

        private IRunTimeValue value;
        private RunTimeDataType rtType;
        public void setDefaultType(RunTimeDataType type)
        {
            rtType = type;
            value = TypeConverter.getDefaultValue(rtType).getValue(null);
        }


        public void directSet(IRunTimeValue value)
        {
            //value只会在内部new出来，因此，如果value不为null,也肯定是自己new出来的
            rtType = value.rtType;

            if (this.value == null || this.value.rtType != rtType)
            {
                //new 一个
                this.value = (IRunTimeValue)value.Clone();
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
                    case RunTimeDataType.unknown:
                        this.value = null;
                        break;
                    default:
                        break;
                }
            }
        }

        public IRunTimeValue getValue()
        {
            return value;
        }

        public void setValue(string value)
        {
            throw new NotImplementedException();
        }

        public void setValue(rtUndefined value)
        {
            throw new NotImplementedException();
        }

        public void setValue(rtNull value)
        {
            throw new NotImplementedException();
        }

        public void setValue(uint value)
        {
            throw new NotImplementedException();
        }

        public void setValue(int value)
        {
            throw new NotImplementedException();
        }

        public void setValue(double value)
        {
            throw new NotImplementedException();
        }

        public void setValue(rtBoolean value)
        {
            throw new NotImplementedException();
        }
    }
}
