using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_globalization_NumberStyles_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_globalization_NumberStyles_creator", default(System.Globalization.NumberStyles)));
			bin.regNativeFunction(new system_globalization_NumberStyles_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_None_getter",()=>{ return System.Globalization.NumberStyles.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowLeadingWhite_getter",()=>{ return System.Globalization.NumberStyles.AllowLeadingWhite;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowTrailingWhite_getter",()=>{ return System.Globalization.NumberStyles.AllowTrailingWhite;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowLeadingSign_getter",()=>{ return System.Globalization.NumberStyles.AllowLeadingSign;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowTrailingSign_getter",()=>{ return System.Globalization.NumberStyles.AllowTrailingSign;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowParentheses_getter",()=>{ return System.Globalization.NumberStyles.AllowParentheses;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowDecimalPoint_getter",()=>{ return System.Globalization.NumberStyles.AllowDecimalPoint;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowThousands_getter",()=>{ return System.Globalization.NumberStyles.AllowThousands;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowExponent_getter",()=>{ return System.Globalization.NumberStyles.AllowExponent;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowCurrencySymbol_getter",()=>{ return System.Globalization.NumberStyles.AllowCurrencySymbol;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_AllowHexSpecifier_getter",()=>{ return System.Globalization.NumberStyles.AllowHexSpecifier;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_Integer_getter",()=>{ return System.Globalization.NumberStyles.Integer;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_HexNumber_getter",()=>{ return System.Globalization.NumberStyles.HexNumber;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_Number_getter",()=>{ return System.Globalization.NumberStyles.Number;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_Float_getter",()=>{ return System.Globalization.NumberStyles.Float;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_Currency_getter",()=>{ return System.Globalization.NumberStyles.Currency;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_NumberStyles_Any_getter",()=>{ return System.Globalization.NumberStyles.Any;}));
			bin.regNativeFunction(new system_globalization_NumberStyles_operator_bitOr());
		}

		class system_globalization_NumberStyles_ctor : NativeFunctionBase
		{
			public system_globalization_NumberStyles_ctor()
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
					return "system_globalization_NumberStyles_ctor";
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

		class system_globalization_NumberStyles_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_globalization_NumberStyles_operator_bitOr() : base(2)
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
					return "system_globalization_NumberStyles_operator_bitOr";
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
				System.Globalization.NumberStyles ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Globalization.NumberStyles);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Globalization.NumberStyles)argObj.value;
				}

				System.Globalization.NumberStyles ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Globalization.NumberStyles);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Globalization.NumberStyles)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
