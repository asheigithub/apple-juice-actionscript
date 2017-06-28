using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_accesscontrol_AceFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_accesscontrol_AceFlags_creator", default(System.Security.AccessControl.AceFlags)));
			bin.regNativeFunction(new system_security_accesscontrol_AceFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_None_getter",()=>{ return System.Security.AccessControl.AceFlags.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_ObjectInherit_getter",()=>{ return System.Security.AccessControl.AceFlags.ObjectInherit;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_ContainerInherit_getter",()=>{ return System.Security.AccessControl.AceFlags.ContainerInherit;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_NoPropagateInherit_getter",()=>{ return System.Security.AccessControl.AceFlags.NoPropagateInherit;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_InheritOnly_getter",()=>{ return System.Security.AccessControl.AceFlags.InheritOnly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_Inherited_getter",()=>{ return System.Security.AccessControl.AceFlags.Inherited;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_SuccessfulAccess_getter",()=>{ return System.Security.AccessControl.AceFlags.SuccessfulAccess;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_FailedAccess_getter",()=>{ return System.Security.AccessControl.AceFlags.FailedAccess;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_InheritanceFlags_getter",()=>{ return System.Security.AccessControl.AceFlags.InheritanceFlags;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_AceFlags_AuditFlags_getter",()=>{ return System.Security.AccessControl.AceFlags.AuditFlags;}));
			bin.regNativeFunction(new system_security_accesscontrol_AceFlags_operator_bitOr());
		}

		class system_security_accesscontrol_AceFlags_ctor : NativeFunctionBase
		{
			public system_security_accesscontrol_AceFlags_ctor()
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
					return "system_security_accesscontrol_AceFlags_ctor";
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

		class system_security_accesscontrol_AceFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_accesscontrol_AceFlags_operator_bitOr() : base(2)
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
					return "system_security_accesscontrol_AceFlags_operator_bitOr";
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
				System.Security.AccessControl.AceFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.AccessControl.AceFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Security.AccessControl.AceFlags)argObj.value;
				}

				System.Security.AccessControl.AceFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.AccessControl.AceFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Security.AccessControl.AceFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
