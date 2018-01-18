using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
	public interface IWapperDelegateMaker
	{
		FunctionWapper MakeWapper( ASBinCode.RunTimeValueBase function, Player player ,ASBinCode.rtti.Class cls);
	}
}
