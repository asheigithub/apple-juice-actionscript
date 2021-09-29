using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    /// <summary>
    /// 包装yield方法
    /// </summary>
    public class YieldObject:DynamicObject
    {
        public HeapSlot[] argements;
        public FunctionDefine yield_function;
        public CodeBlock block;
        public RunTimeValueBase thispointer;
        public RunTimeScope function_bindscope;

        public HeapSlot returnSlot;
        public SLOT _moveNextResultSlot;
        public object _callbacker;

        public YieldObject(ASBinCode.rtti.Class _class):base(_class)
        {
            returnSlot = new HeapSlot();
            
        }

        public override string ToString()
        {
            return "yield object";
        }
    }
}
