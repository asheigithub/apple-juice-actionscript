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

        private Dictionary<string, ASBinCode.Register> dictCompileRegisters;

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

        public ASBinCode.Register getAdditionalRegister()
        {
            //ASBinCode.Register reg = new ASBinCode.Register(additionalEaxList.Count);
            //additionalEaxList.Add(reg);

            //return reg;

            ASBinCode.Register reg = new ASBinCode.Register(dictCompileRegisters.Count, lastStmtId);
            dictCompileRegisters.Add("ADDITIONAL" + reg.Id, reg);

            return reg;
        }

        /// <summary>
        /// 根据语法树来获取创建Register
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ASBinCode.Register createASTRegister(ASTool.AS3.Expr.AS3Reg as3reg)
        {
            if (dictCompileRegisters.ContainsKey("V" + as3reg.ID))
            {
                return dictCompileRegisters["V" + as3reg.ID];
            }
            else
            {
                ASBinCode.Register reg = new ASBinCode.Register(dictCompileRegisters.Count,as3reg.StmtID);
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
        public ASBinCode.Register loadRegisterByAST(int id)
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

            Dictionary<Variable, Register> toReplace = new Dictionary<Variable, Register>();

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
                        
                        Register varReg = getAdditionalRegister();
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
            


        }


		class sslot
		{
			public Register register;
			public int index;
		}

		private void combieRegs()
		{
			var steps = block.opSteps;
			//槽位池
			Queue<sslot> slotpool = new Queue<sslot>();
			//寄存器分配的槽位
			Dictionary<Register, sslot> regisetSlot = new Dictionary<Register, sslot>();
			//最后访问寄存器的操作行
			Dictionary<Register, OpStep> regLastStep = new Dictionary<Register, OpStep>();
			//某个操作行前要插入对寄存器的初始化
			Dictionary<Register, OpStep> dictAddResetStackOp = new Dictionary<Register, OpStep>();

			List<sslot> allocedslots = new List<sslot>();

			for (int i = 0; i < steps.Count; i++)
			{
				OpStep step = steps[i];

				List<Register> testregisters = new List<Register>();
				if (step.reg is Register)
				{
					testregisters.Add((Register)step.reg);
				}
				if (step.arg1 is Register)
				{
					testregisters.Add((Register)step.arg1);
				}
				if (step.arg2 is Register)
				{
					testregisters.Add((Register)step.arg2);
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
										Register r = sl.reg as Register;
										if (r !=null && r.stmtid == stmtid)
										{
											regLastStep.Add(reg, sl);
											break;
										}
									}
									{
										Register r = sl.arg1 as Register;
										if (r != null && r.stmtid == stmtid)
										{
											regLastStep.Add(reg, sl);
											break;
										}
									}
									{
										Register r = sl.arg2 as Register;
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
			foreach (var item in dictAddResetStackOp)
			{
				OpStep step = item.Value;

				for (int i = 0; i < steps.Count; i++)
				{
					if (steps[i] == step)
					{
						OpStep insetStep = new OpStep(OpCode.reset_stackslot, step.token);
						insetStep.arg1 = item.Key;
						steps.Insert(i, insetStep);
						break;
					}
				}

			}

			
			List<Register> reglist = new List<Register>();
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

			//////***合并寄存器***
			////List<Register> reglist = new List<Register>();
			////var steps = block.opSteps;
			////foreach (var item in dictCompileRegisters.Values)
			////{
			////	reglist.Add(item);
			////}

			////Dictionary<Register, OpStep> dictAddResetStackOp = new Dictionary<Register, OpStep>();


			////for (int i = 0; i < reglist.Count; i++)
			////{
			////	bool found = false;

			////	var reg = reglist[i];
			////	//if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete || reg.isFuncResult)
			////	//{
			////	//	continue;
			////	//}


			////	int firstline = reglist.Count;
			////	int lastline = -1;

			////	for (int j = 0; j < steps.Count; j++)
			////	{
			////		var step = steps[j];
			////		if (ReferenceEquals(step.reg, reg) ||
			////			ReferenceEquals(step.arg1, reg) ||
			////			ReferenceEquals(step.arg2, reg)
			////			)
			////		{
			////			if (j < firstline)
			////			{
			////				firstline = j;
			////			}
			////			if (j > lastline)
			////			{
			////				lastline = j;
			////			}
			////		}
			////	}


			////	//***查找刚才寄存器最后一次出现后，才出现的新寄存器,公用一个槽****
			////	for (int j = 0; j < reglist.Count; j++)
			////	{
			////		var r2 = reglist[j];
			////		if (ReferenceEquals(r2, reg)
			////			||
			////			//r2._isassigntarget || r2._hasUnaryOrShuffixOrDelete
			////			//||
			////			r2._index != r2.Id
			////			)
			////		{
			////			continue;
			////		}

			////		for (int k = 0; k < steps.Count; k++)
			////		{
			////			var step = steps[k];
			////			if (ReferenceEquals(step.reg, r2) ||
			////				ReferenceEquals(step.arg1, r2) ||
			////				ReferenceEquals(step.arg2, r2)
			////				)
			////			{
			////				if (k <= lastline)
			////				{
			////					break;
			////				}
			////				else
			////				{
			////					if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete || reg.isFuncResult)
			////					{
			////						dictAddResetStackOp.Add(r2, step);
			////					}


			////					r2._index = reg._index;
			////					found = true;
			////					break;
			////				}
			////			}
			////		}
			////		if (found)
			////		{

			////			break;
			////		}
			////	}
			////}

			//////插入需要追加Reset的操作
			////foreach (var item in dictAddResetStackOp)
			////{
			////	OpStep step = item.Value;

			////	for (int i = 0; i < steps.Count; i++)
			////	{
			////		if (steps[i] == step)
			////		{
			////			OpStep insetStep = new OpStep(OpCode.reset_stackslot, step.token);
			////			insetStep.arg1 = item.Key;
			////			steps.Insert(i, insetStep);
			////			break;
			////		}
			////	}

			////}


			////Dictionary<int, int> dictnumber = new Dictionary<int, int>();
			////for (int i = 0; i < reglist.Count; i++)
			////{
			////	var idx = reglist[i]._index;
			////	if (!dictnumber.ContainsKey(idx))
			////	{
			////		dictnumber.Add(idx, dictnumber.Count);
			////	}

			////	reglist[i]._index = dictnumber[idx];

			////}
			combieRegs();

			completJump();

        }

		private void completJump()
		{
			for (int i = 0; i < block.opSteps.Count; i++)
			{
				ASBinCode.OpStep step = block.opSteps[i];

				string findflag = null;

				if (step.opCode == ASBinCode.OpCode.if_jmp
					)
				{
					findflag = ((ASBinCode.rtData.rtString)step.arg2.getValue(null, null)).value;
				}
				else if (step.opCode == ASBinCode.OpCode.jmp)
				{
					findflag = ((ASBinCode.rtData.rtString)step.arg1.getValue(null, null)).value;
				}

				if (findflag != null)
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

				}
			}


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



        public readonly bool isEval;

        public CompileEnv(ASBinCode.CodeBlock block,bool isEval)
        {
            this.block = block;

            dictCompileRegisters = new Dictionary<string, ASBinCode.Register>();

            this.isEval = isEval;

        }

    }
}
