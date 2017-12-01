using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
    class String_length : NativeConstParameterFunction
    {
        public String_length():base(0)
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
                return "_string_length";
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
                return RunTimeDataType.rt_int;
            }
        }


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(),null,null);

		//    if (b==null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        return new ASBinCode.rtData.rtInt(b.Length);
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			

			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				//errormessage = "Cannot access a property or method of a null object reference.";
				//errorno = 1009;

				//return ASBinCode.rtData.rtUndefined.undefined;
				success = false;
				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				//return new ASBinCode.rtData.rtInt(b.Length);
				returnSlot.setValue(b.Length);
			}
		}

	}

    class String_charAt : NativeConstParameterFunction
    {
        public String_charAt():base(1)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_charat";
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


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        int idx = TypeConverter.ConvertToInt(argements[0].getValue(),null,null);
		//        if (idx < 0 || idx >= b.Length)
		//        {
		//            return new rtString(string.Empty);
		//        }
		//        else
		//        {
		//            return new rtString(b[idx].ToString());
		//        }

		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			

			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				//errormessage = "Cannot access a property or method of a null object reference.";
				//errorno = 1009;

				//return ASBinCode.rtData.rtUndefined.undefined;
				success = false;
				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");

				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				int idx = TypeConverter.ConvertToInt(argements[0]);
				if (idx < 0 || idx >= b.Length)
				{
					//return new rtString(string.Empty);
					returnSlot.setValue(string.Empty);
				}
				else
				{
					//return new rtString(b[idx].ToString());
					returnSlot.setValue(b[idx].ToString());
				}

			}
		}

	}


    class String_charCodeAt : NativeConstParameterFunction
    {
        public String_charCodeAt():base(1)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_charcodeat";
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
                return RunTimeDataType.rt_number;
            }
        }


        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
            

        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			

			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				//errormessage = "Cannot access a property or method of a null object reference.";
				//errorno = 1009;

				success = false;
				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");

				returnSlot.setValue( ASBinCode.rtData.rtUndefined.undefined);
			}
			else
			{
				success = true;
				int idx = TypeConverter.ConvertToInt(argements[0]);
				if (idx < 0 || idx >= b.Length)
				{
					returnSlot.setValue(double.NaN);
					//return new rtNumber(double.NaN);
				}
				else
				{
					returnSlot.setValue((double)b[idx]);
					//return new rtNumber(b[idx]);
				}

			}
		}
	}

    class String_fromCharCode : NativeFunctionBase
    {
        public String_fromCharCode()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_array);
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
                return "_string_fromCharCode";
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

            StringBuilder sb = new StringBuilder();

            rtArray arr = (rtArray)argements[0].getValue();
            for (int i = 0; i < arr.innerArray.Count; i++)
            {
                var v = arr.innerArray[i];
                if (v.rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(((rtObjectBase)v).value._class, out ot))
                    {
                        v = TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)v);
                    }

                }

                int charcode = TypeConverter.ConvertToInt(v);
                sb.Append((char)charcode);
            }


            return new rtString(sb.ToString());

        }

    }



    class String_indexOf : NativeConstParameterFunction
    {
        public String_indexOf():base(2)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_string);
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_indexof";
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
                return RunTimeDataType.rt_int;
            }
        }


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        string search = TypeConverter.ConvertToString(argements[0].getValue(), null, null);
		//        int st = TypeConverter.ConvertToInt(argements[1].getValue(), null, null);

		//        return new rtInt(  b.IndexOf(search,st));

		//    }

		//}
		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			

			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				//errormessage = "Cannot access a property or method of a null object reference.";
				//errorno = 1009;

				//return ASBinCode.rtData.rtUndefined.undefined;

				success = false;

				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");

				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				string search = TypeConverter.ConvertToString(argements[0], null, null);
				int st = TypeConverter.ConvertToInt(argements[1]);

				//return new rtInt(b.IndexOf(search, st));
				success = true;

				returnSlot.setValue(b.IndexOf(search, st));

			}
		}
	}

    class String_lastindexOf : NativeFunctionBase
    {
        public String_lastindexOf()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_string);
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_lastindexof";
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
                return RunTimeDataType.rt_int;
            }
        }


        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            string b = TypeConverter.ConvertToString(
                ((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

            if (b == null)
            {
                errormessage = "Cannot access a property or method of a null object reference.";
                errorno = 1009;

                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                string search = TypeConverter.ConvertToString(argements[0].getValue(), null, null);
                double  sst = TypeConverter.ConvertToNumber(argements[1].getValue());

                int st;
                if (double.IsNaN(sst))
                {
                    st = 0x7FFFFFFF;
                }
                else
                {
                    st = (int)sst;

					if (st <= 0x7FFFFFFF-search.Length)
					{
						st = st + search.Length;
					}
                }

                //****坑****
                //var text = 'Mississippi';
                //var p = text.lastIndexOf('ss', 5);
                //p=5  !!!

                

                if (st < 0) st = 0;
                if (st > b.Length) { st = b.Length; }

                return new rtInt(b.LastIndexOf(search, st));

            }

        }

    }


    class String_slice : NativeConstParameterFunction
    {
        public String_slice():base(2)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_slice";
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


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {

		//        int st = TypeConverter.ConvertToInt(argements[0].getValue(), null, null);
		//        int ed = TypeConverter.ConvertToInt(argements[1].getValue(), null, null);

		//        if (st > b.Length)
		//        {
		//            return new rtString(string.Empty);
		//        }
		//        else if (st < 0)
		//        {
		//            st = b.Length + st;
		//            if (st < 0)
		//            {
		//                st = 0;
		//            }
		//        }

		//        if (ed < 0)
		//        {
		//            ed =b.Length + ed;
		//            if (ed < 0)
		//            {
		//                ed = 0;
		//            }
		//        }


		//        int len = ed - st;
		//        if (len == 0)
		//        {
		//            return new rtString(string.Empty);
		//        }
		//        if (len > b.Length - st)
		//        {
		//            return new rtString(b.Substring(st));
		//        }
		//        else
		//        {
		//            return new rtString(b.Substring(st,len));
		//        }
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			
			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				success = false;
				//errormessage = "Cannot access a property or method of a null object reference.";
				//errorno = 1009;

				//return ASBinCode.rtData.rtUndefined.undefined;
				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				int st = TypeConverter.ConvertToInt(argements[0]);
				int ed = TypeConverter.ConvertToInt(argements[1]);

				if (st > b.Length)
				{
					//return new rtString(string.Empty);
					returnSlot.setValue(string.Empty);
				}
				else if (st < 0)
				{
					st = b.Length + st;
					if (st < 0)
					{
						st = 0;
					}
				}

				if (ed < 0)
				{
					ed = b.Length + ed;
					if (ed < 0)
					{
						ed = 0;
					}
				}


				int len = ed - st;
				if (len == 0)
				{
					//return new rtString(string.Empty);
					returnSlot.setValue(string.Empty);
				}
				if (len > b.Length - st)
				{
					//return new rtString(b.Substring(st));
					returnSlot.setValue(b.Substring(st));
				}
				else
				{
					//return new rtString(b.Substring(st, len));
					returnSlot.setValue(b.Substring(st, len));
				}
			}
		}

	}

    class String_split : NativeConstParameterFunction
    {
        public String_split():base(2)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_string);
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_split";
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
                return RunTimeDataType.rt_array;
            }
        }


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        string delimiter = TypeConverter.ConvertToString(argements[0].getValue(), null, null);
		//        int maxcount = TypeConverter.ConvertToInt(argements[1].getValue(), null, null);

		//        if (maxcount < 0) { return new rtArray(); }


		//        var split = b.Split(new string[] { delimiter }, StringSplitOptions.None );
		//        rtArray result = new rtArray();
		//        for (int i = 0; i < split.Length && i<maxcount; i++)
		//        {
		//            result.innerArray.Add(new rtString(split[i]));
		//        }

		//        return result;
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			
			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{

				success = false;
				
				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				string delimiter = TypeConverter.ConvertToString(argements[0],stackframe,token);
				int maxcount = TypeConverter.ConvertToInt(argements[1]);

				if (maxcount < 0) //{ return new rtArray(); }
				{
					returnSlot.directSet(new rtArray());
				}

				var split = b.Split(new string[] { delimiter }, StringSplitOptions.None);
				rtArray result = new rtArray();
				for (int i = 0; i < split.Length && i < maxcount; i++)
				{
					result.innerArray.Add(new rtString(split[i]));
				}

				//return result;
				returnSlot.directSet(result);

			}
		}

	}



    class String_substr : NativeConstParameterFunction
    {
        public String_substr():base(2)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_substr";
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


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {

		//        int st = TypeConverter.ConvertToInt(argements[0].getValue(), null, null);
		//        int len = TypeConverter.ConvertToInt(argements[1].getValue(), null, null);

		//        if (st > b.Length)
		//        {
		//            return new rtString(string.Empty);
		//        }
		//        else if (st < 0)
		//        {
		//            st = b.Length + st;
		//            if (st < 0)
		//            {
		//                st = 0;
		//            }
		//        }

		//        if (len < 0)
		//        {
		//            len = 0;
		//        }


		//        if (len > b.Length - st)
		//        {
		//            return new rtString(b.Substring(st));
		//        }
		//        else
		//        {
		//            return new rtString(b.Substring(st, len));
		//        }
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			

			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				success = false;

				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				int st = TypeConverter.ConvertToInt(argements[0]);
				int len = TypeConverter.ConvertToInt(argements[1]);

				if (st > b.Length)
				{
					//return new rtString(string.Empty);
					returnSlot.setValue(string.Empty);
				}
				else if (st < 0)
				{
					st = b.Length + st;
					if (st < 0)
					{
						st = 0;
					}
				}

				if (len < 0)
				{
					len = 0;
				}


				if (len > b.Length - st)
				{
					//return new rtString(b.Substring(st));
					returnSlot.setValue(b.Substring(st));
				}
				else
				{
					//return new rtString(b.Substring(st, len));
					returnSlot.setValue(b.Substring(st, len));
				}
			}
		}


	}

    class String_substring : NativeConstParameterFunction
    {
        public String_substring():base(2)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_string_substring";
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


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {

		//        int st = TypeConverter.ConvertToInt(argements[0].getValue(), null, null);
		//        int ed = TypeConverter.ConvertToInt(argements[1].getValue(), null, null);

		//        if (st > ed)
		//        {
		//            int temp = st;
		//            st = ed;
		//            ed = temp;
		//        }


		//        if (st > b.Length)
		//        {
		//            return new rtString(string.Empty);
		//        }
		//        else if (st < 0)
		//        {
		//            st = 0;
		//        }

		//        int len = ed - st;

		//        if (len > b.Length - st)
		//        {
		//            return new rtString(b.Substring(st));
		//        }
		//        else
		//        {
		//            return new rtString(b.Substring(st, len));
		//        }
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			
			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				success = false;

				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				int st = TypeConverter.ConvertToInt(argements[0]);
				int ed = TypeConverter.ConvertToInt(argements[1]);

				if (st > ed)
				{
					int temp = st;
					st = ed;
					ed = temp;
				}


				if (st > b.Length)
				{
					//return new rtString(string.Empty);
					returnSlot.setValue(string.Empty);
				}
				else if (st < 0)
				{
					st = 0;
				}

				int len = ed - st;

				if (len > b.Length - st)
				{
					//return new rtString(b.Substring(st));
					returnSlot.setValue(b.Substring(st));
				}
				else
				{
					//return new rtString(b.Substring(st, len));
					returnSlot.setValue(b.Substring(st, len));
				}
			}
		}

	}



    class String_tolower : NativeConstParameterFunction
    {
        public String_tolower():base(0)
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
                return "_string_tolower";
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


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        return new ASBinCode.rtData.rtString(b.ToLower());
		//    }

		//}
		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			
			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				success = false;

				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				//return new ASBinCode.rtData.rtString(b.ToLower());
				returnSlot.setValue(b.ToLower());
			}
		}
	}

    class String_toupper : NativeConstParameterFunction
    {
        public String_toupper():base(0)
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
                return "_string_toupper";
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


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        return new ASBinCode.rtData.rtString(b.ToUpper());
		//    }

		//}
		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				success = false;

				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				//return new ASBinCode.rtData.rtString(b.ToLower());
				returnSlot.setValue(b.ToUpper());
			}
		}
	}



    class String_replace : NativeConstParameterFunction
    {
        public String_replace():base(2)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_string);
            _paras.Add(RunTimeDataType.rt_string);
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
                return "_string_replace";
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


		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string b = TypeConverter.ConvertToString(
		//        ((ASBinCode.rtData.rtObject)thisObj).value.memberData[0].getValue(), null, null);

		//    if (b == null)
		//    {
		//        errormessage = "Cannot access a property or method of a null object reference.";
		//        errorno = 1009;

		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {

		//        string pattern = TypeConverter.ConvertToString(argements[0].getValue(), null, null);
		//        string repl = TypeConverter.ConvertToString(argements[1].getValue(), null, null);

		//        if (pattern == null) pattern = string.Empty;
		//        if (repl == null) repl = string.Empty;


		//        return new rtString(b.Replace(pattern, repl));


		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			

			string b = TypeConverter.ConvertToString(
				((ASBinCode.rtData.rtObjectBase)thisObj).value.memberData[0].getValue(), null, null);

			if (b == null)
			{
				success = false;

				stackframe.throwError(token, 1009, "Cannot access a property or method of a null object reference.");
				returnSlot.directSet(rtUndefined.undefined);
			}
			else
			{
				success = true;
				string pattern = TypeConverter.ConvertToString(argements[0], null, null);
				string repl = TypeConverter.ConvertToString(argements[1], null, null);

				if (pattern == null) pattern = string.Empty;
				if (repl == null) repl = string.Empty;


				//return new rtString(b.Replace(pattern, repl));
				returnSlot.setValue(b.Replace(pattern, repl));

			}
		}

	}


}
