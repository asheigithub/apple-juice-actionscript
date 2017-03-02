using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public interface IMember
    {
        string name { get; }

        int indexOfMembers { get; }

        IMember clone();

    }
}
