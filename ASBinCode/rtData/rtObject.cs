using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public sealed class rtObject :RunTimeValueBase
    {

        public RunTimeScope objScope;
        public readonly rtti.Object value;
        
        public rtObject(rtti.Object v, RunTimeScope scope):base(v._class.classid + RunTimeDataType._OBJECT)
        {
            value = v;
            objScope = scope;
            
        }

        public override double toNumber()
        {
            return double.NaN;
        }

        //public  RunTimeDataType rtType
        //{
        //    get
        //    {
        //        return value._class.classid+RunTimeDataType._OBJECT;
        //    }
        //}

        public void ReplaceValue(rtObject other)
        {
            other.objScope = objScope;
            ((rtti.LinkSystemObject)other.value).CopyData((rtti.LinkSystemObject)value);
        }

        public sealed override  object Clone()
        {
            if (value._class.isStruct)
            {
                rtti.LinkSystemObject lobj = (rtti.LinkSystemObject)value;
                if (lobj.__iscreateout)
                {
                    lobj.__iscreateout = false;
                    return this;
                }
                else
                {
                    rtObject clone=new rtObject(((rtti.LinkSystemObject)value).Clone(),
                        //objScope
                        null
                        );
                    return clone;

                }
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
            //return base.Equals(obj);
        }


        public override int GetHashCode()
        {
            return value.GetHashCode();
        }


        //public void CopyFrom(rtObject right)
        //{
        //    value = right.value;
        //    objScope = right.objScope;

        //}

        public override string ToString()
        {
            return value.ToString();
        }

        
    }
}
