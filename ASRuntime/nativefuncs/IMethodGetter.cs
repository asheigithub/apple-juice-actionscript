using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
	/// <summary>
	/// 指定可以获取链接到的MethodInfo
	/// </summary>
	public interface IMethodGetter
	{
		System.Reflection.MethodInfo GetMethodInfo(ASBinCode.rtti.FunctionDefine functionDefine,ASBinCode.CSWC swc,ASRuntime.Player player);
	}
}
