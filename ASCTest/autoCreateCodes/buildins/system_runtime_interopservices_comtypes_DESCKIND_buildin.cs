using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_comtypes_DESCKIND_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_comtypes_DESCKIND_creator", default(System.Runtime.InteropServices.ComTypes.DESCKIND)));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_DESCKIND_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_DESCKIND_DESCKIND_NONE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.DESCKIND.DESCKIND_NONE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_DESCKIND_DESCKIND_FUNCDESC_getter",()=>{ return System.Runtime.InteropServices.ComTypes.DESCKIND.DESCKIND_FUNCDESC;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_DESCKIND_DESCKIND_VARDESC_getter",()=>{ return System.Runtime.InteropServices.ComTypes.DESCKIND.DESCKIND_VARDESC;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_DESCKIND_DESCKIND_TYPECOMP_getter",()=>{ return System.Runtime.InteropServices.ComTypes.DESCKIND.DESCKIND_TYPECOMP;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_DESCKIND_DESCKIND_IMPLICITAPPOBJ_getter",()=>{ return System.Runtime.InteropServices.ComTypes.DESCKIND.DESCKIND_IMPLICITAPPOBJ;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_DESCKIND_DESCKIND_MAX_getter",()=>{ return System.Runtime.InteropServices.ComTypes.DESCKIND.DESCKIND_MAX;}));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_DESCKIND_operator_bitOr());
		}

		class system_runtime_interopservices_comtypes_DESCKIND_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_comtypes_DESCKIND_ctor()
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
					return "system_runtime_interopservices_comtypes_DESCKIND_ctor";
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

		class system_runtime_interopservices_comtypes_DESCKIND_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_comtypes_DESCKIND_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_comtypes_DESCKIND_operator_bitOr";
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
				System.Runtime.InteropServices.ComTypes.DESCKIND ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.ComTypes.DESCKIND);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.ComTypes.DESCKIND)argObj.value;
				}

				System.Runtime.InteropServices.ComTypes.DESCKIND ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.ComTypes.DESCKIND);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.ComTypes.DESCKIND)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
