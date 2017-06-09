using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public sealed class rtObject :RunTimeValueBase
    {

        public RunTimeScope objScope;
        public rtti.Object value;

        /// <summary>
        /// 仅用于创建用于StackSlot的缓存对象。
        /// </summary>
        /// <returns></returns>
        public static rtObject create_cache_linkObject()
        {
            return new rtObject();
        }
        
        private rtObject() : base(RunTimeDataType.unknown)
        {
            value = null;
            objScope = new RunTimeScope(null,-2,null,this, RunTimeScopeType.objectinstance );
        }

        public rtObject(rtti.Object v, RunTimeScope scope):base(v._class.classid + RunTimeDataType._OBJECT)
        {
            value = v;
            objScope = scope;
            
        }

        public override double toNumber()
        {
            return double.NaN;
        }

        /// <summary>
        /// 仅用于作为cache时，清空缓存
        /// </summary>
        public void cache_clear()
        {
            value = null;
            rtType = RunTimeDataType.unknown;

            objScope.blockId = -2;

        }

        /// <summary>
        /// 仅用于更新链接的类对象
        /// </summary>
        /// <param name="clsType"></param>
        /// <param name="linkObject"></param>
        public void cache_setTypeAndLinkObject(rtti.Class clsType,object linkObject)
        {
#if DEBUG
            if (clsType.isStruct)
            {
                throw new ASRunTimeException();
            }
#endif
            ((rtti.LinkSystemObject)value).SetLinkData(linkObject);
            rtType = clsType.classid + RunTimeDataType._OBJECT;

            objScope.blockId = clsType.blockid;

        }

        public void cache_setValue(ASBinCode.rtti.LinkSystemObject otherValue)
        {
            if (otherValue._class.isStruct)
            {
                if (rtType == otherValue._class.classid + RunTimeDataType._OBJECT)
                {
                    ((rtti.LinkSystemObject)value).CopyStructData(otherValue);
                }
                else
                {
                    {
                        value = otherValue.Clone();
                    }  
                }
            }
            else
            {
                //if (rtType == otherValue._class.classid + RunTimeDataType._OBJECT)
                //{
                //    ((rtti.LinkSystemObject)value).SetLinkData(otherValue.GetLinkData());
                //}
                //else
                //{
                //    value = otherValue.Clone();
                //}
                value = otherValue;
            }

            objScope.blockId = value._class.blockid;
            rtType = otherValue._class.classid + RunTimeDataType._OBJECT;
        }




        //public void ReplaceStructValue(rtObject other)
        //{
        //    other.objScope = objScope;
            
        //    ((rtti.LinkSystemObject)other.value).CopyStructData((rtti.LinkSystemObject)value);
        //}

        public sealed override  object Clone()
        {
            if (value._class.isLink_System)
            {
                rtti.LinkSystemObject lobj = (rtti.LinkSystemObject)value;
                //if (lobj.__iscreateout)
                //{
                    //lobj.__iscreateout = false;

                //    return this;
                //}
                //else
                //{
                    rtObject clone = new rtObject(((rtti.LinkSystemObject)value).Clone(),
                        null
                        );

                RunTimeScope scope =
                    new RunTimeScope(null, objScope.blockId, null, 
                    clone, RunTimeScopeType.objectinstance);
                clone.objScope = scope;

                return clone;

                //}
            }
            else
            {
                return this;
            }
            //var result= new rtObject(value,objScope);
            //return result;
        }


        public override bool Equals(object obj)
        {
            rtObject o = obj as rtObject;
            if (o == null)
            {
                return false;
            }

            return value.Equals(o.value);
        }


        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        
        public override string ToString()
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value.ToString();
        }

        
    }
}
