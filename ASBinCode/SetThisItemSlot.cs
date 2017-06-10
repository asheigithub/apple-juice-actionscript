using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASBinCode
{
    public sealed class SetThisItemSlot : SLOT
    {
        public rtObject bindObj;
        public RunTimeValueBase setindex;
        
        public override void clear()
        {
            setindex = null;
            
            bindObj = null;

            
        }

        public override bool isSetThisItem
        {
            get { return true; }
        }

        public override bool directSet(RunTimeValueBase value)
        {
           
            return false;
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
