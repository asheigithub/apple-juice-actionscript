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

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
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

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override bool isAsync
        {
            get
            {
                return true;
            }
        }


        public override void executeAsync(IRunTimeValue thisObj, ISLOT[] argements, ISLOT resultSlot, object callbacker, object stackframe, SourceToken token, IRunTimeScope scope)
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

            object[] sendargs = new object[8];
            sendargs[0] = cb;
            sendargs[1] = array;
            sendargs[2] = frame;
            sendargs[3] = token;
            sendargs[4] = scope;
            sendargs[5] = " ";
            sendargs[6] = new StringBuilder();
            sendargs[7] = resultSlot;
            sepcb.args = sendargs;

            _SeptoString_CB(sepcb, sendargs);
        }

        private void _SeptoString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            
            rtArray array = (rtArray)receiveArgs[1];

            BlockCallBackBase valueCB = new BlockCallBackBase();
            valueCB._intArg = sender._intArg + 1;
            valueCB.args = receiveArgs;
            valueCB.setCallBacker(_ValueToString_CB);

            operators.OpCast.CastValue(array.innerArray[sender._intArg], RunTimeDataType.rt_string,
                frame,
                (SourceToken)receiveArgs[3],
                (IRunTimeScope)receiveArgs[4],
                frame._tempSlot1,
                valueCB, false
                );

        }

        private void _ValueToString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            rtArray array = (rtArray)receiveArgs[1];

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
                    (IRunTimeScope)receiveArgs[4],
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

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            double num = TypeConverter.ConvertToNumber(argements[0].getValue(), null, null);

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

}
