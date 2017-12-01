using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_MethodImplAttributes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_MethodImplAttributes_creator", default(System.Reflection.MethodImplAttributes)));
			bin.regNativeFunction(new system_reflection_MethodImplAttributes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_CodeTypeMask_getter",()=>{ return System.Reflection.MethodImplAttributes.CodeTypeMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_IL_getter",()=>{ return System.Reflection.MethodImplAttributes.IL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_Native_getter",()=>{ return System.Reflection.MethodImplAttributes.Native;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_OPTIL_getter",()=>{ return System.Reflection.MethodImplAttributes.OPTIL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_Runtime_getter",()=>{ return System.Reflection.MethodImplAttributes.Runtime;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_ManagedMask_getter",()=>{ return System.Reflection.MethodImplAttributes.ManagedMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_Unmanaged_getter",()=>{ return System.Reflection.MethodImplAttributes.Unmanaged;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_Managed_getter",()=>{ return System.Reflection.MethodImplAttributes.Managed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_ForwardRef_getter",()=>{ return System.Reflection.MethodImplAttributes.ForwardRef;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_PreserveSig_getter",()=>{ return System.Reflection.MethodImplAttributes.PreserveSig;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_InternalCall_getter",()=>{ return System.Reflection.MethodImplAttributes.InternalCall;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_Synchronized_getter",()=>{ return System.Reflection.MethodImplAttributes.Synchronized;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_NoInlining_getter",()=>{ return System.Reflection.MethodImplAttributes.NoInlining;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_NoOptimization_getter",()=>{ return System.Reflection.MethodImplAttributes.NoOptimization;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MethodImplAttributes_MaxMethodImplVal_getter",()=>{ return System.Reflection.MethodImplAttributes.MaxMethodImplVal;}));
			bin.regNativeFunction(new system_reflection_MethodImplAttributes_operator_bitOr());
		}

		class system_reflection_MethodImplAttributes_ctor : NativeFunctionBase
		{
			public system_reflection_MethodImplAttributes_ctor()
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
					return "system_reflection_MethodImplAttributes_ctor";
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

		class system_reflection_MethodImplAttributes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_MethodImplAttributes_operator_bitOr() : base(2)
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
					return "system_reflection_MethodImplAttributes_operator_bitOr";
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
				System.Reflection.MethodImplAttributes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.MethodImplAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Reflection.MethodImplAttributes)argObj.value;
				}

				System.Reflection.MethodImplAttributes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.MethodImplAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Reflection.MethodImplAttributes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
