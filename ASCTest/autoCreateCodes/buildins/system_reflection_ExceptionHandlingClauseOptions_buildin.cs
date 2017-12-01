using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_ExceptionHandlingClauseOptions_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_ExceptionHandlingClauseOptions_creator", default(System.Reflection.ExceptionHandlingClauseOptions)));
			bin.regNativeFunction(new system_reflection_ExceptionHandlingClauseOptions_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ExceptionHandlingClauseOptions_Clause_getter",()=>{ return System.Reflection.ExceptionHandlingClauseOptions.Clause;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ExceptionHandlingClauseOptions_Filter_getter",()=>{ return System.Reflection.ExceptionHandlingClauseOptions.Filter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ExceptionHandlingClauseOptions_Finally_getter",()=>{ return System.Reflection.ExceptionHandlingClauseOptions.Finally;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_ExceptionHandlingClauseOptions_Fault_getter",()=>{ return System.Reflection.ExceptionHandlingClauseOptions.Fault;}));
			bin.regNativeFunction(new system_reflection_ExceptionHandlingClauseOptions_operator_bitOr());
		}

		class system_reflection_ExceptionHandlingClauseOptions_ctor : NativeFunctionBase
		{
			public system_reflection_ExceptionHandlingClauseOptions_ctor()
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
					return "system_reflection_ExceptionHandlingClauseOptions_ctor";
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

		class system_reflection_ExceptionHandlingClauseOptions_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_ExceptionHandlingClauseOptions_operator_bitOr() : base(2)
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
					return "system_reflection_ExceptionHandlingClauseOptions_operator_bitOr";
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
				System.Reflection.ExceptionHandlingClauseOptions ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.ExceptionHandlingClauseOptions);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Reflection.ExceptionHandlingClauseOptions)argObj.value;
				}

				System.Reflection.ExceptionHandlingClauseOptions ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.ExceptionHandlingClauseOptions);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Reflection.ExceptionHandlingClauseOptions)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
