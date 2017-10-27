
using ASBinCode;
using ASBinCode.rtData;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpForIn
    {
        public static void forin_get_enumerator(StackFrame frame, OpStep step, RunTimeScope scope)
        {

            var player = frame.player;

            SLOT slot = step.reg.getSlot(scope, frame);
            ASBinCode.rtti.HostedDynamicObject saveObj = new HostedDynamicObject(player.swc.ObjectClass);
            rtObject save = new rtObject(saveObj, null);
            slot.directSet(save);

            var obj = step.arg1.getValue(scope, frame);
            if (obj.rtType > RunTimeDataType.unknown)
            {
                rtObject rtObj = (rtObject)obj;

                if (ClassMemberFinder.isInherits(rtObj.value._class,
                    player.swc.primitive_to_class_table[RunTimeDataType.rt_array]))
                {
                    rtArray arr = (rtArray)rtObj.value.memberData[0].getValue();
                    saveObj.hosted_object = getArrayForIn(arr.innerArray);
                }
                else if (player.swc.dict_Vector_type.ContainsKey(rtObj.value._class))
                {
                    saveObj.hosted_object = getArrayForIn(((Vector_Data)((HostedObject)rtObj.value).hosted_object).innnerList);
                }
                else if (ClassMemberFinder.isImplements(rtObj.value._class, player.swc.IEnumerableInterface))
                {
                    //***调用getIEnumerator方法****
                    var movenext = ClassMemberFinder.find(
                        frame.player.swc.IEnumerableInterface, "getEnumerator", 
                        frame.player.swc.IEnumerableInterface);

                    var method = ((InterfaceMethodGetter)movenext.bindField).getMethod(
                        rtObj);

                    //***调用方法***
                    var funCaller = player.funcCallerPool.create(frame.player, frame, step.token);
                    //funCaller.releaseAfterCall = true;
                    funCaller.SetFunction((rtFunction)method);((rtFunction)method).Clear();
                    funCaller.loadDefineFromFunction();
                    if (!funCaller.createParaScope()) { return; }

                    funCaller._tempSlot = frame._tempSlot1;
                    funCaller.returnSlot = frame._tempSlot1;

                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.setCallBacker(_getEnumerator_callbacker);
                    cb.step = step;
                    cb.args = frame;
                    cb.scope = scope;

                    funCaller.callbacker = cb;
                    funCaller.call();

                    return;


                }
                else if (ClassMemberFinder.isImplements(
                    rtObj.value._class, player.swc.IEnumeratorInterface))
                {
                    saveObj.hosted_object = rtObj;
                }
                else
                {
                    IEnumerator<RunTimeValueBase> forinenum = getForinIEnumerator(player, rtObj.value, frame, step, scope);
                    saveObj.hosted_object = forinenum;
                }
            }

            frame.endStep();
        }

        private static void _getEnumerator_callbacker(BlockCallBackBase sender, object args)
        {
            StackFrame frame = (StackFrame)sender.args;
            rtObject save = (rtObject)sender.step.reg.getValue(sender.scope, frame);
            HostedDynamicObject saveObj = (HostedDynamicObject)save.value;
            saveObj.hosted_object = frame._tempSlot1.getValue();


            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void foreach_get_enumerator(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var player = frame.player;

            SLOT slot= step.reg.getSlot(scope, frame);
            ASBinCode.rtti.HostedDynamicObject saveObj = new ASBinCode.rtti.HostedDynamicObject(player.swc.ObjectClass);
            rtObject save = new rtObject(saveObj, null);
            slot.directSet(save);


            var obj = step.arg1.getValue(scope, frame);
            if (obj.rtType > RunTimeDataType.unknown)
            {
                rtObject rtObj = (rtObject)obj;

                if (ClassMemberFinder.isInherits(rtObj.value._class,
                    player.swc.primitive_to_class_table[RunTimeDataType.rt_array]))
                {
                    rtArray arr = (rtArray)rtObj.value.memberData[0].getValue();
                    saveObj.hosted_object = getArrayForEach(arr.innerArray);
                    
                }
                else if (player.swc.dict_Vector_type.ContainsKey(rtObj.value._class))
                {
                    saveObj.hosted_object = getArrayForEach(((Vector_Data)((HostedObject)rtObj.value).hosted_object).innnerList);
                }
                else if (ClassMemberFinder.isImplements(rtObj.value._class, player.swc.IEnumerableInterface))
                {
                    //***调用getIEnumerator方法****
                    var movenext = ClassMemberFinder.find(
                        frame.player.swc.IEnumerableInterface, "getEnumerator",
                        frame.player.swc.IEnumerableInterface);

                    var method = ((InterfaceMethodGetter)movenext.bindField).getMethod(
                        rtObj);

                    //***调用方法***
                    var funCaller = player.funcCallerPool.create(frame.player, frame, step.token);
                    //funCaller.releaseAfterCall = true;
                    funCaller.SetFunction((rtFunction)method);((rtFunction)method).Clear();
                    funCaller.loadDefineFromFunction();
                    if (!funCaller.createParaScope()) { return; }

                    funCaller._tempSlot = frame._tempSlot1;
                    funCaller.returnSlot = frame._tempSlot1;

                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.setCallBacker(_getEnumerator_callbacker);
                    cb.step = step;
                    cb.args = frame;
                    cb.scope = scope;

                    funCaller.callbacker = cb;
                    funCaller.call();

                    return;


                }
                else if (ClassMemberFinder.isImplements(
                    rtObj.value._class, player.swc.IEnumeratorInterface))
                {
                    saveObj.hosted_object = rtObj;
                }
                else
                {
                    IEnumerator<RunTimeValueBase> forinenum = getForEach_IEnumerator(player, rtObj.value, frame, step, scope);
                    saveObj.hosted_object = forinenum;
                }
            }

            frame.endStep();
        }


        public static void enumerator_movenext(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            //StackSlot slot = (StackSlot)((Register)step.arg1).getSlot(scope);

            
            rtObject save = (rtObject)(step.arg1).getValue(scope,frame);
            HostedDynamicObject saveObj = (HostedDynamicObject)save.value;

            IEnumerator<RunTimeValueBase> enumerator = saveObj.hosted_object as IEnumerator<RunTimeValueBase>;


            if ( enumerator !=null && enumerator.MoveNext() )//slot.cache_enumerator !=null && slot.cache_enumerator.MoveNext())
            {
                step.reg.getSlot(scope, frame).setValue(rtBoolean.True);
            }
            else
            {
                if (saveObj.hosted_object is rtObject)  //是否是接口
                {
                    var movenext = ClassMemberFinder.find(frame.player.swc.IEnumeratorInterface, "moveNext", frame.player.swc.IEnumeratorInterface);
                    var method=((InterfaceMethodGetter)movenext.bindField).getMethod(
                        (((rtObject)saveObj.hosted_object)) );

                    //***调用方法***
                    var funCaller = frame.player.funcCallerPool.create(frame.player, frame, step.token);
                    //funCaller.releaseAfterCall = true;
                    funCaller.SetFunction((ASBinCode.rtData.rtFunction)method);((ASBinCode.rtData.rtFunction)method).Clear();
                    funCaller.loadDefineFromFunction();
                    if (!funCaller.createParaScope()) { return; }

                    funCaller._tempSlot = step.reg.getSlot(scope, frame);
                    funCaller.returnSlot = step.reg.getSlot(scope, frame);

                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.setCallBacker(_enumerator_operator_callbacker);
                    cb.step = step;
                    cb.args = frame;

                    funCaller.callbacker = cb;
                    funCaller.call();

                    return;

                }
                else
                {
                    step.reg.getSlot(scope, frame).setValue(rtBoolean.False);
                }
            }

            frame.endStep(step);
        }

        private static void _enumerator_operator_callbacker(BlockCallBackBase sender, object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void enumerator_current(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            //StackSlot slot = (StackSlot)((Register)step.arg1).getSlot(scope);

            rtObject save = (rtObject)(step.arg1).getValue(scope,frame);
            HostedDynamicObject saveObj = (HostedDynamicObject)save.value;

            IEnumerator<RunTimeValueBase> enumerator = saveObj.hosted_object as IEnumerator<RunTimeValueBase>;


            //IEnumerator<RunTimeValueBase> enumerator = scope.cache_enumerator as  IEnumerator<RunTimeValueBase>;
            if (enumerator != null)
            {
                step.reg.getSlot(scope, frame).directSet(enumerator.Current);
            }
            else
            {
                if (saveObj.hosted_object is rtObject)  //是否是接口
                {
                    var movenext = ClassMemberFinder.find(frame.player.swc.IEnumeratorInterface, "current", frame.player.swc.IEnumeratorInterface);
                    var method = 
                        ((ClassPropertyGetter)movenext.bindField).getter.getMethod(((rtObject)saveObj.hosted_object));

                    //***调用方法***
                    var funCaller = frame.player.funcCallerPool.create(frame.player, frame, step.token);
                    //funCaller.releaseAfterCall = true;
                    funCaller.SetFunction((ASBinCode.rtData.rtFunction)method);((ASBinCode.rtData.rtFunction)method).Clear();
                    funCaller.loadDefineFromFunction();
                    if (!funCaller.createParaScope()) { return; }

                    funCaller._tempSlot = step.reg.getSlot(scope, frame);
                    funCaller.returnSlot = step.reg.getSlot(scope, frame);

                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.setCallBacker(_enumerator_operator_callbacker);
                    cb.step = step;
                    cb.args = frame;

                    funCaller.callbacker = cb;
                    funCaller.call();

                    return;

                }
            }

            frame.endStep(step);
        }

        public static void enumerator_close(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            //StackSlot slot = (StackSlot)((Register)step.arg1).getSlot(scope);

            rtObject save = (rtObject)(step.arg1).getValue(scope,frame);
            HostedDynamicObject saveObj = (HostedDynamicObject)save.value;

            //if (scope.cache_enumerator != null)
            {
                IEnumerator<RunTimeValueBase> enumerator = saveObj.hosted_object as IEnumerator<RunTimeValueBase>;
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }

                saveObj.hosted_object = null;
            }
            frame.endStep(step);
        }

        private static IEnumerator<RunTimeValueBase> getArrayForIn(IList<RunTimeValueBase> arr)
        {
            int length = arr.Count;
            for (int i = 0; i < length; i++)
            {
                yield return new rtInt(i);
            }
        }

        private static IEnumerator<RunTimeValueBase> getArrayForEach(IList<RunTimeValueBase> arr)
        {
            int length = arr.Count;
            for (int i = 0; i < length; i++)
            {
                yield return arr[i];
            }
        }

        private static IEnumerator<RunTimeValueBase> getForinIEnumerator(
            Player player,ASBinCode.rtti.Object obj ,StackFrame frame, OpStep step, RunTimeScope scope,
            Dictionary<object, object> visited=null
            )
        {
            if (obj is ASBinCode.rtti.DynamicObject)
            {
                ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj;
                {
                    var k = dobj.eachSlot();
                    while (k.MoveNext())
                    {
                        var c = k.Current;
                        DynamicPropertySlot ds = c as DynamicPropertySlot;
                        if (c != null)
                        {
                            yield return new rtString(ds._propname);
                        }
                    }
                }

                if (obj is DictionaryObject)
                {
                    DictionaryObject dictObj = (DictionaryObject)obj;
                    var k = dictObj.eachDictSlot();
                    while (k.MoveNext())
                    {
                        var c = k.Current;
                        DictionarySlot ds = c as DictionarySlot;
                        if (c != null)
                        {
                            yield return ds._key.key;
                        }
                    }

                }
                if(visited==null)
                    visited = new Dictionary<object, object>();
                //***再到原型链中查找
                if (dobj._prototype_ != null)
                {
                    var protoObj = dobj._prototype_;
                    //****_prototype_的类型，只可能是Function对象或Class对象
                    if (protoObj._class.classid == player.swc.FunctionClass.classid) //Function 
                    {
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[1].getValue()).value;
                        if (visited.ContainsKey(dobj))
                        {
                            yield break;
                        }
                        visited.Add(dobj, null);

                        var res = getForinIEnumerator(player, dobj, frame, step, scope,visited);
                        while (res.MoveNext())
                        {
                            yield return res.Current;
                        }
                    }
                    else if (protoObj._class.classid == 1) //搜索到根Object
                    {
                        //***根Object有继承自Class的prototype,再没有就没有了
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[0].getValue()).value;
                        {
                            var k = dobj.eachSlot();
                            while (k.MoveNext())
                            {
                                var c = k.Current;
                                DynamicPropertySlot ds = c as DynamicPropertySlot;
                                if (c != null)
                                {
                                    yield return new rtString(ds._propname);
                                }
                            }
                        }
                        yield break;
                    }
                    else if (protoObj._class.staticClass == null)
                    {
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[0].getValue()).value;
                        var res = getForinIEnumerator(player, dobj, frame, step, scope);
                        while (res.MoveNext())
                        {
                            yield return res.Current;
                        }
                    }
                    else
                    {

                        frame.throwError((new error.InternalError(step.token,
                             "遭遇了异常的_prototype_"
                             )));
                        yield break;
                    }
                }
            }
            else if (obj is ASBinCode.rtti.Object)
            {
                var dobj = ((ASBinCode.rtti.DynamicObject)
                    frame.player.static_instance[obj._class.staticClass.classid].value);

                dobj = (ASBinCode.rtti.DynamicObject)((rtObject)dobj.memberData[0].getValue()).value;
                var res = getForinIEnumerator(player, dobj, frame, step, scope);
                while (res.MoveNext())
                {
                    yield return res.Current;
                }
            }

            yield break;
        }


        private static IEnumerator<RunTimeValueBase> getForEach_IEnumerator(
            Player player, ASBinCode.rtti.Object obj, StackFrame frame, OpStep step, RunTimeScope scope,
            Dictionary<object,object> visited=null
            )
        {
            if (obj is ASBinCode.rtti.DynamicObject)
            {
                ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj;
                {
                    var k = dobj.eachSlot();
                    while (k.MoveNext())
                    {
                        var c = k.Current;
                        DynamicPropertySlot ds = c as DynamicPropertySlot;
                        if (c != null)
                        {
                            yield return ds.getValue(); //new rtString(ds._propname);
                        }
                    }
                }

                if (obj is DictionaryObject)
                {
                    DictionaryObject dictObj = (DictionaryObject)obj;
                    var k = dictObj.eachDictSlot();
                    while (k.MoveNext())
                    {
                        var c = k.Current;
                        DictionarySlot ds = c as DictionarySlot;
                        if (c != null)
                        {
                            yield return ds.getValue(); //ds._key.key;
                        }
                    }

                }

                if (visited == null)
                    visited = new Dictionary<object, object>();
                //***再到原型链中查找
                if (dobj._prototype_ != null)
                {
                    var protoObj = dobj._prototype_;
                    //****_prototype_的类型，只可能是Function对象或Class对象
                    if (protoObj._class.classid == player.swc.FunctionClass.classid) //Function 
                    {
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[1].getValue()).value;

                        if (visited.ContainsKey(dobj))
                        {
                            yield break;
                        }
                        visited.Add(dobj, null);

                        var res = getForEach_IEnumerator(player, dobj, frame, step, scope,visited);
                        while (res.MoveNext())
                        {
                            yield return res.Current;
                        }
                    }
                    else if (protoObj._class.classid == 1) //搜索到根Object
                    {
                        //***根Object有继承自Class的prototype,再没有就没有了
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[0].getValue()).value;
                        {
                            var k = dobj.eachSlot();
                            while (k.MoveNext())
                            {
                                var c = k.Current;
                                DynamicPropertySlot ds = c as DynamicPropertySlot;
                                if (c != null)
                                {
                                    yield return ds.getValue(); //new rtString(ds._propname);
                                }
                            }
                        }
                        yield break;
                    }
                    else if (protoObj._class.staticClass == null)
                    {
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[0].getValue()).value;
                        var res = getForEach_IEnumerator(player, dobj, frame, step, scope);
                        while (res.MoveNext())
                        {
                            yield return res.Current;
                        }
                    }
                    else
                    {

                        frame.throwError((new error.InternalError(step.token,
                             "遭遇了异常的_prototype_"
                             )));
                        yield break;
                    }
                }
            }
            else if (obj is ASBinCode.rtti.Object)
            {
                var dobj = ((ASBinCode.rtti.DynamicObject)
                    frame.player.static_instance[obj._class.staticClass.classid].value);

                dobj = (ASBinCode.rtti.DynamicObject)((rtObject)dobj.memberData[0].getValue()).value;
                var res = getForEach_IEnumerator(player, dobj, frame, step, scope);
                while (res.MoveNext())
                {
                    yield return res.Current;
                }
            }

            yield break;
        }


    }
}
