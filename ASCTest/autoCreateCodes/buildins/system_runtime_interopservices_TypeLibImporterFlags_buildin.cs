using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_interopservices_TypeLibImporterFlags_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_interopservices_TypeLibImporterFlags_creator", default(System.Runtime.InteropServices.TypeLibImporterFlags)));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibImporterFlags_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_None_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_PrimaryInteropAssembly_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.PrimaryInteropAssembly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_UnsafeInterfaces_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.UnsafeInterfaces;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_SafeArrayAsSystemArray_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.SafeArrayAsSystemArray;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_TransformDispRetVals_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.TransformDispRetVals;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_PreventClassMembers_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.PreventClassMembers;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_SerializableValueClasses_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.SerializableValueClasses;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_ImportAsX86_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.ImportAsX86;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_ImportAsX64_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.ImportAsX64;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_ImportAsItanium_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.ImportAsItanium;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_ImportAsAgnostic_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.ImportAsAgnostic;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_interopservices_TypeLibImporterFlags_ReflectionOnlyLoading_getter",()=>{ return System.Runtime.InteropServices.TypeLibImporterFlags.ReflectionOnlyLoading;}));
			bin.regNativeFunction(new system_runtime_interopservices_TypeLibImporterFlags_operator_bitOr());
		}

		class system_runtime_interopservices_TypeLibImporterFlags_ctor : NativeFunctionBase
		{
			public system_runtime_interopservices_TypeLibImporterFlags_ctor()
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
					return "system_runtime_interopservices_TypeLibImporterFlags_ctor";
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

		class system_runtime_interopservices_TypeLibImporterFlags_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_interopservices_TypeLibImporterFlags_operator_bitOr() : base(2)
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
					return "system_runtime_interopservices_TypeLibImporterFlags_operator_bitOr";
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
				System.Runtime.InteropServices.TypeLibImporterFlags ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.InteropServices.TypeLibImporterFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.InteropServices.TypeLibImporterFlags)argObj.value;
				}

				System.Runtime.InteropServices.TypeLibImporterFlags ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.InteropServices.TypeLibImporterFlags);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.InteropServices.TypeLibImporterFlags)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
