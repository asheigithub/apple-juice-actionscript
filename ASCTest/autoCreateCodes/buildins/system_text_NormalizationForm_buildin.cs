using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_text_NormalizationForm_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_text_NormalizationForm_creator", default(System.Text.NormalizationForm)));
			bin.regNativeFunction(new system_text_NormalizationForm_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_text_NormalizationForm_FormC_getter",()=>{ return System.Text.NormalizationForm.FormC;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_text_NormalizationForm_FormD_getter",()=>{ return System.Text.NormalizationForm.FormD;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_text_NormalizationForm_FormKC_getter",()=>{ return System.Text.NormalizationForm.FormKC;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_text_NormalizationForm_FormKD_getter",()=>{ return System.Text.NormalizationForm.FormKD;}));
			bin.regNativeFunction(new system_text_NormalizationForm_operator_bitOr());
		}

		class system_text_NormalizationForm_ctor : NativeFunctionBase
		{
			public system_text_NormalizationForm_ctor()
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
					return "system_text_NormalizationForm_ctor";
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

		class system_text_NormalizationForm_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_text_NormalizationForm_operator_bitOr() : base(2)
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
					return "system_text_NormalizationForm_operator_bitOr";
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
				System.Text.NormalizationForm ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Text.NormalizationForm);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Text.NormalizationForm)argObj.value;
				}

				System.Text.NormalizationForm ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Text.NormalizationForm);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Text.NormalizationForm)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
