using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;
using ASBinCode.rtti;

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

		public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		{
			errormessage = null;
			errorno = 0;

			var newvalue = new ASBinCode.rtData.rtArray();
			newvalue.objHandle.bindArrayObject = (rtObjectBase)thisObj;

			((rtObjectBase)thisObj).value.memberData[0].directSet(newvalue);


			var args = argements[0].getValue();

			if (args.rtType == RunTimeDataType.rt_array)
			{
				rtArray arr = (rtArray)args;

				if (arr.innerArray.Count == 1)
				{
					var a1 = arr.innerArray[0];
					if (TypeConverter.ObjectImplicit_ToNumber(a1.rtType, bin))
					{
						a1 = TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)a1);
					}

					if (a1.rtType == RunTimeDataType.rt_int || a1.rtType == RunTimeDataType.rt_uint || a1.rtType == RunTimeDataType.rt_number)
					{
						int c = TypeConverter.ConvertToInt(a1);

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

	//	public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
	//	{
			
	//		var newvalue = new ASBinCode.rtData.rtArray();
	//		newvalue.objHandle.bindArrayObject = (rtObject)thisObj;

	//		((rtObject)thisObj).value.memberData[0].directSet(newvalue);


	//		var args = argements[0];

	//		if (args.rtType == RunTimeDataType.rt_array)
	//		{
	//			rtArray arr = (rtArray)args;

	//			if (arr.innerArray.Count == 1)
	//			{
	//				var a1 = arr.innerArray[0];
	//				if (TypeConverter.ObjectImplicit_ToNumber(a1.rtType, bin))
	//				{
	//					a1 = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)a1);
	//				}

	//				if (a1.rtType == RunTimeDataType.rt_int || a1.rtType == RunTimeDataType.rt_uint || a1.rtType == RunTimeDataType.rt_number)
	//				{
	//					int c = TypeConverter.ConvertToInt(a1, null, null);

	//					if (c < 0)
	//					{
	//						//errormessage = "Array index is not a positive integer (" + c + ").";
	//						//errorno = 1005;
	//						//return ASBinCode.rtData.rtUndefined.undefined;
	//						success = false;
	//						stackframe.throwError(token, 1005, "Array index is not a positive integer (" + c + ").");

	//						returnSlot.directSet(rtUndefined.undefined);
	//						return;
	//					}
	//					else
	//					{
	//						while (newvalue.innerArray.Count < c)
	//						{
	//							newvalue.innerArray.Add(rtUndefined.undefined);
	//						}
	//					}

	//				}
	//				else
	//				{
	//					newvalue.innerArray.Add(a1);
	//				}
	//			}
	//			else
	//			{
	//				newvalue.innerArray.AddRange(arr.innerArray);
	//			}

	//		}

	//		//throw new NotImplementedException();
	//		success = true;
	//		returnSlot.directSet(rtUndefined.undefined);
	//	}
	}


    class Array_fill : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_fill():base(2)
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

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        //{
        //    ((rtObject)argements[0].getValue()).value.memberData[0].directSet(argements[1].getValue());
        //    ((rtArray)argements[1].getValue()).objHandle.bindArrayObject = ((rtObject)argements[0].getValue());

        //    errormessage = null;
        //    errorno = 0;
        //    return rtUndefined.undefined;

        //    //throw new NotImplementedException();
        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			((rtObjectBase)argements[0]).value.memberData[0].directSet(argements[1]);
			((rtArray)argements[1]).objHandle.bindArrayObject = ((rtObjectBase)argements[0]);

			success = true;
			returnSlot.directSet(rtUndefined.undefined);
			//return rtUndefined.undefined;
		}
	}


    class Array_load : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_load():base(1)
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

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        //{
        //    errormessage = null;
        //    errorno = 0;

        //    if (((rtArray)argements[0].getValue()).objHandle.bindArrayObject == null)
        //    {
        //        return rtNull.nullptr;
        //    }
        //    else
        //    {
        //        return ((rtArray)argements[0].getValue()).objHandle.bindArrayObject;
        //    }
        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			if (((rtArray)argements[0]).objHandle.bindArrayObject == null)
			{
				//return rtNull.nullptr;
				returnSlot.directSet(rtNull.nullptr);
			}
			else
			{
				//return ((rtArray)argements[0].getValue()).objHandle.bindArrayObject;
				returnSlot.directSet(((rtArray)argements[0]).objHandle.bindArrayObject);
			}
		}
	}


    class Array_getLength : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_getLength():base(0)
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

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        //{
        //    errormessage = null;
        //    errorno = 0;

        //    return 
        //        new rtUInt(
        //            (uint)
        //            ((rtArray)(((rtObject)thisObj).value.memberData[0].getValue())).innerArray.Count
        //        )
        //        ;
        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//throw new NotImplementedException();
			//errormessage = null;
			//errorno = 0;

			success = true;
			returnSlot.setValue((uint)
					((rtArray)(((rtObjectBase)thisObj).value.memberData[0].getValue())).innerArray.Count);

			//return
			//	new rtUInt(
			//		(uint)
			//		((rtArray)(((rtObject)thisObj).value.memberData[0].getValue())).innerArray.Count
			//	)
			//	;
		}
	}



    class Array_setLength : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_setLength():base(1)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    var list = ((rtArray)(((rtObject)thisObj).value.memberData[0].getValue())).innerArray;

		//    uint newlen = ((rtUInt)argements[0].getValue()).value;

		//    if (newlen > list.Count)
		//    {
		//        while (list.Count < newlen)
		//        {
		//            list.Add(rtUndefined.undefined);
		//        }
		//    }
		//    else if (newlen < list.Count)
		//    {
		//        list.RemoveRange((int)newlen, list.Count - (int)newlen);
		//    }


		//    return rtUndefined.undefined;

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			var list = ((rtArray)(((rtObjectBase)thisObj).value.memberData[0].getValue())).innerArray;

			uint newlen = ((rtUInt)argements[0]).value;

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


			returnSlot.directSet(rtUndefined.undefined);
		}

	}

    class Array_insertAt : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_insertAt():base(2)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

		//    int idx = ((rtInt)argements[0].getValue()).value;
		//    var toinsert = argements[1].getValue();
		//    if (idx < 0)
		//    {
		//        idx = arr.innerArray.Count + idx;
		//        if (idx < 0) { idx = 0; }
		//    }
		//    else if (idx > arr.innerArray.Count)
		//    {
		//        idx = arr.innerArray.Count;
		//    }

		//    arr.innerArray.Insert(idx, toinsert);


		//    return rtUndefined.undefined;

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

			int idx = ((rtInt)argements[0]).value;
			var toinsert = argements[1];
			if (idx < 0)
			{
				idx = arr.innerArray.Count + idx;
				if (idx < 0) { idx = 0; }
			}
			else if (idx > arr.innerArray.Count)
			{
				idx = arr.innerArray.Count;
			}

			arr.innerArray.Insert(idx, (RunTimeValueBase)toinsert.Clone());

			returnSlot.directSet(rtUndefined.undefined);
			//return rtUndefined.undefined;
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

            rtArray array = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

            if (array.innerArray.Count == 0)
            {
                cb.call(cb.args);
                resultSlot.directSet(new rtString(string.Empty));
                return;
            }


            BlockCallBackBase sepcb = frame.player.blockCallBackPool.create();
            sepcb.scope = scope;
            sepcb._intArg = 0;

            object[] sendargs = sepcb.cacheObjects;
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

            BlockCallBackBase valueCB = frame.player.blockCallBackPool.create();
            valueCB._intArg = sender._intArg + 1;
            valueCB.args = valueCB.copyFromReceiveArgs( receiveArgs);
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
                BlockCallBackBase valueCB = frame.player.blockCallBackPool.create();
                valueCB._intArg = sender._intArg;
                valueCB.args = valueCB.copyFromReceiveArgs( receiveArgs);
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

                        BlockCallBackBase valueCB = frame.player.blockCallBackPool.create();
                        valueCB._intArg = sender._intArg + 1;
                        valueCB.args = valueCB.copyFromReceiveArgs( receiveArgs);
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


    class Array_pop : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_pop():base(0)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

		//    if (arr.innerArray.Count > 0)
		//    {
		//        var result = arr.innerArray[arr.innerArray.Count - 1];
		//        arr.innerArray.RemoveAt(arr.innerArray.Count-1);
		//        return result;
		//    }
		//    else
		//    {
		//        return rtUndefined.undefined;
		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

			if (arr.innerArray.Count > 0)
			{
				var result = arr.innerArray[arr.innerArray.Count - 1];
				arr.innerArray.RemoveAt(arr.innerArray.Count - 1);
				//return result;
				returnSlot.directSet(result);
			}
			else
			{
				//return rtUndefined.undefined;
				returnSlot.directSet(rtUndefined.undefined);
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

            rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();
            var args = argements[0].getValue();

            if (args.rtType == RunTimeDataType.rt_array)
            {
                var arglist = ((rtArray)args).innerArray;
                arr.innerArray.AddRange(arglist);
            }
            
            return new rtUInt((uint)arr.innerArray.Count);
            

        }
    }

    class Array_removeAt : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_removeAt():base(1)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

		//    int idx = ((rtInt)argements[0].getValue()).value;
		//    if (idx < 0)
		//    {
		//        idx = arr.innerArray.Count + idx;
		//        if (idx < 0) { idx = 0; }
		//    }
		//    else if (idx > arr.innerArray.Count-1)
		//    {
		//        return rtUndefined.undefined;
		//    }

		//    var r = arr.innerArray[idx];
		//    arr.innerArray.RemoveAt(idx);
		//    return r;

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;
			rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

			int idx = ((rtInt)argements[0]).value;
			if (idx < 0)
			{
				idx = arr.innerArray.Count + idx;
				if (idx < 0) { idx = 0; }
			}
			else if (idx > arr.innerArray.Count - 1)
			{
				returnSlot.directSet(rtUndefined.undefined);
			}

			var r = arr.innerArray[idx];
			arr.innerArray.RemoveAt(idx);
			//return r;
			returnSlot.directSet(r);
		}

	}

    class Array_reverse : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_reverse():base(0)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();
		//    arr.innerArray.Reverse();
		//    return arr;


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;
			rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();
			arr.innerArray.Reverse();
			//return arr;
			returnSlot.directSet(arr);
		}

	}

    class Array_shift : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_shift():base(0)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

		//    if (arr.innerArray.Count > 0)
		//    {
		//        var result = arr.innerArray[0];
		//        arr.innerArray.RemoveAt(0);
		//        return result;
		//    }
		//    else
		//    {
		//        return rtUndefined.undefined;
		//    }


		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

			if (arr.innerArray.Count > 0)
			{
				var result = arr.innerArray[0];
				arr.innerArray.RemoveAt(0);
				//return result;
				returnSlot.directSet(result);
			}
			else
			{
				//return rtUndefined.undefined;
				returnSlot.directSet(rtUndefined.undefined);
			}
		}


	}

    class Array_slice : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_slice():base(2)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

		//    int startindex =  ((rtInt)argements[0].getValue()).value;
		//    int endindex = ((rtInt)argements[1].getValue()).value;

		//    if (startindex >= arr.innerArray.Count)
		//    {
		//        return new rtArray();
		//    }
		//    else if (startindex < 0)
		//    {
		//        startindex = arr.innerArray.Count + startindex;
		//        if (startindex < 0)
		//        {
		//            startindex = 0;
		//        }
		//    }

		//    if (endindex > arr.innerArray.Count)
		//    {
		//        endindex = arr.innerArray.Count ;
		//    }
		//    else if (endindex < 0)
		//    {
		//        endindex = arr.innerArray.Count + endindex;
		//        if (endindex < 0)
		//        {
		//            endindex = 0;
		//        }
		//    }

		//    rtArray newArray = new rtArray();
		//    for (int i = startindex; i < endindex; i++)
		//    {
		//        newArray.innerArray.Add(arr.innerArray[i]);
		//    }

		//    return newArray;

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

			int startindex = ((rtInt)argements[0]).value;
			int endindex = ((rtInt)argements[1]).value;

			if (startindex >= arr.innerArray.Count)
			{
				//return new rtArray();
				returnSlot.directSet(new rtArray());
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
				endindex = arr.innerArray.Count;
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
				newArray.innerArray.Add((RunTimeValueBase)arr.innerArray[i].Clone());
			}

			returnSlot.directSet(newArray);
			//return newArray;
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

            rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

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
                newArray.innerArray.Add(arr.innerArray[st]); //此处无需clone,因为会移除
                st++;
                deleteCount--;
            }

            if (newArray.innerArray.Count > 0)
            {
                arr.innerArray.RemoveRange(startindex, newArray.innerArray.Count);
            }

            if (insert != null)
            {
				for (int i = 0; i < insert.Count; i++)
				{
					arr.innerArray.Insert(startindex, (RunTimeValueBase)insert[i].Clone());
					//arr.innerArray.InsertRange(startindex, insert);
				}                
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

            rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();
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

            rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();
            var args = argements[0].getValue();

            rtArray result = new rtArray();
			for (int i = 0; i < arr.innerArray.Count; i++)
			{
				result.innerArray.Add((RunTimeValueBase)arr.innerArray[i].Clone());
			}
            

            if (args.rtType == RunTimeDataType.rt_array)
            {
                var arglist = ((rtArray)args).innerArray;
				for (int i = 0; i < arglist.Count; i++)
				{
					result.innerArray.Add((RunTimeValueBase)arglist[i].Clone());
				}
                
            }

            return result;

        }
    }



    class Array_toString : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Array_toString():base(0)
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



		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    rtArray arr = (rtArray)((rtObject)thisObj).value.memberData[0].getValue();

		//    return new rtString(arr.ToString());
		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			rtArray arr = (rtArray)((rtObjectBase)thisObj).value.memberData[0].getValue();

			//return new rtString(arr.ToString());
			returnSlot.setValue(arr.ToString());
		}

	}


}
