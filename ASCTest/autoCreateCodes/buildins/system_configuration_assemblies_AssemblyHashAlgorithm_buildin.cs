using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_configuration_assemblies_AssemblyHashAlgorithm_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_configuration_assemblies_AssemblyHashAlgorithm_creator", default(System.Configuration.Assemblies.AssemblyHashAlgorithm)));
			bin.regNativeFunction(new system_configuration_assemblies_AssemblyHashAlgorithm_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_configuration_assemblies_AssemblyHashAlgorithm_None_getter",()=>{ return System.Configuration.Assemblies.AssemblyHashAlgorithm.None;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_configuration_assemblies_AssemblyHashAlgorithm_MD5_getter",()=>{ return System.Configuration.Assemblies.AssemblyHashAlgorithm.MD5;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_configuration_assemblies_AssemblyHashAlgorithm_SHA1_getter",()=>{ return System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1;}));
			bin.regNativeFunction(new system_configuration_assemblies_AssemblyHashAlgorithm_operator_bitOr());
		}

		class system_configuration_assemblies_AssemblyHashAlgorithm_ctor : NativeFunctionBase
		{
			public system_configuration_assemblies_AssemblyHashAlgorithm_ctor()
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
					return "system_configuration_assemblies_AssemblyHashAlgorithm_ctor";
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

		class system_configuration_assemblies_AssemblyHashAlgorithm_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_configuration_assemblies_AssemblyHashAlgorithm_operator_bitOr() : base(2)
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
					return "system_configuration_assemblies_AssemblyHashAlgorithm_operator_bitOr";
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
				System.Configuration.Assemblies.AssemblyHashAlgorithm ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Configuration.Assemblies.AssemblyHashAlgorithm);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Configuration.Assemblies.AssemblyHashAlgorithm)argObj.value;
				}

				System.Configuration.Assemblies.AssemblyHashAlgorithm ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Configuration.Assemblies.AssemblyHashAlgorithm);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Configuration.Assemblies.AssemblyHashAlgorithm)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
