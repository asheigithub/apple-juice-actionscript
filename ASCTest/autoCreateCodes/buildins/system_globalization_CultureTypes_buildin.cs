using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_globalization_CultureTypes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_globalization_CultureTypes_creator", default(System.Globalization.CultureTypes)));
			bin.regNativeFunction(new system_globalization_CultureTypes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_NeutralCultures_getter",()=>{ return System.Globalization.CultureTypes.NeutralCultures;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_SpecificCultures_getter",()=>{ return System.Globalization.CultureTypes.SpecificCultures;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_InstalledWin32Cultures_getter",()=>{ return System.Globalization.CultureTypes.InstalledWin32Cultures;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_AllCultures_getter",()=>{ return System.Globalization.CultureTypes.AllCultures;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_UserCustomCulture_getter",()=>{ return System.Globalization.CultureTypes.UserCustomCulture;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_ReplacementCultures_getter",()=>{ return System.Globalization.CultureTypes.ReplacementCultures;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_WindowsOnlyCultures_getter",()=>{ return System.Globalization.CultureTypes.WindowsOnlyCultures;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CultureTypes_FrameworkCultures_getter",()=>{ return System.Globalization.CultureTypes.FrameworkCultures;}));
			bin.regNativeFunction(new system_globalization_CultureTypes_operator_bitOr());
		}

		class system_globalization_CultureTypes_ctor : NativeFunctionBase
		{
			public system_globalization_CultureTypes_ctor()
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
					return "system_globalization_CultureTypes_ctor";
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

		class system_globalization_CultureTypes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_globalization_CultureTypes_operator_bitOr() : base(2)
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
					return "system_globalization_CultureTypes_operator_bitOr";
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
				System.Globalization.CultureTypes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Globalization.CultureTypes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Globalization.CultureTypes)argObj.value;
				}

				System.Globalization.CultureTypes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Globalization.CultureTypes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Globalization.CultureTypes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
