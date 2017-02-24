using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
    public class CompileEnv
    {
        public ASBinCode.CodeBlock block;

        //public List<ASBinCode.Register> tempEaxList;
        ///// <summary>
        ///// 编译期间额外增加的寄存器
        ///// </summary>
        //private List<ASBinCode.Register> additionalEaxList;


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
        /// 合并新增寄存器
        /// </summary>
        /// <returns></returns>
        public int combieRegisters()
        {
            return dictCompileRegisters.Count;
        }

        /// <summary>
        /// 刷新条件跳转语句等的目标行
        /// </summary>
        public void completSteps()
        {
            for (int i = 0; i < block.opSteps.Count; i++)
            {
                ASBinCode.OpStep step = block.opSteps[i];

                string findflag=null;

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
                    for (int j = 0; j < block.opSteps.Count ; j++)
                    {
                        if (block.opSteps[j].flag == findflag)
                        {
                            step.jumoffset  = j-i;
                            isfound = true;
                            break;
                        }
                    }

                    if (!isfound)
                    {
                        throw new BuildException(step.token.line,step.token.ptr,step.token.sourceFile ,"跳转标记没有找到");
                    }

                }

            }
        }



        public CompileEnv(ASBinCode.CodeBlock block)
        {
            this.block = block;

            dictCompileRegisters = new Dictionary<string, ASBinCode.Register>();

            //tempEaxList = new List<ASBinCode.Register>();

            //additionalEaxList = new List<ASBinCode.Register>();
        }

    }
}
