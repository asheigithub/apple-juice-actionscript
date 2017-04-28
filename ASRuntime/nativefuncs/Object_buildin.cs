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

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            return  new ASBinCode.rtData.rtString( TypeConverter.ConvertToString(thisObj, null, null));


        }
    }

    //class Object_init : NativeFunctionBase
    //{
    //    public Object_init()
    //    {
    //        para.Add(RunTimeDataType.rt_void);
    //    }

    //    public override bool isMethod
    //    {
    //        get
    //        {
    //            return true;
    //        }
    //    }

    //    public override string name
    //    {
    //        get
    //        {
    //            return "_Object_init";
    //        }
    //    }

    //    private List<RunTimeDataType> para = new List<RunTimeDataType>();
    //    public override List<RunTimeDataType> parameters
    //    {
    //        get
    //        {
    //            return para;
    //        }
    //    }

    //    public override RunTimeDataType returnType
    //    {
    //        get
    //        {
    //            return RunTimeDataType.fun_void;
    //        }
    //    }

    //    public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
    //    {
    //        errormessage = null;
    //        errorno = 0;

    //        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].directSet(argements[0].getValue());

    //        return ASBinCode.rtData.rtUndefined.undefined;


    //    }
    //}

}
