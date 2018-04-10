using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASRuntime
{
	public class Player
	{
		internal const int STACKSLOTLENGTH = 512;

		internal IRuntimeOutput infoOutput;

		internal Dictionary<int, rtObjectBase> static_instance;
		internal Dictionary<int, RunTimeScope> outpackage_runtimescope;

		private rtObjectBase _buildin_class_;
		private rtFunction _getMethod;
		private rtFunction _createinstance;
		private rtFunction _getMemberValue;
		private rtFunction _setMemberValue;

		public RuntimeLinkTypeMapper linktypemapper;


		/// <summary>
		/// 内存缓存number
		/// </summary>
		private double[] memnumber;
		/// <summary>
		/// 内存缓存int
		/// </summary>
		private int[] memint;

#if WHENDEV
		internal CSWC swc;
#else
		public CSWC swc;
#endif
		private CodeBlock defaultblock;
		public void loadCode(CSWC swc, CodeBlock block = null)
		{
			this.swc = swc;

			memnumber = new double[swc.MaxMemNumberCount];
			memint = new int[swc.MaxMemIntCount];
			
			foreach (var m in swc.MemRegList)
			{
				m.setMemCache_Number(memnumber);
				m.setMemCache_Int(memint);
			}
			
			if (swc.NativeFunctionCount == 0)
			{
				ASRuntime.nativefuncs.BuildInFunctionLoader.loadBuildInFunctions(swc);
			}

			static_instance = new Dictionary<int, rtObjectBase>();
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
						defaultblock = new CodeBlock(int.MaxValue, "_player_run", -65535, true);
						defaultblock.scope = new ASBinCode.scopes.StartUpBlockScope();
						defaultblock.totalStackSlots = 1;

						{
							OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_argement, SourceToken.Empty);
							opMakeArgs.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(swc.classes[i].classid));
							opMakeArgs.arg1Type = RunTimeDataType.rt_int;
							defaultblock.opSteps.Add(opMakeArgs);

						}
						{
							OpStep step = new OpStep(OpCode.new_instance, SourceToken.Empty);
							step.arg1 = new RightValue(new rtInt(swc.classes[i].classid));
							step.arg1Type = swc.classes[i].getRtType();
							step.reg = new StackSlotAccessor(0, ushort.MaxValue);
							step.regType = swc.classes[i].getRtType();

							defaultblock.opSteps.Add(step);
						}
						{
							defaultblock.instructions = defaultblock.opSteps.ToArray();
							defaultblock.opSteps = null;
						}
						break;
					}
				}
				if (defaultblock == null)
				{
					//***查找第一个类的包外代码
					for (int i = swc.classes.Count - 1; i > 0; i--)
					{
						if (swc.classes[i].staticClass != null)
						{
							defaultblock = swc.blocks[swc.classes[i].outscopeblockid];
							break;
						}
					}
				}

				if (defaultblock == null && swc.blocks.Count > 0)
				{
					defaultblock = swc.blocks[0];
				}

			}

		}

		private bool _hasInitStack = false;
		private bool _hasInitBaseCode = false;

		/// <summary>
		/// 当调用本地函数时，会自动设置这个字段，如果本地函数抛出异常并被try catch到后，会根据这个字段继续后续操作
		/// </summary>
		internal operators.FunctionCaller _nativefuncCaller;

		/// <summary>
		/// 当调用本地函数NativeConstParameterFunction类型时，会先从NativeConstParameterFunction申请一个令牌，然后对此赋值。
		/// 如此本地函数可根据此令牌判断是否是从Player调用,以确定是否需要调用重写版本
		/// </summary>
		internal nativefuncs.NativeConstParameterFunction.ExecuteToken _executeToken;
		public nativefuncs.NativeConstParameterFunction.ExecuteToken ExecuteToken
		{
			get { return _executeToken; }
		}


		private error.InternalError runtimeError;


		/// <summary>
		/// 调用堆栈
		/// </summary>
		private MyStack runtimeStack;
		internal StackSlot[] stackSlots;
		private FrameInfo displayStackFrame;

		internal StackFrame.StackFramePool stackframePool;
		internal operators.FunctionCaller.FunctionCallerPool funcCallerPool;
		internal BlockCallBackBase.BlockCallBackBasePool blockCallBackPool;
		private runFuncResult.ResultPool runFuncresultPool;

		public Player(IRuntimeOutput output)
		{
			infoOutput = output;
		}
		public Player() : this(new ConsoleOutput()) { }

		private void clearEnv()
		{
			for (int i = 0; i < STACKSLOTLENGTH; i++)
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

			_nativefuncCaller = null;
			_executeToken = nativefuncs.NativeConstParameterFunction.ExecuteToken.nulltoken;
		}

		private void initPlayer()
		{
			if (!_hasInitStack)
			{
				
				stackSlots = new StackSlot[STACKSLOTLENGTH];
				for (int i = 0; i < STACKSLOTLENGTH; i++)
				{
					stackSlots[i] = new StackSlot(swc, this);
				}
				StackLinkObjectCache lobjcache = new StackLinkObjectCache(swc, this);
				stackSlots[0]._linkObjCache = lobjcache;
				for (int i = 1; i < STACKSLOTLENGTH; i++)
				{
					stackSlots[i]._linkObjCache = lobjcache.Clone();
				}


				

				


				stackframePool = new StackFrame.StackFramePool(this, stackSlots, memnumber, memint);
				funcCallerPool = new operators.FunctionCaller.FunctionCallerPool(this);
				blockCallBackPool = new BlockCallBackBase.BlockCallBackBasePool(this);
				runFuncresultPool = new runFuncResult.ResultPool();
				runtimeStack = new MyStack(stackframePool.maxcount);

				_hasInitStack = true;
			}
			if (!_hasInitBaseCode)
			{
				if (swc.ErrorClass != null)
				{
					//***先执行必要代码初始化****
					var block = swc.blocks[swc.ErrorClass.outscopeblockid];
					HeapSlot[] initdata = genHeapFromCodeBlock(block);
					callBlock(block, initdata, new StackSlot(swc, this), null,
						SourceToken.Empty, null,
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
			lock (this)
			{
				initPlayer();

				HeapSlot[] data = genHeapFromCodeBlock(defaultblock);
				return run2(defaultblock, data, result);
			}
		}


		private RunTimeValueBase run2(CodeBlock runblock, HeapSlot[] blockMemberHeap, RightValueBase result)
		{

			var topscope = callBlock(runblock, blockMemberHeap, new StackSlot(swc, this), null,
				SourceToken.Empty, null,
				null, RunTimeScopeType.startup
				);
			displayStackFrame = runtimeStack.Peek().getInfo();

			try
			{

				while (true)
				{
#if DEBUG
					while (step())
					{

					}
					break;
#else

					try
					{
						bool isstep = true;
						while (isstep)
						{
							isstep = false;
							if (runtimeError != null)
							{
								isstep= false;continue;
							}
							if (currentRunFrame == null)
							{
								isstep= false;continue;
							}

							if (receive_error != null)
							{
								var temp = receive_error;
								receive_error = null;

								currentRunFrame.receiveErrorFromStackFrame(temp);


								isstep= true;continue;
							}

							if (_tempcallbacker != null)
							{
								var temp = _tempcallbacker;
								_tempcallbacker = null;
								temp.call(temp.args);

								isstep= true;continue;
							}

							if (currentRunFrame.codeLinePtr >= currentRunFrame.stepCount ) //执行完成
							{
								if (currentRunFrame.callbacker != null)
								{
									_tempcallbacker = currentRunFrame.callbacker;
									currentRunFrame.callbacker = null;

								}
#if DEBUG
								currentRunFrame.close();
								
#else
								//人肉内联close代码
								{
									currentRunFrame.isclosed = true;
									int bottomidx = currentRunFrame.baseBottomSlotIndex;
									for (int i = currentRunFrame.offset; i < bottomidx; i++)
									{
										StackSlot slot = stackSlots[i];
										if (slot.refPropChanged)
										{
											slot.refPropChanged = false;
											slot.stackObjects = StackSlot.StackObjects.EMPTY;

											if (slot.needclear)
											{
												slot.linktarget = null;
												slot._cache_arraySlot.clear();
												slot._cache_vectorSlot.clear();
												slot._cache_prototypeSlot.clear();
												slot._cache_setthisslot.clear();
												slot._linkObjCache.clearRefObj();
												slot._functionValue.Clear();
												slot._functon_result.Clear();
												slot.needclear = false;
											}


											slot.store[StackSlot.COMMREFTYPEOBJ] = rtNull.nullptr;
											
										}
										slot.index = (int)RunTimeDataType.unknown;
									}


									currentRunFrame.scope = null;
									currentRunFrame.typeconvertoperator = null;
									currentRunFrame.funCaller = null;
									currentRunFrame.deActiveInstanceCreator();
									currentRunFrame.returnSlot = null;
									currentRunFrame.callbacker = null;
									currentRunFrame.holdedError = null;


									currentRunFrame.tryCatchState.Clear();
									currentRunFrame.runtimeError = null;
									currentRunFrame.hascallstep = false;

								}

#endif



								stackframePool.ret(currentRunFrame);
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
								//currentRunFrame.step();
					#region 人肉内联

								var block = currentRunFrame.block;

								var scope = currentRunFrame.scope;

								currentRunFrame.hascallstep = true;

								OpStep step = block.instructions[currentRunFrame.codeLinePtr];
								//exec(step);
								switch (step.opCode)
								{
									case OpCode.cast:
										operators.OpCast.execCast(currentRunFrame, step, scope);
										break;
									case OpCode.cast_primitive:
										operators.OpCast.exec_CastPrimitive(currentRunFrame, step, scope);
										break;
									case OpCode.assigning:
										operators.OpAssigning.execAssigning(currentRunFrame, step, scope);
										break;

									case OpCode.add_number:
										operators.OpAdd.execAdd_Number(currentRunFrame, step, scope);
										break;
									case OpCode.add_string:
										operators.OpAdd.execAdd_String(currentRunFrame, step, scope);
										break;
									case OpCode.add:
										operators.OpAdd.execAdd(currentRunFrame, step, scope);
										break;
									case OpCode.sub_number:
										operators.OpSub.execSub_Number(currentRunFrame, step, scope);
										break;
									case OpCode.sub:
										operators.OpSub.execSub(currentRunFrame, step, scope);
										break;
									case OpCode.multi:
										operators.OpMulti.execMulti(currentRunFrame, step, scope);
										break;
									case OpCode.multi_number:
										operators.OpMulti.exec_MultiNumber(currentRunFrame, step, scope);
										break;
									case OpCode.div:
										operators.OpMulti.execDiv(currentRunFrame, step, scope);
										break;
									case OpCode.div_number:
										operators.OpMulti.exec_DivNumber(currentRunFrame, step, scope);
										break;
									case OpCode.mod:
										operators.OpMulti.execMod(currentRunFrame, step, scope);
										break;
									case OpCode.mod_number:
										operators.OpMulti.exec_ModNumber(currentRunFrame, step, scope);
										break;
									case OpCode.unary_plus:
										operators.OpUnaryPlus.execUnaryPlus(currentRunFrame, step, scope);
										break;
									case OpCode.neg:
										operators.OpNeg.execNeg(currentRunFrame, step, scope);
										break;
									case OpCode.gt_num:
										operators.OpLogic.execGT_NUM(currentRunFrame, step, scope);
										break;
									case OpCode.gt_void:
										operators.OpLogic.execGT_Void(currentRunFrame, step, scope);
										break;
									case OpCode.lt_num:
										operators.OpLogic.execLT_NUM(currentRunFrame, step, scope);
										break;
									case OpCode.lt_void:
										operators.OpLogic.execLT_VOID(currentRunFrame, step, scope);
										break;
									case OpCode.ge_num:
										operators.OpLogic.execGE_NUM(currentRunFrame, step, scope);
										break;
									case OpCode.ge_void:
										operators.OpLogic.execGE_Void(currentRunFrame, step, scope);
										break;
									case OpCode.le_num:
										operators.OpLogic.execLE_NUM(currentRunFrame, step, scope);
										break;
									case OpCode.le_void:
										operators.OpLogic.execLE_VOID(currentRunFrame, step, scope);
										break;
									case OpCode.equality:
										operators.OpLogic.execEQ(currentRunFrame, step, scope);
										break;
									case OpCode.not_equality:
										operators.OpLogic.execNotEQ(currentRunFrame, step, scope);
										break;
									case OpCode.equality_num_num:
										operators.OpLogic.execEQ_NumNum(currentRunFrame, step, scope);
										break;
									case OpCode.not_equality_num_num:
										operators.OpLogic.execNotEQ_NumNum(currentRunFrame, step, scope);
										break;
									case OpCode.equality_str_str:
										operators.OpLogic.execEQ_StrStr(currentRunFrame, step, scope);
										break;
									case OpCode.not_equality_str_str:
										operators.OpLogic.execNotEQ_StrStr(currentRunFrame, step, scope);
										break;
									case OpCode.strict_equality:
										operators.OpLogic.execStrictEQ(currentRunFrame, step, scope);
										break;
									case OpCode.not_strict_equality:
										operators.OpLogic.execStrictNotEQ(currentRunFrame, step, scope);
										break;
									case OpCode.logic_not:
										operators.OpLogic.execNOT(currentRunFrame, step, scope);
										break;
									case OpCode.bitAnd:
										operators.OpBit.execBitAnd(currentRunFrame, step, scope);
										break;
									case OpCode.bitOr:
										operators.OpBit.execBitOR(currentRunFrame, step, scope);
										break;
									case OpCode.bitXOR:
										operators.OpBit.execBitXOR(currentRunFrame, step, scope);
										break;
									case OpCode.bitNot:
										operators.OpBit.execBitNot(currentRunFrame, step, scope);
										break;
									case OpCode.bitLeftShift:
										operators.OpBit.execBitLeftShift(currentRunFrame, step, scope);
										break;
									case OpCode.bitRightShift:
										operators.OpBit.execBitRightShift(currentRunFrame, step, scope);
										break;
									case OpCode.bitUnsignedRightShift:
										operators.OpBit.execBitUnSignRightShift(currentRunFrame, step, scope);
										break;
									case OpCode.increment:
										operators.OpIncrementDecrement.execIncrement(currentRunFrame, step, scope);
										break;
									case OpCode.increment_int:
										operators.OpIncrementDecrement.execIncInt(currentRunFrame, step, scope);
										break;
									case OpCode.increment_uint:
										operators.OpIncrementDecrement.execIncUInt(currentRunFrame, step, scope);
										break;
									case OpCode.increment_number:
										operators.OpIncrementDecrement.execIncNumber(currentRunFrame, step, scope);
										break;
									case OpCode.decrement:
										operators.OpIncrementDecrement.execDecrement(currentRunFrame, step, scope);
										break;

									case OpCode.decrement_int:
										operators.OpIncrementDecrement.execDecInt(currentRunFrame, step, scope);
										break;
									case OpCode.decrement_uint:
										operators.OpIncrementDecrement.execDecUInt(currentRunFrame, step, scope);
										break;
									case OpCode.decrement_number:
										operators.OpIncrementDecrement.execDecNumber(currentRunFrame, step, scope);
										break;

									case OpCode.suffix_inc:
										operators.OpIncrementDecrement.execSuffixInc(currentRunFrame, step, scope);
										break;
									case OpCode.suffix_inc_int:
										operators.OpIncrementDecrement.execSuffixIncInt(currentRunFrame, step, scope);
										break;
									case OpCode.suffix_inc_uint:
										operators.OpIncrementDecrement.execSuffixIncUint(currentRunFrame, step, scope);
										break;
									case OpCode.suffix_inc_number:
										operators.OpIncrementDecrement.execSuffixIncNumber(currentRunFrame, step, scope);
										break;
									case OpCode.suffix_dec:
										operators.OpIncrementDecrement.execSuffixDec(currentRunFrame, step, scope);
										break;
									case OpCode.suffix_dec_int:
										operators.OpIncrementDecrement.execSuffixDecInt(currentRunFrame, step, scope);
										break;
									case OpCode.suffix_dec_uint:
										operators.OpIncrementDecrement.execSuffixDecUInt(currentRunFrame, step, scope);
										break;
									case OpCode.suffix_dec_number:
										operators.OpIncrementDecrement.execSuffixDecNumber(currentRunFrame, step, scope);
										break;
									case OpCode.flag:
										//标签行，不做任何操作
										currentRunFrame.endStepNoError();
										break;
									case OpCode.if_jmp:
										{
											if (((rtBoolean)step.arg1.getValue(scope, currentRunFrame)).value)//ReferenceEquals(ASBinCode.rtData.rtBoolean.True, step.arg1.getValue(scope)))
											{
												currentRunFrame.hasCallJump = true;
												currentRunFrame.jumptoline = currentRunFrame.codeLinePtr + step.jumoffset;
												currentRunFrame.endStep(step);
												break;
											}
											else
											{
												++currentRunFrame.codeLinePtr; //currentRunFrame.endStepNoError(); //.endStep(step);
											}
										}
										break;
									case OpCode.if_jmp_notry:
										{
											if (((rtBoolean)step.arg1.getValue(scope, currentRunFrame)).value)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;
											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.jmp:
										currentRunFrame.hasCallJump = true;
										currentRunFrame.jumptoline = currentRunFrame.codeLinePtr + step.jumoffset;
										currentRunFrame.endStep(step);
										break;
									case OpCode.jmp_notry:
										{
											currentRunFrame.codeLinePtr += step.jumoffset + 1;
											break;
										}
									case OpCode.raise_error:
										nativefuncs.Throw.exec(currentRunFrame, step, scope);
										break;
									case OpCode.enter_try:
										{
											int tryid = ((rtInt)step.arg1.getValue(scope, currentRunFrame)).value;
											currentRunFrame.enter_try(tryid);

											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.quit_try:
										{
											int tryid = ((rtInt)step.arg1.getValue(scope,currentRunFrame)).value;
											currentRunFrame.quit_try(tryid, step.token);

											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.enter_catch:
										{
											int catchid = ((rtInt)step.arg1.getValue(scope, currentRunFrame)).value;
											currentRunFrame.enter_catch(catchid);

											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.quit_catch:
										{
											int catchid = ((rtInt)step.arg1.getValue(scope, currentRunFrame)).value;
											currentRunFrame.quit_catch(catchid, step.token);

											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.enter_finally:
										{
											int finallyid = ((rtInt)step.arg1.getValue(scope, currentRunFrame)).value;
											currentRunFrame.enter_finally(finallyid);

											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.quit_finally:
										{
											int finallyid = ((rtInt)step.arg1.getValue(scope,  currentRunFrame)).value;
											currentRunFrame.quit_finally(finallyid, step.token);

											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.native_trace:
										//    nativefuncs.Trace.exec(this, step, scope);
										break;
									case OpCode.bind_scope:
										operators.OpCallFunction.bind(currentRunFrame, step, scope);
										break;
									case OpCode.clear_thispointer:
										operators.OpCallFunction.clear_thispointer(currentRunFrame, step, scope);
										break;
									case OpCode.make_para_scope:
										operators.OpCallFunction.create_paraScope(currentRunFrame, step, scope);
										break;
									case OpCode.push_parameter:
										operators.OpCallFunction.push_parameter(currentRunFrame, step, scope);
										break;
									case OpCode.call_function:
										operators.OpCallFunction.exec(currentRunFrame, step, scope);
										break;
									case OpCode.function_return:
										currentRunFrame.hasCallReturn = true;
										operators.OpCallFunction.exec_return(currentRunFrame, step, scope);

										break;

									case OpCode.new_instance:
										operators.OpCreateInstance.exec(currentRunFrame, step, scope);
										break;
									case OpCode.init_staticclass:
										operators.OpCreateInstance.init_static(currentRunFrame, step, scope);
										break;
									case OpCode.new_instance_class:
										operators.OpCreateInstance.exec_instanceClass(currentRunFrame, step, scope);
										break;
									case OpCode.prepare_constructor_argement:
										operators.OpCreateInstance.prepareConstructorArgements(currentRunFrame, step, scope);
										break;
									case OpCode.prepare_constructor_class_argement:
										operators.OpCreateInstance.prepareConstructorClassArgements(currentRunFrame, step, scope);
										break;
									case OpCode.push_parameter_class:
										operators.OpCreateInstance.push_parameter_class(currentRunFrame, step, scope);
										break;
									case OpCode.access_dot:
										operators.OpAccess_Dot.exec_dot(currentRunFrame, step, scope);
										break;
									case OpCode.access_dot_byname:
										operators.OpAccess_Dot.exec_dot_byname(currentRunFrame, step, scope);
										break;
									case OpCode.bracket_access:
										operators.OpAccess_Dot.exec_bracket_access(currentRunFrame, step, scope);
										break;
									case OpCode.bracket_byname:
										operators.OpAccess_Dot.exec_dot_byname(currentRunFrame, step, scope);
										break;
									case OpCode.access_method:
										operators.OpAccess_Dot.exec_method(currentRunFrame, step, scope);
										break;
									case OpCode.delete_prop:
										operators.OpDynamicProperty.exec_delete(currentRunFrame, step, scope);
										break;
									case OpCode.set_dynamic_prop:
										operators.OpDynamicProperty.exec_set_dynamic_prop(currentRunFrame, step, scope);
										break;
									case OpCode.try_read_getter:
										operators.OpPropGetSet.exec_try_read_prop(currentRunFrame, step, scope);
										break;
									case OpCode.try_write_setter:
										operators.OpPropGetSet.exec_try_write_prop(currentRunFrame, step, scope);
										break;
									case OpCode.array_push:
										operators.OpArray.exec_Push(currentRunFrame, step, scope);
										break;
									case OpCode.array_create:
										operators.OpArray.exec_create(currentRunFrame, step, scope);
										break;
									case OpCode.arrayAccessor_bind:
										operators.OpAccess_Dot.exec_arrayAccessor_bind(currentRunFrame, step, scope);
										break;
									case OpCode.vectorAccessor_bind:
										operators.OpVector.exec_AccessorBind(currentRunFrame, step, scope);
										break;
									case OpCode.vector_push:
										operators.OpVector.exec_push(currentRunFrame, step, scope);
										break;
									case OpCode.vector_pusharray:
										operators.OpVector.exec_pusharray(currentRunFrame, step, scope);
										break;
									case OpCode.vector_pushvector:
										operators.OpVector.exec_pushVector(currentRunFrame, step, scope);
										break;
									case OpCode.vectorAccessor_convertidx:
										operators.OpVector.exec_AccessorBind_ConvertIdx(currentRunFrame, step, scope);
										break;
									case OpCode.vector_initfrmdata:
										operators.OpVector.exec_initfromdata(currentRunFrame, step, scope);
										break;
									case OpCode.link_outpackagevairable:
										operators.OpLinkOutPackageScope.exec_link(currentRunFrame, step, scope);
										break;
									case OpCode.flag_call_super_constructor:
										currentRunFrame.endStepNoError();
										break;
									case OpCode.forin_get_enumerator:
										operators.OpForIn.forin_get_enumerator(currentRunFrame, step, scope);
										break;
									case OpCode.enumerator_movenext:
										operators.OpForIn.enumerator_movenext(currentRunFrame, step, scope);
										break;
									case OpCode.enumerator_current:
										operators.OpForIn.enumerator_current(currentRunFrame, step, scope);
										break;
									case OpCode.enumerator_close:
										operators.OpForIn.enumerator_close(currentRunFrame, step, scope);
										break;
									case OpCode.foreach_get_enumerator:
										operators.OpForIn.foreach_get_enumerator(currentRunFrame, step, scope);
										break;
									case OpCode.logic_is:
										operators.OpLogic.exec_IS(currentRunFrame, step, scope);
										break;
									case OpCode.logic_instanceof:
										operators.OpLogic.exec_instanceof(currentRunFrame, step, scope);
										break;
									case OpCode.convert_as:
										operators.OpLogic.exec_AS(currentRunFrame, step, scope);
										break;
									case OpCode.logic_in:
										operators.OpLogic.exec_In(currentRunFrame, step, scope);
										break;
									case OpCode.unary_typeof:
										operators.OpTypeOf.exec_TypeOf(currentRunFrame, step, scope);
										break;
									case OpCode.function_create:
										{
											rtArray arr = (rtArray)step.arg1.getValue(scope, currentRunFrame);
											int funcid = ((rtInt)arr.innerArray[0]).value;
											bool ismethod = ((rtBoolean)arr.innerArray[1]).value;

											rtFunction function = new rtFunction(funcid, ismethod);
											function.bind(scope);
											step.reg.getSlot(scope, currentRunFrame).directSet(function);

											currentRunFrame.endStepNoError();
										}
										break;
									case OpCode.yield_return:

										operators.OpCallFunction.exec_yieldreturn(currentRunFrame, step, scope);

										break;
									case OpCode.yield_continuetoline:
										{
											//跳转继续下一次yield
											currentRunFrame.codeLinePtr = ((rtInt)scope.memberData[scope.memberData.Length - 2].getValue()).value - 1;
											currentRunFrame.endStepNoError();
										}
										break;
									case OpCode.yield_break:
										currentRunFrame.hasCallReturn = true;
										currentRunFrame.returnSlot.directSet(rtUndefined.undefined);
										currentRunFrame.endStep(step);
										break;
									case OpCode.call_function_notcheck:
										operators.OpCallFunction.exec_notcheck(currentRunFrame, step, scope);
										break;
									case OpCode.cast_int_number:
										{
											var v1 = step.arg1.getValue(scope, currentRunFrame);
											step.reg.getSlot(scope, currentRunFrame).setValue((double)((rtInt)v1).value);
											currentRunFrame.endStepNoError();
											break;
										}
									case OpCode.cast_number_int:
										{
											var v1 = step.arg1.getValue(scope, currentRunFrame);
											step.reg.getSlot(scope, currentRunFrame).setValue(TypeConverter.ConvertToInt(v1));
											currentRunFrame.endStepNoError();
											break;
										}
									case OpCode.cast_uint_number:
										{
											var v1 = step.arg1.getValue(scope, currentRunFrame);
											step.reg.getSlot(scope, currentRunFrame).setValue((double)((rtUInt)v1).value);
											currentRunFrame.endStepNoError();
											break;
										}
									case OpCode.cast_number_uint:
										{
											var v1 = step.arg1.getValue(scope, currentRunFrame);
											step.reg.getSlot(scope, currentRunFrame).setValue(TypeConverter.ConvertToUInt(v1, currentRunFrame, null));
											currentRunFrame.endStepNoError();
											break;
										}
									case OpCode.cast_int_uint:
										{
											var v1 = step.arg1.getValue(scope,  currentRunFrame);
											step.reg.getSlot(scope, currentRunFrame).setValue((uint)((rtInt)v1).value);
											currentRunFrame.endStepNoError();
											break;
										}
									case OpCode.cast_uint_int:
										{
											var v1 = step.arg1.getValue(scope, currentRunFrame);
											step.reg.getSlot(scope, currentRunFrame).setValue((int)((rtUInt)v1).value);
											currentRunFrame.endStepNoError();
											break;
										}
									case OpCode.push_parameter_skipcheck_storetoheap:
										operators.OpCallFunction.push_parameter_skipcheck_stroetoheap(currentRunFrame, step, scope);
										break;
									case OpCode.push_parameter_skipcheck_storetostack:
										operators.OpCallFunction.push_parameter_skipcheck_stroetostack(currentRunFrame, step, scope);
										break;
									case OpCode.push_parameter_skipcheck_testnative:
										operators.OpCallFunction.push_parameter_skipcheck_testnative(currentRunFrame, step, scope);
										break;
									case OpCode.push_parameter_nativeconstpara:
										//operators.OpCallFunction.push_parameter_nativeconstpara(currentRunFrame, step, scope);
										{
											var fc = currentRunFrame.funCaller;

											var toCallFunc = fc.toCallFunc;
											var nf = (nativefuncs.NativeConstParameterFunction)swc.getNativeFunction(toCallFunc);
											{
												//int id = ((rtInt)step.arg2.getValue(scope, this)).value;
												RunTimeValueBase arg = step.arg1.getValue(scope, currentRunFrame);
												bool success;
												fc.pushParameterNativeModeConstParameter(nf, arg, step.memregid1, out success);

											}

											int count = step.jumoffset;
											for (int i = 1; i < count; i++)
											{
												var stepn = block.instructions[currentRunFrame.codeLinePtr + i];
												//int id = ((rtInt)stepn.arg2.getValue(scope, this)).value;
												RunTimeValueBase arg = stepn.arg1.getValue(scope, currentRunFrame);
												bool success;
												fc.pushParameterNativeModeConstParameter(nf, arg, step.memregid1+i, out success);

											}

											currentRunFrame.codeLinePtr += count;

											fc.callbacker = fc;
											fc.returnSlot = block.instructions[currentRunFrame.codeLinePtr].reg.getSlot(scope, currentRunFrame);
											nf.bin = swc;
											fc.doCall_allcheckpass_nativefunctionconstpara(nf);
											currentRunFrame.funCaller = null;



										}

										break;
									case OpCode.push_parameter_para:
										operators.OpCallFunction.push_parameter_para(currentRunFrame, step, scope);
										break;
									case OpCode.make_para_scope_method:
										operators.OpCallFunction.create_paraScope_Method(currentRunFrame, step, scope);
										break;
									case OpCode.make_para_scope_withsignature_nativeconstpara:

										if (step.jumoffset == step.memregid1)
										{
											++currentRunFrame.codeLinePtr;
											rtFunction function;
											MethodGetterBase mb = step.arg1 as MethodGetterBase;
											if (mb != null)
											{
												function = (rtFunction)mb.getMethod(scope);
											}
											else
											{
												function = (rtFunction)step.arg1.getValue(scope, currentRunFrame);
											}
											var nf = (nativefuncs.NativeConstParameterFunction)swc.getNativeFunction(function.functionId);

											int count = step.jumoffset;

											nf.bindTempSlot();
											nf.bin = swc;
											bool success = false;
											try
											{
												for (int i = 0; i < count; i++)
												{
													var stepn = block.instructions[currentRunFrame.codeLinePtr];
													RunTimeValueBase arg = stepn.arg1.getValue(scope, currentRunFrame);

													nf.setTempSlotValue(arg, i);
											
													++ currentRunFrame.codeLinePtr;
												}

												var returnslot = block.instructions[currentRunFrame.codeLinePtr].reg.getSlot(scope, currentRunFrame);
												_executeToken = nativefuncs.NativeConstParameterFunction.ExecuteToken.nulltoken;
												nf.execute3(
												function.this_pointer != null ? function.this_pointer : scope.this_pointer,
												swc.functions[function.functionId],
												returnslot,
												step.token,
												currentRunFrame,
												out success
												);
											}
											finally
											{
												_executeToken = nativefuncs.NativeConstParameterFunction.ExecuteToken.nulltoken;
												nf.unbindTempSlot();
												if (mb != null)
													function.Clear();
											}


											_executeToken = nativefuncs.NativeConstParameterFunction.ExecuteToken.nulltoken;
											_nativefuncCaller = null;

											if (success)
											{
												var receive_err = clear_nativeinvokeraiseerror();
												if (receive_err != null)
												{
													currentRunFrame.receiveErrorFromStackFrame(receive_err);
												}
												else
												{
													++currentRunFrame.codeLinePtr;
												}
											}
											else
											{
												currentRunFrame.endStep();
											}
										}
										else
										{
											rtFunction function;
											MethodGetterBase mb = step.arg1 as MethodGetterBase;
											if (mb != null)
											{
												function = (rtFunction)mb.getMethod(scope);
											}
											else
											{
												function = (rtFunction)step.arg1.getValue(scope, currentRunFrame);
											}

											var funcCaller = funcCallerPool.create(currentRunFrame, step.token);
											funcCaller.SetFunction(function);
											if (mb != null)
											{
												function.Clear();
											}
											funcCaller._tempSlot = currentRunFrame._tempSlot1;
											funcCaller.toCallFunc = swc.functions[function.functionId];
											if (!funcCaller.createParaScope()) { break; }

											currentRunFrame.funCaller = funcCaller;

											currentRunFrame.codeLinePtr++;

											var nf = (nativefuncs.NativeConstParameterFunction)swc.getNativeFunction(function.functionId);
											int count = step.jumoffset;
											for (int i = 0; i < count; i++)
											{
												var stepn = block.instructions[currentRunFrame.codeLinePtr + i];
												RunTimeValueBase arg = stepn.arg1.getValue(scope, currentRunFrame);
												bool success;
												funcCaller.pushParameterNativeModeConstParameter(nf, arg, i, out success);

											}

											if (step.jumoffset == step.memregid1)
											{
												currentRunFrame.codeLinePtr += count;

												funcCaller.callbacker = funcCaller;
												funcCaller.returnSlot = block.instructions[currentRunFrame.codeLinePtr].reg.getSlot(scope, currentRunFrame);
												nf.bin = swc;
												funcCaller.doCall_allcheckpass_nativefunctionconstpara(nf);
												currentRunFrame.funCaller = null;
											}
											else
											{
												currentRunFrame.codeLinePtr += count;
											}
										}

										break;
									case OpCode.make_para_scope_withsignature:
										operators.OpCallFunction.create_paraScope_WithSignature(currentRunFrame, step, scope);
										break;
									case OpCode.make_para_scope_method_notnativeconstpara_allparaonstack:
										operators.OpCallFunction.create_paraScope_Method_NotNativeConstPara_AllParaOnStack(currentRunFrame, step, scope);
										break;
									case OpCode.make_para_scope_withsignature_allparaonstack:
										operators.OpCallFunction.create_paraScope_WithSignature_AllParaOnStack(currentRunFrame, step, scope);
										
										break;
									case OpCode.make_para_scope_method_noparameters:
										operators.OpCallFunction.create_paraScope_Method_NoParameters(currentRunFrame, step, scope);
										break;
									case OpCode.make_para_scope_withsignature_noparameters:
										operators.OpCallFunction.create_paraScope_WithSignature_NoParameters(currentRunFrame, step, scope);
										break;
									case OpCode.function_return_funvoid:
										{
											currentRunFrame.hasCallReturn = true;
											currentRunFrame.returnSlot.directSet(rtUndefined.undefined);
											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.function_return_nofunction:
										{
											currentRunFrame.hasCallReturn = true;
											RunTimeValueBase rv = step.arg1.getValue(scope, currentRunFrame);
											currentRunFrame.returnSlot.directSet(rv);
											currentRunFrame.endStep(step);
										}
										break;
									case OpCode.function_return_funvoid_notry:
										{
											currentRunFrame.returnSlot.directSet(rtUndefined.undefined);
											currentRunFrame.codeLinePtr = currentRunFrame.stepCount;
										}
										break;
									case OpCode.function_return_nofunction_notry:
										{
											currentRunFrame.returnSlot.directSet(step.arg1.getValue(scope, currentRunFrame));
											currentRunFrame.codeLinePtr = currentRunFrame.stepCount;
										}
										break;
									case OpCode.call_function_notcheck_notreturnobject:
										{

											currentRunFrame.funCaller.callbacker = currentRunFrame.funCaller;
											currentRunFrame.funCaller.returnSlot = step.reg.getSlot(scope, currentRunFrame);
											currentRunFrame.funCaller.doCall_allcheckpass();
											currentRunFrame.funCaller = null;

											break;
										}
									case OpCode.call_function_notcheck_notreturnobject_notnative:
										{
											currentRunFrame.funCaller.callbacker = currentRunFrame.funCaller;
											currentRunFrame.funCaller.returnSlot = step.reg.getSlot(scope, currentRunFrame);
											currentRunFrame.funCaller.returnSlot.directSet(step.arg2.getValue(null, null));
											currentRunFrame.funCaller.doCall_allcheckpass_nonative_hassetreturndefault();
											currentRunFrame.funCaller = null;

											break;
										}
									case OpCode.call_function_notcheck_notreturnobject_notnative_method:
										{
											var funCaller = currentRunFrame.funCaller;

											funCaller.callbacker = funCaller;
											funCaller.returnSlot = step.reg.getSlot(scope,  currentRunFrame);
											funCaller.returnSlot.directSet(step.arg2.getValue(null, null));
											
											callBlock_Method_NoHeap(swc.blocks[funCaller.toCallFunc.blockid],
												funCaller.returnSlot, step.token, funCaller.callbacker, funCaller.functionThisPointer);

											currentRunFrame.funCaller = null;

											break;
										}
									case OpCode.vector_getvalue:
										{
											operators.OpVector.exec_GetValue(currentRunFrame, step, scope);
											break;
										}
									case OpCode.vector_getvalue_memint_memintidx:
										{
											ASBinCode.rtti.Vector_Data vector =
												(ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)step.arg1.getValue(scope, currentRunFrame)).value).hosted_object;

											int idx = memint[step.memregid3];

											if (idx < 0 || idx >= vector.innnerList.Count)
											{
												currentRunFrame.throwError(step.token, 1125,
													"The index " + idx + " is out of range " + vector.innnerList.Count + ".");
												currentRunFrame.endStep(step);
											}
											else
											{
												memint[step.memregid1] = ((rtInt)vector.innnerList[idx]).value;
												++currentRunFrame.codeLinePtr;
											}
											break;
										}
									case OpCode.if_equality_num_num_jmp_notry:
										{
											var n1 = (step.arg1.getValue(scope,currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope,currentRunFrame)).toNumber();
											if (n1 == n2)
											{
												step.reg.getSlot(scope,  currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.True);
												currentRunFrame.codeLinePtr += step.jumoffset + 1; //jumtooffset位置为label,因此直接可以再跳过一层

											}
											else
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.False);
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_equality_num_num_jmp_notry_noreference:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 == n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1; //jumtooffset位置为label,因此直接可以再跳过一层

											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_not_equality_num_num_jmp_notry:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 != n2)
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.True);
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.False);
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_not_equality_num_num_jmp_notry_noreference:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 != n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;
											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_le_num_jmp_notry:
										{
											var n1 = (step.arg1.getValue(scope,  currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope,  currentRunFrame)).toNumber();
											if (n1 <= n2)
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.True);
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.False);
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_le_num_jmp_notry_noreference:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 <= n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_lt_num_jmp_notry:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 < n2)
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.True);
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												step.reg.getSlot(scope,currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.False);
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_lt_num_jmp_notry_noreference:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 < n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_ge_num_jmp_notry:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 >= n2)
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.True);
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.False);
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_ge_num_jmp_notry_noreference:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 >= n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_gt_num_jmp_notry:
										{
											var n1 = (step.arg1.getValue(scope,currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope,currentRunFrame)).toNumber();
											if (n1 > n2)
											{
												step.reg.getSlot(scope,currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.True);
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												step.reg.getSlot(scope, currentRunFrame).setValue(ASBinCode.rtData.rtBoolean.False);
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_gt_num_jmp_notry_noreference:
										{
											var n1 = (step.arg1.getValue(scope, currentRunFrame)).toNumber();
											var n2 = (step.arg2.getValue(scope, currentRunFrame)).toNumber();
											if (n1 > n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;

											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.afterIncDes_clear_v1_link:
										{
											((StackSlot)((StackSlotAccessor)step.arg1).getSlotForAssign(scope, currentRunFrame)).linkTo(null);
											currentRunFrame.endStepNoError();
											break;
										}
									case OpCode.access_dot_memregister:
										operators.OpAccess_Dot.exec_dot_register(currentRunFrame, step, scope);
										break;
									case OpCode.sub_number_memnumber_memnumber:
										memnumber[step.memregid1] =
											memnumber[step.memregid2] - memnumber[step.memregid3];
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.sub_number_memnumber_slt_constnumber:

										memnumber[step.memregid1] =
											step.arg1.getValue(scope, currentRunFrame).toNumber()
											- step.constnumber2;
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.div_number_memnumber_memnumber:
										
										memnumber[step.memregid1] =
											memnumber[step.memregid2] / memnumber[step.memregid3];
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.multi_number_memnumber_memnumber:
										
										memnumber[step.memregid1] =
											memnumber[step.memregid2] * memnumber[step.memregid3];
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.add_number_memnumber_memnumber:
										
										memnumber[step.memregid1] =
											memnumber[step.memregid2] + memnumber[step.memregid3];
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.add_number_memint_memint:

										memnumber[step.memregid1] =
											memint[step.memregid2] + (double)memint[step.memregid3];
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.add_number_memnumber_constnumber:
										
										memnumber[step.memregid1] =
											memnumber[step.memregid2] + step.constnumber2;
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.add_number_memint_constnumber:

										memnumber[step.memregid1] =
											memint[step.memregid2] + step.constnumber2;
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.add_number_memnumber_slt_memint:
										memnumber[step.memregid1] =
											(step.arg1.getValue(scope, currentRunFrame)).toNumber()

											+ (double)memint[step.memregid3];
										++currentRunFrame.codeLinePtr;

										break;
									case OpCode.add_number_memnumber_slt_memnumber:
										memnumber[step.memregid1] =
											step.arg1.getValue(scope, currentRunFrame).toNumber()

											+ memnumber[step.memregid3];
										++currentRunFrame.codeLinePtr;

										break;
									case OpCode.div_number_memnumber_constnumber:
										
										memnumber[step.memregid1] =
											memnumber[step.memregid2] / step.constnumber2;
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.div_number_memint_constnumber:
										memnumber[step.memregid1] =
											memint[step.memregid2] / step.constnumber2;
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.suffix_inc_number_memnumber:
										{
											
											memnumber[step.memregid1] = memnumber[step.memregid2]++;

											++currentRunFrame.codeLinePtr;
										}
										break;
									case OpCode.suffix_inc_int_memint:
										{
											memint[step.memregid1] = memint[step.memregid2]++;
											++currentRunFrame.codeLinePtr;
										}
										break;
									case OpCode.assign_tomemnumber:
										
										memnumber[step.memregid1] = step.arg1.getValue(scope, currentRunFrame).toNumber();

										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.assign_tomemint:
										
										memint[step.memregid1] = (int)step.arg1.getValue(scope, currentRunFrame).toNumber();
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.assign_memnumber_tomemnumber:
										
										memnumber[step.memregid1] = memnumber[step.memregid2];
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.assign_memint_tomemint:
										memint[step.memregid1] = memint[step.memregid2];
										++currentRunFrame.codeLinePtr;
										break;
									case OpCode.if_lt_memnumber_constnum_jmp_notry_noreference:
										{
											var n1 = memnumber[step.memregid2];
											var n2 = step.constnumber2;
											if (n1 < n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;
											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.if_lt_memint_constnum_jmp_notry_noreference:
										{
											var n1 = memint[step.memregid2];
											var n2 = step.constnumber2;
											if (n1 < n2)
											{
												currentRunFrame.codeLinePtr += step.jumoffset + 1;
											}
											else
											{
												++currentRunFrame.codeLinePtr;
											}
										}
										break;
									case OpCode.cast_number_int_memnumber_memint:
										{
											double r = memnumber[step.memregid2];
											if (double.IsNaN(r) || double.IsInfinity(r))
											{
												memint[step.memregid1] = 0;
											}
											else
											{
												memint[step.memregid1] = (int)((long)r);
											}
											++currentRunFrame.codeLinePtr;
										}
										break;
									case OpCode.cast_number_int_constnum_memint:
										{
											memint[step.memregid1] = (int)((long)step.constnumber1);
											++currentRunFrame.codeLinePtr;
										}
										break;
									default:
										throw new Exception(step.opCode + "操作未实现");
								}

					#endregion


							}

							isstep= true;continue;





						}
						break;
					}
					catch (EngineException)   //引擎抛出的异常直接抛出
					{
						throw;
					}
					catch (StackOverflowException)
					{
						throw;
					}
					catch (OutOfMemoryException)
					{
						throw;
					}
					catch (Exception le)    //捕获外部函数异常
					{
						if (currentRunFrame != null)
						{
							if (_nativefuncCaller != null)
							{
								if (_nativefuncCaller.callbacker != null)
								{
									_nativefuncCaller.callbacker.noticeRunFailed();
								}
								_nativefuncCaller.release();
								_nativefuncCaller = null;
							}

							if (currentRunFrame.instanceCreator != null)
							{
								currentRunFrame.instanceCreator.noticeWhenCreateAneFailed();
							}

							SourceToken token;

							if (currentRunFrame.codeLinePtr < currentRunFrame.block.instructions.Length)
							{
								token = currentRunFrame.block.instructions[currentRunFrame.codeLinePtr].token;
							}
							else
							{
								token = new SourceToken(0, 0, string.Empty);
							}

							currentRunFrame.throwAneException(token
								, le.Message);

							

							currentRunFrame.receiveErrorFromStackFrame(currentRunFrame.runtimeError);

							continue;
						}
						else
						{
							throw;
						}
					}

#endif



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

					return result.getValue(topscope, displayStackFrame.getTempFrame());
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






		internal static readonly HeapSlot[] emptyMembers = new HeapSlot[0];
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
					TypeConverter.getDefaultValue(vt).getValue(null, null)
					);
			}
			return memberDataList;
		}

		private CodeBlock blankBlock;
		private RunTimeScope blankScope;
		internal void CallBlankBlock(IBlockCallBack callbacker)
		{
			if (blankBlock == null)
			{
				blankBlock = new CodeBlock(int.MaxValue - 1, "#blank", -65535, false);
				blankBlock.opSteps.Add(new OpStep(OpCode.flag, SourceToken.Empty)); blankBlock.instructions = blankBlock.opSteps.ToArray(); blankBlock.opSteps = null;
				blankScope = new RunTimeScope(emptyMembers, blankBlock.id, null, null, RunTimeScopeType.function);
			}

			callBlock(blankBlock, null, null, null, null, callbacker, null, RunTimeScopeType.function);

		}

		public rtObjectBase alloc_pureHostedOrLinkedObject(ASBinCode.rtti.Class cls)
		{
			return operators.InstanceCreator.createPureHostdOrLinkObject(this, cls);
		}



		public bool init_static_class(ASBinCode.rtti.Class cls, SourceToken token)
		{
			return operators.InstanceCreator.init_static_class(cls, this, token);
		}



		internal void callBlock_Method_NoHeap(CodeBlock calledblock,
			SLOT returnSlot,
			SourceToken token,
			IBlockCallBack callbacker,
			RunTimeValueBase this_pointer)
		{
			int startOffset = 0;
			var rs = runtimeStack.Peek();
			startOffset = rs.baseBottomSlotIndex + rs.call_parameter_slotCount;

			StackFrame frame = null;

			if (startOffset + calledblock.totalStackSlots + 1 + 1 >= STACKSLOTLENGTH || !stackframePool.hasCacheObj())
			{
				if (callbacker != null)
				{
					callbacker.noticeRunFailed();
				}
				currentRunFrame.receiveErrorFromStackFrame(new error.InternalError(swc, token, "stack overflow"));
				return;
			}
			else
			{
				frame = stackframePool.create(calledblock);
				frame.codeLinePtr = 0;

				frame.returnSlot = returnSlot;
				frame.callbacker = callbacker;

				frame.offset = startOffset;
				frame.baseBottomSlotIndex = startOffset + frame.baseUseSlots;

				//frame._tempSlot1 = stackSlots[frame.baseBottomSlotIndex - 2];
				//frame._tempSlot2 = stackSlots[frame.baseBottomSlotIndex - 1];
				runtimeStack.Push(frame);
				currentRunFrame = frame;

				var block = calledblock; int len = block.regConvFromVar.Length;
				for (int i = 0; i < len; i++)
				{
					StackSlotAccessor regvar = block.regConvFromVar[i];
					var slot = (StackSlot)regvar.getSlot(null, frame);
					TypeConverter.setDefaultValueToStackSlot(
						regvar.valueType, slot
						);
				}
			}

			frame.scope = ((rtObjectBase)this_pointer).objScope;



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

				startOffset = rs.baseBottomSlotIndex + rs.call_parameter_slotCount;

			}

			StackFrame frame = null;

			if (startOffset + calledblock.totalStackSlots + 1 + 1 >= STACKSLOTLENGTH || !stackframePool.hasCacheObj())
			{
				//runtimeError = new error.InternalError(token, "stack overflow");
				if (callbacker != null)
				{
					callbacker.noticeRunFailed();
				}
				//frame.close();
				//currentRunFrame.receiveErrorFromStackFrame(runtimeError);
				//runtimeError = null;

				currentRunFrame.receiveErrorFromStackFrame(new error.InternalError(swc, token, "stack overflow"));

				return null;
			}
			else
			{
				frame = stackframePool.create(calledblock);
				frame.codeLinePtr = 0;

				frame.returnSlot = returnSlot;
				frame.callbacker = callbacker;

				frame.offset = startOffset;
				frame.baseBottomSlotIndex = startOffset + frame.baseUseSlots;

				//frame._tempSlot1 = stackSlots[frame.baseBottomSlotIndex-2];
				//frame._tempSlot2 = stackSlots[frame.baseBottomSlotIndex-1];
				runtimeStack.Push(frame);
				currentRunFrame = frame;

				var regConvFromVar = calledblock.regConvFromVar;
				for (int i = 0; i < regConvFromVar.Length; i++)
				{
					StackSlotAccessor regvar = regConvFromVar[i];
					var slot = (StackSlot)regvar.getSlot(null, frame);
					TypeConverter.setDefaultValueToStackSlot(
						regvar.valueType, slot
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
			if (ReferenceEquals(membersHeap, emptyMembers) && type == RunTimeScopeType.function)
			{
				if (this_pointer is rtObjectBase)
				{
					if (callerScope.scopeType != RunTimeScopeType.function)
					{
						frame.scope = ((rtObjectBase)this_pointer).objScope;
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
			else if (ReferenceEquals(calledblock, blankBlock))
			{
				frame.scope = blankScope;
				return null;
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

		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, null, null, null, null, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, v1, null, null, null, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1, RunTimeValueBase v2)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, v1, v2, null, null, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1, RunTimeValueBase v2, RunTimeValueBase v3)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, v1, v2, v3, null, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1, RunTimeValueBase v2, RunTimeValueBase v3, RunTimeValueBase v4)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, v1, v2, v3, v4, null, null);
		}
		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error, RunTimeValueBase v1, RunTimeValueBase v2, RunTimeValueBase v3,
			RunTimeValueBase v4, RunTimeValueBase v5)
		{
			return runFunction(function, thisObj, resultSlot, token, out error, v1, v2, v3, v4, v5, null);
		}

		internal bool runFunction(rtFunction function, RunTimeValueBase thisObj, SLOT resultSlot, SourceToken token, out error.InternalError error,
			RunTimeValueBase v1,
			RunTimeValueBase v2,
			RunTimeValueBase v3,
			RunTimeValueBase v4,
			RunTimeValueBase v5,
			RunTimeValueBase[] paraArgs
			)
		{
			var funcCaller = funcCallerPool.create(currentRunFrame, token);
			funcCaller.SetFunction(function);
			funcCaller.SetFunctionThis(thisObj);
			funcCaller.loadDefineFromFunction();

			if (!funcCaller.createParaScope()) { error = currentRunFrame.runtimeError == null ? new error.InternalError(swc, token, "创建参数失败") : currentRunFrame.runtimeError; return false; }
#region pushparameter
			int c = 0;
			bool success;

			int args;
			if (paraArgs != null)
				args = 6;
			else if (v5 != null)
				args = 5;
			else if (v4 != null)
				args = 4;
			else if (v3 != null)
				args = 3;
			else if (v2 != null)
				args = 2;
			else if (v1 != null)
				args = 1;
			else
				args = 0;

			if (v1 != null)
			{
				funcCaller.pushParameter(v1, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc, token, "创建参数失败") : currentRunFrame.runtimeError;
					return false;
				}
				c++;
			}
			else if (args > 0)
			{
				throw new ArgumentNullException("v1");
			}


			if (v2 != null)
			{
				funcCaller.pushParameter(v2, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc, token, "创建参数失败") : currentRunFrame.runtimeError;
					return false;
				}
				c++;
			}
			else if (args > 1)
			{
				throw new ArgumentNullException("v2");
			}

			if (v3 != null)
			{
				funcCaller.pushParameter(v3, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc, token, "创建参数失败") : currentRunFrame.runtimeError;
					return false;
				}
				c++;
			}
			else if (args > 2)
			{
				throw new ArgumentNullException("v3");
			}

			if (v4 != null)
			{
				funcCaller.pushParameter(v4, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc, token, "创建参数失败") : currentRunFrame.runtimeError;
					return false;
				}
				c++;
			}
			else if (args > 3)
			{
				throw new ArgumentNullException("v4");
			}

			if (v5 != null)
			{
				funcCaller.pushParameter(v5, c, out success);
				if (!success)
				{
					error = currentRunFrame.runtimeError == null ? new error.InternalError(swc, token, "创建参数失败") : currentRunFrame.runtimeError;
					return false;
				}
				c++;
			}
			else if (args > 4)
			{
				throw new ArgumentNullException("v5");
			}

			if (paraArgs != null)
			{
				for (int i = 0; i < paraArgs.Length; i++)
				{
					funcCaller.pushParameter(paraArgs[i], c, out success);
					if (!success)
					{
						error = currentRunFrame.runtimeError == null ? new error.InternalError(swc, token, "创建参数失败") : currentRunFrame.runtimeError;
						return false;
					}
					c++;
				}
			}

#endregion

			funcCaller.returnSlot = resultSlot;
			funcCaller._tempSlot = currentRunFrame._tempSlot2;
			return runFuncCaller(funcCaller, token, out error);
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
			r.isSuccess = false; r.isEnd = false;

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
						error = runtimeError == null ? new error.InternalError(swc, token, "函数执行失败") : runtimeError;
					}
					else
					{
						error = receive_error == null ? new error.InternalError(swc, token, "函数执行失败") : receive_error;
					}
				}

				return result;
			}
			finally
			{
				runFuncresultPool.ret(r);

			}
		}

		private static void runfuntionEnd(BlockCallBackBase sender, object args)
		{
			((runFuncResult)sender.cacheObjects[0]).isEnd = true;
			((runFuncResult)sender.cacheObjects[0]).isSuccess = true;
			sender.isSuccess = true;
		}
		private static void runfuntionFailed(BlockCallBackBase sender, object args)
		{
			((runFuncResult)sender.cacheObjects[0]).isEnd = true;
			((runFuncResult)sender.cacheObjects[0]).isSuccess = false;
			sender.isSuccess = false;
		}

		internal error.InternalError clear_nativeinvokeraiseerror()
		{
			var temp = receive_error;
			receive_error = null;
			return temp;
		}

		private IBlockCallBack _tempcallbacker;

		//private IBlockCallBack _runningtempcallbacker;

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
				var _runningtempcallbacker = _tempcallbacker;
				_tempcallbacker = null;
				_runningtempcallbacker.call(_runningtempcallbacker.args);

				//_runningtempcallbacker = null;

				return true;
			}

			if (currentRunFrame.codeLinePtr >= currentRunFrame.stepCount) //执行完成
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
#if DEBUG || true
				currentRunFrame.step();
#else

#region 人肉内联

				var block = currentRunFrame.block;
				
				var scope = currentRunFrame.scope;

				currentRunFrame.hascallstep = true;

				OpStep step = block.opSteps[currentRunFrame.codeLinePtr];
				//exec(step);
				switch (step.opCode)
				{
					case OpCode.cast:
						operators.OpCast.execCast(currentRunFrame, step, scope);
						break;
					case OpCode.cast_primitive:
						operators.OpCast.exec_CastPrimitive(currentRunFrame, step, scope);
						break;
					case OpCode.assigning:
						operators.OpAssigning.execAssigning(currentRunFrame, step, scope);
						break;

					case OpCode.add_number:
						operators.OpAdd.execAdd_Number(currentRunFrame, step, scope);
						break;
					case OpCode.add_string:
						operators.OpAdd.execAdd_String(currentRunFrame, step, scope);
						break;
					case OpCode.add:
						operators.OpAdd.execAdd(currentRunFrame, step, scope);
						break;
					case OpCode.sub_number:
						operators.OpSub.execSub_Number(currentRunFrame, step, scope);
						break;
					case OpCode.sub:
						operators.OpSub.execSub(currentRunFrame, step, scope);
						break;
					case OpCode.multi:
						operators.OpMulti.execMulti(currentRunFrame, step, scope);
						break;
					case OpCode.multi_number:
						operators.OpMulti.exec_MultiNumber(currentRunFrame, step, scope);
						break;
					case OpCode.div:
						operators.OpMulti.execDiv(currentRunFrame, step, scope);
						break;
					case OpCode.div_number:
						operators.OpMulti.exec_DivNumber(currentRunFrame, step, scope);
						break;
					case OpCode.mod:
						operators.OpMulti.execMod(currentRunFrame, step, scope);
						break;
					case OpCode.mod_number:
						operators.OpMulti.exec_ModNumber(currentRunFrame, step, scope);
						break;
					case OpCode.unary_plus:
						operators.OpUnaryPlus.execUnaryPlus(currentRunFrame, step, scope);
						break;
					case OpCode.neg:
						operators.OpNeg.execNeg(currentRunFrame, step, scope);
						break;
					case OpCode.gt_num:
						operators.OpLogic.execGT_NUM(currentRunFrame, step, scope);
						break;
					case OpCode.gt_void:
						operators.OpLogic.execGT_Void(currentRunFrame, step, scope);
						break;
					case OpCode.lt_num:
						operators.OpLogic.execLT_NUM(currentRunFrame, step, scope);
						break;
					case OpCode.lt_void:
						operators.OpLogic.execLT_VOID(currentRunFrame, step, scope);
						break;
					case OpCode.ge_num:
						operators.OpLogic.execGE_NUM(currentRunFrame, step, scope);
						break;
					case OpCode.ge_void:
						operators.OpLogic.execGE_Void(currentRunFrame, step, scope);
						break;
					case OpCode.le_num:
						operators.OpLogic.execLE_NUM(currentRunFrame, step, scope);
						break;
					case OpCode.le_void:
						operators.OpLogic.execLE_VOID(currentRunFrame, step, scope);
						break;
					case OpCode.equality:
						operators.OpLogic.execEQ(currentRunFrame, step, scope);
						break;
					case OpCode.not_equality:
						operators.OpLogic.execNotEQ(currentRunFrame, step, scope);
						break;
					case OpCode.equality_num_num:
						operators.OpLogic.execEQ_NumNum(currentRunFrame, step, scope);
						break;
					case OpCode.not_equality_num_num:
						operators.OpLogic.execNotEQ_NumNum(currentRunFrame, step, scope);
						break;
					case OpCode.equality_str_str:
						operators.OpLogic.execEQ_StrStr(currentRunFrame, step, scope);
						break;
					case OpCode.not_equality_str_str:
						operators.OpLogic.execNotEQ_StrStr(currentRunFrame, step, scope);
						break;
					case OpCode.strict_equality:
						operators.OpLogic.execStrictEQ(currentRunFrame, step, scope);
						break;
					case OpCode.not_strict_equality:
						operators.OpLogic.execStrictNotEQ(currentRunFrame, step, scope);
						break;
					case OpCode.logic_not:
						operators.OpLogic.execNOT(currentRunFrame, step, scope);
						break;
					case OpCode.bitAnd:
						operators.OpBit.execBitAnd(currentRunFrame, step, scope);
						break;
					case OpCode.bitOr:
						operators.OpBit.execBitOR(currentRunFrame, step, scope);
						break;
					case OpCode.bitXOR:
						operators.OpBit.execBitXOR(currentRunFrame, step, scope);
						break;
					case OpCode.bitNot:
						operators.OpBit.execBitNot(currentRunFrame, step, scope);
						break;
					case OpCode.bitLeftShift:
						operators.OpBit.execBitLeftShift(currentRunFrame, step, scope);
						break;
					case OpCode.bitRightShift:
						operators.OpBit.execBitRightShift(currentRunFrame, step, scope);
						break;
					case OpCode.bitUnsignedRightShift:
						operators.OpBit.execBitUnSignRightShift(currentRunFrame, step, scope);
						break;
					case OpCode.increment:
						operators.OpIncrementDecrement.execIncrement(currentRunFrame, step, scope);
						break;
					case OpCode.increment_int:
						operators.OpIncrementDecrement.execIncInt(currentRunFrame, step, scope);
						break;
					case OpCode.increment_uint:
						operators.OpIncrementDecrement.execIncUInt(currentRunFrame, step, scope);
						break;
					case OpCode.increment_number:
						operators.OpIncrementDecrement.execIncNumber(currentRunFrame, step, scope);
						break;
					case OpCode.decrement:
						operators.OpIncrementDecrement.execDecrement(currentRunFrame, step, scope);
						break;

					case OpCode.decrement_int:
						operators.OpIncrementDecrement.execDecInt(currentRunFrame, step, scope);
						break;
					case OpCode.decrement_uint:
						operators.OpIncrementDecrement.execDecUInt(currentRunFrame, step, scope);
						break;
					case OpCode.decrement_number:
						operators.OpIncrementDecrement.execDecNumber(currentRunFrame, step, scope);
						break;

					case OpCode.suffix_inc:
						operators.OpIncrementDecrement.execSuffixInc(currentRunFrame, step, scope);
						break;
					case OpCode.suffix_inc_int:
						operators.OpIncrementDecrement.execSuffixIncInt(currentRunFrame, step, scope);
						break;
					case OpCode.suffix_inc_uint:
						operators.OpIncrementDecrement.execSuffixIncUint(currentRunFrame, step, scope);
						break;
					case OpCode.suffix_inc_number:
						operators.OpIncrementDecrement.execSuffixIncNumber(currentRunFrame, step, scope);
						break;
					case OpCode.suffix_dec:
						operators.OpIncrementDecrement.execSuffixDec(currentRunFrame, step, scope);
						break;
					case OpCode.suffix_dec_int:
						operators.OpIncrementDecrement.execSuffixDecInt(currentRunFrame, step, scope);
						break;
					case OpCode.suffix_dec_uint:
						operators.OpIncrementDecrement.execSuffixDecUInt(currentRunFrame, step, scope);
						break;
					case OpCode.suffix_dec_number:
						operators.OpIncrementDecrement.execSuffixDecNumber(currentRunFrame, step, scope);
						break;
					case OpCode.flag:
						//标签行，不做任何操作
						currentRunFrame.endStepNoError();
						break;
					case OpCode.if_jmp:
						{
							if (((rtBoolean)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value)//ReferenceEquals(ASBinCode.rtData.rtBoolean.True, step.arg1.getValue(scope)))
							{
								currentRunFrame.hasCallJump = true;
								currentRunFrame.jumptoline = currentRunFrame.codeLinePtr + step.jumoffset;
								currentRunFrame.endStep(step);
								break;
							}
							else
							{
								++currentRunFrame.codeLinePtr; //currentRunFrame.endStepNoError(); //.endStep(step);
							}
						}
						break;
					case OpCode.if_jmp_notry:
						{
							if (((rtBoolean)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value)
							{
								currentRunFrame.codeLinePtr += step.jumoffset+1;				
							}
							else
							{
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.jmp:
						currentRunFrame.hasCallJump = true;
						currentRunFrame.jumptoline = currentRunFrame.codeLinePtr + step.jumoffset;
						currentRunFrame.endStep(step);
						break;
					case OpCode.jmp_notry:
						{
							currentRunFrame.codeLinePtr += step.jumoffset+1 ;
							break;
						}
					case OpCode.raise_error:
						nativefuncs.Throw.exec(currentRunFrame, step, scope);
						break;
					case OpCode.enter_try:
						{
							int tryid = ((rtInt)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;
							currentRunFrame.enter_try(tryid);

							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.quit_try:
						{
							int tryid = ((rtInt)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;
							currentRunFrame.quit_try(tryid, step.token);

							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.enter_catch:
						{
							int catchid = ((rtInt)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;
							currentRunFrame.enter_catch(catchid);

							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.quit_catch:
						{
							int catchid = ((rtInt)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;
							currentRunFrame.quit_catch(catchid, step.token);

							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.enter_finally:
						{
							int finallyid = ((rtInt)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;
							currentRunFrame.enter_finally(finallyid);

							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.quit_finally:
						{
							int finallyid = ((rtInt)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;
							currentRunFrame.quit_finally(finallyid, step.token);

							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.native_trace:
						//    nativefuncs.Trace.exec(this, step, scope);
						break;
					case OpCode.bind_scope:
						operators.OpCallFunction.bind(currentRunFrame, step, scope);
						break;
					case OpCode.clear_thispointer:
						operators.OpCallFunction.clear_thispointer(currentRunFrame, step, scope);
						break;
					case OpCode.make_para_scope:
						operators.OpCallFunction.create_paraScope(currentRunFrame, step, scope);
						break;
					case OpCode.push_parameter:
						operators.OpCallFunction.push_parameter(currentRunFrame, step, scope);
						break;
					case OpCode.call_function:
						operators.OpCallFunction.exec(currentRunFrame, step, scope);
						break;
					case OpCode.function_return:
						currentRunFrame.hasCallReturn = true;
						operators.OpCallFunction.exec_return(currentRunFrame, step, scope);

						break;

					case OpCode.new_instance:
						operators.OpCreateInstance.exec(currentRunFrame, step, scope);
						break;
					case OpCode.init_staticclass:
						operators.OpCreateInstance.init_static(currentRunFrame, step, scope);
						break;
					case OpCode.new_instance_class:
						operators.OpCreateInstance.exec_instanceClass(currentRunFrame, step, scope);
						break;
					case OpCode.prepare_constructor_argement:
						operators.OpCreateInstance.prepareConstructorArgements(currentRunFrame, step, scope);
						break;
					case OpCode.prepare_constructor_class_argement:
						operators.OpCreateInstance.prepareConstructorClassArgements(currentRunFrame, step, scope);
						break;
					case OpCode.push_parameter_class:
						operators.OpCreateInstance.push_parameter_class(currentRunFrame, step, scope);
						break;
					case OpCode.access_dot:
						operators.OpAccess_Dot.exec_dot(currentRunFrame, step, scope);
						break;
					case OpCode.access_dot_byname:
						operators.OpAccess_Dot.exec_dot_byname(currentRunFrame, step, scope);
						break;
					case OpCode.bracket_access:
						operators.OpAccess_Dot.exec_bracket_access(currentRunFrame, step, scope);
						break;
					case OpCode.bracket_byname:
						operators.OpAccess_Dot.exec_dot_byname(currentRunFrame, step, scope);
						break;
					case OpCode.access_method:
						operators.OpAccess_Dot.exec_method(currentRunFrame, step, scope);
						break;
					case OpCode.delete_prop:
						operators.OpDynamicProperty.exec_delete(currentRunFrame, step, scope);
						break;
					case OpCode.set_dynamic_prop:
						operators.OpDynamicProperty.exec_set_dynamic_prop(currentRunFrame, step, scope);
						break;
					case OpCode.try_read_getter:
						operators.OpPropGetSet.exec_try_read_prop(currentRunFrame, step, scope);
						break;
					case OpCode.try_write_setter:
						operators.OpPropGetSet.exec_try_write_prop(currentRunFrame, step, scope);
						break;
					case OpCode.array_push:
						operators.OpArray.exec_Push(currentRunFrame, step, scope);
						break;
					case OpCode.array_create:
						operators.OpArray.exec_create(currentRunFrame, step, scope);
						break;
					case OpCode.vectorAccessor_bind:
						operators.OpVector.exec_AccessorBind(currentRunFrame, step, scope);
						break;
					case OpCode.vector_push:
						operators.OpVector.exec_push(currentRunFrame, step, scope);
						break;
					case OpCode.vector_pusharray:
						operators.OpVector.exec_pusharray(currentRunFrame, step, scope);
						break;
					case OpCode.vector_pushvector:
						operators.OpVector.exec_pushVector(currentRunFrame, step, scope);
						break;
					case OpCode.vectorAccessor_convertidx:
						operators.OpVector.exec_AccessorBind_ConvertIdx(currentRunFrame, step, scope);
						break;
					case OpCode.vector_initfrmdata:
						operators.OpVector.exec_initfromdata(currentRunFrame, step, scope);
						break;
					case OpCode.link_outpackagevairable:
						operators.OpLinkOutPackageScope.exec_link(currentRunFrame, step, scope);
						break;
					case OpCode.flag_call_super_constructor:
						currentRunFrame.endStepNoError();
						break;
					case OpCode.forin_get_enumerator:
						operators.OpForIn.forin_get_enumerator(currentRunFrame, step, scope);
						break;
					case OpCode.enumerator_movenext:
						operators.OpForIn.enumerator_movenext(currentRunFrame, step, scope);
						break;
					case OpCode.enumerator_current:
						operators.OpForIn.enumerator_current(currentRunFrame, step, scope);
						break;
					case OpCode.enumerator_close:
						operators.OpForIn.enumerator_close(currentRunFrame, step, scope);
						break;
					case OpCode.foreach_get_enumerator:
						operators.OpForIn.foreach_get_enumerator(currentRunFrame, step, scope);
						break;
					case OpCode.logic_is:
						operators.OpLogic.exec_IS(currentRunFrame, step, scope);
						break;
					case OpCode.logic_instanceof:
						operators.OpLogic.exec_instanceof(currentRunFrame, step, scope);
						break;
					case OpCode.convert_as:
						operators.OpLogic.exec_AS(currentRunFrame, step, scope);
						break;
					case OpCode.logic_in:
						operators.OpLogic.exec_In(currentRunFrame, step, scope);
						break;
					case OpCode.unary_typeof:
						operators.OpTypeOf.exec_TypeOf(currentRunFrame, step, scope);
						break;
					case OpCode.function_create:
						{
							rtArray arr = (rtArray)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							int funcid = ((rtInt)arr.innerArray[0]).value;
							bool ismethod = ((rtBoolean)arr.innerArray[1]).value;

							rtFunction function = new rtFunction(funcid, ismethod);
							function.bind(scope);
							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).directSet(function);

							currentRunFrame.endStepNoError();
						}
						break;
					case OpCode.yield_return:

						operators.OpCallFunction.exec_yieldreturn(currentRunFrame, step, scope);

						break;
					case OpCode.yield_continuetoline:
						{
							//跳转继续下一次yield
							currentRunFrame.codeLinePtr = ((rtInt)scope.memberData[scope.memberData.Length - 2].getValue()).value - 1;
							currentRunFrame.endStepNoError();
						}
						break;
					case OpCode.yield_break:
						currentRunFrame.hasCallReturn = true;
						currentRunFrame.returnSlot.directSet(rtUndefined.undefined);
						currentRunFrame.endStep(step);
						break;
					case OpCode.call_function_notcheck:
						operators.OpCallFunction.exec_notcheck(currentRunFrame, step, scope);
						break;
					case OpCode.cast_int_number:
						{
							var v1 = step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue((double)((rtInt)v1).value);
							currentRunFrame.endStepNoError();
							break;
						}
					case OpCode.cast_number_int:
						{
							var v1 = step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(TypeConverter.ConvertToInt(v1, currentRunFrame, null));
							currentRunFrame.endStepNoError();
							break;
						}
					case OpCode.cast_uint_number:
						{
							var v1 = step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue((double)((rtUInt)v1).value);
							currentRunFrame.endStepNoError();
							break;
						}
					case OpCode.cast_number_uint:
						{
							var v1 = step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(TypeConverter.ConvertToUInt(v1, currentRunFrame, null));
							currentRunFrame.endStepNoError();
							break;
						}
					case OpCode.cast_int_uint:
						{
							var v1 = step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue((uint)((rtInt)v1).value);
							currentRunFrame.endStepNoError();
							break;
						}
					case OpCode.cast_uint_int:
						{
							var v1 = step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue((int)((rtUInt)v1).value);
							currentRunFrame.endStepNoError();
							break;
						}
					case OpCode.push_parameter_skipcheck_storetoheap:
						operators.OpCallFunction.push_parameter_skipcheck_stroetoheap(currentRunFrame, step, scope);
						break;
					case OpCode.push_parameter_skipcheck_storetostack:
						operators.OpCallFunction.push_parameter_skipcheck_stroetostack(currentRunFrame, step, scope);
						break;
					case OpCode.push_parameter_skipcheck_testnative:
						operators.OpCallFunction.push_parameter_skipcheck_testnative(currentRunFrame, step, scope);
						break;
					case OpCode.push_parameter_nativeconstpara:
						operators.OpCallFunction.push_parameter_nativeconstpara(currentRunFrame, step, scope);
						break;
					case OpCode.push_parameter_para:
						operators.OpCallFunction.push_parameter_para(currentRunFrame, step, scope);
						break;
					case OpCode.make_para_scope_method:
						operators.OpCallFunction.create_paraScope_Method(currentRunFrame, step, scope);
						break;
					case OpCode.make_para_scope_withsignature:
						operators.OpCallFunction.create_paraScope_WithSignature(currentRunFrame, step, scope);
						break;
					case OpCode.make_para_scope_method_notnativeconstpara_allparaonstack:
						operators.OpCallFunction.create_paraScope_Method_NotNativeConstPara_AllParaOnStack(currentRunFrame, step, scope);
						break;
					case OpCode.make_para_scope_withsignature_allparaonstack:
						operators.OpCallFunction.create_paraScope_WithSignature_AllParaOnStack(currentRunFrame, step, scope);
						break;
					case OpCode.make_para_scope_method_noparameters:
						operators.OpCallFunction.create_paraScope_Method_NoParameters(currentRunFrame, step, scope);
						break;
					case OpCode.make_para_scope_withsignature_noparameters:
						operators.OpCallFunction.create_paraScope_WithSignature_NoParameters(currentRunFrame, step, scope);
						break;
					case OpCode.function_return_funvoid:
						{
							currentRunFrame.hasCallReturn = true;
							currentRunFrame.returnSlot.directSet(rtUndefined.undefined);
							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.function_return_nofunction:
						{
							currentRunFrame.hasCallReturn = true;
							RunTimeValueBase result = step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset);
							currentRunFrame.returnSlot.directSet(result);
							currentRunFrame.endStep(step);
						}
						break;
					case OpCode.function_return_funvoid_notry:
						{
							currentRunFrame.returnSlot.directSet(rtUndefined.undefined);
							currentRunFrame.codeLinePtr = currentRunFrame.stepCount;
						}
						break;
					case OpCode.function_return_nofunction_notry:
						{
							currentRunFrame.returnSlot.directSet(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							currentRunFrame.codeLinePtr = currentRunFrame.stepCount;
						}
						break;
					case OpCode.call_function_notcheck_notreturnobject:
						{

							currentRunFrame.funCaller.callbacker = currentRunFrame.funCaller;
							currentRunFrame.funCaller.returnSlot = step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset);
							currentRunFrame.funCaller.doCall_allcheckpass();
							currentRunFrame.funCaller = null;

							break;
						}
					case OpCode.call_function_notcheck_notreturnobject_notnative:
						{
							currentRunFrame.funCaller.callbacker = currentRunFrame.funCaller;
							currentRunFrame.funCaller.returnSlot = step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset);
							currentRunFrame.funCaller.returnSlot.directSet(step.arg2.getValue(null, null, 0));
							currentRunFrame.funCaller.doCall_allcheckpass_nonative_hassetreturndefault();
							currentRunFrame.funCaller = null;

							break;
						}
					case OpCode.call_function_notcheck_notreturnobject_notnative_method:
						{
							var funCaller = currentRunFrame.funCaller;

							funCaller.callbacker = funCaller;
							funCaller.returnSlot = step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset);
							funCaller.returnSlot.directSet(step.arg2.getValue(null, null, 0));
							//currentRunFrame.funCaller.doCall_allcheckpass_nonative_hassetreturndefault_method();
							callBlock_Method_NoHeap(swc.blocks[ funCaller.toCallFunc.blockid], 
								funCaller.returnSlot, step.token, funCaller.callbacker, funCaller.functionThisPointer );

							currentRunFrame.funCaller = null;

							break;
						}
					case OpCode.sub_number_number:
						{
							double a1 = ((rtNumber)step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;
							double a2 = ((rtNumber)step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset)).value;

							step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(a1 - a2);//new ASBinCode.rtData.rtNumber(a1.value - a2.value));
							currentRunFrame.endStepNoError();
							break;
						}
					case OpCode.if_equality_num_num_jmp_notry:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 == n2)
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.True);
								currentRunFrame.codeLinePtr += step.jumoffset+1; //jumtooffset位置为label,因此直接可以再跳过一层
								
							}
							else
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.False);
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_equality_num_num_jmp_notry_noreference:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 == n2)
							{
								currentRunFrame.codeLinePtr += step.jumoffset+1; //jumtooffset位置为label,因此直接可以再跳过一层
								
							}
							else
							{
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_not_equality_num_num_jmp_notry:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 != n2)
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.True);
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.False);
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_not_equality_num_num_jmp_notry_noreference:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 != n2)
							{
								currentRunFrame.codeLinePtr += step.jumoffset+1;
							}
							else
							{
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_le_num_jmp_notry:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 <= n2)
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.True);
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.False);
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_le_num_jmp_notry_noreference:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 <= n2)
							{
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_lt_num_jmp_notry:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 < n2)
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.True);
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.False);
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_lt_num_jmp_notry_noreference:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 < n2)
							{
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_ge_num_jmp_notry:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 >= n2)
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.True);
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.False);
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_ge_num_jmp_notry_noreference:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 >= n2)
							{
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_gt_num_jmp_notry:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 > n2)
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.True);
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								step.reg.getSlot(scope, currentRunFrame.stack, currentRunFrame.offset).setValue(ASBinCode.rtData.rtBoolean.False);
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					case OpCode.if_gt_num_jmp_notry_noreference:
						{
							var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope, currentRunFrame.stack, currentRunFrame.offset));
							if (n1 > n2)
							{
								currentRunFrame.codeLinePtr += step.jumoffset+1;
								
							}
							else
							{
								++currentRunFrame.codeLinePtr;
							}
						}
						break;
					default:
						throw new Exception(step.opCode + "操作未实现");
				}


#endregion

#endif

			}

			return true;
		}

		private error.InternalError receive_error;
		internal void exitStackFrameWithError(error.InternalError error, StackFrame raiseframe)
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
				throw new EngineException();
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

		internal void throwWhenMakeObjFailed(ASRunTimeException exception)
		{
			if (currentRunFrame != null)
			{
				currentRunFrame.receiveErrorFromStackFrame(new error.InternalError(swc, SourceToken.Empty, exception.ToString()));
			}
			else
			{
				if (infoOutput != null)
				{
					infoOutput.Error("运行时错误");
					infoOutput.Error(exception.ToString());
				}
			}
		}

		public void throwOrShowError(ASRunTimeException exception)
		{
			if (currentRunFrame != null)
			{
				currentRunFrame.throwAneException(SourceToken.Empty, exception.ToString());
			}
			else
			{
				if (infoOutput != null)
				{
					infoOutput.Error("运行时错误");
					infoOutput.Error(exception.ToString());
				}
			}
		}

		internal void outPutErrorMessage(error.InternalError err)
		{
			if (infoOutput != null)
			{

				infoOutput.Error("运行时错误");
				//Console.WriteLine("file :" + err.token.sourceFile);
				//Console.WriteLine("line :" + err.token.line + " ptr :" + err.token.ptr);

				infoOutput.Error(err.getErrorInfo());

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

				while (err.callStack != null && err.callStack.Count > 0)
				{
					_temp.Push(err.callStack.Pop());
					displayStackFrame = _temp.Peek();
				}

				foreach (var item in _temp)
				{
					if (item.codeLinePtr < item.block.instructions.Length)
					{
						infoOutput.Error(item.block.name + " at file:" + item.block.instructions[item.codeLinePtr].token.sourceFile);
						infoOutput.Error("\t\t line:" + (item.block.instructions[item.codeLinePtr].token.line + 1) + " ptr:" + (item.block.instructions[item.codeLinePtr].token.ptr + 1));
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

				if (item.block == blankBlock)
				{
					continue;
				}

				if (item.codeLinePtr < item.block.instructions.Length)
				{
					sb.Append("\tat ");
					sb.Append(item.block.name);
					sb.Append(" [");
					sb.Append(item.block.instructions[item.codeLinePtr].token.sourceFile);
					sb.Append(" ");
					sb.Append(item.block.instructions[item.codeLinePtr].token.line + 1);
					sb.Append(" ptr:");
					sb.Append(item.block.instructions[item.codeLinePtr].token.ptr + 1);
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
						return (((rtBoolean)rv).value);
					case RunTimeDataType.rt_int:
						return TypeConverter.ConvertToInt(rv);
					case RunTimeDataType.rt_uint:
						return TypeConverter.ConvertToUInt(rv, null, null);
					case RunTimeDataType.rt_number:
						return TypeConverter.ConvertToNumber(rv);
					case RunTimeDataType.rt_string:
						return TypeConverter.ConvertToString(rv, null, null);
					case RunTimeDataType.rt_null:
						return null;
					default:
						if (rv.rtType > RunTimeDataType.unknown)
						{
							var cls = swc.getClassByRunTimeDataType(rv.rtType);
							if (cls.staticClass == null)
							{
								try
								{
									return linktypemapper.getLinkType(rv.rtType);
								}
								catch (RuntimeLinkTypeMapper.TypeLinkClassException)
								{
									return obj;
								}
							}

						}

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
			catch (RuntimeLinkTypeMapper.TypeLinkClassException e)
			{
				throw new ASRunTimeException("函数参数转换失败", e);
			}
			catch (KeyNotFoundException e)
			{
				throw new ASRunTimeException("函数参数转换失败", e);
			}
			catch (ArgumentException e)
			{
				throw new ASRunTimeException("函数参数转换失败", e);
			}
			catch (InvalidCastException e)
			{
				throw new ASRunTimeException("函数参数转换失败", e);
			}
			catch (IndexOutOfRangeException e)
			{
				throw new ASRunTimeException("函数参数转换失败", e);
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
			lock (this)
			{
				try
				{
					initPlayer();

					var cls = getClass(classname);
					if (cls == null)
					{
						throw new ASRunTimeException(classname + "类型未找到", string.Empty);
					}

					CallBlankBlock(null);

					if (!operators.InstanceCreator.init_static_class(cls, this, SourceToken.Empty))
					{
						throw new ASRunTimeException("初始化静态实例时失败", string.Empty);
					}

					var sig = swc.functions[cls.constructor_functionid].signature;
					RunTimeValueBase vb1 = null;
					RunTimeValueBase vb2 = null;
					RunTimeValueBase vb3 = null;

					RunTimeValueBase[] paraArgs = null;

					currentRunFrame.call_parameter_slotCount += argcount;
					int slotidx = currentRunFrame.baseBottomSlotIndex; //currentRunFrame.offset + currentRunFrame.block.totalRegisters + 1 + 1;
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
					bool issuccess = runFunction(_createinstance, _buildin_class_, currentRunFrame._tempSlot1, SourceToken.Empty, out err,
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
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
						else
						{
							throw new ASRunTimeException("对象创建失败", string.Empty);
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
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}

						return v as rtObject;
					}

				}
				finally
				{
					clearEnv();
				}

			}
		}

#endregion

#region getMethod

		public rtFunction getMethod(rtObjectBase thisObj, string name)
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
		public object invokeMethod(string type, string methodname, object v1)
		{
			return invokeMethod(type, methodname, 1, v1, null, null, null, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1, object v2)
		{
			return invokeMethod(type, methodname, 2, v1, v2, null, null, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1, object v2, object v3)
		{
			return invokeMethod(type, methodname, 3, v1, v2, v3, null, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1, object v2, object v3, object v4)
		{
			return invokeMethod(type, methodname, 4, v1, v2, v3, v4, null, null);
		}
		public object invokeMethod(string type, string methodname, object v1, object v2, object v3, object v4, object v5)
		{
			return invokeMethod(type, methodname, 5, v1, v2, v3, v4, v5, null);
		}
		public object invokeMethod(string type, string methodname, int argcount, object v1, object v2, object v3, object v4, object v5, params object[] args)
		{
			lock (this)
			{
				var cls = getClassStaticInstance(type);
				return invokeMethod(cls, methodname, argcount, v1, v2, v3, v4, v5, args);
			}

		}


		public object invokeMethod(rtObjectBase thisObj, string methodname)
		{
			return invokeMethod(thisObj, methodname, 0, null, null, null, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, string methodname, object v1)
		{
			return invokeMethod(thisObj, methodname, 1, v1, null, null, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, string methodname, object v1, object v2)
		{
			return invokeMethod(thisObj, methodname, 2, v1, v2, null, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, string methodname, object v1, object v2, object v3)
		{
			return invokeMethod(thisObj, methodname, 3, v1, v2, v3, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, string methodname, object v1, object v2, object v3, object v4)
		{
			return invokeMethod(thisObj, methodname, 4, v1, v2, v3, v4, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, string methodname, object v1, object v2, object v3, object v4, object v5)
		{
			return invokeMethod(thisObj, methodname, 5, v1, v2, v3, v4, v5, null);
		}

		public object invokeMethod(rtObjectBase thisObj, string methodname, int argcount, object v1, object v2, object v3, object v4, object v5, params object[] args)
		{
			lock (this)
			{
				var method = getMethod(thisObj, methodname);
				if (method == null)
					throw new ASRunTimeException("方法未找到", string.Empty);

				return invokeMethod(thisObj, method, argcount, v1, v2, v3, v4, v5, args);
			}

		}


		public object invokeMethod(rtObjectBase thisObj, rtFunction method)
		{
			return invokeMethod(thisObj, method, 0, null, null, null, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, rtFunction method, object v1)
		{
			return invokeMethod(thisObj, method, 1, v1, null, null, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, rtFunction method, object v1, object v2)
		{
			return invokeMethod(thisObj, method, 2, v1, v2, null, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, rtFunction method, object v1, object v2, object v3)
		{
			return invokeMethod(thisObj, method, 3, v1, v2, v3, null, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, rtFunction method, object v1, object v2, object v3, object v4)
		{
			return invokeMethod(thisObj, method, 4, v1, v2, v3, v4, null, null);
		}
		public object invokeMethod(rtObjectBase thisObj, rtFunction method, object v1, object v2, object v3, object v4, object v5)
		{
			return invokeMethod(thisObj, method, 5, v1, v2, v3, v4, v5, null);
		}
		public object invokeMethod(rtObjectBase thisObj, rtFunction method, int argcount, object v1, object v2, object v3, object v4, object v5, params object[] args)
		{
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");
			lock (this)
			{
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

					int slotidx = currentRunFrame.baseBottomSlotIndex; //currentRunFrame.offset + currentRunFrame.block.totalRegisters + 1 + 1;
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
					bool issuccess = runFunction(method, thisObj, currentRunFrame._tempSlot1, SourceToken.Empty, out err
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
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
					}
					else
					{
						while (step())
						{

						}
						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
						else
						{
							throw new ASRunTimeException("方法调用失败", string.Empty);
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
						throw new ASRunTimeException("返回值转化失败", string.Empty);
					}

				}


				finally
				{
					clearEnv();
				}
			}

		}


		internal object InvokeFunctionWapper(FunctionWapper wapper, int argcount, object v1, object v2, object v3, object v4, object v5, object[] args)
		{
			return InvokeFunction(wapper.function, argcount, v1, v2, v3, v4, v5, args);
		}

		public object InvokeFunction(rtFunction function, int argcount, object v1, object v2, object v3, object v4, object v5, object[] args)
		{

			lock (this)
			{
				bool isblank = false;
				if (currentRunFrame == null)
				{
					isblank = true;
					CallBlankBlock(null);
				}

				try
				{
					var method = function;

					int startOffset = currentRunFrame.baseBottomSlotIndex + argcount;
					if (startOffset + swc.blocks[swc.functions[method.functionId].blockid].totalStackSlots + 1 + 1 >= STACKSLOTLENGTH || !stackframePool.hasCacheObj())
					{
						throw new ASRunTimeException("stack overflow", stackTrace(0));
					}


					//int flag = getRuntimeStackFlag();

					//CallBlankBlock(null);

					var signature = swc.functions[method.functionId].signature;

					RunTimeValueBase vb1 = null;
					RunTimeValueBase vb2 = null;
					RunTimeValueBase vb3 = null;
					RunTimeValueBase vb4 = null;
					RunTimeValueBase vb5 = null;

					RunTimeValueBase[] paraArgs = null;

					currentRunFrame.call_parameter_slotCount += argcount;

					int slotidx = currentRunFrame.baseBottomSlotIndex; //currentRunFrame.offset + currentRunFrame.block.totalRegisters + 1 + 1;
					int stslotidx = slotidx;
					try
					{

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
					}
					catch
					{
						//清理
						currentRunFrame.call_parameter_slotCount -= argcount;
						for (int i = stslotidx; i < slotidx; i++)
						{
							stackSlots[slotidx].clear();
						}

						//step_toStackflag(flag);

						throw;
					}




					error.InternalError err;
					RunTimeValueBase v = null;
					bool issuccess = runFunction(method, method.this_pointer, currentRunFrame._tempSlot1, SourceToken.Empty, out err
						, vb1, vb2, vb3, vb4, vb5, paraArgs
						);

					currentRunFrame.call_parameter_slotCount -= argcount;
					for (int i = stslotidx; i < slotidx; i++)
					{
						stackSlots[slotidx].clear();
					}

					if (issuccess)
					{
						v = (RunTimeValueBase)currentRunFrame._tempSlot1.getValue().Clone();

						//step_toStackflag(flag);
						if (isblank)
						{
							while (step()) ;
						}

						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
					}
					else
					{
						//step_toStackflag(flag);
						if (isblank)
						{
							while (step()) ;
						}

						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
						else
						{
							throw new ASRunTimeException("方法调用失败", string.Empty);
						}
					}

					if (signature.returnType == RunTimeDataType.fun_void)
					{
						return rtUndefined.undefined;
					}

					Type rttype = null;
					try
					{
						rttype = linktypemapper.getLinkType(signature.returnType);
					}
					catch (RuntimeLinkTypeMapper.TypeLinkClassException)
					{

					}


					object obj;
					if (rttype != null)
					{
						if (linktypemapper.rtValueToLinkObject(v, rttype, swc, true, out obj))
						{
							return convertReturnValue(obj);
						}
						else
						{
							throw new ASRunTimeException("返回值转化失败", string.Empty);
						}
					}
					else
					{
						return convertReturnValue(v);
					}
				}
				catch (Exception ex)
				{
					if (isblank)
					{
						clearEnv();

						throwOrShowError(new ASRunTimeException("方法调用失败", ex));
						//return null;
					}
					throw;
				}
				finally
				{
					if (isblank)
					{
						clearEnv();
					}
				}




			}


		}


		public void MakeICrossExtendAdapterEnvironment(ICrossExtendAdapter adapter, ASBinCode.rtti.Class as3class)
		{
			if (adapter == null)
				throw new ArgumentNullException("adapter");
			if (as3class == null)
			{
				throw new ArgumentNullException("as3class");
			}
			if (adapter.AS3Object != null)
			{
				throw new ArgumentException("adapter对象已经组装完成");
			}

			ASBinCode.rtData.rtObject obj = createInstance(as3class.instanceClass);
			ASBinCode.rtti.LinkSystemObject lo = (ASBinCode.rtti.LinkSystemObject)obj.value;
			lo.SetLinkData(adapter);
			adapter.SetAS3RuntimeEnvironment(this, as3class.instanceClass, obj);

		}

		private ASBinCode.rtData.rtObject createInstance(ASBinCode.rtti.Class as3class)
		{

			lock (this)
			{
				var cls = as3class;
				if (cls == null)
				{
					throw new ASRunTimeException("参数as3class不能为空", string.Empty);
				}

				bool isblank = false;
				if (currentRunFrame == null)
				{
					isblank = true;
					CallBlankBlock(null);
				}

				try
				{
					initPlayer();

					if (!operators.InstanceCreator.init_static_class(cls, this, SourceToken.Empty))
					{
						throw new ASRunTimeException("初始化静态实例时失败", string.Empty);
					}

					var sig = swc.functions[cls.constructor_functionid].signature;
					RunTimeValueBase vb1 = null;
					RunTimeValueBase vb2 = null;
					RunTimeValueBase vb3 = null;

					RunTimeValueBase[] paraArgs = null;


					error.InternalError err;
					bool issuccess = runFunction(_createinstance, _buildin_class_, currentRunFrame._tempSlot1, SourceToken.Empty, out err,
						static_instance[cls.staticClass.classid],
						new rtInt(0), vb1, vb2, vb3, paraArgs);

					if (!issuccess)
					{
						if (isblank)
						{
							while (step()) ;
						}
						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
						else
						{
							throw new ASRunTimeException("跨脚本适配器创建失败", string.Empty);
						}
					}
					else
					{
						var v = currentRunFrame._tempSlot1.getValue().Clone();

						if (isblank)
						{
							while (step()) ;
						}

						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}

						return v as rtObject;
					}

				}
				catch (Exception ex)
				{
					if (isblank)
					{
						clearEnv();

						throwOrShowError(new ASRunTimeException("跨脚本适配器创建失败", ex));
						//return null;
					}
					throw;
				}
				finally
				{
					if (isblank)
					{
						clearEnv();
					}
				}
			}


		}



#endregion

#region getClassStaticInstance
		private rtObjectBase getClassStaticInstance(string type)
		{

			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");

			try
			{
				initPlayer();

				var cls = getClass(type);
				if (cls == null)
				{
					throw new ASRunTimeException(type + "类型未找到", string.Empty);
				}

				CallBlankBlock(null);

				if (!operators.InstanceCreator.init_static_class(cls, this, SourceToken.Empty))
				{
					throw new ASRunTimeException("初始化静态实例时失败", string.Empty);
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

		public object getMemberValue(rtObjectBase thisObj, string memberPath)
		{
			return getMemberValue(thisObj, memberPath, null);
		}
		/// <summary>
		/// 访问成员的值
		/// </summary>
		/// <param name="thisObj"></param>
		/// <param name="memberPath"></param>
		/// <param name="indexArgs">如果是需要用方括号访问的成员，则输入方括号内的参数</param>
		/// <returns></returns>
		public object getMemberValue(rtObjectBase thisObj, string memberPath, object indexArgs)
		{
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");
			lock (this)
			{
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
					var signature = swc.functions[_getMemberValue.functionId].signature;

					RunTimeValueBase p1 = rtNull.nullptr;
					RunTimeValueBase p2 = rtNull.nullptr;
					RunTimeValueBase extpath = rtNull.nullptr;
					RunTimeValueBase index = rtNull.nullptr;

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
							((rtArray)extpath).innerArray.Add(new rtString(path[i]));
						}
					}

					if (indexArgs != null)
					{
						index = prepareParameter(signature, 4, indexArgs, currentRunFrame._tempSlot1);
					}

					error.InternalError err;
					RunTimeValueBase v = null;
					bool issuccess = runFunction(_getMemberValue, _getMemberValue.this_pointer, currentRunFrame._tempSlot2, SourceToken.Empty, out err
						, thisObj, p1, p2, extpath, index);

					if (issuccess)
					{
						v = (RunTimeValueBase)currentRunFrame._tempSlot2.getValue().Clone();
						while (step())
						{

						}
						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
					}
					else
					{
						while (step())
						{

						}
						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
						else
						{
							throw new ASRunTimeException("成员访问失败", string.Empty);
						}
					}
					object obj;
					if (linktypemapper.rtValueToLinkObject(v, linktypemapper.getLinkType(signature.returnType), swc, true, out obj))
					{
						return convertReturnValue(obj);
					}
					else
					{
						throw new ASRunTimeException("成员获取返回值转化失败", string.Empty);
					}
				}

				finally
				{
					clearEnv();
				}
			}


		}

		public void setMemberValue(rtObjectBase thisObj, string memberPath, object value)
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
		public void setMemberValue(rtObjectBase thisObj, string memberPath, object value, object indexArgs)
		{
			if (currentRunFrame != null)
				throw new InvalidOperationException("状态异常,不能在运行中调用此方法");
			lock (this)
			{
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

					RunTimeValueBase p1 = rtNull.nullptr;
					RunTimeValueBase extpath = ASBinCode.rtData.rtNull.nullptr;
					RunTimeValueBase index = rtNull.nullptr;

					if (path.Length > 0)
					{
						p1 = new rtString(path[0]);
					}
					if (path.Length > 1)
					{
						extpath = new rtArray();
						for (int i = 1; i < path.Length; i++)
						{
							((rtArray)extpath).innerArray.Add(new rtString(path[i]));
						}
					}


					if (indexArgs != null)
					{
						index = prepareParameter(signature, 4, indexArgs, currentRunFrame._tempSlot1);
					}

					error.InternalError err;

					bool issuccess = runFunction(_setMemberValue, _setMemberValue.this_pointer, currentRunFrame._tempSlot2, SourceToken.Empty, out err
						, thisObj, setvalue, p1, extpath, index);

					if (issuccess)
					{

						while (step())
						{

						}
						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
					}
					else
					{
						while (step())
						{

						}
						if (err != null)
						{
							throw new ASRunTimeException(err.message, err.getStackTrace());
						}
						else
						{
							throw new ASRunTimeException("成员赋值失败", String.Empty);
						}
					}
				}
				finally
				{
					clearEnv();
				}
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
			setMemberValue(type, memberPath, value, null);
		}

		/// <summary>
		/// 设置静态成员的值
		/// </summary>
		/// <param name="type"></param>
		/// <param name="memberPath"></param>
		/// <param name="value"></param>
		/// <param name="indexArgs"></param>
		public void setMemberValue(string type, string memberPath, object value, object indexArgs)
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
		public rtObjectBase createByteArrayObject(out flash.utils.ByteArray byteArray)
		{


			var thisObj = createInstance("flash.utils.ByteArray");
			byteArray =
					(flash.utils.ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			byteArray.bindAS3Object = thisObj;

			return thisObj;
		}


#endregion

		delegate Type ddd(Type t);

		public Delegate WapperFunctionDelegate(RunTimeValueBase func, ASBinCode.rtti.Class cls, Type delegateType, Action<FunctionWapper> createDelegate)
		{
			ASBinCode.rtData.rtFunction function;

			if (func.rtType == RunTimeDataType.rt_function)
			{
				function = (ASBinCode.rtData.rtFunction)func;
			}
			else if (func.rtType == swc.FunctionClass.getRtType())
			{
				function = (ASBinCode.rtData.rtFunction)
					TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)func);
			}
			else
			{
				throw new ASRunTimeException("目标不是Function", stackTrace(0));
			}

			if (function.ismethod)
			{
				rtObjectBase rtObject = function.this_pointer as rtObjectBase;
				if (rtObject == null)
				{
#if DEBUG
					throw new InvalidOperationException("method的thispointer不应该为空且是rtObject");
#else

					throw new ASRunTimeException("method的thispointer不应该为空且是rtObject",stackTrace(0));
#endif
				}
				var thisobjtype = swc.getClassByRunTimeDataType(rtObject.rtType);
				if (thisobjtype.isLink_System)
				{
					var functiondefine = swc.functions[function.functionId];
					if (functiondefine.isNative)
					{
						var nativefunction = swc.getNativeFunction(function.functionId);
						if (nativefunction == null)
						{
							throw new ASRunTimeException("函数" + functiondefine.name + "([" + functiondefine.native_name + "])" + "的本地代码没找到", stackTrace(0));
						}


						nativefuncs.IMethodGetter methodGetter = nativefunction as nativefuncs.IMethodGetter;
						if (methodGetter != null)
						{
							System.Reflection.MethodInfo method;
							try
							{
								method = methodGetter.GetMethodInfo(functiondefine,swc,this);
							}
							catch (System.Reflection.AmbiguousMatchException e)
							{
								throw new ASRunTimeException("尝试获取" + functiondefine.name + "的MethodInfo失败," + e.Message, stackTrace(0));
							}
							catch (ArgumentNullException e)
							{
								throw new ASRunTimeException("尝试获取" + functiondefine.name + "的MethodInfo失败," + e.Message, stackTrace(0));
							}
							catch (ArgumentException e)
							{
								throw new ASRunTimeException("尝试获取" + functiondefine.name + "的MethodInfo失败," + e.Message, stackTrace(0));
							}

							object target = ((ASBinCode.rtti.LinkSystemObject)rtObject.value).GetLinkData();

							try
							{
								return Delegate.CreateDelegate(delegateType, target, method);
							}
							catch (ArgumentNullException e)
							{
								throw new ASRunTimeException("尝试创建" + functiondefine.name + "的委托失败," + e.Message, stackTrace(0));
							}
							catch (ArgumentException e)
							{
								throw new ASRunTimeException("尝试创建" + functiondefine.name + "的委托失败," + e.Message, stackTrace(0));
							}
							catch (MissingMethodException e)
							{
								throw new ASRunTimeException("尝试创建" + functiondefine.name + "的委托失败," + e.Message, stackTrace(0));
							}
							catch (MethodAccessException e)
							{
								throw new ASRunTimeException("尝试创建" + functiondefine.name + "的委托失败," + e.Message, stackTrace(0));
							}

						}
					}





					throw new ASRunTimeException("此方法不支持创建委托", stackTrace(0));
				}

			}



			//if (function.dictWappers == null)
			//{
			//	function.dictWappers = new Dictionary<RunTimeDataType, FunctionWapper>();
			//}

			if (!function.dictWappers.ContainsKey(cls.getRtType()))
			{
				FunctionWapper wapper = new FunctionWapper(function, this);

				function.dictWappers.Add(cls.getRtType(), wapper);

				createDelegate(wapper);

				return wapper.action;
			}
			else
			{
				return function.dictWappers[cls.getRtType()].action;
			}


		}

		/// <summary>
		/// 将可枚举对象包装成IEnumerator
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public System.Collections.IEnumerator WapperIterator(ASBinCode.RunTimeValueBase v)
		{
			return new ASRuntime.nativefuncs.linksystem.Iterator(v, this);
		}

#endregion







	}
}
