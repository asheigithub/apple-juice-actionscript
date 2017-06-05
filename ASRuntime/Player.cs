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


        /// <summary>
        /// 当调用本地函数时，会自动设置这个字段，如果本地函数抛出异常并被try catch到后，会根据这个字段继续后续操作
        /// </summary>
        internal operators.FunctionCaller _nativefuncCaller;
        


        private error.InternalError runtimeError;

       
        /// <summary>
        /// 调用堆栈
        /// </summary>
        private Stack<StackFrame> runtimeStack;
        StackSlot[] stackSlots;
        private FrameInfo displayStackFrame;
        
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
                callBlock(block, initdata, new StackSlot(swc), null,
                    new SourceToken(0, 0, ""), null,
                    null, RunTimeScopeType.startup
                    );
                while (step())
                {

                }
            }


            HeapSlot[] data = genHeapFromCodeBlock(defaultblock);
            var topscope = callBlock(defaultblock,data ,new StackSlot(swc), null, 
                new SourceToken(0, 0, ""),null,
                null, RunTimeScopeType.startup
                );
            displayStackFrame = runtimeStack.Peek().getInfo();


            while (true)
            {
                //try
                {
                    while (step())
                    {

                    }
                    break;
                }
                //catch (ASRunTimeException)   //引擎抛出的异常直接抛出
                //{
                //    throw;
                //}
                //catch (StackOverflowException)
                //{
                //    throw;
                //}
                //catch (OutOfMemoryException)
                //{
                //    throw;
                //}
                //catch (Exception le)    //捕获外部函数异常
                //{
                //    if (currentRunFrame != null)
                //    {
                //        if (_nativefuncCaller != null)
                //        {
                //            if (_nativefuncCaller.callbacker != null)
                //            {
                //                _nativefuncCaller.callbacker.noticeRunFailed();
                //            }
                //            _nativefuncCaller.release();
                //            _nativefuncCaller = null;
                //        }

                //        SourceToken token;

                //        if (currentRunFrame.codeLinePtr < currentRunFrame.block.opSteps.Count)
                //        {
                //            token = currentRunFrame.block.opSteps[currentRunFrame.codeLinePtr].token;
                //        }
                //        else
                //        {
                //            token = new SourceToken(0, 0, string.Empty);
                //        }

                //        currentRunFrame.throwAneException(token
                //            , le.Message);
                //        currentRunFrame.receiveErrorFromStackFrame(currentRunFrame.runtimeError);

                //        continue;
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
            }
            

            


            if (runtimeError != null)
            {
                outPutErrorMessage(runtimeError);
            }

            operators.FunctionCaller.checkpool();
            BlockCallBackBase.checkpool();
            StackFrame.checkpool();

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
                return result.getValue(topscope,displayStackFrame);
            }
            else
            {
                return null;
            }

        }

        private static readonly HeapSlot[] emptyMembers = new HeapSlot[0];
        internal HeapSlot[] genHeapFromCodeBlock(ASBinCode.CodeBlock calledblock)
        {
            if (calledblock.scope.members.Count == 0)
            {
                return emptyMembers;
            }

            var memberDataList = new HeapSlot[calledblock.scope.members.Count];
            for (int i = 0; i < memberDataList.Length; i++)
            {
                memberDataList[i] = new HeapSlot();
                var vt = ((VariableBase)calledblock.scope.members[i]).valueType;
                memberDataList[i].setDefaultType(
                    vt,
                    TypeConverter.getDefaultValue(vt).getValue(null,null)
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

            callBlock(blankBlock, null, null, null,null, callbacker, null, RunTimeScopeType.function);

        }

        public rtObject alloc_pureHostedOrLinkedObject(ASBinCode.rtti.Class cls)
        {
            return operators.InstanceCreator.createPureHostdOrLinkObject(this, cls);
        }

        public ASBinCode.rtti.LinkSystemObject alloc_LinkObjValue(ASBinCode.rtti.Class cls)
        {
            return operators.InstanceCreator.createLinkObjectValue(this, cls);
        }

        public bool init_static_class(ASBinCode.rtti.Class cls,SourceToken token)
        {
            return operators.InstanceCreator.init_static_class(cls, this, token);
        }


        internal RunTimeScope callBlock(ASBinCode.CodeBlock calledblock,
            HeapSlot[] membersHeap,
            SLOT returnSlot,
            RunTimeScope callerScope,
            SourceToken token,
            IBlockCallBack callbacker,
            RunTimeValueBase this_pointer,
            RunTimeScopeType type
            )
        {
            StackFrame frame =  StackFrame.create(calledblock);
            frame.codeLinePtr = 0;
            frame.player = this;
            frame.returnSlot = returnSlot;
            frame.callbacker = callbacker;
            frame.static_objects = static_instance;

            int startOffset = 0;
            if (runtimeStack.Count > 0)
            {
                var rs = runtimeStack.Peek();

                startOffset = rs.offset + rs.block.totalRegisters+1+1 + rs.call_parameter_slotCount;
                
            }
            
            frame.offset = startOffset;
            frame.stack = stackSlots;

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


            if (
                ReferenceEquals(membersHeap, emptyMembers)
                && type == RunTimeScopeType.function
                &&
                this_pointer is rtObject
                &&
                (callerScope==null ||
                callerScope.scopeType != RunTimeScopeType.function)
                )
            {
                frame.scope = ((rtObject)this_pointer).objScope; //callerScope;
            }
            else
            {
                RunTimeScope scope;

                scope = new RunTimeScope(
                    membersHeap, calledblock.id, callerScope
                    ,
                    this_pointer,
                    type
                );

                frame.scope = scope;
            }
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
                error.callStack = new Stack<FrameInfo>();
            }
            error.callStack.Push(currentRunFrame.getInfo()); 

            runtimeStack.Pop();

            currentRunFrame.close();
            if (runtimeStack.Count > 0)
            {
                currentRunFrame = runtimeStack.Peek();
                receive_error = error;
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

                Stack<FrameInfo> _temp = new Stack<FrameInfo>();

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
                        Console.WriteLine("\t\t line:" + (item.block.opSteps[item.codeLinePtr].token.line+1 ) + " ptr:" + (item.block.opSteps[item.codeLinePtr].token.ptr+1));
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
