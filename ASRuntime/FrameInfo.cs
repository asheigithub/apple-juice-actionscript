using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASRuntime
{
    /// <summary>
    /// 保存StackFrame的状态
    /// </summary>
    class FrameInfo
    {
        public readonly ASBinCode.CodeBlock block;
        public readonly int codeLinePtr;

        //public readonly ASBinCode.RunTimeScope scope;
		//public readonly ASBinCode.RunTimeValueBase scope_thispointer;

		
		private readonly int offset;
        private readonly StackSlot[] stack;

		public FrameInfo(ASBinCode.CodeBlock block,int codeLinePtr,
            ASBinCode.RunTimeScope scope,
            //ASBinCode.RunTimeValueBase scope_thispointer,
            int offset,StackSlot[] stack
            )
        {
            this.codeLinePtr = codeLinePtr;
            this.block = block;
            //this.scope = scope;
            //this.scope_thispointer = scope_thispointer;
            
            this.offset = offset;
            this.stack = stack;
        }

		public StackFrame getTempFrame()
		{
			StackFrame stackframe = new StackFrame();
			stackframe.offset = offset;
			stackframe.stack = stack;

			return stackframe;
		}
       

    }
}
