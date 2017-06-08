using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public interface ILinkSystemObjCreator
    {
        Type getLinkSystemObjType();
        void setLinkObjectValueToSlot(SLOT slot,object player, object value, Class clsType);
    }
}
