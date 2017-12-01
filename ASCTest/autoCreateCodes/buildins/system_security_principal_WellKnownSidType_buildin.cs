using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_principal_WellKnownSidType_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_principal_WellKnownSidType_creator", default(System.Security.Principal.WellKnownSidType)));
			bin.regNativeFunction(new system_security_principal_WellKnownSidType_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_NullSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.NullSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_WorldSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.WorldSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_LocalSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.LocalSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_CreatorOwnerSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.CreatorOwnerSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_CreatorGroupSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.CreatorGroupSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_CreatorOwnerServerSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.CreatorOwnerServerSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_CreatorGroupServerSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.CreatorGroupServerSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_NTAuthoritySid_getter",()=>{ return System.Security.Principal.WellKnownSidType.NTAuthoritySid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_DialupSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.DialupSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_NetworkSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.NetworkSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BatchSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BatchSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_InteractiveSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.InteractiveSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_ServiceSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.ServiceSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AnonymousSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AnonymousSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_ProxySid_getter",()=>{ return System.Security.Principal.WellKnownSidType.ProxySid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_EnterpriseControllersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.EnterpriseControllersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_SelfSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.SelfSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AuthenticatedUserSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AuthenticatedUserSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_RestrictedCodeSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.RestrictedCodeSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_TerminalServerSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.TerminalServerSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_RemoteLogonIdSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.RemoteLogonIdSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_LogonIdsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.LogonIdsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_LocalSystemSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.LocalSystemSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_LocalServiceSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.LocalServiceSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_NetworkServiceSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.NetworkServiceSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinDomainSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinDomainSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinAdministratorsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinAdministratorsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinUsersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinUsersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinGuestsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinGuestsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinPowerUsersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinPowerUsersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinAccountOperatorsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinAccountOperatorsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinSystemOperatorsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinSystemOperatorsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinPrintOperatorsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinPrintOperatorsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinBackupOperatorsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinBackupOperatorsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinReplicatorSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinReplicatorSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinPreWindows2000CompatibleAccessSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinPreWindows2000CompatibleAccessSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinRemoteDesktopUsersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinRemoteDesktopUsersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinNetworkConfigurationOperatorsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinNetworkConfigurationOperatorsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountAdministratorSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountAdministratorSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountGuestSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountGuestSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountKrbtgtSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountKrbtgtSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountDomainAdminsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountDomainAdminsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountDomainUsersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountDomainUsersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountDomainGuestsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountDomainGuestsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountComputersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountComputersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountControllersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountControllersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountCertAdminsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountCertAdminsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountSchemaAdminsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountSchemaAdminsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountEnterpriseAdminsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountEnterpriseAdminsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountPolicyAdminsSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountPolicyAdminsSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_AccountRasAndIasServersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.AccountRasAndIasServersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_NtlmAuthenticationSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.NtlmAuthenticationSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_DigestAuthenticationSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.DigestAuthenticationSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_SChannelAuthenticationSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.SChannelAuthenticationSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_ThisOrganizationSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.ThisOrganizationSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_OtherOrganizationSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.OtherOrganizationSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinIncomingForestTrustBuildersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinIncomingForestTrustBuildersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinPerformanceMonitoringUsersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinPerformanceMonitoringUsersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinPerformanceLoggingUsersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinPerformanceLoggingUsersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_BuiltinAuthorizationAccessSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.BuiltinAuthorizationAccessSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_WinBuiltinTerminalServerLicenseServersSid_getter",()=>{ return System.Security.Principal.WellKnownSidType.WinBuiltinTerminalServerLicenseServersSid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WellKnownSidType_MaxDefined_getter",()=>{ return System.Security.Principal.WellKnownSidType.MaxDefined;}));
			bin.regNativeFunction(new system_security_principal_WellKnownSidType_operator_bitOr());
		}

		class system_security_principal_WellKnownSidType_ctor : NativeFunctionBase
		{
			public system_security_principal_WellKnownSidType_ctor()
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
					return "system_security_principal_WellKnownSidType_ctor";
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

		class system_security_principal_WellKnownSidType_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_principal_WellKnownSidType_operator_bitOr() : base(2)
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
					return "system_security_principal_WellKnownSidType_operator_bitOr";
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
				System.Security.Principal.WellKnownSidType ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Principal.WellKnownSidType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Principal.WellKnownSidType)argObj.value;
				}

				System.Security.Principal.WellKnownSidType ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Principal.WellKnownSidType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Principal.WellKnownSidType)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
