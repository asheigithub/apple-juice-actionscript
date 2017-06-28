using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_DayOfWeek_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_DayOfWeek_creator", default(System.DayOfWeek)));
			bin.regNativeFunction(new system_DayOfWeek_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_DayOfWeek_Sunday_getter",()=>{ return System.DayOfWeek.Sunday;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_DayOfWeek_Monday_getter",()=>{ return System.DayOfWeek.Monday;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_DayOfWeek_Tuesday_getter",()=>{ return System.DayOfWeek.Tuesday;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_DayOfWeek_Wednesday_getter",()=>{ return System.DayOfWeek.Wednesday;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_DayOfWeek_Thursday_getter",()=>{ return System.DayOfWeek.Thursday;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_DayOfWeek_Friday_getter",()=>{ return System.DayOfWeek.Friday;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_DayOfWeek_Saturday_getter",()=>{ return System.DayOfWeek.Saturday;}));
			bin.regNativeFunction(new system_DayOfWeek_operator_bitOr());
		}

		class system_DayOfWeek_ctor : NativeFunctionBase
		{
			public system_DayOfWeek_ctor()
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
					return "system_DayOfWeek_ctor";
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

		class system_DayOfWeek_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_DayOfWeek_operator_bitOr() : base(2)
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
					return "system_DayOfWeek_operator_bitOr";
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
				System.DayOfWeek ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.DayOfWeek);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.DayOfWeek)argObj.value;
				}

				System.DayOfWeek ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.DayOfWeek);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.DayOfWeek)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
