using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASRuntime.nativefuncs;
using ASRuntime;

namespace ASCTest.regNativeFunctions
{

    class system_int64_ctor : NativeFunctionBase
    {
        public system_int64_ctor()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_Int64_ctor";
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
            errormessage = null;errorno = 0;
            ((LinkObj<long>)((ASBinCode.rtData.rtObject)thisObj).value).value = (long)ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            return ASBinCode.rtData.rtUndefined.undefined;

        }
    }

    //class system_int64_toString : NativeFunctionBase
    //{
    //    public system_int64_toString()
    //    {
    //        para = new List<RunTimeDataType>();
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
    //            return "_system_Int64_toString";
    //        }
    //    }

    //    List<RunTimeDataType> para;
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
    //            return RunTimeDataType.rt_string;
    //        }
    //    }

    //    public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
    //    {
    //        errormessage = null; errorno = 0;
    //        LinkObj<long> i64 = ((LinkObj<long>)((ASBinCode.rtData.rtObject)thisObj).value);
    //        return new ASBinCode.rtData.rtString(i64.value.ToString());

    //    }
    //}

    class system_int64_explicit_from : NativeFunctionBase
    {
        public system_int64_explicit_from()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_Int64_explicit_from_";
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
            errormessage = null;
            errorno = 0;

            ASRuntime.StackFrame frame = (ASRuntime.StackFrame)stackframe;

            var v = (frame.player.alloc_pureHostedOrLinkedObject(((ASBinCode.rtData.rtObject)thisObj).value._class.instanceClass));

            LinkObj<long> obj =
                (LinkObj<long>)(v.value);
            obj.value = (long)ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            //LinkObj_Int64 obj = new LinkObj_Int64( ((ASBinCode.rtData.rtObject)thisObj).value._class);

            //return new ASBinCode.rtData.rtObject(obj, new RunTimeScope());
            return v;
        }
    }


    class system_int64_implicit_from : NativeFunctionBase
    {
        public system_int64_implicit_from()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_Int64_implicit_from_";
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
            errormessage = null;
            errorno = 0;

            ASRuntime.StackFrame frame = (ASRuntime.StackFrame)stackframe;

            var v = (frame.player.alloc_pureHostedOrLinkedObject(((ASBinCode.rtData.rtObject)thisObj).value._class.instanceClass));

            LinkObj<long> obj =
                (LinkObj<long>)(v.value);
            obj.value = (long)ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            
            return v;
        }
    }


    class system_int64_valueOf : NativeFunctionBase
    {
        public system_int64_valueOf()
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
                return "_system_Int64_valueOf";
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
                return RunTimeDataType.rt_number;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null; errorno = 0;
            LinkObj<long> i64 = ((LinkObj<long>)((ASBinCode.rtData.rtObject)thisObj).value);
            
            return new ASBinCode.rtData.rtNumber(i64.value);

        }
    }


    sealed class system_int64_MaxValue_getter : NativeFunctionBase
    {
        public system_int64_MaxValue_getter()
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
                return "_system_Int64_MaxValue_getter";
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

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.normal_1;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            var maxValue = Int64.MaxValue;

            ((ASRuntime.StackSlot)returnSlot)
                .setLinkObjectValue<long>(
                bin.getClassByRunTimeDataType(
                functionDefine.signature.returnType
                ) 
                ,
                ((ASRuntime.StackFrame)stackframe).player
                ,maxValue);

            success = true;
        }

    }

    sealed class system_int64_MinValue_getter : NativeFunctionBase
    {
        public system_int64_MinValue_getter()
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
                return "_system_Int64_MinValue_getter";
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

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.normal_1;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            var maxValue = Int64.MinValue;

            ((ASRuntime.StackSlot)returnSlot)
                .setLinkObjectValue<long>(
                bin.getClassByRunTimeDataType(
                functionDefine.signature.returnType
                )
                ,
                ((ASRuntime.StackFrame)stackframe).player
                , maxValue);

            success = true;
        }

    }


    sealed class system_int64_static_Parse : NativeFunctionBase
    {
        public system_int64_static_Parse()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_string);
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
                return "_system_Int64_static_parse";
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

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.normal_1;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            long v = long.Parse(ASRuntime.TypeConverter.ConvertToString(argements[0].getValue(), null, null));

            ((ASRuntime.StackSlot)returnSlot)
                .setLinkObjectValue<long>(
                bin.getClassByRunTimeDataType(
                functionDefine.signature.returnType
                )
                ,
                ((ASRuntime.StackFrame)stackframe).player
                , v);

            success = true;
        }

    }

    class system_int64_toString_ : NativeConstParameterFunction
    {
        public system_int64_toString_() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_string);
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
                return "_system_Int64_toString_";
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
                return RunTimeDataType.rt_string;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            string format = TypeConverter.ConvertToString(argements[0], stackframe, token);


            LinkObj<Int64> obj = ((LinkObj<Int64>)((ASBinCode.rtData.rtObject)thisObj).value);

            returnSlot.setValue(obj.value.ToString(format));
            success = true;
        }

    }

}
