using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ASRuntime.nativefuncs.linksystem
{
	class asruntime_nativefuncs_linksystem_Iterator_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("asruntime_nativefuncs_linksystem_Iterator_creator", default(ASRuntime.nativefuncs.linksystem.Iterator)));
			bin.regNativeFunction(new asruntime_nativefuncs_linksystem_Iterator_ctor());
			bin.regNativeFunction(new static_asruntime_nativefuncs_linksystem_Iterator_op_Explicit());
		}

		class asruntime_nativefuncs_linksystem_Iterator_ctor : NativeConstParameterFunction
		{
			public asruntime_nativefuncs_linksystem_Iterator_ctor() : base(1)
			{
				para = new List<RunTimeDataType>();
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
					return "asruntime_nativefuncs_linksystem_Iterator_ctor";
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
					ASBinCode.RunTimeValueBase arg0;
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
						arg0 = (ASBinCode.RunTimeValueBase)_temp;
					}

					((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value =
						new ASRuntime.nativefuncs.linksystem.Iterator((ASBinCode.RunTimeValueBase)arg0, stackframe.player);
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

		class static_asruntime_nativefuncs_linksystem_Iterator_op_Explicit : NativeConstParameterFunction
		{
			public static_asruntime_nativefuncs_linksystem_Iterator_op_Explicit() : base(1)
			{
				para = new List<RunTimeDataType>();
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
					return "static_asruntime_nativefuncs_linksystem_Iterator_op_Explicit";
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
					ASBinCode.RunTimeValueBase arg0;
					{
						object _temp;
						if (!stackframe.player.linktypemapper.rtValueToLinkObject(
							argements[0],

							stackframe.player.linktypemapper.getLinkType(functionDefine.signature.parameters[0].type)
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
						arg0 = (ASBinCode.RunTimeValueBase)_temp;
					}

					//object _result_ = (ASRuntime.nativefuncs.linksystem.Iterator)arg0;

					if (arg0 == null)
					{
						stackframe.player.linktypemapper.storeLinkObject_ToSlot(null, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

					}
					else
					{
						stackframe.player.linktypemapper.storeLinkObject_ToSlot(
							new ASRuntime.nativefuncs.linksystem.Iterator(arg0, stackframe.player)

							, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

					}


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
		}

	}

}
