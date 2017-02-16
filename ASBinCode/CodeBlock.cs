using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 代码块
    /// </summary>
    public class CodeBlock
    {
        /// <summary>
        /// 运行环境定义
        /// </summary>
        public IScope scope;
        
        public List<OpStep> opSteps;

        /// <summary>
        /// 本段代码共用了多少个寄存器
        /// </summary>
        public int totalRegisters;

        public CodeBlock()
        {
            opSteps = new List<OpStep>();
        }

        public override string ToString()
        {
            string r = string.Empty;

            for (int i = 0; i < opSteps.Count; i++)
            {
                r = r + opSteps[i].ToString() + "\n";
            }

            return r;
        }

    }
}
