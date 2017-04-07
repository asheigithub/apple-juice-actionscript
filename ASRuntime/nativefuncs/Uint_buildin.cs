using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class UInt_toPrecision : NativeFunctionBase
    {
        public UInt_toPrecision()
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

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
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
                int r = TypeConverter.ConvertToInt(v, null, null, false);

                if (r < 1 || r > 21)
                {
                    errormessage = "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
                    errorno = 1002;

                    return ASBinCode.rtData.rtUndefined.undefined;
                }
                else
                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtString(

                        ((rtUInt)thisObj.value.memberData[0].getValue()).value.ToString("g" + (r))

                        );
                }



            }


        }
    }


    class UInt_toExponential : NativeFunctionBase
    {
        public UInt_toExponential()
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

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
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
                int r = TypeConverter.ConvertToInt(v, null, null, false);

                if (r < 0 || r > 20)
                {
                    errormessage = "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
                    errorno = 1002;

                    return ASBinCode.rtData.rtUndefined.undefined;
                }
                else
                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtString(

                        ((rtUInt)thisObj.value.memberData[0].getValue()).value.ToString("e" + (r + 1))

                        );
                }



            }


        }
    }

    class UInt_toFixed : NativeFunctionBase
    {
        public UInt_toFixed()
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

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
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
                int r = TypeConverter.ConvertToInt(v, null, null, false);

                if (r < 0 || r > 20)
                {
                    errormessage = "Number.toPrecision has a range of 1 to 21. Number.toFixed and Number.toExponential have a range of 0 to 20. Specified value is not within expected range.";
                    errorno = 1002;

                    return ASBinCode.rtData.rtUndefined.undefined;
                }
                else
                {
                    errormessage = null;
                    errorno = 0;

                    return
                        new rtString(

                        ((rtUInt)thisObj.value.memberData[0].getValue()).value.ToString("f" + (r))

                        );
                }



            }


        }
    }


    class UInt_toString : NativeFunctionBase
    {
        public UInt_toString()
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

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
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
                int r = TypeConverter.ConvertToInt(v, null, null, false);

                if (r < 2 || r > 36)
                {
                    errormessage = "The radix argument must be between 2 and 36; got " + r + ".";
                    errorno = 1002;

                    return ASBinCode.rtData.rtUndefined.undefined;
                }
                else
                {
                    errormessage = null;
                    errorno = 0;

                    var toCast = ((rtUInt)thisObj.value.memberData[0].getValue()).value;

                    if (r == 10)
                    {
                        return new rtString(toCast.ToString());
                    }
                    else
                    {
                        string result = string.Empty;

                        //int sign = 1;
                        //if (toCast < 0) { toCast = -toCast; sign = -1; }

                        while (toCast > 0)
                        {
                            result = symbols[toCast % r] + result;
                            toCast = toCast / (uint)r;
                        }
                        //if (sign == -1)
                        //{
                        //    result = "-" + result;
                        //}


                        return new rtString(result);

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
