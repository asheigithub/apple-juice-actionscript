using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASRuntime
{
    class ObjectMemberSlot : HeapSlot
    {
        internal readonly ASBinCode.rtData.rtObject obj;
        public ObjectMemberSlot(ASBinCode.rtData.rtObject obj)
        {
            this.obj = obj;
        }

        public override void directSet(IRunTimeValue value)
        {
            

            base.directSet(value);
            if (value.rtType == RunTimeDataType.rt_function)
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)getValue();
                if (function.this_pointer == null || !function.ismethod)
                {
                    function.setThis(obj);
                }
            }
        }
    }
}
