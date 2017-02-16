using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    
    public class RightValue<T> :IRightValue
        where T :IRunTimeValue 
    {
        private T value;
        public RightValue(T value)
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

        public IRunTimeValue getValue(IList<IEAX> temporarys, IRunTimeScope scope)
        {
            return value;
        }


        public override string ToString()
        {
            return value.ToString();
        }

        

    }
}
