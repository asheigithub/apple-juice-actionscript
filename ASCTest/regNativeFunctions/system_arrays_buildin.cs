using ASBinCode;
using ASBinCode.rtti;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;
using ASRuntime;

namespace ASCTest.regNativeFunctions
{
    class system_arrays_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(new system_array_length());
            bin.regNativeFunction(new system_array_rank());

            bin.regNativeFunction(new system_array_ctor<object>("_system_ArrayOfObject_ctor_"));

            bin.regNativeFunction(new system_array_ctor<int>("_system_ArrayOfInt_ctor_"));
            bin.regNativeFunction(new system_arrayOfInt_getValue());
            bin.regNativeFunction(new system_arrayOfInt_setValue());

            bin.regNativeFunction(new system_array_ctor<string>("_system_ArrayOfString_ctor_"));
            bin.regNativeFunction(new system_arrayOfString_getValue());
            bin.regNativeFunction(new system_arrayOfString_setValue());

        }
    }

    class system_array_length : NativeConstParameterFunction
    {
        public system_array_length() : base(0)
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
                return "_system_Array_length_";
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
                return RunTimeDataType.rt_int;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            success = true;


            var array = (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            returnSlot.setValue(array.Length);

        }


        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null; errorno = 0;

        //    ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value =
        //         new int[1];

        //    return ASBinCode.rtData.rtUndefined.undefined;

        //}


    }
    class system_array_rank : NativeConstParameterFunction
    {
        public system_array_rank() : base(0)
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
                return "_system_Array_rank_";
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
                return RunTimeDataType.rt_int;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            success = true;


            var array = (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            returnSlot.setValue(array.Rank);

        }


        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null; errorno = 0;

        //    ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value =
        //         new int[1];

        //    return ASBinCode.rtData.rtUndefined.undefined;

        //}


    }

    class system_array_ctor<T> : NativeConstParameterFunction
    {
        private string funcname;
        public system_array_ctor(string funcname):base(1)
        {
            this.funcname = funcname;
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
        }
        List<RunTimeDataType> para;
        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
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
                return funcname;
            }
        }
        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }
        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            success = true;

            int length = TypeConverter.ConvertToInt(argements[0], stackframe, token);
            ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value =
                 new T[length];

            returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

        }
    }



    //class system_arrayOfInt_ctor : NativeConstParameterFunction
    //{
    //    public system_arrayOfInt_ctor():base(1)
    //    {
    //        para = new List<RunTimeDataType>();
    //        para.Add(RunTimeDataType.rt_int);
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
    //            return "_system_ArrayOfInt_ctor_";
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

    //    public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
    //    {
    //        success = true;

    //        int length = TypeConverter.ConvertToInt(argements[0],stackframe,token);
    //        ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value =
    //             new int[length];

    //        returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

    //    }


    //    //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
    //    //{
    //    //    errormessage = null; errorno = 0;

    //    //    ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value =
    //    //         new int[1];

    //    //    return ASBinCode.rtData.rtUndefined.undefined;

    //    //}


    //}

    class system_arrayOfInt_getValue : NativeConstParameterFunction
    {
        public system_arrayOfInt_getValue() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_ArrayOfInt_getValue_";
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
                return RunTimeDataType.rt_int;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
           

            int index = TypeConverter.ConvertToInt(argements[0], stackframe, token);

            Array array =
                (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }

            
        }


    }
    class system_arrayOfInt_setValue : NativeConstParameterFunction
    {
        public system_arrayOfInt_setValue() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_ArrayOfInt_setValue_";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            int value = TypeConverter.ConvertToInt(argements[0], stackframe, token);

            int index = TypeConverter.ConvertToInt(argements[1], stackframe, token);

            Array array =
                (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                array.SetValue(value, index);
                returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                success = true;
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null; errorno = 0;

        //    ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value =
        //         new int[1];

        //    return ASBinCode.rtData.rtUndefined.undefined;

        //}


    }


    class system_arrayOfString_getValue : NativeConstParameterFunction
    {
        public system_arrayOfString_getValue() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_ArrayOfString_getValue_";
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


            int index = TypeConverter.ConvertToInt(argements[0], stackframe, token);

            Array array =
                (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                returnSlot.setValue((string)array.GetValue(index));
                success = true;
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }
    class system_arrayOfString_setValue : NativeConstParameterFunction
    {
        public system_arrayOfString_setValue() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_string);
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_ArrayOfString_setValue_";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            string value = TypeConverter.ConvertToString(argements[0], stackframe, token);

            int index = TypeConverter.ConvertToInt(argements[1], stackframe, token);

            Array array =
                (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                array.SetValue(value, index);
                returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                success = true;
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null; errorno = 0;

        //    ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value =
        //         new int[1];

        //    return ASBinCode.rtData.rtUndefined.undefined;

        //}


    }



}
