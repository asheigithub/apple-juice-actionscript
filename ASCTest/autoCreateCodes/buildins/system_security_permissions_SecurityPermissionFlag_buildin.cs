using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_permissions_SecurityPermissionFlag_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_permissions_SecurityPermissionFlag_creator", default(System.Security.Permissions.SecurityPermissionFlag)));
			bin.regNativeFunction(new system_security_permissions_SecurityPermissionFlag_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_NoFlags_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.NoFlags;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_Assertion_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.Assertion;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_UnmanagedCode_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_SkipVerification_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.SkipVerification;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_Execution_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.Execution;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_ControlThread_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.ControlThread;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_ControlEvidence_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.ControlEvidence;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_ControlPolicy_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.ControlPolicy;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_SerializationFormatter_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_ControlDomainPolicy_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.ControlDomainPolicy;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_ControlPrincipal_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.ControlPrincipal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_ControlAppDomain_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.ControlAppDomain;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_RemotingConfiguration_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.RemotingConfiguration;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_Infrastructure_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.Infrastructure;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_BindingRedirects_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.BindingRedirects;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_SecurityPermissionFlag_AllFlags_getter",()=>{ return System.Security.Permissions.SecurityPermissionFlag.AllFlags;}));
			bin.regNativeFunction(new system_security_permissions_SecurityPermissionFlag_operator_bitOr());
		}

		class system_security_permissions_SecurityPermissionFlag_ctor : NativeFunctionBase
		{
			public system_security_permissions_SecurityPermissionFlag_ctor()
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
					return "system_security_permissions_SecurityPermissionFlag_ctor";
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

		class system_security_permissions_SecurityPermissionFlag_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_permissions_SecurityPermissionFlag_operator_bitOr() : base(2)
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
					return "system_security_permissions_SecurityPermissionFlag_operator_bitOr";
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
				System.Security.Permissions.SecurityPermissionFlag ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Permissions.SecurityPermissionFlag);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Permissions.SecurityPermissionFlag)argObj.value;
				}

				System.Security.Permissions.SecurityPermissionFlag ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Permissions.SecurityPermissionFlag);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Permissions.SecurityPermissionFlag)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
