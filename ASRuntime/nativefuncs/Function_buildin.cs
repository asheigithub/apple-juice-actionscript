using ASBinCode;
using ASBinCode.rtData;
using ASRuntime.operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Function_fill : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Function_fill()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_function);
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
                return "_function_fill";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            ((rtObject)argements[0].getValue()).value.memberData[0].directSet(argements[1].getValue());
            ((rtFunction)argements[1].getValue()).objHandle.bindFunctionObj = ((rtObject)argements[0].getValue());

            errormessage = null;
            errorno = 0;
            return rtUndefined.undefined;

            //throw new NotImplementedException();
        }
    }

    class Function_load : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Function_load()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_function);
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
                return "_function_load";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            if (((rtFunction)argements[0].getValue()).objHandle.bindFunctionObj == null)
            {
                return rtNull.nullptr;
            }
            else
            {
                return ((rtFunction)argements[0].getValue()).objHandle.bindFunctionObj;
            }
        }
    }


    class Function_setPrototype : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Function_setPrototype()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
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
                return "_function_setPrototype";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            if (argements[0].getValue().rtType < RunTimeDataType.unknown)
            {
                errormessage = "Prototype objects must be vanilla Objects.";
                errorno = 1049;
            }
            else
            {
                ((rtObject)thisObj).value.memberData[1].directSet(argements[0].getValue());
            }
            return rtUndefined.undefined;
        }
    }




    class Function_apply : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;
        public Function_apply()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_array);
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
                return "_function_apply";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override bool isAsync
        {
            get
            {
                return true;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements, SLOT resultSlot, 
            object callbacker, 
            object stackframe, 
            SourceToken token, RunTimeScope scope)
        {
            //base.executeAsync(thisObj, argements, resultSlot, callbacker, stackframe, token, scope);

            var thisArg = argements[0].getValue();
            

            rtFunction func = (rtFunction)((rtObject)thisObj).value.memberData[0].getValue();
            rtFunction toApply= (rtFunction)func.Clone();

            //toApply.setThis(thisArg);
            operators.FunctionCaller caller = new operators.FunctionCaller(((StackFrame)stackframe).player, (StackFrame)(stackframe), token);
            caller.callbacker = (IBlockCallBack)callbacker;
            caller.function = toApply;
            caller.loadDefineFromFunction();
            caller.createParaScope();
            caller._tempSlot = ((StackFrame)stackframe)._tempSlot1;
            caller.returnSlot = resultSlot;

            if (argements[1].getValue().rtType != rtNull.nullptr.rtType)
            {
                rtArray argArray = (rtArray)argements[1].getValue();
                for (int i = 0; i < argArray.innerArray.Count; i++)
                {
                    caller.pushParameter(argArray.innerArray[i], i);
                }
            }

            if (!func.ismethod) //方法无法更改this
            {
                if (!(thisArg is rtObject))
                {
                    var player = ((StackFrame)stackframe).player;
                    var objtype = thisArg.rtType;
                    if (objtype < RunTimeDataType.unknown
                        &&
                        player.swc.primitive_to_class_table[objtype] != null
                        )
                    {
                        //***转换为对象***

                        OpCast.Primitive_to_Object(thisArg, (StackFrame)stackframe, token, scope,
                            ((StackFrame)stackframe)._tempSlot1,
                            null, _primitive_toObj);

                        stackCallers.Push(caller);

                        return;
                    }
                    else
                    {
                        caller.function.setThis(null);
                    }
                }
                else
                {
                    caller.function.setThis((rtObject)thisArg);
                }
            }
            caller.call();
        }

        private Stack<FunctionCaller> stackCallers = new Stack<FunctionCaller>();

        private  void _primitive_toObj(ASBinCode.RunTimeValueBase v1, 
            ASBinCode.RunTimeValueBase v_temp, 
            StackFrame frame, ASBinCode.OpStep step, 
            ASBinCode.RunTimeScope scope)
        {
            var c = stackCallers.Pop();
            if (v1.rtType < RunTimeDataType.unknown)
            {
                
                c.function.setThis(null);
                c.call();
                return;
            }
            else
            {
                rtObject rtObj = (rtObject)v1;
                c.function.setThis(rtObj);
                c.call();
                return;
            }
            

            
        }

    }



    class Function_call : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;
        public Function_call()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_array);
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
                return "_function_call";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override bool isAsync
        {
            get
            {
                return true;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements, SLOT resultSlot,
            object callbacker,
            object stackframe,
            SourceToken token, RunTimeScope scope)
        {
            //base.executeAsync(thisObj, argements, resultSlot, callbacker, stackframe, token, scope);

            var thisArg = argements[0].getValue();


            rtFunction func = (rtFunction)((rtObject)thisObj).value.memberData[0].getValue();
            rtFunction toApply = (rtFunction)func.Clone();

            //toApply.setThis(thisArg);
            operators.FunctionCaller caller = new operators.FunctionCaller(((StackFrame)stackframe).player, (StackFrame)(stackframe), token);
            caller.callbacker = (IBlockCallBack)callbacker;
            caller.function = toApply;
            caller.loadDefineFromFunction();
            caller.createParaScope();
            caller._tempSlot = ((StackFrame)stackframe)._tempSlot1;
            caller.returnSlot = resultSlot;

            if (argements[1].getValue().rtType == RunTimeDataType.rt_array)
            {
                rtArray argArray = (rtArray)argements[1].getValue();
                for (int i = 0; i < argArray.innerArray.Count; i++)
                {
                    caller.pushParameter(argArray.innerArray[i], i);
                }
            }

            if (!func.ismethod) //方法无法更改this
            {
                if (!(thisArg is rtObject))
                {
                    var player = ((StackFrame)stackframe).player;
                    var objtype = thisArg.rtType;
                    if (objtype < RunTimeDataType.unknown
                        &&
                        player.swc.primitive_to_class_table[objtype] != null
                        )
                    {
                        //***转换为对象***

                        OpCast.Primitive_to_Object(thisArg, (StackFrame)stackframe, token, scope,
                            ((StackFrame)stackframe)._tempSlot1,
                            null, _primitive_toObj);

                        stackCallers.Push(caller);

                        return;
                    }
                    else
                    {
                        caller.function.setThis(null);
                    }
                }
                else
                {
                    caller.function.setThis((rtObject)thisArg);
                }
            }
            caller.call();
        }

        private Stack<FunctionCaller> stackCallers = new Stack<FunctionCaller>();

        private void _primitive_toObj(ASBinCode.RunTimeValueBase v1,
            ASBinCode.RunTimeValueBase v_temp,
            StackFrame frame, ASBinCode.OpStep step,
            ASBinCode.RunTimeScope scope)
        {
            var c = stackCallers.Pop();
            if (v1.rtType < RunTimeDataType.unknown)
            {
                c.function.setThis(null);
                c.call();
                return;
            }
            else
            {
                rtObject rtObj = (rtObject)v1;
                c.function.setThis(rtObj);
                c.call();
                return;
            }



        }

    }

}
