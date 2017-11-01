using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASRuntime
{
    public class Player
    {
		internal IRuntimeOutput infoOutput;

        internal Dictionary<int, rtObject> static_instance;
        internal Dictionary<int, RunTimeScope> outpackage_runtimescope;

		private rtObject _buildin_class_;
		private rtFunction _getMethod;
		private rtFunction _createinstance;
		private rtFunction _getMemberValue;
		private rtFunction _setMemberValue;

		public LinkTypeMapper linktypemapper;
        
        internal CSWC swc;
        private CodeBlock defaultblock;
        public void loadCode(CSWC swc,CodeBlock block=null)
        {
            this.swc = swc;

            static_instance = new Dictionary<int, rtObject>();
            outpackage_runtimescope = new Dictionary<int, RunTimeScope>();
			_buildin_class_ = null;
			_getMethod = null;
			_createinstance = null;
			_getMemberValue = null;
			_setMemberValue = null;

			//***初始化类型映射****

			linktypemapper = new RuntimeLinkTypeMapper();
            linktypemapper.init(swc);

			//****************
			


            if (block != null)
            {
                defaultblock = block;
            }
            else if (swc.blocks.Count == 1)
            {
                defaultblock = swc.blocks[0];
            }
            else
            {
                //查找文档类
                for (int i = 0; i < swc.classes.Count; i++)
                {
                    if (swc.classes[i].isdocumentclass)
                    {
                        defaultblock = new CodeBlock(int.MaxValue, "_player_run",-65535,true);
                        defaultblock.scope = new ASBinCode.scopes.StartUpBlockScope();
                        defaultblock.totalRegisters = 1;

                        {
                            OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_argement, new SourceToken(0, 0, ""));
                            opMakeArgs.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(swc.classes[i].classid));
                            opMakeArgs.arg1Type = RunTimeDataType.rt_int;
                            defaultblock.opSteps.Add(opMakeArgs);

                        }
                        {
                            OpStep step = new OpStep(OpCode.new_instance, new SourceToken(0, 0, ""));
                            step.arg1 = new RightValue(new rtInt(swc.classes[i].classid));
                            step.arg1Type = swc.classes[i].getRtType();
                            step.reg = new Register(0,ushort.MaxValue);
                            step.regType = swc.classes[i].getRtType();

                            defaultblock.opSteps.Add(step);
                        }
                        break;
                    }
                }
                if (defaultblock == null)
                {
                    //***查找第一个类的包外代码
                    for (int i = swc.classes.Count-1; i >0;i--)
                    {
                        if (swc.classes[i].staticClass != null)
                        {
                            defaultblock = swc.blocks[swc.classes[i].outscopeblockid];
                            break;
                        }
                    }
                }

                if (defaultblock == null && swc.blocks.Count >0)
                {
                    defaultblock = swc.blocks[0];
                }
                
            }
            
        }

		private bool _hasInitStack=false;
		private bool _hasInitBaseCode = false;

        /// <summary>
        /// 当调用本地函数时，会自动设置这个字段，如果本地函数抛出异常并被try catch到后，会根据这个字段继续后续操作
        /// </summary>
        internal operators.FunctionCaller _nativefuncCaller;
        


        private error.InternalError runtimeError;

       
        /// <summary>
        /// 调用堆栈
        /// </summary>
        private Stack<StackFrame> runtimeStack;
        StackSlot[] stackSlots;
        private FrameInfo displayStackFrame;

		internal StackFrame.StackFramePool stackframePool;
		internal operators.FunctionCaller.FunctionCallerPool funcCallerPool;
		internal BlockCallBackBase.BlockCallBackBasePool blockCallBackPool;
		private runFuncResult.ResultPool runFuncresultPool;

		public Player(IRuntimeOutput output)
		{
			infoOutput = output;
		}
		public Player():this(new ConsoleOutput()) { }

		private void clearEnv()
		{
			for (int i = 0; i < stackSlots.Length; i++)
			{
				stackSlots[i].clear();
			}
			funcCallerPool.reset();
			blockCallBackPool.reset();
			stackframePool.reset();
			runFuncresultPool.reset();

			runtimeStack.Clear();
			runtimeError = null;
			receive_error = null;

			//while (runtimeStack.Count >0)
			//{
			//	runtimeStack.Pop().close();
			//}
			currentRunFrame = null;
		}

		private void initPlayer()
		{
			if (!_hasInitStack)
			{
				stackframePool = new StackFrame.StackFramePool();
				funcCallerPool = new operators.FunctionCaller.FunctionCallerPool(this);
				blockCallBackPool = new BlockCallBackBase.BlockCallBackBasePool(this);
				runFuncresultPool = new runFuncResult.ResultPool();

				runtimeStack = new Stack<StackFrame>();
				stackSlots = new StackSlot[1024];
				for (int i = 0; i < stackSlots.Length; i++)
				{
					stackSlots[i] = new StackSlot(swc);
				}
				StackLinkObjectCache lobjcache = new StackLinkObjectCache(swc, this);
				stackSlots[0]._linkObjCache = lobjcache;
				for (int i = 1; i < stackSlots.Length; i++)
				{
					stackSlots[i]._linkObjCache = lobjcache.Clone();
				}
				_hasInitStack = true;
			}
			if (!_hasInitBaseCode)
			{
				if (swc.ErrorClass != null)
				{
					//***先执行必要代码初始化****
					var block = swc.blocks[swc.ErrorClass.outscopeblockid];
					HeapSlot[] initdata = genHeapFromCodeBlock(block);
					callBlock(block, initdata, new StackSlot(swc), null,
						new SourceToken(0, 0, ""), null,
						null, RunTimeScopeType.startup
						);
					while (step())
					{

					}

					foreach (var item in static_instance)
					{
						if (item.Value.value._class.name == "$@__buildin__")
						{
							_buildin_class_ = item.Value;

							for (int i = 0; i < _buildin_class_.value._class.classMembers.Count; i++)
							{
								var m = _buildin_class_.value._class.classMembers[i];
								if (m.name == "_getMethod")
								{
									_getMethod = (rtFunction)((ClassMethodGetter)m.bindField).getMethod(_buildin_class_);
									continue;
								}
								if (m.name == "_createInstance")
								{
									_createinstance = (rtFunction)((ClassMethodGetter)m.bindField).getMethod(_buildin_class_);
									continue;
								}
								if (m.name == "_getMemberValue")
								{
									_getMemberValue = (rtFunction)((ClassMethodGetter)m.bindField).getMethod(_buildin_class_);
									continue;
								}
								if (m.name == "_setMemberValue")
								{
									_setMemberValue = (rtFunction)((ClassMethodGetter)m.bindField).getMethod(_buildin_class_);
									continue;
								}
							}


							break;
						}
					}

				}


				_hasInitBaseCode = true;
			}
		}

		public RunTimeValueBase run(RightValueBase result)
		{
			if (defaultblock == null || swc == null || swc.blocks.Count == 0)
			{
				if (infoOutput != null)
				{
					infoOutput.Info(string.Empty);
					infoOutput.Info("====没有找到可执行的代码====");
					infoOutput.Info("用[Doc]标记文档类");
					infoOutput.Info("或者第一个类文件的包外代码作为入口");
				}
				return null;
			}
			
			initPlayer();

			HeapSlot[] data = genHeapFromCodeBlock(defaultblock);
			return run2(defaultblock,data, result);
		}


        private RunTimeValueBase run2(CodeBlock runblock,HeapSlot[] blockMemberHeap, RightValueBase result)
        {
           
            var topscope = callBlock(runblock, blockMemberHeap, new StackSlot(swc), null, 
                new SourceToken(0, 0, ""),null,
                null, RunTimeScopeType.startup
                );
            displayStackFrame = runtimeStack.Peek().getInfo();

			try
			{

				while (true)
				{
					//try
					{
						while (step())
						{

						}
						break;
					}
					//catch (ASRunTimeException)   //引擎抛出的异常直接抛出
					//{
					//	throw;
					//}
					//catch (StackOverflowException)
					//{
					//	throw;
					//}
					//catch (OutOfMemoryException)
					//{
					//	throw;
					//}
					//catch (Exception le)    //捕获外部函数异常
					//{
					//	if (currentRunFrame != null)
					//	{
					//		if (_nativefuncCaller != null)
					//		{
					//			if (_nativefuncCaller.callbacker != null)
					//			{
					//				_nativefuncCaller.callbacker.noticeRunFailed();
					//			}
					//			_nativefuncCaller.release();
					//			_nativefuncCaller = null;
					//		}

					//		SourceToken token;

					//		if (currentRunFrame.codeLinePtr < currentRunFrame.block.opSteps.Count)
					//		{
					//			token = currentRunFrame.block.opSteps[currentRunFrame.codeLinePtr].token;
					//		}
					//		else
					//		{
					//			token = new SourceToken(0, 0, string.Empty);
					//		}

					//		currentRunFrame.throwAneException(token
					//			, le.Message);
					//		currentRunFrame.receiveErrorFromStackFrame(currentRunFrame.runtimeError);

					//		continue;
					//	}
					//	else
					//	{
					//		throw;
					//	}
					//}
				}





				if (runtimeError != null)
				{
					outPutErrorMessage(runtimeError);
				}

				funcCallerPool.checkpool();
				blockCallBackPool.checkpool();
				stackframePool.checkpool();
				runFuncresultPool.checkpool();

				//#if DEBUG
				//            if (infoOutput !=null)
				//            {

				//                Console.WriteLine();
				//                Console.WriteLine("====程序状态====");
				//                Console.ForegroundColor = ConsoleColor.Yellow;
				//                Console.WriteLine("Variables:");

				//                for (int i = 0; i < displayStackFrame.block.scope.members.Count; i++)
				//                {
				//                    Console.WriteLine("\t" + displayStackFrame.block.scope.members[i].name + "\t|\t" + displayStackFrame.scope.memberData[i].getValue());
				//                }
				//                Console.ForegroundColor = ConsoleColor.Green;
				//                Console.WriteLine("Registers:");
				//                for (int i = 0; i < displayStackFrame.block.totalRegisters; i++)
				//                {
				//                    if (stackSlots[i].getValue()!=null)
				//                    {
				//                        Console.WriteLine("\t" + "EAX(" + i + ")\t|\t" + stackSlots[i].getValue());
				//                    }
				//                }
				//                Console.ResetColor();
				//            }
				//#endif
				if (result != null && runtimeError == null)
				{
					return result.getValue(topscope, displayStackFrame);
				}
				else
				{
					return null;
				}

			}
			finally
			{
				clearEnv();
			}
        }






        private static readonly HeapSlot[] emptyMembers = new HeapSlot[0];
        internal HeapSlot[] genHeapFromCodeBlock(ASBinCode.CodeBlock calledblock)
        {
            if (calledblock.scope.members.Count == 0)
            {
                return emptyMembers;
            }

            var memberDataList = new HeapSlot[calledblock.scope.members.Count];
            for (int i = 0; i < memberDataList.Length; i++)
            {
                memberDataList[i] = new HeapSlot();
                var vt = ((VariableBase)calledblock.scope.members[i]).valueType;
                memberDataList[i].setDefaultType(
                    vt,
                    TypeConverter.getDefaultValue(vt).getValue(null,null)
                    );
            }
            return memberDataList;
        }

        private static CodeBlock blankBlock;
        internal void CallBlankBlock(IBlockCallBack callbacker)
        {
            if (blankBlock == null)
            {
                blankBlock = new CodeBlock(int.MaxValue - 1, "#blank", -65535, false);
				blankBlock.opSteps.Add(new OpStep(OpCode.flag, new SourceToken(0, 0, string.Empty)));
            }

            callBlock(blankBlock, null, null, null,null, callbacker, null, RunTimeScopeType.function);

        }

        public rtObject alloc_pureHostedOrLinkedObject(ASBinCode.rtti.Class cls)
        {
            return operators.InstanceCreator.createPureHostdOrLinkObject(this, cls);
        }

        public ASBinCode.rtti.LinkSystemObject alloc_LinkObjValue(ASBinCode.rtti.Class cls)
        {
            return operators.InstanceCreator.createLinkObjectValue(this, cls);
        }

        public bool init_static_class(ASBinCode.rtti.Class cls,SourceToken token)
        {
            return operators.InstanceCreator.init_static_class(cls, this, token);
        }


        internal RunTimeScope callBlock(ASBinCode.CodeBlock calledblock,
            HeapSlot[] membersHeap,
            SLOT returnSlot,
            RunTimeScope callerScope,
            SourceToken token,
            IBlockCallBack callbacker,
            RunTimeValueBase this_pointer,
            RunTimeScopeType type
            )
        {
            

            int startOffset = 0;
            if (runtimeStack.Count > 0)
            {
                var rs = runtimeStack.Peek();

                startOffset = rs.offset + rs.block.totalRegisters+1+1 + rs.call_parameter_slotCount;
                
            }

            StackFrame frame = null;

            if (startOffset + calledblock.totalRegisters+1+1 >= stackSlots.Length || !stackframePool.hasCacheObj())
            {
                //runtimeError = new error.InternalError(token, "stack overflow");
                if (callbacker != null)
                {
                    callbacker.noticeRunFailed();
                }
                //frame.close();
                //currentRunFrame.receiveErrorFromStackFrame(runtimeError);
                //runtimeError = null;

                currentRunFrame.receiveErrorFromStackFrame(new error.InternalError( swc, token, "stack overflow"));

                return null;
            }
            else
            {
                frame = stackframePool.create(calledblock);
                frame.codeLinePtr = 0;
                frame.player = this;
                frame.returnSlot = returnSlot;
                frame.callbacker = callbacker;
                frame.static_objects = static_instance;

                frame.offset = startOffset;
                frame.stack = stackSlots;

                frame._tempSlot1 = stackSlots[startOffset + frame.block.totalRegisters];
                frame._tempSlot2 = stackSlots[startOffset + frame.block.totalRegisters+1];
                runtimeStack.Push(frame);
                currentRunFrame = frame;

                var block = calledblock;
                for (int i = 0; i < block.regConvFromVar.Count; i++)
                {
                    Register regvar = block.regConvFromVar[i];
                    var slot = (StackSlot)regvar.getSlot(null, frame);
                    TypeConverter.setDefaultValueToStackSlot(
                        regvar.valueType,slot
                        ); 
                }

            }


			//if (
			//    ReferenceEquals(membersHeap, emptyMembers)
			//    && type == RunTimeScopeType.function
			//    &&
			//    this_pointer is rtObject
			//    &&
			//    (callerScope == null ||
			//    callerScope.scopeType != RunTimeScopeType.function)
			//    )
			//{
			//    frame.scope = ((rtObject)this_pointer).objScope; //callerScope;
			//}
			if (ReferenceEquals(membersHeap, emptyMembers) && type== RunTimeScopeType.function)
			{
				if (this_pointer is rtObject)
				{
					if (callerScope == null || callerScope.scopeType != RunTimeScopeType.function)
					{
						frame.scope = ((rtObject)this_pointer).objScope;
					}
					else
					{
						RunTimeScope scope;

						scope = new RunTimeScope(
							membersHeap, calledblock.id, callerScope
							,
							this_pointer,
							type
						);

						frame.scope = scope;
					}
				}
				else
				{
					if (callerScope == null)
					{
						RunTimeScope scope;

						scope = new RunTimeScope(
							membersHeap, calledblock.id, callerScope
							,
							this_pointer,
							type
						);

						frame.scope = scope;
					}
					else
					{
						frame.scope = callerScope;
					}
				}
			}
			else
			{
				RunTimeScope scope;

				scope = new RunTimeScope(
					membersHeap, calledblock.id, callerScope
					,
					this_pointer,
					type
				);

				frame.scope = scope;

			}
            //frame.scope_thispointer = this_pointer;
            
            return frame.scope;
        }


        internal int getRuntimeStackFlag()
        {
            return runtimeStack.Count;
        }

        /// <summary>
        /// 执行到当前代码块结束
        /// </summary>
        /// <returns></returns>
        internal bool step_toStackflag(int stackflag)
        {
            int f = stackflag;
            while (step())// && receive_error==null)
            {
                if (runtimeStack.Count == f)
                {
					break;
                    //return (receive_error==null);
                }
            }
            return (receive_error == null);

        }

		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token,out error.InternalError error)
		{
			return runFunction(function,thisObj, resultSlot, token,out error,null,null,null,null,null,null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1)
		{
			return runFunction(function, thisObj, resultSlot, token,out error, v1, null, null, null, null,null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1,RunTimeValueBase v2)
		{
			return runFunction(function, thisObj, resultSlot, token,out error, v1, v2, null, null, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token,out error.InternalError error, RunTimeValueBase v1, RunTimeValueBase v2,RunTimeValueBase v3)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, v1, v2, v3, null, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1, RunTimeValueBase v2, RunTimeValueBase v3,RunTimeValueBase v4)
		{
			return runFunction(function, thisObj, resultSlot, token, out error,v1, v2, v3, v4, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1, RunTimeValueBase v2, RunTimeValueBase v3, 
			RunTimeValueBase v4,RunTimeValueBase v5)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, v1, v2, v3, v4, v5, null);
		}
		
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT  resultSlot,SourceToken token,out error.InternalError error,
			RunTimeValueBase v1,
			RunTimeValueBase v2,
			RunTimeValueBase v3,
			RunTimeValueBase v4,
			RunTimeValueBase v5,
			RunTimeValueBase[] paraArgs
			)
		{
			var funcCaller = funcCallerPool.create(currentRunFrame,token);
			funcCaller.SetFunction(function);
			funcCaller.SetFunctionThis(thisObj);
			funcCaller.loadDefineFromFunction();
			
			if (!funcCaller.createParaScope()) { error = currentRunFrame.runtimeError==null?new error.InternalError(swc,token, "创建参数失败"):receive_error ; return false ; }
			#region pushparameter
			int c = 0;
			bool success;
			if (v1 != null)
			{
				funcCaller.pushParameter(v1,c,out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc,token, "创建参数失败") : receive_error;
					return false;
				}
				c++;
			}
			if (v2 != null)
			{
				funcCaller.pushParameter(v2, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc,token, "创建参数失败") : receive_error;
					return false;
				}
				c++;
			}
			if (v3 != null)
			{
				funcCaller.pushParameter(v3, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc,token, "创建参数失败") : receive_error;
					return false;
				}
				c++;
			}
			if (v4 != null)
			{
				funcCaller.pushParameter(v4, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc,token, "创建参数失败") : receive_error;
					return false;
				}
				c++;
			}
			if (v5 != null)
			{
				funcCaller.pushParameter(v5, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc,token, "创建参数失败") : receive_error;
					return false;
				}
				c++;
			}
			
			if (paraArgs != null)
			{
				for (int i = 0; i < paraArgs.Length; i++)
				{
					funcCaller.pushParameter(paraArgs[i], c, out success);
					if (!success)
					{
						error = currentRunFrame.runtimeError == null ? new error.InternalError(swc,token, "创建参数失败") : receive_error;
						return false;
					}
					c++;
				}
			}
			#endregion

			funcCaller.returnSlot = resultSlot;
			funcCaller._tempSlot = currentRunFrame._tempSlot2;
			return runFuncCaller(funcCaller,token,out error);
		}

		class runFuncResult
		{
			internal class ResultPool : PoolBase<runFuncResult>
			{
				public ResultPool() : base(256)
				{
				}
			}

			public bool isSuccess;
			public bool isEnd;
		}

		internal bool runFuncCaller(operators.FunctionCaller funcCaller, SourceToken token, out error.InternalError error)
		{
			if (funcCaller.callbacker != null)
			{
				throw new ArgumentException();
			}

			runFuncResult r = runFuncresultPool.create();
			r.isSuccess = false;r.isEnd = false;

			BlockCallBackBase cb = blockCallBackPool.create();
			cb.setCallBacker(runfuntionEnd);
			cb.setWhenFailed(runfuntionFailed);
			cb.cacheObjects[0] = r;
			
			funcCaller.callbacker = cb;
			

			try
			{
				funcCaller.call();
				while (!r.isEnd && step()) ;

				bool result = r.isSuccess;

				if (result)
				{
					error = null;
				}
				else
				{
					if (currentRunFrame == null)
					{
						error = runtimeError == null ? new error.InternalError(swc,token, "函数执行失败") : runtimeError;
					}
					else
					{
						error = receive_error == null ? new error.InternalError(swc,token, "函数执行失败") : receive_error;
					}
				}

				return result;
			}
			finally
			{
				runFuncresultPool.ret(r);
			
			}
		}

		private void runfuntionEnd(BlockCallBackBase sender, object args)
		{
			((runFuncResult)sender.cacheObjects[0]).isEnd = true;
			((runFuncResult)sender.cacheObjects[0]).isSuccess = true;
			sender.isSuccess = true;
		}
		private void runfuntionFailed(BlockCallBackBase sender, object args)
		{
			((runFuncResult)sender.cacheObjects[0]).isEnd = true;
			((runFuncResult)sender.cacheObjects[0]).isSuccess = false;
			sender.isSuccess = false;
		}

		private IBlockCallBack _tempcallbacker;

        private StackFrame currentRunFrame;
        public bool step()
        {
            if (runtimeError != null)
            {
                return false;
            }
            if (currentRunFrame == null)
            {
                return false;
            }

            if (receive_error != null)
            {
                var temp = receive_error;
                receive_error = null;
				
				currentRunFrame.receiveErrorFromStackFrame(temp);
				
				
                return true;
            }

            if (_tempcallbacker != null)
            {
                var temp = _tempcallbacker;
                _tempcallbacker = null;
                temp.call(temp.args);
                
                return true;
            }

            if (currentRunFrame.IsEnd()) //执行完成
            {
                if (currentRunFrame.callbacker != null)
                {
                    _tempcallbacker = currentRunFrame.callbacker;
                    currentRunFrame.callbacker = null;
                    
                }

                currentRunFrame.close(); stackframePool.ret(currentRunFrame);
                runtimeStack.Pop();

                if (runtimeStack.Count > 0)
                {
                    currentRunFrame = runtimeStack.Peek();
                }
                else
                {
                    currentRunFrame = null;
                }
                
                
            }
            else
            {
                currentRunFrame.step();
            }

            return true;
        }

        private error.InternalError receive_error;
        internal void exitStackFrameWithError(error.InternalError error,StackFrame raiseframe)
        {
            if (error.callStack == null) //收集调用栈
            {
                error.callStack = new Stack<FrameInfo>();
            }
            error.callStack.Push(raiseframe.getInfo()); 

            runtimeStack.Pop();

			

			raiseframe.close(); stackframePool.ret(raiseframe);

#if DEBUG

            if (!ReferenceEquals(currentRunFrame, raiseframe))
            {
                //currentRunFrame.close();
                throw new ASRunTimeException("");
            }
#endif

            //currentRunFrame.close();
            if (runtimeStack.Count > 0)
            {
                currentRunFrame = runtimeStack.Peek();
                receive_error = error;
            }
            else
            {
                currentRunFrame = null;
                runtimeError = error;
            }
        }

        internal void outPutErrorMessage(error.InternalError err)
        {
            if (infoOutput !=null)
            {

				infoOutput.Error("运行时错误");
				//Console.WriteLine("file :" + err.token.sourceFile);
				//Console.WriteLine("line :" + err.token.line + " ptr :" + err.token.ptr);

				infoOutput.Error( err.getErrorInfo());

     //           if (err.errorValue != null)
     //           {
     //               string errinfo= err.errorValue.ToString();
     //               if (err.errorValue.rtType > RunTimeDataType.unknown && swc.ErrorClass !=null)
     //               {
     //                   if (ClassMemberFinder.check_isinherits(err.errorValue, swc.ErrorClass.getRtType(), swc))
     //                   {
     //                       errinfo =
     //                           ((rtObject)err.errorValue).value.memberData[1].getValue().ToString()+" #"+
     //                           ((rtObject)err.errorValue).value.memberData[2].getValue().ToString()+" " +
     //                           ((rtObject)err.errorValue).value.memberData[0].getValue().ToString();
     //                   }
     //               }


					//infoOutput.Error("[故障] " + "信息=" + errinfo);
     //           }
     //           else
     //           {
					//infoOutput.Error(err.message);
     //           }

                Stack<FrameInfo> _temp = new Stack<FrameInfo>();

                while (err.callStack !=null && err.callStack.Count>0)
                {
                    _temp.Push(err.callStack.Pop());
                    displayStackFrame = _temp.Peek();
                }

                foreach (var item in _temp)
                {
                    if (item.codeLinePtr < item.block.opSteps.Count)
                    {
						infoOutput.Error(item.block.name + " at file:" + item.block.opSteps[item.codeLinePtr].token.sourceFile);
						infoOutput.Error("\t\t line:" + (item.block.opSteps[item.codeLinePtr].token.line+1 ) + " ptr:" + (item.block.opSteps[item.codeLinePtr].token.ptr+1));
                    }
                    else
                    {
						infoOutput.Error(item.block.name);
                    }

					infoOutput.Error("----");
                }


                
            }
        }


        StringBuilder sb = new StringBuilder();
        internal string stackTrace(int skipline)
        {
            foreach (var item in runtimeStack)
            {
                if (skipline > 0)
                { 
                    skipline--;
                    continue;
                }

                if (item.codeLinePtr < item.block.opSteps.Count)
                {
                    sb.Append("\tat ");
                    sb.Append(item.block.name);
                    sb.Append(" [");
                    sb.Append(item.block.opSteps[item.codeLinePtr].token.sourceFile);
                    sb.Append(" ");
                    sb.Append(item.block.opSteps[item.codeLinePtr].token.line+1);
                    sb.Append(" ptr:");
                    sb.Append(  item.block.opSteps[item.codeLinePtr].token.ptr+1);
                    sb.Append("]");
                    sb.AppendLine();
                }
                else
                {
                    sb.Append("\tat ");
                    sb.AppendLine(item.block.name);
                }

                
            }

            

            string t = sb.ToString();
            sb.Remove(0, sb.Length);
            return t;
        }





















		#region 外部接口

		private object convertReturnValue(object obj)
		{
			if (obj is RunTimeValueBase)
			{
				RunTimeValueBase rv = (RunTimeValueBase)obj;

				switch (rv.rtType)
				{
					case RunTimeDataType.rt_boolean:
						return TypeConverter.ConvertToBoolean(rv, null, null);
					case RunTimeDataType.rt_int:
						return TypeConverter.ConvertToInt(rv, null, null);
					case RunTimeDataType.rt_uint:
						return TypeConverter.ConvertToUInt(rv, null, null);
					case RunTimeDataType.rt_number:
						return TypeConverter.ConvertToNumber(rv);
					case RunTimeDataType.rt_string:
						return TypeConverter.ConvertToString(rv, null, null);
					default:
						return obj;
				}
			}
			else
			{
				return obj;
			}
		}

		#region getClass

		public ASBinCode.rtti.Class getClass(string name)
		{
			if (swc == null)
			{
				throw new InvalidOperationException("需要先加载代码");
			}

			return swc.getClassDefinitionByName(name);
		}

#endregion

		#region prepaeParameter

		private RunTimeValueBase prepareParameter(ASBinCode.rtti.FunctionSignature sig, int paraIndex, object value, StackSlot tempSLot)
		{
			RunTimeValueBase vb1 = null;
			try
			{

				if (sig.parameters.Count > paraIndex)
				{
					if (sig.parameters[paraIndex].isPara)
					{
						linktypemapper.storeLinkObject_ToSlot(value, RunTimeDataType.rt_void, tempSLot, swc, this);
						vb1 = tempSLot.getValue();
					}
					else
					{
						linktypemapper.storeLinkObject_ToSlot(value, sig.parameters[paraIndex].type, tempSLot, swc, this);
						vb1 = tempSLot.getValue();
					}
					return vb1;

				}
				else
				{
					linktypemapper.storeLinkObject_ToSlot(value, RunTimeDataType.rt_void, tempSLot, swc, this);
					vb1 = tempSLot.getValue();
					return vb1;
				}
			}
			catch (KeyNotFoundException e)
			{
				throw new ASRunTimeException("构造函数参数转换失败", e);
			}
			catch (ArgumentException e)
			{
				throw new ASRunTimeException("构造函数参数转换失败", e);
			}
			catch (InvalidCastException e)
			{
				throw new ASRunTimeException("构造函数参数转换失败", e);
			}
			catch (IndexOutOfRangeException e)
			{
				throw new ASRunTimeException("构造函数参数转换失败", e);
			}
		}

		#endregion

		#region createInstance

		public ASBinCode.rtData.rtObject createInstance(string classname)
		{
			return createInstance(classname, 0, null, null, null, null);
		}

		public ASBinCode.rtData.rtObject createInstance(string classname, object v1)
		{
			return createInstance(classname, 1, v1, null, null, null);
		}

		public ASBinCode.rtData.rtObject createInstance(string classname, object v1, object v2)
		{
			return createInstance(classname, 2, v1, v2, null, null);
		}

		public ASBinCode.rtData.rtObject createInstance(string classname, int argcount, object v1, object v2, object v3, params object[] args)
		{
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");

			try
			{
				initPlayer();

				var cls = getClass(classname);
				if (cls == null)
				{
					throw new ASRunTimeException(classname + "类型未找到");
				}

				CallBlankBlock(null);

				if (!operators.InstanceCreator.init_static_class(cls, this, new SourceToken(0, 0, string.Empty)))
				{
					throw new ASRunTimeException("初始化静态实例时失败");
				}

				var sig = swc.functions[cls.constructor_functionid].signature;
				RunTimeValueBase vb1 = null;
				RunTimeValueBase vb2 = null;
				RunTimeValueBase vb3 = null;

				RunTimeValueBase[] paraArgs = null;

				currentRunFrame.call_parameter_slotCount += argcount;
				int slotidx = currentRunFrame.offset + currentRunFrame.block.totalRegisters + 1 + 1;
				int stslotidx = slotidx;
				if (argcount > 0)
				{
					vb1 = prepareParameter(sig, 0, v1, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 1)
				{
					vb2 = prepareParameter(sig, 1, v2, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 2)
				{
					vb3 = prepareParameter(sig, 2, v3, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 3)
				{
					paraArgs = new RunTimeValueBase[argcount - 3];
					for (int i = 0; i < paraArgs.Length; i++)
					{
						paraArgs[i] = prepareParameter(sig, i + 3, args[i], stackSlots[slotidx]);
						slotidx++;
					}
				}


				error.InternalError err;
				bool issuccess = runFunction(_createinstance, _buildin_class_, currentRunFrame._tempSlot1, new SourceToken(0, 0, string.Empty), out err,
					static_instance[cls.staticClass.classid],
					new rtInt(argcount), vb1, vb2, vb3, paraArgs);

				currentRunFrame.call_parameter_slotCount = 0;
				for (int i = stslotidx; i < slotidx; i++)
				{
					stackSlots[slotidx].clear();
				}

				if (!issuccess)
				{
					while (step())
					{

					}
					if (err != null)
					{
						throw new ASRunTimeException(err.getErrorInfo());
					}
					else
					{
						throw new ASRunTimeException("对象创建失败");
					}
				}
				else
				{
					var v = currentRunFrame._tempSlot1.getValue().Clone();

					while (step())
					{

					}

					if (err != null)
					{
						throw new ASRunTimeException(err.message);
					}

					return v as rtObject;
				}

			}
			finally
			{
				clearEnv();
			}
		}

		#endregion

		#region getMethod

		private rtFunction getMethod(rtObject thisObj, string name)
		{
			//if (currentRunFrame != null)
			//	throw new InvalidOperationException("状态异常,不能在运行中调用此方法");
			if (thisObj == null)
			{
				throw new ArgumentNullException("thisObj");
			}

			var member = thisObj.value._class.classMembers.FindByName(name);
			if (member == null)
			{
				return null;
			}
			else
			{
				if (member.bindField is MethodGetterBase)
				{
					rtFunction method = (rtFunction)((MethodGetterBase)member.bindField).getMethod(thisObj);

					var result = (rtFunction)method.Clone();
					method.Clear();

					return result;
				}
				else
				{
					return null;
				}
			}

			//	try
			//	{
			//		initPlayer();
			//		CallBlankBlock(null);

			//		error.InternalError err;
			//		bool issuccess =
			//			runFunction(_getMethod, _buildin_class_, currentRunFrame._tempSlot1, new SourceToken(0, 0, string.Empty), out err,
			//				thisObj,
			//				new rtString(name));

			//		if (!issuccess)
			//		{
			//			while (step())
			//			{

			//			}
			//			return null;
			//		}
			//		else
			//		{
			//			var v = currentRunFrame._tempSlot1.getValue().Clone();

			//			while (step())
			//			{

			//			}

			//			if (err != null)
			//				return null;

			//			return v as rtFunction;
			//		}

			//	}
			//	finally
			//	{
			//		clearEnv();
			//	}
			//}
		}
		#endregion


		#region invokeMethod
		public object invokeMethod(string type, string methodname)
		{
			return invokeMethod(type, methodname, 0, null, null, null, null, null, null);
		}
		public object invokeMethod(string type, string methodname,object v1)
		{
			return invokeMethod(type, methodname, 1, v1, null, null, null, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1,object v2)
		{
			return invokeMethod(type, methodname, 2, v1, v2, null, null, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1, object v2,object v3)
		{
			return invokeMethod(type, methodname, 3, v1, v2, v3, null, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1, object v2,object v3,object v4)
		{
			return invokeMethod(type, methodname, 4, v1, v2, v3, v4, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1, object v2, object v3, object v4,object v5)
		{
			return invokeMethod(type, methodname, 5, v1, v2, v3, v4, v5, null);
		}
		public object invokeMethod(string type, string methodname, int argcount, object v1, object v2, object v3, object v4, object v5, params object[] args)
		{
			var cls = getClassStaticInstance(type);
			return invokeMethod(cls, methodname, argcount, v1, v2, v3, v4, v5, args);
		}


		public object invokeMethod(rtObject thisObj, string methodname)
		{
			return invokeMethod(thisObj, methodname, 0, null, null, null, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, string methodname, object v1)
		{
			return invokeMethod(thisObj, methodname, 1, v1, null, null, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, string methodname, object v1, object v2)
		{
			return invokeMethod(thisObj, methodname, 2, v1, v2, null, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, string methodname, object v1, object v2, object v3)
		{
			return invokeMethod(thisObj, methodname, 3, v1, v2, v3, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, string methodname, object v1, object v2, object v3, object v4)
		{
			return invokeMethod(thisObj, methodname, 4, v1, v2, v3, v4, null, null);
		}
		public object invokeMethod(rtObject thisObj, string methodname, object v1, object v2, object v3, object v4, object v5)
		{
			return invokeMethod(thisObj, methodname, 5, v1, v2, v3, v4, v5, null);
		}

		public object invokeMethod(rtObject thisObj, string methodname, int argcount, object v1, object v2, object v3, object v4, object v5, params object[] args)
		{
			var method = getMethod(thisObj, methodname);
			if (method == null)
				throw new ASRunTimeException("方法未找到");

			return invokeMethod(thisObj, method, argcount, v1, v2, v3, v4, v5, args);
		}


		public object invokeMethod(rtObject thisObj, rtFunction method)
		{
			return invokeMethod(thisObj, method, 0, null, null, null, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, rtFunction method, object v1)
		{
			return invokeMethod(thisObj, method, 1, v1, null, null, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, rtFunction method, object v1, object v2)
		{
			return invokeMethod(thisObj, method, 2, v1, v2, null, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, rtFunction method, object v1, object v2, object v3)
		{
			return invokeMethod(thisObj, method, 3, v1, v2, v3, null, null, null);
		}
		public object invokeMethod(rtObject thisObj, rtFunction method, object v1, object v2, object v3, object v4)
		{
			return invokeMethod(thisObj, method, 4, v1, v2, v3, v4, null, null);
		}
		public object invokeMethod(rtObject thisObj, rtFunction method, object v1, object v2, object v3, object v4, object v5)
		{
			return invokeMethod(thisObj, method, 5, v1, v2, v3, v4, v5, null);
		}
		public object invokeMethod(rtObject thisObj, rtFunction method, int argcount, object v1, object v2, object v3, object v4, object v5, params object[] args)
		{
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");

			try
			{
				initPlayer();
				CallBlankBlock(null);

				var signature = swc.functions[method.functionId].signature;

				RunTimeValueBase vb1 = null;
				RunTimeValueBase vb2 = null;
				RunTimeValueBase vb3 = null;
				RunTimeValueBase vb4 = null;
				RunTimeValueBase vb5 = null;

				RunTimeValueBase[] paraArgs = null;

				currentRunFrame.call_parameter_slotCount += argcount;

				int slotidx = currentRunFrame.offset + currentRunFrame.block.totalRegisters + 1 + 1;
				int stslotidx = slotidx;
				if (argcount > 0)
				{
					vb1 = prepareParameter(signature, 0, v1, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 1)
				{
					vb2 = prepareParameter(signature, 1, v2, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 2)
				{
					vb3 = prepareParameter(signature, 2, v3, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 3)
				{
					vb4 = prepareParameter(signature, 3, v4, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 4)
				{
					vb5 = prepareParameter(signature, 4, v5, stackSlots[slotidx]);
					slotidx++;
				}
				if (argcount > 5)
				{
					paraArgs = new RunTimeValueBase[argcount - 5];
					for (int i = 0; i < paraArgs.Length; i++)
					{
						paraArgs[i] = prepareParameter(signature, i + 5, args[i], stackSlots[slotidx]);
						slotidx++;
					}
				}


				error.InternalError err;
				RunTimeValueBase v = null;
				bool issuccess = runFunction(method, thisObj, currentRunFrame._tempSlot1, new SourceToken(0, 0, string.Empty), out err
					, vb1, vb2, vb3, vb4, vb5, paraArgs
					);

				currentRunFrame.call_parameter_slotCount = 0;
				for (int i = stslotidx; i < slotidx; i++)
				{
					stackSlots[slotidx].clear();
				}

				if (issuccess)
				{
					v = (RunTimeValueBase)currentRunFrame._tempSlot1.getValue().Clone();
					while (step())
					{

					}
					if (err != null)
					{
						throw new ASRunTimeException(err.message);
					}
				}
				else
				{
					while (step())
					{

					}
					if (err != null)
					{
						throw new ASRunTimeException(err.getErrorInfo());
					}
					else
					{
						throw new ASRunTimeException("方法调用失败");
					}
				}

				if (signature.returnType == RunTimeDataType.fun_void)
				{
					return rtUndefined.undefined;
				}

				object obj;
				if (linktypemapper.rtValueToLinkObject(v, linktypemapper.getLinkType(signature.returnType), swc, true, out obj))
				{
					return convertReturnValue(obj);
				}
				else
				{
					throw new ASRunTimeException("返回值转化失败");
				}
			}
			finally
			{
				clearEnv();
			}
		}


		#endregion

		#region getClassStaticInstance
		private rtObject getClassStaticInstance(string type)
		{
			
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");

			try
			{
				initPlayer();

				var cls = getClass(type);
				if (cls == null)
				{
					throw new ASRunTimeException(type + "类型未找到");
				}

				CallBlankBlock(null);

				if (!operators.InstanceCreator.init_static_class(cls, this, new SourceToken(0, 0, string.Empty)))
				{
					throw new ASRunTimeException("初始化静态实例时失败");
				}
				while (step()) ;
				return static_instance[cls.staticClass.classid];

			}
			finally
			{
				clearEnv();
			}

		}
		#endregion

		#region get_set_member

		public object getMemberValue(rtObject thisObj, string memberPath)
		{
			return getMemberValue(thisObj, memberPath,null);
		}
		/// <summary>
		/// 访问成员的值
		/// </summary>
		/// <param name="thisObj"></param>
		/// <param name="memberPath"></param>
		/// <param name="indexArgs">如果是需要用方括号访问的成员，则输入方括号内的参数</param>
		/// <returns></returns>
		public object getMemberValue(rtObject thisObj,string memberPath,object indexArgs)
		{
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");

			try
			{
				initPlayer();
				CallBlankBlock(null);

				string[] path = null;
				if (memberPath != null)
				{
					memberPath = memberPath.Trim();
					path=memberPath.Split('.');
				}
				var signature = swc.functions[_getMemberValue.functionId].signature;

				RunTimeValueBase p1 = null;
				RunTimeValueBase p2 = null;
				rtArray extpath = null;
				RunTimeValueBase index = null;

				if (path.Length > 0)
				{
					p1 = new rtString(path[0]);
				}
				if (path.Length > 1)
				{
					p2 = new rtString(path[1]);
				}
				if (path.Length > 2)
				{
					extpath = new rtArray();
					for (int i = 2; i < path.Length; i++)
					{
						extpath.innerArray.Add(new rtString(path[i]));
					}
				}

				if (indexArgs != null)
				{
					index = prepareParameter(signature, 4, index, currentRunFrame._tempSlot1);
				}

				error.InternalError err;
				RunTimeValueBase v = null;
				bool issuccess = runFunction(_getMemberValue, _getMemberValue.this_pointer, currentRunFrame._tempSlot2, new SourceToken(0, 0, string.Empty), out err
					, thisObj, p1, p2, extpath, index);

				if (issuccess)
				{
					v = (RunTimeValueBase)currentRunFrame._tempSlot2.getValue().Clone();
					while (step())
					{

					}
					if (err != null)
					{
						throw new ASRunTimeException(err.message);
					}
				}
				else
				{
					while (step())
					{

					}
					if (err != null)
					{
						throw new ASRunTimeException(err.getErrorInfo());
					}
					else
					{
						throw new ASRunTimeException("成员访问失败");
					}
				}
				object obj;
				if (linktypemapper.rtValueToLinkObject(v, linktypemapper.getLinkType(signature.returnType), swc, true, out obj))
				{
					return convertReturnValue(obj);
				}
				else
				{
					throw new ASRunTimeException("成员获取返回值转化失败");
				}
			}
			finally
			{
				clearEnv();
			}

		}

		public void setMemberValue(rtObject thisObj, string memberPath, object value)
		{
			setMemberValue(thisObj, memberPath, value, null);
		}
		/// <summary>
		/// 设置成员的值
		/// </summary>
		/// <param name="thisObj"></param>
		/// <param name="memberPath"></param>
		/// <param name="value"></param>
		/// <param name="indexArgs">如果是需要用方括号访问的成员，则输入方括号内的参数</param>
		/// <returns></returns>
		public void setMemberValue(rtObject thisObj, string memberPath, object value, object indexArgs)
		{
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");

			try
			{
				initPlayer();
				CallBlankBlock(null);

				string[] path = null;
				if (memberPath != null)
				{
					memberPath = memberPath.Trim();
					path = memberPath.Split('.');
				}
				var signature = swc.functions[_setMemberValue.functionId].signature;

				RunTimeValueBase setvalue = null;
				setvalue = prepareParameter(signature, 1, value, currentRunFrame._tempSlot2);

				RunTimeValueBase p1 = null;
				rtArray extpath = null;
				RunTimeValueBase index = null;

				if (path.Length > 0)
				{
					p1 = new rtString(path[0]);
				}
				if (path.Length > 1)
				{
					extpath = new rtArray();
					for (int i = 1; i < path.Length; i++)
					{
						extpath.innerArray.Add(new rtString(path[i]));
					}
				}

				if (indexArgs != null)
				{
					index = prepareParameter(signature, 5, index, currentRunFrame._tempSlot1);
				}

				error.InternalError err;
				
				bool issuccess = runFunction(_setMemberValue, _setMemberValue.this_pointer, currentRunFrame._tempSlot2, new SourceToken(0, 0, string.Empty), out err
					, thisObj,setvalue, p1, extpath, index);

				if (issuccess)
				{
					
					while (step())
					{

					}
					if (err != null)
					{
						throw new ASRunTimeException(err.message);
					}
				}
				else
				{
					while (step())
					{

					}
					if (err != null)
					{
						throw new ASRunTimeException(err.getErrorInfo());
					}
					else
					{
						throw new ASRunTimeException("成员赋值失败");
					}
				}
				
			}
			finally
			{
				clearEnv();
			}
		}


		public object getMemberValue(string type, string memberPath)
		{
			return getMemberValue(type, memberPath, null);
		}

		/// <summary>
		/// 访问静态成员的值
		/// </summary>
		/// <param name="type"></param>
		/// <param name="memberPath"></param>
		/// <param name="indexArgs"></param>
		/// <returns></returns>
		public object getMemberValue(string type, string memberPath, object indexArgs)
		{
			var clsObj = getClassStaticInstance(type);
			return getMemberValue(clsObj, memberPath, indexArgs);

		}

		public void setMemberValue(string type, string memberPath, object value)
		{
			setMemberValue(type, memberPath, value,null);
		}

		/// <summary>
		/// 设置静态成员的值
		/// </summary>
		/// <param name="type"></param>
		/// <param name="memberPath"></param>
		/// <param name="value"></param>
		/// <param name="indexArgs"></param>
		public void setMemberValue(string type,string memberPath,object value,object indexArgs)
		{
			var clsObj = getClassStaticInstance(type);
			setMemberValue(clsObj, memberPath, value, indexArgs);
		}

		#endregion


		#region ByteArray

		/// <summary>
		/// 创建一个ByteArray对象
		/// </summary>
		/// <param name="byteArray"></param>
		/// <returns></returns>
		public rtObject createByteArrayObject(out flash.utils.ByteArray byteArray)
		{
			var thisObj = createInstance("flash.utils.ByteArray");
			byteArray =
					(flash.utils.ByteArray)((ASBinCode.rtti.HostedObject)((rtObject)(((rtObject)thisObj).value.memberData[0].getValue())).value).hosted_object;

			return thisObj;
		}


		#endregion




		#endregion







	}
}
