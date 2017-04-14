using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASRuntime.operators
{
    class OpCast
    {


        public static void execCast(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            BlockCallBackBase cb = new BlockCallBackBase();
            cb.setCallBacker(_CastCallBacker);
            cb.args = frame;
            cb.scope = scope;

            CastValue(
                step.arg1.getValue(scope),
                step.regType,
                frame,
                step.token,
                scope,
                step.reg.getISlot(scope)
                ,
                cb
                ,
                false
                );

            //if (!CastValue(
            //    step.arg1.getValue(scope), step.regType, step.reg.getISlot(scope), frame, step.token, scope))
            //{
            //    frame.throwCastException(step.token, step.arg1.getValue(scope).rtType, step.regType);
            //}

            //frame.endStep(step);
        }
        private static void _CastCallBacker(BlockCallBackBase sender, object args)
        {
            if (sender.isSuccess)
            {
                ((StackFrame)sender.args).endStep();
            }
            else
            {
                StackFrame frame = (StackFrame)sender.args;
                OpStep step = frame.block.opSteps[frame.codeLinePtr];
                frame.throwCastException(step.token, step.arg1.getValue(sender.scope).rtType, step.regType);

                frame.endStep();

            }
        }


        public static void CastValue(
            ASBinCode.IRunTimeValue srcValue,
            ASBinCode.RunTimeDataType targetType,
            StackFrame frame,
            ASBinCode.SourceToken token,
            ASBinCode.IRunTimeScope scope,
            ASBinCode.ISLOT storeto,
            BlockCallBackBase callbacker,
            bool igrionValueOf
            )
        {
            if ((srcValue.rtType < ASBinCode.RunTimeDataType._OBJECT || igrionValueOf) && targetType < ASBinCode.RunTimeDataType._OBJECT)
            {
                switch (targetType)
                {
                    case ASBinCode.RunTimeDataType.rt_boolean:
                        {
                            storeto.setValue(
                                TypeConverter.ConvertToBoolean(
                                srcValue, frame, token, true
                            )
                                );
                            callbacker.isSuccess = true;
                            callbacker.call(null);
                            return;
                        }
                    case ASBinCode.RunTimeDataType.rt_int:
                        {
                            storeto.setValue(
                             TypeConverter.ConvertToInt(
                                srcValue, frame, token, true
                            )
                            );
                            callbacker.isSuccess = true;
                            callbacker.call(null);
                            return;
                        }
                    case ASBinCode.RunTimeDataType.rt_uint:
                        {
                            storeto.setValue(
                                TypeConverter.ConvertToUInt(
                                srcValue, frame, token, true
                            )
                                );
                            callbacker.isSuccess = true;
                            callbacker.call(null);
                            return;
                        }
                    case ASBinCode.RunTimeDataType.rt_number:
                        {
                            storeto.setValue(
                                TypeConverter.ConvertToNumber(
                                srcValue, frame, token, true
                            )
                                );
                            callbacker.isSuccess = true;
                            callbacker.call(null);
                            return;
                        }
                    case ASBinCode.RunTimeDataType.rt_string:
                        {
                            string str = TypeConverter.ConvertToString(
                                srcValue, frame, token, true
                            );

                            storeto.setValue(
                                str
                                );
                            callbacker.isSuccess = true;
                            callbacker.call(null);
                            return;
                        }
                    case ASBinCode.RunTimeDataType.rt_void:
                    case ASBinCode.RunTimeDataType.rt_null:
                    case ASBinCode.RunTimeDataType.fun_void:
                        {
                            storeto.directSet(srcValue);
                            callbacker.isSuccess = true;
                            callbacker.call(null);
                            return;
                        }
                    case ASBinCode.RunTimeDataType.rt_function:
                        {
                            if (srcValue.rtType == ASBinCode.RunTimeDataType.rt_function
                                ||
                                srcValue.rtType == ASBinCode.RunTimeDataType.rt_null
                                ||
                                srcValue.rtType == ASBinCode.RunTimeDataType.rt_void
                                )
                            {
                                {
                                    storeto.directSet(srcValue);
                                    callbacker.isSuccess = true;
                                    callbacker.call(null);
                                    return;
                                }
                            }
                            else
                            {
                                {
                                    frame.throwCastException(token, srcValue.rtType, targetType);
                                    frame.endStep();

                                    return;
                                }
                            }
                        }
                    case RunTimeDataType.rt_array:
                        {
                            if (srcValue.rtType == RunTimeDataType.rt_null)
                            {
                                storeto.directSet(srcValue);
                                callbacker.isSuccess = true;
                                callbacker.call(null);
                                return;
                            }
                            else
                            {
                                frame.throwCastException(token, srcValue.rtType, targetType);
                                frame.endStep();

                                return;
                            }
                        }
                    case ASBinCode.RunTimeDataType.unknown:
                        {
                            frame.throwCastException(token, srcValue.rtType, targetType);
                            frame.endStep();
                            return;
                        }
                    default:
                        {
                            frame.throwCastException(token, srcValue.rtType, targetType);
                            frame.endStep();
                            return;
                        }
                }
            }
            else
            {
                if (srcValue.rtType == targetType
                            ||
                            targetType == ASBinCode.RunTimeDataType.rt_void
                            )
                {
                    storeto.directSet(srcValue);
                    callbacker.isSuccess = true;
                    callbacker.call(null);
                }
                else if ((srcValue.rtType == RunTimeDataType.rt_null
                        ||
                        srcValue.rtType == RunTimeDataType.rt_void
                    )
                    &&
                    targetType > RunTimeDataType.unknown //将null赋值给对象
                    )
                {
                    storeto.directSet(rtNull.nullptr);
                    callbacker.isSuccess = true;
                    callbacker.call(null);
                }
                else if (targetType == RunTimeDataType.rt_string)
                {
                    #region toString
                    //***调用toString()
                    ASBinCode.rtData.rtObject obj = (ASBinCode.rtData.rtObject)srcValue;
                    var toStr = (ASBinCode.ClassMemberFinder.find(obj.value._class, "toString", obj.value._class));

                    rtFunction function = null;

                    if (
                        toStr != null
                        && toStr.valueType == RunTimeDataType.rt_function
                        && !toStr.isStatic
                        && toStr.isPublic
                        && !toStr.isConstructor
                        && !toStr.isGetter
                        && !toStr.isSetter

                        )
                    {
                        function = (rtFunction)((ILeftValue)toStr.bindField).getValue(obj.objScope);
                    }
                    else
                    {
                        //***未找到

                        if (obj.value is ASBinCode.rtti.DynamicObject)
                        {
                            ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;
                            bool haserror;
                            var find = OpAccess_Dot.findInProtoType(dobj, "toString", frame, token, out haserror);
                            if (haserror)
                            {
                                frame.endStep();
                                return;
                            }

                            if (find != null)
                            {
                                var prop = find["toString"].getValue();
                                if (prop is rtFunction)
                                {
                                    function = (rtFunction)prop;

                                    if (!ReferenceEquals(find, dobj))
                                    {
                                        function = (rtFunction)function.Clone();
                                        function.setThis(obj);
                                    }

                                }
                            }

                            //if (dobj.hasproperty("toString"))
                            //{
                            //    var prop = dobj["toString"].getValue();

                            //    if (prop is rtFunction)
                            //    {
                            //        function = (rtFunction)prop;
                            //    }
                            //}
                        }

                    }

                    if (function != null)
                    {
                        BlockCallBackBase toStringCB = new BlockCallBackBase();
                        toStringCB.setCallBacker(_toString_CB);
                        toStringCB._intArg = targetType;



                        operators.FunctionCaller fc = new operators.FunctionCaller(frame.player, frame, token);
                        object[] sendargs = new object[7];
                        sendargs[0] = frame;
                        sendargs[1] = token;
                        sendargs[2] = scope;
                        sendargs[3] = storeto;
                        sendargs[4] = callbacker;
                        sendargs[5] = fc;
                        sendargs[6] = srcValue;
                        toStringCB.args = sendargs;

                        fc.function = function;
                        fc.loadDefineFromFunction();
                        fc.createParaScope();
                        fc.returnSlot = storeto;
                        fc.callbacker = toStringCB;
                        fc.call();
                    }
                    else
                    {
                        storeto.setValue(((rtObject)srcValue).value.ToString());
                        callbacker.isSuccess = true;
                        callbacker.call(null);
                    }
                    #endregion
                }
                else if (targetType > ASBinCode.RunTimeDataType.unknown)
                {
                    //**检查基类
                    frame.throwCastException(token, srcValue.rtType, targetType);
                    frame.endStep();
                }
                else
                {
                    RunTimeDataType ot;
                    //***转成基本类型****
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(srcValue.rtType, frame.player.swc, out ot))
                    {
                        CastValue(
                            TypeConverter.ObjectImplicit_ToPrimitive((rtObject)srcValue),
                            targetType, frame, token, scope, storeto, callbacker,false

                            );
                        return;
                    }
                    else
                    {
                        BlockCallBackBase valueofCB = new BlockCallBackBase();
                        valueofCB.setCallBacker(_Cast_ValueOf_CB);
                        valueofCB._intArg = targetType;

                        object[] sendargs = new object[7];
                        sendargs[0] = frame;
                        sendargs[1] = token;
                        sendargs[2] = scope;
                        sendargs[3] = storeto;
                        sendargs[4] = callbacker;
                        sendargs[5] = srcValue;
                        sendargs[6] = true;

                        valueofCB.args = sendargs;


                        //取ValueOf
                        InvokeValueOf((rtObject)srcValue, frame, token, scope, storeto, valueofCB);

                        return;
                    }

                    //frame.throwCastException(token, srcValue.rtType, targetType);
                    //frame.endStep();
                }
            }
        }

        private static void _toString_CB(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            StackFrame frame = (StackFrame)a[0];
            FunctionCaller fc = (FunctionCaller)a[5];

            var rv = fc.returnSlot.getValue();
            if (rv.rtType > RunTimeDataType.unknown ||
                (rv.rtType == RunTimeDataType.rt_null) && frame.player.swc.functions[fc.function.functionId].signature.returnType == RunTimeDataType.rt_void)
            {
                BlockCallBackBase callbacker = (BlockCallBackBase)a[4];

                frame.throwCastException((SourceToken)a[1], ((IRunTimeValue)a[6]).rtType, RunTimeDataType.rt_string);
                //转换异常后立刻结束执行
                frame.endStep();
            }
            else
            {
                CastValue(fc.returnSlot.getValue(),
                    sender._intArg,
                    frame,
                    (SourceToken)a[1],
                    (IRunTimeScope)a[2],
                    (ISLOT)a[3],
                    (BlockCallBackBase)a[4],false
                    );
            }
        }

        private static void _Cast_ValueOf_CB(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            StackFrame frame = (StackFrame)a[0];
            BlockCallBackBase fc = (BlockCallBackBase)a[4];
            IRunTimeValue rv = ((ISLOT)a[3]).getValue();
            RunTimeDataType targetType = sender._intArg;
            if (rv.rtType > RunTimeDataType.unknown)
            {
                if ((bool)a[6])
                {
                    frame.throwCastException((SourceToken)a[1], ((IRunTimeValue)a[5]).rtType,
                       targetType);
                    //转换异常后立刻结束执行
                    frame.endStep();
                }
                else
                {
                    CastValue(rv,
                        sender._intArg,
                        frame,
                        (SourceToken)a[1],
                        (IRunTimeScope)a[2],
                        (ISLOT)a[3],
                        fc,
                        true
                    );
                }
            }
            else
            {
                CastValue(rv,
                    sender._intArg,
                    frame,
                    (SourceToken)a[1],
                    (IRunTimeScope)a[2],
                    (ISLOT)a[3],
                    fc,
                    false
                    );
            }
        }


        public static void CastTwoValue(
            ASBinCode.IRunTimeValue srcValue1,
            ASBinCode.IRunTimeValue srcValue2,
            ASBinCode.RunTimeDataType targetType,
            StackFrame frame,
            ASBinCode.SourceToken token,
            ASBinCode.IRunTimeScope scope,
            ASBinCode.ISLOT _tempstoreto1,
            ASBinCode.ISLOT _tempstoreto2,
            BlockCallBackBase callbacker

            )
        {
            BlockCallBackBase cb1 = new BlockCallBackBase();
            cb1._intArg = targetType;
            cb1.setCallBacker(_CastTwoValue_Backer);

            object[] tosend = new object[6];
            tosend[0] = srcValue2;
            tosend[1] = frame;
            tosend[2] = token;
            tosend[3] = scope;
            tosend[4] = _tempstoreto2;
            tosend[5] = callbacker;
            cb1.args = tosend;

            CastValue(srcValue1, targetType, frame, token, scope, _tempstoreto1, cb1,false);

        }
        private static void _CastTwoValue_Backer(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;

            StackFrame frame = (StackFrame)a[1];
            BlockCallBackBase callbacker = (BlockCallBackBase)a[5];
            if (sender.isSuccess)
            {
                CastValue(
                    (IRunTimeValue)a[0],
                    sender._intArg,
                    frame,
                    (SourceToken)a[2],
                    (IRunTimeScope)a[3],
                    (ISLOT)a[4],
                    callbacker,
                    false
                    );

            }
            else
            {
                frame.endStep();
            }

        }



        public static void Primitive_to_Object(
            ASBinCode.IRunTimeValue srcValue,
            StackFrame frame,
            SourceToken token,
            IRunTimeScope scope,
            ISLOT _tempstoreto,
            ASBinCode.OpStep step,
            StackFrame.DelegeExec exec
            )
        {
            if (srcValue.rtType < RunTimeDataType.unknown && srcValue.rtType != RunTimeDataType.rt_void)
            {
                var cls = frame.player.swc.primitive_to_class_table[srcValue.rtType].staticClass;
                if (cls != null)
                {
                    var funConv = (rtFunction)((ClassMethodGetter)cls.implicit_from.bindField).getValue(scope);

                    FunctionCaller fc = new FunctionCaller(frame.player, frame, token);
                    fc.function = funConv;
                    fc.loadDefineFromFunction();
                    fc.createParaScope();
                    fc.pushParameter(srcValue, 0);
                    fc._tempSlot = _tempstoreto;
                    fc.returnSlot = _tempstoreto;

                    BlockCallBackBase cb = new BlockCallBackBase();
                    cb.setCallBacker(_primitive_to_obj_callbacker);
                    cb.step = step;
                    cb.scope = scope;

                    object[] args = new object[3];
                    args[0] = _tempstoreto;
                    args[1] = frame;
                    args[2] = exec;
                    cb.args = args;
                    
                    fc.callbacker = cb;
                    fc.call();

                    return;
                }
                else
                {
                    exec(srcValue, rtNull.nullptr, frame, step, scope);
                }
            }
            else
            {
                exec(srcValue, rtNull.nullptr, frame, step, scope);
            }
        }

        private static void _primitive_to_obj_callbacker(BlockCallBackBase cb,object args)
        {
            object[] a = (object[])cb.args;

            StackFrame frame = (StackFrame)a[1];
            ISLOT result = (ISLOT)a[0];
            StackFrame.DelegeExec exec = (StackFrame.DelegeExec)a[2];

            exec(result.getValue(), null, frame, cb.step, cb.scope);


        }


        #region valueOf

        private static void _exec_valueof_callback(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;

            ((StackFrame.DelegeExec)a[5])(
                ((ISLOT)a[0]).getValue(),
                ((ISLOT)a[1]).getValue(),
                (StackFrame)a[2],
                (OpStep)a[3],
                (IRunTimeScope)a[4]
                );
        }

        public static void InvokeTwoValueOf(
            ASBinCode.IRunTimeValue srcValue1,
            ASBinCode.IRunTimeValue srcValue2,
            StackFrame frame,
            SourceToken token,
            IRunTimeScope scope,
            ISLOT _tempstoreto1,
            ISLOT _tempstoreto2,
            ASBinCode.OpStep step,
            StackFrame.DelegeExec exec
            //BlockCallBackBase callbacker
            )
        {
            RunTimeDataType ot;
            if (TypeConverter.Object_CanImplicit_ToPrimitive(srcValue1.rtType, frame.player.swc, out ot))
            {
                srcValue1 = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)srcValue1);
            }
            if (TypeConverter.Object_CanImplicit_ToPrimitive(srcValue2.rtType, frame.player.swc, out ot))
            {
                srcValue2 = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)srcValue2);
            }

            if (srcValue1 is rtObject)
            {
                BlockCallBackBase callbacker = new BlockCallBackBase();
                {
                    object[] backargs = new object[6];
                    backargs[0] = _tempstoreto1;
                    backargs[1] = _tempstoreto2;
                    backargs[2] = frame;
                    backargs[3] = step;
                    backargs[4] = scope;
                    backargs[5] = exec;
                    callbacker.args = backargs;
                    callbacker.setCallBacker(_exec_valueof_callback);
                }

                BlockCallBackBase cb = new BlockCallBackBase();
                object[] tosend = new object[6];
                tosend[0] = srcValue2;
                tosend[1] = frame;
                tosend[2] = token;
                tosend[3] = scope;
                tosend[4] = _tempstoreto2;
                tosend[5] = callbacker;
                cb.args = tosend;
                cb.setCallBacker(_AfterGetOneValueOf);

                InvokeValueOf((rtObject)srcValue1, frame, token, scope, _tempstoreto1, cb);
            }
            else
            {
                _tempstoreto1.directSet(srcValue1);
                if (srcValue2 is rtObject)
                {
                    BlockCallBackBase callbacker = new BlockCallBackBase();
                    {
                        object[] backargs = new object[6];
                        backargs[0] = _tempstoreto1;
                        backargs[1] = _tempstoreto2;
                        backargs[2] = frame;
                        backargs[3] = step;
                        backargs[4] = scope;
                        backargs[5] = exec;
                        callbacker.args = backargs;
                        callbacker.setCallBacker(_exec_valueof_callback);
                    }

                    BlockCallBackBase cb = new BlockCallBackBase();
                    cb.setCallBacker(_AfterGetTwoValueOf);
                    cb.args = callbacker;
                    InvokeValueOf((rtObject)srcValue2, frame, token, scope, _tempstoreto2, cb);
                }
                else
                {
                    _tempstoreto2.directSet(srcValue2);
                    //callbacker.call(null);

                    exec(srcValue1, srcValue2, frame, step, scope);

                }
            }
        }

        private static void _AfterGetOneValueOf(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            IRunTimeValue srcValue2 = (IRunTimeValue)a[0];
            StackFrame frame = (StackFrame)a[1]; //tosend[1] = frame;
            SourceToken token = (SourceToken)a[2]; //tosend[2] = token;
            IRunTimeScope scope = (IRunTimeScope)a[3];  //tosend[3] = scope;
            ISLOT storeto = (ISLOT)a[4]; //tosend[4] = _tempstoreto2;
            BlockCallBackBase callbacker = (BlockCallBackBase)a[5];


            if (srcValue2 is rtObject)
            {
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_AfterGetTwoValueOf);
                cb.args = callbacker;
                InvokeValueOf((rtObject)srcValue2, frame, token, scope, storeto, cb);
            }
            else
            {
                storeto.directSet(srcValue2);
                callbacker.call(null);
            }
        }

        private static void _AfterGetTwoValueOf(BlockCallBackBase sender, object args)
        {
            ((BlockCallBackBase)sender.args).call(null);
        }

        private static void InvokeValueOf(
            rtObject obj, StackFrame frame, SourceToken token, IRunTimeScope scope, ISLOT storeto,
            BlockCallBackBase callbacker
            )
        {
            //***调用valueOf()
            var valueOf = (ASBinCode.ClassMemberFinder.find(obj.value._class, "valueOf", obj.value._class));
            rtFunction function = null;
            if (
                valueOf != null
                && valueOf.valueType == RunTimeDataType.rt_function
                && !valueOf.isStatic
                && valueOf.isPublic
                && !valueOf.isConstructor
                && !valueOf.isGetter
                && !valueOf.isSetter
                )
            {
                function = (rtFunction)((ILeftValue)valueOf.bindField).getValue(obj.objScope);
            }
            else
            {
                //***未找到

                if (obj.value is ASBinCode.rtti.DynamicObject)
                {
                    ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;

                    bool haserror;
                    var find= OpAccess_Dot.findInProtoType(dobj, "valueOf", frame, token, out haserror);
                    if (haserror)
                    {
                        frame.endStep();
                        return;
                    }

                    if (find != null)
                    {
                        var prop = find["valueOf"].getValue();
                        if (prop is rtFunction)
                        {
                            function = (rtFunction)prop;
                            if(!ReferenceEquals(find,dobj))
                            {
                                function = (rtFunction)function.Clone();
                                function.setThis(obj);
                            }
                            
                        }
                    }

                    //if (dobj.hasproperty("valueOf"))
                    //{
                    //    var prop = dobj["valueOf"].getValue();

                    //    if (prop is rtFunction)
                    //    {
                    //        function = (rtFunction)prop;
                    //    }
                    //}
                }

            }

            if (function != null)
            {
                BlockCallBackBase valueofCB = new BlockCallBackBase();
                valueofCB.setCallBacker(_InvokeValueOf_Backer);

                operators.FunctionCaller fc = new operators.FunctionCaller(frame.player, frame, token);
                object[] sendargs = new object[5];
                sendargs[0] = obj;
                sendargs[1] = callbacker;
                sendargs[2] = storeto;
                //sendargs[3] = frame;
                //sendargs[4] = token;
                valueofCB.args = sendargs;

                fc.function = function;
                fc.loadDefineFromFunction();
                fc.createParaScope();
                fc.returnSlot = storeto;
                fc.callbacker = valueofCB;
                fc.call();
            }
            else
            {

                if (
                    callbacker.args is object[]
                    &&
                    ((object[])callbacker.args).Length > 6)
                {
                    ((object[])callbacker.args)[6] = false;
                }
                storeto.directSet(obj);
                callbacker.call(null);
            }
        }

        private static void _InvokeValueOf_Backer(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;

            ISLOT returnValue = (ISLOT)a[2];
            BlockCallBackBase callbacker = (BlockCallBackBase)a[1];

            if (returnValue.getValue().rtType > RunTimeDataType.unknown)
            {
                returnValue.directSet((rtObject)a[0]); //valueOf取值不正确，返回原始对象
                //((StackFrame)a[3]).throwCastException((SourceToken)a[4], ((rtObject)a[0]).rtType,
                //   sender._intArg);
                ////转换异常后立刻结束执行
                //((StackFrame)a[3]).endStep();

                //return;
            }


            callbacker.call(null);

        }

        #endregion


        #region toString
        private static void _exec_toString_callback(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;

            ((StackFrame.DelegeExec)a[5])(
                ((ISLOT)a[0]).getValue(),
                ((ISLOT)a[1]).getValue(),
                (StackFrame)a[2],
                (OpStep)a[3],
                (IRunTimeScope)a[4]
                );
        }

        public static void InvokeTwoToString(
            ASBinCode.IRunTimeValue srcValue1,
            ASBinCode.IRunTimeValue srcValue2,
            StackFrame frame,
            SourceToken token,
            IRunTimeScope scope,
            ISLOT _tempstoreto1,
            ISLOT _tempstoreto2,
            ASBinCode.OpStep step,
            StackFrame.DelegeExec exec
            //BlockCallBackBase callbacker
            )
        {

            if (srcValue1 is rtObject)
            {
                BlockCallBackBase callbacker = new BlockCallBackBase();
                {
                    object[] backargs = new object[6];
                    backargs[0] = _tempstoreto1;
                    backargs[1] = _tempstoreto2;
                    backargs[2] = frame;
                    backargs[3] = step;
                    backargs[4] = scope;
                    backargs[5] = exec;
                    callbacker.args = backargs;
                    callbacker.setCallBacker(_exec_toString_callback);
                }

                BlockCallBackBase cb = new BlockCallBackBase();
                object[] tosend = new object[6];
                tosend[0] = srcValue2;
                tosend[1] = frame;
                tosend[2] = token;
                tosend[3] = scope;
                tosend[4] = _tempstoreto2;
                tosend[5] = callbacker;
                cb.args = tosend;
                cb.setCallBacker(_AfterGetOneToString);

                InvokeToString((rtObject)srcValue1, frame, token, scope, _tempstoreto1, cb);
            }
            else
            {
                _tempstoreto1.directSet(srcValue1);
                if (srcValue2 is rtObject)
                {
                    BlockCallBackBase callbacker = new BlockCallBackBase();
                    {
                        object[] backargs = new object[6];
                        backargs[0] = _tempstoreto1;
                        backargs[1] = _tempstoreto2;
                        backargs[2] = frame;
                        backargs[3] = step;
                        backargs[4] = scope;
                        backargs[5] = exec;
                        callbacker.args = backargs;
                        callbacker.setCallBacker(_exec_toString_callback);
                    }

                    BlockCallBackBase cb = new BlockCallBackBase();
                    cb.setCallBacker(_AfterGetTwoToString);
                    cb.args = callbacker;
                    InvokeToString((rtObject)srcValue2, frame, token, scope, _tempstoreto2, cb);
                }
                else
                {
                    _tempstoreto2.directSet(srcValue2);
                    //callbacker.call(null);

                    exec(srcValue1, srcValue2, frame, step, scope);

                }
            }
        }

        private static void _AfterGetOneToString(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            IRunTimeValue srcValue2 = (IRunTimeValue)a[0];
            StackFrame frame = (StackFrame)a[1]; //tosend[1] = frame;
            SourceToken token = (SourceToken)a[2]; //tosend[2] = token;
            IRunTimeScope scope = (IRunTimeScope)a[3];  //tosend[3] = scope;
            ISLOT storeto = (ISLOT)a[4]; //tosend[4] = _tempstoreto2;
            BlockCallBackBase callbacker = (BlockCallBackBase)a[5];


            if (srcValue2 is rtObject)
            {
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_AfterGetTwoToString);
                cb.args = callbacker;
                InvokeToString((rtObject)srcValue2, frame, token, scope, storeto, cb);
            }
            else
            {
                storeto.directSet(srcValue2);
                callbacker.call(null);
            }
        }

        private static void _AfterGetTwoToString(BlockCallBackBase sender, object args)
        {
            ((BlockCallBackBase)sender.args).call(null);
        }

        private static void InvokeToString(
            rtObject obj, StackFrame frame, SourceToken token, IRunTimeScope scope, ISLOT storeto,
            BlockCallBackBase callbacker
            )
        {
            //***调用toString()
            var toString = (ASBinCode.ClassMemberFinder.find(obj.value._class, "toString", obj.value._class));
            rtFunction function = null;
            if (
                toString != null
                && toString.valueType == RunTimeDataType.rt_function
                && !toString.isStatic
                && toString.isPublic
                && !toString.isConstructor
                && !toString.isGetter
                && !toString.isSetter
                )
            {
                function = (rtFunction)((ILeftValue)toString.bindField).getValue(obj.objScope);
            }
            else
            {
                //***未找到

                if (obj.value is ASBinCode.rtti.DynamicObject)
                {
                    ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;

                    bool haserror;
                    var find = OpAccess_Dot.findInProtoType(dobj, "toString", frame, token, out haserror);
                    if (haserror)
                    {
                        frame.endStep();
                        return;
                    }

                    if (find != null)
                    {
                        var prop = find["toString"].getValue();
                        if (prop is rtFunction)
                        {
                            function = (rtFunction)prop;

                            if (!ReferenceEquals(find, dobj))
                            {
                                function = (rtFunction)function.Clone();
                                function.setThis(obj);
                            }

                        }
                    }

                    //if (dobj.hasproperty("toString"))
                    //{
                    //    var prop = dobj["toString"].getValue();

                    //    if (prop is rtFunction)
                    //    {
                    //        function = (rtFunction)prop;
                    //    }
                    //}
                }

            }

            if (function != null)
            {
                BlockCallBackBase toStringCB = new BlockCallBackBase();
                toStringCB.setCallBacker(_InvokeToString_Backer);

                operators.FunctionCaller fc = new operators.FunctionCaller(frame.player, frame, token);
                object[] sendargs = new object[5];
                sendargs[0] = obj;
                sendargs[1] = callbacker;
                sendargs[2] = storeto;
                //sendargs[3] = frame;
                //sendargs[4] = token;
                toStringCB.args = sendargs;

                fc.function = function;
                fc.loadDefineFromFunction();
                fc.createParaScope();
                fc.returnSlot = storeto;
                fc.callbacker = toStringCB;
                fc.call();
            }
            else
            {
                //storeto.directSet(obj);
                storeto.directSet(new rtString(TypeConverter.ConvertToString(obj, frame, token)));
                callbacker.call(null);
            }
        }

        private static void _InvokeToString_Backer(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            BlockCallBackBase callbacker = (BlockCallBackBase)a[1];

            callbacker.call(null);

        }

        #endregion


        public static void exec_CastPrimitive(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            step.reg.getISlot(scope).directSet
                ( 
                TypeConverter.ObjectImplicit_ToPrimitive( (rtObject)step.arg1.getValue(scope) )
                );

            frame.endStep(step);
        }
    }
}
