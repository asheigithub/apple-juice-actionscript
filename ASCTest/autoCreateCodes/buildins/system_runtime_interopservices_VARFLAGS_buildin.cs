using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_VARFLAGS_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_VARFLAGS_creator", default(System.Runtime.InteropServices.VARFLAGS)));
			bin.regNativeFunction(new system_runtime_interopservices_VARFLAGS_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FREADONLY_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FREADONLY;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FSOURCE_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FSOURCE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FBINDABLE_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FBINDABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FREQUESTEDIT_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FREQUESTEDIT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FDISPLAYBIND_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FDISPLAYBIND;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FDEFAULTBIND_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FDEFAULTBIND;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FHIDDEN_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FHIDDEN;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FRESTRICTED_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FRESTRICTED;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FDEFAULTCOLLELEM_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FDEFAULTCOLLELEM;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FUIDEFAULT_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FUIDEFAULT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FNONBROWSABLE_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FNONBROWSABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FREPLACEABLE_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FREPLACEABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VARFLAGS_VARFLAG_FIMMEDIATEBIND_getter",()=>{ return System.Runtime.InteropServices.VARFLAGS.VARFLAG_FIMMEDIATEBIND;}));
			bin.regNativeFunction(new system_runtime_interopservices_VARFLAGS_operator_bitOr());
		}

		class system_runtime_interopservices_VARFLAGS_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_VARFLAGS_ctor()
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
					return "system_runtime_interopservices_VARFLAGS_ctor";
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

		class system_runtime_interopservices_VARFLAGS_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_VARFLAGS_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_VARFLAGS_operator_bitOr";
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
				System.Runtime.InteropServices.VARFLAGS ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.VARFLAGS);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.VARFLAGS)argObj.value;
				}

				System.Runtime.InteropServices.VARFLAGS ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.VARFLAGS);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.VARFLAGS)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
