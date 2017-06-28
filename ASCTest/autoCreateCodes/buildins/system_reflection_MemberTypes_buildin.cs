using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_MemberTypes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_MemberTypes_creator", default(System.Reflection.MemberTypes)));
			bin.regNativeFunction(new system_reflection_MemberTypes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_Constructor_getter",()=>{ return System.Reflection.MemberTypes.Constructor;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_Event_getter",()=>{ return System.Reflection.MemberTypes.Event;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_Field_getter",()=>{ return System.Reflection.MemberTypes.Field;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_Method_getter",()=>{ return System.Reflection.MemberTypes.Method;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_Property_getter",()=>{ return System.Reflection.MemberTypes.Property;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_TypeInfo_getter",()=>{ return System.Reflection.MemberTypes.TypeInfo;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_Custom_getter",()=>{ return System.Reflection.MemberTypes.Custom;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_NestedType_getter",()=>{ return System.Reflection.MemberTypes.NestedType;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_MemberTypes_All_getter",()=>{ return System.Reflection.MemberTypes.All;}));
			bin.regNativeFunction(new system_reflection_MemberTypes_operator_bitOr());
		}

		class system_reflection_MemberTypes_ctor : NativeFunctionBase
		{
			public system_reflection_MemberTypes_ctor()
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
					return "system_reflection_MemberTypes_ctor";
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

		class system_reflection_MemberTypes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_MemberTypes_operator_bitOr() : base(2)
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
					return "system_reflection_MemberTypes_operator_bitOr";
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
				System.Reflection.MemberTypes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.MemberTypes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Reflection.MemberTypes)argObj.value;
				}

				System.Reflection.MemberTypes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.MemberTypes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Reflection.MemberTypes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
