using ASBinCode;
using ASBinCode.rtData;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Number_toPrecision : NativeConstParameterFunction
    {
        public Number_toPrecision():base(1)
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
                return "_number_toPrecision";
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

		//                ((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("g" + (r))

		//                );
		//        }



		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;
			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v, null, null);

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
					returnSlot.setValue(((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("g" + (r)));
				}



			}
		}


	}


	class Number_toExponential : NativeConstParameterFunction
    {
        public Number_toExponential():base(1)
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
                return "_number_toExponential";
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

		//                ((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r + 1))

		//                );
		//        }



		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v, null, null);

				if (r < 0 || r > 20)
				{
					//errormessage = "Number.toExponential has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
					//errorno = 1002;

					//return ASBinCode.rtData.rtUndefined.undefined;

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

					//	((rtInt)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r + 1))

					//	);
					success = true;
					returnSlot.setValue(((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("e" + (r)));
				}



			}
		}


	}

	class Number_toFixed : NativeConstParameterFunction
    {
        public Number_toFixed():base(1)
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
                return "_number_toFixed";
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

		//                ((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("f" + (r))

		//                );
		//        }



		//    }


		//}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v, null, null);

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

					returnSlot.setValue(((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value.ToString("f" + (r)));

				}



			}
		}
	}


    class Number_toString : NativeConstParameterFunction
    {
        public Number_toString():base(1)
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
                return "_number_toString";
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

		//            var toCastV = ((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value;



		//            if (r == 10)
		//            {
		//                return new rtString(toCastV.ToString());
		//            }
		//            else
		//            {
		//                string result = string.Empty;

		//                int toCast = (int)toCastV;

		//                int sign = 1;
		//                if (toCast < 0) { toCast = -toCast; sign = -1; }

		//                while (toCast > 0)
		//                {
		//                    result = symbols[toCast % r] + result;
		//                    toCast = toCast / r;
		//                }
		//                if (sign == -1)
		//                {
		//                    result = "-" + result;
		//                }


		//                return new rtString(result);

		//            }

		//        }
		//    }


		//}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			{
				var v = argements[0];
				int r = TypeConverter.ConvertToInt(v, null, null);

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

					var toCastV = ((rtNumber)((rtObject)thisObj).value.memberData[0].getValue()).value;

					if (r == 10)
					{
						//return new rtString(toCast.ToString());
						returnSlot.setValue(toCastV.ToString());
					}
					else
					{
						string result = string.Empty;

						int toCast = (int)toCastV;

						int sign = 1;
						if (toCast < 0) { toCast = -toCast; sign = -1; }

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
        static Number_toString()
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

    class Number_pow : NativeFunctionBase
    {
        public Number_pow()
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
                return "_number_pow";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 2)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                double v1 = TypeConverter.ConvertToNumber(argements[0].getValue());
                double v2 = TypeConverter.ConvertToNumber(argements[1].getValue());

                errormessage = null;
                errorno = 0;

                return new rtNumber( Math.Pow(v1,v2 ));

            }


        }
    }


    class Number_random : NativeFunctionBase
    {
        public Number_random()
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
                return "_number_random";
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


        private static Random rnd=new Random();
        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length >0)
            {
                errormessage = "参数错误";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                errormessage = null;
                errorno = 0;

                return new rtNumber(rnd.NextDouble());

            }


        }
    }


    class Number_round : NativeFunctionBase
    {
        public Number_round()
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
            return "_number_round";
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

    public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
    {
        if (argements.Length < 1)
        {
            errormessage = "参数不足";
            errorno = 0;
            return ASBinCode.rtData.rtUndefined.undefined;
        }
        else
        {
            var v = argements[0].getValue();
            double r = TypeConverter.ConvertToNumber(v);

            
            {
                errormessage = null;
                errorno = 0;

                return
                    new rtNumber(
                        Math.Round(r)
                    );
            }



        }


    }
}


    class Number_sin : NativeFunctionBase
    {
        public Number_sin()
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
                return "_number_sin";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Sin(r)
                        );
                }



            }


        }
    }

    class Number_sqrt : NativeFunctionBase
    {
        public Number_sqrt()
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
                return "_number_sqrt";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Sqrt(r)
                        );
                }



            }


        }
    }


    class Number_tan : NativeFunctionBase
    {
        public Number_tan()
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
                return "_number_tan";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Tan(r)
                        );
                }



            }


        }
    }

    class Number_abs : NativeFunctionBase
    {
        public Number_abs()
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
                return "_number_abs";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Abs(r)
                        );
                }



            }


        }
    }

    class Number_acos : NativeFunctionBase
    {
        public Number_acos()
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
                return "_number_acos";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Acos(r)
                        );
                }



            }


        }
    }

    class Number_asin : NativeFunctionBase
    {
        public Number_asin()
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
                return "_number_asin";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Asin(r)
                        );
                }



            }


        }
    }

    class Number_atan : NativeFunctionBase
    {
        public Number_atan()
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
                return "_number_atan";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Atan(r)
                        );
                }



            }


        }
    }

    class Number_atan2 : NativeFunctionBase
    {
        public Number_atan2()
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
                return "_number_atan2";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 2)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                double v1 = TypeConverter.ConvertToNumber(argements[0].getValue());
                double v2 = TypeConverter.ConvertToNumber(argements[1].getValue());

                errormessage = null;
                errorno = 0;

                return new rtNumber(Math.Atan2(v1, v2));

            }


        }
    }

    class Number_ceil : NativeFunctionBase
    {
        public Number_ceil()
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
                return "_number_ceil";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Ceiling(r)
                        );
                }



            }


        }
    }

    class Number_cos : NativeFunctionBase
    {
        public Number_cos()
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
                return "_number_cos";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Cos(r)
                        );
                }



            }


        }
    }

    class Number_exp : NativeFunctionBase
    {
        public Number_exp()
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
                return "_number_exp";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Exp(r)
                        );
                }



            }


        }
    }

    class Number_floor : NativeFunctionBase
    {
        public Number_floor()
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
                return "_number_floor";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Floor(r)
                        );
                }



            }


        }
    }

    class Number_log : NativeFunctionBase
    {
        public Number_log()
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
                return "_number_log";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            if (argements.Length < 1)
            {
                errormessage = "参数不足";
                errorno = 0;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            {
                var v = argements[0].getValue();
                double r = TypeConverter.ConvertToNumber(v);


                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtNumber(
                            Math.Log(r)
                        );
                }



            }


        }
    }


}
