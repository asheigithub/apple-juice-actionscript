using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_TypeCode_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_TypeCode_creator", default(System.TypeCode)));
			bin.regNativeFunction(new system_TypeCode_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Empty_getter",()=>{ return System.TypeCode.Empty;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Object_getter",()=>{ return System.TypeCode.Object;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_DBNull_getter",()=>{ return System.TypeCode.DBNull;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Boolean_getter",()=>{ return System.TypeCode.Boolean;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Char_getter",()=>{ return System.TypeCode.Char;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_SByte_getter",()=>{ return System.TypeCode.SByte;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Byte_getter",()=>{ return System.TypeCode.Byte;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Int16_getter",()=>{ return System.TypeCode.Int16;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_UInt16_getter",()=>{ return System.TypeCode.UInt16;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Int32_getter",()=>{ return System.TypeCode.Int32;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_UInt32_getter",()=>{ return System.TypeCode.UInt32;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Int64_getter",()=>{ return System.TypeCode.Int64;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_UInt64_getter",()=>{ return System.TypeCode.UInt64;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Single_getter",()=>{ return System.TypeCode.Single;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Double_getter",()=>{ return System.TypeCode.Double;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_Decimal_getter",()=>{ return System.TypeCode.Decimal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_DateTime_getter",()=>{ return System.TypeCode.DateTime;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_TypeCode_String_getter",()=>{ return System.TypeCode.String;}));
			bin.regNativeFunction(new system_TypeCode_operator_bitOr());
		}

		class system_TypeCode_ctor : NativeFunctionBase
		{
			public system_TypeCode_ctor()
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
					return "system_TypeCode_ctor";
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

		class system_TypeCode_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_TypeCode_operator_bitOr() : base(2)
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
					return "system_TypeCode_operator_bitOr";
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
				System.TypeCode ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.TypeCode);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.TypeCode)argObj.value;
				}

				System.TypeCode ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.TypeCode);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.TypeCode)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
