using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3BreakBuilder
    {
        public void buildAS3Break(CompileEnv env, ASTool.AS3.AS3Break as3break)
        {
            if (string.IsNullOrEmpty(as3break.breakFlag))
            {
                //throw new BuildException(as3break.Token.line, as3break.Token.ptr, as3break.Token.sourceFile,
                //                    "语句关联break尚未实现");
                //***向上查找

                int l = 0;

                for (int i = env.block.opSteps.Count -1; i >=0; i--)
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
                                OpStep op = new OpStep(OpCode.jmp, new SourceToken(as3break.Token.line, as3break.Token.ptr, as3break.Token.sourceFile));
                                op.reg = null;
                                op.regType = RunTimeDataType.unknown;
                                op.arg1 = new ASBinCode.rtData.RightValue(
                                    new ASBinCode.rtData.rtString("LOOP_END_" + id));
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

                throw new BuildException(as3break.Token.line, as3break.Token.ptr, as3break.Token.sourceFile,
                                    "break的关联块未找到");
            }
            else
            {

                //语句块break

                int l = 0;
                for (int i = 0; i < env.block.opSteps.Count ; i++)
                {
                    var s = env.block.opSteps[i];
                    if (s.opCode == OpCode.flag)
                    {
                        if (s.flag == "Label_End_" + as3break.breakFlag)
                        {
                            l--;
                        }
                        else if (s.flag == "Label_Start_" + as3break.breakFlag)
                        {
                            l++;
                        }
                    }
                }
                if (l < 1)
                {
                    throw new BuildException(as3break.Token.line, as3break.Token.ptr, as3break.Token.sourceFile,
                                    "break的关联标记["+ as3break.breakFlag +"]未找到");
                }

                //label标记跳转
                OpStep op = new OpStep(OpCode.jmp, new SourceToken(as3break.Token.line, as3break.Token.ptr, as3break.Token.sourceFile));
                op.reg = null;
                op.regType = RunTimeDataType.unknown;
                op.arg1 = new ASBinCode.rtData.RightValue(
                    new ASBinCode.rtData.rtString("Label_End_"+as3break.breakFlag));
                op.arg1Type = RunTimeDataType.rt_string;
                op.arg2 = null;
                op.arg2Type = RunTimeDataType.unknown;

                env.block.opSteps.Add(op);
            }

        }
    }
}
