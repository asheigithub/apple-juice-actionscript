using System;
using System.Collections.Generic;
using System.Linq;
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
            //for (int i = 0; i < additionalEaxList.Count; i++)
            //{
            //    additionalEaxList[i].Id += tempEaxList.Count;
            //}

            //for (int i = 0; i < additionalEaxList.Count; i++)
            //{
            //    tempEaxList.Add(additionalEaxList[i]);
            //}

            //tempEaxList.RemoveAt(0);
            //for (int i = 0; i < tempEaxList.Count; i++)
            //{
            //    tempEaxList[i].Id -= 1;
            //}

            //return tempEaxList.Count;


            return dictCompileRegisters.Count;
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
