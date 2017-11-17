using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
    class UInt_toPrecision : NativeConstParameterFunction
    {
        public UInt_toPrecision():base(1)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
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
                return "_uint_toPrecision";
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    if (argements.Length < 1)
		//    {
		//        errormessage = "参数不足";
		//        errorno = 0;
		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        var v = argements[0].getValue();
		//        int r = TypeConverter.ConvertToInt(v, null, null);

		//        if (r < 1 || r > 21)
		//        {
		//            errormessage = "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
		//            errorno = 1002;

		//            return ASBinCode.rtData.rtUndefined.undefined;
		//        }
		//        else
		//        {
		//            errormessage = null;
		//            errorno = 0;

		//            return
		//                new rtString(

		//                ((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("g" + (r))

		//                );
		//        }



		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;
			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v);

				if (r < 1 || r > 21)
				{
					success = false;
					
					stackframe.throwError(token, 1002, "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.");
					returnSlot.setValue(rtUndefined.undefined);
				}
				else
				{
					
					success = true;
					returnSlot.setValue(((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("g" + (r)));
				}



			}
		}

	}


    class UInt_toExponential : NativeConstParameterFunction
    {
        public UInt_toExponential():base(1)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
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
                return "_uint_toExponential";
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    if (argements.Length < 1)
		//    {
		//        errormessage = "参数不足";
		//        errorno = 0;
		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        var v = argements[0].getValue();
		//        int r = TypeConverter.ConvertToInt(v, null, null);

		//        if (r < 0 || r > 20)
		//        {
		//            errormessage = "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
		//            errorno = 1002;

		//            return ASBinCode.rtData.rtUndefined.undefined;
		//        }
		//        else
		//        {
		//            errormessage = null;
		//            errorno = 0;

		//            return
		//                new rtString(

		//                ((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r + 1))

		//                );
		//        }



		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v);

				if (r < 0 || r > 20)
				{
					success = false;
					stackframe.throwError(token, 1002, "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.");

					returnSlot.directSet(rtUndefined.undefined);
				}
				else
				{
					
					success = true;
					returnSlot.setValue(((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r)));
				}



			}
		}

	}

    class UInt_toFixed : NativeConstParameterFunction
    {
        public UInt_toFixed():base(1)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
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
                return "_uint_toFixed";
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    if (argements.Length < 1)
		//    {
		//        errormessage = "参数不足";
		//        errorno = 0;
		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        var v = argements[0].getValue();
		//        int r = TypeConverter.ConvertToInt(v, null, null);

		//        if (r < 0 || r > 20)
		//        {
		//            errormessage = "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
		//            errorno = 1002;

		//            return ASBinCode.rtData.rtUndefined.undefined;
		//        }
		//        else
		//        {
		//            errormessage = null;
		//            errorno = 0;

		//            return
		//                new rtString(

		//                ((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("f" + (r))

		//                );
		//        }



		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v);

				if (r < 0 || r > 20)
				{
					success = false;
					stackframe.throwError(token, 1002, "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.");

					returnSlot.directSet(rtUndefined.undefined);
				}
				else
				{
					

					success = true;

					returnSlot.setValue(((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("f" + (r)));

				}



			}
		}

	}


    class UInt_toString : NativeConstParameterFunction
    {
        public UInt_toString():base(1)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
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
                return "_uint_toString";
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    if (argements.Length < 1)
		//    {
		//        errormessage = "参数不足";
		//        errorno = 0;
		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        var v = argements[0].getValue();
		//        int r = TypeConverter.ConvertToInt(v, null, null);

		//        if (r < 2 || r > 36)
		//        {
		//            errormessage = "The radix argument must be between 2 and 36; got " + r + ".";
		//            errorno = 1002;

		//            return ASBinCode.rtData.rtUndefined.undefined;
		//        }
		//        else
		//        {
		//            errormessage = null;
		//            errorno = 0;

		//            var toCast = ((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value;

		//            if (r == 10)
		//            {
		//                return new rtString(toCast.ToString());
		//            }
		//            else
		//            {
		//                string result = string.Empty;

		//                //int sign = 1;
		//                //if (toCast < 0) { toCast = -toCast; sign = -1; }

		//                while (toCast > 0)
		//                {
		//                    result = symbols[toCast % r] + result;
		//                    toCast = toCast / (uint)r;
		//                }
		//                //if (sign == -1)
		//                //{
		//                //    result = "-" + result;
		//                //}


		//                return new rtString(result);

		//            }

		//        }
		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v);

				if (r < 2 || r > 36)
				{
					success = false;
					//errormessage = "The radix argument must be between 2 and 36; got " + r + ".";
					//errorno = 1002;

					//return ASBinCode.rtData.rtUndefined.undefined;

					stackframe.throwError(token, 1002, "The radix argument must be between 2 and 36; got " + r + ".");
					returnSlot.setValue(rtUndefined.undefined);
				}
				else
				{
					success = true;

					var toCast = ((rtUInt)((rtObject)thisObj).value.memberData[0].getValue()).value;

					if (r == 10)
					{
						//return new rtString(toCast.ToString());
						returnSlot.setValue(toCast.ToString());
					}
					else
					{
						string result = string.Empty;
						result = symbols[toCast % r]+result;
						toCast = toCast / (uint)r;

						while (toCast > 0)
						{
							result = symbols[toCast % r] + result;
							toCast = toCast / (uint)r;
						}
						


						returnSlot.setValue(result);
						//return new rtString(result);

					}

				}
			}
		}


		private static readonly char[] symbols = new char[36];
        static UInt_toString()
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                if (i < 10)
                {
                    symbols[i] = i.ToString()[0];
                }
                else
                {
                    symbols[i] = ((char)(i - 10 + 'a'));
                }
            }
        }


    }


}
