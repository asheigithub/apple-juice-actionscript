using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_io_FileAttributes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_io_FileAttributes_creator", default(System.IO.FileAttributes)));
			bin.regNativeFunction(new system_io_FileAttributes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_ReadOnly_getter",()=>{ return System.IO.FileAttributes.ReadOnly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Hidden_getter",()=>{ return System.IO.FileAttributes.Hidden;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_System_getter",()=>{ return System.IO.FileAttributes.System;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Directory_getter",()=>{ return System.IO.FileAttributes.Directory;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Archive_getter",()=>{ return System.IO.FileAttributes.Archive;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Device_getter",()=>{ return System.IO.FileAttributes.Device;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Normal_getter",()=>{ return System.IO.FileAttributes.Normal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Temporary_getter",()=>{ return System.IO.FileAttributes.Temporary;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_SparseFile_getter",()=>{ return System.IO.FileAttributes.SparseFile;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_ReparsePoint_getter",()=>{ return System.IO.FileAttributes.ReparsePoint;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Compressed_getter",()=>{ return System.IO.FileAttributes.Compressed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Offline_getter",()=>{ return System.IO.FileAttributes.Offline;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_NotContentIndexed_getter",()=>{ return System.IO.FileAttributes.NotContentIndexed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_FileAttributes_Encrypted_getter",()=>{ return System.IO.FileAttributes.Encrypted;}));
			bin.regNativeFunction(new system_io_FileAttributes_operator_bitOr());
		}

		class system_io_FileAttributes_ctor : NativeFunctionBase
		{
			public system_io_FileAttributes_ctor()
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
					return "system_io_FileAttributes_ctor";
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

		class system_io_FileAttributes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_io_FileAttributes_operator_bitOr() : base(2)
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
					return "system_io_FileAttributes_operator_bitOr";
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
				System.IO.FileAttributes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.IO.FileAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.IO.FileAttributes)argObj.value;
				}

				System.IO.FileAttributes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.IO.FileAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.IO.FileAttributes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
