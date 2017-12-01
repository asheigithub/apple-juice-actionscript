using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_RegistrationClassContext_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_RegistrationClassContext_creator", default(System.Runtime.InteropServices.RegistrationClassContext)));
			bin.regNativeFunction(new system_runtime_interopservices_RegistrationClassContext_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_InProcessServer_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.InProcessServer;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_InProcessHandler_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.InProcessHandler;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_LocalServer_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.LocalServer;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_InProcessServer16_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.InProcessServer16;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_RemoteServer_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.RemoteServer;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_InProcessHandler16_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.InProcessHandler16;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_Reserved1_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.Reserved1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_Reserved2_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.Reserved2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_Reserved3_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.Reserved3;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_Reserved4_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.Reserved4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_NoCodeDownload_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.NoCodeDownload;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_Reserved5_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.Reserved5;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_NoCustomMarshal_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.NoCustomMarshal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_EnableCodeDownload_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.EnableCodeDownload;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_NoFailureLog_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.NoFailureLog;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_DisableActivateAsActivator_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.DisableActivateAsActivator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_EnableActivateAsActivator_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.EnableActivateAsActivator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_RegistrationClassContext_FromDefaultContext_getter",()=>{ return System.Runtime.InteropServices.RegistrationClassContext.FromDefaultContext;}));
			bin.regNativeFunction(new system_runtime_interopservices_RegistrationClassContext_operator_bitOr());
		}

		class system_runtime_interopservices_RegistrationClassContext_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_RegistrationClassContext_ctor()
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
					return "system_runtime_interopservices_RegistrationClassContext_ctor";
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

		class system_runtime_interopservices_RegistrationClassContext_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_RegistrationClassContext_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_RegistrationClassContext_operator_bitOr";
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
				System.Runtime.InteropServices.RegistrationClassContext ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.RegistrationClassContext);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.RegistrationClassContext)argObj.value;
				}

				System.Runtime.InteropServices.RegistrationClassContext ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.RegistrationClassContext);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.RegistrationClassContext)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
