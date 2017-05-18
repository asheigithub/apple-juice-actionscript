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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var newvalue = new ASBinCode.rtData.rtArray();
            newvalue.objHandle.bindArrayObject = (rtObject)thisObj;

            ((rtObject)thisObj).value.memberData[0].directSet(newvalue);
            

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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            return 
                new rtUInt(
                    (uint)
                    ((rtArray)(((rtObject)thisObj).value.memberData[0].getValue())).innerArray.Count
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var list = ((rtArray)(((rtObject)thisObj).value.memberData[0].getValue())).innerArray;

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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

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

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.async_0;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            throw new ASRunTimeException();
        }

        public override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements,
            SLOT resultSlot, object callbacker, object stackframe, SourceToken token, RunTimeScope scope)
        {
            IBlockCallBack cb = (IBlockCallBack)callbacker;
            StackFrame frame = (StackFrame)stackframe;

            rtArray array = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

            if (array.innerArray.Count == 0)
            {
                cb.call(cb.args);
                resultSlot.directSet(new rtString(string.Empty));
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
            sendargs[5] = argements[0].getValue();
            sendargs[6] = new StringBuilder();
            sendargs[7] = resultSlot;
            sendargs[8] = new rtInt(0);

            sepcb.args = sendargs;
            sepcb.setCallBacker(_SeptoString_CB);

            operators.OpCast.CastValue(argements[0].getValue(), RunTimeDataType.rt_string,
                frame, token, scope, frame._tempSlot1, sepcb, false);


        }

        private void _SeptoString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            RunTimeValueBase aSep = (RunTimeValueBase)receiveArgs[5];
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
                valueCB.args = sender.args;
                valueCB.setCallBacker(_ValueToString_CB);

                frame.player.CallBlankBlock(valueCB);
                return;
            }


            StringBuilder sb = (StringBuilder)receiveArgs[6];
            SourceToken token = (SourceToken)receiveArgs[3];

            string aSep = (string)receiveArgs[5];
            string toappend = TypeConverter.ConvertToString(frame._tempSlot1.getValue(), frame, token);

            while (true)
            {
                sb.Append(toappend == null ? "null" : toappend);
                if (sender._intArg < array.innerArray.Count)
                {
                    sb.Append(aSep);

                    if (//sender._intArg  < array.innerArray.Count
                        //&&
                        array.innerArray[sender._intArg ].rtType < RunTimeDataType.unknown
                        )
                    {
                        toappend = TypeConverter.ConvertToString(array.innerArray[sender._intArg ], frame, token);
                        sender._intArg++;
                        continue;
                    }
                    else
                    {

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
                        break;
                    }
                }
                else
                {
                    SLOT result = (SLOT)receiveArgs[7];

                    result.directSet(new rtString(sb.ToString()));

                    IBlockCallBack cb = (IBlockCallBack)receiveArgs[0];
                    cb.call(cb.args);
                    break;
                }
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

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

    class Array_reverse : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_reverse()
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
                return "_array_reverse";
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
                return RunTimeDataType.rt_array;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();
            arr.innerArray.Reverse();
            return arr;


        }
    }

    class Array_shift : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_shift()
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
                return "_array_shift";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

            if (arr.innerArray.Count > 0)
            {
                var result = arr.innerArray[0];
                arr.innerArray.RemoveAt(0);
                return result;
            }
            else
            {
                return rtUndefined.undefined;
            }


        }
    }

    class Array_slice : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_slice()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
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
                return "_array_slice";
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
                return RunTimeDataType.rt_array;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

            int startindex =  ((rtInt)argements[0].getValue()).value;
            int endindex = ((rtInt)argements[1].getValue()).value;

            if (startindex >= arr.innerArray.Count)
            {
                return new rtArray();
            }
            else if (startindex < 0)
            {
                startindex = arr.innerArray.Count + startindex;
                if (startindex < 0)
                {
                    startindex = 0;
                }
            }

            if (endindex > arr.innerArray.Count)
            {
                endindex = arr.innerArray.Count ;
            }
            else if (endindex < 0)
            {
                endindex = arr.innerArray.Count + endindex;
                if (endindex < 0)
                {
                    endindex = 0;
                }
            }

            rtArray newArray = new rtArray();
            for (int i = startindex; i < endindex; i++)
            {
                newArray.innerArray.Add(arr.innerArray[i]);
            }

            return newArray;

        }
    }

    class Array_splice : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_splice()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
            _paras.Add(RunTimeDataType.rt_uint);
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
                return "_array_splice";
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
                return RunTimeDataType.rt_array;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

            int startindex = ((rtInt)argements[0].getValue()).value;
            uint deleteCount = ((rtUInt)argements[1].getValue()).value;

            List<RunTimeValueBase> insert = null;
            if (argements[2].getValue().rtType == RunTimeDataType.rt_array)
            {
                insert = ((rtArray)argements[2].getValue()).innerArray;
            }

            if (startindex < 0)
            {
                startindex = arr.innerArray.Count + startindex;
                if (startindex < 0)
                {
                    startindex = 0;
                }
            }

            rtArray newArray = new rtArray();

            int st = startindex;
            while (deleteCount>0 && st< arr.innerArray.Count)
            {
                newArray.innerArray.Add(arr.innerArray[st]);
                st++;
                deleteCount--;
            }

            if (newArray.innerArray.Count > 0)
            {
                arr.innerArray.RemoveRange(startindex, newArray.innerArray.Count);
            }

            if (insert != null)
            {
                arr.innerArray.InsertRange(startindex, insert);
            }

            if (newArray.innerArray.Count > 0)
            {
                return newArray;
            }
            else
            {
                return rtNull.nullptr;
            }

        }
    }

    class Array_unshift : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_unshift()
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
                return "_array_unshift";
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();
            var args = argements[0].getValue();

            if (args.rtType == RunTimeDataType.rt_array)
            {
                var arglist = ((rtArray)args).innerArray;
                arr.innerArray.InsertRange(0,arglist);
            }

            return new rtUInt((uint)arr.innerArray.Count);


        }
    }


    class Array_concat : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_concat()
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
                return "_array_concat";
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
                return RunTimeDataType.rt_array;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();
            var args = argements[0].getValue();

            rtArray result = new rtArray();
            result.innerArray.AddRange(arr.innerArray);

            if (args.rtType == RunTimeDataType.rt_array)
            {
                var arglist = ((rtArray)args).innerArray;
                result.innerArray.AddRange(arglist);
            }

            return result;

        }
    }



    class Array_toString : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Array_toString()
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
                return "_array_toString";
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

        

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

            return new rtString(arr.ToString());
        }
    }


}
