using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_FieldAttributes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_FieldAttributes_creator", default(System.Reflection.FieldAttributes)));
			bin.regNativeFunction(new system_reflection_FieldAttributes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_FieldAccessMask_getter",()=>{ return System.Reflection.FieldAttributes.FieldAccessMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_PrivateScope_getter",()=>{ return System.Reflection.FieldAttributes.PrivateScope;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_Private_getter",()=>{ return System.Reflection.FieldAttributes.Private;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_FamANDAssem_getter",()=>{ return System.Reflection.FieldAttributes.FamANDAssem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_Assembly_getter",()=>{ return System.Reflection.FieldAttributes.Assembly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_Family_getter",()=>{ return System.Reflection.FieldAttributes.Family;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_FamORAssem_getter",()=>{ return System.Reflection.FieldAttributes.FamORAssem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_Public_getter",()=>{ return System.Reflection.FieldAttributes.Public;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_Static_getter",()=>{ return System.Reflection.FieldAttributes.Static;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_InitOnly_getter",()=>{ return System.Reflection.FieldAttributes.InitOnly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_Literal_getter",()=>{ return System.Reflection.FieldAttributes.Literal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_NotSerialized_getter",()=>{ return System.Reflection.FieldAttributes.NotSerialized;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_SpecialName_getter",()=>{ return System.Reflection.FieldAttributes.SpecialName;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_PinvokeImpl_getter",()=>{ return System.Reflection.FieldAttributes.PinvokeImpl;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_ReservedMask_getter",()=>{ return System.Reflection.FieldAttributes.ReservedMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_RTSpecialName_getter",()=>{ return System.Reflection.FieldAttributes.RTSpecialName;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_HasFieldMarshal_getter",()=>{ return System.Reflection.FieldAttributes.HasFieldMarshal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_HasDefault_getter",()=>{ return System.Reflection.FieldAttributes.HasDefault;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_FieldAttributes_HasFieldRVA_getter",()=>{ return System.Reflection.FieldAttributes.HasFieldRVA;}));
			bin.regNativeFunction(new system_reflection_FieldAttributes_operator_bitOr());
		}

		class system_reflection_FieldAttributes_ctor : NativeFunctionBase
		{
			public system_reflection_FieldAttributes_ctor()
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
					return "system_reflection_FieldAttributes_ctor";
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

		class system_reflection_FieldAttributes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_FieldAttributes_operator_bitOr() : base(2)
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
					return "system_reflection_FieldAttributes_operator_bitOr";
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
				System.Reflection.FieldAttributes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.FieldAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Reflection.FieldAttributes)argObj.value;
				}

				System.Reflection.FieldAttributes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.FieldAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Reflection.FieldAttributes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
