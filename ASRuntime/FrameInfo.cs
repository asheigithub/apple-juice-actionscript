using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASRuntime
{
    /// <summary>
    /// 保存StackFrame的状态
    /// </summary>
    class FrameInfo : ASBinCode.RunTimeDataHolder
    {
        public readonly ASBinCode.CodeBlock block;
        public readonly int codeLinePtr;

        public readonly ASBinCode.RunTimeScope scope;
        //public readonly ASBinCode.RunTimeValueBase scope_thispointer;

        public FrameInfo(ASBinCode.CodeBlock block,int codeLinePtr,
            ASBinCode.RunTimeScope scope,
            //ASBinCode.RunTimeValueBase scope_thispointer,
            Dictionary<int,ASBinCode.rtData.rtObject> static_objects,int offset,ASBinCode.SLOT[] stack
            )
        {
            this.codeLinePtr = codeLinePtr;
            this.block = block;
            this.scope = scope;
            //this.scope_thispointer = scope_thispointer;
            this.static_objects = static_objects;
            this.offset = offset;
            this.stack = stack;
        }

        public override void throwError(SourceToken token, int code, string errormessage)
        {
            throw new NotImplementedException();
        }

    }
}
