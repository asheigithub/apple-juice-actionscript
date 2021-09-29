using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	/// <summary>
	/// 引擎本身执行异常，必须抛出
	/// </summary>
	public class EngineException : Exception
	{
		public EngineException()
		{
		}

		public EngineException(string message) : base(message)
		{

		}
	}
}
