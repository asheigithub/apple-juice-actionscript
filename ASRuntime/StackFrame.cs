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
    class StackFrame
    {
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

        /// <summary>
        /// 临时保存准备调用函数的参数
        /// </summary>
        internal HeapSlot[] tempCallFuncHeap;
        internal ASBinCode.rtti.FunctionDefine _toCallFunc;
        internal int _pushedArgs;
        internal StackSlot _tempSlot;

        private Stack<TryState> tryCatchState = new Stack<TryState>() ;
        /// <summary>
        /// 暂存已发生的错误
        /// </summary>
        private error.InternalError holdedError;
        /// <summary>
        /// 执行阶段发生的错误
        /// </summary>
        private error.InternalError runtimeError;

        /// <summary>
        /// 暂存是否调用了return
        /// </summary>
        private bool holdHasCallReturn;

        /// <summary>
        /// 当前指向的指令行
        /// </summary>
        public int codeLinePtr;

        /// <summary>
        /// 代码段
        /// </summary>
        public ASBinCode.CodeBlock block;

        public Player player;

        public ASBinCode.IRunTimeScope scope;

        /// <summary>
        /// 返回值存储槽
        /// </summary>
        public ASBinCode.ISLOT returnSlot;

        
        public bool IsEnd()
        {
            return codeLinePtr >= block.opSteps.Count;
        }

        /// <summary>
        /// 运行一行
        /// </summary>
        public void step()
        {
            ASBinCode.OpStep step = block.opSteps[codeLinePtr];

            exec(step);

            codeLinePtr++;
        }


        private void exec(ASBinCode.OpStep step)
        {
            switch (step.opCode)
            {
                case OpCode.cast:
                    operators.OpCast.execCast(this, step, scope);
                    break;
                case OpCode.assigning:
                    operators.OpAssigning.execAssigning(player, step, scope);
                    break;
                case OpCode.add_number:
                    operators.OpAdd.execAdd_Number(player, step, scope);
                    break;
                case OpCode.add_string:
                    operators.OpAdd.execAdd_String(player, step, scope);
                    break;
                case OpCode.add:
                    operators.OpAdd.execAdd(this, step, scope);
                    break;
                case OpCode.sub_number:
                    operators.OpSub.execSub_Number(player, step, scope);
                    break;
                case OpCode.sub:
                    operators.OpSub.execSub(this, step, scope);
                    break;
                case OpCode.multi:
                    operators.OpMulti.execMulti(this, step, scope);
                    break;
                case OpCode.div:
                    operators.OpMulti.execDiv(this, step, scope);
                    break;
                case OpCode.mod:
                    operators.OpMulti.execMod(this, step, scope);
                    break;
                case OpCode.neg:
                    operators.OpNeg.execNeg(player, step, scope);
                    break;
                case OpCode.gt_num:
                    operators.OpLogic.execGT_NUM(player, step, scope);
                    break;
                case OpCode.gt_void:
                    operators.OpLogic.execGT_Void(this, step, scope);
                    break;
                case OpCode.lt_num:
                    operators.OpLogic.execLT_NUM(player, step, scope);
                    break;
                case OpCode.lt_void:
                    operators.OpLogic.execLT_VOID(this, step, scope);
                    break;
                case OpCode.ge_num:
                    operators.OpLogic.execGE_NUM(player, step, scope);
                    break;
                case OpCode.ge_void:
                    operators.OpLogic.execGE_Void(this, step, scope);
                    break;
                case OpCode.le_num:
                    operators.OpLogic.execLE_NUM(player, step, scope);
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
                    operators.OpIncrementDecrement.execIncInt(player, step, scope);
                    break;
                case OpCode.increment_uint:
                    operators.OpIncrementDecrement.execIncUInt(player, step, scope);
                    break;
                case OpCode.increment_number:
                    operators.OpIncrementDecrement.execIncNumber(player, step, scope);
                    break;
                case OpCode.decrement:
                    operators.OpIncrementDecrement.execDecrement(this, step, scope);
                    break;

                case OpCode.decrement_int:
                    operators.OpIncrementDecrement.execDecInt(player, step, scope);
                    break;
                case OpCode.decrement_uint:
                    operators.OpIncrementDecrement.execDecUInt(player, step, scope);
                    break;
                case OpCode.decrement_number:
                    operators.OpIncrementDecrement.execDecNumber(player, step, scope);
                    break;

                case OpCode.suffix_inc:
                    operators.OpIncrementDecrement.execSuffixInc(this, step, scope);
                    break;
                case OpCode.suffix_inc_int:
                    operators.OpIncrementDecrement.execSuffixIncInt(player, step, scope);
                    break;
                case OpCode.suffix_inc_uint:
                    operators.OpIncrementDecrement.execSuffixIncUint(player, step, scope);
                    break;
                case OpCode.suffix_inc_number:
                    operators.OpIncrementDecrement.execSuffixIncNumber(player, step, scope);
                    break;
                case OpCode.suffix_dec:
                    operators.OpIncrementDecrement.execSuffixDec(this, step, scope);
                    break;
                case OpCode.suffix_dec_int:
                    operators.OpIncrementDecrement.execSuffixDecInt(player, step, scope);
                    break;
                case OpCode.suffix_dec_uint:
                    operators.OpIncrementDecrement.execSuffixDecUInt(player, step, scope);
                    break;
                case OpCode.suffix_dec_number:
                    operators.OpIncrementDecrement.execSuffixDecNumber(player, step, scope);
                    break;
                case OpCode.flag:
                    //标签行，不做任何操作
                    break;
                case OpCode.if_jmp:
                    {
                        if (ReferenceEquals(ASBinCode.rtData.rtBoolean.True, step.arg1.getValue(scope)))
                        {
                            codeLinePtr += step.jumoffset - 1;
                            break;
                        }
                    }
                    break;
                case OpCode.jmp:
                    codeLinePtr += step.jumoffset - 1;
                    break;
                case OpCode.raise_error:
                    nativefuncs.Throw.exec(this, step, scope);
                    break;
                case OpCode.enter_try:
                    {
                        int tryid = ((rtInt)step.arg1.getValue(scope)).value;
                        enter_try( tryid);
                    }
                    break;
                case OpCode.quit_try:
                    {
                        int tryid = ((rtInt)step.arg1.getValue(scope)).value;
                        quit_try( tryid, step.token);
                    }
                    break;
                case OpCode.enter_catch:
                    {
                        int catchid = ((rtInt)step.arg1.getValue(scope)).value;
                        enter_catch( catchid);
                    }
                    break;
                case OpCode.quit_catch:
                    {
                        int catchid = ((rtInt)step.arg1.getValue(scope)).value;
                        quit_catch( catchid, step.token);
                    }
                    break;
                case OpCode.enter_finally:
                    {
                        int finallyid = ((rtInt)step.arg1.getValue(scope)).value;
                        enter_finally( finallyid);
                    }
                    break;
                case OpCode.quit_finally:
                    {
                        int finallyid = ((rtInt)step.arg1.getValue(scope)).value;
                        quit_finally( finallyid, step.token);
                    }
                    break;
                case OpCode.native_trace:
                    nativefuncs.Trace.exec(this, step, scope);
                    break;
                case OpCode.bind_scope:
                    operators.OpCallFunction.bind(player,this,step);
                    break;
                case OpCode.make_para_scope:
                    operators.OpCallFunction.create_paraScope(player, this, step);
                    break;
                case OpCode.push_parameter:
                    operators.OpCallFunction.push_parameter(player, this, step);
                    break;
                case OpCode.call_function:
                    operators.OpCallFunction.exec(player, this, step);
                    break;
                case OpCode.function_return:
                    operators.OpCallFunction.exec_return(player, this, step);
                    hasCallReturn = true;
                    break;
                default:

                    runtimeError = (new error.InternalError(step.token,
                         step.opCode + "操作未实现"
                         ));
                    break;
            }

            doTryCatchReturn(step);
        }

        private bool hasCallReturn;

        private void doTryCatchReturn(OpStep step)
        {

            //检查该步骤是否发生错误
            {
                if (runtimeError != null)
                {

                    if (runtimeError.catchable
                            &&
                        tryCatchState.Count > 0
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

                        IRunTimeValue errorValue = err.errorValue;
                        //***查找匹配catch找到后给捕获异常变量赋值.**
                        for (int j = codeLinePtr + 1; j < block.opSteps.Count; j++)
                        {
                            var op = block.opSteps[j];
                            if (op.opCode == OpCode.catch_error
                                )
                            {
                                if (nativefuncs.Catch.isCatchError(tryid, errorValue, op, scope))
                                {
                                    ((Variable)op.reg).getISlot(scope).directSet(errorValue);
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
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null)).value;
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
                        tryCatchState.Count > 0
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
                                    ((ASBinCode.rtData.RightValue)op.arg1).getValue(null)).value;
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
                        //var err = runtimeError;
                        //player.runtimeError = err;
                        player.exitStackFrameWithError(runtimeError);
                    }
                }
                else if (hasCallReturn) //已经调用了Return
                {                       //将进入finally块
                    if (
                        tryCatchState.Count > 0
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
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null)).value;
                                    if (id == tryid)
                                    {
                                        codeLinePtr = j - 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (tryCatchState.Count > 0
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
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null)).value;
                                    if (id == tryid)
                                    {
                                        codeLinePtr = j - 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (tryCatchState.Count > 0
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
                                        ((ASBinCode.rtData.RightValue)op.arg1).getValue(null)).value;
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
                        //bool existsTry = false;
                        ////查找是还有try块未脱掉
                        //foreach (var item in tryCatchState)
                        //{
                        //    if (item.state == Try_catch_finally.Try)
                        //    {
                        //        existsTry = true;
                        //        break;
                        //    }
                        //}
                        //if (!existsTry)
                        {
                            codeLinePtr = block.opSteps.Count;
                        }
                        //else
                        //{

                        //}
                    }
                    
                }
                
            }
        }

        /// <summary>
        /// 接收从调用栈底
        /// </summary>
        /// <param name="error"></param>
        internal void receiveErrorFromStackFrame(error.InternalError error)
        {
            runtimeError = error;
            doTryCatchReturn(block.opSteps[codeLinePtr - 1]);
            codeLinePtr++;
        }

        private void enter_try( int tryid)
        {
            tryCatchState.Push(new TryState(Try_catch_finally.Try, tryid));
        }

        private int quit_try( int tryid, SourceToken token)
        {
            var s = tryCatchState.Pop();

            if (s.state != Try_catch_finally.Try || s.tryid != tryid)
            {
                //player.runtimeError = (new error.InternalError(token,
                //                                                    "运行时异常 try块不匹配"
                //                                                    ));
                //引擎异常，抛出
                throw new InvalidOperationException();
            }
            return s.tryid;
        }

        private void enter_catch( int catchid)
        {
            tryCatchState.Push(new TryState(Try_catch_finally.Catch, catchid));
        }
        private int quit_catch( int catchid, SourceToken token)
        {
            var s = tryCatchState.Pop();

            if (s.state != Try_catch_finally.Catch || s.tryid != catchid)
            {
                //player.runtimeError = (new error.InternalError(token,
                //                                                    "运行时异常 catch块不匹配"
                //                                                    ));
                //引擎异常，抛出
                throw new InvalidOperationException();
            }
            return s.tryid;
        }

        private void enter_finally( int finallyid)
        {
            tryCatchState.Push(new TryState(Try_catch_finally.Finally, finallyid));
        }
        private int quit_finally( int finallyid, SourceToken token)
        {
            var s = tryCatchState.Pop();

            if (s.state != Try_catch_finally.Finally || s.tryid != finallyid)
            {
                //player.runtimeError = (new error.InternalError(token,
                //                                                    "运行时异常 finally块不匹配"
                //                                                    ));
                //引擎异常，抛出
                throw new InvalidOperationException();
            }


            // finally块执行完成后，再次抛出异常
            if (holdedError != null)
            {
                runtimeError = holdedError;
                holdedError = null;
            }

            hasCallReturn = holdHasCallReturn;
            if (hasCallReturn)
            {
                doTryCatchReturn(block.opSteps[codeLinePtr]);
            }

            return s.tryid;
        }


        internal void throwCastException(ASBinCode.SourceToken token, RunTimeDataType srctype, RunTimeDataType dsttype)
        {
            runtimeError = (new error.InternalError(token, "类型转换失败:" + srctype + "->" + dsttype));
        }

        internal void throwOpException(ASBinCode.SourceToken token, OpCode opcode)
        {
            runtimeError = (new error.InternalError(token, "无法执行操作" + opcode));
        }

        internal void throwError(error.InternalError err)
        {
            runtimeError = err;
        }

        /// <summary>
        /// 退出程序栈时
        /// </summary>
        public void close()
        {
            //清除执行栈
            for (int i = scope.offset; i < scope.offset + block.totalRegisters; i++)
            {
                scope.stack[i].clear();
            }

        }

    }
}
