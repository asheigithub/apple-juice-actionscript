using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_TypeLibTypeFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_TypeLibTypeFlags_creator", default(System.Runtime.InteropServices.TypeLibTypeFlags)));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibTypeFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FAppObject_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FAppObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FCanCreate_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FCanCreate;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FLicensed_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FLicensed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FPreDeclId_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FPreDeclId;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FHidden_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FHidden;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FControl_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FControl;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FDual_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FDual;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FNonExtensible_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FNonExtensible;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FOleAutomation_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FOleAutomation;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FRestricted_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FRestricted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FAggregatable_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FAggregatable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FReplaceable_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FReplaceable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FDispatchable_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FDispatchable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibTypeFlags_FReverseBind_getter",()=>{ return System.Runtime.InteropServices.TypeLibTypeFlags.FReverseBind;}));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibTypeFlags_operator_bitOr());
		}

		class system_runtime_interopservices_TypeLibTypeFlags_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_TypeLibTypeFlags_ctor()
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
					return "system_runtime_interopservices_TypeLibTypeFlags_ctor";
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

		class system_runtime_interopservices_TypeLibTypeFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_TypeLibTypeFlags_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_TypeLibTypeFlags_operator_bitOr";
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
				System.Runtime.InteropServices.TypeLibTypeFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.TypeLibTypeFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.TypeLibTypeFlags)argObj.value;
				}

				System.Runtime.InteropServices.TypeLibTypeFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.TypeLibTypeFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.TypeLibTypeFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
