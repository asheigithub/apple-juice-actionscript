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

        internal CSWC swc;
        private CodeBlock defaultblock;
        public void loadCode(CSWC swc)
        {
            this.swc = swc;
            defaultblock = swc.blocks[0];

            runtimeStack = new Stack<StackFrame>();
            stackSlots = new StackSlot[1024];
            
        }

        private error.InternalError runtimeError;

       
        /// <summary>
        /// 调用堆栈
        /// </summary>
        private Stack<StackFrame> runtimeStack;
        StackSlot[] stackSlots;

        public IRunTimeValue run2(IRightValue result)
        {
            
            for (int i = 0; i < stackSlots.Length; i++)
            {
                stackSlots[i] = new StackSlot();
            }

            var topscope = CallBlock(defaultblock, genHeapFromCodeBlock(defaultblock) ,new StackSlot(), null, new SourceToken(0, 0, ""));
            while (step())
            {

            }

            if (runtimeError != null)
            {
                outPutErrorMessage(runtimeError);
            }

            if (isConsoleOut)
            {
                Console.WriteLine();
                Console.WriteLine("====程序状态====");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Variables:");
                for (int i = 0; i < defaultblock.scope.members.Count; i++)
                {
                    Console.WriteLine("\t" + defaultblock.scope.members[i].name + "\t|\t" + topscope.memberData[i].getValue());
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Registers:");
                for (int i = 0; i < defaultblock.totalRegisters; i++)
                {
                    Console.WriteLine("\t" + "EAX(" + i + ")\t|\t" + stackSlots[i].getValue());
                }
                Console.ResetColor();
            }

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
                memberDataList[i].setDefaultType(
                    ((Variable)calledblock.scope.members[i]).valueType
                    );
            }
            return memberDataList;
        }

        internal IRunTimeScope CallBlock(ASBinCode.CodeBlock calledblock, 
            HeapSlot[] membersHeap ,
            ISLOT returnSlot, 
            IRunTimeScope callerScope, 
            SourceToken token)
        {
            StackFrame frame = new StackFrame();
            frame.block = calledblock;
            frame.codeLinePtr = 0;
            frame.player = this;
            frame.returnSlot = returnSlot;
            
            

            int startOffset = 0;
            if (runtimeStack.Count > 0)
            {
                startOffset = runtimeStack.Peek().scope.offset + runtimeStack.Peek().block.totalRegisters;
            }

            if (startOffset + calledblock.totalRegisters >= stackSlots.Length)
            {
                runtimeError = new error.InternalError(token, "stack overflow");
                
            }
            else
            {
                runtimeStack.Push(frame);
                currentRunFrame = frame;
            }

            

            RunTimeScope scope = new RunTimeScope(membersHeap, stackSlots, startOffset, calledblock.id,callerScope);
            frame.scope = scope;

            return frame.scope;
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

            if (currentRunFrame.IsEnd()) //执行完成
            {
                
                runtimeStack.Pop(); //出栈
                if (runtimeStack.Count > 0)
                {
                    currentRunFrame.close();//Eval需保留第一个栈的数值不清空
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

        internal void exitStackFrameWithError(error.InternalError error)
        {
            if (error.callStack == null) //收集调用栈
            {
                error.callStack = new Stack<StackFrame>();
            }
            error.callStack.Push(currentRunFrame); 

            
            runtimeStack.Pop();
            if (runtimeStack.Count > 0)
            {
                currentRunFrame.close();//Eval需保留第一个栈的数值不清空
                currentRunFrame = runtimeStack.Peek();
                currentRunFrame.receiveErrorFromStackFrame(error);
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

    }
}
