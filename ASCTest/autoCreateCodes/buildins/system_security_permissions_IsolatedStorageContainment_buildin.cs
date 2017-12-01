using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_permissions_IsolatedStorageContainment_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_permissions_IsolatedStorageContainment_creator", default(System.Security.Permissions.IsolatedStorageContainment)));
			bin.regNativeFunction(new system_security_permissions_IsolatedStorageContainment_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_None_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_DomainIsolationByUser_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.DomainIsolationByUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_ApplicationIsolationByUser_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.ApplicationIsolationByUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_AssemblyIsolationByUser_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.AssemblyIsolationByUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_DomainIsolationByMachine_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.DomainIsolationByMachine;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_AssemblyIsolationByMachine_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.AssemblyIsolationByMachine;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_ApplicationIsolationByMachine_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.ApplicationIsolationByMachine;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_DomainIsolationByRoamingUser_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.DomainIsolationByRoamingUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_AssemblyIsolationByRoamingUser_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.AssemblyIsolationByRoamingUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_ApplicationIsolationByRoamingUser_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.ApplicationIsolationByRoamingUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_AdministerIsolatedStorageByUser_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.AdministerIsolatedStorageByUser;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_permissions_IsolatedStorageContainment_UnrestrictedIsolatedStorage_getter",()=>{ return System.Security.Permissions.IsolatedStorageContainment.UnrestrictedIsolatedStorage;}));
			bin.regNativeFunction(new system_security_permissions_IsolatedStorageContainment_operator_bitOr());
		}

		class system_security_permissions_IsolatedStorageContainment_ctor : NativeFunctionBase
		{
			public system_security_permissions_IsolatedStorageContainment_ctor()
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
					return "system_security_permissions_IsolatedStorageContainment_ctor";
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

		class system_security_permissions_IsolatedStorageContainment_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_permissions_IsolatedStorageContainment_operator_bitOr() : base(2)
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
					return "system_security_permissions_IsolatedStorageContainment_operator_bitOr";
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
				System.Security.Permissions.IsolatedStorageContainment ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Permissions.IsolatedStorageContainment);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Permissions.IsolatedStorageContainment)argObj.value;
				}

				System.Security.Permissions.IsolatedStorageContainment ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Permissions.IsolatedStorageContainment);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Permissions.IsolatedStorageContainment)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
