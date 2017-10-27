using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASBinCode.rtData;

namespace ASRuntime.operators
{
    class OpCallFunction
    {
        public static void bind(StackFrame frame, ASBinCode.OpStep step,RunTimeScope scope)
        {
            var rv = step.arg1.getValue(frame.scope, frame);
            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(
                    step.token,0, "value is not a function");
            }
            else
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;

                if (function.bindScope == null
                    ||
                    function.bindScope.blockId == frame.scope.blockId
                    )
                {
                    function.bind(frame.scope);
                }

                if (!function.ismethod)
                {
                    if (function.this_pointer == null)
                    {
                        var s = frame.scope;
                        if (s.this_pointer != null && s.this_pointer is rtObject)
                        {
                            rtObject obj = (rtObject)s.this_pointer;

                            if (obj.value is Global_Object)
                            {
                                function.setThis(obj);
                            }
                            else
                            {
                                var cls = obj.value._class;
                                if (cls.staticClass == null)
                                {
                                    cls = cls.instanceClass;
                                }
                                if (cls.mainClass != null)
                                {
                                    cls = cls.mainClass;
                                }

                                var ot = frame.player.outpackage_runtimescope[cls.classid];
                                function.setThis(ot.this_pointer);
                            }
                            
                        }
                        else
                        {
                            if (frame.player.infoOutput !=null)
                            {

								frame.player.infoOutput.Warring("当前函数没有this指针。也许不是从文档类启动，而是从包外代码启动的");
                                
                            }
                        }
                    }
                }
            }
            frame.endStep(step);
        }

        private static void _do_clear_thispointer(Player player, 
            ASBinCode.rtData.rtFunction function, StackFrame frame,RunTimeValueBase outscope)
        {

            if (!function.ismethod)
            {
                function.setThis(outscope);
            }
        }

        public static void clear_thispointer( StackFrame frame, ASBinCode.OpStep step, RunTimeScope scope)
        {
            RunTimeValueBase rv;
			rtFunction toclear = null;
            if (step.arg1 is MethodGetterBase)
            {
                rv = ((ClassMethodGetter)step.arg1).getMethodForClearThis(frame.scope);
				toclear = (rtFunction)rv;
            }
            else
            {
                rv= step.arg1.getValue(frame.scope, frame);
            }

            if (rv.rtType > RunTimeDataType.unknown && ClassMemberFinder.check_isinherits(rv, RunTimeDataType._OBJECT + 2, frame.player.swc))
            {
                //***说明要调用强制类型转换***
                ASBinCode.rtti.Class cls = ((rtObject)rv).value._class;

                if (cls.explicit_from != null)
                {

                    var member = (MethodGetterBase)cls.explicit_from.bindField;
                    var func = member.getValue(((rtObject)rv).objScope,null);

                    step.reg.getSlot(scope, frame).directSet(func);

                }
                else if (cls.implicit_from != null)
                {
                    var member = (MethodGetterBase)cls.implicit_from.bindField;
                    var func = member.getValue(((rtObject)rv).objScope,null);

                    step.reg.getSlot(scope, frame).directSet(func);
                }
                else
                {
                    frame.typeconvertoperator = new typeConvertOperator();
                    frame.typeconvertoperator.targettype = cls;

                    step.reg.getSlot(scope, frame).directSet(rv);
                }

				if (toclear != null)
				{
					toclear.Clear();
				}

                frame.endStep(step);
                return;
            }


            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(
                    step.token,0, "value is not a function");
            }
            else
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;

                step.reg.getSlot(scope, frame).directSet(rv);

                if (!function.ismethod)
                {
                    int classid = ((ASBinCode.rtData.rtInt)step.arg2.getValue(scope, frame)).value;

                    var o = frame.player.outpackage_runtimescope[classid];
                    _do_clear_thispointer(frame.player, (ASBinCode.rtData.rtFunction)step.reg.getValue(scope, frame), frame,o.this_pointer);
                }

                
            }

			if (toclear != null)
			{
				toclear.Clear();
			}

			frame.endStep(step);
        }


        //public static void bind_this(Player player, StackFrame frame, ASBinCode.OpStep step)
        //{
        //    var rv = step.arg1.getValue(frame.scope);
        //    if (rv.rtType != RunTimeDataType.rt_function)
        //    {
        //        frame.throwError(new error.InternalError(step.token, "value is not a function",
        //            new ASBinCode.rtData.rtString("value is not a function")));
        //    }
        //    else
        //    {
        //        ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;
        //        function.setThis(frame.scope.this_pointer);
        //    }
        //    frame.endStep(step);
        //}


        public static void create_paraScope(StackFrame frame, ASBinCode.OpStep step,RunTimeScope scope)
        {
            RunTimeValueBase rv;rtFunction toclear = null;
            if (step.arg1 is MethodGetterBase)
            {
                rv = ((MethodGetterBase)step.arg1).getMethod(frame.scope);toclear = (rtFunction)rv;
            }
            else
            {
                rv = step.arg1.getValue(frame.scope, frame);
            }

            if (rv.rtType > RunTimeDataType.unknown && ClassMemberFinder.check_isinherits(rv, RunTimeDataType._OBJECT + 2, frame.player.swc))
            {
                //***说明要调用强制类型转换***
                ASBinCode.rtti.Class cls = ((rtObject)rv).value._class;
                if (frame.typeconvertoperator != null)
                {
                    frame.endStep(step);
                    return;
                }
                else if (frame.typeconvertoperator.targettype.instanceClass == null
                    ||
                    frame.typeconvertoperator.targettype !=cls
                    )
                {
                    frame.throwError(new error.InternalError(step.token, "类型转换函数发现内部错误"
                        ));
                    return;
                }
            }

            if (rv.rtType == frame.player.swc.FunctionClass.getRtType())
            {
                rv = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)rv);
            }

            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(step.token,0, "value is not a function");
            }
            else
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;
                var funcCaller = frame.player.funcCallerPool.create(frame.player, frame, step.token);
                funcCaller.SetFunction(function);if (toclear != null) { toclear.Clear(); }
                funcCaller._tempSlot = frame._tempSlot1;
                funcCaller.loadDefineFromFunction();
                if (!funcCaller.createParaScope()) { return; }
                //funcCaller.releaseAfterCall = true;

                frame.funCaller = funcCaller;
            }
            frame.endStep(step);
        }

        public static void push_parameter(StackFrame frame, ASBinCode.OpStep step,RunTimeScope scope)
        {
            int id = ((rtInt)step.arg2.getValue(frame.scope, frame)).value;
            RunTimeValueBase arg = step.arg1.getValue(frame.scope, frame);

            //**当function作为参数被传入时，重新绑定scope
            {
                if (arg.rtType == frame.player.swc.FunctionClass.getRtType())
                {
                    var function = (rtFunction)TypeConverter.ObjectImplicit_ToPrimitive((rtObject)arg);
                    if (!function.ismethod && (function.bindScope == null
                        ||
                        function.bindScope.blockId == frame.scope.blockId
                        )
                        )
                    {
                        function.bind(frame.scope);
                    }
                }
                else if (arg.rtType == RunTimeDataType.rt_function)
                {
                    var function = (rtFunction)arg;
                    if (!function.ismethod && (function.bindScope == null
                        ||
                        function.bindScope.blockId == frame.scope.blockId
                        )
                        )
                    {
                        function.bind(frame.scope);
                    }
                }

                
            }

            if (frame.typeconvertoperator != null)
            {
                if (frame.typeconvertoperator.inputvalue == null && id==0)
                {
                    frame.typeconvertoperator.inputvalue = arg;
                }
            }
            else
            {
                bool success;
                frame.funCaller.pushParameter(arg, id,out success);
				
            }
            frame.endStep(step);
        }



        public static void exec(StackFrame frame, ASBinCode.OpStep step,RunTimeScope scope)
        {
#if DEBUG

            RunTimeValueBase rv;rtFunction toclear = null;
            if (step.arg1 is MethodGetterBase)
            {
                rv = ((MethodGetterBase)step.arg1).getMethod(frame.scope);toclear = (rtFunction)rv;
            }
            else
            {
                rv = step.arg1.getValue(frame.scope, frame);
            }
            if (rv.rtType == frame.player.swc.FunctionClass.getRtType())
            {
                rv = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)rv);
            }
            if (rv.rtType > RunTimeDataType.unknown && ClassMemberFinder.check_isinherits(rv, RunTimeDataType._OBJECT + 2, frame.player.swc))
            {
                //***说明要调用强制类型转换***
                ASBinCode.rtti.Class cls = ((rtObject)rv).value._class;
                if (frame.typeconvertoperator == null || frame.typeconvertoperator.targettype != cls

                    )
                {
                    frame.throwError(new error.InternalError(step.token, "应该是强制类型转换，内部异常"));

                    frame.endStep(step);
                    return;
                }
                else if (frame.typeconvertoperator.inputvalue == null)
                {
                    frame.throwError(step.token,0, "Argument count mismatch on class coercion.  Expected 1, got 0."
                        );

                    frame.endStep(step);
                    return;
                }
            }
            else
            {
                if (rv.rtType != RunTimeDataType.rt_function)
                {
                    frame.throwError(step.token,0, "value is not a function"
                        );

                    frame.endStep(step);
                    return;
                }

                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;
                ASBinCode.rtti.FunctionDefine funcDefine = frame.player.swc.functions[function.functionId];


                if (!frame.funCaller.isFuncEquals(function))
                {
					if (toclear != null) { toclear.Clear(); }
                    frame.throwError(new error.InternalError(step.token, "运行时异常，调用函数不对"));
                    frame.endStep(step);
                    return;
                }
            }
#endif

            if (frame.typeconvertoperator == null)
            {
                //funbacker cb = new funbacker();
                //object[] args = new object[2];
                //args[0] = frame;
                //args[1] = step;
                //cb.args = args;

                
                frame.funCaller.callbacker = frame.funCaller;
                frame.funCaller.returnSlot = step.reg.getSlot(frame.scope, frame);
                frame.funCaller.call();

                frame.funCaller = null;
            }
            else
            {
                if (frame.typeconvertoperator.targettype.instanceClass == null)
                {
                    frame.throwError(new error.InternalError(step.token, "强制类型转换类型错误",
                        new ASBinCode.rtData.rtString("强制类型转换类型错误")));

                    frame.endStep(step);
                    return;
                }
                else
                {
                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.step = step;
                    cb.args = frame;
                    cb.setCallBacker(_convert_cb);

                    

                    OpCast.CastValue(frame.typeconvertoperator.inputvalue,
                        frame.typeconvertoperator.targettype.instanceClass.getRtType(),
                        frame, step.token, frame.scope, step.reg.getSlot(frame.scope, frame),
                        cb,
                        false);
                    frame.typeconvertoperator = null;
                }
            }

        }
        private static void _convert_cb(BlockCallBackBase sender,object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }


        //class funbacker : IBlockCallBack
        //{
        //    public object args
        //    {
        //        get
        //        ;

        //        set
        //        ;
        //    }

        //    public void call(object args)
        //    {
        //        object[] a = (object[])args;
        //        ((StackFrame)a[0]).endStep((OpStep)a[1]);
        //    }
        //}


        public static void exec_return(StackFrame frame, ASBinCode.OpStep step,RunTimeScope scope)
        {
            RunTimeValueBase result = step.arg1.getValue(frame.scope, frame);
            if (result.rtType == RunTimeDataType.rt_function)
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)result;
                if (!function.ismethod)//闭包
                {
                    function.setThis(null);
                }
            }

            frame.returnSlot.directSet(result);
            frame.endStep(step);
        }

        public static void exec_yieldreturn(StackFrame frame,OpStep step,RunTimeScope scope)
        {
            RunTimeValueBase result = step.arg1.getValue(scope, frame);
            if (result.rtType == RunTimeDataType.rt_function)
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)result;
                if (!function.ismethod)//闭包
                {
                    function.setThis(null);
                }
            }

            scope.memberData[scope.memberData.Length - 2].directSet( new rtInt( frame.codeLinePtr + 1));
            scope.memberData[scope.memberData.Length - 1].directSet(rtBoolean.True);

            frame.returnSlot.directSet(result);
            frame.endStep(step);

            //退出当前调用 *** yield语句不可能包含在try中，所以直接移动到最后一行退出即可
            frame.codeLinePtr = frame.block.opSteps.Count;
        }


        public class typeConvertOperator
        {
            public Class targettype;
            public RunTimeValueBase inputvalue;
        }

    }
}
