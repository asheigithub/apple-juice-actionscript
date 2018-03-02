using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    public struct DictionaryKey:DictionaryObject.IDictionaryKey,IEquatable<DictionaryKey>
    {
        public readonly RunTimeValueBase key;
        public DictionaryKey(RunTimeValueBase key,bool needclone)
        {
			if (needclone)
			{
				key = (RunTimeValueBase)key.Clone();
			}
            this.key = key;
        }

        public override bool Equals(object obj)
        {
            if (obj is DictionaryKey)
            {
                return Equals((DictionaryKey)obj);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(DictionaryKey other)
        {
            return operators.OpLogic.StrictEqual(key, other.key);
        }

        public override int GetHashCode()
        {
            var type = key.rtType;

            switch (type)
            {
                case RunTimeDataType.fun_void:
                    return 0;
                case RunTimeDataType.rt_array:
                    return ((ASBinCode.rtData.rtArray)key).GetHashCode();
                case RunTimeDataType.rt_boolean:
                    return ((ASBinCode.rtData.rtBoolean)key).GetHashCode();
                case RunTimeDataType.rt_function:
                    return ((ASBinCode.rtData.rtFunction)key).functionId.GetHashCode();
                case RunTimeDataType.rt_int:
                    return ((double)((ASBinCode.rtData.rtInt)key).value).GetHashCode();
                case RunTimeDataType.rt_null:
                    return ((ASBinCode.rtData.rtNull)key).GetHashCode();
                case RunTimeDataType.rt_number:
                    return (((ASBinCode.rtData.rtNumber)key).value).GetHashCode();
                case RunTimeDataType.rt_string:
                    return (((ASBinCode.rtData.rtString)key).valueString()).GetHashCode();
                case RunTimeDataType.rt_uint:
                    return ((double)((ASBinCode.rtData.rtUInt)key).value).GetHashCode();
                case RunTimeDataType.rt_void:
                    return ((ASBinCode.rtData.rtUndefined)key).GetHashCode();
                case RunTimeDataType.unknown:
                    return 0;
                default:
                    return ((ASBinCode.rtData.rtObjectBase)key).GetHashCode();
                    
            }


        }
        public override string ToString()
        {
            return key.ToString();
        }
    }

    class DictionarySlot : ObjectMemberSlot,ILinkSlot
    {
        
        public DictionarySlot(ASBinCode.rtData.rtObjectBase obj,RunTimeDataType functionClassRtType) : base(obj,functionClassRtType,RunTimeDataType.rt_void,null)
        {
            propertyIsEnumerable = false;
        }

        

        internal DictionaryKey _key;

        public ILinkSlot preSlot
        {
            get
            ;

            set
            ;
        }

        public ILinkSlot nextSlot
        {
            get
            ;

            set
            ;
        }

        public bool propertyIsEnumerable
        {
            get
            ;

            set
            ;
        }

        public bool isDeleted
        {
            get
            ;

            set
            ;
        }

        public override bool directSet(RunTimeValueBase value)
        {
            base.directSet(value);
            return true;
        }

    }


}
