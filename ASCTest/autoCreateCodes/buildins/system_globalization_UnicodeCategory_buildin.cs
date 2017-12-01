using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_globalization_UnicodeCategory_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_globalization_UnicodeCategory_creator", default(System.Globalization.UnicodeCategory)));
			bin.regNativeFunction(new system_globalization_UnicodeCategory_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_UppercaseLetter_getter",()=>{ return System.Globalization.UnicodeCategory.UppercaseLetter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_LowercaseLetter_getter",()=>{ return System.Globalization.UnicodeCategory.LowercaseLetter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_TitlecaseLetter_getter",()=>{ return System.Globalization.UnicodeCategory.TitlecaseLetter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_ModifierLetter_getter",()=>{ return System.Globalization.UnicodeCategory.ModifierLetter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_OtherLetter_getter",()=>{ return System.Globalization.UnicodeCategory.OtherLetter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_NonSpacingMark_getter",()=>{ return System.Globalization.UnicodeCategory.NonSpacingMark;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_SpacingCombiningMark_getter",()=>{ return System.Globalization.UnicodeCategory.SpacingCombiningMark;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_EnclosingMark_getter",()=>{ return System.Globalization.UnicodeCategory.EnclosingMark;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_DecimalDigitNumber_getter",()=>{ return System.Globalization.UnicodeCategory.DecimalDigitNumber;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_LetterNumber_getter",()=>{ return System.Globalization.UnicodeCategory.LetterNumber;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_OtherNumber_getter",()=>{ return System.Globalization.UnicodeCategory.OtherNumber;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_SpaceSeparator_getter",()=>{ return System.Globalization.UnicodeCategory.SpaceSeparator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_LineSeparator_getter",()=>{ return System.Globalization.UnicodeCategory.LineSeparator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_ParagraphSeparator_getter",()=>{ return System.Globalization.UnicodeCategory.ParagraphSeparator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_Control_getter",()=>{ return System.Globalization.UnicodeCategory.Control;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_Format_getter",()=>{ return System.Globalization.UnicodeCategory.Format;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_Surrogate_getter",()=>{ return System.Globalization.UnicodeCategory.Surrogate;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_PrivateUse_getter",()=>{ return System.Globalization.UnicodeCategory.PrivateUse;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_ConnectorPunctuation_getter",()=>{ return System.Globalization.UnicodeCategory.ConnectorPunctuation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_DashPunctuation_getter",()=>{ return System.Globalization.UnicodeCategory.DashPunctuation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_OpenPunctuation_getter",()=>{ return System.Globalization.UnicodeCategory.OpenPunctuation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_ClosePunctuation_getter",()=>{ return System.Globalization.UnicodeCategory.ClosePunctuation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_InitialQuotePunctuation_getter",()=>{ return System.Globalization.UnicodeCategory.InitialQuotePunctuation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_FinalQuotePunctuation_getter",()=>{ return System.Globalization.UnicodeCategory.FinalQuotePunctuation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_OtherPunctuation_getter",()=>{ return System.Globalization.UnicodeCategory.OtherPunctuation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_MathSymbol_getter",()=>{ return System.Globalization.UnicodeCategory.MathSymbol;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_CurrencySymbol_getter",()=>{ return System.Globalization.UnicodeCategory.CurrencySymbol;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_ModifierSymbol_getter",()=>{ return System.Globalization.UnicodeCategory.ModifierSymbol;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_OtherSymbol_getter",()=>{ return System.Globalization.UnicodeCategory.OtherSymbol;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_globalization_UnicodeCategory_OtherNotAssigned_getter",()=>{ return System.Globalization.UnicodeCategory.OtherNotAssigned;}));
			bin.regNativeFunction(new system_globalization_UnicodeCategory_operator_bitOr());
		}

		class system_globalization_UnicodeCategory_ctor : NativeFunctionBase
		{
			public system_globalization_UnicodeCategory_ctor()
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
					return "system_globalization_UnicodeCategory_ctor";
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

		class system_globalization_UnicodeCategory_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_globalization_UnicodeCategory_operator_bitOr() : base(2)
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
					return "system_globalization_UnicodeCategory_operator_bitOr";
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
				System.Globalization.UnicodeCategory ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Globalization.UnicodeCategory);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Globalization.UnicodeCategory)argObj.value;
				}

				System.Globalization.UnicodeCategory ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Globalization.UnicodeCategory);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Globalization.UnicodeCategory)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
