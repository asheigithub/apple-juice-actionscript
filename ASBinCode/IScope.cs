using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    
    public interface IScope
    {
        
        List<IMember> members { get; }

        IScope parentScope { get; }
    }
}
