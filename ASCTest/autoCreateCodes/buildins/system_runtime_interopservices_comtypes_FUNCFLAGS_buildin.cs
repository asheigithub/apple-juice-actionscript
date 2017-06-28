using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_comtypes_FUNCFLAGS_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_comtypes_FUNCFLAGS_creator", default(System.Runtime.InteropServices.ComTypes.FUNCFLAGS)));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_FUNCFLAGS_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FRESTRICTED_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FRESTRICTED;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FSOURCE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FSOURCE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FBINDABLE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FBINDABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FREQUESTEDIT_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FREQUESTEDIT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FDISPLAYBIND_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FDISPLAYBIND;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FDEFAULTBIND_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FDEFAULTBIND;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FHIDDEN_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FHIDDEN;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FUSESGETLASTERROR_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FUSESGETLASTERROR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FDEFAULTCOLLELEM_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FDEFAULTCOLLELEM;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FUIDEFAULT_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FUIDEFAULT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FNONBROWSABLE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FNONBROWSABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FREPLACEABLE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FREPLACEABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_FUNCFLAGS_FUNCFLAG_FIMMEDIATEBIND_getter",()=>{ return System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FIMMEDIATEBIND;}));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_FUNCFLAGS_operator_bitOr());
		}

		class system_runtime_interopservices_comtypes_FUNCFLAGS_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_comtypes_FUNCFLAGS_ctor()
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
					return "system_runtime_interopservices_comtypes_FUNCFLAGS_ctor";
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

		class system_runtime_interopservices_comtypes_FUNCFLAGS_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_comtypes_FUNCFLAGS_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_comtypes_FUNCFLAGS_operator_bitOr";
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
				System.Runtime.InteropServices.ComTypes.FUNCFLAGS ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.ComTypes.FUNCFLAGS);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.ComTypes.FUNCFLAGS)argObj.value;
				}

				System.Runtime.InteropServices.ComTypes.FUNCFLAGS ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.ComTypes.FUNCFLAGS);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.ComTypes.FUNCFLAGS)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
