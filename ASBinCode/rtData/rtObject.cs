using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtObject :RunTimeValueBase
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

        public sealed override  object Clone()
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
