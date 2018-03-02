using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
	public interface INativeFunctionFactory
	{
		NativeFunctionBase Create(string typename);
	}
}
