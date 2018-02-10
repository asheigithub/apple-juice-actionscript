using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3ContinueBuilder
    {
        public void buildAS3Continue(CompileEnv env, ASTool.AS3.AS3Continue as3continue,Builder builder)
        {
            if (string.IsNullOrEmpty(as3continue.continueFlag))
            {


                int l = 0;

                for (int i = env.block.opSteps.Count - 1; i >= 0; i--)
                {
                    var s = env.block.opSteps[i];
                    if (s.opCode == OpCode.flag)
                    {
                        if (s.flag.StartsWith("LOOP_END_"))
                        {
                            l--;
                        }

                        if (s.flag.StartsWith("LOOP_START_"))
                        {
                            if (l == 0)
                            {
                                string id = s.flag.Substring(11);

                                //label标记跳转
                                OpStep op = new OpStep(OpCode.jmp, new SourceToken(as3continue.Token.line, as3continue.Token.ptr, as3continue.Token.sourceFile));
                                op.reg = null;
                                op.regType = RunTimeDataType.unknown;
                                op.arg1 = new ASBinCode.rtData.RightValue(
									new ASBinCode.rtData.rtString("LOOP_CONTINUE_" + id));
									//new ASBinCode.rtData.rtString("LOOP_START_" + id));
								op.arg1Type = RunTimeDataType.rt_string;
                                op.arg2 = null;
                                op.arg2Type = RunTimeDataType.unknown;

                                env.block.opSteps.Add(op);

                                return;
                            }
                            else
                            {
                                l++;
                            }
                        }
                    }
                }

                throw new BuildException(as3continue.Token.line, as3continue.Token.ptr, as3continue.Token.sourceFile,
                                    "continue的关联块未找到");


            }
            else
            {

                //命名label的continue;
                int l = 0;
                int stidx = 0;
                for (int i = 0; i < env.block.opSteps.Count; i++)
                {
                    var s = env.block.opSteps[i];
                    if (s.opCode == OpCode.flag)
                    {
                        if (s.flag == "LOOP_LABEL_END_" + as3continue.continueFlag)
                        {
                            l--;
                        }
                        else if (s.flag == "LOOP_LABEL_START_" + as3continue.continueFlag)
                        {
                            l++;
                            stidx = i;
                        }
                    }
                }
                if (l < 1)
                {
                    throw new BuildException(as3continue.Token.line, as3continue.Token.ptr, as3continue.Token.sourceFile,
                                    "continue的关联标记[" + as3continue.continueFlag + "]未找到");
                }

                //**查找关联的循环**
                for (int i = stidx; i < env.block.opSteps.Count; i++)
                {
                    var s = env.block.opSteps[i];
                    if (s.opCode == OpCode.flag)
                    {
                        if (s.flag.StartsWith("LOOP_START_"))
                        {
                            string id = s.flag.Substring(11);

                            //label标记跳转
                            OpStep op = new OpStep(OpCode.jmp, new SourceToken(as3continue.Token.line, as3continue.Token.ptr, as3continue.Token.sourceFile));
                            op.reg = null;
                            op.regType = RunTimeDataType.unknown;
                            op.arg1 = new ASBinCode.rtData.RightValue(
								//new ASBinCode.rtData.rtString("LOOP_CONTINUE_" + id));
								new ASBinCode.rtData.rtString("LOOP_START_" + id));
							op.arg1Type = RunTimeDataType.rt_string;
                            op.arg2 = null;
                            op.arg2Type = RunTimeDataType.unknown;

                            env.block.opSteps.Add(op);

                            return;
                            
                        }
                    }
                }

                throw new BuildException(as3continue.Token.line, as3continue.Token.ptr, as3continue.Token.sourceFile,
                                    "continue的关联标记[" + as3continue.continueFlag + "]未找到");
            }
        }
    }
}
