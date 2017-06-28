using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_globalization_DateTimeStyles_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_globalization_DateTimeStyles_creator", default(System.Globalization.DateTimeStyles)));
			bin.regNativeFunction(new system_globalization_DateTimeStyles_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_None_getter",()=>{ return System.Globalization.DateTimeStyles.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_AllowLeadingWhite_getter",()=>{ return System.Globalization.DateTimeStyles.AllowLeadingWhite;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_AllowTrailingWhite_getter",()=>{ return System.Globalization.DateTimeStyles.AllowTrailingWhite;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_AllowInnerWhite_getter",()=>{ return System.Globalization.DateTimeStyles.AllowInnerWhite;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_AllowWhiteSpaces_getter",()=>{ return System.Globalization.DateTimeStyles.AllowWhiteSpaces;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_NoCurrentDateDefault_getter",()=>{ return System.Globalization.DateTimeStyles.NoCurrentDateDefault;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_AdjustToUniversal_getter",()=>{ return System.Globalization.DateTimeStyles.AdjustToUniversal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_AssumeLocal_getter",()=>{ return System.Globalization.DateTimeStyles.AssumeLocal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_AssumeUniversal_getter",()=>{ return System.Globalization.DateTimeStyles.AssumeUniversal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_DateTimeStyles_RoundtripKind_getter",()=>{ return System.Globalization.DateTimeStyles.RoundtripKind;}));
			bin.regNativeFunction(new system_globalization_DateTimeStyles_operator_bitOr());
		}

		class system_globalization_DateTimeStyles_ctor : NativeFunctionBase
		{
			public system_globalization_DateTimeStyles_ctor()
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
					return "system_globalization_DateTimeStyles_ctor";
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

		class system_globalization_DateTimeStyles_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_globalization_DateTimeStyles_operator_bitOr() : base(2)
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
					return "system_globalization_DateTimeStyles_operator_bitOr";
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
				System.Globalization.DateTimeStyles ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Globalization.DateTimeStyles);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Globalization.DateTimeStyles)argObj.value;
				}

				System.Globalization.DateTimeStyles ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Globalization.DateTimeStyles);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Globalization.DateTimeStyles)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
