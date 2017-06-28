using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_diagnostics_symbolstore_SymAddressKind_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_diagnostics_symbolstore_SymAddressKind_creator", default(System.Diagnostics.SymbolStore.SymAddressKind)));
			bin.regNativeFunction(new system_diagnostics_symbolstore_SymAddressKind_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_ILOffset_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.ILOffset;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeRVA_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeRVA;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeRegister_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeRegister;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeRegisterRelative_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeRegisterRelative;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeOffset_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeOffset;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeRegisterRegister_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeRegisterRegister;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeRegisterStack_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeRegisterStack;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeStackRegister_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeStackRegister;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_BitField_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.BitField;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_diagnostics_symbolstore_SymAddressKind_NativeSectionOffset_getter",()=>{ return System.Diagnostics.SymbolStore.SymAddressKind.NativeSectionOffset;}));
			bin.regNativeFunction(new system_diagnostics_symbolstore_SymAddressKind_operator_bitOr());
		}

		class system_diagnostics_symbolstore_SymAddressKind_ctor : NativeFunctionBase
		{
			public system_diagnostics_symbolstore_SymAddressKind_ctor()
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
					return "system_diagnostics_symbolstore_SymAddressKind_ctor";
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

		class system_diagnostics_symbolstore_SymAddressKind_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_diagnostics_symbolstore_SymAddressKind_operator_bitOr() : base(2)
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
					return "system_diagnostics_symbolstore_SymAddressKind_operator_bitOr";
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
				System.Diagnostics.SymbolStore.SymAddressKind ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Diagnostics.SymbolStore.SymAddressKind);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Diagnostics.SymbolStore.SymAddressKind)argObj.value;
				}

				System.Diagnostics.SymbolStore.SymAddressKind ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Diagnostics.SymbolStore.SymAddressKind);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Diagnostics.SymbolStore.SymAddressKind)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
