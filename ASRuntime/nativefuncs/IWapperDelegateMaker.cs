using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
	public interface IWapperDelegateMaker
	{
		Delegate MakeWapper( ASBinCode.RunTimeValueBase function, Player player ,ASBinCode.rtti.Class cls);
	}
}
