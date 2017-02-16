using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 寄存器接口
    /// </summary>
    public interface IEAX
    {
        void setValue(IRunTimeValue value);

        IRunTimeValue getValue();
    }
}
