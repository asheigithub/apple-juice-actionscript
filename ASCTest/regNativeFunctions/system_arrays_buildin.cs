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
            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "_system_Array_creator_", default(Array)));
            bin.regNativeFunction(new system_array_length());
            bin.regNativeFunction(new system_array_rank());
            bin.regNativeFunction(new system_array_getlength());
            bin.regNativeFunction(new system_array_getlowerbound());
            bin.regNativeFunction(new system_array_getupperbound());
            bin.regNativeFunction(new system_array_ctor<object>("_system_Array_ctor_"));
            bin.regNativeFunction(new system_array_static_createinstance());
            bin.regNativeFunction(new system_array_getValue());
            bin.regNativeFunction(new system_array_setValue());
            
        }
    }

    class system_array_static_createinstance : NativeConstParameterFunction
    {
        public system_array_static_createinstance() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_Array_static_createInstance";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            success = true;

            if (argements[0].rtType == RunTimeDataType.rt_null)
            {
                success = false;
                stackframe.throwArgementException(token,"参数elementType不能为null");

                return;
            }

            Class cls = bin.getClassByRunTimeDataType( argements[0].rtType).instanceClass;
            int length = TypeConverter.ConvertToInt(argements[1], stackframe, token);

            Class _array_ = ((ASBinCode.rtData.rtObject)thisObj).value._class;

            var arr = stackframe.player.alloc_pureHostedOrLinkedObject(_array_.instanceClass);

            LinkSystemObject lo = (LinkSystemObject)arr.value;


            try
            {
                RunTimeDataType et = cls.getRtType();
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(cls, out ot))
                {
                    et = ot;
                }

                Type elementType = stackframe.player.linktypemapper.getLinkType(et);
                lo.SetLinkData(Array.CreateInstance(elementType, length));

                returnSlot.directSet(arr);
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwArgementException(token, "类型"+ cls +"不是一个链接到系统类库的对象，不能创建托管数组");
            }
            catch
            {
                throw new ASRunTimeException();
            }
            
            
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
    class system_array_getlength : NativeConstParameterFunction
    {
        public system_array_getlength() : base(1)
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
                return "_system_Array_getLength_";
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
            int dimension = TypeConverter.ConvertToInt( argements[0],stackframe,token);

            var array = (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            returnSlot.setValue(array.GetLength(dimension));

        }

    }
    class system_array_getlowerbound : NativeConstParameterFunction
    {
        public system_array_getlowerbound() : base(1)
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
                return "_system_Array_getLowerBound_";
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
            int dimension = TypeConverter.ConvertToInt(argements[0], stackframe, token);

            var array = (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            returnSlot.setValue(array.GetLowerBound(dimension));

        }

    }
    class system_array_getupperbound : NativeConstParameterFunction
    {
        public system_array_getupperbound() : base(1)
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
                return "_system_Array_getUpperBound_";
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
            int dimension = TypeConverter.ConvertToInt(argements[0], stackframe, token);

            var array = (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            returnSlot.setValue(array.GetUpperBound(dimension));

        }

    }
    class system_array_getValue : NativeConstParameterFunction
    {
        public system_array_getValue() : base(1)
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
                return "_system_Array_getValue_";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {


            int index = TypeConverter.ConvertToInt(argements[0], stackframe, token);

            Array array =
                (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object obj = array.GetValue(index);
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, array.ToString() + "没有链接到脚本");
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

    class system_array_setValue : NativeConstParameterFunction
    {
        public system_array_setValue() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_Array_setValue_";
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
            int index = TypeConverter.ConvertToInt(argements[1], stackframe, token);

            Array array =
                (Array)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0], array.GetType().GetElementType(), bin, out lo
                    ))
                {
                    array.SetValue(lo, index);
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        stackframe.player.linktypemapper.getRuntimeDataType(array.GetType().GetElementType())
                        );
                    success = false;
                }
                
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
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

    

}
