using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_ConsoleColor_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_ConsoleColor_creator", default(System.ConsoleColor)));
			bin.regNativeFunction(new system_ConsoleColor_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Black_getter",()=>{ return System.ConsoleColor.Black;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_DarkBlue_getter",()=>{ return System.ConsoleColor.DarkBlue;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_DarkGreen_getter",()=>{ return System.ConsoleColor.DarkGreen;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_DarkCyan_getter",()=>{ return System.ConsoleColor.DarkCyan;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_DarkRed_getter",()=>{ return System.ConsoleColor.DarkRed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_DarkMagenta_getter",()=>{ return System.ConsoleColor.DarkMagenta;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_DarkYellow_getter",()=>{ return System.ConsoleColor.DarkYellow;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Gray_getter",()=>{ return System.ConsoleColor.Gray;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_DarkGray_getter",()=>{ return System.ConsoleColor.DarkGray;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Blue_getter",()=>{ return System.ConsoleColor.Blue;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Green_getter",()=>{ return System.ConsoleColor.Green;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Cyan_getter",()=>{ return System.ConsoleColor.Cyan;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Red_getter",()=>{ return System.ConsoleColor.Red;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Magenta_getter",()=>{ return System.ConsoleColor.Magenta;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_Yellow_getter",()=>{ return System.ConsoleColor.Yellow;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleColor_White_getter",()=>{ return System.ConsoleColor.White;}));
			bin.regNativeFunction(new system_ConsoleColor_operator_bitOr());
		}

		class system_ConsoleColor_ctor : NativeFunctionBase
		{
			public system_ConsoleColor_ctor()
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
					return "system_ConsoleColor_ctor";
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

		class system_ConsoleColor_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_ConsoleColor_operator_bitOr() : base(2)
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
					return "system_ConsoleColor_operator_bitOr";
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
				System.ConsoleColor ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.ConsoleColor);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.ConsoleColor)argObj.value;
				}

				System.ConsoleColor ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.ConsoleColor);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.ConsoleColor)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
