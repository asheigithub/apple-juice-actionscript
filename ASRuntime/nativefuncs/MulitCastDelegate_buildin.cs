using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
	class MulitCastDelegate_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_Delegate_creator_", default(Delegate)));
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_MulticastDelegate_creator_", default(MulticastDelegate)));


			bin.regNativeFunction(new system_MulticastDelegate_add());
			bin.regNativeFunction(new system_MulticastDelegate_remove());
		}


		class system_MulticastDelegate_add : NativeConstParameterFunction
		{
			public system_MulticastDelegate_add() : base(2)
			{
				para = new List<RunTimeDataType>();
				para.Add(RunTimeDataType.rt_void);
				para.Add(RunTimeDataType.rt_function);

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
					return "_system_MulticastDelegate_combine_";
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
					var cls = stackframe.player.swc.getClassByRunTimeDataType(functionDefine.signature.returnType).staticClass;

					Delegate arg0;
					{
						object _temp;
						if (!stackframe.player.linktypemapper.rtValueToLinkObject(
							argements[0],

							stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
							,
							bin, true, out _temp
							))
						{
							stackframe.throwCastException(token, argements[0].rtType,

								functionDefine.signature.parameters[0].type
								);
							success = false;
							return;
						}
						arg0 = (Delegate)_temp;
					}

					Delegate arg1;
					{
						if (argements[1].rtType == RunTimeDataType.rt_null)
						{
							stackframe.throwArgementException(token, "参数"+functionDefine.signature.parameters[1].name + "不能为null");

							success = false;
							return;
						}

						//***执行隐式转换****
						var implfromfunction = stackframe.player.swc.functions[cls.implicit_from_functionid];
						if (implfromfunction.native_index < 0)
						{
							implfromfunction.native_index = stackframe.player.swc.nativefunctionNameIndex[implfromfunction.native_name];
						}
						IWapperDelegateMaker nativefunc = (IWapperDelegateMaker)stackframe.player.swc.nativefunctions[implfromfunction.native_index];

						var wapper = nativefunc.MakeWapper(argements[1], stackframe.player, cls.instanceClass);
						arg1 = wapper;
					}

					object _result_ = MulticastDelegate.Combine(arg0, arg1);
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;

					//var cls = ((ASBinCode.rtData.rtObjectBase)thisObj).value._class.staticClass;

					////***执行隐式转换****
					//var implfromfunction = stackframe.player.swc.functions[cls.implicit_from_functionid];
					//if (implfromfunction.native_index < 0)
					//{
					//	implfromfunction.native_index = stackframe.player.swc.nativefunctionNameIndex[implfromfunction.native_name];
					//}
					//IWapperDelegateMaker nativefunc = (IWapperDelegateMaker)stackframe.player.swc.nativefunctions[implfromfunction.native_index];

					//var wapper = nativefunc.MakeWapper(argements[0], stackframe.player, cls);

					//Delegate _this =
					//	(MulticastDelegate)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

					//_this= MulticastDelegate.Combine(_this, wapper.action);

					//((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).SetLinkData(_this);

					//returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

					//success = true;

				}
				catch (ASRunTimeException ex)
				{
					success = false;
					stackframe.throwError(token, 9998, ex.ToString());
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


		class system_MulticastDelegate_remove : NativeConstParameterFunction
		{
			public system_MulticastDelegate_remove() : base(2)
			{
				para = new List<RunTimeDataType>();
				para.Add(RunTimeDataType.rt_void);
				para.Add(RunTimeDataType.rt_function);
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
					return "_system_MulticastDelegate_remove_";
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
					var cls = stackframe.player.swc.getClassByRunTimeDataType(functionDefine.signature.returnType).staticClass;

					Delegate arg0;
					{
						object _temp;
						if (!stackframe.player.linktypemapper.rtValueToLinkObject(
							argements[0],

							stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
							,
							bin, true, out _temp
							))
						{
							stackframe.throwCastException(token, argements[0].rtType,

								functionDefine.signature.parameters[0].type
								);
							success = false;
							return;
						}
						arg0 = (Delegate)_temp;
					}

					Delegate arg1;
					{
						if (argements[1].rtType == RunTimeDataType.rt_null)
						{
							stackframe.throwArgementException(token, "参数" + functionDefine.signature.parameters[1].name + "不能为null");

							success = false;
							return;
						}

						//***执行隐式转换****
						var implfromfunction = stackframe.player.swc.functions[cls.implicit_from_functionid];
						if (implfromfunction.native_index < 0)
						{
							implfromfunction.native_index = stackframe.player.swc.nativefunctionNameIndex[implfromfunction.native_name];
						}
						IWapperDelegateMaker nativefunc = (IWapperDelegateMaker)stackframe.player.swc.nativefunctions[implfromfunction.native_index];

						var wapper = nativefunc.MakeWapper(argements[1], stackframe.player, cls.instanceClass);
						arg1 = wapper;
					}

					object _result_ = MulticastDelegate.Remove(arg0, arg1);
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;

				}
				catch (ASRunTimeException ex)
				{
					success = false;
					stackframe.throwError(token, 9998, ex.ToString());
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


	}
}
