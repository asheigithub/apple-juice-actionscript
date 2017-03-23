using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    /// <summary>
    /// 函数定义
    /// </summary>
    public class FunctionDefine
    {
        //public List<FunctionParameter> parameters;

        public bool IsAnonymous;

        public string name;

        public bool isMethod;
        ///// <summary>
        ///// 如果是method,绑定于哪个类。
        ///// </summary>
        //public Class bindClass;

        public bool isConstructor;

        public FunctionSignature signature;

        

        public int blockid;

        public readonly int functionid;

        public FunctionDefine(int id)
        {
            functionid = id;
        }
    }
}
