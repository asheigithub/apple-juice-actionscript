using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_accesscontrol_RegistryRights_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_accesscontrol_RegistryRights_creator", default(System.Security.AccessControl.RegistryRights)));
			bin.regNativeFunction(new system_security_accesscontrol_RegistryRights_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_QueryValues_getter",()=>{ return System.Security.AccessControl.RegistryRights.QueryValues;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_SetValue_getter",()=>{ return System.Security.AccessControl.RegistryRights.SetValue;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_CreateSubKey_getter",()=>{ return System.Security.AccessControl.RegistryRights.CreateSubKey;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_EnumerateSubKeys_getter",()=>{ return System.Security.AccessControl.RegistryRights.EnumerateSubKeys;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_Notify_getter",()=>{ return System.Security.AccessControl.RegistryRights.Notify;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_CreateLink_getter",()=>{ return System.Security.AccessControl.RegistryRights.CreateLink;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_ExecuteKey_getter",()=>{ return System.Security.AccessControl.RegistryRights.ExecuteKey;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_ReadKey_getter",()=>{ return System.Security.AccessControl.RegistryRights.ReadKey;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_WriteKey_getter",()=>{ return System.Security.AccessControl.RegistryRights.WriteKey;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_Delete_getter",()=>{ return System.Security.AccessControl.RegistryRights.Delete;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_ReadPermissions_getter",()=>{ return System.Security.AccessControl.RegistryRights.ReadPermissions;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_ChangePermissions_getter",()=>{ return System.Security.AccessControl.RegistryRights.ChangePermissions;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_TakeOwnership_getter",()=>{ return System.Security.AccessControl.RegistryRights.TakeOwnership;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_RegistryRights_FullControl_getter",()=>{ return System.Security.AccessControl.RegistryRights.FullControl;}));
			bin.regNativeFunction(new system_security_accesscontrol_RegistryRights_operator_bitOr());
		}

		class system_security_accesscontrol_RegistryRights_ctor : NativeFunctionBase
		{
			public system_security_accesscontrol_RegistryRights_ctor()
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
					return "system_security_accesscontrol_RegistryRights_ctor";
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

		class system_security_accesscontrol_RegistryRights_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_accesscontrol_RegistryRights_operator_bitOr() : base(2)
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
					return "system_security_accesscontrol_RegistryRights_operator_bitOr";
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
				System.Security.AccessControl.RegistryRights ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.AccessControl.RegistryRights);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.AccessControl.RegistryRights)argObj.value;
				}

				System.Security.AccessControl.RegistryRights ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.AccessControl.RegistryRights);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.AccessControl.RegistryRights)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
