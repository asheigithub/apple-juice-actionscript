using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class RightValue : IRightValue
    {
        private RunTimeValueBase value;

        public RightValue(RunTimeValueBase value)
        {
            this.value = value;
        }

        public RunTimeDataType valueType
        {
            get
            {
                return value.rtType;
            }
        }

        public RunTimeValueBase getValue(RunTimeScope scope)
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }


    //public class RightValue<T> :IRightValue
    //    where T :IRunTimeValue 
    //{
    //    private T value;
    //    public RightValue(T value)
    //    {
    //        this.value = value;
    //    }

    //    public RunTimeDataType valueType
    //    {
    //        get
    //        {
    //            return value.rtType;
    //        }
    //    }

    //    public IRunTimeValue getValue(IRunTimeScope scope)
    //    {
    //        return value;
    //    }


    //    public override string ToString()
    //    {
    //        return value.ToString();
    //    }

        

    //}
}
