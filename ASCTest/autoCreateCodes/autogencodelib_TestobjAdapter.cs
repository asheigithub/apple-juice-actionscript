using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;

namespace ASCTest.autoCreateCodes
{
	public class autogencodelib_TestobjAdapter :AutoGenCodeLib.Testobj ,ASRuntime.ICrossExtendAdapter
	{
		

		public ASBinCode.rtti.Class AS3Class { get { return typeclass; } }

		public ASBinCode.rtData.rtObjectBase AS3Object { get { return bindAS3Object; } }

		private Player player;
		private Class typeclass;
		private ASBinCode.rtData.rtObjectBase bindAS3Object;

		public void SetAS3RuntimeEnvironment(Player player, Class typeclass, ASBinCode.rtData.rtObjectBase bindAS3Object)
		{
			this.player = player;
			this.typeclass = typeclass;
			this.bindAS3Object = bindAS3Object;
		}

		private ASBinCode.rtData.rtFunction _testType;
		private int _testTypeFunctionId=-1;
		public override Type TestType(Type type)
		{

			if (_testType == null)
				_testType = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, "testType");
			if (_testTypeFunctionId == -1)
			{
				var c = typeclass.getBaseLinkSystemClass();
				_testTypeFunctionId = ((ClassMethodGetter)c.classMembers.FindByName("testType").bindField).functionId;
			}


			if (_testType != null &&
				(player == null || (player != null && NativeConstParameterFunction.checkToken(
				new NativeConstParameterFunction.ExecuteToken(
				player.ExecuteToken.tokenid, _testTypeFunctionId
				)
				))
				)
				)
			{
				return base.TestType(type);
			}
			else
			{

				var b = player.InvokeFunction(_testType, 1, type, null, null, null, null, null);

				return (Type)b;


			}
			
		}




		class static_autogencodelib_Testobj_createTestObj : NativeConstParameterFunction
		{
			public static_autogencodelib_Testobj_createTestObj() : base(1)
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
					return "static_autogencodelib_Testobj_createTestObj";
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
					System.Type arg0;
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
						arg0 = (System.Type)_temp;
					}

					object _result_ = AutoGenCodeLib.Testobj.CreateTestObj((System.Type)arg0)
					;

					ASBinCode.rtti.Class as3class = ((ASBinCode.rtData.rtObjectBase)argements[0]).value._class;
					stackframe.player.MakeICrossExtendAdapterEnvironment((ICrossExtendAdapter)_result_, as3class);


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
		}

	}


	class autogencodelib_TestobjAdapter_ctor : NativeConstParameterFunction ,ICrossExtendAdapterCreator
	{
		public autogencodelib_TestobjAdapter_ctor() : base(0)
		{
			para = new List<RunTimeDataType>();
			
		}

		public Type GetAdapterType()
		{
			return typeof(autogencodelib_TestobjAdapter);
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
				return "autogencodelib_TestobjAdapter_ctor";
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

				((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = new autogencodelib_TestobjAdapter();
				returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);


				((ICrossExtendAdapter)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value)
					.SetAS3RuntimeEnvironment(stackframe.player, ((ASBinCode.rtData.rtObjectBase)thisObj).value._class, (ASBinCode.rtData.rtObjectBase)thisObj);


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


}
