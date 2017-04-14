using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public class Vector_Data
    {
        public bool isFixed;

        public List<IRunTimeValue> innnerList;

        public readonly RunTimeDataType vector_type;

        public Vector_Data(RunTimeDataType vector_type)
        {
            this.vector_type = vector_type;
        }


    }
}
