using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_permissions_KeyContainerPermissionFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_permissions_KeyContainerPermissionFlags_creator", default(System.Security.Permissions.KeyContainerPermissionFlags)));
			bin.regNativeFunction(new system_security_permissions_KeyContainerPermissionFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_NoFlags_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.NoFlags;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_Create_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.Create;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_Open_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.Open;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_Delete_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.Delete;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_Import_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.Import;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_Export_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.Export;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_Sign_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.Sign;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_Decrypt_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.Decrypt;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_ViewAcl_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.ViewAcl;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_ChangeAcl_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.ChangeAcl;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_KeyContainerPermissionFlags_AllFlags_getter",()=>{ return System.Security.Permissions.KeyContainerPermissionFlags.AllFlags;}));
			bin.regNativeFunction(new system_security_permissions_KeyContainerPermissionFlags_operator_bitOr());
		}

		class system_security_permissions_KeyContainerPermissionFlags_ctor : NativeFunctionBase
		{
			public system_security_permissions_KeyContainerPermissionFlags_ctor()
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
					return "system_security_permissions_KeyContainerPermissionFlags_ctor";
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

		class system_security_permissions_KeyContainerPermissionFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_permissions_KeyContainerPermissionFlags_operator_bitOr() : base(2)
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
					return "system_security_permissions_KeyContainerPermissionFlags_operator_bitOr";
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
				System.Security.Permissions.KeyContainerPermissionFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Permissions.KeyContainerPermissionFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Permissions.KeyContainerPermissionFlags)argObj.value;
				}

				System.Security.Permissions.KeyContainerPermissionFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Permissions.KeyContainerPermissionFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Permissions.KeyContainerPermissionFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
