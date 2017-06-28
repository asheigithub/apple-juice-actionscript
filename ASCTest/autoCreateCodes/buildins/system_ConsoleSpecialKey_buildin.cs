using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_ConsoleSpecialKey_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_ConsoleSpecialKey_creator", default(System.ConsoleSpecialKey)));
			bin.regNativeFunction(new system_ConsoleSpecialKey_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleSpecialKey_ControlC_getter",()=>{ return System.ConsoleSpecialKey.ControlC;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleSpecialKey_ControlBreak_getter",()=>{ return System.ConsoleSpecialKey.ControlBreak;}));
			bin.regNativeFunction(new system_ConsoleSpecialKey_operator_bitOr());
		}

		class system_ConsoleSpecialKey_ctor : NativeFunctionBase
		{
			public system_ConsoleSpecialKey_ctor()
			{
				para = new List<RunTimeDataType>();
			}

			public override bool isMethod
			{
				get
				{
					return true;
				}
			}

			public override string name
			{
				get
				{
					return "system_ConsoleSpecialKey_ctor";
				}
			}

			List<RunTimeDataType> para;
			public override List<RunTimeDataType> parameters
			{
				get
				{
					return para;
				}
			}

			public override RunTimeDataType returnType
			{
				get
				{
					return RunTimeDataType.rt_void;
				}
			}

			public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
			{
				errormessage = null; errorno = 0;
				return ASBinCode.rtData.rtUndefined.undefined;

			}
		}

		class system_ConsoleSpecialKey_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_ConsoleSpecialKey_operator_bitOr() : base(2)
			{
				para = new List<RunTimeDataType>();
				para.Add(RunTimeDataType.rt_void);
				para.Add(RunTimeDataType.rt_void);
			}

			public override bool isMethod
			{
				get
				{
					return true;
				}
			}

			public override string name
			{
				get
				{
					return "system_ConsoleSpecialKey_operator_bitOr";
				}
			}

			List<RunTimeDataType> para;
			public override List<RunTimeDataType> parameters
			{
				get
				{
					return para;
				}
			}

			public override RunTimeDataType returnType
			{
				get
				{
					return RunTimeDataType.rt_void;
				}
			}

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{
				System.ConsoleSpecialKey ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.ConsoleSpecialKey);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.ConsoleSpecialKey)argObj.value;
				}

				System.ConsoleSpecialKey ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.ConsoleSpecialKey);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.ConsoleSpecialKey)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
