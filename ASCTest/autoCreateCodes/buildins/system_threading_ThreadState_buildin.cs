using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_threading_ThreadState_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_threading_ThreadState_creator", default(System.Threading.ThreadState)));
			bin.regNativeFunction(new system_threading_ThreadState_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_Running_getter",()=>{ return System.Threading.ThreadState.Running;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_StopRequested_getter",()=>{ return System.Threading.ThreadState.StopRequested;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_SuspendRequested_getter",()=>{ return System.Threading.ThreadState.SuspendRequested;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_Background_getter",()=>{ return System.Threading.ThreadState.Background;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_Unstarted_getter",()=>{ return System.Threading.ThreadState.Unstarted;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_Stopped_getter",()=>{ return System.Threading.ThreadState.Stopped;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_WaitSleepJoin_getter",()=>{ return System.Threading.ThreadState.WaitSleepJoin;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_Suspended_getter",()=>{ return System.Threading.ThreadState.Suspended;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_AbortRequested_getter",()=>{ return System.Threading.ThreadState.AbortRequested;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_threading_ThreadState_Aborted_getter",()=>{ return System.Threading.ThreadState.Aborted;}));
			bin.regNativeFunction(new system_threading_ThreadState_operator_bitOr());
		}

		class system_threading_ThreadState_ctor : NativeFunctionBase
		{
			public system_threading_ThreadState_ctor()
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
					return "system_threading_ThreadState_ctor";
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

		class system_threading_ThreadState_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_threading_ThreadState_operator_bitOr() : base(2)
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
					return "system_threading_ThreadState_operator_bitOr";
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
				System.Threading.ThreadState ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Threading.ThreadState);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Threading.ThreadState)argObj.value;
				}

				System.Threading.ThreadState ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Threading.ThreadState);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Threading.ThreadState)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
