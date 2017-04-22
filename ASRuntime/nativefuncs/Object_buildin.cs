using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Object_toString : NativeFunctionBase
    {
        public override bool isMethod
        {
            get
            {
                return false;
            }
        }

        public override string name
        {
            get
            {
                return "_Object_toString";
            }
        }

        private List<RunTimeDataType> para = new List<RunTimeDataType>();
        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_string;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            return  new ASBinCode.rtData.rtString( TypeConverter.ConvertToString(thisObj, null, null));


        }
    }
}
