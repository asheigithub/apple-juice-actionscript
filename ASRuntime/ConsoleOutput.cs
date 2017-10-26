using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	class ConsoleOutput : IRuntimeOutput
	{
		public void Error(string str)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(str);
			Console.ResetColor();
		}

		public void Info(string str)
		{
			Console.WriteLine(str);
		}

		public void Warring(string str)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(str);
			Console.ResetColor();
		}
	}
}
