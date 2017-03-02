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

        /// <summary>
        /// 行标
        /// </summary>
        public string flag;
        /// <summary>
        /// 跳转偏移量
        /// </summary>
        public int jumoffset;
        /// <summary>
        /// 所属标签
        /// </summary>
        public Stack<string> labels;


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
            if (opCode == OpCode.increment || opCode == OpCode.decrement
                ||
                opCode == OpCode.increment_int || opCode == OpCode.increment_number
                ||
                opCode == OpCode.increment_uint
                ||
                opCode == OpCode.decrement_int || opCode == OpCode.decrement_number || opCode == OpCode.decrement_uint
                )
            {
                return arg1.ToString() + "\t" + opCode.ToString() + "\t";
            }
            else if (opCode == OpCode.if_jmp)
            {
                return opCode.ToString() + "\t" + arg1.ToString() + "\t" + arg2.ToString();
            }
            else if (opCode == OpCode.jmp)
            {
                return opCode.ToString() + "\t" + arg1.ToString();
            }
            else if (opCode == OpCode.flag)
            {
                return flag + ":";
            }
            else if (opCode == OpCode.raise_error)
            {
                return "throw" + "\t" + (arg1 != null ? arg1.ToString() : "");
            }
            else if (opCode == OpCode.enter_try)
            {
                return "enter_try" + "\t" + arg1.ToString();
            }
            else if (opCode == OpCode.quit_try)
            {
                return "quit_try" + "\t" + arg1.ToString();
            }
            else if (opCode == OpCode.enter_catch)
            {
                return "enter_catch" + "\t" + arg1.ToString();
            }
            else if (opCode == OpCode.quit_catch)
            {
                return "quit_catch" + "\t" + arg1.ToString();
            }
            else if (opCode == OpCode.enter_finally)
            {
                return "enter_finally" + "\t" + arg1.ToString();
            }
            else if (opCode == OpCode.quit_finally)
            {
                return "quit_finally" + "\t" + arg1.ToString();
            }
            else if (opCode == OpCode.bind_scope)
            {
                return "bind scope" + "\t" + arg1.ToString();
            }

            if (reg == null)
            {
                return opCode.ToString() + "\t" + (arg1==null?"":arg1.ToString());
            }

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
