using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Error_getstack : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Error_getstack()
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
                return "Error_getstack";
            }
        }

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

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            StackFrame frame = (StackFrame)stackframe;
            return new rtString(frame.player.stackTrace(1));

            //throw new NotImplementedException();
        }
    }

}
