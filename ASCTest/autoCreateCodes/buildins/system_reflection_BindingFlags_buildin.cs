using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_BindingFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_BindingFlags_creator", default(System.Reflection.BindingFlags)));
			bin.regNativeFunction(new system_reflection_BindingFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_Default_getter",()=>{ return System.Reflection.BindingFlags.Default;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_IgnoreCase_getter",()=>{ return System.Reflection.BindingFlags.IgnoreCase;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_DeclaredOnly_getter",()=>{ return System.Reflection.BindingFlags.DeclaredOnly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_Instance_getter",()=>{ return System.Reflection.BindingFlags.Instance;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_Static_getter",()=>{ return System.Reflection.BindingFlags.Static;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_Public_getter",()=>{ return System.Reflection.BindingFlags.Public;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_NonPublic_getter",()=>{ return System.Reflection.BindingFlags.NonPublic;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_FlattenHierarchy_getter",()=>{ return System.Reflection.BindingFlags.FlattenHierarchy;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_InvokeMethod_getter",()=>{ return System.Reflection.BindingFlags.InvokeMethod;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_CreateInstance_getter",()=>{ return System.Reflection.BindingFlags.CreateInstance;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_GetField_getter",()=>{ return System.Reflection.BindingFlags.GetField;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_SetField_getter",()=>{ return System.Reflection.BindingFlags.SetField;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_GetProperty_getter",()=>{ return System.Reflection.BindingFlags.GetProperty;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_SetProperty_getter",()=>{ return System.Reflection.BindingFlags.SetProperty;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_PutDispProperty_getter",()=>{ return System.Reflection.BindingFlags.PutDispProperty;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_PutRefDispProperty_getter",()=>{ return System.Reflection.BindingFlags.PutRefDispProperty;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_ExactBinding_getter",()=>{ return System.Reflection.BindingFlags.ExactBinding;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_SuppressChangeType_getter",()=>{ return System.Reflection.BindingFlags.SuppressChangeType;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_OptionalParamBinding_getter",()=>{ return System.Reflection.BindingFlags.OptionalParamBinding;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_BindingFlags_IgnoreReturn_getter",()=>{ return System.Reflection.BindingFlags.IgnoreReturn;}));
			bin.regNativeFunction(new system_reflection_BindingFlags_operator_bitOr());
		}

		class system_reflection_BindingFlags_ctor : NativeFunctionBase
		{
			public system_reflection_BindingFlags_ctor()
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
					return "system_reflection_BindingFlags_ctor";
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

		class system_reflection_BindingFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_BindingFlags_operator_bitOr() : base(2)
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
					return "system_reflection_BindingFlags_operator_bitOr";
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
				System.Reflection.BindingFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.BindingFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Reflection.BindingFlags)argObj.value;
				}

				System.Reflection.BindingFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.BindingFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Reflection.BindingFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
