using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public class OpStep
    {
        public SourceToken token;

        public OpStep(OpCode op,SourceToken token)
        {
            this.opCode = op;
            this.token = token;
        }


        public OpCode opCode;

        public ILeftValue reg;
        public IRightValue arg1;
        public IRightValue arg2;


        /// <summary>
        /// 输出结果类型
        /// </summary>
        public RunTimeDataType regType;
        /// <summary>
        /// 输入参数1类型
        /// </summary>
        public RunTimeDataType arg1Type;
        /// <summary>
        /// 输入参数2类型
        /// </summary>
        public RunTimeDataType arg2Type;


        public override string ToString()
        {
            string result = reg.ToString() + "\t" + opCode.ToString();

            if (arg1 != null)
            {
                result += "\t" + arg1.ToString();
            }

            if (arg2 != null)
            {
                result += "\t" + arg2.ToString();
            }

            return result;
        }

    }
}
