using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3SwitchBuilder
    {
        public void buildAS3Switch(CompileEnv env, ASTool.AS3.AS3Switch as3switch, Builder builder)
        {
            if (!string.IsNullOrEmpty(as3switch.label))
            {
                //**包装一个block**
                ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3switch.Token);
                tempblock.CodeList = new List<ASTool.AS3.IAS3Stmt>();
                tempblock.CodeList.Add(as3switch);
                tempblock.label = as3switch.label;
                as3switch.label = null;
                builder.buildStmt(env, tempblock);
            }
            else
            {
                //***先编译条件***

                ASTool.AS3.AS3StmtExpressions expressions = new ASTool.AS3.AS3StmtExpressions(as3switch.Token);
                expressions.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                expressions.as3exprlist.Add(as3switch.Expr);
                builder.buildStmt(env, expressions);
                //**取判断条件***
                ASTool.AS3.AS3Expression expr = as3switch.Expr;

                int defaults = 0;
                for (int i = 0; i < as3switch.CaseList.Count; i++)
                {
                    ASTool.AS3.AS3SwitchCase c = as3switch.CaseList[i];
                    if (c.IsDefault)
                    {
                        defaults++;
                    }

                    if (defaults > 1)
                    {
                        throw new BuildException(c.token.line,c.token.ptr,c.token.sourceFile,"switch只能有1个default:");
                    }
                }
                


                List<string> switchlabels = new List<string>();
                for (int i = 0; i < as3switch.CaseList.Count; i++)
                {
                    ASTool.AS3.AS3SwitchCase c = as3switch.CaseList[i];
                    if (!c.IsDefault)
                    {
                        switchlabels.Add(env.makeLabel("SWITCH_CASE"));
                    }
                    else
                    {
                        switchlabels.Add(env.makeLabel("SWITCH_DEFAULT"));
                    }
                }

                for (int i = 0; i < as3switch.CaseList.Count; i++)
                {
                    ASTool.AS3.AS3SwitchCase c = as3switch.CaseList[i];
                    if (!c.IsDefault)
                    {
                        RunTimeValueBase v= ExpressionEval.Eval(c.Condition);
                        if (v != null && c.Condition.Value.IsReg)
                        {
                            OpStep op = new OpStep(OpCode.assigning, new SourceToken(c.token.line, c.token.ptr, c.token.sourceFile));
                            var eax = env.createASTRegister(c.Condition.Value.Reg);
                            eax.setEAXTypeWhenCompile(v.rtType);

                            op.reg = eax;
                            op.regType = eax.valueType;
                            op.arg1 = new ASBinCode.rtData.RightValue(v);
                            op.arg1Type = v.rtType;

                            env.block.opSteps.Add(op);
                        }
                        else
                        {
                            ASTool.AS3.AS3StmtExpressions caseExpr = new ASTool.AS3.AS3StmtExpressions(c.token);
                            caseExpr.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                            caseExpr.as3exprlist.Add(c.Condition);
                            builder.buildStmt(env, caseExpr);
                        }
                        {
                            ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(c.token);
                            step.Type = ASTool.AS3.Expr.OpType.LogicEQ;
                            step.OpCode = "==";
                            step.Arg1 = c.holdreg;
                            step.Arg2 = expr.Value;
                            step.Arg3 = c.Condition.Value;

                            ASTool.AS3.AS3StmtExpressions compare = new ASTool.AS3.AS3StmtExpressions(c.token);
                            ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(c.token);
                            expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();
                            expression.exprStepList.Add(step);

							expression.Value = c.holdreg;

                            compare.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                            compare.as3exprlist.Add(expression);

                            builder.buildStmt(env, compare);
                        }

                        {
                            ASTool.AS3.AS3StmtExpressions conditionsJump = new ASTool.AS3.AS3StmtExpressions(c.token);
                            ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(c.token);
                            expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();
                            ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(c.token);
                            step.Type = ASTool.AS3.Expr.OpType.IF_GotoFlag;
                            step.OpCode = switchlabels[i];
                            step.Arg1 = c.holdreg;
                            expression.exprStepList.Add(step);

                            conditionsJump.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                            conditionsJump.as3exprlist.Add(expression);
                            builder.buildStmt(env, conditionsJump);
                        }
                    }
                    else
                    {
                        ASTool.AS3.AS3StmtExpressions jump = new ASTool.AS3.AS3StmtExpressions(c.token);
                        ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(c.token);
                        expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();
                        ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(c.token);
                        step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                        step.OpCode = switchlabels[i];
                        expression.exprStepList.Add(step);

                        jump.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                        jump.as3exprlist.Add(expression);
                        builder.buildStmt(env, jump);
                    }
                }

                int lblid = env.getLabelId();



                OpStep lbl_case_start = new OpStep(OpCode.flag, new SourceToken(as3switch.Token.line, as3switch.Token.ptr, as3switch.Token.sourceFile));
                lbl_case_start.flag = "SWITCH_START_" + lblid;
                env.block.opSteps.Add(lbl_case_start);

                for (int i = 0; i < as3switch.CaseList.Count; i++)
                {
                    ASTool.AS3.AS3SwitchCase c = as3switch.CaseList[i];

                    //if (!c.IsDefault)
                    {
                        OpStep lbl_case = new OpStep(OpCode.flag, new SourceToken(c.token.line, c.token.ptr, c.token.sourceFile));
                        lbl_case.flag = switchlabels[i];
                        env.block.opSteps.Add(lbl_case);
                    }
                    if (c.Body != null)
                    {
                        for (int j = 0; j < c.Body.Count; j++)
                        {
                            builder.buildStmt(env, c.Body[j]);
                        }
                    }
                }

                OpStep lbl_case_end = new OpStep(OpCode.flag, new SourceToken(as3switch.Token.line, as3switch.Token.ptr, as3switch.Token.sourceFile));
                lbl_case_end.flag = "SWITCH_END_" + lblid;
                env.block.opSteps.Add(lbl_case_end);
            }

        }
    }
}
