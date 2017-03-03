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

                CompileEnv tempEnv = new CompileEnv(new CodeBlock(builder.getBlockId(),"temp"),true);
                tempEnv.block.scope = new ASBinCode.scopes.OutPackageMemberScope();
                builder.buildExpressNotEval(tempEnv, expression);
                tempEnv.completSteps();
                tempEnv.block.totalRegisters = tempEnv.combieRegisters();

                
                if (builder.buildErrors.Count == 0)
                {
                    IRightValue value = builds.ExpressionBuilder.getRightValue(tempEnv, expression.Value, expression.token, builder);

                    ASRuntime.Player player = new ASRuntime.Player();
                    player.isConsoleOut = false;

                    CSWC tempswc = new CSWC();tempswc.blocks.Add(tempEnv.block);
                    player.loadCode(tempswc);

                    IRunTimeValue result=  player.run2(value);

                    return result;
                    //IRunTimeScope scope = player.run();

                    //if (player.runtimeError ==null)
                    //{
                    //    return value.getValue(scope);
                    //}
                    //else
                    //{
                    //    return null;
                    //}
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
