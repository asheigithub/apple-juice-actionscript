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
		private static ASRuntime.Player player;

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

                tempEnv.completSteps(builder);
                tempEnv.block.totalStackSlots = tempEnv.combieNeedStackSlots();

                if (builder.buildErrors.Count == 0)
                {
                    RightValueBase value = builds.ExpressionBuilder.getRightValue(tempEnv, expression.Value, expression.token, builder);

					if (player == null)
					{
						player = new ASRuntime.Player(null);
						
					}
                    CSWC tempswc = new CSWC();
                    if (importBuilder != null)
                    {
                        tempswc.blocks.AddRange(importBuilder.bin.blocks);
                        tempswc.classes.AddRange(importBuilder.bin.classes);
                        tempswc.functions.AddRange(importBuilder.bin.functions);
                    }
                    tempswc.blocks.Add(tempEnv.block);

					Variable variableResult = new Variable("@@", tempEnv.block.scope.members.Count, tempEnv.block.id);
					tempEnv.block.scope.members.Add(variableResult);
					OpStep step = new OpStep(OpCode.assigning, new SourceToken(0, 0, ""));
					step.reg = variableResult;
					step.arg1 = value;
					tempEnv.block.opSteps.Add(step);
					tempEnv.block.instructions = tempEnv.block.opSteps.ToArray();
					tempEnv.block.opSteps = null;
                    player.loadCode(tempswc,tempEnv.block);

                    RunTimeValueBase result=  player.run(variableResult);

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
