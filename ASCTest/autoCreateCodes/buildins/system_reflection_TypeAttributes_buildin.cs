using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_TypeAttributes_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_TypeAttributes_creator", default(System.Reflection.TypeAttributes)));
			bin.regNativeFunction(new system_reflection_TypeAttributes_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_VisibilityMask_getter",()=>{ return System.Reflection.TypeAttributes.VisibilityMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_NotPublic_getter",()=>{ return System.Reflection.TypeAttributes.NotPublic;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_Public_getter",()=>{ return System.Reflection.TypeAttributes.Public;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_NestedPublic_getter",()=>{ return System.Reflection.TypeAttributes.NestedPublic;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_NestedPrivate_getter",()=>{ return System.Reflection.TypeAttributes.NestedPrivate;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_NestedFamily_getter",()=>{ return System.Reflection.TypeAttributes.NestedFamily;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_NestedAssembly_getter",()=>{ return System.Reflection.TypeAttributes.NestedAssembly;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_NestedFamANDAssem_getter",()=>{ return System.Reflection.TypeAttributes.NestedFamANDAssem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_NestedFamORAssem_getter",()=>{ return System.Reflection.TypeAttributes.NestedFamORAssem;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_LayoutMask_getter",()=>{ return System.Reflection.TypeAttributes.LayoutMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_AutoLayout_getter",()=>{ return System.Reflection.TypeAttributes.AutoLayout;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_SequentialLayout_getter",()=>{ return System.Reflection.TypeAttributes.SequentialLayout;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_ExplicitLayout_getter",()=>{ return System.Reflection.TypeAttributes.ExplicitLayout;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_ClassSemanticsMask_getter",()=>{ return System.Reflection.TypeAttributes.ClassSemanticsMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_Class_getter",()=>{ return System.Reflection.TypeAttributes.Class;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_Interface_getter",()=>{ return System.Reflection.TypeAttributes.Interface;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_Abstract_getter",()=>{ return System.Reflection.TypeAttributes.Abstract;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_Sealed_getter",()=>{ return System.Reflection.TypeAttributes.Sealed;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_SpecialName_getter",()=>{ return System.Reflection.TypeAttributes.SpecialName;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_Import_getter",()=>{ return System.Reflection.TypeAttributes.Import;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_Serializable_getter",()=>{ return System.Reflection.TypeAttributes.Serializable;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_StringFormatMask_getter",()=>{ return System.Reflection.TypeAttributes.StringFormatMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_AnsiClass_getter",()=>{ return System.Reflection.TypeAttributes.AnsiClass;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_UnicodeClass_getter",()=>{ return System.Reflection.TypeAttributes.UnicodeClass;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_AutoClass_getter",()=>{ return System.Reflection.TypeAttributes.AutoClass;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_CustomFormatClass_getter",()=>{ return System.Reflection.TypeAttributes.CustomFormatClass;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_CustomFormatMask_getter",()=>{ return System.Reflection.TypeAttributes.CustomFormatMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_BeforeFieldInit_getter",()=>{ return System.Reflection.TypeAttributes.BeforeFieldInit;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_ReservedMask_getter",()=>{ return System.Reflection.TypeAttributes.ReservedMask;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_RTSpecialName_getter",()=>{ return System.Reflection.TypeAttributes.RTSpecialName;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_TypeAttributes_HasSecurity_getter",()=>{ return System.Reflection.TypeAttributes.HasSecurity;}));
			bin.regNativeFunction(new system_reflection_TypeAttributes_operator_bitOr());
		}

		class system_reflection_TypeAttributes_ctor : NativeFunctionBase
		{
			public system_reflection_TypeAttributes_ctor()
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
					return "system_reflection_TypeAttributes_ctor";
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

		class system_reflection_TypeAttributes_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_TypeAttributes_operator_bitOr() : base(2)
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
					return "system_reflection_TypeAttributes_operator_bitOr";
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
				System.Reflection.TypeAttributes ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.TypeAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[0]).value;
					ts1 = (System.Reflection.TypeAttributes)argObj.value;
				}

				System.Reflection.TypeAttributes ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.TypeAttributes);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObject)argements[1]).value;
					ts2 = (System.Reflection.TypeAttributes)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
