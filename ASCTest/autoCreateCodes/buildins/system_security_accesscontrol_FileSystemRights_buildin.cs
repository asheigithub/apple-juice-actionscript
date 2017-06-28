using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_accesscontrol_FileSystemRights_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_accesscontrol_FileSystemRights_creator", default(System.Security.AccessControl.FileSystemRights)));
			bin.regNativeFunction(new system_security_accesscontrol_FileSystemRights_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ReadData_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ReadData;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ListDirectory_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ListDirectory;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_WriteData_getter",()=>{ return System.Security.AccessControl.FileSystemRights.WriteData;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_CreateFiles_getter",()=>{ return System.Security.AccessControl.FileSystemRights.CreateFiles;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_AppendData_getter",()=>{ return System.Security.AccessControl.FileSystemRights.AppendData;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_CreateDirectories_getter",()=>{ return System.Security.AccessControl.FileSystemRights.CreateDirectories;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ReadExtendedAttributes_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ReadExtendedAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_WriteExtendedAttributes_getter",()=>{ return System.Security.AccessControl.FileSystemRights.WriteExtendedAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ExecuteFile_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ExecuteFile;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_Traverse_getter",()=>{ return System.Security.AccessControl.FileSystemRights.Traverse;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_DeleteSubdirectoriesAndFiles_getter",()=>{ return System.Security.AccessControl.FileSystemRights.DeleteSubdirectoriesAndFiles;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ReadAttributes_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ReadAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_WriteAttributes_getter",()=>{ return System.Security.AccessControl.FileSystemRights.WriteAttributes;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_Delete_getter",()=>{ return System.Security.AccessControl.FileSystemRights.Delete;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ReadPermissions_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ReadPermissions;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ChangePermissions_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ChangePermissions;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_TakeOwnership_getter",()=>{ return System.Security.AccessControl.FileSystemRights.TakeOwnership;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_Synchronize_getter",()=>{ return System.Security.AccessControl.FileSystemRights.Synchronize;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_FullControl_getter",()=>{ return System.Security.AccessControl.FileSystemRights.FullControl;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_Read_getter",()=>{ return System.Security.AccessControl.FileSystemRights.Read;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_ReadAndExecute_getter",()=>{ return System.Security.AccessControl.FileSystemRights.ReadAndExecute;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_Write_getter",()=>{ return System.Security.AccessControl.FileSystemRights.Write;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_accesscontrol_FileSystemRights_Modify_getter",()=>{ return System.Security.AccessControl.FileSystemRights.Modify;}));
			bin.regNativeFunction(new system_security_accesscontrol_FileSystemRights_operator_bitOr());
		}

		class system_security_accesscontrol_FileSystemRights_ctor : NativeFunctionBase
		{
			public system_security_accesscontrol_FileSystemRights_ctor()
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
					return "system_security_accesscontrol_FileSystemRights_ctor";
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

		class system_security_accesscontrol_FileSystemRights_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_accesscontrol_FileSystemRights_operator_bitOr() : base(2)
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
					return "system_security_accesscontrol_FileSystemRights_operator_bitOr";
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
				System.Security.AccessControl.FileSystemRights ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.AccessControl.FileSystemRights);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Security.AccessControl.FileSystemRights)argObj.value;
				}

				System.Security.AccessControl.FileSystemRights ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.AccessControl.FileSystemRights);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Security.AccessControl.FileSystemRights)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
