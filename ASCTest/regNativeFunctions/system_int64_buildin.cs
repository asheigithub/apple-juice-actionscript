using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASCTest.regNativeFunctions
{
    //sealed class LinkObj_Int64 : LinkSystemObject
    //{
    //    internal long value;

    //    public LinkObj_Int64(Class cls):base(cls)
    //    {
    //        value = 0;
    //    }

    //    public override LinkSystemObject Clone()
    //    {
    //        return new LinkObj_Int64(_class) { value = value };
    //    }

    //    public override void CopyData(LinkSystemObject other)
    //    {
    //        value = ((LinkObj_Int64)other).value;
    //    }


    //    public sealed override bool Equals(object obj)
    //    {
    //        LinkObj_Int64 r = obj as LinkObj_Int64;
    //        if (r == null)
    //        {
    //            return false;
    //        }
    //        return value == r.value;

    //    }



    //    public sealed override int GetHashCode()
    //    {
    //        return value.GetHashCode();
    //    }
    //}


    //class system_int64_creator : NativeFunctionBase
    //{
    //    public system_int64_creator()
    //    {
    //        para = new List<RunTimeDataType>();
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
    //            return "_system_Int64_creator__";
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
    //            return RunTimeDataType._OBJECT;
    //        }
    //    }

    //    public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
    //    {
    //        errormessage = null;
    //        errorno = 0;

    //        int classid =  ((ASBinCode.rtData.rtInt)argements[0].getValue()).value;
            

    //        LinkObj<long> obj = new LinkObj<long>((ASBinCode.rtti.Class)stackframe);

    //        return new ASBinCode.rtData.rtObject(obj, null);

    //    }
    //}


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
                return RunTimeDataType._OBJECT;
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


    //class system_int64_MaxValue_getter : NativeFunctionBase
    //{
    //    public system_int64_MaxValue_getter()
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
    //            return "_system_Int64_MaxValue_getter";
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
    //            return RunTimeDataType.rt_void;
    //        }
    //    }

    //    private ASBinCode.rtData.rtObject maxvalue = null;

    //    public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
    //    {
    //        errormessage = null; errorno = 0;
    //        //LinkObj_Int64 i64 = ((LinkObj_Int64)((ASBinCode.rtData.rtObject)thisObj).value);

    //        //return new ASBinCode.rtData.rtNumber(i64.value);
    //        if (maxvalue == null)
    //        {
    //            ASRuntime.StackFrame frame = (ASRuntime.StackFrame)stackframe;

    //            var obj = frame.player.alloc_pureHostedOrLinkedObject(
    //                ((ASBinCode.rtData.rtObject)thisObj).value._class.instanceClass);

    //            LinkObj<long> i64 = (LinkObj<long>)obj.value;
    //            i64.value = long.MaxValue;
    //            maxvalue = obj;
    //        }
    //        return maxvalue;
    //    }
    //}

    class system_int64_MaxValue_setter : NativeFunctionBase
    {
        public system_int64_MaxValue_setter()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_Int64_MaxValue_setter";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null; errorno = 0;


            return ASBinCode.rtData.rtUndefined.undefined;

        }
    }


    class system_int64_MinValue_getter : NativeFunctionBase
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

        private ASBinCode.rtData.rtObject minvalue = null;

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null; errorno = 0;
            
            if (minvalue == null)
            {
                ASRuntime.StackFrame frame = (ASRuntime.StackFrame)stackframe;

                var obj = frame.player.alloc_pureHostedOrLinkedObject(
                    ((ASBinCode.rtData.rtObject)thisObj).value._class.instanceClass);

                LinkObj<long> i64 = (LinkObj<long>)obj.value;
                i64.value = long.MinValue;
                minvalue = obj;
            }
            return minvalue;
        }
    }

}
