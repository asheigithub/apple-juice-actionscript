using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3ThrowBuilder
    {
        public void buildAS3Throw(CompileEnv env, ASTool.AS3.AS3Throw as3throw,Builder builder)
        {
            if (as3throw.Exception != null)
            {
                var testV = ExpressionEval.Eval(as3throw.Exception);
                if (testV != null && as3throw.Exception.Value.IsReg)
                {
                    OpStep op = new OpStep(OpCode.raise_error, new SourceToken(as3throw.Token.line, as3throw.Token.ptr, as3throw.Token.sourceFile));

                    op.arg1 = new ASBinCode.rtData.RightValue( testV);
                    op.arg1Type = testV.rtType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;
                    op.reg = null;
                    op.regType = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);
                }
                else
                {
                    builder.buildExpression(env, as3throw.Exception);

                    OpStep op = new OpStep(OpCode.raise_error, new SourceToken(as3throw.Token.line, as3throw.Token.ptr, as3throw.Token.sourceFile));

                    IRightValue lv = builds.ExpressionBuilder.getRightValue(env, as3throw.Exception.Value, as3throw.Token);
                    op.arg1 = lv;
                    op.arg1Type = lv.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;
                    op.reg = null;
                    op.regType = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);

                }

            }
            else
            {
                OpStep op = new OpStep(OpCode.raise_error, new SourceToken(as3throw.Token.line, as3throw.Token.ptr, as3throw.Token.sourceFile));

                op.arg1 = null;
                op.arg1Type =  RunTimeDataType.unknown;
                op.arg2 = null;
                op.arg2Type = RunTimeDataType.unknown;
                op.reg = null;
                op.regType = RunTimeDataType.unknown;

                env.block.opSteps.Add(op);
            }
        }
    }
}
