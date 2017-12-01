using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_accesscontrol_ControlFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_accesscontrol_ControlFlags_creator", default(System.Security.AccessControl.ControlFlags)));
			bin.regNativeFunction(new system_security_accesscontrol_ControlFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_None_getter",()=>{ return System.Security.AccessControl.ControlFlags.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_OwnerDefaulted_getter",()=>{ return System.Security.AccessControl.ControlFlags.OwnerDefaulted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_GroupDefaulted_getter",()=>{ return System.Security.AccessControl.ControlFlags.GroupDefaulted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_DiscretionaryAclPresent_getter",()=>{ return System.Security.AccessControl.ControlFlags.DiscretionaryAclPresent;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_DiscretionaryAclDefaulted_getter",()=>{ return System.Security.AccessControl.ControlFlags.DiscretionaryAclDefaulted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_SystemAclPresent_getter",()=>{ return System.Security.AccessControl.ControlFlags.SystemAclPresent;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_SystemAclDefaulted_getter",()=>{ return System.Security.AccessControl.ControlFlags.SystemAclDefaulted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_DiscretionaryAclUntrusted_getter",()=>{ return System.Security.AccessControl.ControlFlags.DiscretionaryAclUntrusted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_ServerSecurity_getter",()=>{ return System.Security.AccessControl.ControlFlags.ServerSecurity;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_DiscretionaryAclAutoInheritRequired_getter",()=>{ return System.Security.AccessControl.ControlFlags.DiscretionaryAclAutoInheritRequired;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_SystemAclAutoInheritRequired_getter",()=>{ return System.Security.AccessControl.ControlFlags.SystemAclAutoInheritRequired;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_DiscretionaryAclAutoInherited_getter",()=>{ return System.Security.AccessControl.ControlFlags.DiscretionaryAclAutoInherited;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_SystemAclAutoInherited_getter",()=>{ return System.Security.AccessControl.ControlFlags.SystemAclAutoInherited;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_DiscretionaryAclProtected_getter",()=>{ return System.Security.AccessControl.ControlFlags.DiscretionaryAclProtected;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_SystemAclProtected_getter",()=>{ return System.Security.AccessControl.ControlFlags.SystemAclProtected;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_RMControlValid_getter",()=>{ return System.Security.AccessControl.ControlFlags.RMControlValid;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_ControlFlags_SelfRelative_getter",()=>{ return System.Security.AccessControl.ControlFlags.SelfRelative;}));
			bin.regNativeFunction(new system_security_accesscontrol_ControlFlags_operator_bitOr());
		}

		class system_security_accesscontrol_ControlFlags_ctor : NativeFunctionBase
		{
			public system_security_accesscontrol_ControlFlags_ctor()
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
					return "system_security_accesscontrol_ControlFlags_ctor";
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

		class system_security_accesscontrol_ControlFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_accesscontrol_ControlFlags_operator_bitOr() : base(2)
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
					return "system_security_accesscontrol_ControlFlags_operator_bitOr";
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
				System.Security.AccessControl.ControlFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.AccessControl.ControlFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.AccessControl.ControlFlags)argObj.value;
				}

				System.Security.AccessControl.ControlFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.AccessControl.ControlFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.AccessControl.ControlFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
