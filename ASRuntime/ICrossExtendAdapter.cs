using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	/// <summary>
	/// 标记这是一个可以跨脚本语言继承对象创建器
	/// </summary>
	public interface ICrossExtendAdapterCreator
	{
		Type GetAdapterType();
	}
	/// <summary>
	/// 标记这是一个可以跨脚本语言继承的对象
	/// </summary>
	public interface ICrossExtendAdapter
	{
		void SetAS3RuntimeEnvironment(Player player, ASBinCode.rtti.Class typeclass, ASBinCode.rtData.rtObjectBase bindAS3Object);

		ASBinCode.rtti.Class AS3Class { get; }
		ASBinCode.rtData.rtObjectBase AS3Object { get; }

	}
}
