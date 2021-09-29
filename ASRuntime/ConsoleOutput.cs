using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	class ConsoleOutput : IRuntimeOutput
	{
		public void Error(string str)
		{
#if !WHENDEV
			UnityEngine.Debug.LogError(str);
			
#else
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(str);
			Console.ResetColor();

#endif

		}

		public void Info(string str)
		{
#if !WHENDEV
			UnityEngine.Debug.Log(str);
#else
			Console.WriteLine(str);
#endif
		}

		public void Warring(string str)
		{
#if !WHENDEV
			UnityEngine.Debug.LogWarning(str);
#else
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(str);
			Console.ResetColor();
#endif
		}
	}
}
