using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 输出的类库
    /// </summary>
    public class CSWC
    {
        public List<CodeBlock> blocks = new List<CodeBlock>();
        public List<ASBinCode.rtti.FunctionDefine> functions = new List<ASBinCode.rtti.FunctionDefine>();

        public List<rtti.Class> classes = new List<rtti.Class>();

    }
}
