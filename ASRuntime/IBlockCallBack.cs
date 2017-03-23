using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    interface IBlockCallBack
    {
        object args { get; set; }

        ASBinCode.rtti.Object obj { get; set; }

        ASBinCode.IRunTimeScope objScope { get; set; }

        void call(object args);

    }
}
