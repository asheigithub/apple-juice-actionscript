using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_AttributeTargets_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_AttributeTargets_creator", default(System.AttributeTargets)));
			bin.regNativeFunction(new system_AttributeTargets_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Assembly_getter",()=>{ return System.AttributeTargets.Assembly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Module_getter",()=>{ return System.AttributeTargets.Module;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Class_getter",()=>{ return System.AttributeTargets.Class;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Struct_getter",()=>{ return System.AttributeTargets.Struct;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Enum_getter",()=>{ return System.AttributeTargets.Enum;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Constructor_getter",()=>{ return System.AttributeTargets.Constructor;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Method_getter",()=>{ return System.AttributeTargets.Method;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Property_getter",()=>{ return System.AttributeTargets.Property;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Field_getter",()=>{ return System.AttributeTargets.Field;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Event_getter",()=>{ return System.AttributeTargets.Event;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Interface_getter",()=>{ return System.AttributeTargets.Interface;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Parameter_getter",()=>{ return System.AttributeTargets.Parameter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_Delegate_getter",()=>{ return System.AttributeTargets.Delegate;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_ReturnValue_getter",()=>{ return System.AttributeTargets.ReturnValue;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_GenericParameter_getter",()=>{ return System.AttributeTargets.GenericParameter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_AttributeTargets_All_getter",()=>{ return System.AttributeTargets.All;}));
			bin.regNativeFunction(new system_AttributeTargets_operator_bitOr());
		}

		class system_AttributeTargets_ctor : NativeFunctionBase
		{
			public system_AttributeTargets_ctor()
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
					return "system_AttributeTargets_ctor";
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

		class system_AttributeTargets_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_AttributeTargets_operator_bitOr() : base(2)
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
					return "system_AttributeTargets_operator_bitOr";
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
				System.AttributeTargets ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.AttributeTargets);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.AttributeTargets)argObj.value;
				}

				System.AttributeTargets ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.AttributeTargets);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.AttributeTargets)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
