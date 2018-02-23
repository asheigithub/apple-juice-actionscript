using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler
{
	public class BuildOptions
	{
		/// <summary>
		/// 是否检查本地函数签名
		/// </summary>
		public bool CheckNativeFunctionSignature;

		public bool isConsoleOut;

		public BuildOptions()
		{
			CheckNativeFunctionSignature = true;

			isConsoleOut = true;

		}
	}
}
