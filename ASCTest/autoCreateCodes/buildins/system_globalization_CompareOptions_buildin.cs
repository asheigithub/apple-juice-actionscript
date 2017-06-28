using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_globalization_CompareOptions_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_globalization_CompareOptions_creator", default(System.Globalization.CompareOptions)));
			bin.regNativeFunction(new system_globalization_CompareOptions_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_None_getter",()=>{ return System.Globalization.CompareOptions.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_IgnoreCase_getter",()=>{ return System.Globalization.CompareOptions.IgnoreCase;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_IgnoreNonSpace_getter",()=>{ return System.Globalization.CompareOptions.IgnoreNonSpace;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_IgnoreSymbols_getter",()=>{ return System.Globalization.CompareOptions.IgnoreSymbols;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_IgnoreKanaType_getter",()=>{ return System.Globalization.CompareOptions.IgnoreKanaType;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_IgnoreWidth_getter",()=>{ return System.Globalization.CompareOptions.IgnoreWidth;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_OrdinalIgnoreCase_getter",()=>{ return System.Globalization.CompareOptions.OrdinalIgnoreCase;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_StringSort_getter",()=>{ return System.Globalization.CompareOptions.StringSort;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_CompareOptions_Ordinal_getter",()=>{ return System.Globalization.CompareOptions.Ordinal;}));
			bin.regNativeFunction(new system_globalization_CompareOptions_operator_bitOr());
		}

		class system_globalization_CompareOptions_ctor : NativeFunctionBase
		{
			public system_globalization_CompareOptions_ctor()
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
					return "system_globalization_CompareOptions_ctor";
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

		class system_globalization_CompareOptions_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_globalization_CompareOptions_operator_bitOr() : base(2)
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
					return "system_globalization_CompareOptions_operator_bitOr";
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
				System.Globalization.CompareOptions ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Globalization.CompareOptions);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Globalization.CompareOptions)argObj.value;
				}

				System.Globalization.CompareOptions ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Globalization.CompareOptions);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Globalization.CompareOptions)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
