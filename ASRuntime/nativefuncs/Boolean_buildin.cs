using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Boolean_toString : NativeFunctionBase
    {
        public Boolean_toString()
        {
            _paras = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_boolean_toString";
            }
        }

        private List<RunTimeDataType> _paras;

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_string;
            }
        }

        
        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            
            
                
            errormessage = null;
            errorno = 0;

            ASBinCode.rtData.rtBoolean b = (ASBinCode.rtData.rtBoolean)((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue();


            if (b.value == true)
            {
                return new ASBinCode.rtData.rtString("true");
            }
            else
            {
                return new ASBinCode.rtData.rtString("false");
            }

        }

    }


}
