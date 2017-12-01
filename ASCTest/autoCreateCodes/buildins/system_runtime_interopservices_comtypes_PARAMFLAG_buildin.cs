using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_comtypes_PARAMFLAG_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_comtypes_PARAMFLAG_creator", default(System.Runtime.InteropServices.ComTypes.PARAMFLAG)));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_PARAMFLAG_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_NONE_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_NONE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_FIN_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FIN;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_FOUT_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FOUT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_FLCID_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FLCID;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_FRETVAL_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FRETVAL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_FOPT_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FOPT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_FHASDEFAULT_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FHASDEFAULT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_comtypes_PARAMFLAG_PARAMFLAG_FHASCUSTDATA_getter",()=>{ return System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FHASCUSTDATA;}));
			bin.regNativeFunction(new system_runtime_interopservices_comtypes_PARAMFLAG_operator_bitOr());
		}

		class system_runtime_interopservices_comtypes_PARAMFLAG_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_comtypes_PARAMFLAG_ctor()
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
					return "system_runtime_interopservices_comtypes_PARAMFLAG_ctor";
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

		class system_runtime_interopservices_comtypes_PARAMFLAG_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_comtypes_PARAMFLAG_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_comtypes_PARAMFLAG_operator_bitOr";
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
				System.Runtime.InteropServices.ComTypes.PARAMFLAG ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.ComTypes.PARAMFLAG);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.ComTypes.PARAMFLAG)argObj.value;
				}

				System.Runtime.InteropServices.ComTypes.PARAMFLAG ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.ComTypes.PARAMFLAG);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.ComTypes.PARAMFLAG)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
