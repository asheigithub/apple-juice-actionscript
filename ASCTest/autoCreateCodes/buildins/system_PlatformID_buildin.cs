using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_PlatformID_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_PlatformID_creator", default(System.PlatformID)));
			bin.regNativeFunction(new system_PlatformID_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_PlatformID_Win32S_getter",()=>{ return System.PlatformID.Win32S;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_PlatformID_Win32Windows_getter",()=>{ return System.PlatformID.Win32Windows;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_PlatformID_Win32NT_getter",()=>{ return System.PlatformID.Win32NT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_PlatformID_WinCE_getter",()=>{ return System.PlatformID.WinCE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_PlatformID_Unix_getter",()=>{ return System.PlatformID.Unix;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_PlatformID_Xbox_getter",()=>{ return System.PlatformID.Xbox;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_PlatformID_MacOSX_getter",()=>{ return System.PlatformID.MacOSX;}));
			bin.regNativeFunction(new system_PlatformID_operator_bitOr());
		}

		class system_PlatformID_ctor : NativeFunctionBase
		{
			public system_PlatformID_ctor()
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
					return "system_PlatformID_ctor";
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

		class system_PlatformID_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_PlatformID_operator_bitOr() : base(2)
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
					return "system_PlatformID_operator_bitOr";
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
				System.PlatformID ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.PlatformID);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.PlatformID)argObj.value;
				}

				System.PlatformID ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.PlatformID);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.PlatformID)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
