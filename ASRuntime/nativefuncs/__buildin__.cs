using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
    class __buildin__ismethod : NativeConstParameterFunction
    {
        List<RunTimeDataType> para;
        public __buildin__ismethod():base(1)
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

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null;
        //    errorno = 0;

        //    if (argements[0].getValue().rtType == RunTimeDataType.rt_null)
        //    {
        //        return ASBinCode.rtData.rtBoolean.False;
        //    }
        //    else
        //    {
        //        ASBinCode.rtData.rtFunction f = (ASBinCode.rtData.rtFunction)argements[0].getValue();
        //        if (f.ismethod)
        //        {
        //            return ASBinCode.rtData.rtBoolean.True;
        //        }
        //        else
        //        {
        //            return ASBinCode.rtData.rtBoolean.False;
        //        }
        //    }
        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//errormessage = null;
			//errorno = 0;

			success = true;

			if (argements[0].rtType == RunTimeDataType.rt_null)
			{
				//return ASBinCode.rtData.rtBoolean.False;
				returnSlot.directSet(rtBoolean.False);
			}
			else
			{
				ASBinCode.rtData.rtFunction f = (ASBinCode.rtData.rtFunction)argements[0];
				if (f.ismethod)
				{
					//return ASBinCode.rtData.rtBoolean.True;
					returnSlot.directSet(rtBoolean.True);
				}
				else
				{
					//return ASBinCode.rtData.rtBoolean.False;
					returnSlot.directSet(rtBoolean.False);
				}
			}

			//throw new NotImplementedException();
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
				if (frame.player.infoOutput != null)
				{
					frame.player.infoOutput.Info(string.Empty);
				}
                cb.call(cb.args);
                return;
            }

            rtArray array = (rtArray)(argements[0].getValue());

            if (array.innerArray.Count == 0)
            {
				if (frame.player.infoOutput != null)
				{
					frame.player.infoOutput.Info(string.Empty);
				}
                cb.call(cb.args);
                return;
            }


            BlockCallBackBase sepcb = frame.player.blockCallBackPool.create();
            sepcb.scope = scope;
            sepcb._intArg = 0;
            sepcb.setCallBacker(null);
			sepcb._intArg2 = 0;

            object[] sendargs = sepcb.cacheObjects;
            sendargs[0] = cb;
            sendargs[1] = array;
            sendargs[2] = frame;
            sendargs[3] = token;
            sendargs[4] = scope;
            sendargs[5] = " ";
            sendargs[6] = new StringBuilder();
            sendargs[7] = resultSlot;
            //sendargs[8] = new rtInt(0);
            sepcb.args = sendargs;

            _SeptoString_CB(sepcb, sendargs);
        }

        private static rtString UNDEFINED = new rtString("undefined");

        private void _SeptoString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            
            rtArray array = (rtArray)receiveArgs[1];

            BlockCallBackBase valueCB = frame.player.blockCallBackPool.create();
            valueCB._intArg = sender._intArg + 1;
            valueCB.args = valueCB.copyFromReceiveArgs( receiveArgs);
            valueCB.setCallBacker(_ValueToString_CB);
			valueCB._intArg2 = sender._intArg2;
            var v = array.innerArray[sender._intArg];
            if (v.rtType == RunTimeDataType.rt_void)
            {
                v = UNDEFINED;
            }

            sender.noticeEnd();


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

			//((rtInt)receiveArgs[8]).value++;
			sender._intArg2++;

            if (sender._intArg2==10)//((rtInt)receiveArgs[8]).value == 10)    //堆栈清理,防止溢出...
            {
				sender._intArg2 = 0; //((rtInt)receiveArgs[8]).value = 0;
                BlockCallBackBase valueCB = frame.player.blockCallBackPool.create();
                valueCB._intArg = sender._intArg;valueCB._intArg2 = 0;
                valueCB.args = valueCB.copyFromReceiveArgs( receiveArgs);
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


                BlockCallBackBase valueCB = frame.player.blockCallBackPool.create();
                valueCB._intArg = sender._intArg + 1;valueCB._intArg2 = sender._intArg2;
                valueCB.args = valueCB.copyFromReceiveArgs(receiveArgs);
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
            else
            {
				//ISLOT result = (ISLOT)receiveArgs[7];

				//result.directSet(new rtString(sb.ToString()));
				if (frame.player.infoOutput != null)
				{
					frame.player.infoOutput.Info(sb.ToString());
				}
                IBlockCallBack cb = (IBlockCallBack)receiveArgs[0];
                cb.call(cb.args);
            }

        }
    }



    class __buildin__isnan : NativeConstParameterFunction
    {
        List<RunTimeDataType> para;
        public __buildin__isnan():base(1)
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

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null;
        //    errorno = 0;

        //    double num = TypeConverter.ConvertToNumber(argements[0].getValue());

        //    if (double.IsNaN(num))
        //    {
        //        return rtBoolean.True;
        //    }
        //    else
        //    {
        //        return rtBoolean.False;
        //    }

        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;
			double num = TypeConverter.ConvertToNumber(argements[0]);

			if (double.IsNaN(num))
			{
				//return rtBoolean.True;
				returnSlot.directSet(rtBoolean.True);
			}
			else
			{
				//return rtBoolean.False;
				returnSlot.directSet(rtBoolean.False);
			}
		}

	}


    class __buildin__isfinite : NativeConstParameterFunction
    {
        List<RunTimeDataType> para;
        public __buildin__isfinite():base(1)
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

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null;
        //    errorno = 0;

        //    double num = TypeConverter.ConvertToNumber(argements[0].getValue());

        //    if (double.IsInfinity(num))
        //    {
        //        return rtBoolean.True;
        //    }
        //    else
        //    {
        //        return rtBoolean.False;
        //    }

        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;
			double num = TypeConverter.ConvertToNumber(argements[0]);

			if (!double.IsInfinity(num))
			{
				//return rtBoolean.True;
				returnSlot.directSet(rtBoolean.True);
			}
			else
			{
				//return rtBoolean.False;
				returnSlot.directSet(rtBoolean.False);
			}
		}

	}

    class __buildin__parseint : NativeConstParameterFunction
    {
        List<RunTimeDataType> para;
        public __buildin__parseint():base(2)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string str= TypeConverter.ConvertToString(argements[0].getValue(),null,null);
		//    uint radix = TypeConverter.ConvertToUInt(argements[1].getValue(), null, null);

		//    if (String.IsNullOrEmpty(str))
		//    {
		//        return new rtNumber(double.NaN);
		//    }
		//    //ASCII 48-57 : 0-9 ,65-90 : A-Z;

		//    str = str.ToUpper();
		//    str=str.TrimStart();
		//    str=str.TrimStart('0');

		//    if (radix == 0) { radix = 10; }
		//    if (radix < 2 || radix > 36) { return new rtNumber(double.NaN); }

		//    uint allowidx = 48 + radix;

		//    if (radix > 10)
		//    {
		//        allowidx = 65 + radix - 10;
		//    }

		//    double output = double.NaN;

		//    for (int i = 0; i < str.Length; i++)
		//    {
		//        char c = str[i];
		//        if (c < allowidx && ((c < 58 && c >= 48) || c>= 65))
		//        {
		//            if (double.IsNaN(output))
		//            {
		//                output = c < 58 ? (c - 48) : (c - 65 + 10);
		//            }
		//            else
		//            {
		//                output = output * radix + (c < 58 ? (c - 48) : (c - 65 + 10));
		//            }
		//        }
		//        else
		//        {
		//            break;
		//        }
		//    }


		//    return new rtNumber(output);

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			string str = TypeConverter.ConvertToString(argements[0], null, null);
			uint radix = TypeConverter.ConvertToUInt(argements[1], null, null);

			if (String.IsNullOrEmpty(str))
			{
				returnSlot.setValue(double.NaN);
				 //new rtNumber(double.NaN);
			}
			//ASCII 48-57 : 0-9 ,65-90 : A-Z;

			str = str.ToUpper();
			str = str.TrimStart();
			str = str.TrimStart('0');

			if (radix == 0) { radix = 10; }
			if (radix < 2 || radix > 36) { returnSlot.setValue(double.NaN); }//return new rtNumber(double.NaN); }

			uint allowidx = 48 + radix;

			if (radix > 10)
			{
				allowidx = 65 + radix - 10;
			}

			double output = double.NaN;

			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (c < allowidx && ((c < 58 && c >= 48) || c >= 65))
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

			returnSlot.setValue(output);
			//return new rtNumber(output);
		}

	}

    class __buildin__parsefloat : NativeConstParameterFunction
    {
        List<RunTimeDataType> para;
        public __buildin__parsefloat():base(1)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;

		//    string str = TypeConverter.ConvertToString(argements[0].getValue(), null, null);

		//    if (String.IsNullOrEmpty(str))
		//    {
		//        return new rtNumber(double.NaN);
		//    }

		//    str=str.Trim();

		//    bool hasreaddot=false;

		//    string newstr = string.Empty;
		//    for (int i = 0; i < str.Length; i++)
		//    {
		//        char c = str[i];

		//        if (c == '.')
		//        {
		//            if (hasreaddot)
		//            {
		//                break;
		//            }
		//            hasreaddot = true;
		//        }
		//        else if (c == '-' || c == '+')
		//        {
		//            if (!string.IsNullOrEmpty(newstr))
		//            {
		//                break;
		//            }
		//            if (i + 1 >= str.Length)
		//            {
		//                return new rtNumber(double.NaN);
		//            }
		//            char n = str[i + 1];

		//            if (n == '.')
		//            {
		//                if (i + 2 >= str.Length)
		//                {
		//                    return new rtNumber(double.NaN);
		//                }

		//                n = str[i + 2];
		//                if (n < 48 || n > 57)
		//                {
		//                    return new rtNumber(double.NaN);
		//                }
		//            }
		//            else if (n < 48 || n > 57)
		//            {
		//                return new rtNumber(double.NaN);
		//            }


		//        }
		//        else if (c == 'e' || c == 'E')
		//        {
		//            if (string.IsNullOrEmpty(newstr))
		//            {
		//                return new rtNumber(double.NaN);
		//            }
		//            else
		//            {
		//                if (i + 1 >= str.Length)
		//                {
		//                    break;
		//                }

		//                string epart = "e";
		//                int st = i + 1;
		//                char ep = str[st];
		//                if (ep == '+' || ep == '-')
		//                {
		//                    epart += ep;
		//                    st++;

		//                    if (!(st < str.Length))
		//                    {
		//                        break;
		//                    }

		//                    ep = str[st];
		//                    if (ep < 48 || ep > 57)
		//                    {
		//                        break;
		//                    }
		//                    epart += ep;
		//                    st++;
		//                }


		//                for (int j = st; j < str.Length; j++)
		//                {
		//                    char n = str[j];
		//                    if (n < 48 || n > 57)
		//                    {
		//                        break;
		//                    }
		//                    epart += n;
		//                }
		//                newstr += epart;

		//                break;
		//            }

		//        }
		//        else if (c < 48 || c > 57)
		//        {
		//            if (string.IsNullOrEmpty(newstr))
		//            {
		//                return new rtNumber(double.NaN);
		//            }
		//            else
		//            {
		//                break;
		//            }
		//        }
		//        newstr = newstr + c;

		//    }

		//    return new rtNumber(double.Parse(newstr));

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			string str = TypeConverter.ConvertToString(argements[0], null, null);

			if (String.IsNullOrEmpty(str))
			{
				//return new rtNumber(double.NaN);
				returnSlot.setValue(double.NaN);
			}

			str = str.Trim();

			bool hasreaddot = false;

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
						//return new rtNumber(double.NaN);
						returnSlot.setValue(double.NaN);
					}
					char n = str[i + 1];

					if (n == '.')
					{
						if (i + 2 >= str.Length)
						{
							//return new rtNumber(double.NaN);
							returnSlot.setValue(double.NaN);
						}

						n = str[i + 2];
						if (n < 48 || n > 57)
						{
							//return new rtNumber(double.NaN);
							returnSlot.setValue(double.NaN);
						}
					}
					else if (n < 48 || n > 57)
					{
						//return new rtNumber(double.NaN);
						returnSlot.setValue(double.NaN);
					}


				}
				else if (c == 'e' || c == 'E')
				{
					if (string.IsNullOrEmpty(newstr))
					{
						//return new rtNumber(double.NaN);
						returnSlot.setValue(double.NaN);
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
						//return new rtNumber(double.NaN);
						returnSlot.setValue(double.NaN);
					}
					else
					{
						break;
					}
				}
				newstr = newstr + c;

			}

			//return new rtNumber(double.Parse(newstr));
			returnSlot.setValue(double.Parse(newstr));
		}

	}

	class __buildin__getDefinitionByName : nativefuncs.NativeConstParameterFunction
	{
		public __buildin__getDefinitionByName():base(1)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_string);
		}

		public override string name
		{
			get
			{
				return "_flash_util_functions_getDefinitionByName";
			}
		}

		List<RunTimeDataType> para;
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
				return RunTimeDataType.rt_void;
			}
		}

		public override bool isMethod
		{
			get { return true; }
		}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			string classname = TypeConverter.ConvertToString(argements[0], stackframe, token);

			if (classname==null)
			{
				success = false;
				stackframe.throwArgementException(token, "Argument name cannot be null");
			}
			else
			{
				var c = bin.getClassDefinitionByName(classname);

				if (c != null)
				{
					if (!operators.InstanceCreator.init_static_class(c, stackframe.player, token))
					{
						success = false;
						return;
					}
					else
					{
						success = true;

						returnSlot.directSet(stackframe.player.static_instance[c.staticClass.classid]);

					}

					
					
					//returnSlot.directSet(new ASBinCode.rtData.rtObject(c,null));

				}
				else
				{
					success = false;
					stackframe.throwError(token,1065, "Variable "+classname+" is not defined");
				}
			}

			
		}
	}


	class __buildin__getQualifiedClassName : nativefuncs.NativeConstParameterFunction
	{
		public __buildin__getQualifiedClassName() : base(1)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
		}

		public override string name
		{
			get
			{
				return "flash_utils_functions_getQualifiedClassName";
			}
		}

		List<RunTimeDataType> para;
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
				return RunTimeDataType.rt_string;
			}
		}

		public override bool isMethod
		{
			get { return true; }
		}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			var type = argements[0].rtType;

			switch (type)
			{
				case RunTimeDataType.fun_void:
					returnSlot.setValue("void");
					break;
				case RunTimeDataType.rt_array:
					returnSlot.setValue("Array");
					break;
				case RunTimeDataType.rt_boolean:
					returnSlot.setValue("Boolean");
					break;
				case RunTimeDataType.rt_function:
					returnSlot.setValue("Function");
					break;
				case RunTimeDataType.rt_int:
					returnSlot.setValue("int");
					break;
				case RunTimeDataType.rt_null:
					returnSlot.setValue("null");
					break;
				case RunTimeDataType.rt_number:
					returnSlot.setValue("Number");
					break;
				case RunTimeDataType.rt_string:
					returnSlot.setValue("String");
					break;
				case RunTimeDataType.rt_uint:
					returnSlot.setValue("uint");
					break;
				case RunTimeDataType.rt_void:
					returnSlot.setValue("void");
					break;
				case RunTimeDataType.unknown:
					returnSlot.setValue("void");
					break;
				case RunTimeDataType._OBJECT:
					returnSlot.setValue("Object");
					break;

				default:

					var c = stackframe.player.swc.getClassByRunTimeDataType(type);
					if (c.instanceClass != null)
						c = c.instanceClass;

					returnSlot.setValue(c.package+"::"+c.name);
					break;
			}



			success = true;
		}
	}


}
