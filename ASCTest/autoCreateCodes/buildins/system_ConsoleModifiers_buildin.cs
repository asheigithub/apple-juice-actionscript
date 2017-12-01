using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_ConsoleModifiers_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_ConsoleModifiers_creator", default(System.ConsoleModifiers)));
			bin.regNativeFunction(new system_ConsoleModifiers_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleModifiers_Alt_getter",()=>{ return System.ConsoleModifiers.Alt;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleModifiers_Shift_getter",()=>{ return System.ConsoleModifiers.Shift;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleModifiers_Control_getter",()=>{ return System.ConsoleModifiers.Control;}));
			bin.regNativeFunction(new system_ConsoleModifiers_operator_bitOr());
		}

		class system_ConsoleModifiers_ctor : NativeFunctionBase
		{
			public system_ConsoleModifiers_ctor()
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
					return "system_ConsoleModifiers_ctor";
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

		class system_ConsoleModifiers_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_ConsoleModifiers_operator_bitOr() : base(2)
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
					return "system_ConsoleModifiers_operator_bitOr";
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
				System.ConsoleModifiers ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.ConsoleModifiers);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.ConsoleModifiers)argObj.value;
				}

				System.ConsoleModifiers ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.ConsoleModifiers);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.ConsoleModifiers)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
