using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;
using ASRuntime;

namespace ASRuntime.nativefuncs
{
	/// <summary>
	/// 给没有公共构造函数的链接类准备一个通用构造函数
	/// </summary>
	class system_noctorclass_buildin : ASRuntime.nativefuncs.NativeConstParameterFunction
	{
		public system_noctorclass_buildin() : base(0)
		{
			para = new List<RunTimeDataType>();
		}


		public override string name { get { return "$$_noctorclass"; } }

		List<RunTimeDataType> para;
		public override List<RunTimeDataType> parameters
		{
			get
			{
				return para;
			}
		}

		public override RunTimeDataType returnType { get { return RunTimeDataType.rt_void; } }

		public override bool isMethod { get { return true; } }

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;
		}
	}
}
