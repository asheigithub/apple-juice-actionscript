using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_UnmanagedType_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_UnmanagedType_creator", default(System.Runtime.InteropServices.UnmanagedType)));
			bin.regNativeFunction(new system_runtime_interopservices_UnmanagedType_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_Bool_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.Bool;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_I1_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.I1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_U1_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.U1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_I2_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.I2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_U2_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.U2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_I4_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.I4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_U4_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.U4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_I8_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.I8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_U8_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.U8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_R4_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.R4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_R8_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.R8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_Currency_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.Currency;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_BStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.BStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_LPStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.LPStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_LPWStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.LPWStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_LPTStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.LPTStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_ByValTStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.ByValTStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_IUnknown_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.IUnknown;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_IDispatch_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.IDispatch;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_Struct_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.Struct;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_Interface_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.Interface;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_SafeArray_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.SafeArray;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_ByValArray_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.ByValArray;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_SysInt_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.SysInt;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_SysUInt_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.SysUInt;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_VBByRefStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.VBByRefStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_AnsiBStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.AnsiBStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_TBStr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.TBStr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_VariantBool_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.VariantBool;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_FunctionPtr_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.FunctionPtr;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_AsAny_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.AsAny;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_LPArray_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.LPArray;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_LPStruct_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.LPStruct;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_CustomMarshaler_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.CustomMarshaler;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_UnmanagedType_Error_getter",()=>{ return System.Runtime.InteropServices.UnmanagedType.Error;}));
			bin.regNativeFunction(new system_runtime_interopservices_UnmanagedType_operator_bitOr());
		}

		class system_runtime_interopservices_UnmanagedType_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_UnmanagedType_ctor()
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
					return "system_runtime_interopservices_UnmanagedType_ctor";
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

		class system_runtime_interopservices_UnmanagedType_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_UnmanagedType_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_UnmanagedType_operator_bitOr";
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
				System.Runtime.InteropServices.UnmanagedType ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.UnmanagedType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.UnmanagedType)argObj.value;
				}

				System.Runtime.InteropServices.UnmanagedType ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.UnmanagedType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.UnmanagedType)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
