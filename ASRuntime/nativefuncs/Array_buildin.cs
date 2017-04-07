using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASRuntime.nativefuncs
{
    class Array_constructor : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_constructor()
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
                return "_array_constructor";
            }
        }

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
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var newvalue = new ASBinCode.rtData.rtArray();
            newvalue.objHandle.bindArrayObject = thisObj;

            thisObj.value.memberData[0].directSet(newvalue);
            

            var args = argements[0].getValue();

            if(args.rtType == RunTimeDataType.rt_array)
            {
                rtArray arr = (rtArray)args;

                if (arr.innerArray.Count == 1)
                {
                    var a1 = arr.innerArray[0];
                    if (TypeConverter.ObjectImplicit_ToNumber(a1.rtType, bin))
                    {
                        a1 = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)a1);
                    }

                    if (a1.rtType == RunTimeDataType.rt_int || a1.rtType == RunTimeDataType.rt_uint || a1.rtType == RunTimeDataType.rt_number)
                    {
                        int c = TypeConverter.ConvertToInt(a1, null, null);

                        if (c < 0)
                        {
                            errormessage = "Array index is not a positive integer (" + c + ").";
                            errorno = 1005;
                            return ASBinCode.rtData.rtUndefined.undefined;
                        }
                        else
                        {
                            while (newvalue.innerArray.Count < c)
                            {
                                newvalue.innerArray.Add(rtUndefined.undefined);
                            }
                        }

                    }
                    else
                    {
                        newvalue.innerArray.Add(a1);
                    }
                }
                else
                {
                    newvalue.innerArray.AddRange(arr.innerArray);
                }

            }
            
            //throw new NotImplementedException();

            return ASBinCode.rtData.rtUndefined.undefined;
        }
    }


    class Array_fill : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_fill()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
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
                return "_array_fill";
            }
        }

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
                return RunTimeDataType.fun_void;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            ((rtObject)argements[0].getValue()).value.memberData[0].directSet(argements[1].getValue());
            ((rtArray)argements[1].getValue()).objHandle.bindArrayObject = ((rtObject)argements[0].getValue());

            errormessage = null;
            errorno = 0;
            return rtUndefined.undefined;

            //throw new NotImplementedException();
        }
    }


    class Array_load : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_load()
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
                return "_array_load";
            }
        }

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
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            if (((rtArray)argements[0].getValue()).objHandle.bindArrayObject == null)
            {
                return rtNull.nullptr;
            }
            else
            {
                return ((rtArray)argements[0].getValue()).objHandle.bindArrayObject;
            }
        }
    }


    class Array_getLength : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_getLength()
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
                return "_array_getlength";
            }
        }

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
                return RunTimeDataType.rt_uint;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            return 
                new rtUInt(
                    (uint)
                    ((rtArray)(thisObj.value.memberData[0].getValue())).innerArray.Count
                )
                ;
        }
    }



    class Array_setLength : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_setLength()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_uint);
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
                return "_array_setlength";
            }
        }

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
                return RunTimeDataType.fun_void;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var list = ((rtArray)(thisObj.value.memberData[0].getValue())).innerArray;

            uint newlen = ((rtUInt)argements[0].getValue()).value;

            if (newlen > list.Count)
            {
                while (list.Count < newlen)
                {
                    list.Add(rtUndefined.undefined);
                }
            }
            else if (newlen < list.Count)
            {
                list.RemoveRange((int)newlen, list.Count - (int)newlen);
            }


            return rtUndefined.undefined;
                
        }
    }

    class Array_insertAt : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_insertAt()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
            _paras.Add(RunTimeDataType.rt_void);
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
                return "_array_insertAt";
            }
        }

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
                return RunTimeDataType.fun_void;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)thisObj.value.memberData[0].getValue();

            int idx = ((rtInt)argements[0].getValue()).value;
            var toinsert = argements[1].getValue();
            if (idx < 0)
            {
                idx = arr.innerArray.Count + idx;
                if (idx < 0) { idx = 0; }
            }
            else if (idx > arr.innerArray.Count)
            {
                idx = arr.innerArray.Count;
            }

            arr.innerArray.Insert(idx, toinsert);


            return rtUndefined.undefined;

        }
    }



    class Array_join : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_join()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
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
                return "_array_join";
            }
        }

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

        public override bool isAsync
        {
            get
            {
                return true;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            throw new InvalidOperationException();
        }

        public override void executeAsync(rtObject thisObj, ISLOT[] argements,
            ISLOT resultSlot, object callbacker, object stackframe, SourceToken token, IRunTimeScope scope)
        {
            IBlockCallBack cb = (IBlockCallBack)callbacker;
            StackFrame frame = (StackFrame)stackframe;

            rtArray array = (rtArray)thisObj.value.memberData[0].getValue();

            if (array.innerArray.Count == 0)
            {
                cb.call(cb.args);
                resultSlot.directSet(new rtString(string.Empty));
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
            sendargs[5] = argements[0].getValue();
            sendargs[6] = new StringBuilder();
            sendargs[7] = resultSlot;

            sepcb.args = sendargs;
            sepcb.setCallBacker(_SeptoString_CB);

            operators.OpCast.CastValue(argements[0].getValue(), RunTimeDataType.rt_string,
                frame, token, scope, frame._tempSlot1, sepcb, false);


        }

        private void _SeptoString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            IRunTimeValue aSep = (IRunTimeValue)receiveArgs[5];
            if (aSep.rtType != RunTimeDataType.rt_null)
            {
                string sep = TypeConverter.ConvertToString( frame._tempSlot1.getValue(),frame,(SourceToken)receiveArgs[3]);
                receiveArgs[5] = sep;
            }
            else
            {
                receiveArgs[5] = ",";
            }

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
            sb.Append(TypeConverter.ConvertToString(frame._tempSlot1.getValue(), frame, token));
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
                ISLOT result = (ISLOT)receiveArgs[7];

                result.directSet(new rtString( sb.ToString() ));

                IBlockCallBack cb = (IBlockCallBack)receiveArgs[0];
                cb.call(cb.args);
            }

        }
    }


    class Array_pop : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_pop()
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
                return "_array_pop";
            }
        }

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
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)thisObj.value.memberData[0].getValue();

            if (arr.innerArray.Count > 0)
            {
                var result = arr.innerArray[arr.innerArray.Count - 1];
                arr.innerArray.RemoveAt(arr.innerArray.Count-1);
                return result;
            }
            else
            {
                return rtUndefined.undefined;
            }

            
        }
    }

    class Array_push : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_push()
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
                return "_array_push";
            }
        }

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
                return RunTimeDataType.rt_uint;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)thisObj.value.memberData[0].getValue();
            var args = argements[0].getValue();

            if (args.rtType == RunTimeDataType.rt_array)
            {
                var arglist = ((rtArray)args).innerArray;
                arr.innerArray.AddRange(arglist);
            }
            
            return new rtUInt((uint)arr.innerArray.Count);
            

        }
    }

    class Array_removeAt : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_removeAt()
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
                return "_array_removeat";
            }
        }

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
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(rtObject thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)thisObj.value.memberData[0].getValue();

            int idx = ((rtInt)argements[0].getValue()).value;
            if (idx < 0)
            {
                idx = arr.innerArray.Count + idx;
                if (idx < 0) { idx = 0; }
            }
            else if (idx > arr.innerArray.Count-1)
            {
                return rtUndefined.undefined;
            }

            var r = arr.innerArray[idx];
            arr.innerArray.RemoveAt(idx);
            return r;

        }
    }



}
