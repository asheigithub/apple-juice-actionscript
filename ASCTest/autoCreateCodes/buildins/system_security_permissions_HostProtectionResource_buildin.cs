using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_permissions_HostProtectionResource_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_permissions_HostProtectionResource_creator", default(System.Security.Permissions.HostProtectionResource)));
			bin.regNativeFunction(new system_security_permissions_HostProtectionResource_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_None_getter",()=>{ return System.Security.Permissions.HostProtectionResource.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_Synchronization_getter",()=>{ return System.Security.Permissions.HostProtectionResource.Synchronization;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_SharedState_getter",()=>{ return System.Security.Permissions.HostProtectionResource.SharedState;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_ExternalProcessMgmt_getter",()=>{ return System.Security.Permissions.HostProtectionResource.ExternalProcessMgmt;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_SelfAffectingProcessMgmt_getter",()=>{ return System.Security.Permissions.HostProtectionResource.SelfAffectingProcessMgmt;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_ExternalThreading_getter",()=>{ return System.Security.Permissions.HostProtectionResource.ExternalThreading;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_SelfAffectingThreading_getter",()=>{ return System.Security.Permissions.HostProtectionResource.SelfAffectingThreading;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_SecurityInfrastructure_getter",()=>{ return System.Security.Permissions.HostProtectionResource.SecurityInfrastructure;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_UI_getter",()=>{ return System.Security.Permissions.HostProtectionResource.UI;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_MayLeakOnAbort_getter",()=>{ return System.Security.Permissions.HostProtectionResource.MayLeakOnAbort;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_HostProtectionResource_All_getter",()=>{ return System.Security.Permissions.HostProtectionResource.All;}));
			bin.regNativeFunction(new system_security_permissions_HostProtectionResource_operator_bitOr());
		}

		class system_security_permissions_HostProtectionResource_ctor : NativeFunctionBase
		{
			public system_security_permissions_HostProtectionResource_ctor()
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
					return "system_security_permissions_HostProtectionResource_ctor";
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

		class system_security_permissions_HostProtectionResource_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_permissions_HostProtectionResource_operator_bitOr() : base(2)
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
					return "system_security_permissions_HostProtectionResource_operator_bitOr";
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
				System.Security.Permissions.HostProtectionResource ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Permissions.HostProtectionResource);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Permissions.HostProtectionResource)argObj.value;
				}

				System.Security.Permissions.HostProtectionResource ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Permissions.HostProtectionResource);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Permissions.HostProtectionResource)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
