using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3IFBuilder
    {
        public void buildAS3IF(CompileEnv env, ASTool.AS3.AS3IF as3if,Builder builder)
        {

            if (!string.IsNullOrEmpty(as3if.label))
            {
                //**包装一个block**
                ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3if.Token);
                tempblock.CodeList = new List<ASTool.AS3.IAS3Stmt>();
                tempblock.CodeList.Add(as3if);
                tempblock.label = as3if.label;
                as3if.label = null;
                builder.buildStmt(env, tempblock);
            }
            else
            {
                //***先编译条件***
                builder.buildStmt(env, as3if.Condition);
                //**取判断条件***
                ASTool.AS3.AS3Expression expr = as3if.Condition.as3exprlist[as3if.Condition.as3exprlist.Count - 1];

                //if true goto TRUE_LBL
                //***
                //goto IF_END_LBL
                //TRUE_LBL
                //***
                //IF_END_LBL


                string truelabel = env.makeLabel("IF_TRUE");
                string endlabel = env.makeLabel("IF_END");

                {
                    ASTool.AS3.AS3StmtExpressions conditionsJump = new ASTool.AS3.AS3StmtExpressions(as3if.Condition.Token);
                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3if.Condition.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();
                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3if.Token);
                    step.Type = ASTool.AS3.Expr.OpType.IF_GotoFlag;
                    step.OpCode = truelabel;
                    step.Arg1 = expr.Value;
                    expression.exprStepList.Add(step);

                    conditionsJump.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                    conditionsJump.as3exprlist.Add(expression);
                    builder.buildStmt(env, conditionsJump);
                }
                {
                    //false部分
                    if (as3if.FalsePass != null)
                    {
                        for (int i = 0; i < as3if.FalsePass.Count; i++)
                        {
                            builder.buildStmt(env, as3if.FalsePass[i]);
                        }
                    }

                    //跳转到end;
                    ASTool.AS3.AS3StmtExpressions jumptoend = new ASTool.AS3.AS3StmtExpressions(as3if.Token);
                    jumptoend.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3if.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3if.Token);
                    step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                    step.OpCode = endlabel;
                    expression.exprStepList.Add(step);

                    jumptoend.as3exprlist.Add(expression);
                    builder.buildStmt(env, jumptoend);

                    //**写入true标记
                    OpStep lbl_true = new OpStep(OpCode.flag, new SourceToken(as3if.Token.line, as3if.Token.ptr, as3if.Token.sourceFile));
                    lbl_true.flag = truelabel;
                    env.block.opSteps.Add(lbl_true);
                }
                {
                    //true部分
                    if (as3if.TruePass != null)
                    {
                        for (int i = 0; i < as3if.TruePass.Count; i++)
                        {
                            builder.buildStmt(env, as3if.TruePass[i]);
                        }
                    }

                    //**写入end标记
                    OpStep lbl_end = new OpStep(OpCode.flag, new SourceToken(as3if.Token.line, as3if.Token.ptr, as3if.Token.sourceFile));
                    lbl_end.flag = endlabel;
                    env.block.opSteps.Add(lbl_end);
                }


            }
        }
    }
}
