using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtti;

namespace ASRuntime.operators
{

    /// <summary>
    /// 创建类的实例
    /// </summary>
    class OpCreateInstance
    {
        private static ASBinCode.rtti.Class getClass(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var rv = step.arg1.getValue(frame.scope);

            if (rv.rtType > RunTimeDataType.unknown)
            {
                var _class = ((ASBinCode.rtData.rtObject)rv).value._class;

                if (_class.instanceClass == null)
                {
                    frame.throwError(
                        new error.InternalError(step.token,
                                    "不是Class类型对象不能new"
                                    )
                        );
                    return null;
                }
                _class = _class.instanceClass;
                return _class;
            }
            else
            {
                frame.throwError(
                    new error.InternalError(step.token,
                                "此类型不能new" + rv.rtType
                                )
                    );
                return null;
            }
        }

        public static void prepareConstructorClassArgements(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var rv = step.arg1.getValue(scope);

            if (rv.rtType > RunTimeDataType.unknown)
            {
                var _class = getClass(player, frame, step, scope);
                if (_class != null)
                {
                    frame.instanceCreator = new InstanceCreator(player, frame, step, step.token, _class);
                    if (_class.constructor != null)
                    {
                        frame.instanceCreator.prepareConstructorArgements();
                    }
                    //if (_class.constructor != null)
                    //{
                    //    ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[_class.constructor_functionid];
                    //    ASBinCode.rtti.FunctionSignature signature = funcDefine.signature;

                    //    frame.funCaller = new FunctionCaller(player, frame, step.token);
                    //    frame.funCaller.toCallFunc = funcDefine;
                    //    frame.funCaller.createParaScope();

                    //}
                }
            }
            else
            {
                if (rv.rtType == RunTimeDataType.rt_function)
                {
                    ASBinCode.rtData.rtFunction func = (ASBinCode.rtData.rtFunction)rv;
                    if (func.ismethod)
                    {
                        frame.throwError(new error.InternalError(step.token, 
                            "Method cannot be used as a constructor.", 
                            new ASBinCode.rtData.rtString("Method cannot be used as a constructor.")));
                    }
                    else
                    {

                        //***创建一个Object对象，创建完毕之后，使用此函数执行初始化。将此对象作为此函数的this指针执行一次。
                        var _class = player.swc.classes[0];
                        frame.instanceCreator = new InstanceCreator(player, frame, step, step.token, _class);
                        frame.instanceCreator.constructor = (ASBinCode.rtData.rtFunction)rv;
                        frame.instanceCreator.prepareConstructorArgements();
                    }

                }
                else
                {
                    frame.throwError(new error.InternalError( step.token,"原始类型转对象未实现",new ASBinCode.rtData.rtString("原始类型转对象未实现") ));
                }
            }
            frame.endStep(step);
        }

        public static void push_parameter_class(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (frame.instanceCreator.constructorCaller == null)
            {
                frame.throwError(
                    new error.InternalError(step.token,
                                "没有初始化构造函数参数就推送了参数"
                                )
                    );
            }
            else
            {
                int id = ((ASBinCode.rtData.rtInt)step.arg2.getValue(frame.scope)).value;
                IRunTimeValue arg = step.arg1.getValue(frame.scope);

                frame.instanceCreator.push_parameter(arg, id);
                
            }
            frame.endStep(step);
        }

        public static void prepareConstructorArgements(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            var rv = step.arg1.getValue(frame.scope);
            int classid = ((ASBinCode.rtData.rtInt)rv).value;

            frame.instanceCreator = new InstanceCreator(player, frame, step, step.token, player.swc.classes[classid]);
            frame.instanceCreator.prepareConstructorArgements();
            //ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[player.swc.classes[classid].constructor_functionid];
            //ASBinCode.rtti.FunctionSignature signature = funcDefine.signature;

            //frame.funCaller = new FunctionCaller(player, frame, step.token);
            //frame.funCaller.toCallFunc = funcDefine;
            //frame.funCaller.createParaScope();

            frame.endStep(step);
        }

        //abstract class  baseinstancecallbacker : IBlockCallBack
        //{
        //    public object args
        //    {
        //        get
        //        ;

        //        set
        //        ;
        //    }

        //    public ASBinCode.rtti.Object obj { get; set; }

        //    public ASBinCode.IRunTimeScope objScope { get; set; }

        //    public ASBinCode.rtData.rtObject rtObject { get; set; }

        //    public abstract void call(object args);
            
        //}

        //class afterCreateStaticInstance : baseinstancecallbacker
        //{
           
        //    public override void call(object args)
        //    {
        //        object[] a = (object[])args;

        //        exec_step0(
        //            (Player)a[0],
        //             (StackFrame)a[1],
        //              (ASBinCode.OpStep)a[2],
        //               (ASBinCode.rtti.Class)a[3],
        //                (ASBinCode.IRunTimeScope)a[4]
        //            );
        //    }
        //}

        //class afterCreateOutScope : baseinstancecallbacker
        //{
            
            
        //    public override void call(object args)
        //    {
        //        object[] a = (object[])args;

        //        exec_step1(
        //            (Player)a[0],
        //             (StackFrame)a[1],
        //              (ASBinCode.OpStep)a[2],
        //               (ASBinCode.rtti.Class)a[3],
        //                (ASBinCode.IRunTimeScope)a[4]
        //            );
        //    }
        //}

        //class afterCreateInstanceData : baseinstancecallbacker
        //{
            
        //    public override void call(object args)
        //    {
        //        object[] a = (object[])args;

        //        exec_step2(
        //            (Player)a[0],
        //             (StackFrame)a[1],
        //              (ASBinCode.OpStep)a[2],
        //               obj,objScope,
        //                (ASBinCode.IRunTimeScope)a[4],
        //                rtObject
        //            );
        //    }
        //}

        //class afterCallConstructor : baseinstancecallbacker
        //{
        //    public override void call(object args)
        //    {
        //        object[] a = (object[])args;
        //        exec_step3(
                    
        //              (ASBinCode.OpStep)a[0],
        //               obj,objScope,
        //                (ASBinCode.IRunTimeScope)a[1],
        //                (StackFrame)a[2]
        //            );
        //    }
        //}

        public static void init_static(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            int classid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(scope)).value;
            ASBinCode.rtti.Class as3class = player.swc.classes[classid];
            //init_static_class(player, frame, as3class,step.token, scope);

            InstanceCreator ic = new InstanceCreator(player, frame, step, step.token, as3class);
            ic.init_static_class(as3class);

            frame.endStep(step);
        }

        //private static bool init_static_class(Player player, StackFrame frame, Class as3class,SourceToken token, 
        //    ASBinCode.IRunTimeScope scope)
        //{
        //    if (!player.static_instance.ContainsKey(as3class.staticClass.classid))
        //    {

        //        ASBinCode.IRunTimeScope objScope;
        //        ASBinCode.rtti.Object obj = makeObj(player, frame, as3class.staticClass,token,
        //            null, out objScope).value;

        //        player.static_instance.Add(as3class.staticClass.classid,
        //            new ASBinCode.rtData.rtObject(obj, objScope));

        //        return player.step_toblockend();
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}


        public static void exec(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            //int classid = ((ASBinCode.rtData.rtInt)step.arg1.getValue(scope)).value;
            //ASBinCode.rtti.Class as3class = player.swc.classes[classid];
            //exec_createinstance(as3class, player, frame, step, scope);

            frame.instanceCreator.objectResult = step.reg.getISlot(scope);
            frame.instanceCreator.step = step;
            frame.instanceCreator.token = step.token;

            

            BlockCallBackBase cb = new BlockCallBackBase();
            cb.args = frame;
            cb.setCallBacker(objcreated);

            frame.instanceCreator.callbacker = cb;
            frame.instanceCreator.createInstance();
        }

        public static void exec_instanceClass(Player player, StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            frame.instanceCreator.objectResult = step.reg.getISlot(scope);
            frame.instanceCreator.step = step;
            frame.instanceCreator.token = step.token;

            BlockCallBackBase cb = new BlockCallBackBase();
            cb.args = frame;
            cb.setCallBacker(objcreated);

            frame.instanceCreator.callbacker = cb;
            frame.instanceCreator.createInstance();


            //var _class = getClass(player, frame, step, scope);
            //if (_class != null)
            //{
            //    //exec_createinstance(_class, player, frame, step, scope);



            //}
            //else
            //{
            //    frame.endStep(step);
            //}
        }
        private static void objcreated(BlockCallBackBase sender,object args)
        {
            ((StackFrame)sender.args).instanceCreator = null;
            ((StackFrame)sender.args).endStep();
        }


        //private  static void exec_createinstance(Class as3class, Player player, StackFrame frame ,ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        //{

        //    //创建静态对象
        //    if (!player.static_instance.ContainsKey(as3class.staticClass.classid))
        //    {
        //        object[] objArgs = new object[5];
        //        objArgs[0] = player;
        //        objArgs[1] = frame;
        //        objArgs[2] = step;
        //        objArgs[3] = as3class;
        //        objArgs[4] = scope;

        //        afterCreateStaticInstance callbacker = new afterCreateStaticInstance();
        //        callbacker.args = objArgs;

        //        ASBinCode.IRunTimeScope objScope;
        //       var obj= makeObj(player, frame, as3class.staticClass, step.token, callbacker, out objScope);

        //        player.static_instance.Add(as3class.staticClass.classid,
        //            obj);
        //    }
        //    else
        //    {
        //        //exec_step1(player,frame,step,as3class,scope);
        //        exec_step0(player, frame, step,as3class, scope);
        //    }
        //}

        //private static void exec_step0(Player player, StackFrame frame, 
        //    ASBinCode.OpStep step, 
        //    ASBinCode.rtti.Class as3class, 
        //    ASBinCode.IRunTimeScope scope)
        //{
        //    Class cls;
        //    if (as3class.mainClass == null)
        //    {
        //        cls = as3class;
        //    }
        //    else
        //    {
        //        cls = as3class.mainClass;
        //    }

        //    if (!player.outpackage_runtimescope.ContainsKey(cls.classid))
        //    {
        //        ASBinCode.CodeBlock codeblock = player.swc.blocks[cls.outscopeblockid];

        //        object[] objArgs = new object[5];
        //        objArgs[0] = player;
        //        objArgs[1] = frame;
        //        objArgs[2] = step;
        //        objArgs[3] = as3class;
        //        objArgs[4] = scope;

        //        afterCreateOutScope callbacker = new afterCreateOutScope();
        //        callbacker.args = objArgs;

        //        HeapSlot[] globaldata = player.genHeapFromCodeBlock(codeblock);
        //        Global_Object global= Global_Object.formCodeBlock(codeblock, globaldata,player.swc.classes[0]);
        //        ASBinCode.rtData.rtObject globalObj = new ASBinCode.rtData.rtObject(global, null);

        //        ASBinCode.IRunTimeScope rtscope= player.CallBlock(
        //            codeblock, globaldata, null,
        //            player.static_instance[cls.staticClass.classid].objScope,
                    
        //            step.token, 
        //            callbacker,
        //            globalObj
        //            );

        //        globalObj.objScope = rtscope;
        //        player.outpackage_runtimescope.Add(cls.classid, rtscope);

        //        {
        //            var slot = new HeapSlot();
        //            slot.directSet(player.static_instance[cls.staticClass.classid]);
        //            global.createproperty(cls.name, slot);
        //        }
        //        for (int i = 0; i < player.swc.classes.Count; i++)
        //        {
        //            if (player.swc.classes[i].mainClass == cls)
        //            {
        //                if (init_static_class(player, frame, player.swc.classes[i], step.token, scope))
        //                {
        //                    var slot = new HeapSlot();
        //                    slot.directSet(player.static_instance[player.swc.classes[i].staticClass.classid]);
        //                    global.createproperty(player.swc.classes[i].name, slot);
        //                }
        //                else
        //                {
        //                    //***出错了
        //                    frame.endStep(step);
        //                    return;
        //                }
        //            }
        //        }

        //    }
        //    else
        //    {
        //        exec_step1(player, frame, step, as3class, scope);
        //    }
        //}


        //private static void exec_step1(Player player, StackFrame frame, ASBinCode.OpStep step, 
        //    ASBinCode.rtti.Class as3class, ASBinCode.IRunTimeScope scope)
        //{
        //    object[] objArgs = new object[5];
        //    objArgs[0] = player;
        //    objArgs[1] = frame;
        //    objArgs[2] = step;
        //    objArgs[3] = null;
        //    objArgs[4] = scope;

        //    afterCreateInstanceData callbacker = new afterCreateInstanceData();
        //    callbacker.args = objArgs;

        //    ASBinCode.IRunTimeScope objScope;
        //    makeObj(player,frame, as3class, step.token, callbacker,out objScope);

        //}

        //private static void exec_step2(Player player, StackFrame frame, ASBinCode.OpStep step,
        //    ASBinCode.rtti.Object obj,IRunTimeScope objScope, ASBinCode.IRunTimeScope scope ,ASBinCode.rtData.rtObject _object )
        //{
        //    //***添加Object的动态对象****
        //    if (obj._class.classid == 0 && obj._class.staticClass !=null)
        //    {
        //        DynamicObject dobj = (DynamicObject)obj;
        //        Global_Object global = (Global_Object)objScope.parent.this_pointer.value;

        //        dobj.createproperty("toString", new DynamicPropertySlot(_object,false));
        //        dobj["toString"].directSet(global["toString"].getValue());

        //        dobj.createproperty("valueOf", new DynamicPropertySlot(_object, false));
        //        dobj["valueOf"].directSet(global["valueOf"].getValue());
        //    }


        //    //调用构造函数
        //    if (obj._class.constructor != null)
        //    {
        //        ASBinCode.rtData.rtFunction function =
        //            (ASBinCode.rtData.rtFunction)obj._class.constructor.bindField.getValue(objScope);
        //            //(ASBinCode.rtData.rtFunction)obj.memberData[obj._class.constructor.index].getValue();
                

        //        HeapSlot _temp = new HeapSlot();
        //        frame.funCaller.returnSlot = _temp;
        //        frame.funCaller.function = function;

        //        object[] objArgs = new object[5];
        //        objArgs[0] = step;
        //        objArgs[1] = scope;
        //        objArgs[2] = frame;

        //        afterCallConstructor callbacker = new afterCallConstructor();
        //        callbacker.args = objArgs;
        //        callbacker.obj = obj;
        //        callbacker.objScope = objScope;

        //        frame.funCaller.callbacker = callbacker;
        //        frame.funCaller.call();

        //        frame.funCaller = null;

                
        //    }
        //    else
        //    {
        //        exec_step3(step, obj,objScope, scope , frame);
        //    }
        //}

        //private static void exec_step3(ASBinCode.OpStep step, ASBinCode.rtti.Object obj, ASBinCode.IRunTimeScope objscope, ASBinCode.IRunTimeScope scope ,StackFrame frame )
        //{
        //    step.reg.getISlot(scope).directSet(new ASBinCode.rtData.rtObject(obj,objscope));
        //    frame.endStep(step);
        //}


        //private static ASBinCode.rtData.rtObject makeObj(Player player, StackFrame frame,
        //    ASBinCode.rtti.Class cls,SourceToken token,
        //    baseinstancecallbacker callbacker,out ASBinCode.IRunTimeScope objScope)
        //{
        //    ASBinCode.rtti.Object obj=null;// = new ASBinCode.rtti.Object(cls);
        //    if (cls.dynamic)
        //    {
        //        obj = new DynamicObject(cls);
        //    }
        //    else
        //    {
        //        obj = new ASBinCode.rtti.Object(cls);
        //    }

        //    obj.memberData = new ObjectMemberSlot[ cls.classMembers.Count ];

        //    var result = new ASBinCode.rtData.rtObject(obj, null);

        //    for (int i = 0; i < obj.memberData.Length; i++)
        //    {
        //        obj.memberData[i] = new ObjectMemberSlot(result);
        //        if(cls.classMembers[i].defaultValue==null)
        //        {
        //            obj.memberData[i].directSet(TypeConverter.getDefaultValue(cls.classMembers[i].valueType).getValue(null));
        //        }
        //    }

        //    ASBinCode.CodeBlock codeblock = player.swc.blocks[cls.blockid];

        //    if (callbacker != null)
        //    {
        //        callbacker.obj = obj;
        //    }

        //    ASBinCode.IRunTimeScope outpackagescope = null;
        //    if (cls.staticClass != null)
        //    {
        //        if (cls.mainClass == null)
        //        {
        //            outpackagescope = player.outpackage_runtimescope[cls.classid];
        //        }
        //        else
        //        {
        //            outpackagescope = player.outpackage_runtimescope[cls.mainClass.classid];
        //        }
        //    }

            

        //    objScope = player.CallBlock(codeblock, 
        //        (ObjectMemberSlot[])obj.memberData, null,outpackagescope, token,callbacker
        //        ,
        //        result
        //        );

        //    result.objScope = objScope;
        //    if (callbacker != null)
        //    {
        //        callbacker.rtObject = result;
        //        callbacker.objScope = objScope;
                
        //    }
            
        //    return result;
        //}



    }
}
