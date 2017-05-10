using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASRuntime
{
    public class Player
    {
        public bool isConsoleOut = true;


        internal Dictionary<int, rtObject> static_instance;
        internal Dictionary<int, RunTimeScope> outpackage_runtimescope;


        internal CSWC swc;
        private CodeBlock defaultblock;
        public void loadCode(CSWC swc,CodeBlock block=null)
        {
            this.swc = swc;

            static_instance = new Dictionary<int, rtObject>();
            outpackage_runtimescope = new Dictionary<int, RunTimeScope>();

            if (block != null)
            {
                defaultblock = block;
            }
            else if (swc.blocks.Count == 1)
            {
                defaultblock = swc.blocks[0];
            }
            else
            {
                //for (int i = 0; i < swc.blocks.Count; i++)
                //{
                //    if (swc.blocks[i] != null)
                //    {
                //        defaultblock = swc.blocks[i];
                //        break;
                //    }
                //}
                //查找文档类
                for (int i = 0; i < swc.classes.Count; i++)
                {
                    if (swc.classes[i].isdocumentclass)
                    {
                        defaultblock = new CodeBlock(int.MaxValue, "_player_run",-65535,true);
                        defaultblock.scope = new ASBinCode.scopes.StartUpBlockScope();
                        defaultblock.totalRegisters = 1;

                        {
                            OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_argement, new SourceToken(0, 0, ""));
                            opMakeArgs.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(swc.classes[i].classid));
                            opMakeArgs.arg1Type = RunTimeDataType.rt_int;
                            defaultblock.opSteps.Add(opMakeArgs);

                        }
                        {
                            OpStep step = new OpStep(OpCode.new_instance, new SourceToken(0, 0, ""));
                            step.arg1 = new RightValue(new rtInt(swc.classes[i].classid));
                            step.arg1Type = swc.classes[i].getRtType();
                            step.reg = new Register(0);
                            step.regType = swc.classes[i].getRtType();

                            defaultblock.opSteps.Add(step);
                        }
                        break;
                    }
                }
                if (defaultblock == null)
                {
                    //***查找第一个类的包外代码
                    for (int i = swc.classes.Count-1; i >0;i--)
                    {
                        if (swc.classes[i].staticClass != null)
                        {
                            defaultblock = swc.blocks[swc.classes[i].outscopeblockid];
                            break;
                        }
                    }
                }

                if (defaultblock == null && swc.blocks.Count >0)
                {
                    defaultblock = swc.blocks[0];
                }
                
            }
            runtimeStack = new Stack<StackFrame>();
            stackSlots = new StackSlot[1024];
            
        }

        private error.InternalError runtimeError;

       
        /// <summary>
        /// 调用堆栈
        /// </summary>
        private Stack<StackFrame> runtimeStack;
        StackSlot[] stackSlots;
        private StackFrame displayStackFrame;
        
        public RunTimeValueBase run2(RightValueBase result)
        {

            if (defaultblock == null || swc == null || swc.blocks.Count == 0)
            {
                if (isConsoleOut)
                {
                    Console.WriteLine();
                    Console.WriteLine("====没有找到可执行的代码====");
                    Console.WriteLine("用[Doc]标记文档类");
                    Console.WriteLine("或者第一个类文件的包外代码作为入口");
                }
                return null;
            }

            for (int i = 0; i < stackSlots.Length; i++)
            {
                stackSlots[i] = new StackSlot(swc);
            }

            if(swc.ErrorClass !=null)
            {
                //***先执行必要代码初始化****
                var block = swc.blocks[swc.ErrorClass.outscopeblockid];
                HeapSlot[] initdata = genHeapFromCodeBlock(block);
                CallBlock(block, initdata, new StackSlot(swc), null,
                    new SourceToken(0, 0, ""), null,
                    null, RunTimeScopeType.startup
                    );
                while (step())
                {

                }
            }


            HeapSlot[] data = genHeapFromCodeBlock(defaultblock);
            var topscope = CallBlock(defaultblock,data ,new StackSlot(swc), null, 
                new SourceToken(0, 0, ""),null,
                null, RunTimeScopeType.startup
                );
            displayStackFrame = runtimeStack.Peek();

            while (step())
            {

            }
            //人肉内联代码
            //            while (true)
            //            {
            //                if (runtimeError != null)
            //                {
            //                    break;
            //                }
            //                if (currentRunFrame == null)
            //                {
            //                    break;
            //                }

            //                if (receive_error != null)
            //                {
            //                    var temp = receive_error;
            //                    receive_error = null;
            //                    currentRunFrame.receiveErrorFromStackFrame(temp);
            //                    continue;
            //                }

            //                if (currentRunFrame.IsEnd()) //执行完成
            //                {
            //                    runtimeStack.Pop(); //出栈


            //                    var toclose = currentRunFrame;
            //                    if (currentRunFrame.callbacker != null)
            //                    {
            //                        IBlockCallBack temp = currentRunFrame.callbacker;
            //                        currentRunFrame.callbacker = null;
            //                        temp.call(temp.args);

            //                    }

            //                    toclose.close();

            //                    if (runtimeStack.Count > 0)
            //                    {
            //                        currentRunFrame = runtimeStack.Peek();
            //                    }
            //                    else
            //                    {
            //                        currentRunFrame = null;
            //                    }

            //                }
            //                else
            //                {
            //                    currentRunFrame.step();
            //                    #region 内联
            ////                    {
            ////                        OpStep step = currentRunFrame.block.opSteps[currentRunFrame.codeLinePtr];
            ////                        //exec(step);
            ////#if DEBUG
            ////                    if (currentRunFrame.execing)
            ////                    {
            ////                        throw new InvalidOperationException();
            ////                    }

            ////                    currentRunFrame.execing = true;
            ////#endif
            ////                        var scope = currentRunFrame.scope;
            ////                        var frame = currentRunFrame;
            ////                        switch (step.opCode)
            ////                        {
            ////                            case OpCode.cast:
            ////                                operators.OpCast.execCast(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.cast_primitive:
            ////                                operators.OpCast.exec_CastPrimitive(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.assigning:
            ////                                //operators.OpAssigning.execAssigning(currentRunFrame, step, currentRunFrame.scope);
            ////                                {
            ////                                    ASBinCode.RunTimeValueBase v = step.arg1.getValue(scope);
            ////                                    ASBinCode.SLOT slot = step.reg.getSlot(scope);

            ////                                    //if (!slot.isPropGetterSetter)
            ////                                    {
            ////                                        if (!slot.directSet(v))
            ////                                        {
            ////                                            StackSlot oslot = (StackSlot)slot;
            ////                                            //if (oslot.linktarget != null)
            ////                                            {
            ////                                                slot = oslot.linktarget;
            ////                                            }

            ////                                            if (slot is ClassPropertyGetter.PropertySlot)
            ////                                            {
            ////                                                ClassPropertyGetter.PropertySlot propslot =
            ////                                                (ASBinCode.ClassPropertyGetter.PropertySlot)slot;
            ////                                                //***调用访问器。***
            ////                                                ASBinCode.ClassPropertyGetter prop = oslot.propGetSet; //propslot.property;

            ////                                                operators.OpAssigning._doPropAssigning(prop, frame, step, frame.player, scope,
            ////                                                    //propslot.bindObj
            ////                                                    oslot.propBindObj
            ////                                                    , v,
            ////                                                    oslot
            ////                                                    );
            ////                                                break;
            ////                                            }

            ////                                            if (slot is operators.OpVector.vectorSLot)    //Vector类型不匹配
            ////                                            {
            ////                                                BlockCallBackBase cb = new BlockCallBackBase();
            ////                                                cb.scope = scope;
            ////                                                cb.step = step;
            ////                                                cb.args = frame;
            ////                                                cb.setCallBacker(operators.OpAssigning._vectorConvertCallBacker);

            ////                                                //***调用强制类型转换***
            ////                                                operators.OpCast.CastValue(v, ((operators.OpVector.vectorSLot)slot).vector_data.vector_type,
            ////                                                    frame, step.token, scope, frame._tempSlot1, cb, false);

            ////                                                break;
            ////                                            }



            ////                                            string ext = String.Empty;
            ////                                            if (slot is MethodGetterBase.MethodSlot)
            ////                                            {
            ////                                                ext = "Cannot assign to a method ";// + ((ASBinCode.ClassMethodGetter.MethodSlot)slot).method;
            ////                                            }
            ////                                            else if (slot is ObjectMemberSlot)
            ////                                            {
            ////                                                ext = "Illegal write to read-only property ";
            ////                                                //+ ((ObjectMemberSlot)slot).obj.value._class.name
            ////                                                //+" on ppp.PPC."
            ////                                            }
            ////                                            else if (slot is ClassPropertyGetter.PropertySlot)
            ////                                            {
            ////                                                ext = "Illegal write to read-only property ";
            ////                                            }
            ////                                            else if (slot is operators.OpAccess_Dot.prototypeSlot)
            ////                                            {
            ////                                                ext = "Cannot create property "
            ////                                                    + ((operators.OpAccess_Dot.prototypeSlot)slot)._protoname +
            ////                                                    " on " + ((operators.OpAccess_Dot.prototypeSlot)slot)._protoRootObj.value._class.name;
            ////                                            }

            ////                                            frame.throwError(
            ////                                                step.token, 0, ext
            ////                                                );
            ////                                        }

            ////                                        frame.endStep(step);
            ////                                    }
            ////                                }

            ////                                break;

            ////                            case OpCode.add_number:
            ////                                //operators.OpAdd.execAdd_Number(currentRunFrame, step, currentRunFrame.scope);
            ////                                {
            ////                                    double a1 = step.arg1.getValue(scope).toNumber();
            ////                                    double a2 = step.arg2.getValue(scope).toNumber();

            ////                                    step.reg.getSlot(scope).setValue(a1 + a2);//new ASBinCode.rtData.rtNumber(a1.value +a2.value ));
            ////                                    frame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.add_string:
            ////                                operators.OpAdd.execAdd_String(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.add:
            ////                                operators.OpAdd.execAdd(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.sub_number:
            ////                                //operators.OpSub.execSub_Number(currentRunFrame, step, currentRunFrame.scope);
            ////                                {
            ////                                    double a1 = step.arg1.getValue(scope).toNumber();
            ////                                    double a2 = step.arg2.getValue(scope).toNumber();

            ////                                    step.reg.getSlot(scope).setValue(a1 - a2);//new ASBinCode.rtData.rtNumber(a1.value - a2.value));
            ////                                    frame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.sub:
            ////                                operators.OpSub.execSub(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.multi:
            ////                                operators.OpMulti.execMulti(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.multi_number:
            ////                                operators.OpMulti.exec_MultiNumber(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.div:
            ////                                operators.OpMulti.execDiv(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.div_number:
            ////                                operators.OpMulti.exec_DivNumber(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.mod:
            ////                                operators.OpMulti.execMod(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.mod_number:
            ////                                operators.OpMulti.exec_ModNumber(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.neg:
            ////                                operators.OpNeg.execNeg(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.gt_num:
            ////                                operators.OpLogic.execGT_NUM(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.gt_void:
            ////                                operators.OpLogic.execGT_Void(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.lt_num:
            ////                                //operators.OpLogic.execLT_NUM(currentRunFrame, step, currentRunFrame.scope);
            ////                                {
            ////                                    double a1 = (step.arg1.getValue(currentRunFrame.scope)).toNumber();
            ////                                    double a2 = (step.arg2.getValue(currentRunFrame.scope)).toNumber();

            ////                                    if (a1 < a2)
            ////                                    {
            ////                                        step.reg.getSlot(currentRunFrame.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            ////                                    }
            ////                                    else
            ////                                    {
            ////                                        step.reg.getSlot(currentRunFrame.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            ////                                    }


            ////                                    currentRunFrame.endStep(step);
            ////                                }
            ////                                break;
            ////                            //case OpCode.lt_int_int:
            ////                            //    operators.OpLogic.execLT_IntInt(this, step, scope);
            ////                            //    break;
            ////                            case OpCode.lt_void:
            ////                                operators.OpLogic.execLT_VOID(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.ge_num:
            ////                                operators.OpLogic.execGE_NUM(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.ge_void:
            ////                                operators.OpLogic.execGE_Void(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.le_num:
            ////                                operators.OpLogic.execLE_NUM(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.le_void:
            ////                                operators.OpLogic.execLE_VOID(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.equality:
            ////                                operators.OpLogic.execEQ(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.not_equality:
            ////                                operators.OpLogic.execNotEQ(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.equality_num_num:
            ////                                operators.OpLogic.execEQ_NumNum(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.not_equality_num_num:
            ////                                operators.OpLogic.execNotEQ_NumNum(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.equality_str_str:
            ////                                operators.OpLogic.execEQ_StrStr(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.not_equality_str_str:
            ////                                operators.OpLogic.execNotEQ_StrStr(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.strict_equality:
            ////                                operators.OpLogic.execStrictEQ(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.not_strict_equality:
            ////                                operators.OpLogic.execStrictNotEQ(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.logic_not:
            ////                                operators.OpLogic.execNOT(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bitAnd:
            ////                                operators.OpBit.execBitAnd(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bitOr:
            ////                                operators.OpBit.execBitOR(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bitXOR:
            ////                                operators.OpBit.execBitXOR(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bitNot:
            ////                                operators.OpBit.execBitNot(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bitLeftShift:
            ////                                operators.OpBit.execBitLeftShift(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bitRightShift:
            ////                                operators.OpBit.execBitRightShift(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bitUnsignedRightShift:
            ////                                operators.OpBit.execBitUnSignRightShift(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.increment:
            ////                                operators.OpIncrementDecrement.execIncrement(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.increment_int:
            ////                                operators.OpIncrementDecrement.execIncInt(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.increment_uint:
            ////                                operators.OpIncrementDecrement.execIncUInt(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.increment_number:
            ////                                operators.OpIncrementDecrement.execIncNumber(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.decrement:
            ////                                operators.OpIncrementDecrement.execDecrement(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;

            ////                            case OpCode.decrement_int:
            ////                                operators.OpIncrementDecrement.execDecInt(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.decrement_uint:
            ////                                operators.OpIncrementDecrement.execDecUInt(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.decrement_number:
            ////                                operators.OpIncrementDecrement.execDecNumber(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;

            ////                            case OpCode.suffix_inc:
            ////                                operators.OpIncrementDecrement.execSuffixInc(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.suffix_inc_int:
            ////                                //operators.OpIncrementDecrement.execSuffixIncInt(currentRunFrame, step, currentRunFrame.scope);
            ////                                {
            ////                                    var v = step.arg1.getValue(scope);

            ////                                    ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

            ////                                    step.reg.getSlot(scope).setValue(iv.value);

            ////                                    iv.value++;


            ////                                    frame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.suffix_inc_uint:
            ////                                operators.OpIncrementDecrement.execSuffixIncUint(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.suffix_inc_number:
            ////                                operators.OpIncrementDecrement.execSuffixIncNumber(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.suffix_dec:
            ////                                operators.OpIncrementDecrement.execSuffixDec(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.suffix_dec_int:
            ////                                operators.OpIncrementDecrement.execSuffixDecInt(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.suffix_dec_uint:
            ////                                operators.OpIncrementDecrement.execSuffixDecUInt(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.suffix_dec_number:
            ////                                operators.OpIncrementDecrement.execSuffixDecNumber(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.flag:
            ////                                //标签行，不做任何操作
            ////                                currentRunFrame.endStep(step);
            ////                                break;
            ////                            case OpCode.if_jmp:
            ////                                {
            ////                                    if (((rtBoolean)step.arg1.getValue(currentRunFrame.scope)).value)//ReferenceEquals(ASBinCode.rtData.rtBoolean.True, step.arg1.getValue(scope)))
            ////                                    {
            ////                                        if (currentRunFrame.trystateCount != 0)
            ////                                        {
            ////                                            currentRunFrame.hasCallJump = true;
            ////                                            currentRunFrame.jumptoline = currentRunFrame.codeLinePtr + step.jumoffset - 1;
            ////                                            currentRunFrame.endStep(step);
            ////                                            break;
            ////                                        }
            ////                                        else
            ////                                        {
            ////                                            currentRunFrame.codeLinePtr += step.jumoffset - 1;
            ////                                            currentRunFrame.endStep(step);
            ////                                            break;
            ////                                        }
            ////                                    }
            ////                                    else
            ////                                    {
            ////                                        currentRunFrame.endStep(step);
            ////                                    }
            ////                                }
            ////                                break;
            ////                            case OpCode.jmp:
            ////                                if (currentRunFrame.trystateCount != 0)
            ////                                {
            ////                                    currentRunFrame.hasCallJump = true;
            ////                                    currentRunFrame.jumptoline = currentRunFrame.codeLinePtr + step.jumoffset - 1;
            ////                                    currentRunFrame.endStep(step);
            ////                                    break;
            ////                                }
            ////                                else
            ////                                {
            ////                                    currentRunFrame.codeLinePtr += step.jumoffset - 1;
            ////                                    currentRunFrame.endStep(step);
            ////                                    break;
            ////                                }
            ////                            case OpCode.raise_error:
            ////                                nativefuncs.Throw.exec(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.enter_try:
            ////                                {
            ////                                    int tryid = ((rtInt)step.arg1.getValue(currentRunFrame.scope)).value;
            ////                                    currentRunFrame.enter_try(tryid);

            ////                                    currentRunFrame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.quit_try:
            ////                                {
            ////                                    int tryid = ((rtInt)step.arg1.getValue(currentRunFrame.scope)).value;
            ////                                    currentRunFrame.quit_try(tryid, step.token);

            ////                                    currentRunFrame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.enter_catch:
            ////                                {
            ////                                    int catchid = ((rtInt)step.arg1.getValue(currentRunFrame.scope)).value;
            ////                                    currentRunFrame.enter_catch(catchid);

            ////                                    currentRunFrame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.quit_catch:
            ////                                {
            ////                                    int catchid = ((rtInt)step.arg1.getValue(currentRunFrame.scope)).value;
            ////                                    currentRunFrame.quit_catch(catchid, step.token);

            ////                                    currentRunFrame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.enter_finally:
            ////                                {
            ////                                    int finallyid = ((rtInt)step.arg1.getValue(currentRunFrame.scope)).value;
            ////                                    currentRunFrame.enter_finally(finallyid);

            ////                                    currentRunFrame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.quit_finally:
            ////                                {
            ////                                    int finallyid = ((rtInt)step.arg1.getValue(currentRunFrame.scope)).value;
            ////                                    currentRunFrame.quit_finally(finallyid, step.token);

            ////                                    currentRunFrame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.native_trace:
            ////                                nativefuncs.Trace.exec(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bind_scope:
            ////                                operators.OpCallFunction.bind(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.clear_thispointer:
            ////                                operators.OpCallFunction.clear_thispointer(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.make_para_scope:
            ////                                operators.OpCallFunction.create_paraScope(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.push_parameter:
            ////                                operators.OpCallFunction.push_parameter(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.call_function:
            ////                                operators.OpCallFunction.exec(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.function_return:
            ////                                currentRunFrame.hasCallReturn = true;
            ////                                operators.OpCallFunction.exec_return(currentRunFrame, step, currentRunFrame.scope);

            ////                                break;

            ////                            case OpCode.new_instance:
            ////                                operators.OpCreateInstance.exec(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.init_staticclass:
            ////                                operators.OpCreateInstance.init_static(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.new_instance_class:
            ////                                operators.OpCreateInstance.exec_instanceClass(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.prepare_constructor_argement:
            ////                                operators.OpCreateInstance.prepareConstructorArgements(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.prepare_constructor_class_argement:
            ////                                operators.OpCreateInstance.prepareConstructorClassArgements(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.push_parameter_class:
            ////                                operators.OpCreateInstance.push_parameter_class(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.access_dot:
            ////                                operators.OpAccess_Dot.exec_dot(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.access_dot_byname:
            ////                                operators.OpAccess_Dot.exec_dot_byname(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.bracket_access:
            ////                                operators.OpAccess_Dot.exec_bracket_access(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.access_method:
            ////                                operators.OpAccess_Dot.exec_method(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.delete_prop:
            ////                                operators.OpDynamicProperty.exec_delete(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.set_dynamic_prop:
            ////                                operators.OpDynamicProperty.exec_set_dynamic_prop(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.try_read_getter:
            ////                                operators.OpPropGetSet.exec_try_read_prop(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.try_write_setter:
            ////                                operators.OpPropGetSet.exec_try_write_prop(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.array_push:
            ////                                operators.OpArray.exec_Push(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.array_create:
            ////                                operators.OpArray.exec_create(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.vectorAccessor_bind:
            ////                                //operators.OpVector.exec_AccessorBind(currentRunFrame, step, currentRunFrame.scope);
            ////                                {
            ////                                    ASBinCode.rtti.Vector_Data vector =
            ////                                        (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)step.arg1.getValue(scope)).value).hosted_object;

            ////                                    int idx = TypeConverter.ConvertToInt(step.arg2.getValue(scope), frame, step.token);

            ////                                    if (idx < 0 || idx > vector.innnerList.Count)
            ////                                    {
            ////                                        frame.throwError(step.token, 1125,
            ////                                            "The index " + idx + " is out of range " + vector.innnerList.Count + ".");
            ////                                    }

            ////                                    Register reg = (Register)step.reg;

            ////                                    if (idx == vector.innnerList.Count)
            ////                                    {
            ////                                        if (vector.isFixed || !reg._isassigntarget)
            ////                                        {
            ////                                            frame.throwError(step.token, 1125,
            ////                                            "The index " + idx + " is out of range " + vector.innnerList.Count + "."
            ////                                            );
            ////                                            frame.endStep(step);
            ////                                            break;
            ////                                        }
            ////                                        else
            ////                                        {
            ////                                            vector.innnerList.Add(TypeConverter.getDefaultValue(vector.vector_type).getValue(null));
            ////                                        }
            ////                                    }



            ////                                    StackSlot slot = (StackSlot)scope.stack[scope.offset + ((Register)step.reg)._index]; //step.reg.getSlot(scope);
            ////                                    if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete)
            ////                                    {
            ////                                        slot._cache_vectorSlot.idx = idx;
            ////                                        slot._cache_vectorSlot.vector_data = vector;

            ////                                        slot.linkTo(slot._cache_vectorSlot);
            ////                                    }
            ////                                    else
            ////                                    {
            ////                                        slot.directSet(vector.innnerList[idx]);
            ////                                    }

            ////                                    frame.endStep(step);
            ////                                }
            ////                                break;
            ////                            case OpCode.vector_push:
            ////                                operators.OpVector.exec_push(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.vector_pusharray:
            ////                                operators.OpVector.exec_pusharray(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.vector_pushvector:
            ////                                operators.OpVector.exec_pushVector(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.vectorAccessor_convertidx:
            ////                                operators.OpVector.exec_AccessorBind_ConvertIdx(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.vector_initfrmdata:
            ////                                operators.OpVector.exec_initfromdata(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.link_outpackagevairable:
            ////                                operators.OpLinkOutPackageScope.exec_link(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.flag_call_super_constructor:
            ////                                currentRunFrame.endStep(step);
            ////                                break;
            ////                            case OpCode.forin_get_enumerator:
            ////                                operators.OpForIn.forin_get_enumerator(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.enumerator_movenext:
            ////                                operators.OpForIn.enumerator_movenext(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.enumerator_current:
            ////                                operators.OpForIn.enumerator_current(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.enumerator_close:
            ////                                operators.OpForIn.enumerator_close(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.foreach_get_enumerator:
            ////                                operators.OpForIn.foreach_get_enumerator(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.logic_is:
            ////                                operators.OpLogic.exec_IS(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.logic_instanceof:
            ////                                operators.OpLogic.exec_instanceof(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.convert_as:
            ////                                operators.OpLogic.exec_AS(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.logic_in:
            ////                                operators.OpLogic.exec_In(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            case OpCode.unary_typeof:
            ////                                operators.OpTypeOf.exec_TypeOf(currentRunFrame, step, currentRunFrame.scope);
            ////                                break;
            ////                            default:

            ////                                currentRunFrame.runtimeError = (new error.InternalError(step.token,
            ////                                     step.opCode + "操作未实现"
            ////                                     ));
            ////                                currentRunFrame.endStep();
            ////                                break;
            ////                        }
            ////                    }
            //#endregion
            //                }

            //            }


            if (runtimeError != null)
            {
                outPutErrorMessage(runtimeError);
            }

#if DEBUG
            if (isConsoleOut)
            {
                Console.WriteLine();
                Console.WriteLine("====程序状态====");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Variables:");

                for (int i = 0; i < displayStackFrame.block.scope.members.Count; i++)
                {
                    Console.WriteLine("\t" + displayStackFrame.block.scope.members[i].name + "\t|\t" + displayStackFrame.scope.memberData[i].getValue());
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Registers:");
                for (int i = 0; i < displayStackFrame.block.totalRegisters; i++)
                {
                    if (stackSlots[i].getValue()!=null)
                    {
                        Console.WriteLine("\t" + "EAX(" + i + ")\t|\t" + stackSlots[i].getValue());
                    }
                }
                Console.ResetColor();
            }
#endif
            if (result != null && runtimeError==null)
            {
                return result.getValue(topscope);
            }
            else
            {
                return null;
            }

        }

        internal HeapSlot[] genHeapFromCodeBlock(ASBinCode.CodeBlock calledblock)
        {
            var memberDataList = new HeapSlot[calledblock.scope.members.Count];
            for (int i = 0; i < memberDataList.Length; i++)
            {
                memberDataList[i] = new HeapSlot();
                var vt = ((VariableBase)calledblock.scope.members[i]).valueType;
                memberDataList[i].setDefaultType(
                    vt,
                    TypeConverter.getDefaultValue(vt).getValue(null)
                    );
            }
            return memberDataList;
        }

        private static CodeBlock blankBlock;
        internal void CallBlankBlock(IBlockCallBack callbacker)
        {
            if (blankBlock == null)
            {
                blankBlock = new CodeBlock(int.MaxValue - 1, "#blank", -65535, false);
            }

            CallBlock(blankBlock, null, null, null,null, callbacker, null, RunTimeScopeType.function);

        }


        internal RunTimeScope CallBlock(ASBinCode.CodeBlock calledblock,
            HeapSlot[] membersHeap,
            SLOT returnSlot,
            RunTimeScope callerScope,
            SourceToken token,
            IBlockCallBack callbacker,
            RunTimeValueBase this_pointer,
            RunTimeScopeType type
            )
        {
            StackFrame frame = new StackFrame(calledblock);
            frame.codeLinePtr = 0;
            frame.player = this;
            frame.returnSlot = returnSlot;
            frame.callbacker = callbacker;

            int startOffset = 0;
            if (runtimeStack.Count > 0)
            {
                startOffset = runtimeStack.Peek().scope.offset + runtimeStack.Peek().block.totalRegisters+1+1;
            }

            if (startOffset + calledblock.totalRegisters+1+1 >= stackSlots.Length)
            {
                runtimeError = new error.InternalError(token, "stack overflow");
                
            }
            else
            {
                frame._tempSlot1 = stackSlots[startOffset + frame.block.totalRegisters];
                frame._tempSlot2 = stackSlots[startOffset + frame.block.totalRegisters+1];
                runtimeStack.Push(frame);
                currentRunFrame = frame;
            }




            RunTimeScope scope;
            
            scope = new RunTimeScope(swc,
                membersHeap, stackSlots, startOffset, calledblock.id, callerScope
                ,
                static_instance
                ,
                this_pointer,
                type
                //,
                //frame._dictMethods
            );
            
            frame.scope = scope;

            return frame.scope;
        }


        internal int getRuntimeStackFlag()
        {
            return runtimeStack.Count;
        }

        /// <summary>
        /// 执行到当前代码块结束
        /// </summary>
        /// <returns></returns>
        internal bool step_toStackflag(int stackflag)
        {
            int f = stackflag;
            while (step() && receive_error==null)
            {
                if (runtimeStack.Count == f)
                {
                    return true;
                }
            }
            return false;

        }




        private StackFrame currentRunFrame;
        public bool step()
        {
            if (runtimeError != null)
            {
                return false;
            }
            if (currentRunFrame == null)
            {
                return false;
            }

            if (receive_error != null)
            {
                var temp = receive_error;
                receive_error = null;
                currentRunFrame.receiveErrorFromStackFrame(temp);
                return true;
            }

            if (currentRunFrame.IsEnd()) //执行完成
            {
                runtimeStack.Pop(); //出栈


                var toclose = currentRunFrame;
                if (currentRunFrame.callbacker != null)
                {
                    IBlockCallBack temp = currentRunFrame.callbacker;
                    currentRunFrame.callbacker = null;
                    temp.call(temp.args);
                   
                }
                
                toclose.close();

                if (runtimeStack.Count > 0)
                {
                    currentRunFrame = runtimeStack.Peek();
                }
                else
                {
                    currentRunFrame = null;
                }
                
            }
            else
            {
                currentRunFrame.step();
            }

            return true;
        }

        private error.InternalError receive_error;
        internal void exitStackFrameWithError(error.InternalError error)
        {
            if (error.callStack == null) //收集调用栈
            {
                error.callStack = new Stack<StackFrame>();
            }
            error.callStack.Push(currentRunFrame); 

            runtimeStack.Pop();

            currentRunFrame.close();
            if (runtimeStack.Count > 0)
            {
                currentRunFrame = runtimeStack.Peek();
                receive_error = error;
                //currentRunFrame.receiveErrorFromStackFrame(error);
            }
            else
            {
                currentRunFrame = null;
                runtimeError = error;
            }
        }

        internal void outPutErrorMessage(error.InternalError err)
        {
            if (isConsoleOut)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("运行时错误");
                //Console.WriteLine("file :" + err.token.sourceFile);
                //Console.WriteLine("line :" + err.token.line + " ptr :" + err.token.ptr);

                if (err.errorValue != null)
                {
                    string errinfo= err.errorValue.ToString();
                    if (err.errorValue.rtType > RunTimeDataType.unknown && swc.ErrorClass !=null)
                    {
                        if (ClassMemberFinder.check_isinherits(err.errorValue, swc.ErrorClass.getRtType(), swc))
                        {
                            errinfo =
                                ((rtObject)err.errorValue).value.memberData[1].getValue().ToString()+" #"+
                                ((rtObject)err.errorValue).value.memberData[2].getValue().ToString()+" " +
                                ((rtObject)err.errorValue).value.memberData[0].getValue().ToString();
                        }
                    }


                    Console.WriteLine("[故障] " + "信息=" + errinfo);
                }
                else
                {
                    Console.WriteLine(err.message);
                }

                Stack<StackFrame> _temp = new Stack<StackFrame>();

                while (err.callStack !=null && err.callStack.Count>0)
                {
                    _temp.Push(err.callStack.Pop());
                    displayStackFrame = _temp.Peek();
                }

                foreach (var item in _temp)
                {
                    if (item.codeLinePtr < item.block.opSteps.Count)
                    {
                        Console.WriteLine(item.block.name + " at file:" + item.block.opSteps[item.codeLinePtr].token.sourceFile);
                        Console.WriteLine("\t\t line:" + item.block.opSteps[item.codeLinePtr].token.line + " ptr:" + item.block.opSteps[item.codeLinePtr].token.ptr);
                    }
                    else
                    {
                        Console.WriteLine(item.block.name);
                    }

                    Console.WriteLine("----");
                }


                Console.ResetColor();
            }
        }


        StringBuilder sb = new StringBuilder();
        internal string stackTrace(int skipline)
        {
            foreach (var item in runtimeStack)
            {
                if (skipline > 0)
                { 
                    skipline--;
                    continue;
                }

                if (item.codeLinePtr < item.block.opSteps.Count)
                {
                    sb.Append("\tat ");
                    sb.Append(item.block.name);
                    sb.Append(" [");
                    sb.Append(item.block.opSteps[item.codeLinePtr].token.sourceFile);
                    sb.Append(" ");
                    sb.Append(item.block.opSteps[item.codeLinePtr].token.line+1);
                    sb.Append(" ptr:");
                    sb.Append(  item.block.opSteps[item.codeLinePtr].token.ptr+1);
                    sb.Append("]");
                    sb.AppendLine();
                }
                else
                {
                    sb.Append("\tat ");
                    sb.AppendLine(item.block.name);
                }

                
            }

            

            string t = sb.ToString();
            sb.Remove(0, sb.Length);
            return t;
        }

    }
}
