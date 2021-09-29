using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
public class GObj_Special
{
	public static void registSpecialFunctions(CSWC swc)
	{
		swc.regNativeFunction(new GObj_Special.GameObject_addComponent(), true);
		swc.regNativeFunction(new GObj_Special.MonoBehaviourAdapter_ctor(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponent(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponent__(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponentInChildren(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponentInChildren_(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponentsInChildren(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponentsInChildren_(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponentInParent(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponentsInParent(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponentsInParent_(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponents(), true);
		swc.regNativeFunction(new GObj_Special.Component_getComponents_(), true);

		swc.regNativeFunction(new GObj_Special.GameObject_getComponent(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponent__(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponentInChildren(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponentInChildren_(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponentInParent(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponents(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponents__(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponentsInChildren(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponentsInChildren_(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponentsInParent(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_getComponentsInParent_(), true);

		swc.regNativeFunction(new GObj_Special.GameObject_sendMessage(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_sendMessageUpwards(), true);
		swc.regNativeFunction(new GObj_Special.GameObject_broadcastMessage(), true);
		swc.regNativeFunction(new Component_sendMessage(), true);
		swc.regNativeFunction(new Component_sendMessageUpwards(), true);
		swc.regNativeFunction(new Component_broadcastMessage(), true);
	}

	public class MonoBehaviourAdapter : UnityEngine.MonoBehaviour, ASRuntime.ICrossExtendAdapter
	{

		public ASBinCode.rtti.Class AS3Class { get { return typeclass; } }

		public ASBinCode.rtData.rtObjectBase AS3Object { get { return bindAS3Object; } }

		protected Player player;
		private Class typeclass;
		private ASBinCode.rtData.rtObjectBase bindAS3Object;

		public void SetAS3RuntimeEnvironment(Player player, Class typeclass, ASBinCode.rtData.rtObjectBase bindAS3Object)
		{
			this.player = player;
			this.typeclass = typeclass;
			this.bindAS3Object = bindAS3Object;

		}

		public MonoBehaviourAdapter() : base() { }

		


		private ASBinCode.rtData.rtFunction _Awake;
		private bool _Awake_hasfound;
		internal void _FAwake()
		{
			if (!_Awake_hasfound)
			{
				_Awake = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, "Awake");
				_Awake_hasfound = true;
			}
			if (_Awake == null)
				return;
			player.InvokeFunction(_Awake, 0, null, null, null, null, null, null);

		}

		private ASBinCode.rtData.rtFunction _Start;
		private bool _Start_hasfound;
		System.Collections.IEnumerator Start()
		{
			if (!_Start_hasfound)
			{
				_Start = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, "Start");
				_Start_hasfound = true;
			}
			if (_Start == null)
				yield break ;

			var s= player.InvokeFunction(_Start, 0, null, null, null, null, null, null);

			if (s is ASBinCode.rtData.rtObject && ((ASBinCode.rtData.rtObject)s).value is YieldObject)
			{
				s = player.WapperIterator(((ASBinCode.rtData.rtObject)s));
			}

			if (s is System.Collections.IEnumerator)
			{
				System.Collections.IEnumerator ie = (System.Collections.IEnumerator)s;

				while (ie.MoveNext())
				{
					yield return ie.Current;
				}

			}
			else
				yield break;

		}

		private ASBinCode.rtData.rtFunction _Update;
		private bool _Update_hasfound;
		private void Update()
		{
			if (!_Update_hasfound)
			{
				_Update = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, "Update");
				_Update_hasfound = true;
			}
			if (_Update == null)
				return;
			player.InvokeFunction(_Update, 0, null, null, null, null, null, null);
		}



		private ASBinCode.rtData.rtFunction _FixedUpdate;
		private bool _FixedUpdate_hasfound;
		private void FixedUpdate()
		{
			if (!_FixedUpdate_hasfound)
			{
				_FixedUpdate = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, "FixedUpdate");
				_FixedUpdate_hasfound = true;
			}
			if (_FixedUpdate == null)
				return;
			player.InvokeFunction(_FixedUpdate, 0, null, null, null, null, null, null);
		}


		


		private ASBinCode.rtData.rtFunction _OnDestroy;
		private bool _OnDestroy_hasfound;
		private void OnDestroy()
		{
			if (!_OnDestroy_hasfound)
			{
				_OnDestroy = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, "OnDestroy");
				_OnDestroy_hasfound = true;
			}
			if (_OnDestroy == null)
				return;
			player.InvokeFunction(_OnDestroy, 0, null, null, null, null, null, null);
		}


		

		internal bool F6554FF96D0D340948D3DCD4AA9225CCD(string arg0, object arg1, SendMessageOptions arg2)
		{
			var fn = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, arg0);
			if (fn != null)
			{
				var def = player.swc.functions[fn.functionId];

				try
				{
					if (def.signature.parameters.Count == 0)
					{
						player.InvokeFunction(fn, 0, null, null, null, null, null, null);
						return true;
					}
					else if (def.signature.parameters.Count == 1)
					{
						player.InvokeFunction(fn, 1, arg1, null, null, null, null, null);
						return true;
					}

					return false;
				}
				catch (ASRunTimeException)
				{
					//Debug.LogError("catch asruntime error.");
					return true;
				}

				
			}
			else
			{
				return false;
			}
		}



		public override string ToString()
		{
			if (typeclass != null)
			{
				return "MonoBehaviour:define in " + typeclass.ToString();
			}
			else
			{
				return base.ToString() + " script object not mount";
			}
		}

		
	}

	public class MonoBehaviourAdapter_ctor : NativeConstParameterFunction, ICrossExtendAdapterCreator
	{
		public MonoBehaviourAdapter_ctor() : base(0)
		{
			para = new List<RunTimeDataType>();

		}

		public Type GetAdapterType()
		{
			return typeof(MonoBehaviourAdapter);
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
				return "_unityengine_MonoBehaviourAdapter_ctor";
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

				//((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = new MonoBehaviourAdapter();

				//((ICrossExtendAdapter)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value)
				//	.SetAS3RuntimeEnvironment(stackframe.player, ((ASBinCode.rtData.rtObjectBase)thisObj).value._class, (ASBinCode.rtData.rtObjectBase)thisObj);


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


	public class GameObject_addComponent : NativeConstParameterFunction
	{
		public GameObject_addComponent() : base(1)
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
				return "unityengine_GameObject_addComponent";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				object _result_ = _this.AddComponent((System.Type)arg0)
				;
				ICrossExtendAdapter crossExtendAdapter = _result_ as ICrossExtendAdapter;
				if (crossExtendAdapter != null)
				{
					ASBinCode.rtti.Class as3class = ((ASBinCode.rtData.rtObjectBase)argements[0]).value._class;
					stackframe.player.MakeICrossExtendAdapterEnvironment(crossExtendAdapter, as3class);

					MonoBehaviourAdapter madp = crossExtendAdapter as MonoBehaviourAdapter;
					if (madp != null)
					{
						madp._FAwake();
					}
				}

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

	#region Component findComponent


	public class Component_getComponent : NativeConstParameterFunction
	{
		public Component_getComponent() : base(1)
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
				return "unityengine_Component_getComponent";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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


				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass==null &&  cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponents(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if ( ct.getRtType()==rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}
						
					}

				}
				else
				{
					r = _this.GetComponent(arg0);
				}
				
				

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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

	public class Component_getComponent__ : NativeConstParameterFunction
	{
		public Component_getComponent__() : base(1)
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
				return "unityengine_Component_getComponent__";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

			try
			{
				string arg0 = TypeConverter.ConvertToString(argements[0], stackframe, token);

				object _result_ ;

				var as3cls= stackframe.player.getClass(arg0);
				if (as3cls == null)
				{
					_result_ = _this.GetComponent((System.String)arg0)
					;
				}
				else
				{
					
					if (as3cls.isCrossExtend)
					{
						Type t = stackframe.player.linktypemapper.getLinkType(as3cls.getBaseLinkSystemClass().getRtType());
						_result_ = null;
						var rttype = as3cls.getRtType();

						var cs = _this.GetComponents(t);
						for (int i = 0; i < cs.Length; i++)
						{
							var c = cs[i];
							if (c is ICrossExtendAdapter)
							{
								var ct = ((ICrossExtendAdapter)c).AS3Class;
								if (ct.getRtType() ==rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
								{
									_result_ = c;
									break;
								}
							}

						}
					}
					else
					{
						_result_ = _this.GetComponent(stackframe.player.linktypemapper.getLinkType(as3cls.getRtType()));
					;
					}
				}

				
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


	public class Component_getComponentInChildren : NativeConstParameterFunction
	{
		public Component_getComponentInChildren() : base(2)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
			para.Add(RunTimeDataType.rt_boolean);

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
				return "unityengine_Component_getComponentInChildren";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				bool arg1 = TypeConverter.ConvertToBoolean(argements[1], stackframe, token).value;

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponentsInChildren(arg0,arg1);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}

					}

				}
				else
				{
#if !UNITY_5
					r = _this.GetComponentInChildren(arg0);
#else
					r = _this.GetComponentInChildren(arg0,arg1);
#endif
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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



	public class Component_getComponentInChildren_ : NativeConstParameterFunction
	{
		public Component_getComponentInChildren_() : base(1)
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

#if !UNITY_5
				return "unityengine_Component_getComponentInChildren";
#else
				return "unityengine_Component_getComponentInChildren_";
#endif
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponentsInChildren(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}

					}

				}
				else
				{
					r = _this.GetComponentInChildren(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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



	public class Component_getComponentsInChildren : NativeConstParameterFunction
	{
		public Component_getComponentsInChildren() : base(1)
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
				return "unityengine_Component_getComponentsInChildren";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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


				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInChildren(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);
								
							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInChildren(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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


	public class Component_getComponentsInChildren_ : NativeConstParameterFunction
	{
		public Component_getComponentsInChildren_() : base(2)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
			para.Add(RunTimeDataType.rt_boolean);

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
				return "unityengine_Component_getComponentsInChildren_";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				bool arg1 = TypeConverter.ConvertToBoolean(argements[1], stackframe, token).value;

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInChildren(arg0,arg1);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInChildren(arg0,arg1);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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


	public class Component_getComponentInParent : NativeConstParameterFunction
	{
		public Component_getComponentInParent() : base(1)
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
				return "unityengine_Component_getComponentInParent";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponentsInParent(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}

					}

				}
				else
				{
					r = _this.GetComponentInParent(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);



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


	public class Component_getComponentsInParent : NativeConstParameterFunction
	{
		public Component_getComponentsInParent() : base(1)
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
				return "unityengine_Component_getComponentsInParent";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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


				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInParent(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInParent(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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

	public class Component_getComponentsInParent_ : NativeConstParameterFunction
	{
		public Component_getComponentsInParent_() : base(2)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
			para.Add(RunTimeDataType.rt_boolean);

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
				return "unityengine_Component_getComponentsInParent_";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				bool arg1 = TypeConverter.ConvertToBoolean(argements[1], stackframe, token).value;


				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInParent(arg0,arg1);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInParent(arg0,arg1);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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

	public class Component_getComponents : NativeConstParameterFunction
	{
		public Component_getComponents() : base(1)
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
				return "unityengine_Component_getComponents";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponents(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponents(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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

	public class Component_getComponents_ : NativeConstParameterFunction, IMethodGetter
	{
		public Component_getComponents_() : base(2)
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
				return "unityengine_Component_getComponents_";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				System.Collections.Generic.List<UnityEngine.Component> arg1;
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
					arg1 = (System.Collections.Generic.List<UnityEngine.Component>)_temp;
				}

				//_this.GetComponents((System.Type)arg0, (System.Collections.Generic.List<UnityEngine.Component>)arg1)
				//;




				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				
				if (ischeckcross)
				{
					var cs = _this.GetComponents(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								if (arg1 != null)
								{
									arg1.Add(c);
								}
							}
						}

					}
					
				}
				else
				{
					_this.GetComponents(arg0,arg1);
				}


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
				method = typeof(UnityEngine.Component).GetMethod("GetComponents", new Type[] { typeof(System.Type), typeof(System.Collections.Generic.List<UnityEngine.Component>) }); ;
			}
			return method;
		}

	}

#endregion

#region GameObject findComponet

	public class GameObject_getComponent : NativeConstParameterFunction
	{
		public GameObject_getComponent() : base(1)
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
				return "unityengine_GameObject_getComponent";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponents(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}

					}

				}
				else
				{
					r = _this.GetComponent(arg0);
				}



				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);



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


	public class GameObject_getComponent__ : NativeConstParameterFunction
	{
		public GameObject_getComponent__() : base(1)
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
				return "unityengine_GameObject_getComponent__";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

			try
			{
				string arg0 = TypeConverter.ConvertToString(argements[0], stackframe, token);

				object _result_ = _this.GetComponent((System.String)arg0)
				;

				var as3cls = stackframe.player.getClass(arg0);
				if (as3cls == null)
				{
					_result_ = _this.GetComponent((System.String)arg0)
					;
				}
				else
				{

					if (as3cls.isCrossExtend)
					{
						Type t = stackframe.player.linktypemapper.getLinkType(as3cls.getBaseLinkSystemClass().getRtType());
						_result_ = null;
						var rttype = as3cls.getRtType();

						var cs = _this.GetComponents(t);
						for (int i = 0; i < cs.Length; i++)
						{
							var c = cs[i];
							if (c is ICrossExtendAdapter)
							{
								var ct = ((ICrossExtendAdapter)c).AS3Class;
								if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
								{
									_result_ = c;
									break;
								}
							}

						}
					}
					else
					{
						_result_ = _this.GetComponent(stackframe.player.linktypemapper.getLinkType(as3cls.getRtType()));
						;
					}
				}


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

	public class GameObject_getComponentInChildren : NativeConstParameterFunction
	{
		public GameObject_getComponentInChildren() : base(2)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
			para.Add(RunTimeDataType.rt_boolean);

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
				return "unityengine_GameObject_getComponentInChildren";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				bool arg1 = TypeConverter.ConvertToBoolean(argements[1], stackframe, token).value;

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponentsInChildren(arg0, arg1);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}

					}

				}
				else
				{
#if !UNITY_5
					r = _this.GetComponentInChildren(arg0);
#else
					r = _this.GetComponentInChildren(arg0, arg1);
#endif

				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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


	public class GameObject_getComponentInChildren_ : NativeConstParameterFunction
	{
		public GameObject_getComponentInChildren_() : base(1)
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
#if !UNITY_5
				return "unityengine_GameObject_getComponentInChildren";
#else
				return "unityengine_GameObject_getComponentInChildren_";
#endif
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponentsInChildren(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}

					}

				}
				else
				{
					r = _this.GetComponentInChildren(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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

	public class GameObject_getComponentInParent : NativeConstParameterFunction
	{
		public GameObject_getComponentInParent() : base(1)
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
				return "unityengine_GameObject_getComponentInParent";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component r = null;
				if (ischeckcross)
				{
					var cs = _this.GetComponentsInParent(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								r = c;
								break;
							}
						}

					}

				}
				else
				{
					r = _this.GetComponentInParent(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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


	public class GameObject_getComponents : NativeConstParameterFunction
	{
		public GameObject_getComponents() : base(1)
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
				return "unityengine_GameObject_getComponents";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponents(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponents(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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

	public class GameObject_getComponents__ : NativeConstParameterFunction
	{
		public GameObject_getComponents__() : base(2)
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
				return "unityengine_GameObject_getComponents__";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				System.Collections.Generic.List<UnityEngine.Component> arg1;
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
					arg1 = (System.Collections.Generic.List<UnityEngine.Component>)_temp;
				}


				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}


				if (ischeckcross)
				{
					var cs = _this.GetComponents(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								if (arg1 != null)
								{
									arg1.Add(c);
								}
							}
						}

					}

				}
				else
				{
					_this.GetComponents(arg0, arg1);
				}


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


	}


	public class GameObject_getComponentsInChildren : NativeConstParameterFunction
	{
		public GameObject_getComponentsInChildren() : base(1)
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
				return "unityengine_GameObject_getComponentsInChildren";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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


				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInChildren(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInChildren(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);


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

	public class GameObject_getComponentsInChildren_ : NativeConstParameterFunction
	{
		public GameObject_getComponentsInChildren_() : base(2)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
			para.Add(RunTimeDataType.rt_boolean);

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
				return "unityengine_GameObject_getComponentsInChildren_";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				bool arg1 = TypeConverter.ConvertToBoolean(argements[1], stackframe, token).value;

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInChildren(arg0, arg1);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInChildren(arg0, arg1);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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


	public class GameObject_getComponentsInParent : NativeConstParameterFunction
	{
		public GameObject_getComponentsInParent() : base(1)
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
				return "unityengine_GameObject_getComponentsInParent";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInParent(arg0);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInParent(arg0);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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

	public class GameObject_getComponentsInParent_ : NativeConstParameterFunction
	{
		public GameObject_getComponentsInParent_() : base(2)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
			para.Add(RunTimeDataType.rt_boolean);

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
				return "unityengine_GameObject_getComponentsInParent_";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				bool arg1 = TypeConverter.ConvertToBoolean(argements[1], stackframe, token).value;

				bool ischeckcross = false;
				var rttype = argements[0].rtType;
				if (rttype > ASBinCode.RunTimeDataType._OBJECT)
				{
					var cls = bin.getClassByRunTimeDataType(rttype);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						cls = cls.instanceClass;
						rttype = cls.getRtType();
						ischeckcross = true;
					}

				}

				Component[] r = null;
				if (ischeckcross)
				{
					List<Component> list = new List<Component>();

					var cs = _this.GetComponentsInParent(arg0, arg1);
					for (int i = 0; i < cs.Length; i++)
					{
						var c = cs[i];
						if (c is ICrossExtendAdapter)
						{
							var ct = ((ICrossExtendAdapter)c).AS3Class;
							if (ct.getRtType() == rttype || ASBinCode.ClassMemberFinder.check_isinherits(ct.getRtType(), rttype, bin))
							{
								list.Add(c);

							}
						}

					}
					r = list.ToArray();
				}
				else
				{
					r = _this.GetComponentsInParent(arg0, arg1);
				}

				stackframe.player.linktypemapper.storeLinkObject_ToSlot(r, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);

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


	#endregion


	public class GameObject_sendMessage : NativeConstParameterFunction
	{
		public GameObject_sendMessage() : base(3)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_string);
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
				return "unityengine_GameObject_sendMessage";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				UnityEngine.SendMessageOptions arg2;
				{
					object _temp;
					if (!stackframe.player.linktypemapper.rtValueToLinkObject(
						argements[2],

						stackframe.player.linktypemapper.getLinkType(argements[2].rtType)
						,
						bin, true, out _temp
						))
					{
						stackframe.throwCastException(token, argements[2].rtType,

							functionDefine.signature.parameters[2].type
							);
						success = false;
						return;
					}
					arg2 = (UnityEngine.SendMessageOptions)_temp;
				}

				
				var mbas = _this.GetComponents<MonoBehaviourAdapter>();
				bool hasreceiver = false;
				foreach (var item in mbas)
				{					
					hasreceiver = hasreceiver || item.F6554FF96D0D340948D3DCD4AA9225CCD(arg0, arg1,arg2);
				}

				if (hasreceiver)
					arg2 = SendMessageOptions.DontRequireReceiver;

				_this.SendMessage((System.String)arg0, (System.Object)arg1, (UnityEngine.SendMessageOptions)arg2)
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

		

	}

	public class GameObject_sendMessageUpwards : NativeConstParameterFunction
	{
		public GameObject_sendMessageUpwards() : base(3)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_string);
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
				return "unityengine_GameObject_sendMessageUpwards";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				UnityEngine.SendMessageOptions arg2;
				{
					object _temp;
					if (!stackframe.player.linktypemapper.rtValueToLinkObject(
						argements[2],

						stackframe.player.linktypemapper.getLinkType(argements[2].rtType)
						,
						bin, true, out _temp
						))
					{
						stackframe.throwCastException(token, argements[2].rtType,

							functionDefine.signature.parameters[2].type
							);
						success = false;
						return;
					}
					arg2 = (UnityEngine.SendMessageOptions)_temp;
				}

				var mbas = _this.GetComponentsInParent<MonoBehaviourAdapter>();
				bool hasreceiver = false;
				foreach (var item in mbas)
				{
					hasreceiver = hasreceiver || item.F6554FF96D0D340948D3DCD4AA9225CCD(arg0, arg1, arg2);
				}

				if (hasreceiver)
					arg2 = SendMessageOptions.DontRequireReceiver;

				_this.SendMessageUpwards((System.String)arg0, (System.Object)arg1, (UnityEngine.SendMessageOptions)arg2)
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

		

	}

	public class GameObject_broadcastMessage : NativeConstParameterFunction
	{
		public GameObject_broadcastMessage() : base(3)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_string);
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
				return "unityengine_GameObject_broadcastMessage";
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

			UnityEngine.GameObject _this =
				(UnityEngine.GameObject)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				UnityEngine.SendMessageOptions arg2;
				{
					object _temp;
					if (!stackframe.player.linktypemapper.rtValueToLinkObject(
						argements[2],

						stackframe.player.linktypemapper.getLinkType(argements[2].rtType)
						,
						bin, true, out _temp
						))
					{
						stackframe.throwCastException(token, argements[2].rtType,

							functionDefine.signature.parameters[2].type
							);
						success = false;
						return;
					}
					arg2 = (UnityEngine.SendMessageOptions)_temp;
				}

				var mbas = _this.GetComponentsInChildren<MonoBehaviourAdapter>();
				bool hasreceiver = false;
				foreach (var item in mbas)
				{
					hasreceiver = hasreceiver || item.F6554FF96D0D340948D3DCD4AA9225CCD(arg0, arg1, arg2);
				}

				if (hasreceiver)
					arg2 = SendMessageOptions.DontRequireReceiver;

				_this.SendMessageUpwards((System.String)arg0, (System.Object)arg1, (UnityEngine.SendMessageOptions)arg2)
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
		
	}


	public class Component_sendMessage : NativeConstParameterFunction
	{
		public Component_sendMessage() : base(3)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_string);
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
				return "unityengine_Component_sendMessage";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				UnityEngine.SendMessageOptions arg2;
				{
					object _temp;
					if (!stackframe.player.linktypemapper.rtValueToLinkObject(
						argements[2],

						stackframe.player.linktypemapper.getLinkType(argements[2].rtType)
						,
						bin, true, out _temp
						))
					{
						stackframe.throwCastException(token, argements[2].rtType,

							functionDefine.signature.parameters[2].type
							);
						success = false;
						return;
					}
					arg2 = (UnityEngine.SendMessageOptions)_temp;
				}

				var mbas = _this.GetComponents<MonoBehaviourAdapter>();
				bool hasreceiver = false;
				foreach (var item in mbas)
				{
					hasreceiver = hasreceiver || item.F6554FF96D0D340948D3DCD4AA9225CCD(arg0, arg1, arg2);
				}

				if (hasreceiver)
					arg2 = SendMessageOptions.DontRequireReceiver;

				_this.SendMessageUpwards((System.String)arg0, (System.Object)arg1, (UnityEngine.SendMessageOptions)arg2)
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

		

	}


	public class Component_sendMessageUpwards : NativeConstParameterFunction
	{
		public Component_sendMessageUpwards() : base(3)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_string);
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
				return "unityengine_Component_sendMessageUpwards";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				UnityEngine.SendMessageOptions arg2;
				{
					object _temp;
					if (!stackframe.player.linktypemapper.rtValueToLinkObject(
						argements[2],

						stackframe.player.linktypemapper.getLinkType(argements[2].rtType)
						,
						bin, true, out _temp
						))
					{
						stackframe.throwCastException(token, argements[2].rtType,

							functionDefine.signature.parameters[2].type
							);
						success = false;
						return;
					}
					arg2 = (UnityEngine.SendMessageOptions)_temp;
				}

				var mbas = _this.GetComponentsInParent<MonoBehaviourAdapter>();
				bool hasreceiver = false;
				foreach (var item in mbas)
				{
					hasreceiver = hasreceiver || item.F6554FF96D0D340948D3DCD4AA9225CCD(arg0, arg1, arg2);
				}

				if (hasreceiver)
					arg2 = SendMessageOptions.DontRequireReceiver;

				_this.SendMessageUpwards((System.String)arg0, (System.Object)arg1, (UnityEngine.SendMessageOptions)arg2)
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

	}


	public class Component_broadcastMessage : NativeConstParameterFunction
	{
		public Component_broadcastMessage() : base(3)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_string);
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
				return "unityengine_Component_broadcastMessage";
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

			UnityEngine.Component _this =
				(UnityEngine.Component)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

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
				UnityEngine.SendMessageOptions arg2;
				{
					object _temp;
					if (!stackframe.player.linktypemapper.rtValueToLinkObject(
						argements[2],

						stackframe.player.linktypemapper.getLinkType(argements[2].rtType)
						,
						bin, true, out _temp
						))
					{
						stackframe.throwCastException(token, argements[2].rtType,

							functionDefine.signature.parameters[2].type
							);
						success = false;
						return;
					}
					arg2 = (UnityEngine.SendMessageOptions)_temp;
				}


				var mbas = _this.GetComponentsInChildren<MonoBehaviourAdapter>();
				bool hasreceiver = false;
				foreach (var item in mbas)
				{
					hasreceiver = hasreceiver || item.F6554FF96D0D340948D3DCD4AA9225CCD(arg0, arg1, arg2);
				}

				if (hasreceiver)
					arg2 = SendMessageOptions.DontRequireReceiver;

				_this.SendMessageUpwards((System.String)arg0, (System.Object)arg1, (UnityEngine.SendMessageOptions)arg2)
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

	}


}
