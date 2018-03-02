using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASRuntime.nativefuncs.linksystem
{
	class system_ICloneable_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_ICloneable_creator", default(System.ICloneable)));
			bin.regNativeFunction(new system_ICloneable_clone());
		}

		class system_ICloneable_clone : NativeConstParameterFunction,IMethodGetter
		{
			public system_ICloneable_clone() : base(0)
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
					return "system_ICloneable_clone";
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

				System.ICloneable _this =
					(System.ICloneable)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					object _result_ = _this.Clone()
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;
				}
				catch (ASRunTimeException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}

			private System.Reflection.MethodInfo method;
			public System.Reflection.MethodInfo GetMethodInfo()
			{
				if (method == null)
				{
					method=typeof(System.ICloneable).GetMethod("Clone",Type.EmptyTypes);;
				}
				return method;
			}

		}

	}
}
