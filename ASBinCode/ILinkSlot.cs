using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public interface ILinkSlot:ISLOT
    {
        ILinkSlot preSlot { get; set; }

        ILinkSlot nextSlot { get; set; }

        bool propertyIsEnumerable { get; set; }

        bool isDeleted { get; set; }
    }
}
