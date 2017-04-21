using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public sealed class Variable:VariableBase
    {
        public Variable(string name, int index, int refblockid) : this(name, index, false, refblockid, false)
        {

        }
        public Variable(string name, int index, int refblockid, bool isConst):this(name,index,false,refblockid,isConst)
        {

        }
        public Variable(string name, int index, bool ignoreImplicitCast, int refblockid)
            : this(name, index, ignoreImplicitCast, refblockid, RunTimeDataType.rt_void, false)

        {
        }
        public Variable(string name, int index, bool ignoreImplicitCast, int refblockid, bool isConst)
            :this(name,index,ignoreImplicitCast,refblockid, RunTimeDataType.rt_void,isConst)

        {
        }




        private Variable(string name, int index, bool ignoreImplicitCast, int refblockid, RunTimeDataType type, bool isConst)
            :base(name,index,ignoreImplicitCast,refblockid,type,isConst)
        { }


        public override ISLOT getISlot(IRunTimeScope scope)
        {
            while (refblockid != scope.blockId)
            {
                scope = scope.parent;
            }
            return scope.memberData[indexOfMembers];
        }

        protected override IMember _clone()
        {
            return new Variable(name, _indexOfMembers, ignoreImplicitCast, refblockid, valueType, isConst);
        }
    }
}
