using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_VarEnum_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_VarEnum_creator", default(System.Runtime.InteropServices.VarEnum)));
			bin.regNativeFunction(new system_runtime_interopservices_VarEnum_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_EMPTY_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_EMPTY;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_NULL_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_NULL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_I2_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_I2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_I4_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_I4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_R4_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_R4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_R8_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_R8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_CY_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_CY;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_DATE_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_DATE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_BSTR_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_BSTR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_DISPATCH_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_DISPATCH;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_ERROR_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_ERROR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_BOOL_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_BOOL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_VARIANT_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_VARIANT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_UNKNOWN_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_UNKNOWN;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_DECIMAL_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_DECIMAL;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_I1_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_I1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_UI1_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_UI1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_UI2_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_UI2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_UI4_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_UI4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_I8_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_I8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_UI8_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_UI8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_INT_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_INT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_UINT_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_UINT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_VOID_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_VOID;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_HRESULT_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_HRESULT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_PTR_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_PTR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_SAFEARRAY_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_SAFEARRAY;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_CARRAY_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_CARRAY;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_USERDEFINED_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_USERDEFINED;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_LPSTR_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_LPSTR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_LPWSTR_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_LPWSTR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_RECORD_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_RECORD;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_FILETIME_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_FILETIME;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_BLOB_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_BLOB;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_STREAM_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_STREAM;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_STORAGE_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_STORAGE;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_STREAMED_OBJECT_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_STREAMED_OBJECT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_STORED_OBJECT_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_STORED_OBJECT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_BLOB_OBJECT_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_BLOB_OBJECT;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_CF_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_CF;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_CLSID_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_CLSID;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_VECTOR_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_VECTOR;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_ARRAY_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_ARRAY;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_VarEnum_VT_BYREF_getter",()=>{ return System.Runtime.InteropServices.VarEnum.VT_BYREF;}));
			bin.regNativeFunction(new system_runtime_interopservices_VarEnum_operator_bitOr());
		}

		class system_runtime_interopservices_VarEnum_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_VarEnum_ctor()
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
					return "system_runtime_interopservices_VarEnum_ctor";
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

		class system_runtime_interopservices_VarEnum_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_VarEnum_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_VarEnum_operator_bitOr";
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
				System.Runtime.InteropServices.VarEnum ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.VarEnum);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.VarEnum)argObj.value;
				}

				System.Runtime.InteropServices.VarEnum ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.VarEnum);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.VarEnum)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
