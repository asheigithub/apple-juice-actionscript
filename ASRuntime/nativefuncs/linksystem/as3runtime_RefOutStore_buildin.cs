using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASRuntime.nativefuncs.linksystem
{
	public class RefOutStore
	{
		private Dictionary<string, object> dictValuesByName = new Dictionary<string, object>();

		public void SetValue(string name, object value)
		{
			dictValuesByName.Add(name, value);
		}

		public object GetValue(string parameterName)
		{
			return dictValuesByName[parameterName];
		}

		public void Clear()
		{
			dictValuesByName.Clear();
		}

	}
	class as3runtime_RefOutStore_buildin
	{

		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("as3runtime_RefOutStore_creator", default(RefOutStore)));
			bin.regNativeFunction(new as3runtime_RefOutStore_ctor());
			bin.regNativeFunction(new as3runtime_RefOutStore_setValue());
			bin.regNativeFunction(new as3runtime_RefOutStore_getValue());
		}

		class as3runtime_RefOutStore_ctor : NativeConstParameterFunction
		{
			public as3runtime_RefOutStore_ctor() : base(0)
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
					return "as3runtime_RefOutStore_ctor";
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

				try
				{

					((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = new RefOutStore();
					returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

					success = true;
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
		}

		class as3runtime_RefOutStore_setValue : NativeConstParameterFunction, IMethodGetter
		{
			public as3runtime_RefOutStore_setValue() : base(2)
			{
				para = new List<RunTimeDataType>();
				para.Add(RunTimeDataType.rt_string);
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
					return "as3runtime_RefOutStore_setValue";
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
					return RunTimeDataType.fun_void;
				}
			}

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{

				RefOutStore _this =
					(RefOutStore)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{
					string arg0 = TypeConverter.ConvertToString(argements[0], stackframe, token);
					System.Object arg1;
					{
						object _temp;
						if (!stackframe.player.linktypemapper.rtValueToLinkObject(
							argements[1],

							stackframe.player.linktypemapper.getLinkType(argements[1].rtType)
							,
							bin, true, out _temp
							))
						{
							stackframe.throwCastException(token, argements[1].rtType,

								functionDefine.signature.parameters[1].type
								);
							success = false;
							return;
						}
						arg1 = (System.Object)_temp;
					}

					_this.SetValue((System.String)arg0, (System.Object)arg1)
					;
					returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
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
			public System.Reflection.MethodInfo GetMethodInfo(ASBinCode.rtti.FunctionDefine functionDefine, ASBinCode.CSWC swc, ASRuntime.Player player)
			{
				if (method == null)
				{
					method = typeof(RefOutStore).GetMethod("SetValue", new Type[] { typeof(System.String), typeof(System.Object) }); ;
				}
				return method;
			}

		}

		class as3runtime_RefOutStore_getValue : NativeConstParameterFunction, IMethodGetter
		{
			public as3runtime_RefOutStore_getValue() : base(1)
			{
				para = new List<RunTimeDataType>();
				para.Add(RunTimeDataType.rt_string);

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
					return "as3runtime_RefOutStore_getValue";
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

				RefOutStore _this =
					(RefOutStore)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{
					string arg0 = TypeConverter.ConvertToString(argements[0], stackframe, token);

					object _result_ = _this.GetValue((System.String)arg0)
					;
					//if (_result_ == null)
					{
						stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					}
					//else
					//{
					//	stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_,
					//		stackframe.player.linktypemapper.getRuntimeDataType(_result_.GetType())
					//		, returnSlot, bin, stackframe.player);
					//}
					success = true;
				}
				catch (KeyNotFoundException kc)
				{
					success = false;
					stackframe.throwAneException(token, "out or ref parameter not found." + kc.Message);
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
			public System.Reflection.MethodInfo GetMethodInfo(ASBinCode.rtti.FunctionDefine functionDefine, ASBinCode.CSWC swc, ASRuntime.Player player)
			{
				if (method == null)
				{
					method = typeof(RefOutStore).GetMethod("GetValue", new Type[] { typeof(System.String) }); ;
				}
				return method;
			}

		}

	}
}
