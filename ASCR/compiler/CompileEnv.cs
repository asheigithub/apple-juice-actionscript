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

        private int labelIdx=0;
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
                ASBinCode.StackSlotAccessor reg = new ASBinCode.StackSlotAccessor(dictCompileRegisters.Count,as3reg.StmtID);
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

            return maxindex+1;
        }

        struct trystate
        {
            public int type;
            public int tryid;
            public trystate(int type,int tryid)
            {
                this.type = type;
                this.tryid = tryid;
            }
        }

        public void convertVarToReg(Builder builder,ASBinCode.rtti.FunctionDefine f)
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

			optimizeReg();
        }

		private void optimizeReg()
		{
			
			Dictionary<StackSlotAccessor, LeftValueBase> dictToOptimizeRegister = new Dictionary<StackSlotAccessor, LeftValueBase>();
			Dictionary<StackSlotAccessor, object> dictCanNotOptimizeRegister = new Dictionary<StackSlotAccessor, object>();

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (step.reg is StackSlotAccessor && !dictToOptimizeRegister.ContainsKey((StackSlotAccessor)step.reg) && ! dictCanNotOptimizeRegister.ContainsKey( (StackSlotAccessor)step.reg ))
				{
					StackSlotAccessor register = (StackSlotAccessor)step.reg;
					if (canOptimize(step,register))
					{
						
						int lastline = findLastRefLine(register);

						if (isAllSafeOperator(i+1, lastline, register))
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

			for (int i = 0; i < block.opSteps.Count; i++)
			{
				var step = block.opSteps[i];
				if (step.opCode == OpCode.afterIncDes_clear_v1_link)
				{
					if (!(step.arg1 is StackSlotAccessor))
					{
						step.opCode = OpCode.flag;
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
				}
				else if (step.opCode == OpCode.multi_number)
				{
					if (step.arg1 is MemRegister_Number && step.arg2 is MemRegister_Number)
					{
						step.opCode = OpCode.multi_number_memnumber_memnumber;
					}
				}
			}

		}

		private bool isAllSafeOperator(int st,int ed,StackSlotAccessor accessor)
		{
			for (int i = st; i < ed; i++)
			{
				var opcode = block.opSteps[i].opCode;
				if (builds.AS3FunctionBuilder.isIfJmp(opcode) || builds.AS3FunctionBuilder.isJMP(opcode))
				{
					if (block.opSteps[i].jumoffset >= 0)
					{

					}
					else
					{
						if (!isAllSafeOperator(i + block.opSteps[i].jumoffset, st - 1, accessor))
						{
							return false;
						}
					}

				}
				else if (
					opcode == OpCode.call_function ||
					opcode == OpCode.call_function_notcheck ||
					opcode == OpCode.call_function_notcheck_notreturnobject ||
					opcode == OpCode.call_function_notcheck_notreturnobject_notnative ||
					opcode == OpCode.call_function_notcheck_notreturnobject_notnative_method
					)
				{
					return false;
				}
				else if (opcode == OpCode.assigning)
				{

				}
				else if (opcode == OpCode.access_method)
				{
					//必然不会涉及方法调用，因此是安全的
				}
				else if (opcode == OpCode.make_para_scope ||
						opcode== OpCode.make_para_scope_method||
						opcode== OpCode.make_para_scope_method_noparameters||
						opcode== OpCode.make_para_scope_method_notnativeconstpara_allparaonstack||
						opcode== OpCode.make_para_scope_withsignature||
						opcode== OpCode.make_para_scope_withsignature_allparaonstack||
						opcode== OpCode.make_para_scope_withsignature_noparameters ||
						opcode== OpCode.push_parameter ||
						opcode == OpCode.push_parameter_class||
						opcode == OpCode.push_parameter_nativeconstpara||
						opcode == OpCode.push_parameter_para||
						opcode == OpCode.push_parameter_skipcheck_storetoheap||
						opcode == OpCode.push_parameter_skipcheck_storetostack||
						opcode == OpCode.push_parameter_skipcheck_testnative ||
						opcode == OpCode.prepare_constructor_argement 
					)
				{

				}

				else if (!safeStep(block.opSteps[i].opCode))
				{
					return false;
				}
			}
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


				return safeStep(step.opCode);
			}
			return false;
		}

		private bool safeStep(OpCode code)
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
					return false;

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





		class sslot
		{
			public StackSlotAccessor register;
			public int index;
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

			//插入需要追加Reset的操作
			//foreach (var item in dictAddResetStackOp)
			//{
			//	OpStep step = item.Value;

			//	for (int i = 0; i < steps.Count; i++)
			//	{
			//		if (steps[i] == step)
			//		{
			//			OpStep insetStep = new OpStep(OpCode.reset_stackslot, step.token);
			//			insetStep.arg1 = item.Key;
			//			steps.Insert(i, insetStep);
			//			break;
			//		}
			//	}

			//}

			
			List<StackSlotAccessor> reglist = new List<StackSlotAccessor>();
			foreach (var item in dictCompileRegisters.Values)
			{
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
					
					//无用寄存器
					reg._index = -1;

					

				}

				

			}



		}

        /// <summary>
        /// 刷新条件跳转语句等的目标行
        /// </summary>
        public void completSteps(Builder builder)
        {
			{   //***查找所有的InitStaticClass并提前***
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
									}
									else
									{
										block.opSteps.RemoveAt(i);
									}
									
									break;
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




			//****先查找yield return,如果有，则在开头插入跳转步骤**
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

			completJump();

			if (!isEval)
			{
				optimizeReg();
				combieRegs();
				
			}
        }

		private void completJump()
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
					if (i > 0 && step.arg1 is StackSlotAccessor)
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

			Stack<trystate> trys = new Stack<trystate>();
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				ASBinCode.OpStep step = block.opSteps[i];
				if (step.opCode == ASBinCode.OpCode.enter_try)
				{
					block.hasTryStmt = true;
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null,0)).value;
					trys.Push(new trystate(0, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_try)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null,0)).value;
					var s = trys.Pop();
					if (s.tryid != tryid || s.type != 0)
					{
						throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "try块不匹配");
					}
				}
				else if (step.opCode == OpCode.enter_catch)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null,0)).value;
					trys.Push(new trystate(1, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_catch)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null,0)).value;
					var s = trys.Pop();
					if (s.tryid != tryid || s.type != 1)
					{
						throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "catch块不匹配");
					}
				}
				else if (step.opCode == OpCode.enter_finally)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null,0)).value;
					trys.Push(new trystate(2, tryid));
				}
				else if (step.opCode == ASBinCode.OpCode.quit_finally)
				{
					int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null, null,0)).value;
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
						if (!searchRefrece((StackSlotAccessor)step.reg, i + 1))
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
						if (!searchRefrece((StackSlotAccessor)step.reg, i + 1))     
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
						if (!searchRefrece((StackSlotAccessor)step.reg, i + 1))
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
						if (!searchRefrece((StackSlotAccessor)step.reg, i + 1))
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
						if (!searchRefrece((StackSlotAccessor)step.reg, i + 1))
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
						if (!searchRefrece((StackSlotAccessor)step.reg, i + 1))
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
		private bool searchRefrece(StackSlotAccessor register,int stoffset)
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

    }
}
