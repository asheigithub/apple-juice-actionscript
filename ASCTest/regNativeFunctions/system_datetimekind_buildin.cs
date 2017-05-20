using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_DateTimeKind_ctor : NativeFunctionBase
    {
        public system_DateTimeKind_ctor()
        {
            para = new List<RunTimeDataType>();
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
                return "_system_DateTimeKind_ctor";
            }
        }

        List<RunTimeDataType> para;
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
                return RunTimeDataType.rt_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null; errorno = 0;
            return ASBinCode.rtData.rtUndefined.undefined;

        }
    }
}
