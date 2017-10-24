using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
	[Serializable]
	/// <summary>
	/// 函数签名
	/// </summary>
	public class FunctionSignature
    {
        public List<FunctionParameter> parameters;
        public RunTimeDataType returnType;

        public int onStackParameters;

    }
}
