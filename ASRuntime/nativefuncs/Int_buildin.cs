using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;
using ASBinCode;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
    class Int_toPrecision : NativeConstParameterFunction
    {
        public Int_toPrecision():base(1)
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
                return "_int_toPrecision";
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe, out string errormessage,out int errorno )
		//{
		//    if (argements.Length < 1)
		//    {
		//        errormessage = "参数不足";
		//        errorno = 0;
		//        return ASBinCode.rtData.rtUndefined.undefined;
		//    }
		//    else
		//    {
		//        var v =  argements[0].getValue();
		//        int r= TypeConverter.ConvertToInt(v, null, null, false);

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

		//                ((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("g" + (r))

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
					//errormessage = "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
					//errorno = 1002;

					//return ASBinCode.rtData.rtUndefined.undefined;
					stackframe.throwError(token, 1002, "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.");
					returnSlot.setValue(rtUndefined.undefined);
				}
				else
				{
					//errormessage = null;
					//errorno = 0;

					//return
					//	new rtString(

					//	((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("g" + (r))

					//	);
					success = true;
					returnSlot.setValue(((rtInt)((rtObjectBase)thisObj).value.memberData[0].getValue()).value.ToString("g" + (r)));
				}



			}
		}

	}


    class Int_toExponential : NativeConstParameterFunction
    {
        public Int_toExponential():base(1)
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
                return "_int_toExponential";
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
		//        int r = TypeConverter.ConvertToInt(v, null, null, false);

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

		//                ((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r+1))

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
					//errormessage = "Number.toExponential has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
					//errorno = 1002;

					//return ASBinCode.rtData.rtUndefined.undefined;

					success = false;
					stackframe.throwError(token,1002, "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.");

					returnSlot.directSet(rtUndefined.undefined);
				}
				else
				{
					//errormessage = null;
					//errorno = 0;

					//return
					//	new rtString(

					//	((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r + 1))

					//	);
					success = true;
					returnSlot.setValue(((rtInt)((rtObjectBase)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r )));
				}



			}
		}

	}

    class Int_toFixed : NativeConstParameterFunction
    {
        public Int_toFixed():base(1)
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
                return "_int_toFixed";
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
		//        int r = TypeConverter.ConvertToInt(v, null, null, false);

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

		//                ((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("f" + (r))

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
					//errormessage = null;
					//errorno = 0;

					//return
					//	new rtString(

					//	((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("f" + (r))

					//	);

					success = true;

					returnSlot.setValue(((rtInt)((rtObjectBase)thisObj).value.memberData[0].getValue()).value.ToString("f" + (r)));

				}



			}
		}

	}


    class Int_toString : NativeConstParameterFunction
    {
        public Int_toString():base(1)
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
                return "_int_toString";
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

		//  public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//  {
		//      if (argements.Length < 1)
		//      {
		//          errormessage = "参数不足";
		//          errorno = 0;
		//          return ASBinCode.rtData.rtUndefined.undefined;
		//      }
		//      else
		//      {
		//          var v = argements[0].getValue();
		//          int r = TypeConverter.ConvertToInt(v, null, null, false);

		//          if (r < 2 || r > 36)
		//          {
		//              errormessage = "The radix argument must be between 2 and 36; got "+r+".";
		//              errorno = 1002;

		//              return ASBinCode.rtData.rtUndefined.undefined;
		//          }
		//          else
		//          {
		//              errormessage = null;
		//              errorno = 0;

		//              var toCast = ((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value;

		//              if (r == 10)
		//              {
		//                  return new rtString(toCast.ToString());
		//              }
		//              else
		//              {
		//                  string result = string.Empty;

		//                  int sign = 1;
		//                  if (toCast < 0) { toCast = -toCast;sign = -1; }

		//result = symbols[toCast % r].ToString();
		//toCast = toCast / r;

		//while ( toCast > 0)
		//                  {
		//                      result = symbols[toCast % r] + result;
		//                      toCast = toCast / r;
		//                  }
		//                  if (sign == -1)
		//                  {
		//                      result = "-" + result;
		//                  }


		//                  return new rtString(result);

		//              }

		//          }
		//      }


		//  }

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

					stackframe.throwError(token, 1002,"The radix argument must be between 2 and 36; got " + r + ".");
					returnSlot.setValue(rtUndefined.undefined);
				}
				else
				{
					success = true;

					var toCast = ((rtInt)((rtObjectBase)thisObj).value.memberData[0].getValue()).value;

					if (r == 10)
					{
						//return new rtString(toCast.ToString());
						returnSlot.setValue(toCast.ToString());
					}
					else
					{
						string result = string.Empty;

						int sign = 1;
						if (toCast < 0) { toCast = -toCast; sign = -1; }

						result = symbols[toCast % r].ToString();
						toCast = toCast / r;

						while (toCast > 0)
						{
							result = symbols[toCast % r] + result;
							toCast = toCast / r;
						}
						if (sign == -1)
						{
							result = "-" + result;
						}

						returnSlot.setValue(result);
						//return new rtString(result);

					}

				}
			}
		}


		private static readonly char[] symbols = new char[36];
        static Int_toString()
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
