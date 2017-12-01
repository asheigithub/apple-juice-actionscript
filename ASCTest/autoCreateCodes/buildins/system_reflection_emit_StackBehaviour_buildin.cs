using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_reflection_emit_StackBehaviour_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_reflection_emit_StackBehaviour_creator", default(System.Reflection.Emit.StackBehaviour)));
			bin.regNativeFunction(new system_reflection_emit_StackBehaviour_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pop0_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pop0;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pop1_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pop1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pop1_pop1_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pop1_pop1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popi_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popi;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popi_pop1_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popi_pop1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popi_popi_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popi_popi;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popi_popi8_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popi_popi8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popi_popi_popi_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popi_popi_popi;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popi_popr4_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popi_popr4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popi_popr8_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popi_popr8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_pop1_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_pop1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_popi_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_popi;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_popi_popi_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_popi_popi;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_popi_popi8_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_popi_popi8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_popi_popr4_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_popi_popr4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_popi_popr8_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_popi_popr8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_popi_popref_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_popi_popref;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Push0_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Push0;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Push1_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Push1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Push1_push1_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Push1_push1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pushi_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pushi;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pushi8_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pushi8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pushr4_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pushr4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pushr8_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pushr8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Pushref_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Pushref;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Varpop_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Varpop;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Varpush_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Varpush;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_reflection_emit_StackBehaviour_Popref_popi_pop1_getter",()=>{ return System.Reflection.Emit.StackBehaviour.Popref_popi_pop1;}));
			bin.regNativeFunction(new system_reflection_emit_StackBehaviour_operator_bitOr());
		}

		class system_reflection_emit_StackBehaviour_ctor : NativeFunctionBase
		{
			public system_reflection_emit_StackBehaviour_ctor()
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
					return "system_reflection_emit_StackBehaviour_ctor";
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

		class system_reflection_emit_StackBehaviour_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_reflection_emit_StackBehaviour_operator_bitOr() : base(2)
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
					return "system_reflection_emit_StackBehaviour_operator_bitOr";
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
				System.Reflection.Emit.StackBehaviour ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Reflection.Emit.StackBehaviour);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Reflection.Emit.StackBehaviour)argObj.value;
				}

				System.Reflection.Emit.StackBehaviour ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Reflection.Emit.StackBehaviour);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Reflection.Emit.StackBehaviour)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
