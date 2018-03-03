using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	class ConsoleOutput : IRuntimeOutput
	{
		public void Error(string str)
		{
#if UNITY_5_3_OR_NEWER
			UnityEngine.Debug.LogError(str);
			
#else
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(str);
			Console.ResetColor();

#endif

		}

		public void Info(string str)
		{
#if UNITY_5_3_OR_NEWER
			UnityEngine.Debug.Log(str);
#else
			Console.WriteLine(str);
#endif
		}

		public void Warring(string str)
		{
#if UNITY_5_3_OR_NEWER
			UnityEngine.Debug.LogWarning(str);
#else
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(str);
			Console.ResetColor();
#endif
		}
	}
}
