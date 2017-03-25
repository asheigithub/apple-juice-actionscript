using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASBinCode
{
    /// <summary>
    /// 输出的类库
    /// </summary>
    public class CSWC:IClassFinder
    {
        public List<CodeBlock> blocks = new List<CodeBlock>();
        public List<ASBinCode.rtti.FunctionDefine> functions = new List<ASBinCode.rtti.FunctionDefine>();

        public List<rtti.Class> classes = new List<rtti.Class>();

        public Class getClassByRunTimeDataType(RunTimeDataType rttype)
        {
            throw new NotImplementedException();
        }
    }
}
