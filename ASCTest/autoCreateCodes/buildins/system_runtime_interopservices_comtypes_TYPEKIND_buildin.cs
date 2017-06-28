using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_comtypes_TYPEKIND_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_comtypes_TYPEKIND_creator", default(System.Runtime.InteropServices.ComTypes.TYPEKIND)));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_TYPEKIND_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_ENUM_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ENUM;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_RECORD_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_RECORD;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_MODULE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_MODULE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_INTERFACE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_INTERFACE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_DISPATCH_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_DISPATCH;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_COCLASS_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_COCLASS;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_ALIAS_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ALIAS;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_UNION_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_UNION;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_TYPEKIND_TKIND_MAX_getter",()=>{ return System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_MAX;}));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_TYPEKIND_operator_bitOr());
		}

		class system_runtime_interopservices_comtypes_TYPEKIND_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_comtypes_TYPEKIND_ctor()
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
					return "system_runtime_interopservices_comtypes_TYPEKIND_ctor";
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

		class system_runtime_interopservices_comtypes_TYPEKIND_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_comtypes_TYPEKIND_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_comtypes_TYPEKIND_operator_bitOr";
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
				System.Runtime.InteropServices.ComTypes.TYPEKIND ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.ComTypes.TYPEKIND);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.ComTypes.TYPEKIND)argObj.value;
				}

				System.Runtime.InteropServices.ComTypes.TYPEKIND ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.ComTypes.TYPEKIND);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.ComTypes.TYPEKIND)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
