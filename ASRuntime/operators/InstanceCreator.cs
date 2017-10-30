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
        public Player player;
        private StackFrame invokerFrame;
        public SourceToken token;
        public OpStep step;

        private ASBinCode.rtti.Class _class;
        public IBlockCallBack callbacker;

        internal FunctionCaller constructorCaller;

        public RunTimeValueBase objectResult;

        /// <summary>
        /// 是否是通过Function对象创建
        /// </summary>
        public ASBinCode.rtData.rtObject constructor;
        private FunctionCaller _function_constructor;

        public InstanceCreator(Player player, StackFrame invokerFrame, SourceToken token, Class _class)
        {
            this.player = player;
            this.invokerFrame = invokerFrame;
            this.token = token;
            this._class = _class;

            
        }
        
        public bool prepareConstructorArgements()
        {
            int classid = _class.classid;

            
            ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[player.swc.classes[classid].constructor_functionid];
            ASBinCode.rtti.FunctionSignature signature = funcDefine.signature;
            
            constructorCaller = player.funcCallerPool.create(player, invokerFrame, token);
            constructorCaller.toCallFunc = funcDefine;
            constructorCaller._tempSlot = invokerFrame._tempSlot1;
            if (!constructorCaller.createParaScope()) { return false; }

            //constructorCaller.releaseAfterCall = true;

            if (constructor != null)
            {
                _function_constructor = player.funcCallerPool.create(player, invokerFrame, token);
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
                    null, out objScope).value;

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
                            ((DynamicObject)((ASBinCode.rtData.rtObject)obj.memberData[0].getValue()).value)["constructor"].directSet(player.static_instance[cls.staticClass.classid]);

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

            //if (!player.static_instance.ContainsKey(cls.staticClass.classid))
            //{
            //    int f = player.getRuntimeStackFlag();

                
            //    ASBinCode.RunTimeScope objScope;
            //    ASBinCode.rtti.Object obj = makeObj(player,token, cls.staticClass,
            //        null, out objScope).value;

            //    player.static_instance.Add(cls.staticClass.classid,
            //        new ASBinCode.rtData.rtObject(obj, objScope));

            //    if (cls.super != null)
            //    {
            //        bool s = init_static_class(cls.super);

            //        if (s)
            //        {
            //            ((DynamicObject)obj)._prototype_ = (DynamicObject)(player.static_instance[cls.super.staticClass.classid]).value;

            //            bool result= player.step_toStackflag(f);

            //            if (cls.classid !=2)
            //            {
            //                ((DynamicObject)((ASBinCode.rtData.rtObject)obj.memberData[0].getValue()).value)["constructor"].directSet(player.static_instance[cls.staticClass.classid]);
                                
            //            }

            //            return result;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        bool result= player.step_toStackflag(f);

            //        return result;
            //    }
                
            //}
            //else
            //{
            //    return true;
            //}
        }


        public void createInstance()
        {
            //if (!player.static_instance.ContainsKey(1))
            //{
            //    var c = player.swc.getClassByRunTimeDataType(RunTimeDataType._OBJECT);
            //    init_static_class(c);

            //    if (!player.outpackage_runtimescope.ContainsKey(c.classid))
            //    {
            //        int flag = player.getRuntimeStackFlag();
            //        make_outpackage_scope(c, null);
            //        player.step_toStackflag(flag);
            //    }
            //}
            //if (!player.static_instance.ContainsKey(3))
            //{
            //    var c = player.swc.getClassByRunTimeDataType(RunTimeDataType._OBJECT + 2);
            //    init_static_class(c);

            //    if (!player.outpackage_runtimescope.ContainsKey(c.classid))
            //    {
            //        int flag = player.getRuntimeStackFlag();
            //        make_outpackage_scope(c, null);
            //        player.step_toStackflag(flag);
            //    }


            //}


            if (!player.static_instance.ContainsKey(_class.staticClass.classid))
            {

                afterCreateStaticInstance callbacker = new afterCreateStaticInstance();
                callbacker.args = this;

                ASBinCode.RunTimeScope objScope;
                var obj = makeObj(player,token, _class.staticClass, callbacker, out objScope);

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

        private void set_Class_constructor()
        {
            if (_class.classid > 2)
            {
                var obj = player.static_instance[_class.staticClass.classid];

                ((DynamicObject)
                    ((ASBinCode.rtData.rtObject)obj.value.memberData[0].getValue()).value)
                    ["constructor"].directSet(player.static_instance[_class.staticClass.classid]);

            }

            exec_step0();
        }

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
                afterCreateOutScope callbacker = new afterCreateOutScope();
                callbacker.args = this;

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



        private void exec_step1()
        {
            afterCreateInstanceData callbacker = new afterCreateInstanceData();
            callbacker.args = this;

            ASBinCode.RunTimeScope objScope;
            makeObj(player,token, _class, callbacker, out objScope);
        }

        private void exec_step2(
            ASBinCode.rtti.Object obj,
            RunTimeScope objScope, ASBinCode.rtData.rtObject _object)
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
            

            //调用构造函数
            if (obj._class.constructor != null)
            {
                ASBinCode.rtData.rtFunction function =
                    (ASBinCode.rtData.rtFunction)((MethodGetterBase)obj._class.constructor.bindField).getConstructor(objScope);
                

                HeapSlot _temp = new HeapSlot();
                constructorCaller.returnSlot = _temp;
				constructorCaller.SetFunction(function);function.Clear();


                afterCallConstructor callbacker = new afterCallConstructor();
                callbacker.args = this;
                callbacker.obj = obj;
                callbacker.objScope = objScope;
                callbacker.rtObject = _object;

                constructorCaller.callbacker = callbacker;
                constructorCaller.call();

                constructorCaller = null;
                
            }
            else
            {
                exec_step3(_object);
            }
        }

        




        private void exec_step3(ASBinCode.rtData.rtObject rtobject)
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
                HeapSlot _temp = new HeapSlot();
                _function_constructor.returnSlot = _temp;
				//_function_constructor.function = (ASBinCode.rtData.rtFunction)TypeConverter.ObjectImplicit_ToPrimitive(constructor).Clone();
				//_function_constructor.function.setThis(rtobject);
				_function_constructor.SetFunction((ASBinCode.rtData.rtFunction)TypeConverter.ObjectImplicit_ToPrimitive(constructor), rtobject);

                BlockCallBackBase cb = player.blockCallBackPool.create();
                cb.args = cb.cacheObjects; //new object[] { rtobject , _temp };
                cb.cacheObjects[0] = rtobject;
                cb.cacheObjects[1] = _temp;

                cb.setCallBacker(_finalStep);

                _function_constructor.callbacker = cb;
                _function_constructor.call();
                _function_constructor = null;
            }
        }

        private void _finalStep(BlockCallBackBase sender,object args)
        {
            object[] a = (object[])sender.args;

            objectResult = (ASBinCode.rtData.rtObject)a[0];

            //***如果有返回值****
            var returnvalue = ((SLOT)a[1]).getValue();
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
                objectResult = returnvalue;
            }

            //objectResult.directSet((ASBinCode.rtData.rtObject)sender.args);
            if (callbacker != null)
            {
                callbacker.call(this);
                callbacker = null;
            }
        }


        internal static LinkSystemObject createLinkObjectValue(Player player, Class cls)
        {
            if (cls.isLink_System)
            {
                ASBinCode.rtti.Object obj = createObject(player.swc, cls);
                return (LinkSystemObject)obj;
            }
            else
            {
                throw new ASRunTimeException();
            }
        }

        internal static ASBinCode.rtData.rtObject createPureHostdOrLinkObject(Player player, Class cls)
        {
            if (cls.isLink_System)
            {
                ASBinCode.rtti.Object obj = createObject(player.swc, cls);
                ASBinCode.rtData.rtObject rtObj = new ASBinCode.rtData.rtObject(obj, null);

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
                throw new ASRunTimeException();
            }
        }

        
        private static ASBinCode.rtti.Object createObject(CSWC swc,Class cls)
        {
            ASBinCode.rtti.Object obj = null;// = new ASBinCode.rtti.Object(cls);

            if (cls.isLink_System)
            {
                //var creator= swc.functions[((ClassMethodGetter)cls.staticClass.linkObjCreator.bindField).functionId];
                //if (creator.native_index == -1)
                //{
                //    creator.native_index = swc.nativefunctionNameIndex[creator.native_name];
                //}
                //var func = swc.nativefunctions[creator.native_index];
                var func = (NativeFunctionBase)swc.class_Creator[cls];
                

                string err;int no;
                ASBinCode.rtData.rtObject rtObj= 
                    func.execute(null, null, cls, out err,out no) as ASBinCode.rtData.rtObject ;
                if (rtObj == null)
                {
                    return null;
                }
                else
                {
                    return rtObj.value;
                }
                //obj = new LinkSystemObject(cls);
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
        private static ASBinCode.rtData.rtObject makeObj(
            Player player,
            SourceToken token,
            ASBinCode.rtti.Class cls,
            baseinstancecallbacker callbacker, out ASBinCode.RunTimeScope objScope)
        {

            ASBinCode.rtti.Object obj = createObject(player.swc, cls);

            var result = new ASBinCode.rtData.rtObject(obj, null);
            if (cls.fields.Count > 0)
            {
                obj.memberData = new ObjectMemberSlot[cls.fields.Count];
                for (int i = 0; i < obj.memberData.Length; i++)
                {
                    obj.memberData[i] = new ObjectMemberSlot(result,player.swc.FunctionClass.getRtType());

                    if (cls.fields[i].defaultValue == null)
                    {
                        obj.memberData[i].directSet(TypeConverter.getDefaultValue(cls.fields[i].valueType).getValue(null,null));
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
                var scope=player.callBlock(player.swc.blocks[ss.blockid],
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



            if (callbacker != null)
            {
                callbacker.obj = obj;
                callbacker.rtObject = result;
                callbacker.objScope = objScope;
            }

            return result;
        }


        class afterCallConstructor : baseinstancecallbacker
        {
            public override void call(object args)
            {
                ((InstanceCreator)this.args).
                exec_step3(
                       rtObject 
                    );
            }
        }


        class afterCreateInstanceData : baseinstancecallbacker
        {

            public override void call(object args)
            {
                ((InstanceCreator)this.args).
                    exec_step2(obj,objScope,rtObject);
            }
        }


        class afterCreateOutScope : baseinstancecallbacker
        {
            public override void call(object args)
            {
                ((InstanceCreator)this.args). 
                    exec_step1();
            }
        }

        class afterCreateStaticInstance : baseinstancecallbacker
        {
            public override void call(object args)
            {
              
                InstanceCreator ic = ((InstanceCreator)this.args);

                ic.set_Class_constructor();
            }
        }
    }











    

    abstract class baseinstancecallbacker : IBlockCallBack
    {
        public object args
        {
            get
            ;

            set
            ;
        }

        public ASBinCode.rtti.Object obj { get; set; }

        public ASBinCode.RunTimeScope objScope { get; set; }

        public ASBinCode.rtData.rtObject rtObject { get; set; }

        public abstract void call(object args);

        public void noticeRunFailed()
        {
            //throw new NotImplementedException();
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
    }

}
