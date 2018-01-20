using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASRuntime.operators
{
    class OpCast
    {
		

        public static void execCast(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v1 = step.arg1.getValue(scope, frame);

            if (step.regType == RunTimeDataType.rt_void)
            {
                step.reg.getSlot(scope, frame).directSet(v1);
				//frame.endStep(step);
				frame.endStepNoError();
            }
            else if (step.regType < RunTimeDataType.unknown && v1.rtType < RunTimeDataType.unknown)
            {
				if (!CastPrimitive_to_Primitive(step.regType, v1, step.reg.getSlot(scope, frame), frame, step.token))
				{
					frame.throwCastException(step.token, v1.rtType, step.regType);
					frame.endStep(step);
				}
				else
				{
					frame.endStepNoError();
				}
            }
            else
            {

                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                cb.setCallBacker(_CastCallBacker);
                cb.args = frame;
                cb.scope = scope;

                CastValue(
                    v1,
                    step.regType,
                    frame,
                    step.token,
                    scope,
                    step.reg.getSlot(scope, frame)
                    ,
                    cb
                    ,
                    false
                    );

            }
            
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
                OpStep step = frame.block.instructions[frame.codeLinePtr];
                frame.throwCastException(step.token, step.arg1.getValue(sender.scope, frame).rtType, step.regType);

                frame.endStep();

            }
        }


        private static bool CastPrimitive_to_Primitive(RunTimeDataType targetType,RunTimeValueBase srcValue,SLOT storeto,StackFrame frame,SourceToken token)
        {
            switch (targetType)
            {
                case ASBinCode.RunTimeDataType.rt_boolean:
                    {
                        storeto.setValue(
                            TypeConverter.ConvertToBoolean(
                            srcValue, frame, token
                        )
                            );
                        //callbacker.isSuccess = true;
                        //callbacker.call(null);
                        return true;
                    }
                case ASBinCode.RunTimeDataType.rt_int:
                    {
                        storeto.setValue(
                         TypeConverter.ConvertToInt(
                            srcValue
                        )
                        );
                        //callbacker.isSuccess = true;
                        //callbacker.call(null);
                        return true;
                    }
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        storeto.setValue(
                            TypeConverter.ConvertToUInt(
                            srcValue, frame, token
                        )
                            );
                        //callbacker.isSuccess = true;
                        //callbacker.call(null);
                        return true;
                    }
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        storeto.setValue(
                            TypeConverter.ConvertToNumber(
                            srcValue
                        )
                            );
                        //callbacker.isSuccess = true;
                        //callbacker.call(null);
                        return true;
                    }
                case ASBinCode.RunTimeDataType.rt_string:
                    {
                        string str = TypeConverter.ConvertToString(
                            srcValue, frame, token
                        );

                        storeto.setValue(
                            str
                            );
                        //callbacker.isSuccess = true;
                        //callbacker.call(null);
                        return true;
                    }
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                case ASBinCode.RunTimeDataType.fun_void:
                    {
                        storeto.directSet(srcValue);
                        //callbacker.isSuccess = true;
                        //callbacker.call(null);
                        return true;
                    }
                case ASBinCode.RunTimeDataType.rt_function:
                    {
                        if (srcValue.rtType == ASBinCode.RunTimeDataType.rt_function
                            ||
                            srcValue.rtType == ASBinCode.RunTimeDataType.rt_null
                            //||
                            //srcValue.rtType == ASBinCode.RunTimeDataType.rt_void
                            )
                        {
                            {
                                storeto.directSet(srcValue);
                                //callbacker.isSuccess = true;
                                //callbacker.call(null);
                                return true;
                            }
                        }
                        else
                        {
                            {
                                //frame.throwCastException(token, srcValue.rtType, targetType);
                                //frame.endStep();

                                return false;
                            }
                        }
                    }
                case RunTimeDataType.rt_array:
                    {
                        if (srcValue.rtType == RunTimeDataType.rt_null || srcValue.rtType==RunTimeDataType.rt_array )
                        {
                            storeto.directSet(srcValue);
                            //callbacker.isSuccess = true;
                            //callbacker.call(null);
                            return true;
                        }
                        else
                        {
                            //frame.throwCastException(token, srcValue.rtType, targetType);
                            //frame.endStep();

                            return false;
                        }
                    }
                case ASBinCode.RunTimeDataType.unknown:
                    {
                        //frame.throwCastException(token, srcValue.rtType, targetType);
                        //frame.endStep();
                        return false;
                    }
                default:
                    {
                        //frame.throwCastException(token, srcValue.rtType, targetType);
                        //frame.endStep();
                        return false;
                    }
            }
        }


        public static void CastValue(
            ASBinCode.RunTimeValueBase srcValue,
            ASBinCode.RunTimeDataType targetType,
            StackFrame frame,
            ASBinCode.SourceToken token,
            ASBinCode.RunTimeScope scope,
            ASBinCode.SLOT storeto,
            BlockCallBackBase callbacker,
            bool igrionValueOf
            )
        {
            if ((srcValue.rtType < ASBinCode.RunTimeDataType._OBJECT || igrionValueOf) && targetType < ASBinCode.RunTimeDataType._OBJECT)
            {
                if (CastPrimitive_to_Primitive(targetType, srcValue, storeto, frame, token))
                {
                    callbacker.isSuccess = true;
                    callbacker.call(null);
                }
                else
                {
                    frame.throwCastException(token, srcValue.rtType, targetType);
                    frame.endStep();
                    callbacker.noticeRunFailed();
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
                    ASBinCode.rtData.rtObjectBase obj = (ASBinCode.rtData.rtObjectBase)srcValue;
                    var toStr = (ASBinCode.ClassMemberFinder.find(obj.value._class, "toString", obj.value._class));

                    rtFunction function = null;
					rtFunction toclear = null;
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
                        function = (rtFunction)((MethodGetterBase)toStr.bindField).getMethod(obj);
						toclear = function;
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
								callbacker.noticeRunFailed();
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

                        }
                        else
                        {
                            //***从Class定义的原型链中查找
                            var dobj = (ASBinCode.rtti.DynamicObject)
                                frame.player.static_instance[obj.value._class.staticClass.classid].value;

                            dobj = (ASBinCode.rtti.DynamicObject)((rtObjectBase)dobj.memberData[0].getValue()).value;
                            if (!dobj.hasproperty("toString"))
                            {

                                dobj = ((ASBinCode.rtti.DynamicObject)
                                    frame.player.static_instance[obj.value._class.staticClass.classid].value);

                                bool haserror;
                                dobj = OpAccess_Dot.findInProtoType(dobj, "toString", 
                                    frame, token, out haserror);
                                if (haserror)
                                {
									callbacker.noticeRunFailed();
									frame.endStep();
                                    
                                    return;
                                }
                            }

                            if (dobj != null)
                            {
                                var prop = dobj["toString"].getValue();
                                if (prop is rtFunction)
                                {
                                    function = (rtFunction)prop;

                                    if (!ReferenceEquals(dobj, obj.value))
                                    {
                                        function = (rtFunction)function.Clone();
                                        function.setThis(obj);
                                    }

                                }
                            }
                        }

                    }

                    if (function != null)
                    {
                        BlockCallBackBase toStringCB = frame.player.blockCallBackPool.create();
                        toStringCB.setCallBacker(_toString_CB);
                        toStringCB._intArg = targetType;



                        operators.FunctionCaller fc = frame.player.funcCallerPool.create(frame, token);
                        //fc.releaseAfterCall = true;
                        object[] sendargs = toStringCB.cacheObjects; //new object[7];
                        sendargs[0] = frame;
                        sendargs[1] = token;
                        sendargs[2] = scope;
                        sendargs[3] = storeto;
                        sendargs[4] = callbacker;
                        //sendargs[5] = function;
                        sendargs[6] = srcValue;
                        toStringCB.args = sendargs;
						toStringCB._intArg2 = function.functionId;

                        fc.SetFunction(function);if (toclear != null) { toclear.Clear(); }
                        fc.loadDefineFromFunction();
                        fc.returnSlot = storeto;
                        fc.callbacker = toStringCB;
                        if (!fc.createParaScope()) { callbacker.noticeRunFailed(); return; }
                        fc.call();
                    }
                    else
                    {
                        storeto.setValue(((rtObjectBase)srcValue).value.ToString());
                        callbacker.isSuccess = true;
                        callbacker.call(null);
                    }
                    #endregion
                }
                else if (targetType > ASBinCode.RunTimeDataType.unknown && srcValue.rtType > RunTimeDataType.unknown)
                {
					if (ClassMemberFinder.check_isinherits(srcValue, targetType, frame.player.swc)
						||
						ClassMemberFinder.check_isImplements(srcValue, targetType, frame.player.swc)
						)
					{
						storeto.directSet(srcValue);
						callbacker.isSuccess = true;
						callbacker.call(null);
					}
					else
					{
						bool checkinstanceof = false;
						//***检查是否链接对象是基类的情况***
						if (((rtObjectBase)srcValue).value._class.isLink_System)
						{
							var cls = frame.player.swc.getClassByRunTimeDataType(targetType);
							if (cls.isLink_System)
							{
								var f = ((CSWC)frame.player.swc).class_Creator[cls];
								var obj = ((ASBinCode.rtti.LinkSystemObject)((rtObjectBase)srcValue).value).GetLinkData();

								bool isinstanceof= f.getLinkSystemObjType().IsInstanceOfType(obj);

								if (isinstanceof)
								{
									//***更新类型信息***
									srcValue.rtType = targetType;
									((rtObjectBase)srcValue).value._class = cls;
									((rtObjectBase)srcValue).objScope.blockId = cls.blockid;
									//******************

									storeto.directSet(srcValue);
									callbacker.isSuccess = true;
									callbacker.call(null);

									checkinstanceof = true;

								}

							}
						}

						if (!checkinstanceof)
						{
							//**检查基类
							frame.throwCastException(token, srcValue.rtType, targetType);
							callbacker.noticeRunFailed();
							frame.endStep();
							
						}
                    }
                }
                else if (targetType > ASBinCode.RunTimeDataType.unknown)
                {
                    //***从基本类型转换为引用类型***
                    if (srcValue.rtType != RunTimeDataType.rt_void)
                    {
                        
                        var cls = frame.player.swc.primitive_to_class_table[srcValue.rtType].staticClass;

                        if (cls == null || !((ClassMemberFinder.check_isinherits(cls.instanceClass.getRtType()
                                ,
                                targetType, frame.player.swc))))
                        {
                            var targetCls = frame.player.swc.getClassByRunTimeDataType(targetType);
                            if (targetCls.staticClass != null &&
                                targetCls.staticClass.implicit_from !=null &&

                                TypeConverter.testImplicitConvert(srcValue.rtType,
                                targetCls.staticClass.implicit_from_type, null))
                            {
                                cls = targetCls.staticClass;
                            }
                        }

                        if (cls != null)
                        {
                            if (ClassMemberFinder.check_isinherits(cls.instanceClass.getRtType()
                                ,
                                targetType, frame.player.swc))
                            {
                                var funConv = (rtFunction)((ClassMethodGetter)cls.implicit_from.bindField).getImplicitConvertFunction(scope);
                                
                                if (!operators.InstanceCreator.init_static_class(cls.instanceClass,frame.player,token))
                                {
									funConv.Clear();
                                    callbacker.noticeRunFailed();
                                    return;
                                }
                                


                                funConv.setThis(frame.player.static_instance[cls.classid]);

                                FunctionCaller fc = frame.player.funcCallerPool.create( frame, token);
                                
                                fc.SetFunction(funConv);funConv.Clear();//这里得funConv是一个缓存对象，复制完后立刻复位。
                                fc.loadDefineFromFunction();
                                if (!fc.createParaScope()) { callbacker.noticeRunFailed(); return; }
                                bool success;
                                fc.pushParameter(srcValue, 0, out success);
                                fc._tempSlot = frame._tempSlot1;
                                fc.returnSlot = storeto;

                                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                                cb.setCallBacker(_primivite_Obj);

                                //object[] sendargs = new object[5];
                                //sendargs[0] = frame;
                                //sendargs[1] = token;
                                //sendargs[2] = scope;
                                //sendargs[3] = storeto;
                                //sendargs[4] = callbacker;
                                //sendargs[5] = fc;
                                //sendargs[6] = srcValue;
                                cb.args = callbacker; //sendargs;

                                fc.callbacker = cb;
                                fc.call();

                                return;

                            }
                            else
                            {
                                frame.throwCastException(token, srcValue.rtType, targetType);
								callbacker.noticeRunFailed();
								frame.endStep();
								
								
                               
                            }
                        }     
                        else
                        {
                            frame.throwCastException(token, srcValue.rtType, targetType);
							callbacker.noticeRunFailed();
							frame.endStep();
                            
                        }
                    }
                    else
                    {
                        frame.throwCastException(token, srcValue.rtType, targetType);
						callbacker.noticeRunFailed();
						frame.endStep();
                        
                    }
                }
                else
                {
                    RunTimeDataType ot;
                    //***转成基本类型****
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(srcValue.rtType, frame.player.swc, out ot))
                    {
                        CastValue(
                            TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)srcValue),
                            targetType, frame, token, scope, storeto, callbacker, false

                            );
                        return;
                    }
                    else
                    {
                        BlockCallBackBase valueofCB = frame.player.blockCallBackPool.create();
                        valueofCB.setCallBacker(_Cast_ValueOf_CB);
                        valueofCB._intArg = targetType;

                        object[] sendargs = valueofCB.cacheObjects; //new object[7];
                        sendargs[0] = frame;
                        sendargs[1] = token;
                        sendargs[2] = scope;
                        sendargs[3] = storeto;
                        sendargs[4] = callbacker;
                        sendargs[5] = srcValue;
                        sendargs[6] = true;

                        valueofCB.args = sendargs;


                        //取ValueOf
                        InvokeValueOf((rtObjectBase)srcValue, frame, token, scope, storeto, valueofCB);

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
			//rtFunction fc = (rtFunction)a[5];
			int fcid = sender._intArg2;

            var rv =  ((SLOT)a[3]).getValue();

            if (rv.rtType > RunTimeDataType.unknown ||
                (rv.rtType == RunTimeDataType.rt_null) && frame.player.swc.functions[fcid].signature.returnType == RunTimeDataType.rt_void)
            {
                BlockCallBackBase callbacker = (BlockCallBackBase)a[4];

                frame.throwCastException((SourceToken)a[1], ((RunTimeValueBase)a[6]).rtType, RunTimeDataType.rt_string);
				callbacker.noticeRunFailed();
				//转换异常后立刻结束执行
				frame.endStep();
                
            }
            else
            {
                CastValue(rv,
                    sender._intArg,
                    frame,
                    (SourceToken)a[1],
                    (RunTimeScope)a[2],
                    (SLOT)a[3],
                    (BlockCallBackBase)a[4],false
                    );
            }
        }

        private static void _Cast_ValueOf_CB(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            StackFrame frame = (StackFrame)a[0];
            BlockCallBackBase fc = (BlockCallBackBase)a[4];
            RunTimeValueBase rv = ((SLOT)a[3]).getValue();
            RunTimeDataType targetType = sender._intArg;
            if (rv.rtType > RunTimeDataType.unknown)
            {
                if ((bool)a[6] && rv !=a[5])
                {
                    frame.throwCastException((SourceToken)a[1], ((RunTimeValueBase)a[5]).rtType,
                       targetType);
					fc.noticeRunFailed();
					//转换异常后立刻结束执行
					frame.endStep();
                    
                }
                else
                {
                    CastValue(rv,
                        sender._intArg,
                        frame,
                        (SourceToken)a[1],
                        (RunTimeScope)a[2],
                        (SLOT)a[3],
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
                    (RunTimeScope)a[2],
                    (SLOT)a[3],
                    fc,
                    false
                    );
            }
        }


        private static void _primivite_Obj(BlockCallBackBase sender, object args)
        {
            //object[] a = (object[])sender.args;

            BlockCallBackBase callbacker = (BlockCallBackBase)sender.args; //(BlockCallBackBase)a[4];
            callbacker.isSuccess = true;
            callbacker.call(null);

            //StackFrame frame = (StackFrame)a[0];
            //FunctionCaller fc = (FunctionCaller)a[5];

            //frame.endStep();
        }





        public static void CastTwoValue(
            ASBinCode.RunTimeValueBase srcValue1,
            ASBinCode.RunTimeValueBase srcValue2,
            ASBinCode.RunTimeDataType targetType,
            StackFrame frame,
            ASBinCode.SourceToken token,
            ASBinCode.RunTimeScope scope,
            ASBinCode.SLOT _tempstoreto1,
            ASBinCode.SLOT _tempstoreto2,
            BlockCallBackBase callbacker

            )
        {
            BlockCallBackBase cb1 = frame.player.blockCallBackPool.create();
            cb1._intArg = targetType;
            cb1.setCallBacker(_CastTwoValue_Backer);

            object[] tosend = cb1.cacheObjects; //new object[6];
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
                    (RunTimeValueBase)a[0],
                    sender._intArg,
                    frame,
                    (SourceToken)a[2],
                    (RunTimeScope)a[3],
                    (SLOT)a[4],
                    callbacker,
                    false
                    );

            }
            else
            {
				callbacker.noticeRunFailed();
				frame.endStep();
                
            }

        }



        public static void Primitive_to_Object(
            ASBinCode.RunTimeValueBase srcValue,
            StackFrame frame,
            SourceToken token,
            RunTimeScope scope,
            SLOT _tempstoreto,
            ASBinCode.OpStep step,
            StackFrame.DelegeExec exec
            )
        {
            if (srcValue.rtType < RunTimeDataType.unknown && srcValue.rtType != RunTimeDataType.rt_void)
            {
                var cls = frame.player.swc.primitive_to_class_table[srcValue.rtType].staticClass;
                if (cls != null)
                {
                    var funConv = (rtFunction)((ClassMethodGetter)cls.implicit_from.bindField).getImplicitConvertFunction(scope);
                    if (!operators.InstanceCreator.init_static_class(cls.instanceClass, frame.player, token))
                    {
						funConv.Clear();
                        return;
                    }

                    funConv.setThis(frame.player.static_instance[cls.classid]);

                    FunctionCaller fc = frame.player.funcCallerPool.create(frame, token);
                    
                    fc.SetFunction(funConv);funConv.Clear();
                    fc.loadDefineFromFunction();
                    if (!fc.createParaScope()) { return;  }
                    bool success;
                    fc.pushParameter(srcValue, 0,out success);
                    fc._tempSlot = _tempstoreto;
                    fc.returnSlot = _tempstoreto;

                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.setCallBacker(_primitive_to_obj_callbacker);
                    cb.step = step;
                    cb.scope = scope;

                    object[] args = cb.cacheObjects; //new object[3];
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
            SLOT result = (SLOT)a[0];
            StackFrame.DelegeExec exec = (StackFrame.DelegeExec)a[2];

            exec(result.getValue(), null, frame, cb.step, cb.scope);


        }


        #region valueOf

        private static void _exec_valueof_callback(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;

            ((StackFrame.DelegeExec)a[5])(
                ((SLOT)a[0]).getValue(),
                ((SLOT)a[1]).getValue(),
                (StackFrame)a[2],
                (OpStep)a[3],
                (RunTimeScope)a[4]
                );
        }

        public static void InvokeTwoValueOf(
            ASBinCode.RunTimeValueBase srcValue1,
            ASBinCode.RunTimeValueBase srcValue2,
            StackFrame frame,
            SourceToken token,
            RunTimeScope scope,
            SLOT _tempstoreto1,
            SLOT _tempstoreto2,
            ASBinCode.OpStep step,
            StackFrame.DelegeExec exec
            //BlockCallBackBase callbacker
            )
        {
            RunTimeDataType ot;
            if (TypeConverter.Object_CanImplicit_ToPrimitive(srcValue1.rtType, frame.player.swc, out ot))
            {
                srcValue1 = TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)srcValue1);
            }
            if (TypeConverter.Object_CanImplicit_ToPrimitive(srcValue2.rtType, frame.player.swc, out ot))
            {
                srcValue2 = TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)srcValue2);
            }

            if (srcValue1 is rtObjectBase)
            {
                BlockCallBackBase callbacker = frame.player.blockCallBackPool.create();
                {
                    object[] backargs = callbacker.cacheObjects; //new object[6];
                    backargs[0] = _tempstoreto1;
                    backargs[1] = _tempstoreto2;
                    backargs[2] = frame;
                    backargs[3] = step;
                    backargs[4] = scope;
                    backargs[5] = exec;
                    callbacker.args = backargs;
                    callbacker.setCallBacker(_exec_valueof_callback);
                }

                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                object[] tosend = cb.cacheObjects; //new object[6];
                tosend[0] = srcValue2;
                tosend[1] = frame;
                tosend[2] = token;
                tosend[3] = scope;
                tosend[4] = _tempstoreto2;
                tosend[5] = callbacker;
                cb.args = tosend;
                cb.setCallBacker(_AfterGetOneValueOf);
				cb.setWhenFailed(_GetOneValueOfFailed);

                InvokeValueOf((rtObjectBase)srcValue1, frame, token, scope, _tempstoreto1, cb);
            }
            else
            {
                _tempstoreto1.directSet(srcValue1);
                if (srcValue2 is rtObjectBase)
                {
					BlockCallBackBase callbacker = frame.player.blockCallBackPool.create();
					{
						object[] backargs = callbacker.cacheObjects; //new object[6];
						backargs[0] = _tempstoreto1;
						backargs[1] = _tempstoreto2;
						backargs[2] = frame;
						backargs[3] = step;
						backargs[4] = scope;
						backargs[5] = exec;
						callbacker.args = backargs;
						callbacker.setCallBacker(_exec_valueof_callback);
					}

					BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.setCallBacker(_AfterGetTwoValueOf);
					cb.setWhenFailed(_GetTwoValueOfFailed);
                    cb.args = callbacker;
                    InvokeValueOf((rtObjectBase)srcValue2, frame, token, scope, _tempstoreto2, cb);
                }
                else
                {
                    _tempstoreto2.directSet(srcValue2);
                    //callbacker.call(null);

                    exec(srcValue1, srcValue2, frame, step, scope);

                }
            }
        }

		private static void _GetTwoValueOfFailed(BlockCallBackBase sender, object args)
		{
			((BlockCallBackBase)sender.args).noticeRunFailed();
		}

		private static void _GetOneValueOfFailed(BlockCallBackBase sender, object args)
		{
			object[] a = (object[])sender.args;
			BlockCallBackBase callbacker = (BlockCallBackBase)a[5];
			callbacker.noticeRunFailed();
		}

		private static void _AfterGetOneValueOf(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            RunTimeValueBase srcValue2 = (RunTimeValueBase)a[0];
            StackFrame frame = (StackFrame)a[1]; //tosend[1] = frame;
            SourceToken token = (SourceToken)a[2]; //tosend[2] = token;
            RunTimeScope scope = (RunTimeScope)a[3];  //tosend[3] = scope;
            SLOT storeto = (SLOT)a[4]; //tosend[4] = _tempstoreto2;
            BlockCallBackBase callbacker = (BlockCallBackBase)a[5];


            if (srcValue2 is rtObjectBase)
            {
                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                cb.setCallBacker(_AfterGetTwoValueOf);
                cb.args = callbacker;
                InvokeValueOf((rtObjectBase)srcValue2, frame, token, scope, storeto, cb);
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
            rtObjectBase obj, StackFrame frame, SourceToken token, RunTimeScope scope, SLOT storeto,
            BlockCallBackBase callbacker
            )
        {
            //***调用valueOf()
            var valueOf = (ASBinCode.ClassMemberFinder.find(obj.value._class, "valueOf", obj.value._class));
            rtFunction function = null;rtFunction toclear = null;
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
                function = (rtFunction)((MethodGetterBase)valueOf.bindField).getMethod(obj);toclear = function;
            }
            else
            {
                //***未找到

                if (obj.value is ASBinCode.rtti.DynamicObject)
                {
                    ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;

                    bool haserror;
                    var find = OpAccess_Dot.findInProtoType(dobj, "valueOf", frame, token, out haserror);
                    if (haserror)
                    {
						callbacker.noticeRunFailed();
						frame.endStep();
                        
                        return;
                    }

                    if (find != null)
                    {
                        var prop = find["valueOf"].getValue();
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

                    //if (dobj.hasproperty("valueOf"))
                    //{
                    //    var prop = dobj["valueOf"].getValue();

                    //    if (prop is rtFunction)
                    //    {
                    //        function = (rtFunction)prop;
                    //    }
                    //}
                }
                else
                {
                    //***从Class定义的原型链中查找
                    var dobj = (ASBinCode.rtti.DynamicObject)
                        frame.player.static_instance[obj.value._class.staticClass.classid].value;

                    dobj = (ASBinCode.rtti.DynamicObject)((rtObjectBase)dobj.memberData[0].getValue()).value;
                    if (!dobj.hasproperty("valueOf"))
                    {

                        dobj = ((ASBinCode.rtti.DynamicObject)
                            frame.player.static_instance[obj.value._class.staticClass.classid].value);

                        bool haserror;
                        dobj = OpAccess_Dot.findInProtoType(dobj, "valueOf",
                            frame, token, out haserror);
                        if (haserror)
                        {
							callbacker.noticeRunFailed();
							frame.endStep();
                            
                            return;
                        }
                    }

                    if (dobj != null)
                    {
                        var prop = dobj["valueOf"].getValue();
                        if (prop is rtFunction)
                        {
                            function = (rtFunction)prop;

                            if (!ReferenceEquals(dobj, obj.value))
                            {
                                function = (rtFunction)function.Clone();
                                function.setThis(obj);
                            }

                        }
                    }
                }

            }

            if (function != null)
            {
                BlockCallBackBase valueofCB = frame.player.blockCallBackPool.create();
                valueofCB.setCallBacker(_InvokeValueOf_Backer);

                FunctionCaller fc = frame.player.funcCallerPool.create( frame, token);
                //fc.releaseAfterCall = true;
                object[] sendargs = valueofCB.cacheObjects; //new object[5];
                sendargs[0] = obj;
                sendargs[1] = callbacker;
                sendargs[2] = storeto;
                //sendargs[3] = frame;
                //sendargs[4] = token;
                valueofCB.args = sendargs;

                fc.SetFunction(  function);if (toclear != null) { toclear.Clear(); }
                fc.loadDefineFromFunction();
                fc.returnSlot = storeto;
                fc.callbacker = valueofCB;
                if (!fc.createParaScope()) { callbacker.noticeRunFailed(); valueofCB.noticeRunFailed(); return; }

				valueofCB.setWhenFailed(_InvokeValueOf_Backer_Failed);

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

		private static void _InvokeValueOf_Backer_Failed(BlockCallBackBase sender, object args)
		{
			object[] a = (object[])sender.args;

			SLOT returnValue = (SLOT)a[2];
			BlockCallBackBase callbacker = (BlockCallBackBase)a[1];

			callbacker.noticeRunFailed();
		}

		private static void _InvokeValueOf_Backer(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;

            SLOT returnValue = (SLOT)a[2];
            BlockCallBackBase callbacker = (BlockCallBackBase)a[1];

            if (returnValue.getValue().rtType > RunTimeDataType.unknown)
            {
                returnValue.directSet((rtObjectBase)a[0]); //valueOf取值不正确，返回原始对象
                
            }


            callbacker.call(null);

        }

        #endregion


        #region toString
        private static void _exec_toString_callback(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;

            ((StackFrame.DelegeExec)a[5])(
                ((SLOT)a[0]).getValue(),
                ((SLOT)a[1]).getValue(),
                (StackFrame)a[2],
                (OpStep)a[3],
                (RunTimeScope)a[4]
                );
        }

        public static void InvokeTwoToString(
            ASBinCode.RunTimeValueBase srcValue1,
            ASBinCode.RunTimeValueBase srcValue2,
            StackFrame frame,
            SourceToken token,
            RunTimeScope scope,
            SLOT _tempstoreto1,
            SLOT _tempstoreto2,
            ASBinCode.OpStep step,
            StackFrame.DelegeExec exec
            //BlockCallBackBase callbacker
            )
        {

            if (srcValue1 is rtObjectBase)
            {
                BlockCallBackBase callbacker = frame.player.blockCallBackPool.create();
                {
                    object[] backargs = callbacker.cacheObjects; //new object[6];
                    backargs[0] = _tempstoreto1;
                    backargs[1] = _tempstoreto2;
                    backargs[2] = frame;
                    backargs[3] = step;
                    backargs[4] = scope;
                    backargs[5] = exec;
                    callbacker.args = backargs;
                    callbacker.setCallBacker(_exec_toString_callback);
                }

                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                object[] tosend = cb.cacheObjects; //new object[6];
                tosend[0] = srcValue2;
                tosend[1] = frame;
                tosend[2] = token;
                tosend[3] = scope;
                tosend[4] = _tempstoreto2;
                tosend[5] = callbacker;
                cb.args = tosend;
                cb.setCallBacker(_AfterGetOneToString);

                InvokeToString((rtObjectBase)srcValue1, frame, token, scope, _tempstoreto1, cb);
            }
            else
            {
                _tempstoreto1.directSet(srcValue1);
                if (srcValue2 is rtObjectBase)
                {
                    BlockCallBackBase callbacker = frame.player.blockCallBackPool.create();
                    {
                        object[] backargs = callbacker.cacheObjects; //new object[6];
                        backargs[0] = _tempstoreto1;
                        backargs[1] = _tempstoreto2;
                        backargs[2] = frame;
                        backargs[3] = step;
                        backargs[4] = scope;
                        backargs[5] = exec;
                        callbacker.args = backargs;
                        callbacker.setCallBacker(_exec_toString_callback);
                    }

                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.setCallBacker(_AfterGetTwoToString);
					cb.setWhenFailed(_GetTwoStringFailed);
                    cb.args = callbacker;
                    InvokeToString((rtObjectBase)srcValue2, frame, token, scope, _tempstoreto2, cb);
                }
                else
                {
                    _tempstoreto2.directSet(srcValue2);
                    //callbacker.call(null);

                    exec(srcValue1, srcValue2, frame, step, scope);

                }
            }
        }

		private static void _GetTwoStringFailed(BlockCallBackBase sender, object args)
		{
			((BlockCallBackBase)sender.args).noticeRunFailed();
		}

		private static void _AfterGetOneToString(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            RunTimeValueBase srcValue2 = (RunTimeValueBase)a[0];
            StackFrame frame = (StackFrame)a[1]; //tosend[1] = frame;
            SourceToken token = (SourceToken)a[2]; //tosend[2] = token;
            RunTimeScope scope = (RunTimeScope)a[3];  //tosend[3] = scope;
            SLOT storeto = (SLOT)a[4]; //tosend[4] = _tempstoreto2;
            BlockCallBackBase callbacker = (BlockCallBackBase)a[5];


            if (srcValue2 is rtObjectBase)
            {
                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                cb.setCallBacker(_AfterGetTwoToString);
                cb.args = callbacker;
                InvokeToString((rtObjectBase)srcValue2, frame, token, scope, storeto, cb);
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
            rtObjectBase obj, StackFrame frame, SourceToken token, RunTimeScope scope, SLOT storeto,
            BlockCallBackBase callbacker
            )
        {
            //***调用toString()
            var toString = (ASBinCode.ClassMemberFinder.find(obj.value._class, "toString", obj.value._class));
            rtFunction function = null;rtFunction toclear = null;
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
                function = (rtFunction)((MethodGetterBase)toString.bindField).getMethod(obj);toclear = function;
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
						callbacker.noticeRunFailed();
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
                }
                else
                {
                    //***从Class定义的原型链中查找
                    var dobj = (ASBinCode.rtti.DynamicObject)
                        frame.player.static_instance[obj.value._class.staticClass.classid].value;

                    dobj = (ASBinCode.rtti.DynamicObject)((rtObjectBase)dobj.memberData[0].getValue()).value;
                    if (!dobj.hasproperty("toString"))
                    {

                        dobj = ((ASBinCode.rtti.DynamicObject)
                            frame.player.static_instance[obj.value._class.staticClass.classid].value);

                        bool haserror;
                        dobj = OpAccess_Dot.findInProtoType(dobj, "toString",
                            frame, token, out haserror);
                        if (haserror)
                        {
							callbacker.noticeRunFailed();
							frame.endStep();
                            
                            return;
                        }
                    }

                    if (dobj != null)
                    {
                        var prop = dobj["toString"].getValue();
                        if (prop is rtFunction)
                        {
                            function = (rtFunction)prop;

                            if (!ReferenceEquals(dobj, obj.value))
                            {
                                function = (rtFunction)function.Clone();
                                function.setThis(obj);
                            }

                        }
                    }
                }

            }

            if (function != null)
            {
                BlockCallBackBase toStringCB = frame.player.blockCallBackPool.create();
                toStringCB.setCallBacker(_InvokeToString_Backer);

                operators.FunctionCaller fc = frame.player.funcCallerPool.create( frame, token);
                //fc.releaseAfterCall = true;
                object[] sendargs = toStringCB.cacheObjects; //new object[5];
                sendargs[0] = obj;
                sendargs[1] = callbacker;
                sendargs[2] = storeto;
                //sendargs[3] = frame;
                //sendargs[4] = token;
                toStringCB.args = sendargs;

                fc.SetFunction(function);if (toclear != null) { toclear.Clear(); }
                fc.loadDefineFromFunction();
                fc.returnSlot = storeto;
                fc.callbacker = toStringCB;
                if (!fc.createParaScope()) { callbacker.noticeRunFailed(); toStringCB.noticeRunFailed(); return; }
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


        public static void exec_CastPrimitive(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            step.reg.getSlot(scope, frame).directSet
                ( 
                TypeConverter.ObjectImplicit_ToPrimitive( (rtObjectBase)step.arg1.getValue(scope, frame) )
                );
			frame.endStepNoError();
            //frame.endStep(step);
        }
    }
}
