using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.operators
{
    class OpCallFunction
    {
        public static void bind(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
            var rv = step.arg1.getValue(frame.scope);
            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(
                    new error.InternalError(step.token, "value is not a function",
                    new ASBinCode.rtData.rtString("value is not a function")));
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
                        if (s.this_pointer != null && s.this_pointer is ASBinCode.rtData.rtObject)
                        {
                            ASBinCode.rtData.rtObject obj = (ASBinCode.rtData.rtObject)s.this_pointer;

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

                                var ot = player.outpackage_runtimescope[cls.classid];
                                function.setThis(ot.this_pointer);
                            }
                            //while (!(((ASBinCode.rtData.rtObject)s.this_pointer).value is ASBinCode.rtti.Global_Object))
                            //{
                            //    s = s.parent;
                            //}
                            //function.setThis(s.this_pointer);
                        }
                        else
                        {
                            if (player.isConsoleOut)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("当前函数没有this指针。也许不是从文档类启动，而是从包外代码启动的");
                                Console.ResetColor();
                            }
                        }
                    }
                }
            }
            frame.endStep(step);
        }

        private static void _do_clear_thispointer(Player player, 
            ASBinCode.rtData.rtFunction function, StackFrame frame,IRunTimeValue outscope)
        {

            if (!function.ismethod)
            {
                function.setThis(outscope);
            }
        }

        public static void clear_thispointer(Player player, StackFrame frame, ASBinCode.OpStep step,IRunTimeScope scope)
        {
            var rv = step.arg1.getValue(frame.scope);
            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(
                    new error.InternalError(step.token, "value is not a function",
                    new ASBinCode.rtData.rtString("value is not a function")));
            }
            else
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;

                step.reg.getISlot(scope).directSet(rv);

                if (!function.ismethod)
                {
                    int classid = ((ASBinCode.rtData.rtInt)step.arg2.getValue(scope)).value;

                    var o = player.outpackage_runtimescope[classid];
                    _do_clear_thispointer(player, (ASBinCode.rtData.rtFunction)step.reg.getValue(scope), frame,o.this_pointer);
                }

                
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


        public static void create_paraScope(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
            IRunTimeValue rv;
            if (step.arg1 is ClassMethodGetter)
            {
                rv = ((ClassMethodGetter)step.arg1).getMethod(frame.scope);
            }
            else
            {
                rv = step.arg1.getValue(frame.scope);
            }
            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(new error.InternalError(step.token, "value is not a function",
                    new ASBinCode.rtData.rtString("value is not a function")));
            }
            else
            {
                ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;

                frame.funCaller = new FunctionCaller(player, frame, step.token);
                frame.funCaller.function = function;
                frame.funCaller._tempSlot = frame._tempSlot1;
                frame.funCaller.loadDefineFromFunction();
                frame.funCaller.createParaScope();
            }
            frame.endStep(step);
        }

        public static void push_parameter(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
            int id = ((ASBinCode.rtData.rtInt)step.arg2.getValue(frame.scope)).value;
            IRunTimeValue arg = step.arg1.getValue(frame.scope);

            frame.funCaller.pushParameter(arg, id);

            frame.endStep(step);
        }



        public static void exec(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
#if DEBUG

            IRunTimeValue rv;
            if (step.arg1 is ClassMethodGetter)
            {
                rv = ((ClassMethodGetter)step.arg1).getMethod(frame.scope);
            }
            else
            {
                rv = step.arg1.getValue(frame.scope);
            }

            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(new error.InternalError(step.token, "value is not a function",
                    new ASBinCode.rtData.rtString("value is not a function")));

                frame.endStep(step);
                return;
            }

            ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;
            ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[function.functionId];


            if (!frame.funCaller.function.Equals(function))
            {
                frame.throwError(new error.InternalError(step.token, "运行时异常，调用函数不对"));
                frame.endStep(step);
                return;
            }
#endif
            funbacker cb = new funbacker();
            object[] args = new object[2];
            args[0] = frame;
            args[1] = step;
            cb.args = args;

            frame.funCaller.callbacker = cb;
            frame.funCaller.returnSlot = step.reg.getISlot(frame.scope);
            frame.funCaller.call();

            frame.funCaller = null;


        }

        class funbacker : IBlockCallBack
        {
            public object args
            {
                get
                ;

                set
                ;
            }

            public void call(object args)
            {
                object[] a = (object[])args;
                ((StackFrame)a[0]).endStep((OpStep)a[1]);
            }
        }


        public static void exec_return(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
            IRunTimeValue result = step.arg1.getValue(frame.scope);
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

    }
}
