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

        public bool isStatic;

        public bool isConstructor;

        public FunctionSignature signature;

        

        public int blockid;

        public readonly int functionid;

        /// <summary>
        /// 是否本地函数
        /// </summary>
        public bool isNative;
        public string native_name;
        public int native_index;


        public FunctionDefine(int id)
        {
            functionid = id;
            native_index = -1;

        }
    }
}
