using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_resources_UltimateResourceFallbackLocation_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_resources_UltimateResourceFallbackLocation_creator", default(System.Resources.UltimateResourceFallbackLocation)));
			bin.regNativeFunction(new system_resources_UltimateResourceFallbackLocation_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_resources_UltimateResourceFallbackLocation_MainAssembly_getter",()=>{ return System.Resources.UltimateResourceFallbackLocation.MainAssembly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_resources_UltimateResourceFallbackLocation_Satellite_getter",()=>{ return System.Resources.UltimateResourceFallbackLocation.Satellite;}));
			bin.regNativeFunction(new system_resources_UltimateResourceFallbackLocation_operator_bitOr());
		}

		class system_resources_UltimateResourceFallbackLocation_ctor : NativeFunctionBase
		{
			public system_resources_UltimateResourceFallbackLocation_ctor()
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
					return "system_resources_UltimateResourceFallbackLocation_ctor";
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

		class system_resources_UltimateResourceFallbackLocation_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_resources_UltimateResourceFallbackLocation_operator_bitOr() : base(2)
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
					return "system_resources_UltimateResourceFallbackLocation_operator_bitOr";
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
				System.Resources.UltimateResourceFallbackLocation ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Resources.UltimateResourceFallbackLocation);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Resources.UltimateResourceFallbackLocation)argObj.value;
				}

				System.Resources.UltimateResourceFallbackLocation ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Resources.UltimateResourceFallbackLocation);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Resources.UltimateResourceFallbackLocation)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}