using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_StringComparison_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_StringComparison_creator", default(System.StringComparison)));
			bin.regNativeFunction(new system_StringComparison_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_StringComparison_CurrentCulture_getter",()=>{ return System.StringComparison.CurrentCulture;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_StringComparison_CurrentCultureIgnoreCase_getter",()=>{ return System.StringComparison.CurrentCultureIgnoreCase;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_StringComparison_InvariantCulture_getter",()=>{ return System.StringComparison.InvariantCulture;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_StringComparison_InvariantCultureIgnoreCase_getter",()=>{ return System.StringComparison.InvariantCultureIgnoreCase;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_StringComparison_Ordinal_getter",()=>{ return System.StringComparison.Ordinal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_StringComparison_OrdinalIgnoreCase_getter",()=>{ return System.StringComparison.OrdinalIgnoreCase;}));
			bin.regNativeFunction(new system_StringComparison_operator_bitOr());
		}

		class system_StringComparison_ctor : NativeFunctionBase
		{
			public system_StringComparison_ctor()
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
					return "system_StringComparison_ctor";
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

		class system_StringComparison_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_StringComparison_operator_bitOr() : base(2)
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
					return "system_StringComparison_operator_bitOr";
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
				System.StringComparison ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.StringComparison);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.StringComparison)argObj.value;
				}

				System.StringComparison ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.StringComparison);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.StringComparison)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
