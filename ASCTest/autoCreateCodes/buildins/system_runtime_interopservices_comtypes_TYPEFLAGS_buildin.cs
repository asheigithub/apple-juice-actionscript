using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_comtypes_TYPEFLAGS_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_comtypes_TYPEFLAGS_creator", default(System.Runtime.InteropServices.ComTypes.TYPEFLAGS)));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_TYPEFLAGS_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FAPPOBJECT_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FAPPOBJECT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FCANCREATE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FCANCREATE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FLICENSED_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FLICENSED;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FPREDECLID_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FPREDECLID;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FHIDDEN_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FHIDDEN;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FCONTROL_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FCONTROL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FDUAL_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FDUAL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FNONEXTENSIBLE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FNONEXTENSIBLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FOLEAUTOMATION_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FOLEAUTOMATION;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FRESTRICTED_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FRESTRICTED;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FAGGREGATABLE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FAGGREGATABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FREPLACEABLE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FREPLACEABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FDISPATCHABLE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FDISPATCHABLE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FREVERSEBIND_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FREVERSEBIND;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEFLAGS_TYPEFLAG_FPROXY_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FPROXY;}));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_TYPEFLAGS_operator_bitOr());
		}

		class system_runtime_interopservices_comtypes_TYPEFLAGS_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_comtypes_TYPEFLAGS_ctor()
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
					return "system_runtime_interopservices_comtypes_TYPEFLAGS_ctor";
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

		class system_runtime_interopservices_comtypes_TYPEFLAGS_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_comtypes_TYPEFLAGS_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_comtypes_TYPEFLAGS_operator_bitOr";
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
				System.Runtime.InteropServices.ComTypes.TYPEFLAGS ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.ComTypes.TYPEFLAGS);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.ComTypes.TYPEFLAGS)argObj.value;
				}

				System.Runtime.InteropServices.ComTypes.TYPEFLAGS ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.ComTypes.TYPEFLAGS);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.ComTypes.TYPEFLAGS)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
