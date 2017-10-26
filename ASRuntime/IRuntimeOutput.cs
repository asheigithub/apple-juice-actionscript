using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	public interface IRuntimeOutput
	{
		void Info(string str);
		void Warring(string str);
		void Error(string str);

	}
}
