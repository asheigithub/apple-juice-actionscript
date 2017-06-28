using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_emit_OperandType_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_emit_OperandType_creator", default(System.Reflection.Emit.OperandType)));
			bin.regNativeFunction(new system_reflection_emit_OperandType_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineBrTarget_getter",()=>{ return System.Reflection.Emit.OperandType.InlineBrTarget;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineField_getter",()=>{ return System.Reflection.Emit.OperandType.InlineField;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineI_getter",()=>{ return System.Reflection.Emit.OperandType.InlineI;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineI8_getter",()=>{ return System.Reflection.Emit.OperandType.InlineI8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineMethod_getter",()=>{ return System.Reflection.Emit.OperandType.InlineMethod;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineNone_getter",()=>{ return System.Reflection.Emit.OperandType.InlineNone;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlinePhi_getter",()=>{ return System.Reflection.Emit.OperandType.InlinePhi;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineR_getter",()=>{ return System.Reflection.Emit.OperandType.InlineR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineSig_getter",()=>{ return System.Reflection.Emit.OperandType.InlineSig;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineString_getter",()=>{ return System.Reflection.Emit.OperandType.InlineString;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineSwitch_getter",()=>{ return System.Reflection.Emit.OperandType.InlineSwitch;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineTok_getter",()=>{ return System.Reflection.Emit.OperandType.InlineTok;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineType_getter",()=>{ return System.Reflection.Emit.OperandType.InlineType;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_InlineVar_getter",()=>{ return System.Reflection.Emit.OperandType.InlineVar;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_ShortInlineBrTarget_getter",()=>{ return System.Reflection.Emit.OperandType.ShortInlineBrTarget;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_ShortInlineI_getter",()=>{ return System.Reflection.Emit.OperandType.ShortInlineI;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_ShortInlineR_getter",()=>{ return System.Reflection.Emit.OperandType.ShortInlineR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_OperandType_ShortInlineVar_getter",()=>{ return System.Reflection.Emit.OperandType.ShortInlineVar;}));
			bin.regNativeFunction(new system_reflection_emit_OperandType_operator_bitOr());
		}

		class system_reflection_emit_OperandType_ctor : NativeFunctionBase
		{
			public system_reflection_emit_OperandType_ctor()
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
					return "system_reflection_emit_OperandType_ctor";
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

		class system_reflection_emit_OperandType_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_emit_OperandType_operator_bitOr() : base(2)
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
					return "system_reflection_emit_OperandType_operator_bitOr";
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
				System.Reflection.Emit.OperandType ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.Emit.OperandType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Reflection.Emit.OperandType)argObj.value;
				}

				System.Reflection.Emit.OperandType ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.Emit.OperandType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Reflection.Emit.OperandType)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
