using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    public class Player
    {
        private ASBinCode.CodeBlock block;
        public void loadCode(ASBinCode.CodeBlock block)
        {
            this.block = block;
        }

        public Stack<error.InternalError> runtimeErrors = new Stack<error.InternalError>();


        public void run()
        {
            runtimeErrors.Clear();

            eax[] eaxs = new eax[block.totalRegisters];
            runtimeScope scope = new runtimeScope( block.scope.members );// block.scope.members.Count);
            for (int i = 0; i < eaxs.Length; i++)
            {
                eaxs[i] = new eax();
            }
            try
            {
                for (int i = 0; i < block.opSteps.Count; i++)
                {
                    OpStep step = block.opSteps[i];

                    switch (step.opCode)
                    {
                        case OpCode.cast:
                            operators.OpCast.execCast(this, step, eaxs, scope);
                            break;
                        case OpCode.assigning:
                            operators.OpAssigning.execAssigning(this, step, eaxs, scope);
                            break;
                        case OpCode.add_number:
                            operators.OpAdd.execAdd_Number(this, step, eaxs, scope);
                            break;
                        case OpCode.add_string:
                            operators.OpAdd.execAdd_String(this, step, eaxs, scope);
                            break;
                        case OpCode.add:
                            operators.OpAdd.execAdd(this, step, eaxs, scope);
                            break;
                        case OpCode.sub_number:
                            operators.OpSub.execSub_Number(this,step,eaxs,scope);
                            break;
                        case OpCode.sub:
                            operators.OpSub.execSub(this, step, eaxs, scope);
                            break;
                        case OpCode.neg:
                            operators.OpNeg.execNeg(this, step, eaxs, scope);
                            break;
                        case OpCode.gt:
                            operators.OpLogic.execGT(this, step, eaxs, scope);
                            break;
                        default:

                           runtimeErrors.Push(new error.InternalError  (step.token,
                                step.opCode + "操作未实现"
                                ));
                           break;
                    }

                    //检查该步骤是否发生错误
                    {
                        if (runtimeErrors.Count > 0)
                        {
                            var err = runtimeErrors.Peek();

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("运行时错误");
                            Console.WriteLine("file :" + err.token.sourceFile);
                            Console.WriteLine("line :" + err.token.line + " ptr :" + err.token.ptr);

                            Console.WriteLine(err.message);

                            Console.ResetColor();
                            return;
                        }
                    }
                }

            }
            finally
            {
                Console.WriteLine("Variables:");
                for (int i = 0; i < block.scope.members.Count; i++)
                {
                    Console.WriteLine( "\t"+block.scope.members[i].name + "\t|\t"+scope.memberData[i].getValue() );
                }

                Console.WriteLine("Registers:");
                for (int i = 0; i < eaxs.Length; i++)
                {
                    Console.WriteLine("\t"  + "EAX("+i+")\t|\t" + eaxs[i].getValue());
                }

            }
            
        }


        internal void throwCastException(ASBinCode.SourceToken token,RunTimeDataType srctype,RunTimeDataType dsttype)
        {
            runtimeErrors.Push( new error.InternalError (token, "类型转换失败:"+srctype+"->"+dsttype));
        }

        internal void throwOpException(ASBinCode.SourceToken token, OpCode opcode)
        {
            runtimeErrors.Push(new error.InternalError(token, "无法执行操作" + opcode ));
        }




        class eax : ASBinCode.IEAX
        {
            IRunTimeValue v;
            public IRunTimeValue getValue()
            {
                return v;
            }

            public void setValue(IRunTimeValue value)
            {
                v = value;
            }
        }
        class runtimeScope : IRunTimeScope
        {
            eax[] memberDataList;

            public runtimeScope(IList<IMember> members)
            {
                memberDataList = new eax[members.Count];
                for (int i = 0; i < memberDataList.Length; i++)
                {
                    memberDataList[i] = new eax();
                    memberDataList[i].setValue( 
                        TypeConverter.getDefaultValue( ((Variable)members[i]).valueType).getValue( null,null ) );

                }
            }

            public IEAX[] memberData
            {
                get
                {
                    return memberDataList;
                }
            }

            public IRunTimeScope parent
            {
                get
                {
                    return null;
                }
            }
        }
    }
}
