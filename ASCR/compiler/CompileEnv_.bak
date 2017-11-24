using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
	public class CompileEnv
	{
		public ASBinCode.CodeBlock block;

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
			//ASBinCode.Register reg = new ASBinCode.Register(additionalEaxList.Count);
			//additionalEaxList.Add(reg);

			//return reg;

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
						if(!slots.ContainsKey(sa))
							slots.Add(sa,null);
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
			convertVarToReg(builder, f);

			optimizeReg();

			combieRegs();
		
			optimizeJump();

			resortStackSlotAccessorIndex();
			
		}


		#region 尝试私有变量和参数转换到栈上
		private void convertVarToReg(Builder builder,ASBinCode.rtti.FunctionDefine f)
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

                    bool found = false;
                    //***查找是否被其他块引用***
                    foreach (var tofindblock in blocks)
                    {
                        var steps = tofindblock.opSteps;
                        foreach (var op in steps)
                        {
                            Variable v = op.reg as Variable;
                            if (v !=null && v.name==vm.name && v.indexOfMembers==vm.indexOfMembers && v.refdefinedinblockid==vm.refdefinedinblockid)
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
                            varReg._index = block.totalRegisters;
                            block.totalRegisters++;
                            varReg.valueType = vm.valueType;
                            block.regConvFromVar.Add(varReg);

                        }
                        else
                        {
                            //是参数修改

                            f.signature.onStackParameters++;
                            varReg._index = -f.signature.onStackParameters;
                            f.signature.parameters[i].varorreg = varReg;
                            f.signature.parameters[i].isOnStack = true;
							varReg.valueType = f.signature.parameters[i].type;
                        }

                    }
                }
            }

            if (toReplace.Count > 0)
            {
                foreach (var item in toReplace)
                {
					int replacecount = 0;
                    block.scope.members.Remove(item.Key);
                    foreach (var op in block.opSteps)
                    {
                        //***将所有引用到的Variable替换***
                        if (Object.Equals(op.reg, item.Key))
                        {
                            op.reg = item.Value; ++replacecount;
                        }

                        if (Object.Equals(op.arg1, item.Key))
                        {
                            op.arg1 = item.Value; ++replacecount;
						}

                        if (Object.Equals(op.arg2, item.Key))
                        {
                            op.arg2 = item.Value; ++replacecount;
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
                                
                                if ( Equals(op.reg,var) )
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
		#endregion




		private void optimizeReg()
		{
#region 尝试优化参数是否优化后更好
			StackSlotAccessor[] slist = new StackSlotAccessor[dictCompileRegisters.Count];
			dictCompileRegisters.Values.CopyTo(slist, 0);
			foreach (var r in slist)
			{
				if (r._index < 0)
				{
					int insertinto = 0;
					int maxccount = 0;int breaks = 0;int csegs = 0;
					for (int k = 0; k < block.opSteps.Count; k++)
					{
						if ((block.opSteps[k].opCode == OpCode.init_staticclass
							||
							block.opSteps[k].opCode == OpCode.yield_continuetoline
							)
							&& !isReference(r,block.opSteps[k])
							)
						{
							insertinto++;
							continue;
						}

						if (isReference(r, block.opSteps[k]))
						{
							int lastline = findLastRefLine(r);
							int failedline; List<int> continuelines = new List<int>();

							if (isAllSafeOperator(k, lastline,lastline, r, out failedline, continuelines))
							{
								for (int i = k + 1; i < lastline; i++)
								{
									if (isReference(r, block.opSteps[i]))
									{
										maxccount++;
									}
								}
								csegs = maxccount;
								break;
							}
							else
							{
								int ccount = 0;
								breaks++;
								for (int i = k+1; i < lastline; i++)
								{
									if (isReference(r, block.opSteps[i]))
									{
										ccount++;
										if (ccount > maxccount)
										{
											maxccount = ccount;
										}
										if (ccount == 2)
										{
											csegs++;
										}
									}
									if (i == continuelines[0])
									{
										ccount = 0;
									}
								}
							}
							
						}

					}

					if ( ( csegs *maxccount >breaks *2 ) )
					//if(maxccount>0)
					{
						var load = getAdditionalRegister();
						load.valueType = r.valueType;
						OpStep loadstep = new OpStep(OpCode.assigning, block.opSteps[0].token);
						loadstep.reg = load;
						loadstep.regType = r.valueType;
						loadstep.arg1 = r;
						loadstep.arg1Type = r.valueType;

						for (int k = 0; k < block.opSteps.Count; k++)
						{
							refreshStackSlotAccessor(r, load, block.opSteps[k]);
						}
						block.opSteps.Insert(insertinto, loadstep);
					}

				}
			}
#endregion


			Dictionary<StackSlotAccessor, LeftValueBase> dictToOptimizeRegister = new Dictionary<StackSlotAccessor, LeftValueBase>();
			Dictionary<StackSlotAccessor, object> dictCanNotOptimizeRegister = new Dictionary<StackSlotAccessor, object>();

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				#region 先排除未赋值就使用的槽
				if (!(step.reg is StackSlotAccessor))
				{
					{
						StackSlotAccessor test = step.arg1 as StackSlotAccessor;
						if (test != null)
						{
							//未赋值就使用的StackSlotAccessor不可优化。
							if (!dictCanNotOptimizeRegister.ContainsKey(test) && !dictToOptimizeRegister.ContainsKey(test))
							{
								dictCanNotOptimizeRegister.Add(test, null);
							}
						}
					}
					{
						StackSlotAccessor test = step.arg2 as StackSlotAccessor;
						if (test != null)
						{
							//未赋值就使用的StackSlotAccessor不可优化。
							if (!dictCanNotOptimizeRegister.ContainsKey(test) && !dictToOptimizeRegister.ContainsKey(test))
							{
								dictCanNotOptimizeRegister.Add(test, null);
							}
						}
					}
				}
				#endregion

				if (step.reg is StackSlotAccessor && !dictToOptimizeRegister.ContainsKey((StackSlotAccessor)step.reg) && ! dictCanNotOptimizeRegister.ContainsKey( (StackSlotAccessor)step.reg ))
				{
					StackSlotAccessor register = (StackSlotAccessor)step.reg;
					if (canOptimize(step,register))
					{
						
						int lastline = findLastRefLine(register);
						int failedline;List<int> continuelines = new List<int>();
						if (isAllSafeOperator(i+1, lastline,lastline, register,out failedline,continuelines))
						{
							if (register.valueType == RunTimeDataType.rt_number)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_Number(register.Id));
								if (step.opCode == OpCode.access_dot)
									step.opCode = OpCode.access_dot_memregister;
							}
							else if (register.valueType == RunTimeDataType.rt_boolean)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_Boolean(register.Id));
								if (step.opCode == OpCode.access_dot)
									step.opCode = OpCode.access_dot_memregister;
							}
							else if (register.valueType == RunTimeDataType.rt_int)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_Int(register.Id));
								if (step.opCode == OpCode.access_dot)
									step.opCode = OpCode.access_dot_memregister;
							}
							else if (register.valueType == RunTimeDataType.rt_uint)
							{
								dictToOptimizeRegister.Add(register, new MemRegister_UInt(register.Id));
								if (step.opCode == OpCode.access_dot)
									step.opCode = OpCode.access_dot_memregister;
							}
						}
						else
						{
							if (failedline > i)
							{
								bool caninsert = true;
								
								{
									//**如果后面还有对其修改值的操作,比如赋值和自增，说明为从变量转化而来。
									//会导致有可能递归操作时意外修改值。所以检查到就跳过。***
									for (int k = failedline + 1; k < lastline + 1; k++)
									{
										var teststep = block.opSteps[k];
										if (ReferenceEquals(teststep.reg, register))
										{
											caninsert = false; break;
										}
										else
										if (isReference(register, teststep))
										{
											if (teststep.reg is StackSlotAccessor)
											{
												if (((StackSlotAccessor)teststep.reg)._hasUnaryOrShuffix)
												{
													caninsert = false; break;
												}
											}
										}
									}


								}
								if (caninsert)
								{
									int det1 = 0;
									for (int k = i + 1; k < failedline; k++)
									{
										if (isReference(register, block.opSteps[k]))
										{
											det1++;
										}
									}
									List<int> dets = new List<int>();int maxdet2 = 0;
									for (int j = 0; j < continuelines.Count; j++)
									{
										int d = 0;
										for (int k = continuelines[j]; k < lastline; k++)
										{
											if (isReference(register, block.opSteps[k]))
											{
												d++;
											}
										}
										dets.Add(d);
										if (d > maxdet2)
										{
											maxdet2 = d;
										}
									}
									
									if (det1 > 1 || maxdet2 > 1)
									{
										//在失败行前后加入读写
										var store = getAdditionalRegister();
										store.valueType = register.valueType;
										OpStep savestep = new OpStep(OpCode.assigning, block.opSteps[failedline].token);
										savestep.reg = store;
										savestep.regType = register.valueType;
										savestep.arg1 = register;
										savestep.arg1Type = register.valueType;
										store.stmtid = register.stmtid;
										{
											//***在各个分支之间插入相应代码***
											for (int k = continuelines.Count-1; k>=0; k--)
											{
												if (dets[k] <= 1)
												{
													for (int m = continuelines[k] + 1; m < lastline + 1; m++)
													{
														refreshStackSlotAccessor(register, store, block.opSteps[m]);
													}
												}
												else
												{
													int continueline = continuelines[k];

													var load = getAdditionalRegister();
													load.valueType = register.valueType;
													OpStep loadstep = new OpStep(OpCode.assigning, block.opSteps[continueline].token);
													loadstep.reg = load;
													loadstep.regType = register.valueType;
													loadstep.arg1 = store;
													loadstep.arg1Type = register.valueType;



													int insertline = -1;int addline = 0;
													for (int j = continueline + 1; j < lastline + 1 +addline; j++)
													{
														if (refreshStackSlotAccessor(register, store, block.opSteps[j]))
														{
															if (insertline == -1)
															{
																insertline = j;
															}
														}
													}

													load.stmtid = register.stmtid;

													block.opSteps.Insert(insertline, loadstep);
													addline++;

													dictCanNotOptimizeRegister.Add(load, null);
												}
											}
											
											
										}

										block.opSteps.Insert(failedline, savestep);

										dictCanNotOptimizeRegister.Add(store, null);
										//dictCanNotOptimizeRegister.Add(load, null);

										i--;
										continue;

									}
								}
							}

							dictCanNotOptimizeRegister.Add(register, null);
						}
						
					}
					else
					{
						dictCanNotOptimizeRegister.Add(register, null);
					}
				}
			}

			foreach (var item in dictToOptimizeRegister)
			{
				for (int i = 0; i < block.opSteps.Count; i++)
				{
					var opstep = block.opSteps[i];

					if (ReferenceEquals(opstep.reg, item.Key))
					{
						opstep.reg = item.Value;
					}
					if (ReferenceEquals(opstep.arg1, item.Key))
					{
						opstep.arg1 = item.Value;
					}
					if (ReferenceEquals(opstep.arg2, item.Key))
					{
						opstep.arg2 = item.Value;
					}
				}
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
					if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
					{
						step.opCode = OpCode.add_number_memnumber_memnumber;
					}
					else if (step.arg1 is MemRegister_Number && step.arg2 is ASBinCode.rtData.RightValue && step.arg2.valueType == RunTimeDataType.rt_number)
					{
						step.opCode = OpCode.add_number_memnumber_constnumber;
					}
				}
				else if (step.opCode == OpCode.sub_number)
				{
					if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
					{
						step.opCode = OpCode.sub_number_memnumber_memnumber;
					}
				}
				else if (step.opCode == OpCode.div_number)
				{
					if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
					{
						step.opCode = OpCode.div_number_memnumber_memnumber;
					}
					else if (step.arg1 is MemRegister_Number && step.arg2 is ASBinCode.rtData.RightValue && step.arg2.valueType == RunTimeDataType.rt_number)
					{
						step.opCode = OpCode.div_number_memnumber_constnumber;
					}
				}
				else if (step.opCode == OpCode.multi_number)
				{
					if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
					{
						step.opCode = OpCode.multi_number_memnumber_memnumber;
					}
				}
				else if (step.opCode == OpCode.suffix_inc_number)
				{
					if (step.arg1 is MemRegister_Number)
					{
						step.opCode = OpCode.suffix_inc_number_memnumber;
					}
				}
				else if (step.opCode == OpCode.assigning)
				{
					if (step.reg is MemRegister_Number)
					{
						step.opCode = OpCode.assign_tomemnumber;
					}
				}
			}

		}

		private bool isSafeOperator(StackSlotAccessor accessor,int line)
		{
			int fline;List<int> cline = new List<int>();
			return isAllSafeOperator(line, line + 1,line+1, accessor, out fline,cline);
		}

		private bool isAllSafeOperator(int st,int ed,int realend, StackSlotAccessor accessor,out int failedline,List<int> continues)
		{
			for (int i = st; i < ed; i++)
			{
				var opcode = block.opSteps[i].opCode;
				if (builds.AS3FunctionBuilder.isJMP(opcode))
				{
					
				}
				else if (builds.AS3FunctionBuilder.isIfJmp(opcode))
				{
					if (block.opSteps[i].jumoffset > 0)
					{
						if (i + block.opSteps[i].jumoffset < realend)
						{
							List<int> c = new List<int>();
							if (!isAllSafeOperator(i + 1, i + block.opSteps[i].jumoffset, realend ,accessor, out failedline, c)								
							)
							{
								failedline = i;
								//failedline = i;
								//continues.Add(i);
								foreach (var item in c)
								{
									if (!continues.Contains(item))
									{
										continues.Add(item);
									}
								}
								//continues.Add(i + block.opSteps[i].jumoffset);
								return false;
							}
							if (!isAllSafeOperator(i + block.opSteps[i].jumoffset, ed,realend,  accessor, out failedline, c)								
								)
							{
								
								//continues.Add(i + block.opSteps[i].jumoffset);
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
				else if (opcode == OpCode.assigning)
				{
					var step = block.opSteps[i];
					var sa = step.arg1 as StackSlotAccessor;
					if (sa == null)
					{

					}
					else
					{
						if (sa._regMember != null &&
						sa._regMember.bindField is ClassPropertyGetter)
						{
							failedline = i; continues.Add(i);
							return false;
						}

					}
				}
				else if (mabeCallfunction(block.opSteps[i].opCode))
				{
					failedline = i; continues.Add(i);
					return false;
				}
			}
			failedline = 0;
			return true;
		}

		private bool canOptimize(OpStep step, StackSlotAccessor register)
		{
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
						register._regMember !=null &&
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
			return false;
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

		private int findLastRefLine(StackSlotAccessor register)
		{
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
					return j;					
				}
			}
			return -1;
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
				
					return true;
				default:
					
					return false;
			}
		}

		private bool isReference(StackSlotAccessor sa,OpStep step)
		{
			if (ReferenceEquals(sa, step.arg1) || ReferenceEquals(sa, step.arg2) || ReferenceEquals(sa,step.reg)
				)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool refreshStackSlotAccessor(StackSlotAccessor old, StackSlotAccessor replaceto, OpStep step)
		{
			bool result = false;
			if (ReferenceEquals(step.reg,old))
			{
				step.reg = replaceto;result = true;
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
			//某个操作行前要插入对寄存器的初始化
			//Dictionary<Register, OpStep> dictAddResetStackOp = new Dictionary<Register, OpStep>();

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
					//跳过从参数转换来的
					if (reg.isConvertFromVariable && reg._index < 0)
					{
						continue;
					}

					if (!regisetSlot.ContainsKey(reg))
					{
						if (slotpool.Count > 0 )
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
										if (r !=null && r.stmtid == stmtid)
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

							slotpool.Enqueue(regisetSlot[item.Key]);
							
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
				//跳过从参数转换来的
				if (item.isConvertFromVariable && item._index < 0)
				{
					continue;
				}
				reglist.Add(item);
			}

			


			Dictionary<int, int> dictnumber = new Dictionary<int, int>();

			Dictionary<sslot, int> dictslot = new Dictionary<sslot, int>();

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
						//无用寄存器
						reg._index = int.MinValue;
						block.regConvFromVar.Remove(reg);
					}
					else
					{
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
									if (!initstatics.ContainsKey(((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value))
									{
										initstatics.Add(((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value, 0);
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
                    OpStep yieldline = new OpStep(OpCode.yield_continuetoline, new SourceToken(0,0,string.Empty));
                    block.opSteps.Insert(0, yieldline);

                    break;
                }
            }
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
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value;
					trys.Push(new trystate(0, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_try)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value;
					var s = trys.Pop();
					if (s.tryid != tryid || s.type != 0)
					{
						throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "try块不匹配");
					}
				}
				else if (step.opCode == OpCode.enter_catch)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value;
					trys.Push(new trystate(1, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_catch)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value;
					var s = trys.Pop();
					if (s.tryid != tryid || s.type != 1)
					{
						throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "catch块不匹配");
					}
				}
				else if (step.opCode == OpCode.enter_finally)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value;
					trys.Push(new trystate(2, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_finally)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null, 0)).value;
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
					
					findflag = ((ASBinCode.rtData.rtString)step.arg2.getValue(null, null, 0)).value;
					dictJumpSteps.Add(step, findflag);
				}
				else if (step.opCode == ASBinCode.OpCode.jmp)
				{
					findflag = ((ASBinCode.rtData.rtString)step.arg1.getValue(null, null, 0)).value;
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

		/// <summary>
		/// 优化跳转代码并重算trystate
		/// </summary>
		private void optimizeJump()
		{
			Dictionary<OpStep, string> dictJumpSteps = new Dictionary<OpStep, string>();

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				OpStep step = block.opSteps[i];

				string findflag = null;bool isif_jmp = false;
				if (step.opCode == ASBinCode.OpCode.if_jmp
					)
				{
					isif_jmp = true;
					findflag = ((ASBinCode.rtData.rtString)step.arg2.getValue(null, null, 0)).value;
					dictJumpSteps.Add(step, findflag);
				}
				else if (step.opCode == ASBinCode.OpCode.jmp)
				{
					findflag = ((ASBinCode.rtData.rtString)step.arg1.getValue(null, null, 0)).value;
					dictJumpSteps.Add(step, findflag);
				}
				if (isif_jmp)
				{
					if (i > 0 && (step.arg1 is StackSlotAccessor || step.arg1 is MemRegister_Boolean) )
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
							step.reg = null;step.regType = RunTimeDataType.unknown;
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
		private bool searchRefrece(LeftValueBase register,int stoffset)
		{
			for (int i = stoffset; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (ReferenceEquals(register, step.arg1) || ReferenceEquals(register, step.arg2) )
				{
					return true;
				}
			}
			return false;
		}


        public readonly bool isEval;

        public CompileEnv(ASBinCode.CodeBlock block,bool isEval)
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
	}
}
