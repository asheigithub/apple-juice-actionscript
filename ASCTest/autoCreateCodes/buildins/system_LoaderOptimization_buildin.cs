using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_LoaderOptimization_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_LoaderOptimization_creator", default(System.LoaderOptimization)));
			bin.regNativeFunction(new system_LoaderOptimization_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_LoaderOptimization_NotSpecified_getter",()=>{ return System.LoaderOptimization.NotSpecified;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_LoaderOptimization_SingleDomain_getter",()=>{ return System.LoaderOptimization.SingleDomain;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_LoaderOptimization_MultiDomain_getter",()=>{ return System.LoaderOptimization.MultiDomain;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_LoaderOptimization_MultiDomainHost_getter",()=>{ return System.LoaderOptimization.MultiDomainHost;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_LoaderOptimization_DomainMask_getter",()=>{ return System.LoaderOptimization.DomainMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_LoaderOptimization_DisallowBindings_getter",()=>{ return System.LoaderOptimization.DisallowBindings;}));
			bin.regNativeFunction(new system_LoaderOptimization_operator_bitOr());
		}

		class system_LoaderOptimization_ctor : NativeFunctionBase
		{
			public system_LoaderOptimization_ctor()
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
					return "system_LoaderOptimization_ctor";
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

		class system_LoaderOptimization_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_LoaderOptimization_operator_bitOr() : base(2)
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
					return "system_LoaderOptimization_operator_bitOr";
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
				System.LoaderOptimization ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.LoaderOptimization);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.LoaderOptimization)argObj.value;
				}

				System.LoaderOptimization ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.LoaderOptimization);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.LoaderOptimization)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
