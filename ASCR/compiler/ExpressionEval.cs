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
        public static ASBinCode.RunTimeValueBase Eval(ASTool.AS3.AS3Expression expression,
            Builder importBuilder=null
            )
        {

            try
            {

                Builder builder = new Builder(true);
                builder.isConsoleOut = false;

                int bid = builder.getBlockId();

                if (importBuilder != null && importBuilder._currentImports.Count > 0)
                {
                    List<ASBinCode.rtti.Class> imps = new List<ASBinCode.rtti.Class>();
                    imps.AddRange(importBuilder._currentImports.Peek());

                    builder._currentImports.Push(imps);

                    bid = importBuilder.bin.blocks.Count;

                }

                CompileEnv tempEnv = new CompileEnv(new CodeBlock(bid,"temp",-65535,true),true);
                tempEnv.block.scope = new ASBinCode.scopes.StartUpBlockScope();
                builder.buildExpressNotEval(tempEnv, expression);

                tempEnv.completSteps();
                tempEnv.block.totalRegisters = tempEnv.combieNeedStackSlots();

                if (builder.buildErrors.Count == 0)
                {
                    RightValueBase value = builds.ExpressionBuilder.getRightValue(tempEnv, expression.Value, expression.token, builder);

                    ASRuntime.Player player = new ASRuntime.Player();
                    player.isConsoleOut = false;

                    CSWC tempswc = new CSWC();
                    if (importBuilder != null)
                    {
                        tempswc.blocks.AddRange(importBuilder.bin.blocks);
                        tempswc.classes.AddRange(importBuilder.bin.classes);
                        tempswc.functions.AddRange(importBuilder.bin.functions);
                    }
                    tempswc.blocks.Add(tempEnv.block);
                    
                    player.loadCode(tempswc,tempEnv.block);

                    RunTimeValueBase result=  player.run2(value);

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
