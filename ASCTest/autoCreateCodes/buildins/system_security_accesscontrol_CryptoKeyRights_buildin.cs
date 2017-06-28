using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_accesscontrol_CryptoKeyRights_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_accesscontrol_CryptoKeyRights_creator", default(System.Security.AccessControl.CryptoKeyRights)));
			bin.regNativeFunction(new system_security_accesscontrol_CryptoKeyRights_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_ReadData_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.ReadData;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_WriteData_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.WriteData;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_ReadExtendedAttributes_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.ReadExtendedAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_WriteExtendedAttributes_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.WriteExtendedAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_ReadAttributes_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.ReadAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_WriteAttributes_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.WriteAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_Delete_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.Delete;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_ReadPermissions_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.ReadPermissions;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_ChangePermissions_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.ChangePermissions;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_TakeOwnership_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.TakeOwnership;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_Synchronize_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.Synchronize;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_FullControl_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.FullControl;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_GenericAll_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.GenericAll;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_GenericExecute_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.GenericExecute;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_GenericWrite_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.GenericWrite;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_CryptoKeyRights_GenericRead_getter",()=>{ return System.Security.AccessControl.CryptoKeyRights.GenericRead;}));
			bin.regNativeFunction(new system_security_accesscontrol_CryptoKeyRights_operator_bitOr());
		}

		class system_security_accesscontrol_CryptoKeyRights_ctor : NativeFunctionBase
		{
			public system_security_accesscontrol_CryptoKeyRights_ctor()
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
					return "system_security_accesscontrol_CryptoKeyRights_ctor";
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

		class system_security_accesscontrol_CryptoKeyRights_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_accesscontrol_CryptoKeyRights_operator_bitOr() : base(2)
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
					return "system_security_accesscontrol_CryptoKeyRights_operator_bitOr";
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
				System.Security.AccessControl.CryptoKeyRights ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.AccessControl.CryptoKeyRights);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Security.AccessControl.CryptoKeyRights)argObj.value;
				}

				System.Security.AccessControl.CryptoKeyRights ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.AccessControl.CryptoKeyRights);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Security.AccessControl.CryptoKeyRights)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
