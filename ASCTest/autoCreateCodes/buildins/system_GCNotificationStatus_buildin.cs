using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_GCNotificationStatus_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_GCNotificationStatus_creator", default(System.GCNotificationStatus)));
			bin.regNativeFunction(new system_GCNotificationStatus_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_GCNotificationStatus_Succeeded_getter",()=>{ return System.GCNotificationStatus.Succeeded;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_GCNotificationStatus_Failed_getter",()=>{ return System.GCNotificationStatus.Failed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_GCNotificationStatus_Canceled_getter",()=>{ return System.GCNotificationStatus.Canceled;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_GCNotificationStatus_Timeout_getter",()=>{ return System.GCNotificationStatus.Timeout;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_GCNotificationStatus_NotApplicable_getter",()=>{ return System.GCNotificationStatus.NotApplicable;}));
			bin.regNativeFunction(new system_GCNotificationStatus_operator_bitOr());
		}

		class system_GCNotificationStatus_ctor : NativeFunctionBase
		{
			public system_GCNotificationStatus_ctor()
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
					return "system_GCNotificationStatus_ctor";
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

		class system_GCNotificationStatus_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_GCNotificationStatus_operator_bitOr() : base(2)
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
					return "system_GCNotificationStatus_operator_bitOr";
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
				System.GCNotificationStatus ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.GCNotificationStatus);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.GCNotificationStatus)argObj.value;
				}

				System.GCNotificationStatus ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.GCNotificationStatus);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.GCNotificationStatus)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
