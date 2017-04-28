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

        public FunctionCaller constructorCaller;

        public ASBinCode.rtData.rtObject objectResult;

        /// <summary>
        /// 是否是通过Function对象创建
        /// </summary>
        public ASBinCode.rtData.rtObject constructor;
        private FunctionCaller _function_constructor;

        public InstanceCreator(Player player, StackFrame invokerFrame,OpStep step , SourceToken token,ASBinCode.rtti.Class _class)
        {
            this.player = player;
            this.invokerFrame = invokerFrame;
            this.token = token;
            this._class = _class;

            
        }

        public void prepareConstructorArgements()
        {
            int classid = _class.classid;

            
            ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[player.swc.classes[classid].constructor_functionid];
            ASBinCode.rtti.FunctionSignature signature = funcDefine.signature;

            constructorCaller = new FunctionCaller(player, invokerFrame, token);
            constructorCaller.toCallFunc = funcDefine;
            constructorCaller._tempSlot = invokerFrame._tempSlot1;
            constructorCaller.createParaScope();

            if (constructor != null)
            {
                _function_constructor = new FunctionCaller(player, invokerFrame, token);
                _function_constructor._tempSlot = invokerFrame._tempSlot1;
                _function_constructor.toCallFunc = 
                    player.swc.functions[ ((ASBinCode.rtData.rtFunction)
                    TypeConverter.ObjectImplicit_ToPrimitive( constructor)).functionId];

                _function_constructor.createParaScope();
            }

        }

        public void push_parameter(IRunTimeValue arg,int id)
        {
            if (constructor == null)
            {
                constructorCaller.pushParameter(arg, id);
            }
            else
            {
                _function_constructor.pushParameter(arg, id);
            }
        }


        public bool init_static_class(Class cls)
        {
            if (!player.static_instance.ContainsKey(cls.staticClass.classid))
            {
                int f = player.getRuntimeStackFlag();

                ASBinCode.IRunTimeScope objScope;
                ASBinCode.rtti.Object obj = makeObj(cls.staticClass,
                    null, out objScope).value;

                player.static_instance.Add(cls.staticClass.classid,
                    new ASBinCode.rtData.rtObject(obj, objScope));

                if (cls.super != null)
                {
                    bool s = init_static_class(cls.super);

                    if (s)
                    {
                        ((DynamicObject)obj)._prototype_ = (DynamicObject)(player.static_instance[cls.super.staticClass.classid]).value;

                        bool result= player.step_toStackflag(f);

                        if (cls.classid > 0)
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
                    bool result= player.step_toStackflag(f);

                    return result;
                }
                
            }
            else
            {
                return true;
            }
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

                ASBinCode.IRunTimeScope objScope;
                var obj = makeObj(_class.staticClass, callbacker, out objScope);

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
            if (_class.classid > 0)
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

            ASBinCode.IRunTimeScope rtscope = player.CallBlock(
                codeblock, globaldata, null,
                null, //player.static_instance[cls.staticClass.classid].objScope,

                step.token,
                cb,
                globalObj,
                RunTimeScopeType.outpackagemember
                );

            globalObj.objScope = rtscope;
            player.outpackage_runtimescope.Add(cls.classid, rtscope);
            {
                var slot = new DynamicPropertySlot(globalObj,true);
                slot.directSet(player.static_instance[cls.staticClass.classid]);
                global.createproperty(cls.name, slot);
            }
            for (int i = 0; i < player.swc.classes.Count; i++)
            {
                if (player.swc.classes[i].mainClass == cls)
                {
                    if (init_static_class(player.swc.classes[i]))
                    {
                        var slot = new DynamicPropertySlot(globalObj,true);
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

            ASBinCode.IRunTimeScope objScope;
            makeObj(_class, callbacker, out objScope);
        }

        private void exec_step2(
            ASBinCode.rtti.Object obj, 
            IRunTimeScope objScope, ASBinCode.rtData.rtObject _object)
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
                    dobj.createproperty("constructor", new DynamicPropertySlot(_object, false));
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
                //(ASBinCode.rtData.rtFunction)obj.memberData[obj._class.constructor.index].getValue();


                HeapSlot _temp = new HeapSlot();
                constructorCaller.returnSlot = _temp;
                constructorCaller.function = function;


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
                }
            }
            else
            {
                HeapSlot _temp = new HeapSlot();
                _function_constructor.returnSlot = _temp;
                _function_constructor.function = (ASBinCode.rtData.rtFunction)TypeConverter.ObjectImplicit_ToPrimitive(constructor).Clone();
                _function_constructor.function.setThis(rtobject);

                BlockCallBackBase cb = new BlockCallBackBase();
                cb.args = rtobject;
                cb.setCallBacker(_finalStep);

                _function_constructor.callbacker = cb;
                _function_constructor.call();
            }
        }

        private void _finalStep(BlockCallBackBase sender,object args)
        {
            objectResult = (ASBinCode.rtData.rtObject)sender.args;
            //objectResult.directSet((ASBinCode.rtData.rtObject)sender.args);
            if (callbacker != null)
            {
                callbacker.call(this);
            }
        }




        private ASBinCode.rtData.rtObject makeObj(
            ASBinCode.rtti.Class cls,
            baseinstancecallbacker callbacker, out ASBinCode.IRunTimeScope objScope)
        {
            
            ASBinCode.rtti.Object obj = null;// = new ASBinCode.rtti.Object(cls);
            if (
                player.swc.DictionaryClass !=null
                &&
                ClassMemberFinder.isInherits(cls, player.swc.DictionaryClass))
            {
                obj = new DictionaryObject(cls);
            }
            else if (cls.dynamic)
            {
                obj = new DynamicObject(cls);
            }
            else if (cls.isUnmanaged)
            {
                obj = new HostedObject(cls);
            }
            else
            {
                obj = new ASBinCode.rtti.Object(cls);
            }
           

            obj.memberData = new ObjectMemberSlot[cls.fields.Count];

            var result = new ASBinCode.rtData.rtObject(obj, null);

            for (int i = 0; i < obj.memberData.Length; i++)
            {
                obj.memberData[i] = new ObjectMemberSlot(result);

                if (cls.fields[i].defaultValue == null)
                {
                    obj.memberData[i].directSet(TypeConverter.getDefaultValue(cls.fields[i].valueType).getValue(null));
                }

                ((ObjectMemberSlot)obj.memberData[i]).isConstMember = cls.fields[i].isConst;

            }

            ASBinCode.CodeBlock codeblock = player.swc.blocks[cls.blockid];

            objScope = player.CallBlock(codeblock,
                (ObjectMemberSlot[])obj.memberData, null,  
                null
                , token, callbacker
                ,
                result, RunTimeScopeType.objectinstance
                );

            result.objScope = objScope;

            //***把父类的初始化函数推到栈上去***
            var ss = cls.super;
            while (ss != null)
            {
                player.CallBlock(player.swc.blocks[ss.blockid],
                    (ObjectMemberSlot[])obj.memberData,
                    null, null, token, null, result, RunTimeScopeType.objectinstance
                    );

                ss = ss.super;
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

        public ASBinCode.IRunTimeScope objScope { get; set; }

        public ASBinCode.rtData.rtObject rtObject { get; set; }

        public abstract void call(object args);

    }

}
