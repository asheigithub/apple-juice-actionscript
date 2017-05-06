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

            ASBinCode.Register reg = new ASBinCode.Register(dictCompileRegisters.Count);
            dictCompileRegisters.Add("ADDITIONAL" + reg.Id, reg);

            return reg;
        }

        /// <summary>
        /// 根据语法树来获取创建Register
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ASBinCode.Register createASTRegister(int id)
        {
            if (dictCompileRegisters.ContainsKey("V" + id))
            {
                return dictCompileRegisters["V" + id];
            }
            else
            {
                ASBinCode.Register reg = new ASBinCode.Register(dictCompileRegisters.Count);
                dictCompileRegisters.Add("V" + id, reg);
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

        /// <summary>
        /// 刷新条件跳转语句等的目标行
        /// </summary>
        public void completSteps()
        {
            for (int i = 0; i < block.opSteps.Count; i++)
            {
                ASBinCode.OpStep step = block.opSteps[i];

                string findflag = null;

                if (step.opCode == ASBinCode.OpCode.if_jmp
                    )
                {
                    findflag = ((ASBinCode.rtData.rtString)step.arg2.getValue(null)).value;
                }
                else if (step.opCode == ASBinCode.OpCode.jmp)
                {
                    findflag = ((ASBinCode.rtData.rtString)step.arg1.getValue(null)).value;
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
                    int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null)).value;
                    trys.Push(new trystate(0, tryid));
                }
                else if (step.opCode == ASBinCode.OpCode.quit_try)
                {
                    int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null)).value;
                    var s = trys.Pop();
                    if (s.tryid != tryid || s.type !=0)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "try块不匹配");
                    }
                }
                else if (step.opCode == OpCode.enter_catch)
                {
                    int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null)).value;
                    trys.Push(new trystate(1, tryid));
                }
                else if (step.opCode == ASBinCode.OpCode.quit_catch)
                {
                    int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null)).value;
                    var s = trys.Pop();
                    if (s.tryid != tryid || s.type != 1)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile, "catch块不匹配");
                    }
                }
                else if (step.opCode == OpCode.enter_finally)
                {
                    int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null)).value;
                    trys.Push(new trystate(2, tryid));
                }
                else if (step.opCode == ASBinCode.OpCode.quit_finally)
                {
                    int tryid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(null)).value;
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

            //***合并寄存器***
            List<Register> reglist = new List<Register>();
            var steps = block.opSteps;
            foreach (var item in dictCompileRegisters.Values)
            {
                reglist.Add(item);
            }

            

            for (int i = 0; i < reglist.Count; i++)
            {
                bool found = false;

                var reg = reglist[i];
                if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete)
                {
                    continue;
                }
                int firstline = reglist.Count;
                int lastline = -1;

                for (int j = 0; j < steps.Count; j++)
                {
                    var step = steps[j];
                    if (ReferenceEquals(step.reg, reg) ||
                        ReferenceEquals(step.arg1, reg) ||
                        ReferenceEquals(step.arg2, reg) 
                        )
                    {
                        if (j < firstline)
                        {
                            firstline = j;
                        }
                        if (j > lastline)
                        {
                            lastline = j;
                        }
                    }
                }

                //***查找刚才寄存器最后一次出现后，才出现的新寄存器,公用一个槽****
                for (int j = 0; j < reglist.Count; j++)
                {
                    var r2 = reglist[j];
                    if (ReferenceEquals(r2, reg)
                        ||
                        r2._isassigntarget || r2._hasUnaryOrShuffixOrDelete
                        ||
                        r2._index != r2.Id
                        )
                    {
                        continue;
                    }

                    for (int k = 0; k < steps.Count; k++)
                    {
                        var step = steps[k];
                        if (ReferenceEquals(step.reg, r2) ||
                            ReferenceEquals(step.arg1, r2) ||
                            ReferenceEquals(step.arg2, r2)
                            )
                        {
                            if (k <= lastline)
                            {
                                break;
                            }
                            else
                            {
                                r2._index = reg._index;
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }

                

            }

            Dictionary<int, int> dictnumber = new Dictionary<int, int>();
            for (int i = 0; i < reglist.Count; i++)
            {
                var idx = reglist[i]._index;
                if (!dictnumber.ContainsKey(idx))
                {
                    dictnumber.Add(idx, dictnumber.Count);
                }

                reglist[i]._index = dictnumber[idx];

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
