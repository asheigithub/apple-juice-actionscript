using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class FuncCallBuilder
    {
        public void buildFuncCall(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {


            if (step.Arg2.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
            {
                if (step.Arg2.Data.Value.ToString() == "trace")
                {
                    OpStep op = new OpStep(OpCode.native_trace,
                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    var eax = env.createASTRegister( step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                    op.reg = eax;
                    op.regType = RunTimeDataType.rt_void;


                    List<ASTool.AS3.Expr.AS3DataStackElement> args
                        = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;

                    if (args.Count > 1)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "trace函数目前只接受1个函数"));
                    }
                    else if (args.Count > 0)
                    {
                        op.arg1 = ExpressionBuilder.getRightValue(env, args[0], step.token);
                        op.arg1Type = op.arg1.valueType;
                    }
                    else
                    {
                        op.arg1 = null;
                        op.arg1Type = RunTimeDataType.unknown;
                    }
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);
                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "目前只实现了trace函数"));
                }
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "目前只实现了trace函数"));
            }
        }
    }
}
