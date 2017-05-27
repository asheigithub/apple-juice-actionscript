using System;
using System.Collections.Generic;
using System.Text;

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

        public FrameInfo(ASBinCode.CodeBlock block,int codeLinePtr,ASBinCode.RunTimeScope scope,
            Dictionary<int,ASBinCode.rtData.rtObject> static_objects,int offset,ASBinCode.SLOT[] stack
            )
        {
            this.codeLinePtr = codeLinePtr;
            this.block = block;
            this.scope = scope;
            this.static_objects = static_objects;
            this.offset = offset;
            this.stack = stack;
        }
    }
}
