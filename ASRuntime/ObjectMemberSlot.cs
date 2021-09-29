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

		internal readonly RunTimeDataType slottype;
		private IClassFinder classfinder;


		private static ASBinCode.rtData.rtInt _tempint=new ASBinCode.rtData.rtInt(0);
		private static ASBinCode.rtData.rtUInt _tempuint = new ASBinCode.rtData.rtUInt(0);
		private static ASBinCode.rtData.rtNumber _tempnumber = new ASBinCode.rtData.rtNumber(0);



		public ObjectMemberSlot(ASBinCode.rtData.rtObjectBase obj ,RunTimeDataType FunctionClassRtType,RunTimeDataType slottype, IClassFinder classfinder)
        {
            this.obj = obj;
            this.isConstMember = false;
            this.FunctionClassRtType = FunctionClassRtType;
			this.slottype = slottype;
			this.classfinder = classfinder;
        }


		public override SLOT assign(RunTimeValueBase value, out bool success)
		{
			if (slottype != value.rtType && slottype != RunTimeDataType.rt_void
					&&
					!
					(
						//***检查子类关系****
						(slottype > RunTimeDataType.unknown &&
						value.rtType > RunTimeDataType.unknown &&
						(
						ClassMemberFinder.check_isinherits(value, slottype, classfinder)
						||
						ClassMemberFinder.check_isImplements(value, slottype, classfinder)
						)
						)
						||
						(
							slottype > RunTimeDataType.unknown &&
							value.rtType == RunTimeDataType.rt_null
						)
						||
						(
							slottype == RunTimeDataType.rt_array &&
							value.rtType == RunTimeDataType.rt_null
						)
						||
						(
							slottype == RunTimeDataType.rt_function &&
							value.rtType == RunTimeDataType.rt_null
						)
						||
						(
							slottype == RunTimeDataType.rt_string &&
							value.rtType == RunTimeDataType.rt_null
						)
					)
					)
			{
				//return false;
				success = false;
				return this;
			}
			else
			{
				success = directSet(value);
				return this;
			}

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
