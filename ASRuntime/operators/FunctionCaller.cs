using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
	class FunctionCaller : IBlockCallBack
	{
		internal sealed class FunctionCallerPool : PoolBase<FunctionCaller>
		{
			private Player player;
			public FunctionCallerPool(Player player) : base(256) { this.player = player; }

			public FunctionCaller create(StackFrame invokerFrame, SourceToken token)
			{
				FunctionCaller fc = base.create();

				fc.invokerFrame = invokerFrame;
				fc.token = token;
				fc.player = player;
				fc.check_para_id = 0;
				fc.pushedArgs = 0;
				fc.hasReleased = false;
				fc.onstackparametercount = 0;
				fc.tag = null;

				//fc.function.Clear();

				return fc;
			}
			public override void ret(FunctionCaller c)
			{
				base.ret(c);
				c.callbacker = null;
			}
		}


		internal HeapSlot[] CallFuncHeap;

		private ASBinCode.rtData.rtFunction function;

		public void SetFunction(ASBinCode.rtData.rtFunction rtFunction)
		{
			function.CopyFrom(rtFunction);

		}

		public void SetFunction(ASBinCode.rtData.rtFunction rtFunction, RunTimeValueBase thisobj)
		{
			function.CopyFrom(rtFunction);
			if (thisobj != null)
			{
				function.setThis(thisobj);
			}
		}

		public void SetFunctionThis(RunTimeValueBase thisobj)
		{
			function.setThis(thisobj);
		}

		public RunTimeValueBase functionThisPointer
		{
			get
			{
				return function.this_pointer;
			}
		}

		public bool isFuncEquals(ASBinCode.rtData.rtFunction function)
		{
			return this.function.Equals(function);
		}



		internal ASBinCode.rtti.FunctionDefine toCallFunc;

		internal int pushedArgs;

		public SLOT returnSlot;

		public SLOT _tempSlot;

		public IBlockCallBack callbacker;



		private Player player;
		private StackFrame invokerFrame;
		private SourceToken token;
		private int check_para_id;

		public object tag;

		internal int onstackparametercount;

		public FunctionCaller() : this(null, null, null)
		{

		}

		private FunctionCaller(Player player, StackFrame invokerFrame, SourceToken token)
		{
			this.player = player;
			this.invokerFrame = invokerFrame;
			this.token = token;

			check_para_id = 0;
			pushedArgs = 0;
			hasReleased = false;

			function = new ASBinCode.rtData.rtFunction(-1, false);
		}


		bool hasReleased;
		public void release()
		{
			if (!hasReleased)
			{
				//check_para_id = 0;
				//pushedArgs = 0;
				//onstackparametercount = 0;

				hasReleased = true;
				callbacker = null;


				CallFuncHeap = null;
				toCallFunc = null;
				returnSlot = null;
				_tempSlot = null;
				invokerFrame = null;
				token = null;
				tag = null;
				function.Clear();



				player.funcCallerPool.ret(this);


			}

		}

		public void loadDefineFromFunction()
		{
			toCallFunc = player.swc.functions[function.functionId];

		}


		private static ASBinCode.rtData.rtInt _cachertint = new ASBinCode.rtData.rtInt(0);
		private static ASBinCode.rtData.rtUInt _cachertuint = new ASBinCode.rtData.rtUInt(0);
		private static ASBinCode.rtData.rtNumber _cachertnumber = new ASBinCode.rtData.rtNumber(0);

		public static RunTimeValueBase getDefaultParameterValue(RunTimeDataType dt, RunTimeValueBase dv)
		{
			if (dv.rtType != dt && dt != RunTimeDataType.rt_void)
			{
				if (dt == RunTimeDataType.rt_int)
				{
					_cachertint.value = TypeConverter.ConvertToInt(dv);
					dv = _cachertint;
					//dv = new ASBinCode.rtData.rtInt(TypeConverter.ConvertToInt(dv, null, null));
				}
				else if (dt == RunTimeDataType.rt_uint)
				{
					_cachertuint.value = TypeConverter.ConvertToUInt(dv, null, null);
					dv = _cachertuint;
					//dv = new ASBinCode.rtData.rtUInt(TypeConverter.ConvertToUInt(dv, null, null));
				}
				else if (dt == RunTimeDataType.rt_number)
				{
					_cachertnumber.value = TypeConverter.ConvertToNumber(dv);
					dv = _cachertnumber;
					//dv = new ASBinCode.rtData.rtNumber(TypeConverter.ConvertToNumber(dv));
				}
				else if (dt == RunTimeDataType.rt_string)
				{
					dv = new ASBinCode.rtData.rtString(TypeConverter.ConvertToString(dv, null, null));
				}
				else if (dt == RunTimeDataType.rt_boolean)
				{
					dv = TypeConverter.ConvertToBoolean(dv, null, null);
				}
			}

			return dv;
		}


		public static RunTimeValueBase getDefaultParameterValue(ASBinCode.rtti.FunctionSignature signature, int i)
		{
			var dt = signature.parameters[i].type;
			var dv = signature.parameters[i].defaultValue.getValue(null, null);

			return getDefaultParameterValue(dt, dv);
		}



		public bool createParaScope()
		{
			if (toCallFunc.isNative)
			{
				//if (toCallFunc.native_index < 0)
				//{
				//    toCallFunc.native_index = player.swc.nativefunctionNameIndex[toCallFunc.native_name];
				//}
				//var nf = player.swc.nativefunctions[toCallFunc.native_index];
				var nf = player.swc.getNativeFunction(toCallFunc.functionid);
				if (nf == null)
				{
					invokerFrame.throwAneException(token, "函数 " + toCallFunc.name + "([" + toCallFunc.native_name + "])" + "的本地代码没找到");
					invokerFrame.endStep();
					if (callbacker != null)
					{
						callbacker.noticeRunFailed();
					}
					release();
					return false;
				}
				nf.bin = player.swc;
				if (nf.mode == NativeFunctionBase.NativeFunctionMode.const_parameter_0)
				{
					nativefuncs.NativeConstParameterFunction func = ((nativefuncs.NativeConstParameterFunction)nf);


					if (/*invokerFrame.offset +
							invokerFrame.block.totalRegisters + 1 + 1 +
							invokerFrame.call_parameter_slotCount*/ invokerFrame.baseBottomSlotIndex + invokerFrame.call_parameter_slotCount + func.TotalArgs >= Player.STACKSLOTLENGTH)
					{

						invokerFrame.throwError(new error.InternalError(invokerFrame.player.swc, token, "stack overflow"));
						invokerFrame.endStep();
						if (callbacker != null)
						{
							callbacker.noticeRunFailed();
						}
						release();

						return false;
					}
					invokerFrame.call_parameter_slotCount += func.TotalArgs;
					onstackparametercount = func.TotalArgs;

					func.prepareParameter(toCallFunc, invokerFrame.stack, invokerFrame.baseBottomSlotIndex + invokerFrame.call_parameter_slotCount /*invokerFrame.offset +
							invokerFrame.block.totalRegisters + 1 + 1 + invokerFrame.call_parameter_slotCount*/ - onstackparametercount);

					return true;
				}
			}

			ASBinCode.rtti.FunctionSignature signature = toCallFunc.signature;


			if (invokerFrame.baseBottomSlotIndex + invokerFrame.call_parameter_slotCount + signature.onStackParameters  /*invokerFrame.offset + 
                invokerFrame.block.totalRegisters + 1 + 1 + 
                invokerFrame.call_parameter_slotCount+signature.onStackParameters*/ >= Player.STACKSLOTLENGTH)
			{

				invokerFrame.throwError(new error.InternalError(invokerFrame.player.swc, token, "stack overflow"));
				invokerFrame.endStep();
				if (callbacker != null)
				{
					callbacker.noticeRunFailed();
				}
				release();

				return false;
			}
			invokerFrame.call_parameter_slotCount += signature.onStackParameters;
			onstackparametercount = signature.onStackParameters;

			CallFuncHeap =
				player.genHeapFromCodeBlock(player.swc.blocks[toCallFunc.blockid]);

			var parameters = signature.parameters; int len = parameters.Count;

			for (int i = 0; i < len; i++)
			{
				var parameter = parameters[i];
				if (parameter.defaultValue != null)
				{

					var dv = getDefaultParameterValue(signature, i);

					_storeArgementToSlot(i, dv);
				}
				else if (parameter.isPara)
				{

					_storeArgementToSlot(i, new ASBinCode.rtData.rtArray());
				}
				//else
				//{
				//    _storeArgementToSlot(i, ASBinCode.rtData.rtUndefined.undefined);
				//}
			}

			return true;

		}

		private SLOT _getArgementSlot(int id)
		{
			var signature = toCallFunc.signature;

			if (signature.onStackParameters > 0)
			{
				ASBinCode.rtti.FunctionParameter fp = signature.parameters[id];

				if (fp.isOnStack)
				{
					StackSlotAccessor r = (StackSlotAccessor)fp.varorreg;
					//int index = invokerFrame.offset + invokerFrame.block.totalRegisters + 1 + 1 + invokerFrame.call_parameter_slotCount + r._index;
					int index = invokerFrame.baseBottomSlotIndex + invokerFrame.call_parameter_slotCount + r._index;
					return invokerFrame.stack[index];
				}
				else
				{
					return CallFuncHeap[((Variable)fp.varorreg).indexOfMembers];
				}
			}
			else
			{
				return CallFuncHeap[id];
			}
		}

		private void _storeArgementToSlot(int id, RunTimeValueBase v)
		{
			var signature = toCallFunc.signature;

			if (signature.onStackParameters > 0)
			{
				ASBinCode.rtti.FunctionParameter fp = signature.parameters[id];

				if (fp.isOnStack)
				{
					StackSlotAccessor r = (StackSlotAccessor)fp.varorreg;
					//int index = invokerFrame.offset + invokerFrame.block.totalRegisters + 1 + 1+ invokerFrame.call_parameter_slotCount + r._index;
					int index = invokerFrame.baseBottomSlotIndex + invokerFrame.call_parameter_slotCount + r._index;
					invokerFrame.stack[index].directSet(v);
				}
				else
				{
					CallFuncHeap[((Variable)fp.varorreg).indexOfMembers].directSet(
						v

					);
				}
			}
			else
			{
				CallFuncHeap[id].directSet(v);
			}
		}

		private bool doPushNativeModeConstParameter(RunTimeValueBase argement, int id, out bool success)
		{
			if (toCallFunc.isNative)
			{
				//if (toCallFunc.native_index < 0)
				//{
				//	toCallFunc.native_index = player.swc.nativefunctionNameIndex[toCallFunc.native_name];
				//}
				//var nf = player.swc.nativefunctions[toCallFunc.native_index];
				var nf = player.swc.getNativeFunction(toCallFunc);

				if (nf.mode == NativeFunctionBase.NativeFunctionMode.const_parameter_0)
				{
					pushParameterNativeModeConstParameter(
						(nativefuncs.NativeConstParameterFunction)nf,
						argement,
						id,
						out success
						);
					return true;
				}
			}
			success = false;
			return false;
		}

		public void pushParameterNativeModeConstParameter(nativefuncs.NativeConstParameterFunction nf, RunTimeValueBase argement, int id, out bool success)
		{
			nf.bin = player.swc;
			((nativefuncs.NativeConstParameterFunction)nf).pushParameter(toCallFunc, id, argement,
				token, invokerFrame, out success);
			if (success)
			{
				pushedArgs++;
			}
			else
			{
				clear_para_slot(invokerFrame, onstackparametercount);
				if (callbacker != null)
				{
					callbacker.noticeRunFailed();
				}
				release();
			}

		}

		public void pushParameter(RunTimeValueBase argement, int id, out bool success)
		{
			if (doPushNativeModeConstParameter(argement, id, out success))
			{
				return;
			}

			var parameters = toCallFunc.signature.parameters;

			if (parameters.Count > 0 || toCallFunc.IsAnonymous)
			{
				bool lastIsPara = false;

				if (parameters.Count > 0 && parameters[parameters.Count - 1].isPara)
				{
					lastIsPara = true;
				}

				if (!lastIsPara
					|| id < parameters.Count - 1
					||
					(
					toCallFunc.IsAnonymous
					&&
					!(lastIsPara && id >= parameters.Count - 1)
					)

					)
				{
					if (id < parameters.Count)
					{
						//CallFuncHeap[id].directSet(argement);

						_storeArgementToSlot(id, argement);

						pushedArgs++;
					}
					else
					{
						if (!toCallFunc.IsAnonymous)
						{
							//参数数量不匹配
							invokerFrame.throwArgementException(
								token,
								string.Format(
								"Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
								player.swc.blocks[toCallFunc.blockid].name, toCallFunc.signature.parameters.Count, pushedArgs + 1
								)

								);

							//***中断本帧本次代码执行进入try catch阶段
							success = false;

							clear_para_slot(invokerFrame, onstackparametercount);
							if (callbacker != null)
							{
								callbacker.noticeRunFailed();
							}
							release();


							return;
						}

					}

				}
				else
				{
					//***最后一个是参数数组，并且id大于等于最后一个
					SLOT slot = _getArgementSlot(parameters.Count - 1); //CallFuncHeap[toCallFunc.signature.parameters.Count - 1];
					if (slot.getValue().rtType == RunTimeDataType.rt_null)
					{
						slot.directSet(new ASBinCode.rtData.rtArray());
					}

					ASBinCode.rtData.rtArray arr = (ASBinCode.rtData.rtArray)slot.getValue();
					arr.innerArray.Add((RunTimeValueBase)argement.Clone());    //可能从StackSlot中读的数据，因此必须Clone后再传入.

				}

				success = true;
			}
			else
			{
				invokerFrame.throwArgementException(
							token,
							string.Format(
							"Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
							player.swc.blocks[toCallFunc.blockid].name, toCallFunc.signature.parameters.Count, pushedArgs + 1
							)

							);
				success = false;

				clear_para_slot(invokerFrame, onstackparametercount);
				if (callbacker != null)
				{
					callbacker.noticeRunFailed();
				}
				release();


			}
		}


		public void pushParameterToStack(RunTimeValueBase argement, int _index)
		{
			int index = invokerFrame.baseBottomSlotIndex + invokerFrame.call_parameter_slotCount + _index;
			invokerFrame.stack[index].directSet(argement);
			pushedArgs++;
		}

		public void pushParameterToHeap(RunTimeValueBase argement, int _index)
		{
			CallFuncHeap[_index].directSet(
						argement);
		}



		public void pushParameter_noCheck(RunTimeValueBase argement, int id, out bool success)
		{
			_storeArgementToSlot(id, argement);

			pushedArgs++;

			success = true;
		}
		public void pushParameter_noCheck_TestNative(RunTimeValueBase argement, int id, out bool success)
		{
			if (doPushNativeModeConstParameter(argement, id, out success))
			{
				return;
			}


			_storeArgementToSlot(id, argement);

			pushedArgs++;

			success = true;
		}

		public void pushParameter_Para(RunTimeValueBase argement, int id, out bool success)
		{
			if (doPushNativeModeConstParameter(argement, id, out success))
			{
				return;
			}

			//***最后一个是参数数组，并且id大于等于最后一个
			SLOT slot = _getArgementSlot(toCallFunc.signature.parameters.Count - 1); //CallFuncHeap[toCallFunc.signature.parameters.Count - 1];
			if (slot.getValue().rtType == RunTimeDataType.rt_null)
			{
				slot.directSet(new ASBinCode.rtData.rtArray());
			}

			ASBinCode.rtData.rtArray arr = (ASBinCode.rtData.rtArray)slot.getValue();
			arr.innerArray.Add((RunTimeValueBase)argement.Clone());    //可能从StackSlot中读的数据，因此必须Clone后再传入.


			success = true;
		}



		private RunTimeValueBase getToCheckParameter(int para_id)
		{
			if (toCallFunc.isNative)
			{
				//if (toCallFunc.native_index < 0)
				//{
				//    toCallFunc.native_index = player.swc.nativefunctionNameIndex[toCallFunc.native_name];
				//}
				//var nf = player.swc.nativefunctions[toCallFunc.native_index];
				var nf = player.swc.getNativeFunction(toCallFunc);
				nf.bin = player.swc;
				if (nf.mode == NativeFunctionBase.NativeFunctionMode.const_parameter_0)
				{
					return ((nativefuncs.NativeConstParameterFunction)nf).getToCheckParameter(para_id);
				}
			}

			return _getArgementSlot(para_id).getValue();  //CallFuncHeap[para_id].getValue();

		}
		private void setCheckedParameter(int para_id, RunTimeValueBase value)
		{
			//CallFuncHeap[para_id].directSet(value);
			if (toCallFunc.isNative)
			{
				//if (toCallFunc.native_index < 0)
				//{
				//    toCallFunc.native_index = player.swc.nativefunctionNameIndex[toCallFunc.native_name];
				//}
				//var nf = player.swc.nativefunctions[toCallFunc.native_index];
				var nf = player.swc.getNativeFunction(toCallFunc);
				nf.bin = player.swc;
				if (nf.mode == NativeFunctionBase.NativeFunctionMode.const_parameter_0)
				{
					((nativefuncs.NativeConstParameterFunction)nf)
						.setCheckedParameter(para_id, value);
					return;
				}
			}


			_getArgementSlot(para_id).directSet(value);
		}

        private static BlockCallBackBase.dgeCallbacker D_check_para_callbacker = new BlockCallBackBase.dgeCallbacker(check_para_callbacker);
        private static BlockCallBackBase.dgeCallbacker D_check_para_failed = new BlockCallBackBase.dgeCallbacker(check_para_failed);

        private void check_para(BlockCallBackBase checkparacb)
		{
			while (check_para_id < pushedArgs)
			{
				var parameter = toCallFunc.signature.parameters;

				RunTimeValueBase argement = getToCheckParameter(check_para_id);

				var argtype = argement.rtType;
				var targettype = parameter[check_para_id].type;

				if (argtype != targettype
					&&
					targettype != RunTimeDataType.rt_void
					&&
					!(argtype == RunTimeDataType.rt_null && targettype > RunTimeDataType.unknown)
					&&
					!(argtype > RunTimeDataType.unknown && targettype > RunTimeDataType.unknown
						&&
						(
							ClassMemberFinder.check_isinherits(argement, targettype, player.swc)
							||
							ClassMemberFinder.check_isImplements(argement, targettype, player.swc)
						)
					)

					)
				{
					BlockCallBackBase cb = player.blockCallBackPool.create();
					cb.args = argement;
					cb._intArg = check_para_id;
                    cb.cacheObjects[0] = this;
					cb.setCallBacker(D_check_para_callbacker);
					cb.setWhenFailed(D_check_para_failed);
                    
					check_para_id++;

					OpCast.CastValue(argement, parameter[
						cb._intArg].type,

						invokerFrame, token, invokerFrame.scope, _tempSlot, cb, false);

					return;
				}
				else
				{
					check_para_id++;
				}
			}

			if (pushedArgs < toCallFunc.signature.parameters.Count && !toCallFunc.IsAnonymous) //匿名函数能跳过参数检查
			{
				for (int i = pushedArgs; i < toCallFunc.signature.parameters.Count; i++)
				{
					if (toCallFunc.signature.parameters[pushedArgs].defaultValue == null
					&&
					!toCallFunc.signature.parameters[pushedArgs].isPara
					)
					{
						invokerFrame.throwArgementException(
							token,
							string.Format(
							"Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
							player.swc.blocks[toCallFunc.blockid].name, toCallFunc.signature.parameters.Count, pushedArgs
							)

							);
						clear_para_slot(invokerFrame, onstackparametercount); onstackparametercount = 0;
						//***中断本帧本次代码执行进入try catch阶段
						invokerFrame.endStep();

						if (callbacker != null)
						{
							callbacker.noticeRunFailed();
						}
						release();


						return;
					}
				}
			}

			if (!check_return_objtypeisclasscreated())
			{
				return;
			}

			if (checkparacb != null)
			{
				checkparacb.noticeEnd();
			}
			//***全部参数检查通过***
			doCall_allcheckpass();
		}

		private static void check_para_failed(BlockCallBackBase sender, object args)
		{
            FunctionCaller caller = (FunctionCaller)sender.cacheObjects[0];
			//清理栈
			clear_para_slot(caller.invokerFrame, caller.onstackparametercount); caller.onstackparametercount = 0;
			if (caller.callbacker != null)
			{
                caller.callbacker.noticeRunFailed();
			}
            caller.release();
		}

		private static void check_para_callbacker(BlockCallBackBase sender, object args)
		{
			if (sender.isSuccess)
			{
                FunctionCaller caller = (FunctionCaller)sender.cacheObjects[0];
				//CallFuncHeap[sender._intArg].directSet(_tempSlot.getValue());
				caller.setCheckedParameter(sender._intArg, caller._tempSlot.getValue());
				caller.check_para(sender);
			}
			else
			{
				throw new ASRunTimeException("解释器内部错误，参数类型检查", string.Empty);
				//invokerFrame.throwCastException(token, ((RunTimeValueBase)sender.args).rtType, toCallFunc.signature.parameters[sender._intArg].type);
				//return;
			}
		}

        private static BlockCallBackBase.dgeCallbacker D_callfun_cb = new BlockCallBackBase.dgeCallbacker(callfun_cb);
        private static BlockCallBackBase.dgeCallbacker D_callfun_failed = new BlockCallBackBase.dgeCallbacker(callfun_failed);

        internal void doCall_allcheckpass()
		{

			if (toCallFunc.isYield)
			{
				if (!player.static_instance.ContainsKey(player.swc.YieldIteratorClass.staticClass.classid))
				{
					if (!InstanceCreator.init_static_class(player.swc.YieldIteratorClass, player, token))
					{
						invokerFrame.endStep();

						if (callbacker != null)
						{
							callbacker.noticeRunFailed();
						}
						release();


						return;
					}
				}

				ASBinCode.rtti.YieldObject yobj = new ASBinCode.rtti.YieldObject(
					player.swc.YieldIteratorClass);


				CallFuncHeap[CallFuncHeap.Length - 2].directSet(new ASBinCode.rtData.rtInt(1));

				yobj.argements = CallFuncHeap;
				yobj.function_bindscope = function.bindScope;
				yobj.block = player.swc.blocks[toCallFunc.blockid];
				yobj.yield_function = toCallFunc;


				ASBinCode.rtData.rtObject rtYield = new ASBinCode.rtData.rtObject(yobj, null);
				yobj.thispointer =
					(function.this_pointer != null ?
					function.this_pointer : invokerFrame.scope.this_pointer);

				RunTimeScope scope = new RunTimeScope(null,
					 player.swc.YieldIteratorClass.blockid, null, rtYield, RunTimeScopeType.objectinstance);
				rtYield.objScope = scope;

				returnSlot.directSet(rtYield);


				if (callbacker != null)
				{
					callbacker.call(callbacker.args);
				}
				release();

				return;
			}
			else if (!toCallFunc.isNative)
			{
				if (returnSlot is StackSlot)
				{
					TypeConverter.setDefaultValueToStackSlot(
						toCallFunc.signature.returnType,
						(StackSlot)returnSlot);
				}
				else
				{

					returnSlot.directSet(
						TypeConverter.getDefaultValue(toCallFunc.signature.returnType).getValue(null, null));

				}
				if (!ReferenceEquals(callbacker, this))
				{
					//***执行完成后，先清理参数***
					BlockCallBackBase cb = player.blockCallBackPool.create();
					cb.args = cb.cacheObjects;
					cb.cacheObjects[0] = callbacker;
					cb.cacheObjects[1] = invokerFrame;

					cb.setCallBacker(D_callfun_cb);
					cb.setWhenFailed(D_callfun_failed);
					cb._intArg = onstackparametercount;
					onstackparametercount = 0;

					player.callBlock(
						player.swc.blocks[toCallFunc.blockid],
						CallFuncHeap,
						returnSlot,
						function.bindScope,
						token, cb, function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer, RunTimeScopeType.function);

					release();
				}
				else
				{
					player.callBlock(
						player.swc.blocks[toCallFunc.blockid],
						CallFuncHeap,
						returnSlot,
						function.bindScope,
						token, callbacker, function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer, RunTimeScopeType.function);
				}

			}
			else
			{
				//if (toCallFunc.native_index <0)
				//{
				//    toCallFunc.native_index = player.swc.nativefunctionNameIndex[toCallFunc.native_name];
				//}

				//var nf = player.swc.nativefunctions[toCallFunc.native_index];
				var nf = player.swc.getNativeFunction(toCallFunc);
				nf.bin = player.swc;

				if (nf.mode == NativeFunctionBase.NativeFunctionMode.normal_0)
				{
					player._nativefuncCaller = this;

					string errormsg;
					int errorno;
					var result = nf.execute(
						function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer,
						CallFuncHeap, invokerFrame,
						out errormsg,
						out errorno
						);
					player._nativefuncCaller = null;
					if (errormsg == null)
					{
						returnSlot.directSet(result);

						if (callbacker != null)
						{
							callbacker.call(callbacker.args);
						}

						release();

					}
					else
					{
						invokerFrame.throwError(
							token, 0, errormsg
							);

						invokerFrame.endStep();

						if (callbacker != null)
						{
							callbacker.noticeRunFailed();
						}
						release();


					}

				}
				//else if (nf.mode == NativeFunctionBase.NativeFunctionMode.normal_1)
				//{
				//	player._nativefuncCaller = this;
				//	bool success;

				//	nf.execute2(
				//		function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer,
				//		toCallFunc,
				//		CallFuncHeap,
				//		returnSlot,
				//		token,
				//		invokerFrame,
				//		out success
				//		);
				//	player._nativefuncCaller = null;
				//	if (success)
				//	{
				//		if (callbacker != null)
				//		{
				//			callbacker.call(callbacker.args);
				//		}
				//	}
				//	else
				//	{
				//		invokerFrame.endStep();
				//		if (callbacker != null)
				//		{
				//			callbacker.noticeRunFailed();
				//		}

				//	}

				//	release();


				//}
				else if (nf.mode == NativeFunctionBase.NativeFunctionMode.async_0)
				{
					nf.executeAsync(function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer,
						CallFuncHeap,
						returnSlot,
						callbacker,
						invokerFrame,
						token,
						function.bindScope
						);

					if (!ReferenceEquals(callbacker, this))
					{
						release();
					}
				}
				else if (nf.mode == NativeFunctionBase.NativeFunctionMode.const_parameter_0)
				{
					bool success = false;
					player._nativefuncCaller = this;

					var nf3 = (nativefuncs.NativeConstParameterFunction)nf;
					player._executeToken = nf3.getExecToken(toCallFunc.functionid);

					nf3.execute3(
						function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer,
						toCallFunc,
						returnSlot,
						token,
						invokerFrame,
						out success
						);

					player._executeToken = nativefuncs.NativeConstParameterFunction.ExecuteToken.nulltoken;
					player._nativefuncCaller = null;
					((nativefuncs.NativeConstParameterFunction)nf).clearParameter();

					clear_para_slot(invokerFrame, onstackparametercount);
					onstackparametercount = 0;

					if (success)
					{
						var receive_err = player.clear_nativeinvokeraiseerror();
						if (receive_err != null)
						{
							invokerFrame.receiveErrorFromStackFrame(receive_err);
							if (callbacker != null)
							{
								callbacker.noticeRunFailed();
							}
						}
						else if (callbacker != null)
						{
							callbacker.call(callbacker.args);
						}
					}
					else
					{
						invokerFrame.endStep();
						if (callbacker != null)
						{
							callbacker.noticeRunFailed();
						}

					}
					release();

				}

			}
		}

		internal void doCall_allcheckpass_nativefunctionconstpara(NativeFunctionBase nf)
		{
			{
				bool success = false;
				player._nativefuncCaller = this;

				var nf3 = (nativefuncs.NativeConstParameterFunction)nf;
				player._executeToken = nf3.getExecToken(toCallFunc.functionid);

				nf3.execute3(
					function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer,
					toCallFunc,
					returnSlot,
					token,
					invokerFrame,
					out success
					);

				player._executeToken = nativefuncs.NativeConstParameterFunction.ExecuteToken.nulltoken;
				player._nativefuncCaller = null;
				((nativefuncs.NativeConstParameterFunction)nf).clearParameter();

				clear_para_slot(invokerFrame, onstackparametercount);
				onstackparametercount = 0;

				if (success)
				{
					var receive_err = player.clear_nativeinvokeraiseerror();
					if (receive_err != null)
					{
						invokerFrame.receiveErrorFromStackFrame(receive_err);
						if (callbacker != null)
						{
							callbacker.noticeRunFailed();
						}
					}
					else if (callbacker != null)
					{
						callbacker.call(callbacker.args);
					}
				}
				else
				{
					invokerFrame.endStep();
					if (callbacker != null)
					{
						callbacker.noticeRunFailed();
					}

				}
				release();

			}
		}



		internal void doCall_allcheckpass_nonative_hassetreturndefault_method()
		{
#if DEBUG
			if (CallFuncHeap != null && CallFuncHeap.Length > 0)
			{
				throw new Exception("不能这么调用");
			}
#endif

			player.callBlock_Method_NoHeap(player.swc.blocks[toCallFunc.blockid], returnSlot, token, callbacker, function.this_pointer);
		}

		internal void doCall_allcheckpass_nonative_hassetreturndefault()
		{

			player.callBlock(
				player.swc.blocks[toCallFunc.blockid],
				CallFuncHeap,
				returnSlot,
				function.bindScope,
				token, callbacker, function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer, RunTimeScopeType.function);

		}

		private static void callfun_failed(BlockCallBackBase sender, object args)
		{
			IBlockCallBack callbacker = sender.cacheObjects[0] as IBlockCallBack;
			StackFrame frame = (StackFrame)sender.cacheObjects[1];
			clear_para_slot(frame, sender._intArg);
			if (callbacker != null)
			{
				callbacker.noticeRunFailed();
			}
		}

		private static void callfun_cb(BlockCallBackBase sender, object args)
		{
			IBlockCallBack callbacker = sender.cacheObjects[0] as IBlockCallBack;
			StackFrame frame = (StackFrame)sender.cacheObjects[1];
			clear_para_slot(frame, sender._intArg);
			if (callbacker != null)
			{
				callbacker.call(callbacker.args);
			}

		}

		private static void clear_para_slot(StackFrame invokerFrame, int count)
		{
			while (count > 0)
			{
#if DEBUG
				int i = invokerFrame.baseBottomSlotIndex + invokerFrame.call_parameter_slotCount; //invokerFrame.offset + invokerFrame.block.totalRegisters + 1 + 1 + invokerFrame.call_parameter_slotCount;
				--invokerFrame.call_parameter_slotCount;
				--i;
				--count;
				invokerFrame.stack[i].clear();

#else
				--count;

				StackSlot slot = invokerFrame.stack[invokerFrame.baseBottomSlotIndex + (--invokerFrame.call_parameter_slotCount)];
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


					slot.store[StackSlot.COMMREFTYPEOBJ] = ASBinCode.rtData.rtNull.nullptr;
				}
				slot.index = (int)RunTimeDataType.unknown;

#endif
			}


			//if (invokerFrame.call_parameter_slotCount > 0)
			//{
			//	//**清理**
			//	for (int i = invokerFrame.offset + invokerFrame.block.totalRegisters + 1 + 1;
			//		i < invokerFrame.offset + invokerFrame.block.totalRegisters + 1 + 1 + invokerFrame.call_parameter_slotCount
			//		; i++)
			//	{
			//		invokerFrame.stack[i].clear();
			//	}

			//	invokerFrame.call_parameter_slotCount = 0;
			//}
		}

		private bool check_return_objtypeisclasscreated()
		{
			if (toCallFunc.signature.returnType > RunTimeDataType.unknown)
			{
				if (!InstanceCreator.init_static_class(player.swc.getClassByRunTimeDataType(toCallFunc.signature.returnType), player, token))
				{
					//***中断本帧本次代码执行进入try catch阶段
					invokerFrame.endStep();

					if (callbacker != null)
					{
						callbacker.noticeRunFailed();
					}
					release();
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}

		public void call()
		{
			check_para(null);
		}

		public void call_nocheck()
		{
			if (!check_return_objtypeisclasscreated())
			{
				return;
			}

			doCall_allcheckpass();
		}

		object IBlockCallBack.args
		{
			get
			{
				return null;
			}

			set
			{
				;
			}
		}

		void IBlockCallBack.call(object args)
		{
#if DEBUG
			clear_para_slot(invokerFrame, onstackparametercount);
#else
			{
				int count = onstackparametercount;
				while (count > 0)
				{
					--count;
					StackSlot slot = invokerFrame.stack[invokerFrame.baseBottomSlotIndex + (--invokerFrame.call_parameter_slotCount)];
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


						slot.store[StackSlot.COMMREFTYPEOBJ] = ASBinCode.rtData.rtNull.nullptr;
					}
					slot.index = (int)RunTimeDataType.unknown;
				}
			}
#endif


			onstackparametercount = 0;
			invokerFrame.endStepNoError();
			//release();
			//人肉内联release

			if (!hasReleased)
			{
				hasReleased = true;
				callbacker = null;

				CallFuncHeap = null;
				toCallFunc = null;
				returnSlot = null;
				_tempSlot = null;
				invokerFrame = null;
				token = null;
				tag = null;
				function.Clear();

				player.funcCallerPool.ret(this);


			}

		}

		public void noticeRunFailed()
		{

			clear_para_slot(invokerFrame, onstackparametercount);
			onstackparametercount = 0;
			release();
		}
	}
}
