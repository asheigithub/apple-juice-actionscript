using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASRuntime
{
    class ObjectMemberSlot : HeapSlot
    {
        internal readonly ASBinCode.rtData.rtObjectBase obj;

        internal bool isConstMember;

        private RunTimeDataType FunctionClassRtType;

        public ObjectMemberSlot(ASBinCode.rtData.rtObjectBase obj ,RunTimeDataType FunctionClassRtType)
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
                    TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)value);

                if (function.this_pointer == null || !function.ismethod)
                {
                    function.setThis(obj);
                }
            }

            return true;
        }
    }
}
