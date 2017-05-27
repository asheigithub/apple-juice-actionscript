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

        private RunTimeDataType FunctionClassRtType;

        public ObjectMemberSlot(ASBinCode.rtData.rtObject obj ,RunTimeDataType FunctionClassRtType)
        {
            this.obj = obj;
            this.isConstMember = false;
            this.FunctionClassRtType = FunctionClassRtType;
        }

        private bool flaghasset = false;
        public override bool directSet(RunTimeValueBase value)
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
            else if (obj.objScope !=null && value.rtType == FunctionClassRtType)
            {
                ASBinCode.rtData.rtFunction function =(ASBinCode.rtData.rtFunction)
                    TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)value);

                if (function.this_pointer == null || !function.ismethod)
                {
                    function.setThis(obj);
                }
            }

            return true;
        }
    }
}
