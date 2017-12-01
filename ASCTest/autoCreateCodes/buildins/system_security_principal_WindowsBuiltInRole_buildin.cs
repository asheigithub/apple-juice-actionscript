using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_principal_WindowsBuiltInRole_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_principal_WindowsBuiltInRole_creator", default(System.Security.Principal.WindowsBuiltInRole)));
			bin.regNativeFunction(new system_security_principal_WindowsBuiltInRole_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_Administrator_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.Administrator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_User_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.User;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_Guest_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.Guest;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_PowerUser_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.PowerUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_AccountOperator_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.AccountOperator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_SystemOperator_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.SystemOperator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_PrintOperator_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.PrintOperator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_BackupOperator_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.BackupOperator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsBuiltInRole_Replicator_getter",()=>{ return System.Security.Principal.WindowsBuiltInRole.Replicator;}));
			bin.regNativeFunction(new system_security_principal_WindowsBuiltInRole_operator_bitOr());
		}

		class system_security_principal_WindowsBuiltInRole_ctor : NativeFunctionBase
		{
			public system_security_principal_WindowsBuiltInRole_ctor()
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
					return "system_security_principal_WindowsBuiltInRole_ctor";
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

		class system_security_principal_WindowsBuiltInRole_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_principal_WindowsBuiltInRole_operator_bitOr() : base(2)
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
					return "system_security_principal_WindowsBuiltInRole_operator_bitOr";
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
				System.Security.Principal.WindowsBuiltInRole ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Principal.WindowsBuiltInRole);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Principal.WindowsBuiltInRole)argObj.value;
				}

				System.Security.Principal.WindowsBuiltInRole ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Principal.WindowsBuiltInRole);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Principal.WindowsBuiltInRole)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
