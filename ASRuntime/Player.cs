using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASRuntime
{
    public class Player
    {
        public bool isConsoleOut=true;

        private ASBinCode.CodeBlock block;
        public void loadCode(ASBinCode.CodeBlock block)
        {
            this.block = block;
        }

        public error.InternalError runtimeError;


        public IRunTimeScope run()
        {
            runtimeError = null;

            StackSlot[] eaxs = new StackSlot[block.totalRegisters];
            runtimeScope scope = new runtimeScope( block.scope.members,eaxs,0 );// block.scope.members.Count);
            for (int i = 0; i < eaxs.Length; i++)
            {
                eaxs[i] = new StackSlot();
            }
            try
            {
                for (int i = 0; i < block.opSteps.Count; i++)
                {
                    OpStep step = block.opSteps[i];

                    switch (step.opCode)
                    {
                        case OpCode.cast:
                            operators.OpCast.execCast(this, step, scope);
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
                            operators.OpSub.execSub_Number(this,step,scope);
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
                            operators.OpLogic.execNOT (this, step, scope);
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
                            break;
                        case OpCode.if_jmp:
                            {
                                if( ReferenceEquals(  ASBinCode.rtData.rtBoolean.True, step.arg1.getValue(scope)))
                                {
                                    i += step.jumoffset - 1;
                                    break;
                                }
                            }
                            break;
                        case OpCode.jmp:
                            i += step.jumoffset - 1;
                            break;
                        case OpCode.native_trace:
                            nativefuncs.Trace.exec(this, step, scope);
                            break;
                        case OpCode.raise_error:
                            nativefuncs.Throw.exec(this, step, scope);
                            break;
                        case OpCode.enter_try:
                            {
                                int tryid = ((rtInt)step.arg1.getValue(scope)).value;
                                enter_try(scope, tryid);
                            }
                            break;
                        case OpCode.quit_try:
                            {
                                int tryid = ((rtInt)step.arg1.getValue(scope)).value;
                                quit_try(scope, tryid,step.token);
                            }
                            break;
                        case OpCode.enter_catch:
                            {
                                int catchid = ((rtInt)step.arg1.getValue(scope)).value;
                                enter_catch(scope, catchid);
                            }
                            break;
                        case OpCode.quit_catch:
                            {
                                int catchid = ((rtInt)step.arg1.getValue(scope)).value;
                                quit_catch(scope, catchid, step.token);
                            }
                            break;
                        case OpCode.enter_finally:
                            {
                                int finallyid = ((rtInt)step.arg1.getValue(scope)).value;
                                enter_finally(scope, finallyid);
                            }
                            break;
                        case OpCode.quit_finally:
                            {
                                int finallyid = ((rtInt)step.arg1.getValue(scope)).value;
                                quit_finally(scope, finallyid, step.token);
                            }
                            break;
                        default:

                           runtimeError=(new error.InternalError  (step.token,
                                step.opCode + "操作未实现"
                                ));
                           break;
                    }

                    //检查该步骤是否发生错误
                    {
                        if (runtimeError!=null)
                        {

                            if (runtimeError.catchable
                                    &&
                                scope.tryCatchState.Count > 0
                                    &&
                                scope.tryCatchState.Peek().state == runtimeScope.Try_catch_finally.Try
                                )
                            {
                                //先脱掉try;
                                int tryid = quit_try(scope, scope.tryCatchState.Peek().tryid, step.token);
                                var err = runtimeError;
                                //***清除运行时错误***
                                runtimeError = null;

                                bool foundcatch = false;

                                IRunTimeValue errorValue = err.errorValue;
                                //***查找匹配catch找到后给捕获异常变量赋值.**
                                for (int j = i + 1; j < block.opSteps.Count; j++)
                                {
                                    var op = block.opSteps[j];
                                    if (op.opCode == OpCode.catch_error
                                        )
                                    {
                                        if (nativefuncs.Catch.isCatchError(tryid, errorValue, op, scope))
                                        {
                                            ((Variable)op.reg).getISlot(scope).directSet(errorValue);
                                            //引导到catch块
                                            i = j;
                                            foundcatch = true;
                                            break;
                                        }
                                    }
                                }
                                //catch块不存在,前往finally块
                                if (!foundcatch)
                                {
                                    scope.holdedError = err;
                                    for (int j = i + 1; j < block.opSteps.Count; j++)
                                    {
                                        var op = block.opSteps[j];
                                        if (op.opCode == OpCode.enter_finally)
                                        {
                                            int id = ((ASBinCode.rtData.rtInt)
                                                ((ASBinCode.rtData.RightValue)op.arg1).getValue(null)).value;
                                            if (id == tryid)
                                            {
                                                i = j - 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (runtimeError.catchable
                                &&
                                scope.tryCatchState.Count > 0
                                    &&
                                scope.tryCatchState.Peek().state == runtimeScope.Try_catch_finally.Catch
                                )
                            {
                                //catch块中抛错,****移动到finally块***
                                int tryid = quit_catch(scope, scope.tryCatchState.Peek().tryid, step.token);
                                var err = runtimeError;
                                //***清除运行时错误***
                                runtimeError = null;

                                scope.holdedError = err;
                                for (int j = i + 1; j < block.opSteps.Count; j++)
                                {
                                    var op = block.opSteps[j];
                                    if (op.opCode == OpCode.enter_finally)
                                    {
                                        int id = ((ASBinCode.rtData.rtInt)
                                            ((ASBinCode.rtData.RightValue)op.arg1).getValue(null)).value;
                                        if (id == tryid)
                                        {
                                            i = j - 1;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var err = runtimeError;
                                outPutErrorMessage(err);

                                return scope;
                            }
                        }
                    }
                }

            }
            finally
            {
                if (isConsoleOut)
                {
                    Console.WriteLine();
                    Console.WriteLine("====程序状态====");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Variables:");
                    for (int i = 0; i < block.scope.members.Count; i++)
                    {
                        Console.WriteLine("\t" + block.scope.members[i].name + "\t|\t" + scope.memberData[i].getValue());
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Registers:");
                    for (int i = 0; i < eaxs.Length; i++)
                    {
                        Console.WriteLine("\t" + "EAX(" + i + ")\t|\t" + eaxs[i].getValue());
                    }
                    Console.ResetColor();
                }
            }
            return scope;
        }

        private void enter_try(runtimeScope scope,int tryid)
        {
            scope.tryCatchState.Push(new runtimeScope.TryState(runtimeScope.Try_catch_finally.Try, tryid));
        }

        private int quit_try(runtimeScope scope, int tryid,SourceToken token)
        {
            var s = scope.tryCatchState.Pop();

            if ( s.state != runtimeScope.Try_catch_finally.Try || s.tryid != tryid)
            {
                runtimeError=(new error.InternalError(token,
                                                                    "运行时异常 try块不匹配"
                                                                    ));
                //引擎异常，抛出
                throw new InvalidOperationException();
            }
            return s.tryid;
        }

        private void enter_catch(runtimeScope scope, int catchid)
        {
            scope.tryCatchState.Push(new runtimeScope.TryState(runtimeScope.Try_catch_finally.Catch, catchid));
        }
        private int quit_catch(runtimeScope scope, int catchid, SourceToken token)
        {
            var s = scope.tryCatchState.Pop();

            if (s.state != runtimeScope.Try_catch_finally.Catch || s.tryid != catchid)
            {
                runtimeError = (new error.InternalError(token,
                                                                    "运行时异常 catch块不匹配"
                                                                    ));
                //引擎异常，抛出
                throw new InvalidOperationException();
            }
            return s.tryid;
        }

        private void enter_finally(runtimeScope scope, int finallyid)
        {
            scope.tryCatchState.Push(new runtimeScope.TryState(runtimeScope.Try_catch_finally.Finally, finallyid));
        }
        private int quit_finally(runtimeScope scope, int finallyid, SourceToken token)
        {
            var s = scope.tryCatchState.Pop();

            if (s.state != runtimeScope.Try_catch_finally.Finally || s.tryid != finallyid)
            {
                runtimeError = (new error.InternalError(token,
                                                                    "运行时异常 finally块不匹配"
                                                                    ));
                //引擎异常，抛出
                throw new InvalidOperationException();
            }


            // finally块执行完成后，再次抛出异常
            if (scope.holdedError != null)
            {
                runtimeError = scope.holdedError;
                scope.holdedError = null;
            }

            return s.tryid;
        }


        internal void throwCastException(ASBinCode.SourceToken token,RunTimeDataType srctype,RunTimeDataType dsttype)
        {
            runtimeError=( new error.InternalError (token, "类型转换失败:"+srctype+"->"+dsttype));
        }

        internal void throwOpException(ASBinCode.SourceToken token, OpCode opcode)
        {
            runtimeError=(new error.InternalError(token, "无法执行操作" + opcode ));
        }
        
        private void outPutErrorMessage(error.InternalError err)
        {
            if (isConsoleOut)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("运行时错误");
                Console.WriteLine("file :" + err.token.sourceFile);
                Console.WriteLine("line :" + err.token.line + " ptr :" + err.token.ptr);

                if (err.errorValue != null)
                {
                    Console.WriteLine("[故障] " + "信息=" + err.errorValue);
                }
                else
                {
                    Console.WriteLine(err.message);
                }

                Console.ResetColor();
            }
        }





        
        class runtimeScope : IRunTimeScope
        {
            internal enum Try_catch_finally
            {
                Try,
                Catch,
                Finally
            }

            internal struct TryState
            {
                public TryState(Try_catch_finally state,int id)
                {
                    this.state = state;
                    tryid = id;
                }

                public Try_catch_finally state;
                public int tryid;
            }

            public Stack<TryState> tryCatchState;
            /// <summary>
            /// 暂存已发生的错误
            /// </summary>
            internal error.InternalError holdedError;

            HeapSlot[] memberDataList;

            private IList<ISLOT> runtimestack;
            private int _offset;

            public runtimeScope(IList<IMember> members,IList<ISLOT> rtStack,int offset )
            {
                runtimestack = rtStack;
                this._offset = offset;
                memberDataList = new HeapSlot[members.Count];
                for (int i = 0; i < memberDataList.Length; i++)
                {
                    memberDataList[i] = new HeapSlot();
                    memberDataList[i].setDefaultType(
                        ((Variable)members[i]).valueType
                        );
                }

                tryCatchState = new Stack<TryState>();

            }

            

            public ISLOT[] memberData
            {
                get
                {
                    return memberDataList;
                }
            }

            public int offset
            {
                get
                {
                    return _offset;
                }
            }

            public IRunTimeScope parent
            {
                get
                {
                    return null;
                }
            }

            public IList<ISLOT> stack
            {
                get
                {
                    return runtimestack;
                }
            }
        }
    }
}
