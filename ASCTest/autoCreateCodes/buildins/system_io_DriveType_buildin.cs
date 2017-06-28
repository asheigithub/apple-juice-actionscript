using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_io_DriveType_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_io_DriveType_creator", default(System.IO.DriveType)));
			bin.regNativeFunction(new system_io_DriveType_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_DriveType_Unknown_getter",()=>{ return System.IO.DriveType.Unknown;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_DriveType_NoRootDirectory_getter",()=>{ return System.IO.DriveType.NoRootDirectory;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_DriveType_Removable_getter",()=>{ return System.IO.DriveType.Removable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_DriveType_Fixed_getter",()=>{ return System.IO.DriveType.Fixed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_DriveType_Network_getter",()=>{ return System.IO.DriveType.Network;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_DriveType_CDRom_getter",()=>{ return System.IO.DriveType.CDRom;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_io_DriveType_Ram_getter",()=>{ return System.IO.DriveType.Ram;}));
			bin.regNativeFunction(new system_io_DriveType_operator_bitOr());
		}

		class system_io_DriveType_ctor : NativeFunctionBase
		{
			public system_io_DriveType_ctor()
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
					return "system_io_DriveType_ctor";
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

		class system_io_DriveType_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_io_DriveType_operator_bitOr() : base(2)
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
					return "system_io_DriveType_operator_bitOr";
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
				System.IO.DriveType ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.IO.DriveType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.IO.DriveType)argObj.value;
				}

				System.IO.DriveType ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.IO.DriveType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.IO.DriveType)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
