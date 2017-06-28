using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_MethodAttributes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_MethodAttributes_creator", default(System.Reflection.MethodAttributes)));
			bin.regNativeFunction(new system_reflection_MethodAttributes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_MemberAccessMask_getter",()=>{ return System.Reflection.MethodAttributes.MemberAccessMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_PrivateScope_getter",()=>{ return System.Reflection.MethodAttributes.PrivateScope;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Private_getter",()=>{ return System.Reflection.MethodAttributes.Private;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_FamANDAssem_getter",()=>{ return System.Reflection.MethodAttributes.FamANDAssem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Assembly_getter",()=>{ return System.Reflection.MethodAttributes.Assembly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Family_getter",()=>{ return System.Reflection.MethodAttributes.Family;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_FamORAssem_getter",()=>{ return System.Reflection.MethodAttributes.FamORAssem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Public_getter",()=>{ return System.Reflection.MethodAttributes.Public;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Static_getter",()=>{ return System.Reflection.MethodAttributes.Static;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Final_getter",()=>{ return System.Reflection.MethodAttributes.Final;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Virtual_getter",()=>{ return System.Reflection.MethodAttributes.Virtual;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_HideBySig_getter",()=>{ return System.Reflection.MethodAttributes.HideBySig;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_CheckAccessOnOverride_getter",()=>{ return System.Reflection.MethodAttributes.CheckAccessOnOverride;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_VtableLayoutMask_getter",()=>{ return System.Reflection.MethodAttributes.VtableLayoutMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_ReuseSlot_getter",()=>{ return System.Reflection.MethodAttributes.ReuseSlot;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_NewSlot_getter",()=>{ return System.Reflection.MethodAttributes.NewSlot;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_Abstract_getter",()=>{ return System.Reflection.MethodAttributes.Abstract;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_SpecialName_getter",()=>{ return System.Reflection.MethodAttributes.SpecialName;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_PinvokeImpl_getter",()=>{ return System.Reflection.MethodAttributes.PinvokeImpl;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_UnmanagedExport_getter",()=>{ return System.Reflection.MethodAttributes.UnmanagedExport;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_RTSpecialName_getter",()=>{ return System.Reflection.MethodAttributes.RTSpecialName;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_ReservedMask_getter",()=>{ return System.Reflection.MethodAttributes.ReservedMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_HasSecurity_getter",()=>{ return System.Reflection.MethodAttributes.HasSecurity;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodAttributes_RequireSecObject_getter",()=>{ return System.Reflection.MethodAttributes.RequireSecObject;}));
			bin.regNativeFunction(new system_reflection_MethodAttributes_operator_bitOr());
		}

		class system_reflection_MethodAttributes_ctor : NativeFunctionBase
		{
			public system_reflection_MethodAttributes_ctor()
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
					return "system_reflection_MethodAttributes_ctor";
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

		class system_reflection_MethodAttributes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_MethodAttributes_operator_bitOr() : base(2)
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
					return "system_reflection_MethodAttributes_operator_bitOr";
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
				System.Reflection.MethodAttributes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.MethodAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Reflection.MethodAttributes)argObj.value;
				}

				System.Reflection.MethodAttributes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.MethodAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Reflection.MethodAttributes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
