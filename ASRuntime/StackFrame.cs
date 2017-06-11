using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    /// <summary>
    /// 调用堆栈栈帧
    /// </summary>
    public sealed class StackFrame :RunTimeDataHolder
    {
        private static Stack<StackFrame> pool;
        static StackFrame()
        {
            pool = new Stack<StackFrame>();
            for (int i = 0; i < 1024; i++)
            {
                pool.Push(new StackFrame(null));
            }
        }
        
        public static StackFrame create(CodeBlock block)
        {
            StackFrame frame = pool.Pop();
            frame.block = block;
            frame.stepCount = block.opSteps.Count;

            frame.isclosed = false;

            
            return frame;
        }

        public static void checkpool()
        {
            if (pool.Count != 1024)
            {
                throw new ASRunTimeException("缓存池异常");
            }
        }

        private static void ret(StackFrame c)
        {
            
            pool.Push(c);
        }



        internal int stepCount;
        private StackFrame(CodeBlock block)
        {
            //this.block = block;
            //stepCount = block.opSteps.Count;

            
            tryCatchState = new Stack<TryState>();
            

        }

        internal FrameInfo getInfo()
        {
            return new FrameInfo(block, codeLinePtr,scope,
                //scope_thispointer,
                static_objects,offset,stack);
        }


        public delegate void DelegeExec(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope);

        internal enum Try_catch_finally
        {
            Try,
            Catch,
            Finally
        }

        internal struct TryState
        {
            public TryState(Try_catch_finally state, int id)
            {
                this.state = state;
                tryid = id;
            }

            public Try_catch_finally state;
            public int tryid;
        }

        internal operators.InstanceCreator instanceCreator;
        internal operators.FunctionCaller funCaller;
        internal operators.OpCallFunction.typeConvertOperator typeconvertoperator;
        internal StackSlot _tempSlot1;
        internal StackSlot _tempSlot2;

        internal int call_parameter_slotCount;

        //internal Dictionary<ASBinCode.ClassMethodGetter, Dictionary<rtObject, ISLOT>> _dictMethods
        //    =new Dictionary<ClassMethodGetter, Dictionary<rtObject, ISLOT>>();

        private Stack<TryState> tryCatchState;// = new Stack<TryState>();
        /// <summary>
        /// 暂存已发生的错误
        /// </summary>
        private error.InternalError holdedError;
        /// <summary>
        /// 执行阶段发生的错误
        /// </summary>
        internal error.InternalError runtimeError;

        /// <summary>
        /// 暂存是否调用了return
        /// </summary>
        internal bool holdHasCallReturn;

        /// <summary>
        /// 暂存是否调用了跳转
        /// </summary>
        internal bool holdhasjumpto;
        internal int holdjumptoline;

        /// <summary>
        /// 当前指向的指令行
        /// </summary>
        public int codeLinePtr;

        /// <summary>
        /// 代码段
        /// </summary>
        public ASBinCode.CodeBlock block;

        public Player player;

        public ASBinCode.RunTimeScope scope;

        
        /// <summary>
        /// 返回值存储槽
        /// </summary>
        public ASBinCode.SLOT returnSlot;
        /// <summary>
        /// 如果非null,则退出时会回调。
        /// </summary>
        internal IBlockCallBack callbacker;



        public bool IsEnd()
        {
            return  codeLinePtr >= stepCount ;
        }

        /// <summary>
        /// 运行一行
        /// </summary>
        public void step()
        {
            OpStep step = block.opSteps[codeLinePtr];
            //exec(step);
#if DEBUG
            if (execing)
            {
                throw new ASRunTimeException();
            }

            execing = true;
#endif
            
            switch (step.opCode)
            {
                case OpCode.cast:
                    operators.OpCast.execCast(this, step, scope);
                    break;
                case OpCode.cast_primitive:
                    operators.OpCast.exec_CastPrimitive(this, step, scope);
                    break;
                case OpCode.assigning:
                    operators.OpAssigning.execAssigning(this, step, scope);
                    break;

                case OpCode.add_number:
                    operators.OpAdd.execAdd_Number(this, step, scope);
                    break;
                case OpCode.add_string:
                    operators.OpAdd.execAdd_String(this, step, scope);
                    break;
                case OpCode.add:
                    operators.OpAdd.execAdd(this, step, scope);
                    break;
                case OpCode.sub_number:
                    operators.OpSub.execSub_Number(this, step, scope);
                    break;
                case OpCode.sub:
                    operators.OpSub.execSub(this, step, scope);
                    break;
                case OpCode.multi:
                    operators.OpMulti.execMulti(this, step, scope);
                    break;
                case OpCode.multi_number:
                    operators.OpMulti.exec_MultiNumber(this, step, scope);
                    break;
                case OpCode.div:
                    operators.OpMulti.execDiv(this, step, scope);
                    break;
                case OpCode.div_number:
                    operators.OpMulti.exec_DivNumber(this, step, scope);
                    break;
                case OpCode.mod:
                    operators.OpMulti.execMod(this, step, scope);
                    break;
                case OpCode.mod_number:
                    operators.OpMulti.exec_ModNumber(this, step, scope);
                    break;
                case OpCode.unary_plus:
                    operators.OpUnaryPlus.execUnaryPlus(this, step, scope);
                    break;
                case OpCode.neg:
                    operators.OpNeg.execNeg(this, step, scope);
                    break;
                case OpCode.gt_num:
                    operators.OpLogic.execGT_NUM(this, step, scope);
                    break;
                case OpCode.gt_void:
                    operators.OpLogic.execGT_Void(this, step, scope);
                    break;
                case OpCode.lt_num:
                    operators.OpLogic.execLT_NUM(this, step, scope);
                    break;
                //case OpCode.lt_int_int:
                //    operators.OpLogic.execLT_IntInt(this, step, scope);
                //    break;
                case OpCode.lt_void:
                    operators.OpLogic.execLT_VOID(this, step, scope);
                    break;
                case OpCode.ge_num:
                    operators.OpLogic.execGE_NUM(this, step, scope);
                    break;
                case OpCode.ge_void:
                    operators.OpLogic.execGE_Void(this, step, scope);
                    break;
                case OpCode.le_num:
                    operators.OpLogic.execLE_NUM(this, step, scope);
                    break;
                case OpCode.le_void:
                    operators.OpLogic.execLE_VOID(this, step, scope);
                    break;
                case OpCode.equality:
                    operators.OpLogic.execEQ(this, step, scope);
                    break;
                case OpCode.not_equality:
                    operators.OpLogic.execNotEQ(this, step, scope);
                    break;
                case OpCode.equality_num_num:
                    operators.OpLogic.execEQ_NumNum(this, step, scope);
                    break;
                case OpCode.not_equality_num_num:
                    operators.OpLogic.execNotEQ_NumNum(this, step, scope);
                    break;
                case OpCode.equality_str_str:
                    operators.OpLogic.execEQ_StrStr(this, step, scope);
                    break;
                case OpCode.not_equality_str_str:
                    operators.OpLogic.execNotEQ_StrStr(this, step, scope);
                    break;
                case OpCode.strict_equality:
                    operators.OpLogic.execStrictEQ(this, step, scope);
                    break;
                case OpCode.not_strict_equality:
                    operators.OpLogic.execStrictNotEQ(this, step, scope);
                    break;
                case OpCode.logic_not:
                    operators.OpLogic.execNOT(this, step, scope);
                    break;
                case OpCode.bitAnd:
                    operators.OpBit.execBitAnd(this, step, scope);
                    break;
                case OpCode.bitOr:
                    operators.OpBit.execBitOR(this, step, scope);
                    break;
                case OpCode.bitXOR:
                    operators.OpBit.execBitXOR(this, step, scope);
                    break;
                case OpCode.bitNot:
                    operators.OpBit.execBitNot(this, step, scope);
                    break;
                case OpCode.bitLeftShift:
                    operators.OpBit.execBitLeftShift(this, step, scope);
                    break;
                case OpCode.bitRightShift:
                    operators.OpBit.execBitRightShift(this, step, scope);
                    break;
                case OpCode.bitUnsignedRightShift:
                    operators.OpBit.execBitUnSignRightShift(this, step, scope);
                    break;
                case OpCode.increment:
                    operators.OpIncrementDecrement.execIncrement(this, step, scope);
                    break;
                case OpCode.increment_int:
                    operators.OpIncrementDecrement.execIncInt(this, step, scope);
                    break;
                case OpCode.increment_uint:
                    operators.OpIncrementDecrement.execIncUInt(this, step, scope);
                    break;
                case OpCode.increment_number:
                    operators.OpIncrementDecrement.execIncNumber(this, step, scope);
                    break;
                case OpCode.decrement:
                    operators.OpIncrementDecrement.execDecrement(this, step, scope);
                    break;

                case OpCode.decrement_int:
                    operators.OpIncrementDecrement.execDecInt(this, step, scope);
                    break;
                case OpCode.decrement_uint:
                    operators.OpIncrementDecrement.execDecUInt(this, step, scope);
                    break;
                case OpCode.decrement_number:
                    operators.OpIncrementDecrement.execDecNumber(this, step, scope);
                    break;

                case OpCode.suffix_inc:
                    operators.OpIncrementDecrement.execSuffixInc(this, step, scope);
                    break;
                case OpCode.suffix_inc_int:
                    operators.OpIncrementDecrement.execSuffixIncInt(this, step, scope);
                    break;
                case OpCode.suffix_inc_uint:
                    operators.OpIncrementDecrement.execSuffixIncUint(this, step, scope);
                    break;
                case OpCode.suffix_inc_number:
                    operators.OpIncrementDecrement.execSuffixIncNumber(this, step, scope);
                    break;
                case OpCode.suffix_dec:
                    operators.OpIncrementDecrement.execSuffixDec(this, step, scope);
                    break;
                case OpCode.suffix_dec_int:
                    operators.OpIncrementDecrement.execSuffixDecInt(this, step, scope);
                    break;
                case OpCode.suffix_dec_uint:
                    operators.OpIncrementDecrement.execSuffixDecUInt(this, step, scope);
                    break;
                case OpCode.suffix_dec_number:
                    operators.OpIncrementDecrement.execSuffixDecNumber(this, step, scope);
                    break;
                case OpCode.flag:
                    //标签行，不做任何操作
                    endStep(step);
                    break;
                case OpCode.if_jmp:
                    {
                        if (((rtBoolean)step.arg1.getValue(scope, this)).value)//ReferenceEquals(ASBinCode.rtData.rtBoolean.True, step.arg1.getValue(scope)))
                        {
                            if (trystateCount != 0)
                            {
                                hasCallJump = true;
                                jumptoline = codeLinePtr + step.jumoffset - 1;
                                endStep(step);
                                break;
                            }
                            else
                            {
                                codeLinePtr += step.jumoffset - 1;
                                endStep(step);
                                break;
                            }
                        }
                        else
                        {
                            endStep(step);
                        }
                    }
                    break;
                case OpCode.jmp:
                    if (trystateCount != 0)
                    {
                        hasCallJump = true;
                        jumptoline = codeLinePtr + step.jumoffset - 1;
                        endStep(step);
                        break;
                    }
                    else
                    {
                        codeLinePtr += step.jumoffset - 1;
                        endStep(step);
                        break;
                    }
                case OpCode.raise_error:
                    nativefuncs.Throw.exec(this, step, scope);
                    break;
                case OpCode.enter_try:
                    {
                        int tryid = ((rtInt)step.arg1.getValue(scope,this)).value;
                        enter_try(tryid);

                        endStep(step);
                    }
                    break;
                case OpCode.quit_try:
                    {
                        int tryid = ((rtInt)step.arg1.getValue(scope,this)).value;
                        quit_try(tryid, step.token);

                        endStep(step);
                    }
                    break;
                case OpCode.enter_catch:
                    {
                        int catchid = ((rtInt)step.arg1.getValue(scope,this)).value;
                        enter_catch(catchid);

                        endStep(step);
                    }
                    break;
                case OpCode.quit_catch:
                    {
                        int catchid = ((rtInt)step.arg1.getValue(scope,this)).value;
                        quit_catch(catchid, step.token);

                        endStep(step);
                    }
                    break;
                case OpCode.enter_finally:
                    {
                        int finallyid = ((rtInt)step.arg1.getValue(scope,this)).value;
                        enter_finally(finallyid);

                        endStep(step);
                    }
                    break;
                case OpCode.quit_finally:
                    {
                        int finallyid = ((rtInt)step.arg1.getValue(scope,this)).value;
                        quit_finally(finallyid, step.token);

                        endStep(step);
                    }
                    break;
                case OpCode.native_trace:
                    nativefuncs.Trace.exec(this, step, scope);
                    break;
                case OpCode.bind_scope:
                    operators.OpCallFunction.bind(this, step, scope);
                    break;
                case OpCode.clear_thispointer:
                    operators.OpCallFunction.clear_thispointer(this, step, scope);
                    break;
                case OpCode.make_para_scope:
                    operators.OpCallFunction.create_paraScope(this, step, scope);
                    break;
                case OpCode.push_parameter:
                    operators.OpCallFunction.push_parameter(this, step, scope);
                    break;
                case OpCode.call_function:
                    operators.OpCallFunction.exec(this, step, scope);
                    break;
                case OpCode.function_return:
                    hasCallReturn = true;
                    operators.OpCallFunction.exec_return(this, step, scope);

                    break;

                case OpCode.new_instance:
                    operators.OpCreateInstance.exec(this, step, scope);
                    break;
                case OpCode.init_staticclass:
                    operators.OpCreateInstance.init_static(this, step, scope);
                    break;
                case OpCode.new_instance_class:
                    operators.OpCreateInstance.exec_instanceClass(this, step, scope);
                    break;
                case OpCode.prepare_constructor_argement:
                    operators.OpCreateInstance.prepareConstructorArgements(this, step, scope);
                    break;
                case OpCode.prepare_constructor_class_argement:
                    operators.OpCreateInstance.prepareConstructorClassArgements(this, step, scope);
                    break;
                case OpCode.push_parameter_class:
                    operators.OpCreateInstance.push_parameter_class(this, step, scope);
                    break;
                case OpCode.access_dot:
                    operators.OpAccess_Dot.exec_dot(this, step, scope);
                    break;
                case OpCode.access_dot_byname:
                    operators.OpAccess_Dot.exec_dot_byname(this, step, scope);
                    break;
                case OpCode.bracket_access:
                    operators.OpAccess_Dot.exec_bracket_access(this, step, scope);
                    break;
                case OpCode.bracket_byname:
                    operators.OpAccess_Dot.exec_dot_byname(this, step, scope);
                    break;
                case OpCode.access_method:
                    operators.OpAccess_Dot.exec_method(this, step, scope);
                    break;
                case OpCode.delete_prop:
                    operators.OpDynamicProperty.exec_delete(this, step, scope);
                    break;
                case OpCode.set_dynamic_prop:
                    operators.OpDynamicProperty.exec_set_dynamic_prop(this, step, scope);
                    break;
                case OpCode.try_read_getter:
                    operators.OpPropGetSet.exec_try_read_prop(this, step, scope);
                    break;
                case OpCode.try_write_setter:
                    operators.OpPropGetSet.exec_try_write_prop(this, step, scope);
                    break;
                case OpCode.array_push:
                    operators.OpArray.exec_Push(this, step, scope);
                    break;
                case OpCode.array_create:
                    operators.OpArray.exec_create(this, step, scope);
                    break;
                case OpCode.vectorAccessor_bind:
                    operators.OpVector.exec_AccessorBind(this, step, scope);
                    break;
                case OpCode.vector_push:
                    operators.OpVector.exec_push(this, step, scope);
                    break;
                case OpCode.vector_pusharray:
                    operators.OpVector.exec_pusharray(this, step, scope);
                    break;
                case OpCode.vector_pushvector:
                    operators.OpVector.exec_pushVector(this, step, scope);
                    break;
                case OpCode.vectorAccessor_convertidx:
                    operators.OpVector.exec_AccessorBind_ConvertIdx(this, step, scope);
                    break;
                case OpCode.vector_initfrmdata:
                    operators.OpVector.exec_initfromdata(this, step, scope);
                    break;
                case OpCode.link_outpackagevairable:
                    operators.OpLinkOutPackageScope.exec_link(this, step, scope);
                    break;
                case OpCode.flag_call_super_constructor:
                    endStep(step);
                    break;
                case OpCode.forin_get_enumerator:
                    operators.OpForIn.forin_get_enumerator(this, step, scope);
                    break;
                case OpCode.enumerator_movenext:
                    operators.OpForIn.enumerator_movenext(this, step, scope);
                    break;
                case OpCode.enumerator_current:
                    operators.OpForIn.enumerator_current(this, step, scope);
                    break;
                case OpCode.enumerator_close:
                    operators.OpForIn.enumerator_close(this, step, scope);
                    break;
                case OpCode.foreach_get_enumerator:
                    operators.OpForIn.foreach_get_enumerator(this, step, scope);
                    break;
                case OpCode.logic_is:
                    operators.OpLogic.exec_IS(this, step, scope);
                    break;
                case OpCode.logic_instanceof:
                    operators.OpLogic.exec_instanceof(this, step, scope);
                    break;
                case OpCode.convert_as:
                    operators.OpLogic.exec_AS(this, step, scope);
                    break;
                case OpCode.logic_in:
                    operators.OpLogic.exec_In(this, step, scope);
                    break;
                case OpCode.unary_typeof:
                    operators.OpTypeOf.exec_TypeOf(this, step, scope);
                    break;
                case OpCode.function_create:
                    {
                        rtArray arr = (rtArray)step.arg1.getValue(scope,this);
                        int funcid = ((rtInt)arr.innerArray[0]).value;
                        bool ismethod = ((rtBoolean)arr.innerArray[1]).value;

                        rtFunction function = new rtFunction(funcid, ismethod);
                        function.bind(scope);
                        step.reg.getSlot(scope,this).directSet(function);
                        
                        endStep(step);
                    }
                    break;
                case OpCode.yield_return:
                    
                    operators.OpCallFunction.exec_yieldreturn(this, step, scope);

                    break;
                case OpCode.yield_continuetoline:
                    {
                        //跳转继续下一次yield
                        codeLinePtr = ((rtInt)scope.memberData[scope.memberData.Length - 2].getValue()).value - 1;
                        endStep(step);
                    }
                    break;
                case OpCode.yield_break:
                    hasCallReturn = true;
                    returnSlot.directSet(rtUndefined.undefined);
                    endStep(step);
                    break;
                default:

                    runtimeError = (new error.InternalError(step.token,
                         step.opCode + "操作未实现"
                         ));
                    endStep();
                    break;
            }
        }

        internal void endStep()
        {
            endStep(block.opSteps[codeLinePtr]);
        }

        internal void endStep(OpStep step)
        {
#if DEBUG
            if (!execing || isclosed)
            {
                throw new ASRunTimeException();
            }
            execing = false;
            
#endif
            if (hasCallJump || hasCallReturn || runtimeError !=null)
            {
                doTryCatchReturn(step);
            }
            if (!isclosed)//在doTryCatchReturn步骤里可能修改了isclosed.
            {
                codeLinePtr++;
            }
            
        }
#if DEBUG
        internal bool execing = false;
#endif
        //private void exec(ASBinCode.OpStep step)
        //{

        //}

        internal bool hasCallReturn;

        internal bool hasCallJump;
        internal int jumptoline;

        private void doTryCatchReturn(OpStep step)
        {

            //检查该步骤是否发生错误
            {
                if (runtimeError != null)
                {

                    if (runtimeError.catchable
                            &&
                        trystateCount>0
                            &&
                        tryCatchState.Peek().state == Try_catch_finally.Try
                        )
                    {
                        //先脱掉try;
                        int tryid = quit_try(tryCatchState.Peek().tryid, step.token);
                        var err = runtimeError;
                        //***清除运行时错误***
                        runtimeError = null;

                        bool foundcatch = false;

                        RunTimeValueBase errorValue = err.errorValue;
                        //***查找匹配catch找到后给捕获异常变量赋值.**
                        for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                        {
                            var op = block.opSteps[j];
                            if (op.opCode == OpCode.catch_error
                                )
                            {
                                if (nativefuncs.Catch.isCatchError(tryid, errorValue, op, scope,this))
                                {
                                    op.reg.getSlot(scope,this).directSet(errorValue);
                                    //引导到catch块
                                    codeLinePtr = j;
                                    foundcatch = true;
                                    break;
                                }
                            }
                        }
                        //catch块不存在,前往finally块
                        if (!foundcatch)
                        {
                            holdedError = err;
                            for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                            {
                                var op = block.opSteps[j];
                                if (op.opCode == OpCode.enter_finally)
                                {
                                    int id = ((ASBinCode.rtData.rtInt)
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                    if (id == tryid)
                                    {
                                        codeLinePtr = j - 1;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                    else if (runtimeError.catchable
                        &&
                        trystateCount>0
                            &&
                        tryCatchState.Peek().state == Try_catch_finally.Catch
                        )
                    {
                        //catch块中抛错,****移动到finally块***
                        int tryid = quit_catch(tryCatchState.Peek().tryid, step.token);
                        var err = runtimeError;
                        //***清除运行时错误***
                        runtimeError = null;

                        holdedError = err;
                        for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                        {
                            var op = block.opSteps[j];
                            if (op.opCode == OpCode.enter_finally)
                            {
                                int id = ((ASBinCode.rtData.rtInt)
                                    ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                if (id == tryid)
                                {
                                    codeLinePtr = j - 1;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (callbacker != null)
                        {
                            callbacker.noticeRunFailed();
                            callbacker = null;
                        }
                        if (instanceCreator != null)
                        {
                            if (instanceCreator.callbacker != null)
                            {
                                instanceCreator.callbacker.noticeRunFailed();
                            }
                        }
                        if (funCaller != null)
                        {
                            if (funCaller.callbacker != null)
                            {
                                funCaller.callbacker.noticeRunFailed();
                            }
                            
                        }
                        player.exitStackFrameWithError(runtimeError,this);
                    }
                }
                else if (hasCallReturn) //已经调用了Return
                {                       //将进入finally块
                    if (
                        trystateCount>0
                            &&
                        tryCatchState.Peek().state == Try_catch_finally.Try
                        )
                    {
                        //先脱掉try;
                        int tryid = quit_try(tryCatchState.Peek().tryid, step.token);
                        //前往finally块
                        hasCallReturn = false;
                        holdHasCallReturn = true;
                        {
                            for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                            {
                                var op = block.opSteps[j];
                                if (op.opCode == OpCode.enter_finally)
                                {
                                    int id = ((ASBinCode.rtData.rtInt)
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                    if (id == tryid)
                                    {
                                        codeLinePtr = j - 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (trystateCount > 0
                            &&
                        tryCatchState.Peek().state == Try_catch_finally.Catch)
                    {
                        //脱掉catch
                        int tryid = quit_catch(tryCatchState.Peek().tryid, step.token);
                        //前往finally块
                        hasCallReturn = false;
                        holdHasCallReturn = true;
                        {
                            for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                            {
                                var op = block.opSteps[j];
                                if (op.opCode == OpCode.enter_finally)
                                {
                                    int id = ((ASBinCode.rtData.rtInt)
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                    if (id == tryid)
                                    {
                                        codeLinePtr = j - 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (trystateCount > 0
                            &&
                        tryCatchState.Peek().state == Try_catch_finally.Finally)
                    {
                        //***在finally中return**
                        //前往退出finally;
                        int tryid = tryCatchState.Peek().tryid;
                        hasCallReturn = false;
                        holdHasCallReturn = true;
                        {
                            for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                            {
                                var op = block.opSteps[j];
                                if (op.opCode == OpCode.quit_finally)
                                {
                                    int id = ((ASBinCode.rtData.rtInt)
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                    if (id == tryid)
                                    {
                                        codeLinePtr = j - 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        codeLinePtr = block.opSteps.Count;
                    }

                }
                else if (hasCallJump)
                {
                    if (trystateCount>0)
                    {

                        //Stack<int> jumptolinetrys = block.opSteps[jumptoline + 1].trys;

                        //if ((jumptolinetrys != null && jumptolinetrys.Peek() != tryCatchState.Peek().tryid)
                        //    ||
                        //    jumptolinetrys == null
                        //    )
                        int jumptolinetry = block.opSteps[jumptoline + 1].tryid;
                        int jumptolinetrytype = block.opSteps[jumptoline + 1].trytype;
                        if (//jumptolinetry==-1
                            //||
                            jumptolinetry != step.tryid || jumptolinetrytype !=step.trytype //tryCatchState.Peek().tryid
                            )
                        {
                            if (
                                //tryCatchState.Count > 0
                                //    &&
                                tryCatchState.Peek().state == Try_catch_finally.Try
                                )
                            {
                                //先脱掉try;
                                int tryid = quit_try(tryCatchState.Peek().tryid, step.token);
                                //前往finally块
                                hasCallJump = false;
                                holdhasjumpto = true;
                                holdjumptoline = jumptoline;
                                jumptoline = 0;
                                {
                                    for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                                    {
                                        var op = block.opSteps[j];
                                        if (op.opCode == OpCode.enter_finally)
                                        {
                                            int id = ((ASBinCode.rtData.rtInt)
                                                ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                            if (id == tryid)
                                            {
                                                codeLinePtr = j - 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (//tryCatchState.Count > 0
                                    //&&
                                tryCatchState.Peek().state == Try_catch_finally.Catch)
                            {
                                //脱掉catch
                                int tryid = quit_catch(tryCatchState.Peek().tryid, step.token);
                                //前往finally块
                                hasCallJump = false;
                                holdhasjumpto = true;
                                holdjumptoline = jumptoline;
                                jumptoline = 0;

                                {
                                    for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                                    {
                                        var op = block.opSteps[j];
                                        if (op.opCode == OpCode.enter_finally)
                                        {
                                            int id = ((ASBinCode.rtData.rtInt)
                                                ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                            if (id == tryid)
                                            {
                                                codeLinePtr = j - 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (//tryCatchState.Count > 0
                                    //&&
                                tryCatchState.Peek().state == Try_catch_finally.Finally)
                            {
                                //***在finally中break**
                                //前往退出finally;
                                int tryid = tryCatchState.Peek().tryid;
                                hasCallJump = false;
                                holdhasjumpto = true;
                                holdjumptoline = jumptoline;
                                jumptoline = 0;
                                {
                                    for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                                    {
                                        var op = block.opSteps[j];
                                        if (op.opCode == OpCode.quit_finally)
                                        {
                                            int id = ((ASBinCode.rtData.rtInt)
                                                ((ASBinCode.rtData.RightValue)op.arg1).getValue(null,null)).value;
                                            if (id == tryid)
                                            {
                                                codeLinePtr = j - 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                hasCallJump = false;
                                codeLinePtr = jumptoline;
                                jumptoline = 0;
                            }
                        }
                        else
                        {
                            hasCallJump = false;
                            codeLinePtr = jumptoline;
                            jumptoline = 0;
                        }
                    }
                    else
                    {
                        hasCallJump = false;
                        codeLinePtr = jumptoline;
                        jumptoline = 0;
                    }

                }
                
            }
        }

        /// <summary>
        /// 接收从调用栈底发来的异常
        /// </summary>
        /// <param name="error"></param>
        internal void receiveErrorFromStackFrame(error.InternalError error)
        {
            runtimeError = error;
            endStep(block.opSteps[codeLinePtr]);
            
        }

        internal void enter_try( int tryid)
        {
            tryCatchState.Push(new TryState(Try_catch_finally.Try, tryid));
            ++trystateCount;
        }

        internal int quit_try( int tryid, SourceToken token)
        {
            var s = tryCatchState.Pop();
            --trystateCount;
            if (s.state != Try_catch_finally.Try || s.tryid != tryid)
            {
                //player.runtimeError = (new error.InternalError(token,
                //                                                    "运行时异常 try块不匹配"
                //                                                    ));
                //引擎异常，抛出
                throw new ASRunTimeException();
            }
            return s.tryid;
        }

        internal void enter_catch( int catchid)
        {
            tryCatchState.Push(new TryState(Try_catch_finally.Catch, catchid));
            ++trystateCount;
        }
        internal int quit_catch( int catchid, SourceToken token)
        {
            var s = tryCatchState.Pop();
            --trystateCount;
            if (s.state != Try_catch_finally.Catch || s.tryid != catchid)
            {
                //player.runtimeError = (new error.InternalError(token,
                //                                                    "运行时异常 catch块不匹配"
                //                                                    ));
                //引擎异常，抛出
                throw new ASRunTimeException();
            }
            return s.tryid;
        }

        internal int trystateCount;
        internal void enter_finally( int finallyid)
        {
            tryCatchState.Push(new TryState(Try_catch_finally.Finally, finallyid));
            ++trystateCount;
        }
        internal int quit_finally( int finallyid, SourceToken token)
        {
            var s = tryCatchState.Pop();
            --trystateCount;
            if (s.state != Try_catch_finally.Finally || s.tryid != finallyid)
            {
                //player.runtimeError = (new error.InternalError(token,
                //                                                    "运行时异常 finally块不匹配"
                //                                                    ));
                //引擎异常，抛出
                throw new ASRunTimeException();
            }


            // finally块执行完成后，再次抛出异常
            if (holdedError != null)
            {
                runtimeError = holdedError;
                holdedError = null;
            }

            hasCallReturn = holdHasCallReturn;

            if (holdhasjumpto)
            {
                hasCallJump = holdhasjumpto;
                jumptoline = holdjumptoline;
                holdhasjumpto = false;
                holdjumptoline = 0;
            }
            if (hasCallReturn)
            {
                doTryCatchReturn(block.opSteps[codeLinePtr]);
            }
            else if (hasCallJump)
            {
                doTryCatchReturn(block.opSteps[codeLinePtr]);
            }

            return s.tryid;
        }


        public void throwCastException(ASBinCode.SourceToken token, RunTimeDataType srctype, RunTimeDataType dsttype)
        {
            string src = srctype.ToString();
            string dst = dsttype.ToString();

            if (srctype > RunTimeDataType.unknown)
            {
                src = player.swc.classes[srctype - RunTimeDataType._OBJECT].name;
            }

            if (dsttype > RunTimeDataType.unknown)
            {
                dst = player.swc.classes[dsttype - RunTimeDataType._OBJECT].name;
            }

            string message = "类型转换失败:" + src + "->" + dst;

            if (player.swc.ErrorClass != null)
            {
                //***直接开上帝视角从对象里取值赋值***
                var errorinstance =
                    ((rtObject)player.outpackage_runtimescope[player.swc.ErrorClass.classid].memberData[1].getValue());

                errorinstance.value.memberData[0].directSet(new rtString(message));
                errorinstance.value.memberData[1].directSet(new rtString("TypeError"));
                errorinstance.value.memberData[2].directSet(new rtInt(1034));
                errorinstance.value.memberData[3].directSet(new rtString(player.stackTrace(0)));
                runtimeError = (new error.InternalError(token,message,errorinstance));

            }
            else
            {
                runtimeError = (new error.InternalError(token, "类型转换失败:" + src + "->" + dst));
            }
        }

        public void throwArgementException(ASBinCode.SourceToken token,string errormessage)
        {
            if (player.swc.ErrorClass != null)
            {
                //***直接开上帝视角从对象里取值赋值***
                var errorinstance =
                    ((rtObject)player.outpackage_runtimescope[player.swc.ErrorClass.classid].memberData[2].getValue());

                errorinstance.value.memberData[0].directSet(new rtString(errormessage));
                errorinstance.value.memberData[1].directSet(new rtString("ArgumentError"));
                errorinstance.value.memberData[2].directSet(new rtInt(1063));
                errorinstance.value.memberData[3].directSet(new rtString(player.stackTrace(0)));
                runtimeError = (new error.InternalError(token, errormessage, errorinstance));

            }
            else
            {
                runtimeError = (new error.InternalError(token, errormessage));
            }
        }

        public void throwOpException(ASBinCode.SourceToken token, OpCode opcode)
        {
            if (player.swc.ErrorClass != null)
            {
                //***直接开上帝视角从对象里取值赋值***
                var errorinstance =
                    ((rtObject)player.outpackage_runtimescope[player.swc.ErrorClass.classid].memberData[0].getValue());

                errorinstance.value.memberData[0].directSet(new rtString("无法执行操作" + opcode));
                errorinstance.value.memberData[1].directSet(new rtString("Error"));
                errorinstance.value.memberData[2].directSet(new rtInt(0));
                errorinstance.value.memberData[3].directSet(new rtString(player.stackTrace(0)));

                runtimeError = (new error.InternalError(token, "无法执行操作" + opcode, errorinstance));

            }
            else
            {
                runtimeError = (new error.InternalError(token, "无法执行操作" + opcode));
            }
        }

        internal void throwError(error.InternalError err)
        {
            runtimeError = err;
        }

        public override  void throwError(SourceToken token,int code,string errormessage)
        {
            if (player.swc.ErrorClass != null)
            {
                //***直接开上帝视角从对象里取值赋值***
                var errorinstance =
                    ((rtObject)player.outpackage_runtimescope[player.swc.ErrorClass.classid].memberData[0].getValue());

                errorinstance.value.memberData[0].directSet(new rtString(errormessage));
                errorinstance.value.memberData[1].directSet(new rtString("Error"));
                errorinstance.value.memberData[2].directSet(new rtInt(code));
                errorinstance.value.memberData[3].directSet(new rtString(player.stackTrace(0)));

                runtimeError = (new error.InternalError(token, errormessage, errorinstance));

            }
            else
            {
                runtimeError = (new error.InternalError(token, errormessage));
            }

            
        }

        public void throwAneException(ASBinCode.SourceToken token, string errormessage)
        {
            if (player.swc.ErrorClass != null)
            {
                //***直接开上帝视角从对象里取值赋值***
                var errorinstance =
                    ((rtObject)player.outpackage_runtimescope[player.swc.ErrorClass.classid].memberData[3].getValue());

                errorinstance.value.memberData[0].directSet(new rtString(errormessage));
                errorinstance.value.memberData[1].directSet(new rtString("AneError"));
                errorinstance.value.memberData[2].directSet(new rtInt(10001));
                errorinstance.value.memberData[3].directSet(new rtString(player.stackTrace(0)));
                runtimeError = (new error.InternalError(token, errormessage, errorinstance));

            }
            else
            {
                runtimeError = (new error.InternalError(token, errormessage));
            }
        }

        

        private bool isclosed;
        /// <summary>
        /// 退出程序栈时
        /// </summary>
        public void close()
        {
#if DEBUG
            if (isclosed)
            {
                throw new ASRunTimeException();
            }
#endif
            isclosed = true;

            typeconvertoperator = null;
            funCaller = null;
            instanceCreator = null;

            

            //清除执行栈
            for (int i = offset; i < offset + block.totalRegisters + 1+1 + call_parameter_slotCount; i++)
            {
                stack[i].clear();
            }

            
            //_tempSlot1.clear();
            //_tempSlot2.clear();
            //_dictMethods.Clear();


            tryCatchState.Clear();
            block = null;
            scope = null;//scope_thispointer = null;
            static_objects = null;
            offset = 0;
            call_parameter_slotCount = 0;
            stack = null;
            player = null;
            returnSlot = null;
            callbacker = null;
            codeLinePtr = 0;
            holdedError = null;
            hasCallJump = false;
            hasCallReturn = false;
            holdHasCallReturn = false;
            holdhasjumpto = false;
            holdjumptoline = 0;
            jumptoline = 0;
            trystateCount = 0;
            
            typeconvertoperator = null;
#if DEBUG
            execing = false;
#endif   


            ret(this);
            
            
        }

    }
}
