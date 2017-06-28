using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_accesscontrol_AceType_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_accesscontrol_AceType_creator", default(System.Security.AccessControl.AceType)));
			bin.regNativeFunction(new system_security_accesscontrol_AceType_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessAllowed_getter",()=>{ return System.Security.AccessControl.AceType.AccessAllowed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessDenied_getter",()=>{ return System.Security.AccessControl.AceType.AccessDenied;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAudit_getter",()=>{ return System.Security.AccessControl.AceType.SystemAudit;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAlarm_getter",()=>{ return System.Security.AccessControl.AceType.SystemAlarm;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessAllowedCompound_getter",()=>{ return System.Security.AccessControl.AceType.AccessAllowedCompound;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessAllowedObject_getter",()=>{ return System.Security.AccessControl.AceType.AccessAllowedObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessDeniedObject_getter",()=>{ return System.Security.AccessControl.AceType.AccessDeniedObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAuditObject_getter",()=>{ return System.Security.AccessControl.AceType.SystemAuditObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAlarmObject_getter",()=>{ return System.Security.AccessControl.AceType.SystemAlarmObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessAllowedCallback_getter",()=>{ return System.Security.AccessControl.AceType.AccessAllowedCallback;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessDeniedCallback_getter",()=>{ return System.Security.AccessControl.AceType.AccessDeniedCallback;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessAllowedCallbackObject_getter",()=>{ return System.Security.AccessControl.AceType.AccessAllowedCallbackObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_AccessDeniedCallbackObject_getter",()=>{ return System.Security.AccessControl.AceType.AccessDeniedCallbackObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAuditCallback_getter",()=>{ return System.Security.AccessControl.AceType.SystemAuditCallback;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAlarmCallback_getter",()=>{ return System.Security.AccessControl.AceType.SystemAlarmCallback;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAuditCallbackObject_getter",()=>{ return System.Security.AccessControl.AceType.SystemAuditCallbackObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_SystemAlarmCallbackObject_getter",()=>{ return System.Security.AccessControl.AceType.SystemAlarmCallbackObject;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceType_MaxDefinedAceType_getter",()=>{ return System.Security.AccessControl.AceType.MaxDefinedAceType;}));
			bin.regNativeFunction(new system_security_accesscontrol_AceType_operator_bitOr());
		}

		class system_security_accesscontrol_AceType_ctor : NativeFunctionBase
		{
			public system_security_accesscontrol_AceType_ctor()
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
					return "system_security_accesscontrol_AceType_ctor";
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

		class system_security_accesscontrol_AceType_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_accesscontrol_AceType_operator_bitOr() : base(2)
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
					return "system_security_accesscontrol_AceType_operator_bitOr";
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
				System.Security.AccessControl.AceType ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.AccessControl.AceType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Security.AccessControl.AceType)argObj.value;
				}

				System.Security.AccessControl.AceType ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.AccessControl.AceType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Security.AccessControl.AceType)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
