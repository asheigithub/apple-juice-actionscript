using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_principal_TokenAccessLevels_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_principal_TokenAccessLevels_creator", default(System.Security.Principal.TokenAccessLevels)));
			bin.regNativeFunction(new system_security_principal_TokenAccessLevels_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_AssignPrimary_getter",()=>{ return System.Security.Principal.TokenAccessLevels.AssignPrimary;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_Duplicate_getter",()=>{ return System.Security.Principal.TokenAccessLevels.Duplicate;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_Impersonate_getter",()=>{ return System.Security.Principal.TokenAccessLevels.Impersonate;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_Query_getter",()=>{ return System.Security.Principal.TokenAccessLevels.Query;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_QuerySource_getter",()=>{ return System.Security.Principal.TokenAccessLevels.QuerySource;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_AdjustPrivileges_getter",()=>{ return System.Security.Principal.TokenAccessLevels.AdjustPrivileges;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_AdjustGroups_getter",()=>{ return System.Security.Principal.TokenAccessLevels.AdjustGroups;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_AdjustDefault_getter",()=>{ return System.Security.Principal.TokenAccessLevels.AdjustDefault;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_AdjustSessionId_getter",()=>{ return System.Security.Principal.TokenAccessLevels.AdjustSessionId;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_Read_getter",()=>{ return System.Security.Principal.TokenAccessLevels.Read;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_Write_getter",()=>{ return System.Security.Principal.TokenAccessLevels.Write;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_AllAccess_getter",()=>{ return System.Security.Principal.TokenAccessLevels.AllAccess;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_principal_TokenAccessLevels_MaximumAllowed_getter",()=>{ return System.Security.Principal.TokenAccessLevels.MaximumAllowed;}));
			bin.regNativeFunction(new system_security_principal_TokenAccessLevels_operator_bitOr());
		}

		class system_security_principal_TokenAccessLevels_ctor : NativeFunctionBase
		{
			public system_security_principal_TokenAccessLevels_ctor()
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
					return "system_security_principal_TokenAccessLevels_ctor";
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

		class system_security_principal_TokenAccessLevels_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_principal_TokenAccessLevels_operator_bitOr() : base(2)
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
					return "system_security_principal_TokenAccessLevels_operator_bitOr";
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
				System.Security.Principal.TokenAccessLevels ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Principal.TokenAccessLevels);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Principal.TokenAccessLevels)argObj.value;
				}

				System.Security.Principal.TokenAccessLevels ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Principal.TokenAccessLevels);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Principal.TokenAccessLevels)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
