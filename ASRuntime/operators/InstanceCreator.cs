using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    /// <summary>
    /// 对象实例创建器
    /// </summary>
    class InstanceCreator
    {
        
        private readonly StackFrame invokerFrame;
        public SourceToken token;
        public OpStep step;

        private ASBinCode.rtti.Class _class;
        public IBlockCallBack callbacker;

        internal FunctionCaller constructorCaller;

        public RunTimeValueBase objectResult;

        /// <summary>
        /// 是否是通过Function对象创建
        /// </summary>
        public ASBinCode.rtData.rtObjectBase constructor;
        private FunctionCaller _function_constructor;

		private HeapSlot tempSlot;

		public SLOT objectStoreToSlot;

        private InstanceCreator(StackFrame invokerFrame)//, SourceToken token, Class _class)
        {
            this.invokerFrame = invokerFrame;
			//this.token = token;
			//this._class = _class;
			tempSlot = new HeapSlot();
        }

		private Player player
		{
			get { return invokerFrame.player; }
		}

		public static InstanceCreator Create(StackFrame invokerFrame)
		{
			return new InstanceCreator(invokerFrame);
		}

		public void SetTokenAndClass(SourceToken token, Class _class)
		{
			this.token = token;
			this._class = _class;
		}


		public void clear()
		{
			token = null;
			_class = null;
			step = null;
			callbacker = null;
			constructorCaller = null;
			objectResult = null;

			constructor = null;
			_function_constructor = null;
			objectStoreToSlot = null;

			toNoticeFailed1 = null;
			toNoticeFailed2 = null;


			tempSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
		}


        public bool prepareConstructorArgements()
        {
            int classid = _class.classid;

            
            ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[player.swc.classes[classid].constructor_functionid];
            ASBinCode.rtti.FunctionSignature signature = funcDefine.signature;
            
            constructorCaller = player.funcCallerPool.create( invokerFrame, token);
            constructorCaller.toCallFunc = funcDefine;
            constructorCaller._tempSlot = invokerFrame._tempSlot1;
            if (!constructorCaller.createParaScope()) { return false; }

            //constructorCaller.releaseAfterCall = true;

            if (constructor != null)
            {
                _function_constructor = player.funcCallerPool.create(invokerFrame, token);
                _function_constructor._tempSlot = invokerFrame._tempSlot1;
                _function_constructor.toCallFunc = 
                    player.swc.functions[ ((ASBinCode.rtData.rtFunction)
                    TypeConverter.ObjectImplicit_ToPrimitive( constructor)).functionId];

                if (!_function_constructor.createParaScope()) { return false; }

                //_function_constructor.releaseAfterCall = true;
            }
            return true;
        }

        public bool push_parameter(RunTimeValueBase arg,int id)
        {
            bool success;
            if (constructor == null)
            {
                constructorCaller.pushParameter(arg, id,out success);
				if (!success)
				{
					constructorCaller.noticeRunFailed();
				}
            }
            else
            {
                _function_constructor.pushParameter(arg, id,out success);
				if (!success)
				{
					_function_constructor.noticeRunFailed();
				}
            }
			return success;
        }

        public static bool init_static_class(Class cls,Player player,SourceToken token)
        {
            if (!player.static_instance.ContainsKey(cls.staticClass.classid))
            {
                int f = player.getRuntimeStackFlag();


                ASBinCode.RunTimeScope objScope;
                ASBinCode.rtti.Object obj = makeObj(
                    player,token,
                    cls.staticClass,
                    null,null, out objScope).value;

                player.static_instance.Add(cls.staticClass.classid,
                    new ASBinCode.rtData.rtObject(obj, objScope));

                if (cls.super != null)
                {
                    bool s = init_static_class(cls.super,player,token);

                    if (s)
                    {
                        ((DynamicObject)obj)._prototype_ = (DynamicObject)(player.static_instance[cls.super.staticClass.classid]).value;

                        bool result = player.step_toStackflag(f);

                        if (cls.classid != 2)
                        {
                            ((DynamicObject)((ASBinCode.rtData.rtObjectBase)obj.memberData[0].getValue()).value)["constructor"].directSet(player.static_instance[cls.staticClass.classid]);

                        }

                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    bool result = player.step_toStackflag(f);

                    return result;
                }

            }
            else
            {
                return true;
            }
        }

        public bool init_static_class(Class cls)
        {
            return init_static_class(cls, player, token);
        }

        private static BlockCallBackBase.dgeCallbacker D_afterCreateStaticInstanceCallBacker = new BlockCallBackBase.dgeCallbacker(afterCreateStaticInstanceCallBacker);
        private static BlockCallBackBase.dgeCallbacker D_creatorFailed = new BlockCallBackBase.dgeCallbacker(creatorFailed);

        public void createInstance()
        {

            if (!player.static_instance.ContainsKey(_class.staticClass.classid))
            {

				//afterCreateStaticInstance callbacker = new afterCreateStaticInstance();
				var callbacker= player.blockCallBackPool.create();
				callbacker.args = this;
				callbacker.setCallBacker(D_afterCreateStaticInstanceCallBacker);
				callbacker.setWhenFailed(D_creatorFailed);

                ASBinCode.RunTimeScope objScope;
                var obj = makeObj(player,token, _class.staticClass, callbacker,this, out objScope);

                player.static_instance.Add(_class.staticClass.classid,
                    obj);

                if (_class.super != null)
                {
                    bool s = init_static_class(_class.super);

                    if (s)
                    {
                        ((DynamicObject)obj.value)._prototype_ 
                            = (DynamicObject)(player.static_instance[_class.super.staticClass.classid]).value;
                    }
                    else
                    {
                        invokerFrame.endStep();
                    }
                }
                


            }
            else
            {
                //exec_step1(player,frame,step,as3class,scope);
                exec_step0();
            }
        }

		private static void afterCreateStaticInstanceCallBacker(BlockCallBackBase sender, object args)
		{
			InstanceCreator ic = (InstanceCreator)sender.args;
			
			ic.set_Class_constructor();
		}

		private static void creatorFailed(BlockCallBackBase sender, object args)
		{
			InstanceCreator ic = (InstanceCreator)args;

			if (ic.constructorCaller != null)
			{
				ic.constructorCaller.noticeRunFailed();
				ic.constructorCaller = null;
			}

			if (ic.callbacker != null)
			{
				ic.callbacker.noticeRunFailed();
				ic.callbacker = null;
			}
		}

		private void set_Class_constructor()
        {
            if (_class.classid > 2)
            {
                var obj = player.static_instance[_class.staticClass.classid];

                ((DynamicObject)
                    ((ASBinCode.rtData.rtObjectBase)obj.value.memberData[0].getValue()).value)
                    ["constructor"].directSet(player.static_instance[_class.staticClass.classid]);

            }

            exec_step0();
        }
        private static BlockCallBackBase.dgeCallbacker D_afterCreateOutScopeCallbacker = new BlockCallBackBase.dgeCallbacker(afterCreateOutScopeCallbacker);
        private void exec_step0()
        {
            Class cls;
            if (_class.mainClass == null)
            {
                cls = _class;
            }
            else
            {
                cls = _class.mainClass;
            }

            if (!player.outpackage_runtimescope.ContainsKey(cls.classid))
            {
				//afterCreateOutScope callbacker = new afterCreateOutScope();
				var callbacker= player.blockCallBackPool.create();
                callbacker.args = this;
				callbacker.setWhenFailed(D_creatorFailed);
				callbacker.setCallBacker(D_afterCreateOutScopeCallbacker);

                if (make_outpackage_scope(cls, callbacker))
                {
                    var ss = cls.super;

                    while (ss !=null)   //建立父类的包外对象
                    {
                        if (!player.outpackage_runtimescope.ContainsKey(ss.classid))
                        {
                            if (init_static_class(ss))
                            {
                                if (!player.outpackage_runtimescope.ContainsKey(ss.classid))
                                {
                                    bool add = make_outpackage_scope(ss, null);

                                    if (!add)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                invokerFrame.endStep();
                                break;
                            }
                        }

                        ss = ss.super;
                    }

                }

            }
            else
            {
                exec_step1();
            }
        }

		private static void afterCreateOutScopeCallbacker(BlockCallBackBase sender, object args)
		{
			((InstanceCreator)sender.args).
					exec_step1();
		}

		private bool make_outpackage_scope(Class cls,IBlockCallBack cb)
        {
            
            ASBinCode.CodeBlock codeblock = player.swc.blocks[cls.outscopeblockid];

            HeapSlot[] globaldata = player.genHeapFromCodeBlock(codeblock);
            Global_Object global = Global_Object.formCodeBlock(codeblock, globaldata, player.swc.classes[0]);
            ASBinCode.rtData.rtObject globalObj = new ASBinCode.rtData.rtObject(global, null);

            ASBinCode.RunTimeScope rtscope = player.callBlock(
                codeblock, globaldata, null,
                null, //player.static_instance[cls.staticClass.classid].objScope,

                step.token,
                cb,
                globalObj,
                RunTimeScopeType.outpackagemember
                );

			if (rtscope == null)
			{
				invokerFrame.endStep(step);
				return false;
			}

            globalObj.objScope = rtscope;
            player.outpackage_runtimescope.Add(cls.classid, rtscope);
            {
                var slot = new DynamicPropertySlot(globalObj,true,player.swc.FunctionClass.getRtType());
                slot.directSet(player.static_instance[cls.staticClass.classid]);
                global.createproperty(cls.name, slot);
            }
            for (int i = 0; i < player.swc.classes.Count; i++)
            {
                if (player.swc.classes[i].mainClass == cls)
                {
                    if (init_static_class(player.swc.classes[i]))
                    {
                        var slot = new DynamicPropertySlot(globalObj,true,player.swc.FunctionClass.getRtType());
                        slot.directSet(player.static_instance[player.swc.classes[i].staticClass.classid]);
                        global.createproperty(player.swc.classes[i].name, slot);
                    }
                    else
                    {
                        //***出错了
                        invokerFrame.endStep(step);
                        return false;
                    }
                }
            }
            
            return true;
        }


        private static BlockCallBackBase.dgeCallbacker D_afterCreateInstanceDataCallBacker = new BlockCallBackBase.dgeCallbacker(afterCreateInstanceDataCallBacker);
        private void exec_step1()
        {
			
			BlockCallBackBase callbacker = player.blockCallBackPool.create();
			callbacker.args = this;

			callbacker.setCallBacker(D_afterCreateInstanceDataCallBacker);
			callbacker.setWhenFailed(D_creatorFailed);

            ASBinCode.RunTimeScope objScope;
			
			makeObj(player,token, _class, callbacker,this, out objScope);
			
        }

		internal BlockCallBackBase toNoticeFailed1;
		internal BlockCallBackBase toNoticeFailed2;

		internal void noticeWhenCreateAneFailed()
		{
			if (toNoticeFailed1 != null)
			{
				toNoticeFailed1.noticeRunFailed();
				toNoticeFailed1 = null;
			}
			if (toNoticeFailed2 != null)
			{
				toNoticeFailed2.noticeRunFailed();
				toNoticeFailed2 = null;
			}
		}

		private static void afterCreateInstanceDataCallBacker(BlockCallBackBase sender, object args)
		{
			InstanceCreator ic = (InstanceCreator)sender.args;
			
			ASBinCode.rtti.Object obj = (ASBinCode.rtti.Object)sender.cacheObjects[0];
			RunTimeScope objScope = (RunTimeScope)sender.cacheObjects[2];
			ASBinCode.rtData.rtObjectBase rtObject = (ASBinCode.rtData.rtObjectBase)sender.cacheObjects[1];

			ic.toNoticeFailed1 = sender;
			ic.exec_step2(obj, objScope, rtObject);
			ic.toNoticeFailed1 = null;
		}

		private void exec_step2(
            ASBinCode.rtti.Object obj,
            RunTimeScope objScope, ASBinCode.rtData.rtObjectBase _object)
        {
            //***添加Object的动态对象****
            if (
                //(obj._class.classid == 0) 
                obj is DynamicObject
                && obj._class.staticClass != null)
            {
                DynamicObject dobj = (DynamicObject)obj;
               
                if (constructor == null)
                {
                    dobj.createproperty("constructor", new DynamicPropertySlot(_object, false,player.swc.FunctionClass.getRtType()));
                    dobj["constructor"].directSet(player.static_instance[obj._class.staticClass.classid]);
                    dobj._prototype_ = (DynamicObject)player.static_instance[_class.staticClass.classid].value;
                }
                else
                {
                    dobj._prototype_ =  (DynamicObject)constructor.value;
                }
            }

			ClassMember ctor = obj._class.constructor;

			//if (obj._class.isCrossExtend)
			//{
			//	//***创建Adapter***
			//	var scls = obj._class.super;
			//	while (!scls.isLink_System)
			//	{
			//		scls = scls.super;
			//	}

			//	ctor = scls.crossExtendAdapterCreator;

			//	var nf = player.swc.getNativeFunction(((MethodGetterBase)ctor.bindField).functionId);
			//	if (!(nf is ICrossExtendAdapterCreator))
			//	{
			//		invokerFrame.throwAneException(token, "adapter不是ICrossExtendAdapterCreator");
			//		callbacker.noticeRunFailed();
			//		noticeWhenCreateAneFailed();
			//		invokerFrame.endStep();
			//		return;

			//	}

			//	constructorCaller.toCallFunc =  player.swc.functions[((MethodGetterBase)ctor.bindField).functionId];

			//}


            //调用构造函数
            if (ctor != null)
            {
                ASBinCode.rtData.rtFunction function =
                    (ASBinCode.rtData.rtFunction)((MethodGetterBase)ctor.bindField).getConstructor(objScope);


				HeapSlot _temp = tempSlot;
                constructorCaller.returnSlot = _temp;
				constructorCaller.SetFunction(function);function.Clear();
				

				//afterCallConstructor callbacker = new afterCallConstructor();
				var callbacker = player.blockCallBackPool.create();
                callbacker.args = this;
				//callbacker.obj = obj;
				//callbacker.objScope = objScope;
				//callbacker.rtObject = _object;
				callbacker.cacheObjects[0] = _object;
				callbacker.setWhenFailed(D_creatorFailed);
				callbacker.setCallBacker(D_afterCallConstructorCallbacker);

				
                constructorCaller.callbacker = callbacker;

				toNoticeFailed2 = callbacker;
				constructorCaller.call();
				toNoticeFailed2 = null;

				constructorCaller = null;

            }
            else
            {
                exec_step3(_object);
            }
        }
        private static BlockCallBackBase.dgeCallbacker D_afterCallConstructorCallbacker = new BlockCallBackBase.dgeCallbacker(afterCallConstructorCallbacker);

        private static void afterCallConstructorCallbacker(BlockCallBackBase sender, object args)
		{
			((InstanceCreator)sender.args).
			   exec_step3(
					  //rtObject
					  (ASBinCode.rtData.rtObjectBase)sender.cacheObjects[0]
				   );
		}

		private void exec_step3(ASBinCode.rtData.rtObjectBase rtobject)
        {
            if (constructor == null)
            {
                objectResult = rtobject;
                //objectResult.directSet(rtobject);
                if (callbacker != null)
                {
                    callbacker.call(this);
                    callbacker = null;
                }
            }
            else
            {
				HeapSlot _temp = tempSlot; _temp.directSet(ASBinCode.rtData.rtUndefined.undefined);
                _function_constructor.returnSlot = _temp;
				//_function_constructor.function = (ASBinCode.rtData.rtFunction)TypeConverter.ObjectImplicit_ToPrimitive(constructor).Clone();
				//_function_constructor.function.setThis(rtobject);
				_function_constructor.SetFunction((ASBinCode.rtData.rtFunction)TypeConverter.ObjectImplicit_ToPrimitive(constructor), rtobject);

                BlockCallBackBase cb = player.blockCallBackPool.create();
                cb.args = cb.cacheObjects; //new object[] { rtobject , _temp };
                cb.cacheObjects[0] = rtobject;
                cb.cacheObjects[1] = _temp;
                cb.cacheObjects[2] = this;

                cb.setCallBacker(D_finalStep);

                _function_constructor.callbacker = cb;
                _function_constructor.call();
                _function_constructor = null;
            }
        }
        private static BlockCallBackBase.dgeCallbacker D_finalStep = new BlockCallBackBase.dgeCallbacker(_finalStep);
        private static void _finalStep(BlockCallBackBase sender,object args)
        {
            object[] a = (object[])sender.args;

            InstanceCreator creator = (InstanceCreator)a[2];

            creator.objectResult = (ASBinCode.rtData.rtObjectBase)a[0];

            //***如果有返回值****
            var returnvalue = ((SLOT)a[1]).getValue(); ((SLOT)a[1]).directSet(ASBinCode.rtData.rtUndefined.undefined);
			if (returnvalue.rtType != RunTimeDataType.rt_void
                &&
                returnvalue.rtType != RunTimeDataType.rt_string
                &&
                returnvalue.rtType != RunTimeDataType.rt_number
                &&
                returnvalue.rtType != RunTimeDataType.rt_int
                &&
                returnvalue.rtType != RunTimeDataType.rt_boolean
                &&
                returnvalue.rtType != RunTimeDataType.rt_uint
                )
            {
                creator. objectResult = returnvalue;
            }

            //objectResult.directSet((ASBinCode.rtData.rtObject)sender.args);
            if (creator.callbacker != null)
            {
                creator.callbacker.call(creator);
                creator.callbacker = null;
            }
        }


        internal static LinkSystemObject createLinkObjectValue(Player player, Class cls)
        {
            if (cls.isLink_System)
            {
				ASBinCode.rtData.rtObjectBase rb; ASBinCode.rtData.rtObjectBase lr;string err;
				ASBinCode.rtti.Object obj = createObject(player.swc, cls,null,out rb,out lr,out err);
                return (LinkSystemObject)obj;
            }
            else
            {
                throw new EngineException();
            }
        }

        internal static ASBinCode.rtData.rtObjectBase createPureHostdOrLinkObject(Player player, Class cls)
        {
            if (cls.isLink_System)
            {
				ASBinCode.rtData.rtObjectBase rb; ASBinCode.rtData.rtObjectBase lr; string err;
				ASBinCode.rtti.Object obj = createObject(player.swc, cls,null,out rb,out lr,out err);
				ASBinCode.rtData.rtObjectBase rtObj;
				if (lr != null)
					rtObj = lr;
				else
					rtObj= new ASBinCode.rtData.rtObject(obj, null);

                RunTimeScope scope = new RunTimeScope(
                    null
                    //player.genHeapFromCodeBlock(player.swc.blocks[cls.blockid])
                        , cls.blockid, null, rtObj,
                     RunTimeScopeType.objectinstance);
                rtObj.objScope = scope;

                return rtObj;
            }
            else if (cls.isUnmanaged)
            {
                ASBinCode.rtti.Object obj;
                if (cls.dynamic)
                {
                    obj = new HostedDynamicObject(cls);
                }
                else
                {
                    obj = new HostedObject(cls);
                }

                ASBinCode.rtData.rtObject rtObj = new ASBinCode.rtData.rtObject(obj, null);
                RunTimeScope scope = new RunTimeScope(
                    player.genHeapFromCodeBlock(player.swc.blocks[cls.blockid]), 
                    cls.blockid, null, rtObj, RunTimeScopeType.objectinstance);
                rtObj.objScope = scope;
                
                
                return rtObj;
            }
            else
            {
                throw new EngineException();
            }
        }

        
        private static ASBinCode.rtti.Object createObject(CSWC swc,Class cls,InstanceCreator creator,
			out ASBinCode.rtData.rtObjectBase rtObjectBase,
			out ASBinCode.rtData.rtObjectBase linkrtobj,
			out string errinfo
			)
        {
            ASBinCode.rtti.Object obj = null;// = new ASBinCode.rtti.Object(cls);
			rtObjectBase = null;linkrtobj = null;errinfo = null;
			if (cls.isLink_System)
			{
				if (creator != null)
				{
					StackSlot stackSlot = creator.objectStoreToSlot as StackSlot;
					if (stackSlot != null)
					{
						rtObjectBase = stackSlot.getStackCacheObject(cls);
						return rtObjectBase.value;
					}
				}


				var func = (ILinkSystemObjCreator)swc.class_Creator[cls];

				
				ASBinCode.rtData.rtObjectBase rtObj =
					func.makeObject(cls) as ASBinCode.rtData.rtObjectBase;
				linkrtobj = rtObj;
				if (rtObj == null)
				{
					errinfo = cls.ToString() + " create linksystem object failed";
					return null;
				}
				else
				{
					return rtObj.value;
				}
			}
			else if (cls.isCrossExtend)
			{
				var scls = cls.super;
				while (!scls.isLink_System)
				{
					scls = scls.super;
				}

				var cextend = scls.staticClass.linkObjCreator;
				var func = swc.getNativeFunction(( (ClassMethodGetter)cextend.bindField).functionId ) as ILinkSystemObjCreator;

				if (func == null )
				{
					errinfo = cls.ToString() + " create crossextend object failed, creator function not found";
					return null;
				}

				
				ASBinCode.rtData.rtObjectBase rtObj =
					func.makeObject(cls) as ASBinCode.rtData.rtObjectBase;
				linkrtobj = rtObj;
				if (rtObj == null)
				{
					errinfo = cls.ToString() + " create crossextend object failed";
					return null;
				}
				else
				{
					LinkSystemObject lo = (LinkSystemObject)rtObj.value;
					return lo;
					
				}
			}
			else if (
				swc.DictionaryClass != null
				&&
				ClassMemberFinder.isInherits(cls, swc.DictionaryClass))
			{
				obj = new DictionaryObject(cls);
			}
			else if (cls.dynamic)
			{
				if (cls.isUnmanaged)
				{
					obj = new HostedDynamicObject(cls);
				}
				else
				{
					obj = new DynamicObject(cls);
				}
			}
			else if (cls.isUnmanaged)
			{
				obj = new HostedObject(cls);
			}
			else
			{
				obj = new ASBinCode.rtti.Object(cls);
			}

            return obj;
        }



        private static readonly ObjectMemberSlot[] blankFields = new ObjectMemberSlot[0];
        private static ASBinCode.rtData.rtObjectBase makeObj(
            Player player,
            SourceToken token,
            ASBinCode.rtti.Class cls,
            BlockCallBackBase callbacker, InstanceCreator creator, out ASBinCode.RunTimeScope objScope)
        {

			ASBinCode.rtData.rtObjectBase result; ASBinCode.rtData.rtObjectBase lr;string err;

			ASBinCode.rtti.Object obj = createObject(player.swc, cls,creator,out result,out lr,out err);

			if (obj == null)
			{
				objScope = null;
				if (callbacker != null)
				{
					callbacker.noticeRunFailed();
				}
				player.throwWhenMakeObjFailed(new ASRunTimeException(err, player.stackTrace(0)));

				return null;
			}

			if (result == null)
			{
				if (lr == null)
				{
					result = new ASBinCode.rtData.rtObject(obj, null);
				}
				else
				{
					result = lr;
				}
				if (cls.fields.Count > 0)
				{
					obj.memberData = new ObjectMemberSlot[cls.fields.Count];
					for (int i = 0; i < obj.memberData.Length; i++)
					{
						obj.memberData[i] = new ObjectMemberSlot(result, player.swc.FunctionClass.getRtType(),cls.fields[i].valueType,player.swc);

						if (cls.fields[i].defaultValue == null)
						{
							obj.memberData[i].directSet(TypeConverter.getDefaultValue(cls.fields[i].valueType).getValue(null, null));
						}

						((ObjectMemberSlot)obj.memberData[i]).isConstMember = cls.fields[i].isConst;
					}
				}
				else
				{
					obj.memberData = blankFields;
				}


				ASBinCode.CodeBlock codeblock = player.swc.blocks[cls.blockid];

				objScope = player.callBlock(codeblock,
					(ObjectMemberSlot[])obj.memberData, null,
					null
					, token, callbacker
					,
					result, RunTimeScopeType.objectinstance
					);

				result.objScope = objScope;

				if (objScope == null)
				{
					if (callbacker != null)
					{
						callbacker.noticeRunFailed();
					}

					return null;
				}

				//***把父类的初始化函数推到栈上去***
				var ss = cls.super;
				while (ss != null)
				{
					var scope = player.callBlock(player.swc.blocks[ss.blockid],
						(ObjectMemberSlot[])obj.memberData,
						null, null, token, null, result, RunTimeScopeType.objectinstance
						);

					ss = ss.super;

					if (scope == null)
					{
						if (callbacker != null)
						{
							callbacker.noticeRunFailed();
						}

						return null;
					}

				}

			}
			else
			{
				objScope = result.objScope;
				player.CallBlankBlock(callbacker);
			}

            if (callbacker != null)
            {
				//callbacker.obj = obj;
				//callbacker.rtObject = result;
				//callbacker.objScope = objScope;

				callbacker.cacheObjects[0] = obj;
				callbacker.cacheObjects[1] = result;
				callbacker.cacheObjects[2] = objScope;
				
			}

            return result;
        }


        //class afterCallConstructor : baseinstancecallbacker
        //{
        //    public override void call(object args)
        //    {
        //        ((InstanceCreator)this.args).
        //        exec_step3(
        //               rtObject 
        //            );
        //    }
        //}


        //class afterCreateInstanceData : baseinstancecallbacker
        //{

        //    public override void call(object args)
        //    {
        //        ((InstanceCreator)this.args).
        //            exec_step2(obj,objScope,rtObject);
        //    }
        //}


        //class afterCreateOutScope : baseinstancecallbacker
        //{
        //    public override void call(object args)
        //    {
        //        ((InstanceCreator)this.args). 
        //            exec_step1();
        //    }
        //}

        //class afterCreateStaticInstance : baseinstancecallbacker
        //{
        //    public override void call(object args)
        //    {
              
        //        InstanceCreator ic = ((InstanceCreator)this.args);

        //        ic.set_Class_constructor();
        //    }
        //}
    }











    

    //abstract class baseinstancecallbacker : IBlockCallBack
    //{
    //    public object args
    //    {
    //        get
    //        ;

    //        set
    //        ;
    //    }

    //    public ASBinCode.rtti.Object obj { get; set; }

    //    public ASBinCode.RunTimeScope objScope { get; set; }

    //    public ASBinCode.rtData.rtObject rtObject { get; set; }

    //    public abstract void call(object args);

    //    public void noticeRunFailed()
    //    {
    //        //throw new NotImplementedException();
    //        InstanceCreator ic = (InstanceCreator)args;
    //        if (ic.constructorCaller != null)
    //        {
    //            ic.constructorCaller.noticeRunFailed();
    //            ic.constructorCaller = null;
    //        }
			
    //        if (ic.callbacker != null)
    //        {
    //            ic.callbacker.noticeRunFailed();
    //            ic.callbacker = null;
    //        }
    //    }
    //}

}
