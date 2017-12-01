using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_remoting_activation_ActivatorLevel_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_remoting_activation_ActivatorLevel_creator", default(System.Runtime.Remoting.Activation.ActivatorLevel)));
			bin.regNativeFunction(new system_runtime_remoting_activation_ActivatorLevel_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_remoting_activation_ActivatorLevel_Construction_getter",()=>{ return System.Runtime.Remoting.Activation.ActivatorLevel.Construction;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_remoting_activation_ActivatorLevel_Context_getter",()=>{ return System.Runtime.Remoting.Activation.ActivatorLevel.Context;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_remoting_activation_ActivatorLevel_AppDomain_getter",()=>{ return System.Runtime.Remoting.Activation.ActivatorLevel.AppDomain;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_remoting_activation_ActivatorLevel_Process_getter",()=>{ return System.Runtime.Remoting.Activation.ActivatorLevel.Process;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_remoting_activation_ActivatorLevel_Machine_getter",()=>{ return System.Runtime.Remoting.Activation.ActivatorLevel.Machine;}));
			bin.regNativeFunction(new system_runtime_remoting_activation_ActivatorLevel_operator_bitOr());
		}

		class system_runtime_remoting_activation_ActivatorLevel_ctor : NativeFunctionBase
		{
			public system_runtime_remoting_activation_ActivatorLevel_ctor()
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
					return "system_runtime_remoting_activation_ActivatorLevel_ctor";
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

		class system_runtime_remoting_activation_ActivatorLevel_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_remoting_activation_ActivatorLevel_operator_bitOr() : base(2)
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
					return "system_runtime_remoting_activation_ActivatorLevel_operator_bitOr";
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
				System.Runtime.Remoting.Activation.ActivatorLevel ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.Remoting.Activation.ActivatorLevel);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.Remoting.Activation.ActivatorLevel)argObj.value;
				}

				System.Runtime.Remoting.Activation.ActivatorLevel ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.Remoting.Activation.ActivatorLevel);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.Remoting.Activation.ActivatorLevel)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
