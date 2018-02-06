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
		public autogencodelib_TestobjAdapter()
		{

		}

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

			_testType = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, "testType");

		}

		private ASBinCode.rtData.rtFunction _testType;

		public override Type TestType(Type type)
		{
			if ( player==null || ( player != null && NativeConstParameterFunction.checkToken(
				new NativeConstParameterFunction.ExecuteToken(
				player.ExecuteToken.tokenid,"testType"
				)
				)))
			{
				return base.TestType(type);
			}
			else
			{
				var b= player.InvokeFunction(_testType, 1, type,null,null,null,null,null);
				
				return (Type)b;
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
