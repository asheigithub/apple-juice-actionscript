using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
	public class CompileEnv
	{
		public ASBinCode.CodeBlock block;

		/// <summary>
		/// 打上标记的代码块
		/// </summary>
		internal int labelblocks;

		internal List<ASTool.AS3.AS3Function> tobuildNamedfunction = new List<ASTool.AS3.AS3Function>();

		private Dictionary<string, ASBinCode.StackSlotAccessor> dictCompileRegisters;

		private int lastStmtId = short.MaxValue;

		private int labelIdx = 0;
		public string makeLabel(string labelHead)
		{
			return labelHead + "_" + (++labelIdx);
		}

		public int getLabelId()
		{
			return ++labelIdx;
		}

		public ASBinCode.StackSlotAccessor getAdditionalRegister()
		{
			ASBinCode.StackSlotAccessor reg = new ASBinCode.StackSlotAccessor(dictCompileRegisters.Count, lastStmtId);
			dictCompileRegisters.Add("ADDITIONAL" + reg.Id, reg);

			return reg;
		}


		/// <summary>
		/// 根据语法树来获取创建Register
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ASBinCode.StackSlotAccessor createASTRegister(ASTool.AS3.Expr.AS3Reg as3reg)
		{
			if (dictCompileRegisters.ContainsKey("V" + as3reg.ID))
			{
				return dictCompileRegisters["V" + as3reg.ID];
			}
			else
			{
				ASBinCode.StackSlotAccessor reg = new ASBinCode.StackSlotAccessor(dictCompileRegisters.Count, as3reg.StmtID);
				dictCompileRegisters.Add("V" + as3reg.ID, reg);
				lastStmtId = as3reg.StmtID;
				return reg;
			}
		}

		/// <summary>
		/// 查找语法树中定义的Register
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ASBinCode.StackSlotAccessor loadRegisterByAST(int id)
		{
			if (dictCompileRegisters.ContainsKey("V" + id))
			{
				return dictCompileRegisters["V" + id];
			}
			return null;
		}


		/// <summary>
		/// 合并需要的StackSlot
		/// </summary>
		/// <returns></returns>
		public int combieNeedStackSlots()
		{
			//return dictCompileRegisters.Count;
			int maxindex = -1;
			foreach (var item in dictCompileRegisters.Values)
			{
				if (item._index > maxindex)
				{
					maxindex = item._index;
				}
			}

			return maxindex + 1;
		}


		/// <summary>
		/// 重排序StackSlotAccessor的Index
		/// </summary>
		private void resortStackSlotAccessorIndex()
		{
			Dictionary<int, int> dictSlotIndex = new Dictionary<int, int>();
			Dictionary<StackSlotAccessor, object> slots = new Dictionary<StackSlotAccessor, object>();
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				{
					StackSlotAccessor sa = step.reg as StackSlotAccessor;
					if (sa != null && sa._index >= 0)
					{
						if (!slots.ContainsKey(sa))
							slots.Add(sa, null);
						if (!dictSlotIndex.ContainsKey(sa._index))
						{
							dictSlotIndex.Add(sa._index, dictSlotIndex.Count);
						}
					}
				}
				{
					StackSlotAccessor sa = step.arg1 as StackSlotAccessor;
					if (sa != null && sa._index >= 0)
					{
						if (!slots.ContainsKey(sa))
							slots.Add(sa, null);
						if (!dictSlotIndex.ContainsKey(sa._index))
						{
							dictSlotIndex.Add(sa._index, dictSlotIndex.Count);
						}
					}
				}
				{
					StackSlotAccessor sa = step.arg2 as StackSlotAccessor;
					if (sa != null && sa._index >= 0)
					{
						if (!slots.ContainsKey(sa))
							slots.Add(sa, null);
						if (!dictSlotIndex.ContainsKey(sa._index))
						{
							dictSlotIndex.Add(sa._index, dictSlotIndex.Count);
						}
					}
				}
			}

			List<string> toremove = new List<string>();

			foreach (var item in dictCompileRegisters)
			{
				if (!slots.ContainsKey(item.Value))
				{
					toremove.Add(item.Key);
				}
			}

			foreach (var item in toremove)
			{
				dictCompileRegisters.Remove(item);
			}

			foreach (var item in slots)
			{
				item.Key._index = dictSlotIndex[item.Key._index];
			}


		}

		public void optimizeFunctoinBlock(Builder builder, ASBinCode.rtti.FunctionDefine f)
		{
			
			optimizeOtherStep();

			convertVarToReg(builder, f);

			setTryState();//先刷一次TryState,便于优化时参考
			//if (
			//	//!f.name.EndsWith("inflate")
			//	//&&
			//	//!f.name.EndsWith("stored")
			//	//&&
			//	!f.name.EndsWith("codes")
			//	)
			{
				optimizeReg2();
			}
			
			combieRegs();

			optimizeJump();

			resortStackSlotAccessorIndex();

		}

		private void optimizeOtherStep()
		{
			//查找所有vectorAccessor_bind,如果是取值，则优化为直接赋值操作
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (step.opCode == OpCode.vectorAccessor_bind)
				{
					StackSlotAccessor accessor = (StackSlotAccessor)step.reg;
					if(!(accessor._isassigntarget || accessor._hasUnaryOrShuffixOrDelete))
					{
						//***改为直接取值指令***
						step.opCode = OpCode.vector_getvalue;
					}
				}


			}
		}


		private void convertVarToReg(Builder builder, ASBinCode.rtti.FunctionDefine f)
		{

			List<CodeBlock> blocks = new List<CodeBlock>();
			foreach (var b in builder.bin.blocks)
			{
				if (b != null && b.define_class_id == block.define_class_id && b != block)
				{
					blocks.Add(b);
				}
			}

			Dictionary<Variable, StackSlotAccessor> toReplace = new Dictionary<Variable, StackSlotAccessor>();

			for (int i = 0/* f.signature.parameters.Count*/; i < block.scope.members.Count; i++)
			{
				var m = block.scope.members[i];

				{
					Variable vm = (Variable)m;
					if (i < f.signature.parameters.Count)
					{
						f.signature.parameters[i].varorreg = vm;
					}

					//if (vm.valueType == RunTimeDataType.rt_void)
					//{
					//    continue;
					//}
					//if (vm.valueType > RunTimeDataType.unknown)
					//{
					//    var c = builder.bin.getClassByRunTimeDataType(vm.valueType);
					//    if(c==null || c.isLink_System)
					//    {
					//        continue;
					//    }
					//}

					bool found = false;
					//***查找是否被其他块引用***
					foreach (var tofindblock in blocks)
					{
						var steps = tofindblock.opSteps;
						foreach (var op in steps)
						{
							Variable v = op.reg as Variable;
							if (v != null && v.name == vm.name && v.indexOfMembers == vm.indexOfMembers && v.refdefinedinblockid == vm.refdefinedinblockid)
							{
								found = true;
								break;
							}
							v = op.arg1 as Variable;
							if (v != null && v.name == vm.name && v.indexOfMembers == vm.indexOfMembers && v.refdefinedinblockid == vm.refdefinedinblockid)
							{
								found = true;
								break;
							}
							v = op.arg2 as Variable;
							if (v != null && v.name == vm.name && v.indexOfMembers == vm.indexOfMembers && v.refdefinedinblockid == vm.refdefinedinblockid)
							{
								found = true;
								break;
							}
						}
					}

					if (!found)
					{

						StackSlotAccessor varReg = getAdditionalRegister();
						varReg.isConvertFromVariable = true;
						//***将Variable替换为Reg***
						toReplace.Add(vm, varReg);

						if (i >= f.signature.parameters.Count)
						{
							varReg._index = block.totalStackSlots;
							block.totalStackSlots++;
							varReg.valueType = vm.valueType;
							
							StackSlotAccessor[] cfv = new StackSlotAccessor[block.regConvFromVar.Length + 1];
							block.regConvFromVar.CopyTo(cfv, 0);
							cfv[cfv.Length - 1] = varReg;
							block.regConvFromVar = cfv;

						}
						else
						{
							//是参数修改

							f.signature.onStackParameters++;
							varReg._index = -f.signature.onStackParameters;varReg.valueType = f.signature.parameters[i].type;
							f.signature.parameters[i].varorreg = varReg;
							f.signature.parameters[i].isOnStack = true;
						}

					}
				}
			}

			if (toReplace.Count > 0)
			{
				foreach (var item in toReplace)
				{
					block.scope.members.Remove(item.Key);
					foreach (var op in block.opSteps)
					{
						//***将所有引用到的Variable替换***
						if (Object.Equals(op.reg, item.Key))
						{
							op.reg = item.Value;
						}

						if (Object.Equals(op.arg1, item.Key))
						{
							op.arg1 = item.Value;
						}

						if (Object.Equals(op.arg2, item.Key))
						{
							op.arg2 = item.Value;
						}
					}
				}

				for (int i = 0; i < block.scope.members.Count; i++)
				{
					Variable var = (Variable)block.scope.members[i];
					if (var.indexOfMembers != i)
					{
						var tosearchblocks = new List<CodeBlock>();
						tosearchblocks.Add(block);
						tosearchblocks.AddRange(blocks);
						//***更新所有引用的新索引
						foreach (var tofindblock in tosearchblocks)
						{
							var steps = tofindblock.opSteps;
							foreach (var op in steps)
							{

								if (Equals(op.reg, var))
								{
									((Variable)op.reg).setIndexMemberWhenCompile(i);
								}

								if (Equals(op.arg1, var))
								{
									((Variable)op.arg1).setIndexMemberWhenCompile(i);
								}

								if (Equals(op.arg2, var))
								{
									((Variable)op.arg2).setIndexMemberWhenCompile(i);
								}
							}
						}
						var.setIndexMemberWhenCompile(i);

					}
				}

			}

			
		}
		//private void optimizeReg()
		//{
			
		//	#region 尝试优化参数是否优化后更好
		//	StackSlotAccessor[] slist = new StackSlotAccessor[dictCompileRegisters.Count];
		//	dictCompileRegisters.Values.CopyTo(slist, 0);
		//	foreach (var r in slist)
		//	{
		//		if (r._index < 0)
		//		{
		//			int insertinto = 0;
		//			int maxccount = 0; int breaks = 0; int csegs = 0;
		//			for (int k = 0; k < block.opSteps.Count; k++)
		//			{
		//				if ((block.opSteps[k].opCode == OpCode.init_staticclass
		//					||
		//					block.opSteps[k].opCode == OpCode.yield_continuetoline
		//					)
		//					&& !isReference(r, block.opSteps[k])
		//					)
		//				{
		//					insertinto++;
		//					continue;
		//				}

		//				if (isReference(r, block.opSteps[k]))
		//				{
		//					int lastline = findLastRefLine(r);
		//					int failedline; List<int> continuelines = new List<int>();

		//					if (isAllSafeOperator(k, lastline, lastline, r, out failedline, continuelines))
		//					{
		//						for (int i = k + 1; i < lastline; i++)
		//						{
		//							if (isReference(r, block.opSteps[i]))
		//							{
		//								maxccount++;
		//							}
		//						}
		//						csegs = maxccount;
		//						break;
		//					}
		//					else
		//					{
		//						int ccount = 0;
		//						breaks++;
		//						for (int i = k + 1; i < lastline; i++)
		//						{
		//							if (isReference(r, block.opSteps[i]))
		//							{
		//								ccount++;
		//								if (ccount > maxccount)
		//								{
		//									maxccount = ccount;
		//								}
		//								if (ccount == 2)
		//								{
		//									csegs++;
		//								}
		//							}
		//							if (i == continuelines[0])
		//							{
		//								ccount = 0;
		//							}
		//						}
		//					}

		//				}

		//			}

		//			if ((csegs * maxccount > breaks * 2))
		//			//if(maxccount>0)
		//			{
		//				var load = getAdditionalRegister();
		//				load.valueType = r.valueType;
		//				OpStep loadstep = new OpStep(OpCode.assigning, block.opSteps[0].token);
		//				loadstep.reg = load;
		//				loadstep.regType = r.valueType;
		//				loadstep.arg1 = r;
		//				loadstep.arg1Type = r.valueType;

		//				for (int k = 0; k < block.opSteps.Count; k++)
		//				{
		//					replaceStackSlotAccessor(r, load, block.opSteps[k]);
		//				}
		//				block.opSteps.Insert(insertinto, loadstep);
		//			}

		//		}
		//	}
		//	#endregion


		//	Dictionary<StackSlotAccessor, IMemReg> dictToOptimizeRegister = new Dictionary<StackSlotAccessor, IMemReg>();
		//	Dictionary<StackSlotAccessor, IMemReg> dictCanNotOptimizeRegister = new Dictionary<StackSlotAccessor, IMemReg>();

		//	Dictionary<IMemReg, StackSlotAccessor> dictMem_StackSlotAccessor = new Dictionary<IMemReg, StackSlotAccessor>();

		//	for (int i = 0; i < block.opSteps.Count; i++)
		//	{
		//		var step = block.opSteps[i];
		//		#region 先排除未赋值就使用的槽
		//		if (!(step.reg is StackSlotAccessor))
		//		{
		//			{
		//				StackSlotAccessor test = step.arg1 as StackSlotAccessor;
		//				if (test != null)
		//				{
		//					//未赋值就使用的StackSlotAccessor不可优化。
		//					if (!dictCanNotOptimizeRegister.ContainsKey(test) && !dictToOptimizeRegister.ContainsKey(test))
		//					{
		//						dictCanNotOptimizeRegister.Add(test, null);
		//					}
		//				}
		//			}
		//			{
		//				StackSlotAccessor test = step.arg2 as StackSlotAccessor;
		//				if (test != null)
		//				{
		//					//未赋值就使用的StackSlotAccessor不可优化。
		//					if (!dictCanNotOptimizeRegister.ContainsKey(test) && !dictToOptimizeRegister.ContainsKey(test))
		//					{
		//						dictCanNotOptimizeRegister.Add(test, null);
		//					}
		//				}
		//			}
		//		}
		//		#endregion

		//		if (step.reg is StackSlotAccessor && !dictToOptimizeRegister.ContainsKey((StackSlotAccessor)step.reg) && !dictCanNotOptimizeRegister.ContainsKey((StackSlotAccessor)step.reg))
		//		{
		//			StackSlotAccessor register = (StackSlotAccessor)step.reg;
		//			if (canOptimize(step, register) && register._index>=0)
		//			{

		//				int lastline = findLastRefLine(register);
		//				int failedline; List<int> continuelines = new List<int>();
		//				if (isAllSafeOperator(i + 1, lastline, lastline, register, out failedline, continuelines))
		//				{
		//					if (register.valueType == RunTimeDataType.rt_number)
		//					{
								
		//						dictToOptimizeRegister.Add(register, new MemRegister_Number(register.Id));dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
		//						if (step.opCode == OpCode.access_dot)
		//							step.opCode = OpCode.access_dot_memregister;
		//					}
		//					else if (register.valueType == RunTimeDataType.rt_boolean)
		//					{
		//						dictToOptimizeRegister.Add(register, new MemRegister_Boolean(register.Id)); dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
		//						if (step.opCode == OpCode.access_dot)
		//							step.opCode = OpCode.access_dot_memregister;
		//					}
		//					else if (register.valueType == RunTimeDataType.rt_int)
		//					{
		//						dictToOptimizeRegister.Add(register, new MemRegister_Int(register.Id)); dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
		//						if (step.opCode == OpCode.access_dot)
		//							step.opCode = OpCode.access_dot_memregister;
		//					}
		//					else if (register.valueType == RunTimeDataType.rt_uint)
		//					{
		//						dictToOptimizeRegister.Add(register, new MemRegister_UInt(register.Id)); dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
		//						if (step.opCode == OpCode.access_dot)
		//							step.opCode = OpCode.access_dot_memregister;
		//					}
		//				}
		//				else
		//				{
		//					if (failedline > i)
		//					{
		//						continuelines.Sort();
								
		//						bool caninsert = true;

		//						{
		//							//**如果后面还有对其修改值的操作,比如赋值和自增，说明为从变量转化而来。
		//							//会导致有可能递归操作时意外修改值。所以检查到就跳过。***
		//							//如果在catch块,finally块中被引用，可能导致异常的跳转，检查到就跳过
		//							for (int k = failedline + 1; k < lastline + 1; k++)
		//							{
		//								var teststep = block.opSteps[k];
		//								if (ReferenceEquals(teststep.reg, register))
		//								{
		//									//如果后面还有回跳并跳回当前行前，说明有问题
		//									for (int l = k+1; l < block.opSteps.Count; l++)
		//									{
		//										var sss = block.opSteps[l];
		//										if ((builds.AS3FunctionBuilder.isJMP(sss.opCode) || builds.AS3FunctionBuilder.isIfJmp(sss.opCode))
		//											&&
		//											sss.jumoffset < 0
		//											)
		//										{
		//											if (l + sss.jumoffset <= failedline)
		//											{
		//												caninsert = false; break;
		//											}
		//										}

		//									}
		//									if (!caninsert)
		//									{
		//										break;
		//									}
		//								}
		//								else
		//								if (isReference(register, teststep))
		//								{
		//									if (teststep.trytype == 1 || teststep.trytype==2)
		//									{
		//										caninsert = false; break;					
		//									}

		//									if (teststep.reg is StackSlotAccessor)
		//									{
		//										if (((StackSlotAccessor)teststep.reg)._hasUnaryOrShuffix)
		//										{

		//											//如果后面还有回跳并跳回当前行前，说明有问题
		//											for (int l = k + 1; l < block.opSteps.Count; l++)
		//											{
		//												var sss = block.opSteps[l];
		//												if ((builds.AS3FunctionBuilder.isJMP(sss.opCode) || builds.AS3FunctionBuilder.isIfJmp(sss.opCode))
		//													&&
		//													sss.jumoffset < 0
		//													)
		//												{
		//													if (l + sss.jumoffset <= failedline)
		//													{
		//														caninsert = false; break;
		//													}
		//												}

		//											}
		//											if (!caninsert)
		//											{
		//												break;
		//											}
		//										}
		//									}

		//								}
		//							}


		//						}
		//						if (caninsert)
		//						{
		//							int det1 = 0;
		//							for (int k = i + 1; k < failedline; k++)
		//							{
		//								if (isReference(register, block.opSteps[k]))
		//								{
		//									det1++;
		//								}
		//							}
									
		//							int det2 = 0;
		//							for (int k = continuelines[continuelines.Count-1]; k < lastline; k++)
		//							{
		//								if (isReference(register, block.opSteps[k]))
		//								{
		//									det2++;
		//								}
		//							}
										
										
									

		//							if (det1 > 1 || det2 > 1)
		//							{
		//								//在失败行前后加入读写
		//								StackSlotAccessor store = getAdditionalRegister();
		//								store.valueType = register.valueType;
		//								store.stmtid = register.stmtid;
		//								OpStep savestep = new OpStep(OpCode.assigning, block.opSteps[failedline].token);
		//								savestep.reg = store;
		//								savestep.regType = register.valueType;
		//								savestep.arg1 = register;
		//								savestep.arg1Type = register.valueType;

		//								if (det2 <= 1)
		//								{
		//									//***在各个分支之间插入相应代码***						
		//									for (int m = continuelines[0] + 1; m < lastline + 1; m++)
		//									{
		//										replaceStackSlotAccessor(register, store, block.opSteps[m]);
		//									}
		//								}
		//								else
		//								{
		//									StackSlotAccessor load = getAdditionalRegister();
		//									load.valueType = register.valueType;
		//									load.stmtid = register.stmtid;
		//									OpStep loadstep = new OpStep(OpCode.assigning, block.opSteps[continuelines[continuelines.Count - 1]].token);
		//									loadstep.reg = load;
		//									loadstep.regType = register.valueType;
		//									loadstep.arg1 = store;
		//									loadstep.arg1Type = register.valueType;


		//									for (int m = continuelines[continuelines.Count-1]+1; m < lastline+1; m++)
		//									{
		//										replaceStackSlotAccessor(register, load, block.opSteps[m]);
		//									}

		//									for (int m = continuelines[0] + 1; m < lastline + 1; m++)
		//									{
		//										replaceStackSlotAccessor(register, store, block.opSteps[m]);
		//									}

		//									block.opSteps.Insert(continuelines[continuelines.Count - 1] + 1, loadstep);

		//								}


		//								block.opSteps.Insert(failedline, savestep);
										

		//								dictCanNotOptimizeRegister.Add(store, null);
		//								//dictCanNotOptimizeRegister.Add(load, null);

		//								i--;
		//								continue;

		//							}
		//						}
		//					}

		//					dictCanNotOptimizeRegister.Add(register, null);
		//				}

		//			}
		//			else
		//			{
		//				dictCanNotOptimizeRegister.Add(register, null);
		//			}
		//		}
		//	}

		//	foreach (var item in dictToOptimizeRegister)
		//	{
		//		for (int i = 0; i < block.opSteps.Count; i++)
		//		{
		//			var opstep = block.opSteps[i];

		//			if (ReferenceEquals(opstep.reg, item.Key))
		//			{
		//				opstep.reg = (LeftValueBase)item.Value;
		//			}
		//			if (ReferenceEquals(opstep.arg1, item.Key))
		//			{
		//				opstep.arg1 = (LeftValueBase)item.Value;
		//			}
		//			if (ReferenceEquals(opstep.arg2, item.Key))
		//			{
		//				opstep.arg2 = (LeftValueBase)item.Value;
		//			}
		//		}
		//	}


		//	setMemReg(dictMem_StackSlotAccessor);

		//}

		private void setMemReg(Dictionary<IMemReg, StackSlotAccessor> dictMem_StackSlotAccessor)
		{
			
			{
				//****重分配*****
				Dictionary<RunTimeDataType, Dictionary<IMemReg, memregSlot>> dict_MemReg_Slot = new Dictionary<RunTimeDataType, Dictionary<IMemReg, memregSlot>>();
				Dictionary<RunTimeDataType, Queue<memregSlot>> dict_SlotPool = new Dictionary<RunTimeDataType, Queue<memregSlot>>();
				Dictionary<RunTimeDataType, List<memregSlot>> dict_allocedSlots = new Dictionary<RunTimeDataType, List<memregSlot>>();
				Dictionary<IMemReg, OpStep> regLastStep = new Dictionary<IMemReg, OpStep>();

				for (int i = 0; i < block.opSteps.Count; i++)
				{
					var opstep = block.opSteps[i];
					if (opstep.reg is IMemReg)
					{
						if (dictMem_StackSlotAccessor[(IMemReg)opstep.reg].isConvertFromVariable)
						{
							continue;
						}

						LeftValueBase mem = (LeftValueBase)opstep.reg;
						if (!dict_MemReg_Slot.ContainsKey(mem.valueType))
						{
							dict_MemReg_Slot.Add(mem.valueType, new Dictionary<IMemReg, memregSlot>());
						}
						if (!dict_SlotPool.ContainsKey(mem.valueType))
						{
							dict_SlotPool.Add(mem.valueType, new Queue<memregSlot>());
						}
						if (!dict_allocedSlots.ContainsKey(mem.valueType))
						{
							dict_allocedSlots.Add(mem.valueType, new List<memregSlot>());
						}
						Dictionary<IMemReg, memregSlot> m_s = dict_MemReg_Slot[mem.valueType];
						if (!m_s.ContainsKey((IMemReg)mem))
						{
							Queue<memregSlot> slotpool = dict_SlotPool[mem.valueType];
							if (slotpool.Count > 0)
							{
								var s = slotpool.Dequeue(); //复用
								s.memReg = (IMemReg)mem;
								m_s.Add((IMemReg)mem, s);
							}
							else
							{
								var allocedslots = dict_allocedSlots[mem.valueType];
								memregSlot s = new memregSlot();
								s.memReg = (IMemReg)mem;
								s.index = allocedslots.Count;

								allocedslots.Add(s);

								m_s.Add((IMemReg)mem, s);
							}


							{
								//查找这个MemReg最后一次出现的行
								for (int j = block.opSteps.Count - 1; j >= 0; j--)
								{
									var sl = block.opSteps[j];
									if (ReferenceEquals(sl.reg, mem) ||
										ReferenceEquals(sl.arg1, mem) ||
										ReferenceEquals(sl.arg2, mem)
										)
									{
										regLastStep.Add((IMemReg)mem, sl);
										break;
									}
								}

							}



						}
					}


					//***查找这一行释放的槽***

					bool found = true;
					while (found)
					{
						found = false;
						foreach (var item in regLastStep)
						{
							if (item.Value == block.opSteps[i])
							{
								var slotpool = dict_SlotPool[((LeftValueBase)item.Key).valueType];
								var regisetSlot = dict_MemReg_Slot[((LeftValueBase)item.Key).valueType];

								if (slotpool.Contains(regisetSlot[item.Key]))
								{
									throw new Exception("重复的寄存器池");
								}

								slotpool.Enqueue(regisetSlot[item.Key]);

								regLastStep.Remove(item.Key);

								found = true;
								break;
							}
						}
					}


				}


				foreach (var item in dict_allocedSlots)
				{
					var totoalmems = dict_MemReg_Slot[item.Key];
					foreach (var mem in totoalmems)
					{
						for (int i = 0; i < block.opSteps.Count; i++)
						{
							var step = block.opSteps[i];

							if (ReferenceEquals(step.reg, mem.Key))
							{
								step.reg = (LeftValueBase)mem.Value.memReg;
							}

							if (ReferenceEquals(step.arg1, mem.Key))
							{
								step.arg1 = (LeftValueBase)mem.Value.memReg;
							}

							if (ReferenceEquals(step.arg2, mem.Key))
							{
								step.arg2 = (LeftValueBase)mem.Value.memReg;
							}
						}
					}
				}

				#region 重编号
				//***重新编号
				Dictionary<RunTimeDataType, Dictionary<IMemReg, object>> dict_no = new Dictionary<RunTimeDataType, Dictionary<IMemReg, object>>();
				for (int i = 0; i < block.opSteps.Count; i++)
				{
					var step = block.opSteps[i];
					if (step.reg is IMemReg)
					{
						if (!dict_no.ContainsKey(((LeftValueBase)step.reg).valueType))
						{
							dict_no.Add(((LeftValueBase)step.reg).valueType, new Dictionary<IMemReg, object>());
						}
						var no = dict_no[((LeftValueBase)step.reg).valueType];
						if (!no.ContainsKey((IMemReg)step.reg))
						{
							((IMemReg)step.reg).setId(no.Count);
							no.Add((IMemReg)step.reg, null);
						}

						step.memregid1 = (short)((IMemReg)step.reg).getId();

					}
					if (step.arg1 is IMemReg)
					{
						if (!dict_no.ContainsKey(((LeftValueBase)step.arg1).valueType))
						{
							dict_no.Add(((LeftValueBase)step.arg1).valueType, new Dictionary<IMemReg, object>());
						}
						var no = dict_no[((LeftValueBase)step.arg1).valueType];
						if (!no.ContainsKey((IMemReg)step.arg1))
						{
							((IMemReg)step.arg1).setId(no.Count);
							no.Add((IMemReg)step.arg1, null);
						}

						step.memregid2 = (short)((IMemReg)step.arg1).getId();
					}
					if (step.arg2 is IMemReg)
					{
						if (!dict_no.ContainsKey(((LeftValueBase)step.arg2).valueType))
						{
							dict_no.Add(((LeftValueBase)step.arg2).valueType, new Dictionary<IMemReg, object>());
						}
						var no = dict_no[((LeftValueBase)step.arg2).valueType];
						if (!no.ContainsKey((IMemReg)step.arg2))
						{
							((IMemReg)step.arg2).setId(no.Count);
							no.Add((IMemReg)step.arg2, null);
						}

						step.memregid3 = (short)((IMemReg)step.arg2).getId();
					}
				}

				block.dictMemCacheCount = new Dictionary<RunTimeDataType, int>();
				block.memCacheList = new List<IMemReg>();
				foreach (var item in dict_no)
				{
					block.dictMemCacheCount.Add(item.Key, item.Value.Count);

					foreach (var memreg in item.Value.Keys)
					{
						block.memCacheList.Add(memreg);
					}

				}

				#endregion
			}

			
			//调整指令
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (step.opCode == OpCode.afterIncDes_clear_v1_link)
				{
					if (!(step.arg1 is StackSlotAccessor))
					{
						block.opSteps.RemoveAt(i); --i;
						//step.opCode = OpCode.flag;
					}
				}
				else if (step.opCode == OpCode.add_number)
				{
					if (step.reg is MemRegister_Number)
					{
						if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
						{
							step.opCode = OpCode.add_number_memnumber_memnumber;
						}
						else if (step.arg1 is MemRegister_Number && step.arg2 is ASBinCode.rtData.RightValue && step.arg2.valueType == RunTimeDataType.rt_number)
						{
							step.opCode = OpCode.add_number_memnumber_constnumber;
							step.constnumber2 = step.arg2.getValue(null, null).toNumber();
						}
						else if (step.arg1 is MemRegister_Int && step.arg2 is MemRegister_Int)
						{
							step.opCode = OpCode.add_number_memint_memint;
						}
						else if (step.arg1 is MemRegister_Int && step.arg2 is ASBinCode.rtData.RightValue && step.arg2.valueType == RunTimeDataType.rt_number)
						{
							step.opCode = OpCode.add_number_memint_constnumber;
							step.constnumber2 = step.arg2.getValue(null, null).toNumber();
						}
						else if (step.reg is MemRegister_Number && step.arg1 is StackSlotAccessor && step.arg2 is MemRegister_Int)
						{
							step.opCode = OpCode.add_number_memnumber_slt_memint;
						}
						else if (step.reg is MemRegister_Number && step.arg1 is StackSlotAccessor && step.arg2 is MemRegister_Number)
						{
							step.opCode = OpCode.add_number_memnumber_slt_memnumber;
						}
					}
				}
				else if (step.opCode == OpCode.sub_number)
				{
					if (step.reg is MemRegister_Number)
					{
						if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
						{
							step.opCode = OpCode.sub_number_memnumber_memnumber;
						}
						else if (step.reg is MemRegister_Number && step.arg1 is StackSlotAccessor && step.arg2 is ASBinCode.rtData.RightValue && step.arg2.valueType == RunTimeDataType.rt_number)
						{
							step.opCode = OpCode.sub_number_memnumber_slt_constnumber;
							step.constnumber2 = step.arg2.getValue(null, null).toNumber();
						}
					}
				}
				else if (step.opCode == OpCode.div_number)
				{
					if (step.reg is MemRegister_Number)
					{
						if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
						{
							step.opCode = OpCode.div_number_memnumber_memnumber;
						}
						else if (step.arg1 is MemRegister_Number && step.arg2 is ASBinCode.rtData.RightValue && step.arg2.valueType == RunTimeDataType.rt_number)
						{
							step.opCode = OpCode.div_number_memnumber_constnumber;
							step.constnumber2 = step.arg2.getValue(null, null).toNumber();
						}
						else if (step.arg1 is MemRegister_Int && step.arg2 is ASBinCode.rtData.RightValue && step.arg2.valueType == RunTimeDataType.rt_number)
						{
							step.opCode = OpCode.div_number_memint_constnumber;
							step.constnumber2 = step.arg2.getValue(null, null).toNumber();
						}
					}
				}
				else if (step.opCode == OpCode.multi_number)
				{
					if (step.reg is MemRegister_Number)
					{
						if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
						{
							step.opCode = OpCode.multi_number_memnumber_memnumber;
						}
					}
				}
				else if (step.opCode == OpCode.suffix_inc_number)
				{
					if (step.reg is MemRegister_Number)
					{
						if (step.arg1 is MemRegister_Number)
						{
							step.opCode = OpCode.suffix_inc_number_memnumber;
						}
					}
				}
				else if (step.opCode == OpCode.suffix_inc_int)
				{
					if (step.reg is MemRegister_Int)
					{
						if (step.arg1 is MemRegister_Int)
						{
							step.opCode = OpCode.suffix_inc_int_memint;
						}
					}
				}
				else if (step.opCode == OpCode.assigning)
				{
					if (step.reg is MemRegister_Number)
					{
						step.opCode = OpCode.assign_tomemnumber;

						if (step.arg1 is MemRegister_Number)
						{
							step.opCode = OpCode.assign_memnumber_tomemnumber;
						}
					}
					else if (step.reg is MemRegister_Int)
					{
						step.opCode = OpCode.assign_tomemint;
						if (step.arg1 is MemRegister_Int)
						{
							step.opCode = OpCode.assign_memint_tomemint;
						}
					}

				}
				else if (step.opCode == OpCode.cast_number_int)
				{
					if (step.reg is MemRegister_Int && step.arg1 is MemRegister_Number)
					{
						step.opCode = OpCode.cast_number_int_memnumber_memint;
					}
					else if (step.reg is MemRegister_Int && step.arg1 is ASBinCode.rtData.RightValue && step.arg1.valueType == RunTimeDataType.rt_number
						)
					{
						double r = step.arg1.getValue(null, null).toNumber();
						if (!double.IsNaN(r) && !double.IsInfinity(r))
						{
							step.opCode = OpCode.cast_number_int_constnum_memint;
							step.constnumber1 = r;
						}
					}
				}
				else if (step.opCode == OpCode.vector_getvalue)
				{
					if (step.reg is MemRegister_Int && step.arg2 is MemRegister_Int)
					{
						step.opCode = OpCode.vector_getvalue_memint_memintidx;
					}
				}
			}

		}

		//private bool checkTryBlock(int checkline, int lastline, StackSlotAccessor register)
		//{
		//	//**事先已判断所有的回跳行，所以不可能再出现跳回保存行的情况
		//	//如果在catch块,finally块中被引用，可能导致异常的跳转，检查到就跳过
		//	for (int k = checkline + 1; k < lastline + 1; k++)
		//	{
		//		var teststep = block.opSteps[k];
		//		if (isReference(register, teststep))
		//		{
		//			if (teststep.trytype == 1 || teststep.trytype == 2)
		//			{
		//				return true;
		//			}
		//		}

		//	}

		//	return false;
		//}

		private bool checkHasJumpBackOrTryBlock(int checkline, int lastline, StackSlotAccessor register)
		{
			//**如果会跳回保存行，则说明不安全。
			//会导致有可能递归操作时意外修改值。所以检查到就跳过。***
			//如果在catch块,finally块中被引用，可能导致异常的跳转，检查到就跳过
			for (int k = checkline + 1; k < lastline + 1; k++)
			{
				var teststep = block.opSteps[k];
				if (isReference(register, teststep))
				{
					if (teststep.trytype == 1 || teststep.trytype == 2)
					{
						return true;
					}

					//如果后面还有回跳并跳回当前行前，说明有问题
					//for (int l = k + 1; l < block.opSteps.Count; l++)
					//{
					//	var sss = block.opSteps[l];
					//	if ((builds.AS3FunctionBuilder.isJMP(sss.opCode) || builds.AS3FunctionBuilder.isIfJmp(sss.opCode))
					//		&&
					//		sss.jumoffset < 0
					//		)
					//	{
					//		if (l + sss.jumoffset <= checkline)
					//		{
					//			return true;
					//		}
					//	}

					//}

				}

			}

			//List<int> jumpbacks = new List<int>();
			//var allines = collectAllSteps(checkline, jumpbacks);

			//if (jumpbacks.Count > 0)
			//{

			//	//***跳回中间来的不行***
			//	if (jumpbacks[0] <= checkline)
			//	{
			//		for (int i = 0; i < allines.Count; i++)
			//		{
			//			//if (!isSafeStep(block.opSteps[allines[i]]))
			//			return true;
			//		}

			//		//return false;
			//	}


			//}





			return false;
		}

		
		private void optimizeReg2()
		{
			#region 跳转目标
			Dictionary<OpStep, OpStep> jumpTarget = new Dictionary<OpStep, OpStep>();
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if( builds.AS3FunctionBuilder.isIfJmp(step.opCode) || builds.AS3FunctionBuilder.isJMP(step.opCode))
				{
					jumpTarget.Add(step, block.opSteps[i + step.jumoffset]);
				}
			}

			Action<object> actSetJump = (o) => 
			{
				for (int i = 0; i < block.opSteps.Count; i++)
				{
					var step = block.opSteps[i];
					if (jumpTarget.ContainsKey(step))
					{
						var jumpto = jumpTarget[step];
						for (int j = 0; j < block.opSteps.Count; j++)
						{
							if (ReferenceEquals(jumpto, block.opSteps[j]))
							{
								step.jumoffset = j - i;
								break;
							}
						}

					}
				}
			};

			#endregion

			Dictionary<StackSlotAccessor, OpStep> dictNeedSaveStep = new Dictionary<StackSlotAccessor, OpStep>();

			#region 检测转换前需要另开保存的StackSlotAccessor
			{
				Dictionary<StackSlotAccessor, object> stackslotatreg = new Dictionary<StackSlotAccessor, object>();
				for (int i = 0; i < block.opSteps.Count; i++)
				{
					var s = block.opSteps[i];
					if (s.reg is StackSlotAccessor)
					{
						if (!stackslotatreg.ContainsKey((StackSlotAccessor)s.reg) && !dictNeedSaveStep.ContainsKey((StackSlotAccessor)s.reg))
						{
							stackslotatreg.Add((StackSlotAccessor)s.reg, null);
						}
					}
					if (s.arg1 is StackSlotAccessor)
					{
						if (!stackslotatreg.ContainsKey((StackSlotAccessor)s.arg1) && !dictNeedSaveStep.ContainsKey((StackSlotAccessor)s.arg1))
						{
							dictNeedSaveStep.Add((StackSlotAccessor)s.arg1, null);
						}
					}
					if (s.arg2 is StackSlotAccessor)
					{
						if (!stackslotatreg.ContainsKey((StackSlotAccessor)s.arg2) && !dictNeedSaveStep.ContainsKey((StackSlotAccessor)s.arg2))
						{
							dictNeedSaveStep.Add((StackSlotAccessor)s.arg2, null);
						}
					}
				}
			}
			#endregion


			

			Dictionary<StackSlotAccessor, object> dictCanNotOptimize = new Dictionary<StackSlotAccessor, object>();

			Dictionary<StackSlotAccessor, IMemReg> dictToOptimizeRegister = new Dictionary<StackSlotAccessor, IMemReg>();
			Dictionary<IMemReg, StackSlotAccessor> dictMem_StackSlotAccessor = new Dictionary<IMemReg, StackSlotAccessor>();

			Dictionary<OpStep, Dictionary<StackSlotAccessor, int>> dictToCheck = new Dictionary<OpStep, Dictionary<StackSlotAccessor, int>>();

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				#region 准备检测的StackSlotAccessor
				var step = block.opSteps[i];
				List<StackSlotAccessor> list = new List<StackSlotAccessor>();
				{
					if (step.arg1 is StackSlotAccessor)
					{
						if (!dictCanNotOptimize.ContainsKey((StackSlotAccessor)step.arg1) && !dictToOptimizeRegister.ContainsKey((StackSlotAccessor)step.arg1))
						{
							list.Add((StackSlotAccessor)step.arg1);
						}
					}
					if (step.arg2 is StackSlotAccessor)
					{
						if (!dictCanNotOptimize.ContainsKey((StackSlotAccessor)step.arg2) && !dictToOptimizeRegister.ContainsKey((StackSlotAccessor)step.arg2))
						{
							list.Add((StackSlotAccessor)step.arg2);
						}
					}
					if (step.reg is StackSlotAccessor)
					{
						if (!dictCanNotOptimize.ContainsKey((StackSlotAccessor)step.reg)  &&!dictToOptimizeRegister.ContainsKey((StackSlotAccessor)step.reg))
						{
							list.Add((StackSlotAccessor)step.reg);
						}
					}
				}

				if (dictToCheck.ContainsKey(step))
				{
					foreach (var item in dictToCheck[step].Keys)
					{
						if (!dictCanNotOptimize.ContainsKey(item))
						{
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
						
					}
				}
				
				#endregion

				foreach (var item in list)
				{
					Dictionary<OpStep, object> dictJumpBackTarget = new Dictionary<OpStep, object>();
					bool checkjumpbackpass = true;
					#region 检查所有跳回来的目标行
					{
						List<int> jumpbacks = new List<int>();
						var allines = collectAllSteps(i, jumpbacks);
						int lastline = findLastRefLine(item);
						if (jumpbacks.Count > 0)
						{
							foreach (var line in allines) //行中有不安全因素
							{
								if (!isSafeStep(block.opSteps[line],item, null))
								{

									if (step.reg == item && !(step.arg1 == item || step.arg2 == item))
									{
										//***跳回开始和结束之间***
										foreach (var backline in jumpbacks)
										{
											if (backline > i && backline<=lastline)
											{
												if(!dictJumpBackTarget.ContainsKey(block.opSteps[backline]))
													dictJumpBackTarget.Add(block.opSteps[backline],null);
											}
										}
									}
									else
									{
										
										foreach (var backline in jumpbacks)
										{
											if (backline <= i)
											{
												//***如果有跳回头前，失败，不可以优化
												checkjumpbackpass = false;
											}
											else if (backline <= lastline)
											{
												//必须保存
												if (!dictJumpBackTarget.ContainsKey(block.opSteps[backline]))
													dictJumpBackTarget.Add(block.opSteps[backline], null);
											}
										}
									}

									break;
								}
							}
						}
					}


					#endregion



					bool isspecialcheck =false;
					#region 从特殊检测中移除
					if (dictToCheck.ContainsKey(step))
					{
						if (dictToCheck[step].ContainsKey(item))
						{
							dictToCheck[step].Remove(item);
							isspecialcheck = true;
						}
					}
					#endregion


					StackSlotAccessor register = item;
					if (
						checkjumpbackpass
						&&
						(
							isspecialcheck //说明为特殊检测
							||
							canOptimize(i,block.opSteps[i], register) 
						)
						)
					{
						int lastline = findLastRefLine(register);
						int failedline; List<int> continuelines = new List<int>();

						int startcheckline = i + 1;
						if (!isReference(register, step))
						{
							//是特殊检测
							startcheckline = i;
						}

						if (isAllSafeOperator(startcheckline, lastline, lastline,dictJumpBackTarget, register, out failedline, continuelines))
						{
							
							if (dictNeedSaveStep.ContainsKey(register))
							{
								if (checkHasJumpBackOrTryBlock(i, lastline, register))
								{
									//dictCanNotOptimize.Add(register, null);
									//i--;
									//break;
									continue;
								}
								else
								{
									int refs = 0;
									for (int j = startcheckline; j < lastline; j++)
									{
										if (isReference(register, block.opSteps[j]))
										{
											refs++;
										}
									}

									if (refs > 1)
									{
										//***追加一个保存行，将后面的所有对应替换****
										StackSlotAccessor store = getAdditionalRegister();
										store.valueType = register.valueType;
										store.stmtid = register.stmtid;
										OpStep savestep = new OpStep(OpCode.assigning, block.opSteps[i].token);
										savestep.reg = store;
										savestep.regType = register.valueType;
										savestep.arg1 = register;
										savestep.arg1Type = register.valueType;

										for (int j = startcheckline; j < lastline + 1; j++)
										{
											replaceStackSlotAccessor(register, store, block.opSteps[j]);
										}

										block.opSteps.Insert(startcheckline, savestep);
										dictCanNotOptimize.Add(register, null);
										register = store;

										actSetJump(null);

									}
									else
									{
										dictCanNotOptimize.Add(register, null);
										//i--;
										//break;
										continue;
									}
								}
							}

							if (register.valueType == RunTimeDataType.rt_number)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_Number(register.Id)); dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
								if (step.opCode == OpCode.access_dot && register == step.reg)
									step.opCode = OpCode.access_dot_memregister;
							}
							else if (register.valueType == RunTimeDataType.rt_boolean)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_Boolean(register.Id)); dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
								if (step.opCode == OpCode.access_dot && register == step.reg)
									step.opCode = OpCode.access_dot_memregister;
							}
							else if (register.valueType == RunTimeDataType.rt_int)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_Int(register.Id)); dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
								if (step.opCode == OpCode.access_dot && register == step.reg)
									step.opCode = OpCode.access_dot_memregister;
							}
							else if (register.valueType == RunTimeDataType.rt_uint)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_UInt(register.Id)); dictMem_StackSlotAccessor.Add(dictToOptimizeRegister[register], register);
								if (step.opCode == OpCode.access_dot && register == step.reg)
									step.opCode = OpCode.access_dot_memregister;
							}
							
							
							i--;
							break;
						}
						else
						{
							if (failedline > i)
							{
								continuelines.Sort();
								bool caninsert = true;
								{
									if (checkHasJumpBackOrTryBlock(failedline, lastline, register))
									{
										caninsert = false;
									}
								}

								if (caninsert)
								{
									int det1 = 0;bool hasloop=false;List<int> reflists = new List<int>();
									for (int k = i + 1; k < failedline; k++)
									{
										if (isReference(register, block.opSteps[k]))
										{
											det1++;
											reflists.Add(k);
										}
										if (reflists.Count > 0 && !hasloop)
										{
											var s = block.opSteps[k];
											if (builds.AS3FunctionBuilder.isIfJmp(s.opCode) || builds.AS3FunctionBuilder.isJMP(s.opCode))
											{
												int jumpto = k + s.jumoffset;
												foreach (var idx in reflists)
												{
													if (jumpto <= idx)
													{
														hasloop = true;
													}
												}
											}
										}
									}
									
									if ((det1 > 1 || (det1>0 && hasloop)) && !checkHasJumpBackOrTryBlock(i-1,lastline,register))
									{
										//在失败行前后加入读写
										StackSlotAccessor store = getAdditionalRegister();
										store.valueType = register.valueType;
										store.stmtid = register.stmtid;
										OpStep savestep = new OpStep(OpCode.assigning, block.opSteps[failedline].token);
										savestep.reg = store;
										savestep.regType = register.valueType;
										savestep.arg1 = register;
										savestep.arg1Type = register.valueType;

										//***在各个分支之间插入相应代码***						
										//for (int m = continuelines[0] + 1; m < lastline + 1; m++)
										for (int m = failedline ; m < lastline + 1; m++)
										{
											replaceStackSlotAccessor(register, store, block.opSteps[m]);
										}

										block.opSteps.Insert(failedline, savestep);

										//dictCanNotOptimizeRegister.Add(store, null);
										dictNeedSaveStep.Add(store, savestep);

										actSetJump(null);
										i--;

										//***追加特殊检测***
										if (!dictToCheck.ContainsKey(block.opSteps[ i+1]))
										{
											dictToCheck.Add(block.opSteps[i + 1], new Dictionary<StackSlotAccessor, int>());
										}
										if (!dictToCheck[block.opSteps[i + 1]].ContainsKey(register))
										{
											dictToCheck[block.opSteps[i + 1]].Add(register, -1);
										}


										break;

									}
									else
									{
										if (dictJumpBackTarget.ContainsKey(block.opSteps[failedline]))
										{
											//***必须保存，但是又没有保存，则表示无法继续优化了****
											dictCanNotOptimize.Add(register, null);
										}
										else
										{

											//在失败行后立即进行检测
											if (!dictToCheck.ContainsKey(block.opSteps[failedline + 1]))
											{
												dictToCheck.Add(block.opSteps[failedline + 1], new Dictionary<StackSlotAccessor, int>());
											}
											if (!dictToCheck[block.opSteps[failedline + 1]].ContainsKey(register))
											{
												dictToCheck[block.opSteps[failedline + 1]].Add(register, -1);
											}


											if (!dictNeedSaveStep.ContainsKey(register))
											{
												dictNeedSaveStep.Add(register, block.opSteps[i]);
											}
										}
									}
								}
								else
								{
									if (!dictNeedSaveStep.ContainsKey(register))
									{
										dictNeedSaveStep.Add(register, block.opSteps[i]);
									}
								}
							}
							else
							{
								dictCanNotOptimize.Add(register, null);
							}
						}
					}
					else
					{
						dictCanNotOptimize.Add(register, null);
					}
				}
			}
			
			//从局部变量转换来的StackSlot如果被优化成MemReg,则可以移除
			List<StackSlotAccessor> regConvList = new List<StackSlotAccessor>(block.regConvFromVar);

			foreach (var item in dictToOptimizeRegister)
			{
				regConvList.Remove(item.Key);

				for (int i = 0; i < block.opSteps.Count; i++)
				{
					var opstep = block.opSteps[i];

					if (ReferenceEquals(opstep.reg, item.Key))
					{
						opstep.reg = (LeftValueBase)item.Value;
					}
					if (ReferenceEquals(opstep.arg1, item.Key))
					{
						opstep.arg1 = (LeftValueBase)item.Value;
					}
					if (ReferenceEquals(opstep.arg2, item.Key))
					{
						opstep.arg2 = (LeftValueBase)item.Value;
					}
				}
			}

			block.regConvFromVar = regConvList.ToArray();

			setMemReg(dictMem_StackSlotAccessor);

		}


		private bool isAllSafeOperator(int st, int ed, int realend, Dictionary<OpStep,object> jumpbacktargets ,StackSlotAccessor accessor, out int failedline, List<int> continues)
		{
			for (int i = st; i < ed; i++)
			{
				var opcode = block.opSteps[i].opCode;
				if (builds.AS3FunctionBuilder.isJMP(opcode))
				{
					if (block.opSteps[i].jumoffset > 0)
					{
						if (i + block.opSteps[i].jumoffset < realend)
						{
							List<int> c = new List<int>();
							if (!isAllSafeOperator(i + block.opSteps[i].jumoffset, realend, realend,jumpbacktargets, accessor, out failedline, c)
								)
							{

								foreach (var item in c)
								{
									if (!continues.Contains(item))
									{
										continues.Add(item);
									}
								}

								return false;
							}
						}
					}
				}
				else if (builds.AS3FunctionBuilder.isIfJmp(opcode))
				{
					if (block.opSteps[i].jumoffset > 0)
					{
						if (i + block.opSteps[i].jumoffset < realend)
						{
							bool isfailed1 = false; int failedline1;
							List<int> c = new List<int>();
							if (!isAllSafeOperator(i + 1, i + block.opSteps[i].jumoffset, realend,jumpbacktargets, accessor, out failedline1, c)
							)
							{
								isfailed1 = true;
							}

							//List<int> c2 = new List<int>();
							if (!isAllSafeOperator(i + block.opSteps[i].jumoffset, ed, realend,jumpbacktargets, accessor, out failedline, c)
								)
							{
								if (isfailed1)
								{
									if (failedline1 != failedline)
									{
										failedline = i;

										foreach (var item in c)
										{
											if (!continues.Contains(item))
											{
												continues.Add(item);
											}
										}
										return false;
									}
									else //两个分支的失败行是同一行
									{
										foreach (var item in c)
										{
											if (!continues.Contains(item))
											{
												continues.Add(item);
											}
										}
										return false;
									}
								}
								else
								{

									foreach (var item in c)
									{
										if (!continues.Contains(item))
										{
											continues.Add(item);
										}
									}
									return false;
								}

							}
							else if (isfailed1)
							{
								failedline = i;

								foreach (var item in c)
								{
									if (!continues.Contains(item))
									{
										continues.Add(item);
									}
								}
								return false;
							}


						}
					}
				}
				else
				{
					if (!isSafeStep(block.opSteps[i],accessor,jumpbacktargets))
					{
						failedline = i; continues.Add(i);
						return false;
					}
				}
				//else if (opcode == OpCode.assigning)
				//{
				//	var step = block.opSteps[i];
				//	var sa = step.arg1 as StackSlotAccessor;
				//	if (sa == null)
				//	{

				//	}
				//	else
				//	{
				//		if (sa._regMember != null &&
				//		sa._regMember.bindField is ClassPropertyGetter)
				//		{
				//			failedline = i; continues.Add(i);
				//			return false;
				//		}

				//	}
				//}
				//else if (mabeCallfunction(block.opSteps[i].opCode))
				//{
				//	failedline = i; continues.Add(i);
				//	return false;
				//}
			}
			failedline = 0;
			return true;
		}

		private bool isSafeStep(OpStep step,StackSlotAccessor register,Dictionary<OpStep,object> jumbacktagets)
		{
			var opcode = step.opCode;

			if (builds.AS3FunctionBuilder.isIfJmp(opcode) || builds.AS3FunctionBuilder.isJMP(opcode))
			{
				//throw new Exception("这行不能在这里判断");
			}
			if (jumbacktagets !=null && jumbacktagets.ContainsKey(step))
			{
				return false;
			}

			if (opcode == OpCode.assigning)
			{
				var sa = step.arg1 as StackSlotAccessor;
				if (sa == null)
				{

				}
				else
				{
					if (sa._regMember != null &&
					sa._regMember.bindField is ClassPropertyGetter)
					{
						return false;
					}

				}
			}
			else if (mabeCallfunction(opcode))
			{
				return false;
			}
			
			
			return true;
			
		}

		private bool canOptimize(int line,OpStep step, StackSlotAccessor register)
		{
			//***Vector<Boolean>的特殊情况
			if (register.valueType == RunTimeDataType.rt_boolean)
			{
				StackSlotAccessor test = step.arg1 as StackSlotAccessor;
				if (test != null && test._regMember != null && test._regMember.bindField is ClassMethodGetter)
				{
					if (step.opCode == OpCode.call_function || step.opCode == OpCode.call_function_notcheck || step.opCode == OpCode.call_function_notcheck_notreturnobject)
					{
						ClassMethodGetter methodGetter = test._regMember.bindField as ClassMethodGetter;
						if (methodGetter.name == "shift" && methodGetter.classmember.refClass.name == "Vector.<Boolean>")
						{
							return false;
						}

					}
				}
			}


			if (register.valueType == RunTimeDataType.rt_number
				||
				register.valueType == RunTimeDataType.rt_int
				||
				register.valueType == RunTimeDataType.rt_uint
				||
				register.valueType == RunTimeDataType.rt_boolean
				)
			{

				if (step.opCode == OpCode.access_dot)
				{
					if (register._hasUnaryOrShuffixOrDelete || register._isassigntarget ||
						(
						register._regMember != null &&
						register._regMember.bindField is ClassPropertyGetter))
					{
						return false;
					}
					else
					{
						return true;
					}
				}


				return canSaveToMemReg(step.opCode);
			}
			else
			{
				return false;
			}
		}

		private List<int> collectAllSteps(int startline,List<int> jumpbacklines)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			collectAllSteps(startline, dictionary);
			List<int> result= new List<int>(dictionary.Keys);

			result.Sort();

			for (int i = 0; i < result.Count; i++)
			{
				int line = result[i];

				var s = block.opSteps[line];
				if (builds.AS3FunctionBuilder.isIfJmp(s.opCode) || builds.AS3FunctionBuilder.isJMP(s.opCode))
				{
					if (s.jumoffset < 0)
					{
						jumpbacklines.Add(line + s.jumoffset);
					}
				}
			}
			jumpbacklines.Sort();

			return result;
		}




		private void  collectAllSteps(int startline,Dictionary<int,int> visited)
		{			
			for (int i = startline; i < block.opSteps.Count; i++)
			{
				if (!visited.ContainsKey(i))
				{
					visited.Add(i, i);

					var step = block.opSteps[i];

					if (builds.AS3FunctionBuilder.isJMP(step.opCode))
					{
						if (step.jumoffset > 0)
						{
							i += step.jumoffset;
						}
						else
						{
							int line = i + step.jumoffset;
							collectAllSteps(line, visited);
						}
					}
					else if (builds.AS3FunctionBuilder.isIfJmp(step.opCode))
					{
						//collectAllSteps(i + 1, visited);
						collectAllSteps(i + step.jumoffset, visited);
					}
				}
			}

		}



		private bool canSaveToMemReg(OpCode code)
		{
			switch (code)
			{
				case OpCode.flag:
				case OpCode.flag_call_super_constructor:
				case OpCode.cast:
				case OpCode.assigning:
				case OpCode.add:
				case OpCode.add_number:
				case OpCode.sub:
				case OpCode.sub_number:
				case OpCode.multi:
				case OpCode.div:
				case OpCode.mod:
				case OpCode.neg:
				case OpCode.gt_num:
				case OpCode.gt_void:
				case OpCode.lt_num:
				case OpCode.lt_void:
				case OpCode.ge_num:
				case OpCode.ge_void:
				case OpCode.le_num:
				case OpCode.le_void:
				case OpCode.equality:
				case OpCode.equality_num_num:
				case OpCode.equality_str_str:
				case OpCode.not_equality:
				case OpCode.not_equality_num_num:
				case OpCode.not_equality_str_str:
				case OpCode.strict_equality:
				case OpCode.not_strict_equality:
				case OpCode.logic_not:
				case OpCode.bitAnd:
				case OpCode.bitOr:
				case OpCode.bitNot:
				case OpCode.bitXOR:
				case OpCode.bitLeftShift:
				case OpCode.bitRightShift:
				case OpCode.bitUnsignedRightShift:
				case OpCode.increment:
				case OpCode.decrement:
				case OpCode.increment_int:
				case OpCode.increment_uint:
				case OpCode.increment_number:
				case OpCode.decrement_int:
				case OpCode.decrement_uint:
				case OpCode.decrement_number:
				case OpCode.suffix_inc:
				case OpCode.suffix_inc_int:
				case OpCode.suffix_inc_uint:
				case OpCode.suffix_inc_number:
				case OpCode.suffix_dec:
				case OpCode.suffix_dec_int:
				case OpCode.suffix_dec_uint:
				case OpCode.suffix_dec_number:
				case OpCode.cast_primitive:
				case OpCode.logic_is:
				case OpCode.logic_instanceof:
				case OpCode.logic_in:
				case OpCode.multi_number:
				case OpCode.div_number:
				case OpCode.mod_number:
				case OpCode.unary_plus:
				case OpCode.cast_int_number:
				case OpCode.cast_number_int:
				case OpCode.cast_uint_number:
				case OpCode.cast_number_uint:
				case OpCode.cast_int_uint:
				case OpCode.cast_uint_int:
				case OpCode.vector_getvalue:
				case OpCode.function_return_funvoid_notry:
				case OpCode.if_equality_num_num_jmp_notry:
				case OpCode.if_not_equality_num_num_jmp_notry:
				case OpCode.if_le_num_jmp_notry:
				case OpCode.if_lt_num_jmp_notry:
				case OpCode.if_ge_num_jmp_notry:
				case OpCode.if_gt_num_jmp_notry:
				case OpCode.call_function:
				case OpCode.call_function_notcheck:
				case OpCode.call_function_notcheck_notreturnobject:
				case OpCode.call_function_notcheck_notreturnobject_notnative:
				case OpCode.call_function_notcheck_notreturnobject_notnative_method:
				case OpCode.multi_number_memnumber_memnumber:
				case OpCode.div_number_memnumber_memnumber:
				case OpCode.sub_number_memnumber_memnumber:
					return true;
				default:
					if ((int)code > 150)
					{
						return true;
					}
					else
					{
						return false;
					}

			}
		}

		private bool mabeCallfunction(OpCode code)
		{
			
			switch (code)
			{
				case OpCode.cast:
				case OpCode.assigning:
				case OpCode.add:
				case OpCode.sub:
				case OpCode.multi:
				case OpCode.div:
				case OpCode.mod:
				case OpCode.neg:
				case OpCode.gt_void:
				case OpCode.lt_void:
				case OpCode.ge_void:
				case OpCode.le_void:
				case OpCode.equality:
				case OpCode.not_equality:
				case OpCode.bitAnd:
				case OpCode.bitOr:
				case OpCode.bitNot:
				case OpCode.bitXOR:
				case OpCode.bitLeftShift:
				case OpCode.bitRightShift:
				case OpCode.bitUnsignedRightShift:
				case OpCode.increment:
				case OpCode.decrement:
				case OpCode.suffix_inc:
				case OpCode.suffix_dec:
				case OpCode.call_function:
				case OpCode.access_dot:
				case OpCode.access_dot_byname:
				case OpCode.bracket_access:
				case OpCode.bracket_byname:
				case OpCode.try_read_getter:
				case OpCode.try_write_setter:
				case OpCode.vector_pusharray:
				case OpCode.vector_pushvector:
				case OpCode.vector_initfrmdata:
				case OpCode.forin_get_enumerator:
				case OpCode.foreach_get_enumerator:
				case OpCode.enumerator_movenext:
				case OpCode.enumerator_current:
				case OpCode.enumerator_close:
				case OpCode.logic_in:
				case OpCode.unary_plus:
				case OpCode.call_function_notcheck:
				case OpCode.call_function_notcheck_notreturnobject:
				case OpCode.call_function_notcheck_notreturnobject_notnative:
				case OpCode.call_function_notcheck_notreturnobject_notnative_method:
				case OpCode.new_instance:
				case OpCode.new_instance_class:
				case OpCode.init_staticclass:
				case OpCode.arrayAccessor_bind:
				case OpCode.access_dot_memregister:
					return true;
				default:

					return false;
			}
		}

		private bool isReference(StackSlotAccessor sa, OpStep step)
		{
			if (ReferenceEquals(sa, step.arg1) || ReferenceEquals(sa, step.arg2) || ReferenceEquals(sa, step.reg)
				)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool replaceStackSlotAccessor(StackSlotAccessor old, StackSlotAccessor replaceto, OpStep step)
		{
			bool result = false;
			if (ReferenceEquals(step.reg, old))
			{
				step.reg = replaceto; result = true;
			}
			if (ReferenceEquals(step.arg1, old))
			{
				step.arg1 = replaceto; result = true;
			}
			if (ReferenceEquals(step.arg2, old))
			{
				step.arg2 = replaceto; result = true;
			}
			return result;
		}

		private int findLastRefLine(StackSlotAccessor register)
		{
			int lastline = -1;
			//查找这个寄存器最后一次出现的行
			var steps = block.opSteps;
			for (int j = steps.Count - 1; j >= 0; j--)
			{
				var sl = steps[j];

				if (ReferenceEquals(sl.reg, register) ||
					ReferenceEquals(sl.arg1, register) ||
					ReferenceEquals(sl.arg2, register)
					)
				{
					lastline = j;
					break;
				}
			}

			return lastline;
		}





		

		/// <summary>
		/// 合并寄存器
		/// </summary>
		private void combieRegs()
		{
			
			var steps = block.opSteps;
			//槽位池
			Queue<sslot> slotpool = new Queue<sslot>();
			//寄存器分配的槽位
			Dictionary<StackSlotAccessor, sslot> regisetSlot = new Dictionary<StackSlotAccessor, sslot>();
			//最后访问寄存器的操作行
			Dictionary<StackSlotAccessor, OpStep> regLastStep = new Dictionary<StackSlotAccessor, OpStep>();
			
			List<sslot> allocedslots = new List<sslot>();

			for (int i = 0; i < steps.Count; i++)
			{
				OpStep step = steps[i];

				List<StackSlotAccessor> testregisters = new List<StackSlotAccessor>();
				if (step.reg is StackSlotAccessor)
				{
					testregisters.Add((StackSlotAccessor)step.reg);
				}
				if (step.arg1 is StackSlotAccessor)
				{
					testregisters.Add((StackSlotAccessor)step.arg1);
				}
				if (step.arg2 is StackSlotAccessor)
				{
					testregisters.Add((StackSlotAccessor)step.arg2);
				}

				foreach (var reg in testregisters)
				{
					if (reg.isConvertFromVariable)
					{
						
					}
					else if (!regisetSlot.ContainsKey(reg))
					{
						if (slotpool.Count > 0 && !(reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete))
						{
							sslot s = slotpool.Dequeue(); //复用

							//if (s.register._isassigntarget || s.register._hasUnaryOrShuffixOrDelete || s.register.isFuncResult || s.register._isDotAccessTarget )
							//if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete || reg.isFuncResult)
							{
								//dictAddResetStackOp.Add(reg, step);
							}

							s.register = reg;
							regisetSlot.Add(reg, s);

						}
						else
						{
							sslot s = new sslot();
							s.register = reg;
							s.index = allocedslots.Count;


							allocedslots.Add(s);


							regisetSlot.Add(reg, s);


						}


						{
							//if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete || reg.isFuncResult || reg._isDotAccessTarget)
							{
								//***查找所属stmtid最后一次出现的行
								int stmtid = reg.stmtid;

								for (int j = steps.Count - 1; j >= 0; j--)
								{
									var sl = steps[j];
									{
										StackSlotAccessor r = sl.reg as StackSlotAccessor;
										if (r != null && r.stmtid == stmtid)
										{
											regLastStep.Add(reg, sl);
											break;
										}
									}
									{
										StackSlotAccessor r = sl.arg1 as StackSlotAccessor;
										if (r != null && r.stmtid == stmtid)
										{
											regLastStep.Add(reg, sl);
											break;
										}
									}
									{
										StackSlotAccessor r = sl.arg2 as StackSlotAccessor;
										if (r != null && r.stmtid == stmtid)
										{
											regLastStep.Add(reg, sl);
											break;
										}
									}
								}

							}
							//else
							//{
							//	//查找这个寄存器最后一次出现的行
							//	for (int j = steps.Count - 1; j >= 0; j--)
							//	{
							//		var sl = steps[j];
							//		if (ReferenceEquals(sl.reg, reg) ||
							//			ReferenceEquals(sl.arg1, reg) ||
							//			ReferenceEquals(sl.arg2, reg)
							//			)
							//		{
							//			regLastStep.Add(reg, sl);
							//			break;
							//		}
							//	}
							//}
						}
					}



				}


				//***查找这一行释放的槽***

				bool found = true;
				while (found)
				{
					found = false;
					foreach (var item in regLastStep)
					{
						if (item.Value == step)
						{
							if (slotpool.Contains(regisetSlot[item.Key]))
							{
								throw new Exception("重复的寄存器池");
							}

							var test = item.Key;
							if (!(test._isassigntarget || test._hasUnaryOrShuffixOrDelete))
							{
								slotpool.Enqueue(regisetSlot[item.Key]);
							}

							regLastStep.Remove(item.Key);

							found = true;
							break;
						}
					}
				}
			}


			List<StackSlotAccessor> reglist = new List<StackSlotAccessor>();
			foreach (var item in dictCompileRegisters.Values)
			{
				reglist.Add(item);
			}


			for (int i = 0; i < reglist.Count; i++)
			{
				var reg = reglist[i];
				if (regisetSlot.ContainsKey(reg))
				{
					sslot s = regisetSlot[reg];
					reg._index = s.index;

				}
				else
				{
					if (reg.isConvertFromVariable)
					{

					}
					else
					{
						//无用寄存器
						reg._index = -1;
					}


				}



			}



		}

		/// <summary>
		/// 刷新条件跳转语句等的目标行
		/// </summary>
		public void completSteps(Builder builder)
		{
			#region 删除所有的临时property容器

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (step.opCode == OpCode.access_dot)
				{
					if (step.reg is StackSlotAccessor)
					{
						StackSlotAccessor register = (StackSlotAccessor)step.reg;

						if (register._regMember != null && register._regMember.bindField is ClassPropertyGetter)
						{
							bool hasref = false;
							for (int j = i + 1; j < block.opSteps.Count; j++)
							{
								var test = block.opSteps[j];

								if (test.arg1 == register || test.arg2 == register)
								{
									hasref = true;
									break;
								}
							}

							if (!hasref)
							{
								block.opSteps.RemoveAt(i);
								i--;
							}
						}

					}

				}
			}

			#endregion


			#region 查找所有的InitStaticClass并提前
			{
				Dictionary<int, int> initstatics = new Dictionary<int, int>();
				OpStep toinsert = null;
				int st = 0;
				do
				{
					toinsert = null; int insertto = 0;
					for (int i = st; i < block.opSteps.Count; i++)
					{
						var step = block.opSteps[i];
						if (step.opCode == OpCode.init_staticclass)
						{
							if (step.arg1 is ASBinCode.rtData.RightValue)
							{

								if (i > insertto)
								{
									if (!initstatics.ContainsKey(((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value))
									{
										initstatics.Add(((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value, 0);
										block.opSteps.RemoveAt(i);
										toinsert = step;
										break;
									}
									else
									{
										block.opSteps.RemoveAt(i);
										i--;
									}
								}

							}
						}
					}

					if (toinsert != null)
					{
						block.opSteps.Insert(insertto, toinsert);
						st++;
					}


				} while (toinsert != null);
			}

			#endregion

			#region 先查找yield return,如果有，则在开头插入跳转步骤
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				OpStep step = block.opSteps[i];
				if (step.opCode == OpCode.yield_return)
				{
					OpStep yieldline = new OpStep(OpCode.yield_continuetoline, new SourceToken(0, 0, string.Empty));
					block.opSteps.Insert(0, yieldline);

					break;
				}
			}
			#endregion


			#region 将method提前出循环



			#endregion



			setJumpOffSet();
			setTryState();

			if (!isEval && !(block.scope is ASBinCode.scopes.FunctionScope))
			{
				combieRegs();
			}
		}

		/// <summary>
		/// 设置tryid
		/// </summary>
		private void setTryState()
		{
			Stack<trystate> trys = new Stack<trystate>();
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				ASBinCode.OpStep step = block.opSteps[i];
				if (step.opCode == ASBinCode.OpCode.enter_try)
				{
					block.hasTryStmt = true;
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
					trys.Push(new trystate(0, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_try)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
					var s = trys.Pop();
					if (s.tryid != tryid || s.type != 0)
					{
						throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "try块不匹配");
					}
				}
				else if (step.opCode == OpCode.enter_catch)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
					trys.Push(new trystate(1, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_catch)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
					var s = trys.Pop();
					if (s.tryid != tryid || s.type != 1)
					{
						throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "catch块不匹配");
					}
				}
				else if (step.opCode == OpCode.enter_finally)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
					trys.Push(new trystate(2, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_finally)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
					var s = trys.Pop();
					if (s.tryid != tryid || s.type != 2)
					{
						throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "finally块不匹配");
					}
				}
				else if (trys.Count > 0)
				{
					//step.trys = new Stack<int>();
					trystate[] toadd = trys.ToArray();
					for (int j = 0; j < toadd.Length; j++)
					{
						//step.trys.Push(toadd[j]);
						step.tryid = toadd[j].tryid;
						step.trytype = toadd[j].type;
					}
				}
				else
				{
					step.tryid = -1;
				}

			}
		}
		/// <summary>
		/// 计算跳转目标
		/// </summary>
		private void setJumpOffSet()
		{
			Dictionary<OpStep, string> dictJumpSteps = new Dictionary<OpStep, string>();

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				OpStep step = block.opSteps[i];

				string findflag = null;
				if (step.opCode == ASBinCode.OpCode.if_jmp
					)
				{

					findflag = ((ASBinCode.rtData.rtString)step.arg2.getValue(null, null)).value;
					dictJumpSteps.Add(step, findflag);
				}
				else if (step.opCode == ASBinCode.OpCode.jmp)
				{
					findflag = ((ASBinCode.rtData.rtString)step.arg1.getValue(null, null)).value;
					dictJumpSteps.Add(step, findflag);
				}
			}

			foreach (var item in dictJumpSteps)
			{
				OpStep step = item.Key;
				string findflag = item.Value;

				for (int i = 0; i < block.opSteps.Count; i++)
				{
					if (ReferenceEquals(block.opSteps[i], step))
					{
						bool isfound = false;
						for (int j = 0; j < block.opSteps.Count; j++)
						{
							if (block.opSteps[j].flag == findflag)
							{
								step.jumoffset = j - i;
								isfound = true;
								break;
							}
						}

						if (!isfound)
						{
							throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "跳转标记没有找到");
						}

						break;
					}
				}
			}

		}



		//private void completJump()
		//{
		//	Dictionary<OpStep, string> dictJumpSteps = new Dictionary<OpStep, string>();

		//	for (int i = 0; i < block.opSteps.Count; i++)
		//	{
		//		OpStep step = block.opSteps[i];

		//		string findflag = null; bool isif_jmp = false;
		//		if (step.opCode == ASBinCode.OpCode.if_jmp
		//			)
		//		{
		//			isif_jmp = true;
		//			findflag = ((ASBinCode.rtData.rtString)step.arg2.getValue(null, null)).value;
		//			dictJumpSteps.Add(step, findflag);
		//		}
		//		else if (step.opCode == ASBinCode.OpCode.jmp)
		//		{
		//			findflag = ((ASBinCode.rtData.rtString)step.arg1.getValue(null, null)).value;
		//			dictJumpSteps.Add(step, findflag);
		//		}
		//		if (isif_jmp)
		//		{
		//			if (i > 0 && step.arg1 is StackSlotAccessor)
		//			{
		//				var check = block.opSteps[i - 1];
		//				if (ReferenceEquals(step.arg1, check.reg))
		//				{
		//					if (check.opCode == OpCode.equality_num_num)
		//					{
		//						check.opCode = OpCode.if_equality_num_num_jmp_notry;
		//						block.opSteps.RemoveAt(i);
		//						i--;
		//						dictJumpSteps.Add(check, findflag);

		//						dictJumpSteps.Remove(step);
		//					}
		//					else if (check.opCode == OpCode.not_equality_num_num)
		//					{
		//						check.opCode = OpCode.if_not_equality_num_num_jmp_notry;
		//						block.opSteps.RemoveAt(i);
		//						i--;
		//						dictJumpSteps.Add(check, findflag);

		//						dictJumpSteps.Remove(step);
		//					}
		//					else if (check.opCode == OpCode.le_num)
		//					{
		//						check.opCode = OpCode.if_le_num_jmp_notry;
		//						block.opSteps.RemoveAt(i);
		//						i--;
		//						dictJumpSteps.Add(check, findflag);

		//						dictJumpSteps.Remove(step);
		//					}
		//					else if (check.opCode == OpCode.lt_num)
		//					{
		//						check.opCode = OpCode.if_lt_num_jmp_notry;
		//						block.opSteps.RemoveAt(i);
		//						i--;
		//						dictJumpSteps.Add(check, findflag);

		//						dictJumpSteps.Remove(step);
		//					}
		//					else if (check.opCode == OpCode.ge_num)
		//					{
		//						check.opCode = OpCode.if_ge_num_jmp_notry;
		//						block.opSteps.RemoveAt(i);
		//						i--;
		//						dictJumpSteps.Add(check, findflag);

		//						dictJumpSteps.Remove(step);
		//					}
		//					else if (check.opCode == OpCode.gt_num)
		//					{
		//						check.opCode = OpCode.if_gt_num_jmp_notry;
		//						block.opSteps.RemoveAt(i);
		//						i--;
		//						dictJumpSteps.Add(check, findflag);

		//						dictJumpSteps.Remove(step);
		//					}
		//				}
		//			}
		//		}
		//	}

		//	foreach (var item in dictJumpSteps)
		//	{
		//		OpStep step = item.Key;
		//		string findflag = item.Value;

		//		for (int i = 0; i < block.opSteps.Count; i++)
		//		{
		//			if (ReferenceEquals(block.opSteps[i], step))
		//			{
		//				bool isfound = false;
		//				for (int j = 0; j < block.opSteps.Count; j++)
		//				{
		//					if (block.opSteps[j].flag == findflag)
		//					{
		//						step.jumoffset = j - i;
		//						isfound = true;
		//						break;
		//					}
		//				}

		//				if (!isfound)
		//				{
		//					throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "跳转标记没有找到");
		//				}

		//				break;
		//			}
		//		}
		//	}

		//	Stack<trystate> trys = new Stack<trystate>();
		//	for (int i = 0; i < block.opSteps.Count; i++)
		//	{
		//		ASBinCode.OpStep step = block.opSteps[i];
		//		if (step.opCode == ASBinCode.OpCode.enter_try)
		//		{
		//			block.hasTryStmt = true;
		//			int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
		//			trys.Push(new trystate(0, tryid));
		//		}
		//		else if (step.opCode == ASBinCode.OpCode.quit_try)
		//		{
		//			int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
		//			var s = trys.Pop();
		//			if (s.tryid != tryid || s.type != 0)
		//			{
		//				throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "try块不匹配");
		//			}
		//		}
		//		else if (step.opCode == OpCode.enter_catch)
		//		{
		//			int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
		//			trys.Push(new trystate(1, tryid));
		//		}
		//		else if (step.opCode == ASBinCode.OpCode.quit_catch)
		//		{
		//			int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
		//			var s = trys.Pop();
		//			if (s.tryid != tryid || s.type != 1)
		//			{
		//				throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "catch块不匹配");
		//			}
		//		}
		//		else if (step.opCode == OpCode.enter_finally)
		//		{
		//			int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
		//			trys.Push(new trystate(2, tryid));
		//		}
		//		else if (step.opCode == ASBinCode.OpCode.quit_finally)
		//		{
		//			int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null)).value;
		//			var s = trys.Pop();
		//			if (s.tryid != tryid || s.type != 2)
		//			{
		//				throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "finally块不匹配");
		//			}
		//		}
		//		else if (trys.Count > 0)
		//		{
		//			//step.trys = new Stack<int>();
		//			trystate[] toadd = trys.ToArray();
		//			for (int j = 0; j < toadd.Length; j++)
		//			{
		//				//step.trys.Push(toadd[j]);
		//				step.tryid = toadd[j].tryid;
		//				step.trytype = toadd[j].type;
		//			}
		//		}
		//		else
		//		{
		//			step.tryid = -1;
		//		}

		//	}

		//	setJumpInstructions();
		//}

		private void setJumpInstructions()
		{
			//***优化跳转目标调用
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (step.opCode == OpCode.jmp)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						step.opCode = OpCode.jmp_notry;
					}
				}
				else if (step.opCode == OpCode.if_jmp)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						step.opCode = OpCode.if_jmp_notry;
					}
					else
					{
						throw new Exception("if_jmp 跳转到了不同的block,不可能");
					}
				}
				else if (step.opCode == OpCode.if_equality_num_num_jmp_notry)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						if (!searchRefrece(step.reg, i + 1))
						{
							step.opCode = OpCode.if_equality_num_num_jmp_notry_noreference;
							step.reg = null; step.regType = RunTimeDataType.unknown;
						}
					}
					else
					{
						throw new Exception("if_jmp 跳转到了不同的block,不可能");
					}
				}
				else if (step.opCode == OpCode.if_not_equality_num_num_jmp_notry)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						if (!searchRefrece(step.reg, i + 1))
						{
							step.opCode = OpCode.if_not_equality_num_num_jmp_notry_noreference;
							step.reg = null; step.regType = RunTimeDataType.unknown;
						}
					}
					else
					{
						throw new Exception("if_jmp 跳转到了不同的block,不可能");
					}

				}
				else if (step.opCode == OpCode.if_le_num_jmp_notry)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						if (!searchRefrece(step.reg, i + 1))
						{
							step.opCode = OpCode.if_le_num_jmp_notry_noreference;
							step.reg = null; step.regType = RunTimeDataType.unknown;
						}
					}
					else
					{
						throw new Exception("if_jmp 跳转到了不同的block,不可能");
					}

				}
				else if (step.opCode == OpCode.if_lt_num_jmp_notry)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						if (!searchRefrece(step.reg, i + 1))
						{
							step.opCode = OpCode.if_lt_num_jmp_notry_noreference;
							step.reg = null; step.regType = RunTimeDataType.unknown;

							if (step.arg2 is ASBinCode.rtData.RightValue && step.arg1 is MemRegister_Number)
							{
								step.opCode = OpCode.if_lt_memnumber_constnum_jmp_notry_noreference;
								step.constnumber2 = step.arg2.getValue(null, null).toNumber();
							}
							else if (step.arg2 is ASBinCode.rtData.RightValue && step.arg1 is MemRegister_Int)
							{
								step.opCode = OpCode.if_lt_memint_constnum_jmp_notry_noreference;
								step.constnumber2 = step.arg2.getValue(null, null).toNumber();
							}
						}
					}
					else
					{
						throw new Exception("if_jmp 跳转到了不同的block,不可能");
					}
				}
				else if (step.opCode == OpCode.if_ge_num_jmp_notry)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						if (!searchRefrece(step.reg, i + 1))
						{
							step.opCode = OpCode.if_ge_num_jmp_notry_noreference;
							step.reg = null; step.regType = RunTimeDataType.unknown;
						}
					}
					else
					{
						throw new Exception("if_jmp 跳转到了不同的block,不可能");
					}
				}
				else if (step.opCode == OpCode.if_gt_num_jmp_notry)
				{
					var newline = block.opSteps[step.jumoffset + i];
					if (newline.tryid == step.tryid && newline.trytype == step.trytype)
					{
						if (!searchRefrece(step.reg, i + 1))
						{
							step.opCode = OpCode.if_gt_num_jmp_notry_noreference;
							step.reg = null; step.regType = RunTimeDataType.unknown;
						}
					}
					else
					{
						throw new Exception("if_jmp 跳转到了不同的block,不可能");
					}
				}
			}


		}


		/// <summary>
		/// 优化跳转代码并重算trystate
		/// </summary>
		private void optimizeJump()
		{
			Dictionary<OpStep, string> dictJumpSteps = new Dictionary<OpStep, string>();

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				OpStep step = block.opSteps[i];

				string findflag = null; bool isif_jmp = false;
				if (step.opCode == ASBinCode.OpCode.if_jmp
					)
				{
					isif_jmp = true;
					findflag = ((ASBinCode.rtData.rtString)step.arg2.getValue(null, null)).value;
					dictJumpSteps.Add(step, findflag);
				}
				else if (step.opCode == ASBinCode.OpCode.jmp)
				{
					findflag = ((ASBinCode.rtData.rtString)step.arg1.getValue(null, null)).value;
					dictJumpSteps.Add(step, findflag);
				}
				if (isif_jmp)
				{
					if (i > 0 && (step.arg1 is StackSlotAccessor || step.arg1 is MemRegister_Boolean))
					{
						var check = block.opSteps[i - 1];
						if (ReferenceEquals(step.arg1, check.reg))
						{
							if (check.opCode == OpCode.equality_num_num)
							{
								check.opCode = OpCode.if_equality_num_num_jmp_notry;
								block.opSteps.RemoveAt(i);
								i--;
								dictJumpSteps.Add(check, findflag);

								dictJumpSteps.Remove(step);
							}
							else if (check.opCode == OpCode.not_equality_num_num)
							{
								check.opCode = OpCode.if_not_equality_num_num_jmp_notry;
								block.opSteps.RemoveAt(i);
								i--;
								dictJumpSteps.Add(check, findflag);

								dictJumpSteps.Remove(step);
							}
							else if (check.opCode == OpCode.le_num)
							{
								check.opCode = OpCode.if_le_num_jmp_notry;
								block.opSteps.RemoveAt(i);
								i--;
								dictJumpSteps.Add(check, findflag);

								dictJumpSteps.Remove(step);
							}
							else if (check.opCode == OpCode.lt_num)
							{
								check.opCode = OpCode.if_lt_num_jmp_notry;
								block.opSteps.RemoveAt(i);
								i--;
								dictJumpSteps.Add(check, findflag);

								dictJumpSteps.Remove(step);
							}
							else if (check.opCode == OpCode.ge_num)
							{
								check.opCode = OpCode.if_ge_num_jmp_notry;
								block.opSteps.RemoveAt(i);
								i--;
								dictJumpSteps.Add(check, findflag);

								dictJumpSteps.Remove(step);
							}
							else if (check.opCode == OpCode.gt_num)
							{
								check.opCode = OpCode.if_gt_num_jmp_notry;
								block.opSteps.RemoveAt(i);
								i--;
								dictJumpSteps.Add(check, findflag);

								dictJumpSteps.Remove(step);
							}
						}
					}
				}
			}

			foreach (var item in dictJumpSteps)
			{
				OpStep step = item.Key;
				string findflag = item.Value;

				for (int i = 0; i < block.opSteps.Count; i++)
				{
					if (ReferenceEquals(block.opSteps[i], step))
					{
						bool isfound = false;
						for (int j = 0; j < block.opSteps.Count; j++)
						{
							if (block.opSteps[j].flag == findflag)
							{
								step.jumoffset = j - i;
								isfound = true;
								break;
							}
						}

						if (!isfound)
						{
							throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "跳转标记没有找到");
						}

						break;
					}
				}
			}

			setTryState();

			setJumpInstructions();
		}

		private bool searchRefrece(LeftValueBase register, int stoffset)
		{
			for (int i = stoffset; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (ReferenceEquals(register, step.arg1) || ReferenceEquals(register, step.arg2))
				{
					return true;
				}
			}
			return false;
		}


		public readonly bool isEval;

		public CompileEnv(ASBinCode.CodeBlock block, bool isEval)
		{
			this.block = block;

			dictCompileRegisters = new Dictionary<string, ASBinCode.StackSlotAccessor>();

			this.isEval = isEval;

		}












		struct trystate
		{
			public int type;
			public int tryid;
			public trystate(int type, int tryid)
			{
				this.type = type;
				this.tryid = tryid;
			}
		}

		class sslot
		{
			public StackSlotAccessor register;
			public int index;
		}


		class memregSlot
		{
			public IMemReg memReg;
			public int index;
		}

	}
}
