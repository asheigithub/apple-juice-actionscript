using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_principal_WindowsAccountType_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_principal_WindowsAccountType_creator", default(System.Security.Principal.WindowsAccountType)));
			bin.regNativeFunction(new system_security_principal_WindowsAccountType_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsAccountType_Normal_getter",()=>{ return System.Security.Principal.WindowsAccountType.Normal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsAccountType_Guest_getter",()=>{ return System.Security.Principal.WindowsAccountType.Guest;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsAccountType_System_getter",()=>{ return System.Security.Principal.WindowsAccountType.System;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_WindowsAccountType_Anonymous_getter",()=>{ return System.Security.Principal.WindowsAccountType.Anonymous;}));
			bin.regNativeFunction(new system_security_principal_WindowsAccountType_operator_bitOr());
		}

		class system_security_principal_WindowsAccountType_ctor : NativeFunctionBase
		{
			public system_security_principal_WindowsAccountType_ctor()
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
					return "system_security_principal_WindowsAccountType_ctor";
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

		class system_security_principal_WindowsAccountType_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_principal_WindowsAccountType_operator_bitOr() : base(2)
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
					return "system_security_principal_WindowsAccountType_operator_bitOr";
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
				System.Security.Principal.WindowsAccountType ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Principal.WindowsAccountType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Security.Principal.WindowsAccountType)argObj.value;
				}

				System.Security.Principal.WindowsAccountType ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Principal.WindowsAccountType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Security.Principal.WindowsAccountType)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
