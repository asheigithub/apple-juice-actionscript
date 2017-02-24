using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
    /// <summary>
    /// 表达式求值
    /// </summary>
    public class ExpressionEval
    {
        public static ASBinCode.IRunTimeValue Eval(ASTool.AS3.AS3Expression expression)
        {

            try
            {

                Builder builder = new Builder();
                builder.isConsoleOut = false;

                CompileEnv tempEnv = new CompileEnv(new CodeBlock());
                tempEnv.block.scope = new ASBinCode.scopes.OutPackageMemberScope();
                builder.buildExpression(tempEnv, expression);
                tempEnv.completSteps();
                tempEnv.block.totalRegisters = tempEnv.combieRegisters();

                IRightValue value = builds.ExpressionBuilder.getRightValue(tempEnv, expression.Value, expression.token );

                if (builder.buildErrors.Count == 0)
                {
                    ASRuntime.Player player = new ASRuntime.Player();
                    player.isConsoleOut = false;
                    player.loadCode(tempEnv.block);

                    IRunTimeScope scope = player.run();

                    if (player.runtimeErrors.Count == 0)
                    {
                        return value.getValue(scope);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (BuildException)
            {
                return null; 
            }

        }
    }
}
