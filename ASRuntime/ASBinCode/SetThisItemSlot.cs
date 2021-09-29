using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASBinCode
{
    public sealed class SetThisItemSlot : SLOT
    {
        public rtObjectBase bindObj;
        public RunTimeValueBase setindex;
		public ASBinCode.rtti.ClassMember set_this_item;
		public ASBinCode.rtti.ClassMember get_this_item;

		public override void clear()
        {
            setindex = null;
            
            bindObj = null;

			set_this_item = null;
			get_this_item = null;
        }

        public override bool isSetThisItem
        {
            get { return true; }
        }

        public override bool directSet(RunTimeValueBase value)
        {

			throw new NotImplementedException();
		}

		public override SLOT assign(RunTimeValueBase value, out bool success)
		{
			success = false;
			return this;
		}

		public override RunTimeValueBase getValue()
        {
            throw new NotImplementedException();
        }

        public override void setValue(rtBoolean value)
        {
            throw new NotImplementedException();
        }

        public override void setValue(double value)
        {
            throw new NotImplementedException();
        }

        public override void setValue(int value)
        {
            throw new NotImplementedException();
        }

        public override void setValue(uint value)
        {
            throw new NotImplementedException();
        }

        public override void setValue(string value)
        {
            throw new NotImplementedException();
        }

        public override void setValue(rtNull value)
        {
            throw new NotImplementedException();
        }

        public override void setValue(rtUndefined value)
        {
            throw new NotImplementedException();
        }
    }
}
