using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    public static class LinkSystem_Buildin
    {
        public static NativeFunctionBase getCreator<T>(string name, T v)
        {
            return new creator<T>(name, v);
        }
        public static NativeFunctionBase getToString(string name)
        {
            return new toString(name);
        }
        public static NativeFunctionBase getGetHashCode(string name)
        {
            return new getHashCode(name);
        }
        public static NativeFunctionBase getEquals(string name)
        {
            return new equals(name);
        }

        public static NativeFunctionBase getStruct_static_field_getter<T>
            (string name,struct_static_field_getter<T>.DelegateGetStaticValue getter)
            where T : struct
        {
            return new struct_static_field_getter<T>(name, getter);
        }

        public static NativeFunctionBase getCompareTo<T>
            (string name)
            where T : IComparable
        {
            return new compareto<T>(name);
        }

        public static NativeFunctionBase getCompareTo_Generic<T>
            (string name)
            where T : IComparable<T>
        {
            return new compareto_generic<T>(name);
        }
    }

    #region creator

    class creator<T> : NativeFunctionBase,ILinkSystemObjCreator
    {
        private T defaultvalue;
        private string funname;

        public creator(string funname, T v)
        {
            _type = typeof(T);
            this.defaultvalue = v;
            this.funname = funname;
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
                return funname;
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

            var cls = (Class)stackframe;

            if (cls.isStruct)
            {
                LinkObj<T> obj = new LinkObj<T>((Class)stackframe);

                return new ASBinCode.rtData.rtObject(obj, null);
            }
            else
            {
                LinkObj<object> obj = new LinkObj<object>((Class)stackframe);

                return new ASBinCode.rtData.rtObject(obj, null);
            }
        }

        private Type _type;
        public Type getLinkSystemObjType()
        {
            return _type;
        }

        public void setLinkObjectValueToSlot(SLOT slot,object player,  object value, Class clsType)
        {
            
            ((StackSlot)slot).setLinkObjectValue(clsType, (Player)player, (T)value);
            
        }
    }

    #endregion

    #region toString

    class toString : NativeFunctionBase
    {
        string funcname;
        public toString(string name)
        {
            funcname = name;
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
                return funcname;
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

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.normal_1;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new ASRunTimeException();
        }

        public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot,SourceToken token, object stackframe, out bool success)
        {
            //base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
            success = true;
            
            LinkSystemObject iv = ((LinkSystemObject)((ASBinCode.rtData.rtObject)thisObj).value);

            string tostr = iv.ToString();

            returnSlot.setValue(tostr);

            //return new ASBinCode.rtData.rtString(iv.ToString());


        }

    }

    #endregion

    #region getHashCode

    class getHashCode : NativeFunctionBase
    {
        string funcname;
        public getHashCode(string name)
        {
            funcname = name;
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
                return funcname;
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

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.normal_1;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new ASRunTimeException();
        }

        public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            //base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
            success = true;

            LinkSystemObject iv = ((LinkSystemObject)((ASBinCode.rtData.rtObject)thisObj).value);

            int hashcode = iv.GetHashCode();

            returnSlot.setValue(hashcode);

            //return new ASBinCode.rtData.rtString(iv.ToString());


        }

    }

    #endregion

    #region equals

    class equals : NativeConstParameterFunction
    {
        string funcname;
        public equals(string name):base(1)
        {
            funcname = name;
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
                return funcname;
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
                return RunTimeDataType.rt_boolean;
            }
        }

		//public override NativeFunctionMode mode
		//{
		//    get
		//    {
		//        return NativeFunctionMode.normal_1;
		//    }
		//}

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    throw new ASRunTimeException();
		//}

		//public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
		//{

		//    success = true;

		//    ASBinCode.rtData.rtObject obj = argements[0].getValue() as ASBinCode.rtData.rtObject;
		//    if (obj == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//    LinkSystemObject other = obj.value as LinkSystemObject;
		//    if (other == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }


		//    LinkSystemObject iv = ((LinkSystemObject)((ASBinCode.rtData.rtObject)thisObj).value);

		//    if (System.Object.Equals(iv, other))
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
		//        return;
		//    }
		//    else
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			ASBinCode.rtData.rtObject obj = argements[0] as ASBinCode.rtData.rtObject;
			if (obj == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}

			LinkSystemObject other = obj.value as LinkSystemObject;
			if (other == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}


			LinkSystemObject iv = ((LinkSystemObject)((ASBinCode.rtData.rtObject)thisObj).value);

			if (System.Object.Equals(iv, other))
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
				return;
			}
			else
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}
		}
	}

    #endregion

    #region static_field_getter

    public sealed class struct_static_field_getter<T> : NativeFunctionBase
        where T:struct
    {
        public delegate T DelegateGetStaticValue();

        DelegateGetStaticValue valueGetter;

        private string functionname;
        public struct_static_field_getter(string functionname,DelegateGetStaticValue valueGetter)
        {
            this.functionname = functionname;
            this.valueGetter = valueGetter;
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
                return functionname;
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
            var maxValue = valueGetter();

            ((StackSlot)returnSlot)
                .setLinkObjectValue<T>(
                bin.getClassByRunTimeDataType(
                functionDefine.signature.returnType
                )
                ,
                ((StackFrame)stackframe).player
                , maxValue);

            success = true;
        }

    }


    #endregion

    #region compareTo

    class compareto<T> : NativeConstParameterFunction
        where T :IComparable
    {
        private string funname;
        public compareto(string name):base(1)
        {
            this.funname = name;
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
                return funname;
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

        //public override NativeFunctionMode mode
        //{
        //    get
        //    {
        //        return NativeFunctionMode.normal_1;
        //    }
        //}

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void execute2(RunTimeValueBase thisObj,
        //    FunctionDefine functionDefine,
        //    SLOT[] argements,
        //    SLOT returnSlot,
        //    SourceToken token,
        //    object stackframe, out bool success)
        //{
        //    LinkObj<T> lobj = (LinkObj<T>)((ASBinCode.rtData.rtObject)thisObj).value;

        //    StackFrame frame = (StackFrame)stackframe;

        //    var arg = argements[0].getValue();
        //    if (arg.rtType == RunTimeDataType.rt_null)
        //    {
        //        ((StackSlot)returnSlot).setValue(lobj.value.CompareTo(null));
        //    }
        //    else
        //    {
        //        LinkSystemObject argObj = (LinkSystemObject)((ASBinCode.rtData.rtObject)arg).value;
        //        ((StackSlot)returnSlot).setValue(lobj.value.CompareTo(argObj.GetLinkData()));

        //    }
        //    success = true;
        //}

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            LinkObj<T> lobj = (LinkObj<T>)((ASBinCode.rtData.rtObject)thisObj).value;

            StackFrame frame = stackframe;

            var arg = argements[0];
            if (arg.rtType == RunTimeDataType.rt_null)
            {
                (returnSlot).setValue(lobj.value.CompareTo(null));
            }
            else
            {
                LinkSystemObject argObj = (LinkSystemObject)((ASBinCode.rtData.rtObject)arg).value;
                (returnSlot).setValue(lobj.value.CompareTo(argObj.GetLinkData()));

            }
            success = true;
        }

    }


    class compareto_generic<T> : NativeConstParameterFunction
        where T : IComparable<T>
    {
        private string funname;
        public compareto_generic(string name):base(1)
        {
            this.funname = name;
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
                return funname;
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

        //public override NativeFunctionMode mode
        //{
        //    get
        //    {
        //        return NativeFunctionMode.normal_1;
        //    }
        //}

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void execute2(RunTimeValueBase thisObj,
        //    FunctionDefine functionDefine,
        //    SLOT[] argements,
        //    SLOT returnSlot,
        //    SourceToken token,
        //    object stackframe, out bool success)
        //{
        //    LinkObj<T> lobj = (LinkObj<T>)((ASBinCode.rtData.rtObject)thisObj).value;

        //    StackFrame frame = (StackFrame)stackframe;

        //    var arg = argements[0].getValue();
        //    if (arg.rtType == RunTimeDataType.rt_null)
        //    {
        //        ((StackSlot)returnSlot).setValue(lobj.value.CompareTo(default(T)));
        //    }
        //    else
        //    {
        //        LinkObj<T> argObj = (LinkObj<T>)((ASBinCode.rtData.rtObject)arg).value;
        //        ((StackSlot)returnSlot).setValue(lobj.value.CompareTo(argObj.value));

        //    }
        //    success = true;
        //}
        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            LinkObj<T> lobj = (LinkObj<T>)((ASBinCode.rtData.rtObject)thisObj).value;

            StackFrame frame = stackframe;

            var arg = argements[0];
            if (arg.rtType == RunTimeDataType.rt_null)
            {
                (returnSlot).setValue(lobj.value.CompareTo(default(T)));
            }
            else
            {
                LinkObj<T> argObj = (LinkObj<T>)((ASBinCode.rtData.rtObject)arg).value;
                (returnSlot).setValue(lobj.value.CompareTo(argObj.value));

            }
            success = true;
        }
    }


    #endregion

    class system_enum_valueOf : NativeConstParameterFunction
    {
        public system_enum_valueOf() : base(0)
        {
            para = new List<RunTimeDataType>();
        }

        public override string name
        {
            get { return "_system_Enum_valueOf"; }
        }

        public override bool isMethod { get { return true; } }

        public override RunTimeDataType returnType { get { return RunTimeDataType.rt_int; } }

        private List<RunTimeDataType> para;
        public override List<RunTimeDataType> parameters
        {
            get { return para; }
        }


        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            success = true;

            LinkSystemObject lo = (LinkSystemObject)((ASBinCode.rtData.rtObject)thisObj).value;

            (returnSlot).setValue((int)lo.GetLinkData());



        }
    }

}
