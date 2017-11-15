using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 定义编译期内存数据
    /// </summary>
    public interface IScope
    {
        
        List<IMember> members { get; }

        IScope parentScope { get; }
    }
}
