using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_ParameterAttributes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_ParameterAttributes_creator", default(System.Reflection.ParameterAttributes)));
			bin.regNativeFunction(new system_reflection_ParameterAttributes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_None_getter",()=>{ return System.Reflection.ParameterAttributes.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_In_getter",()=>{ return System.Reflection.ParameterAttributes.In;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_Out_getter",()=>{ return System.Reflection.ParameterAttributes.Out;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_Lcid_getter",()=>{ return System.Reflection.ParameterAttributes.Lcid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_Retval_getter",()=>{ return System.Reflection.ParameterAttributes.Retval;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_Optional_getter",()=>{ return System.Reflection.ParameterAttributes.Optional;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_ReservedMask_getter",()=>{ return System.Reflection.ParameterAttributes.ReservedMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_HasDefault_getter",()=>{ return System.Reflection.ParameterAttributes.HasDefault;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_HasFieldMarshal_getter",()=>{ return System.Reflection.ParameterAttributes.HasFieldMarshal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_Reserved3_getter",()=>{ return System.Reflection.ParameterAttributes.Reserved3;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ParameterAttributes_Reserved4_getter",()=>{ return System.Reflection.ParameterAttributes.Reserved4;}));
			bin.regNativeFunction(new system_reflection_ParameterAttributes_operator_bitOr());
		}

		class system_reflection_ParameterAttributes_ctor : NativeFunctionBase
		{
			public system_reflection_ParameterAttributes_ctor()
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
					return "system_reflection_ParameterAttributes_ctor";
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

		class system_reflection_ParameterAttributes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_ParameterAttributes_operator_bitOr() : base(2)
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
					return "system_reflection_ParameterAttributes_operator_bitOr";
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
				System.Reflection.ParameterAttributes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.ParameterAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Reflection.ParameterAttributes)argObj.value;
				}

				System.Reflection.ParameterAttributes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.ParameterAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Reflection.ParameterAttributes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
