using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public sealed class rtObject :RunTimeValueBase
    {

        public RunTimeScope objScope;
        public rtti.Object value;

        public rtObject(rtti.Object v, RunTimeScope scope):base(v._class.classid + RunTimeDataType._OBJECT)
        {
            value = v;
            objScope = scope;
            
        }

        public override double toNumber()
        {
            return double.NaN;
        }

       
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
