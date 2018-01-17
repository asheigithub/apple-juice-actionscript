using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;

namespace ASCTest.autoCreateCodes
{
	class FuncTestbuildin : NativeConstParameterFunction
	{
		public FuncTestbuildin():base(1)
		{
			paras = new List<RunTimeDataType>();
			paras.Add(RunTimeDataType.rt_function);
		}

		public override string name => "autogencodelib_Testobj_FuncTest";

		List<RunTimeDataType> paras;

		public override List<RunTimeDataType> parameters => paras;

		public override RunTimeDataType returnType => RunTimeDataType.fun_void;

		public override bool isMethod => true;

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//throw new NotImplementedException();

			FunctionWapper wapper = new FunctionWapper(argements[0], stackframe.player);

			object r= wapper.Invoke(null);


			success = true;

		}
	}
}
