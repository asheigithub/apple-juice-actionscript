using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_serialization_formatters_FormatterTypeStyle_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_serialization_formatters_FormatterTypeStyle_creator", default(System.Runtime.Serialization.Formatters.FormatterTypeStyle)));
			bin.regNativeFunction(new system_runtime_serialization_formatters_FormatterTypeStyle_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_serialization_formatters_FormatterTypeStyle_TypesWhenNeeded_getter",()=>{ return System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesWhenNeeded;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_serialization_formatters_FormatterTypeStyle_TypesAlways_getter",()=>{ return System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_serialization_formatters_FormatterTypeStyle_XsdString_getter",()=>{ return System.Runtime.Serialization.Formatters.FormatterTypeStyle.XsdString;}));
			bin.regNativeFunction(new system_runtime_serialization_formatters_FormatterTypeStyle_operator_bitOr());
		}

		class system_runtime_serialization_formatters_FormatterTypeStyle_ctor : NativeFunctionBase
		{
			public system_runtime_serialization_formatters_FormatterTypeStyle_ctor()
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
					return "system_runtime_serialization_formatters_FormatterTypeStyle_ctor";
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

		class system_runtime_serialization_formatters_FormatterTypeStyle_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_serialization_formatters_FormatterTypeStyle_operator_bitOr() : base(2)
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
					return "system_runtime_serialization_formatters_FormatterTypeStyle_operator_bitOr";
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
				System.Runtime.Serialization.Formatters.FormatterTypeStyle ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.Serialization.Formatters.FormatterTypeStyle);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.Serialization.Formatters.FormatterTypeStyle)argObj.value;
				}

				System.Runtime.Serialization.Formatters.FormatterTypeStyle ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.Serialization.Formatters.FormatterTypeStyle);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.Serialization.Formatters.FormatterTypeStyle)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
