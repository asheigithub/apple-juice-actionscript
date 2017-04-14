using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtObject :IRunTimeValue
    {

        public IRunTimeScope objScope;
        public rtti.Object value;
        
        public rtObject(rtti.Object v,IRunTimeScope scope)
        {
            value = v;
            objScope = scope;
            
        }

        public  RunTimeDataType rtType
        {
            get
            {
                return value._class.classid+RunTimeDataType._OBJECT;
            }
        }

        public object Clone()
        {
            return this;
            //var result= new rtObject(value,objScope);
            //return result;
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
