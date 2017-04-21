using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASRuntime
{
    class ObjectMemberSlot : HeapSlot
    {
        internal readonly ASBinCode.rtData.rtObject obj;

        internal bool isConstMember;
        public ObjectMemberSlot(ASBinCode.rtData.rtObject obj)
        {
            this.obj = obj;
            this.isConstMember = false;
        }

        private bool flaghasset = false;
        public override bool directSet(IRunTimeValue value)
        {
            if (isConstMember)
            {
                if (!flaghasset)
                {
                    flaghasset = true;
                }
                else
                {
                    return false;
                }
            }

            base.directSet(value);
            if (value.rtType == RunTimeDataType.rt_function)
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)getValue();

                if (function.this_pointer == null || !function.ismethod)
                {
                    function.setThis(obj);
                }
            }

            return true;
        }
    }
}
