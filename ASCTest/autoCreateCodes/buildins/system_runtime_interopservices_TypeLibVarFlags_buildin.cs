using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_TypeLibVarFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_TypeLibVarFlags_creator", default(System.Runtime.InteropServices.TypeLibVarFlags)));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibVarFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FReadOnly_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FReadOnly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FSource_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FSource;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FBindable_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FBindable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FRequestEdit_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FRequestEdit;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FDisplayBind_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FDisplayBind;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FDefaultBind_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FDefaultBind;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FHidden_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FHidden;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FRestricted_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FRestricted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FDefaultCollelem_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FDefaultCollelem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FUiDefault_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FUiDefault;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FNonBrowsable_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FNonBrowsable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FReplaceable_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FReplaceable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibVarFlags_FImmediateBind_getter",()=>{ return System.Runtime.InteropServices.TypeLibVarFlags.FImmediateBind;}));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibVarFlags_operator_bitOr());
		}

		class system_runtime_interopservices_TypeLibVarFlags_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_TypeLibVarFlags_ctor()
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
					return "system_runtime_interopservices_TypeLibVarFlags_ctor";
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

		class system_runtime_interopservices_TypeLibVarFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_TypeLibVarFlags_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_TypeLibVarFlags_operator_bitOr";
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
				System.Runtime.InteropServices.TypeLibVarFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.TypeLibVarFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.TypeLibVarFlags)argObj.value;
				}

				System.Runtime.InteropServices.TypeLibVarFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.TypeLibVarFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.TypeLibVarFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
