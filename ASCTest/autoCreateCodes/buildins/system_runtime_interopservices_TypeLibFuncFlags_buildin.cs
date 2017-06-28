using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_TypeLibFuncFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_TypeLibFuncFlags_creator", default(System.Runtime.InteropServices.TypeLibFuncFlags)));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibFuncFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FRestricted_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FRestricted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FSource_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FSource;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FBindable_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FBindable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FRequestEdit_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FRequestEdit;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FDisplayBind_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FDisplayBind;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FDefaultBind_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FDefaultBind;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FHidden_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FHidden;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FUsesGetLastError_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FUsesGetLastError;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FDefaultCollelem_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FDefaultCollelem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FUiDefault_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FUiDefault;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FNonBrowsable_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FNonBrowsable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FReplaceable_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FReplaceable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibFuncFlags_FImmediateBind_getter",()=>{ return System.Runtime.InteropServices.TypeLibFuncFlags.FImmediateBind;}));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibFuncFlags_operator_bitOr());
		}

		class system_runtime_interopservices_TypeLibFuncFlags_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_TypeLibFuncFlags_ctor()
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
					return "system_runtime_interopservices_TypeLibFuncFlags_ctor";
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

		class system_runtime_interopservices_TypeLibFuncFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_TypeLibFuncFlags_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_TypeLibFuncFlags_operator_bitOr";
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
				System.Runtime.InteropServices.TypeLibFuncFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.TypeLibFuncFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.TypeLibFuncFlags)argObj.value;
				}

				System.Runtime.InteropServices.TypeLibFuncFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.TypeLibFuncFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.TypeLibFuncFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
