using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_runtime_constrainedexecution_Consistency_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_runtime_constrainedexecution_Consistency_creator", default(System.Runtime.ConstrainedExecution.Consistency)));
			bin.regNativeFunction(new system_runtime_constrainedexecution_Consistency_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_constrainedexecution_Consistency_MayCorruptProcess_getter",()=>{ return System.Runtime.ConstrainedExecution.Consistency.MayCorruptProcess;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_constrainedexecution_Consistency_MayCorruptAppDomain_getter",()=>{ return System.Runtime.ConstrainedExecution.Consistency.MayCorruptAppDomain;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_constrainedexecution_Consistency_MayCorruptInstance_getter",()=>{ return System.Runtime.ConstrainedExecution.Consistency.MayCorruptInstance;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_runtime_constrainedexecution_Consistency_WillNotCorruptState_getter",()=>{ return System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState;}));
			bin.regNativeFunction(new system_runtime_constrainedexecution_Consistency_operator_bitOr());
		}

		class system_runtime_constrainedexecution_Consistency_ctor : NativeFunctionBase
		{
			public system_runtime_constrainedexecution_Consistency_ctor()
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
					return "system_runtime_constrainedexecution_Consistency_ctor";
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

		class system_runtime_constrainedexecution_Consistency_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_runtime_constrainedexecution_Consistency_operator_bitOr() : base(2)
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
					return "system_runtime_constrainedexecution_Consistency_operator_bitOr";
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
				System.Runtime.ConstrainedExecution.Consistency ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Runtime.ConstrainedExecution.Consistency);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Runtime.ConstrainedExecution.Consistency)argObj.value;
				}

				System.Runtime.ConstrainedExecution.Consistency ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Runtime.ConstrainedExecution.Consistency);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Runtime.ConstrainedExecution.Consistency)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
