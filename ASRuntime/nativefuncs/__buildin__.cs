using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class __buildin__ismethod : NativeFunctionBase
    {
        List<RunTimeDataType> para;
        public __buildin__ismethod()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_function);
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
                return "__buildin__ismethod";
            }
        }

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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            if (argements[0].getValue().rtType == RunTimeDataType.rt_null)
            {
                return ASBinCode.rtData.rtBoolean.False;
            }
            else
            {
                ASBinCode.rtData.rtFunction f = (ASBinCode.rtData.rtFunction)argements[0].getValue();
                if (f.ismethod)
                {
                    return ASBinCode.rtData.rtBoolean.True;
                }
                else
                {
                    return ASBinCode.rtData.rtBoolean.False;
                }
            }
        }
    }

    class __buildin__trace : NativeFunctionBase
    {
        List<RunTimeDataType> para;
        public __buildin__trace()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_array);
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
                return "__buildin__trace";
            }
        }

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
            throw new ASRunTimeException();
        }

        public override NativeFunctionMode mode
        {
            get
            {
                return  NativeFunctionMode.async_0;
            }
        }


        public override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements, SLOT resultSlot, object callbacker, object stackframe, SourceToken token, RunTimeScope scope)
        {
            IBlockCallBack cb = (IBlockCallBack)callbacker;
            StackFrame frame = (StackFrame)stackframe;

            if (argements[0].getValue().rtType == RunTimeDataType.rt_null)
            {
                Console.WriteLine();
                cb.call(cb.args);
                return;
            }

            rtArray array = (rtArray)(argements[0].getValue());

            if (array.innerArray.Count == 0)
            {
                Console.WriteLine();
                cb.call(cb.args);
                return;
            }


            BlockCallBackBase sepcb = new BlockCallBackBase();
            sepcb.scope = scope;
            sepcb._intArg = 0;

            object[] sendargs = new object[9];
            sendargs[0] = cb;
            sendargs[1] = array;
            sendargs[2] = frame;
            sendargs[3] = token;
            sendargs[4] = scope;
            sendargs[5] = " ";
            sendargs[6] = new StringBuilder();
            sendargs[7] = resultSlot;
            sendargs[8] = new rtInt(0);
            sepcb.args = sendargs;

            _SeptoString_CB(sepcb, sendargs);
        }

        private static rtString UNDEFINED = new rtString("undefined");

        private void _SeptoString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            
            rtArray array = (rtArray)receiveArgs[1];

            BlockCallBackBase valueCB = new BlockCallBackBase();
            valueCB._intArg = sender._intArg + 1;
            valueCB.args = receiveArgs;
            valueCB.setCallBacker(_ValueToString_CB);

            var v = array.innerArray[sender._intArg];
            if (v.rtType == RunTimeDataType.rt_void)
            {
                v = UNDEFINED;
            }

            operators.OpCast.CastValue(v, RunTimeDataType.rt_string,
                frame,
                (SourceToken)receiveArgs[3],
                (RunTimeScope)receiveArgs[4],
                frame._tempSlot1,
                valueCB, false
                );

        }

        private void _ValueToString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            rtArray array = (rtArray)receiveArgs[1];

            ((rtInt)receiveArgs[8]).value++;

            if (((rtInt)receiveArgs[8]).value == 10)    //堆栈清理,防止溢出...
            {
                ((rtInt)receiveArgs[8]).value = 0;
                BlockCallBackBase valueCB = new BlockCallBackBase();
                valueCB._intArg = sender._intArg;
                valueCB.args = receiveArgs;
                valueCB.setCallBacker(_ValueToString_CB);

                frame.player.CallBlankBlock(valueCB);
                return;
            }


            StringBuilder sb = (StringBuilder)receiveArgs[6];
            SourceToken token = (SourceToken)receiveArgs[3];

            string aSep = (string)receiveArgs[5];
            sb.Append(

                TypeConverter.ConvertToString(frame._tempSlot1.getValue(), frame, token)==null?"null":
                TypeConverter.ConvertToString(frame._tempSlot1.getValue(), frame, token)

                );
            if (sender._intArg < array.innerArray.Count)
            {
                sb.Append(aSep);


                BlockCallBackBase valueCB = new BlockCallBackBase();
                valueCB._intArg = sender._intArg + 1;
                valueCB.args = receiveArgs;
                valueCB.setCallBacker(_ValueToString_CB);

                operators.OpCast.CastValue(array.innerArray[sender._intArg], RunTimeDataType.rt_string,
                    frame,
                    (SourceToken)receiveArgs[3],
                    (RunTimeScope)receiveArgs[4],
                    frame._tempSlot1,
                    valueCB, false
                    );

            }
            else
            {
                //ISLOT result = (ISLOT)receiveArgs[7];

                //result.directSet(new rtString(sb.ToString()));
                Console.WriteLine(sb.ToString());

                IBlockCallBack cb = (IBlockCallBack)receiveArgs[0];
                cb.call(cb.args);
            }

        }
    }



    class __buildin__isnan : NativeFunctionBase
    {
        List<RunTimeDataType> para;
        public __buildin__isnan()
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
                return "__buildin__isnan";
            }
        }

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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            double num = TypeConverter.ConvertToNumber(argements[0].getValue());

            if (double.IsNaN(num))
            {
                return rtBoolean.True;
            }
            else
            {
                return rtBoolean.False;
            }

        }
    }


    class __buildin__isfinite : NativeFunctionBase
    {
        List<RunTimeDataType> para;
        public __buildin__isfinite()
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
                return "__buildin__isfinite";
            }
        }

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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            double num = TypeConverter.ConvertToNumber(argements[0].getValue());

            if (double.IsInfinity(num))
            {
                return rtBoolean.True;
            }
            else
            {
                return rtBoolean.False;
            }

        }
    }

    class __buildin__parseint : NativeFunctionBase
    {
        List<RunTimeDataType> para;
        public __buildin__parseint()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_string);
            para.Add(RunTimeDataType.rt_uint);
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
                return "__buildin__parseint";
            }
        }

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
            errormessage = null;
            errorno = 0;

            string str= TypeConverter.ConvertToString(argements[0].getValue(),null,null);
            uint radix = TypeConverter.ConvertToUInt(argements[1].getValue(), null, null);

            if (String.IsNullOrEmpty(str))
            {
                return new rtNumber(double.NaN);
            }
            //ASCII 48-57 : 0-9 ,65-90 : A-Z;

            str = str.ToUpper();
            str=str.TrimStart();
            str=str.TrimStart('0');

            if (radix == 0) { radix = 10; }
            if (radix < 2 || radix > 36) { return new rtNumber(double.NaN); }

            uint allowidx = 48 + radix;

            if (radix > 10)
            {
                allowidx = 65 + radix - 10;
            }

            double output = double.NaN;

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c < allowidx && ((c < 58 && c >= 48) || c>= 65))
                {
                    if (double.IsNaN(output))
                    {
                        output = c < 58 ? (c - 48) : (c - 65 + 10);
                    }
                    else
                    {
                        output = output * radix + (c < 58 ? (c - 48) : (c - 65 + 10));
                    }
                }
                else
                {
                    break;
                }
            }


            return new rtNumber(output);

        }
    }

    class __buildin__parsefloat : NativeFunctionBase
    {
        List<RunTimeDataType> para;
        public __buildin__parsefloat()
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
                return "__buildin__parsefloat";
            }
        }

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
            errormessage = null;
            errorno = 0;

            string str = TypeConverter.ConvertToString(argements[0].getValue(), null, null);
            
            if (String.IsNullOrEmpty(str))
            {
                return new rtNumber(double.NaN);
            }

            str=str.Trim();

            bool hasreaddot=false;

            string newstr = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if (c == '.')
                {
                    if (hasreaddot)
                    {
                        break;
                    }
                    hasreaddot = true;
                }
                else if (c == '-' || c == '+')
                {
                    if (!string.IsNullOrEmpty(newstr))
                    {
                        break;
                    }
                    if (i + 1 >= str.Length)
                    {
                        return new rtNumber(double.NaN);
                    }
                    char n = str[i + 1];

                    if (n == '.')
                    {
                        if (i + 2 >= str.Length)
                        {
                            return new rtNumber(double.NaN);
                        }

                        n = str[i + 2];
                        if (n < 48 || n > 57)
                        {
                            return new rtNumber(double.NaN);
                        }
                    }
                    else if (n < 48 || n > 57)
                    {
                        return new rtNumber(double.NaN);
                    }


                }
                else if (c == 'e' || c == 'E')
                {
                    if (string.IsNullOrEmpty(newstr))
                    {
                        return new rtNumber(double.NaN);
                    }
                    else
                    {
                        if (i + 1 >= str.Length)
                        {
                            break;
                        }

                        string epart = "e";
                        int st = i + 1;
                        char ep = str[st];
                        if (ep == '+' || ep == '-')
                        {
                            epart += ep;
                            st++;

                            if (!(st < str.Length))
                            {
                                break;
                            }

                            ep = str[st];
                            if (ep < 48 || ep > 57)
                            {
                                break;
                            }
                            epart += ep;
                            st++;
                        }


                        for (int j = st; j < str.Length; j++)
                        {
                            char n = str[j];
                            if (n < 48 || n > 57)
                            {
                                break;
                            }
                            epart += n;
                        }
                        newstr += epart;

                        break;
                    }

                }
                else if (c < 48 || c > 57)
                {
                    if (string.IsNullOrEmpty(newstr))
                    {
                        return new rtNumber(double.NaN);
                    }
                    else
                    {
                        break;
                    }
                }
                newstr = newstr + c;

            }

            return new rtNumber(double.Parse(newstr));

        }
    }


}
