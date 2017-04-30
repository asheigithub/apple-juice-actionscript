using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3LoopBuilder
    {
        public void buildAS3For(CompileEnv env, ASTool.AS3.AS3For as3for, Builder builder)
        {
            if (!string.IsNullOrEmpty(as3for.label))
            {


                //**包装一个block**
                ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3for.Token);
                tempblock.CodeList = new List<ASTool.AS3.IAS3Stmt>();
                tempblock.CodeList.Add(as3for);
                tempblock.label = as3for.label;
                as3for.label = null;


                OpStep lbl_forlabel = new OpStep(OpCode.flag, new SourceToken(as3for.Token.line, as3for.Token.ptr, as3for.Token.sourceFile));
                lbl_forlabel.flag = "LOOP_LABEL_START_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel);

                builder.buildStmt(env, tempblock);


                OpStep lbl_forlabel_end = new OpStep(OpCode.flag, new SourceToken(as3for.Token.line, as3for.Token.ptr, as3for.Token.sourceFile));
                lbl_forlabel_end.flag = "LOOP_LABEL_END_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel_end);
            }
            else
            {
                //***
                //FOR_LOOPSTART
                //***For_Part2***
                //***if_true goto FOR_LOOPBODY
                //***goto FOR_END
                //FOR_LOOPBODY
                //***ForBody
                //FOR_CONTINUE
                //***For_Part3
                //**goto FOR_LOOPSTART
                //FOR_END

                int lblid = env.getLabelId();

                string loopstart = "LOOP_START_" + lblid;
                string loopbody = "LOOP_BODY_" + lblid;
                string loopcontinue = "LOOP_CONTINUE_" + lblid;
                string loopend = "LOOP_END_" + lblid;

                {
                    OpStep lbl_loopstart = new OpStep(OpCode.flag, new SourceToken(as3for.Token.line, as3for.Token.ptr, as3for.Token.sourceFile));
                    lbl_loopstart.flag = loopstart;
                    env.block.opSteps.Add(lbl_loopstart);
                }
                if (as3for.Part2 != null)
                {
                    builder.buildStmt(env, as3for.Part2);
                    ASTool.AS3.AS3Expression expr =
                        as3for.Part2.as3exprlist[as3for.Part2.as3exprlist.Count - 1];
                    {
                        ASTool.AS3.AS3StmtExpressions conditionsJump =
                            new ASTool.AS3.AS3StmtExpressions(as3for.Part2.Token);
                        ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3for.Part2.Token);
                        expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                        ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3for.Part2.Token);
                        step.Type = ASTool.AS3.Expr.OpType.IF_GotoFlag;
                        step.OpCode = loopbody;
                        step.Arg1 = expr.Value;
                        expression.exprStepList.Add(step);

                        conditionsJump.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                        conditionsJump.as3exprlist.Add(expression);
                        builder.buildStmt(env, conditionsJump);
                    }
                    {
                        ASTool.AS3.AS3StmtExpressions jumptoend = new ASTool.AS3.AS3StmtExpressions(as3for.Part2.Token);
                        jumptoend.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                        ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3for.Part2.Token);
                        expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                        ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3for.Part2.Token);
                        step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                        step.OpCode = loopend;
                        expression.exprStepList.Add(step);

                        jumptoend.as3exprlist.Add(expression);
                        builder.buildStmt(env, jumptoend);
                    }
                }
                {
                    OpStep lbl_loopbody = new OpStep(OpCode.flag, new SourceToken(as3for.Token.line, as3for.Token.ptr, as3for.Token.sourceFile));
                    lbl_loopbody.flag = loopbody;
                    env.block.opSteps.Add(lbl_loopbody);
                }
                if (as3for.Body != null)
                {
                    for (int i = 0; i < as3for.Body.Count; i++)
                    {
                        builder.buildStmt(env, as3for.Body[i]);
                    }
                }
                {
                    OpStep lbl_loopcontinue = new OpStep(OpCode.flag, new SourceToken(as3for.Token.line, as3for.Token.ptr, as3for.Token.sourceFile));
                    lbl_loopcontinue.flag = loopcontinue;
                    env.block.opSteps.Add(lbl_loopcontinue);
                }
                if (as3for.Part3 != null)
                {
                    builder.buildStmt(env, as3for.Part3);
                }
                {
                    //强行跳回开始
                    ASTool.AS3.AS3StmtExpressions jumptostart = new ASTool.AS3.AS3StmtExpressions(as3for.Token);
                    jumptostart.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3for.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3for.Token);
                    step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                    step.OpCode = loopstart;
                    expression.exprStepList.Add(step);

                    jumptostart.as3exprlist.Add(expression);
                    builder.buildStmt(env, jumptostart);
                }
                {
                    //结束标记
                    OpStep lbl_loopend = new OpStep(OpCode.flag, new SourceToken(as3for.Token.line, as3for.Token.ptr, as3for.Token.sourceFile));
                    lbl_loopend.flag = loopend;
                    env.block.opSteps.Add(lbl_loopend);
                }


            }
        }

        public void buildAS3ForIn(CompileEnv env, ASTool.AS3.AS3ForIn as3forin, Builder builder)
        {
            if (!string.IsNullOrEmpty(as3forin.label))
            {
                //**包装一个block**
                ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3forin.Token);
                tempblock.CodeList = new List<ASTool.AS3.IAS3Stmt>();
                tempblock.CodeList.Add(as3forin);
                tempblock.label = as3forin.label;
                as3forin.label = null;


                OpStep lbl_forlabel = new OpStep(OpCode.flag, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                lbl_forlabel.flag = "LOOP_LABEL_START_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel);

                builder.buildStmt(env, tempblock);


                OpStep lbl_forlabel_end = new OpStep(OpCode.flag, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                lbl_forlabel_end.flag = "LOOP_LABEL_END_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel_end);
            }
            else
            {
                int lblid = env.getLabelId();

                builder.buildStmt(env, as3forin.ForArg);

                ILeftValue varvalue = null;

                if (as3forin.ForArg is ASTool.AS3.AS3StmtExpressions)
                {
                    ASTool.AS3.AS3StmtExpressions exprs = (ASTool.AS3.AS3StmtExpressions)as3forin.ForArg;
                    if (exprs.as3exprlist.Count != 1)
                    {
                        throw new BuildException(
                            as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile, "Syntax error: invalid for-in initializer, only 1 expression expected.");
                    }

                    ASTool.AS3.AS3Expression expr = exprs.as3exprlist[0];
                    varvalue =(ILeftValue)ExpressionBuilder.getRightValue(env, expr.Value, expr.token, builder);
                }
                else if (as3forin.ForArg is ASTool.AS3.AS3Variable)
                {
                    ASTool.AS3.AS3Variable v = (ASTool.AS3.AS3Variable)as3forin.ForArg;
                    varvalue= (VariableBase)MemberFinder.find(v.Name, env, false, builder, v.token);
                }
                else
                {
                    throw new BuildException(
                            as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile, "异常错误，获得的变量类型不正确");
                }

                builder.buildExpression(env, as3forin.ForExpr);
                IRightValue enumerator = ExpressionBuilder.getRightValue(env,as3forin.ForExpr.Value,as3forin.ForExpr.token,builder);

                if (enumerator.valueType == RunTimeDataType.rt_void
                    ||
                    enumerator.valueType < RunTimeDataType.unknown
                    )
                {
                    enumerator = ExpressionBuilder.addCastOpStep(env, enumerator, RunTimeDataType._OBJECT, 
                        new SourceToken( as3forin.ForExpr.token.line,
                         as3forin.ForExpr.token.ptr,
                          as3forin.ForExpr.token.sourceFile)
                        , 
                        builder);
                }

                Register regSaveEnumerator = env.getAdditionalRegister();
                regSaveEnumerator.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                {
                    //**IEnumerator Get And Reset();
                    OpStep opGetEnumerator = new OpStep(OpCode.forin_get_enumerator,
                        new SourceToken(as3forin.Token.line,
                         as3forin.Token.ptr,
                          as3forin.Token.sourceFile)
                        );

                    opGetEnumerator.reg = regSaveEnumerator;
                    opGetEnumerator.regType = RunTimeDataType.rt_void;
                    opGetEnumerator.arg1 = enumerator;
                    opGetEnumerator.arg1Type = enumerator.valueType;

                    env.block.opSteps.Add(opGetEnumerator);
                }

                string loopstart = "LOOP_START_" + lblid;
                string loopbody = "LOOP_BODY_" + lblid;
                string loopcontinue = "LOOP_CONTINUE_" + lblid;
                string loopend = "LOOP_END_" + lblid;

                {
                    OpStep lbl_loopstart = new OpStep(OpCode.flag, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                    lbl_loopstart.flag = loopstart;
                    env.block.opSteps.Add(lbl_loopstart);
                }

                Register eaxismovenext = env.getAdditionalRegister();
                eaxismovenext.setEAXTypeWhenCompile(RunTimeDataType.rt_boolean);
                {
                    //**IEnumerator MoveNext()
                    OpStep opMoveNext = new OpStep(OpCode.enumerator_movenext,
                        new SourceToken(as3forin.Token.line,
                         as3forin.Token.ptr,
                          as3forin.Token.sourceFile)
                        );

                    opMoveNext.reg = eaxismovenext;
                    opMoveNext.regType = RunTimeDataType.rt_boolean;
                    opMoveNext.arg1 = regSaveEnumerator;
                    opMoveNext.arg1Type = regSaveEnumerator.valueType;

                    env.block.opSteps.Add(opMoveNext);
                }

                {
                    OpStep op = new OpStep(OpCode.if_jmp, new SourceToken(as3forin.ForExpr.token.line,
                        as3forin.ForExpr.token.ptr, as3forin.ForExpr.token.sourceFile));
                    op.reg = null;
                    op.regType = RunTimeDataType.unknown;
                    op.arg1 = eaxismovenext;
                    op.arg1Type = eaxismovenext.valueType;
                    op.arg2 = new ASBinCode.rtData.RightValue( 
                        new ASBinCode.rtData.rtString( loopbody  )) ;
                    op.arg2Type = RunTimeDataType.rt_string;

                    env.block.opSteps.Add(op);
                }
                {
                    ASTool.AS3.AS3StmtExpressions jumptoend = new ASTool.AS3.AS3StmtExpressions(as3forin.Token);
                    jumptoend.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3forin.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3forin.Token);
                    step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                    step.OpCode = loopend;
                    expression.exprStepList.Add(step);

                    jumptoend.as3exprlist.Add(expression);
                    builder.buildStmt(env, jumptoend);
                }
                {
                    OpStep lbl_loopbody = new OpStep(OpCode.flag, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                    lbl_loopbody.flag = loopbody;
                    env.block.opSteps.Add(lbl_loopbody);
                }

                {
                    IRightValue loadValue = env.getAdditionalRegister();
                    ((Register)loadValue).setEAXTypeWhenCompile(RunTimeDataType.rt_void);

                    //**IEnumerator Current()
                    OpStep opCurrent = new OpStep(OpCode.enumerator_current,
                        new SourceToken(as3forin.Token.line,
                         as3forin.Token.ptr,
                          as3forin.Token.sourceFile)
                        );

                    opCurrent.reg = (Register)loadValue;
                    opCurrent.regType = RunTimeDataType.rt_void;
                    opCurrent.arg1 = regSaveEnumerator;
                    opCurrent.arg1Type = regSaveEnumerator.valueType;

                    env.block.opSteps.Add(opCurrent);

                    if (varvalue.valueType != RunTimeDataType.rt_void)
                    {
                        loadValue= ExpressionBuilder.addCastOpStep(env, loadValue, varvalue.valueType,
                            new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile)
                            ,
                            builder
                            );
                    }

                    var  op = new OpStep(OpCode.assigning, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                    op.reg = varvalue;
                    op.regType = varvalue.valueType;
                    op.arg1 = loadValue;
                    op.arg1Type = loadValue.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);
                }
                if (as3forin.Body != null)
                {
                    for (int i = 0; i < as3forin.Body.Count; i++)
                    {
                        builder.buildStmt(env, as3forin.Body[i]);
                    }
                }
                {
                    OpStep lbl_loopcontinue = new OpStep(OpCode.flag, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                    lbl_loopcontinue.flag = loopcontinue;
                    env.block.opSteps.Add(lbl_loopcontinue);
                }
                {
                    //强行跳回开始
                    ASTool.AS3.AS3StmtExpressions jumptostart = new ASTool.AS3.AS3StmtExpressions(as3forin.Token);
                    jumptostart.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3forin.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();
                    
                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3forin.Token);
                    step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                    step.OpCode = loopstart;
                    expression.exprStepList.Add(step);

                    jumptostart.as3exprlist.Add(expression);
                    builder.buildStmt(env, jumptostart);
                }
                {
                    //结束标记
                    OpStep lbl_loopend = new OpStep(OpCode.flag, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                    lbl_loopend.flag = loopend;
                    env.block.opSteps.Add(lbl_loopend);
                }
                {
                    OpStep opClose = new OpStep(OpCode.enumerator_close, new SourceToken(as3forin.Token.line, as3forin.Token.ptr, as3forin.Token.sourceFile));
                    opClose.reg = null;
                    opClose.regType = RunTimeDataType.unknown;
                    opClose.arg1 = regSaveEnumerator;
                    opClose.arg1Type = RunTimeDataType.rt_void;
                    opClose.arg2 = null;
                    opClose.arg2Type = RunTimeDataType.rt_void;

                    env.block.opSteps.Add(opClose);

                }

            }
        }

        public void buildAS3ForEach(CompileEnv env, ASTool.AS3.AS3ForEach as3foreach, Builder builder)
        {
            if (!string.IsNullOrEmpty(as3foreach.label))
            {
                //**包装一个block**
                ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3foreach.Token);
                tempblock.CodeList = new List<ASTool.AS3.IAS3Stmt>();
                tempblock.CodeList.Add(as3foreach);
                tempblock.label = as3foreach.label;
                as3foreach.label = null;


                OpStep lbl_forlabel = new OpStep(OpCode.flag, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                lbl_forlabel.flag = "LOOP_LABEL_START_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel);

                builder.buildStmt(env, tempblock);


                OpStep lbl_forlabel_end = new OpStep(OpCode.flag, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                lbl_forlabel_end.flag = "LOOP_LABEL_END_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel_end);
            }
            else
            {
                int lblid = env.getLabelId();

                builder.buildStmt(env, as3foreach.ForArg);

                ILeftValue varvalue = null;

                if (as3foreach.ForArg is ASTool.AS3.AS3StmtExpressions)
                {
                    ASTool.AS3.AS3StmtExpressions exprs = (ASTool.AS3.AS3StmtExpressions)as3foreach.ForArg;
                    if (exprs.as3exprlist.Count != 1)
                    {
                        throw new BuildException(
                            as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile, "Syntax error: invalid for-in initializer, only 1 expression expected.");
                    }

                    ASTool.AS3.AS3Expression expr = exprs.as3exprlist[0];
                    varvalue = (ILeftValue)ExpressionBuilder.getRightValue(env, expr.Value, expr.token, builder);
                }
                else if (as3foreach.ForArg is ASTool.AS3.AS3Variable)
                {
                    ASTool.AS3.AS3Variable v = (ASTool.AS3.AS3Variable)as3foreach.ForArg;
                    varvalue = (VariableBase)MemberFinder.find(v.Name, env, false, builder, v.token);
                }
                else
                {
                    throw new BuildException(
                            as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile, "异常错误，获得的变量类型不正确");
                }

                builder.buildExpression(env, as3foreach.ForExpr);
                IRightValue enumerator = ExpressionBuilder.getRightValue(env, as3foreach.ForExpr.Value, as3foreach.ForExpr.token, builder);

                if (enumerator.valueType == RunTimeDataType.rt_void
                    ||
                    enumerator.valueType < RunTimeDataType.unknown
                    )
                {
                    enumerator = ExpressionBuilder.addCastOpStep(env, enumerator, RunTimeDataType._OBJECT,
                        new SourceToken(as3foreach.ForExpr.token.line,
                         as3foreach.ForExpr.token.ptr,
                          as3foreach.ForExpr.token.sourceFile)
                        ,
                        builder);
                }

                Register regSaveEnumerator = env.getAdditionalRegister();
                regSaveEnumerator.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                {
                    //**IEnumerator Get And Reset();
                    OpStep opGetEnumerator = new OpStep(OpCode.foreach_get_enumerator,
                        new SourceToken(as3foreach.Token.line,
                         as3foreach.Token.ptr,
                          as3foreach.Token.sourceFile)
                        );

                    opGetEnumerator.reg = regSaveEnumerator;
                    opGetEnumerator.regType = RunTimeDataType.rt_void;
                    opGetEnumerator.arg1 = enumerator;
                    opGetEnumerator.arg1Type = enumerator.valueType;

                    env.block.opSteps.Add(opGetEnumerator);
                }

                string loopstart = "LOOP_START_" + lblid;
                string loopbody = "LOOP_BODY_" + lblid;
                string loopcontinue = "LOOP_CONTINUE_" + lblid;
                string loopend = "LOOP_END_" + lblid;

                {
                    OpStep lbl_loopstart = new OpStep(OpCode.flag, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                    lbl_loopstart.flag = loopstart;
                    env.block.opSteps.Add(lbl_loopstart);
                }

                Register eaxismovenext = env.getAdditionalRegister();
                eaxismovenext.setEAXTypeWhenCompile(RunTimeDataType.rt_boolean);
                {
                    //**IEnumerator MoveNext()
                    OpStep opMoveNext = new OpStep(OpCode.enumerator_movenext,
                        new SourceToken(as3foreach.Token.line,
                         as3foreach.Token.ptr,
                          as3foreach.Token.sourceFile)
                        );

                    opMoveNext.reg = eaxismovenext;
                    opMoveNext.regType = RunTimeDataType.rt_boolean;
                    opMoveNext.arg1 = regSaveEnumerator;
                    opMoveNext.arg1Type = regSaveEnumerator.valueType;

                    env.block.opSteps.Add(opMoveNext);
                }

                {
                    OpStep op = new OpStep(OpCode.if_jmp, new SourceToken(as3foreach.ForExpr.token.line,
                        as3foreach.ForExpr.token.ptr, as3foreach.ForExpr.token.sourceFile));
                    op.reg = null;
                    op.regType = RunTimeDataType.unknown;
                    op.arg1 = eaxismovenext;
                    op.arg1Type = eaxismovenext.valueType;
                    op.arg2 = new ASBinCode.rtData.RightValue(
                        new ASBinCode.rtData.rtString(loopbody));
                    op.arg2Type = RunTimeDataType.rt_string;

                    env.block.opSteps.Add(op);
                }
                {
                    ASTool.AS3.AS3StmtExpressions jumptoend = new ASTool.AS3.AS3StmtExpressions(as3foreach.Token);
                    jumptoend.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3foreach.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3foreach.Token);
                    step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                    step.OpCode = loopend;
                    expression.exprStepList.Add(step);

                    jumptoend.as3exprlist.Add(expression);
                    builder.buildStmt(env, jumptoend);
                }
                {
                    OpStep lbl_loopbody = new OpStep(OpCode.flag, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                    lbl_loopbody.flag = loopbody;
                    env.block.opSteps.Add(lbl_loopbody);
                }

                {
                    IRightValue loadValue = env.getAdditionalRegister();
                    ((Register)loadValue).setEAXTypeWhenCompile(RunTimeDataType.rt_void);

                    //**IEnumerator Current()
                    OpStep opCurrent = new OpStep(OpCode.enumerator_current,
                        new SourceToken(as3foreach.Token.line,
                         as3foreach.Token.ptr,
                          as3foreach.Token.sourceFile)
                        );

                    opCurrent.reg = (Register)loadValue;
                    opCurrent.regType = RunTimeDataType.rt_void;
                    opCurrent.arg1 = regSaveEnumerator;
                    opCurrent.arg1Type = regSaveEnumerator.valueType;

                    env.block.opSteps.Add(opCurrent);

                    if (varvalue.valueType != RunTimeDataType.rt_void)
                    {
                        loadValue = ExpressionBuilder.addCastOpStep(env, loadValue, varvalue.valueType,
                            new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile)
                            ,
                            builder
                            );
                    }

                    var op = new OpStep(OpCode.assigning, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                    op.reg = varvalue;
                    op.regType = varvalue.valueType;
                    op.arg1 = loadValue;
                    op.arg1Type = loadValue.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);
                }
                if (as3foreach.Body != null)
                {
                    for (int i = 0; i < as3foreach.Body.Count; i++)
                    {
                        builder.buildStmt(env, as3foreach.Body[i]);
                    }
                }
                {
                    OpStep lbl_loopcontinue = new OpStep(OpCode.flag, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                    lbl_loopcontinue.flag = loopcontinue;
                    env.block.opSteps.Add(lbl_loopcontinue);
                }
                {
                    //强行跳回开始
                    ASTool.AS3.AS3StmtExpressions jumptostart = new ASTool.AS3.AS3StmtExpressions(as3foreach.Token);
                    jumptostart.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3foreach.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3foreach.Token);
                    step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                    step.OpCode = loopstart;
                    expression.exprStepList.Add(step);

                    jumptostart.as3exprlist.Add(expression);
                    builder.buildStmt(env, jumptostart);
                }
                {
                    //结束标记
                    OpStep lbl_loopend = new OpStep(OpCode.flag, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                    lbl_loopend.flag = loopend;
                    env.block.opSteps.Add(lbl_loopend);
                }
                {
                    OpStep opClose = new OpStep(OpCode.enumerator_close, new SourceToken(as3foreach.Token.line, as3foreach.Token.ptr, as3foreach.Token.sourceFile));
                    opClose.reg = null;
                    opClose.regType = RunTimeDataType.unknown;
                    opClose.arg1 = regSaveEnumerator;
                    opClose.arg1Type = RunTimeDataType.rt_void;
                    opClose.arg2 = null;
                    opClose.arg2Type = RunTimeDataType.rt_void;

                    env.block.opSteps.Add(opClose);

                }

            }
        }

        public void buildAS3While(CompileEnv env, ASTool.AS3.AS3While as3while, Builder builder)
        {
            if (!string.IsNullOrEmpty(as3while.label))
            {
                //**包装一个block**
                ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3while.Token);
                tempblock.CodeList = new List<ASTool.AS3.IAS3Stmt>();
                tempblock.CodeList.Add(as3while);
                tempblock.label = as3while.label;
                as3while.label = null;


                OpStep lbl_forlabel = new OpStep(OpCode.flag, new SourceToken(as3while.Token.line, as3while.Token.ptr, as3while.Token.sourceFile));
                lbl_forlabel.flag = "LOOP_LABEL_START_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel);

                builder.buildStmt(env, tempblock);


                OpStep lbl_forlabel_end = new OpStep(OpCode.flag, new SourceToken(as3while.Token.line, as3while.Token.ptr, as3while.Token.sourceFile));
                lbl_forlabel_end.flag = "LOOP_LABEL_END_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel_end);
            }
            else
            {
                int lblid = env.getLabelId();

                string loopstart = "LOOP_START_" + lblid;
                string loopbody = "LOOP_BODY_" + lblid;
                string loopcontinue = "LOOP_CONTINUE_" + lblid;
                string loopend = "LOOP_END_" + lblid;

                //LOOP_START
                //***各种条件计算***
                //***if true goto LOOP_BODY
                //***goto LOOP_END
                //LOOP_BODY
                //****body***
                //LOOP_CONTINUE
                //****goto LOOP_START
                //LOOP_END

                {
                    OpStep lbl_loopstart = new OpStep(OpCode.flag, new SourceToken(as3while.Token.line, as3while.Token.ptr, as3while.Token.sourceFile));
                    lbl_loopstart.flag = loopstart;
                    env.block.opSteps.Add(lbl_loopstart);
                }
                {
                    builder.buildStmt(env, as3while.Condition);
                    ASTool.AS3.AS3Expression expr =
                        as3while.Condition.as3exprlist[as3while.Condition.as3exprlist.Count - 1];
                    {
                        ASTool.AS3.AS3StmtExpressions conditionsJump =
                            new ASTool.AS3.AS3StmtExpressions(as3while.Condition.Token);
                        ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3while.Condition.Token);
                        expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                        ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3while.Condition.Token);
                        step.Type = ASTool.AS3.Expr.OpType.IF_GotoFlag;
                        step.OpCode = loopbody;
                        step.Arg1 = expr.Value;
                        expression.exprStepList.Add(step);

                        conditionsJump.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                        conditionsJump.as3exprlist.Add(expression);
                        builder.buildStmt(env, conditionsJump);
                    }
                    {
                        ASTool.AS3.AS3StmtExpressions jumptoend = new ASTool.AS3.AS3StmtExpressions(as3while.Condition.Token);
                        jumptoend.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                        ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3while.Condition.Token);
                        expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                        ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3while.Condition.Token);
                        step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                        step.OpCode = loopend;
                        expression.exprStepList.Add(step);

                        jumptoend.as3exprlist.Add(expression);
                        builder.buildStmt(env, jumptoend);
                    }
                }
                {
                    OpStep lbl_loopbody = new OpStep(OpCode.flag, new SourceToken(as3while.Token.line, as3while.Token.ptr, as3while.Token.sourceFile));
                    lbl_loopbody.flag = loopbody;
                    env.block.opSteps.Add(lbl_loopbody);
                }
                if (as3while.Body != null)
                {
                    for (int i = 0; i < as3while.Body.Count; i++)
                    {
                        builder.buildStmt(env, as3while.Body[i]);
                    }
                }
                {
                    OpStep lbl_loopcontinue = new OpStep(OpCode.flag, new SourceToken(as3while.Token.line, as3while.Token.ptr, as3while.Token.sourceFile));
                    lbl_loopcontinue.flag = loopcontinue;
                    env.block.opSteps.Add(lbl_loopcontinue);
                }
                {
                    //强行跳回开始
                    ASTool.AS3.AS3StmtExpressions jumptostart = new ASTool.AS3.AS3StmtExpressions(as3while.Token);
                    jumptostart.as3exprlist = new List<ASTool.AS3.AS3Expression>();

                    ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3while.Token);
                    expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                    ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3while.Token);
                    step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
                    step.OpCode = loopstart;
                    expression.exprStepList.Add(step);

                    jumptostart.as3exprlist.Add(expression);
                    builder.buildStmt(env, jumptostart);
                }
                {
                    //结束标记
                    OpStep lbl_loopend = new OpStep(OpCode.flag, new SourceToken(as3while.Token.line, as3while.Token.ptr, as3while.Token.sourceFile));
                    lbl_loopend.flag = loopend;
                    env.block.opSteps.Add(lbl_loopend);
                }

            }
        }


        public void buildAS3DoWhile(CompileEnv env, ASTool.AS3.AS3DoWhile as3dowhile, Builder builder)
        {
            if (!string.IsNullOrEmpty(as3dowhile.label))
            {
                //**包装一个block**
                ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3dowhile.Token);
                tempblock.CodeList = new List<ASTool.AS3.IAS3Stmt>();
                tempblock.CodeList.Add(as3dowhile);
                tempblock.label = as3dowhile.label;
                as3dowhile.label = null;


                OpStep lbl_forlabel = new OpStep(OpCode.flag, new SourceToken(as3dowhile.Token.line, as3dowhile.Token.ptr, as3dowhile.Token.sourceFile));
                lbl_forlabel.flag = "LOOP_LABEL_START_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel);

                builder.buildStmt(env, tempblock);


                OpStep lbl_forlabel_end = new OpStep(OpCode.flag, new SourceToken(as3dowhile.Token.line, as3dowhile.Token.ptr, as3dowhile.Token.sourceFile));
                lbl_forlabel_end.flag = "LOOP_LABEL_END_" + tempblock.label;
                env.block.opSteps.Add(lbl_forlabel_end);
            }
            else
            {
                int lblid = env.getLabelId();

                string loopstart = "LOOP_START_" + lblid;
                string loopbody = "LOOP_BODY_" + lblid;
                string loopcontinue = "LOOP_CONTINUE_" + lblid;
                string loopend = "LOOP_END_" + lblid;

                //LOOP_START
                //LOOP_BODY
                //***body***
                //LOOP_CONTINUE
                //***各种条件计算***
                //***if true goto LOOP_START
                //***LOOP_END

                {
                    OpStep lbl_loopstart = new OpStep(OpCode.flag, new SourceToken(as3dowhile.Token.line, as3dowhile.Token.ptr, as3dowhile.Token.sourceFile));
                    lbl_loopstart.flag = loopstart;
                    env.block.opSteps.Add(lbl_loopstart);
                }
                {
                    OpStep lbl_loopbody = new OpStep(OpCode.flag, new SourceToken(as3dowhile.Token.line, as3dowhile.Token.ptr, as3dowhile.Token.sourceFile));
                    lbl_loopbody.flag = loopbody;
                    env.block.opSteps.Add(lbl_loopbody);
                }
                if (as3dowhile.Body != null)
                {
                    for (int i = 0; i < as3dowhile.Body.Count; i++)
                    {
                        builder.buildStmt(env, as3dowhile.Body[i]);
                    }
                }
                {
                    OpStep lbl_loopcontinue = new OpStep(OpCode.flag, new SourceToken(as3dowhile.Token.line, as3dowhile.Token.ptr, as3dowhile.Token.sourceFile));
                    lbl_loopcontinue.flag = loopcontinue;
                    env.block.opSteps.Add(lbl_loopcontinue);
                }
                {
                    builder.buildStmt(env, as3dowhile.Condition);
                    ASTool.AS3.AS3Expression expr =
                        as3dowhile.Condition.as3exprlist[as3dowhile.Condition.as3exprlist.Count - 1];
                    {
                        ASTool.AS3.AS3StmtExpressions conditionsJump =
                            new ASTool.AS3.AS3StmtExpressions(as3dowhile.Condition.Token);
                        ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3dowhile.Condition.Token);
                        expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

                        ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3dowhile.Condition.Token);
                        step.Type = ASTool.AS3.Expr.OpType.IF_GotoFlag;
                        step.OpCode = loopbody;
                        step.Arg1 = expr.Value;
                        expression.exprStepList.Add(step);

                        conditionsJump.as3exprlist = new List<ASTool.AS3.AS3Expression>();
                        conditionsJump.as3exprlist.Add(expression);
                        builder.buildStmt(env, conditionsJump);
                    }
                    
                }
                {
                    //结束标记
                    OpStep lbl_loopend = new OpStep(OpCode.flag, new SourceToken(as3dowhile.Token.line, as3dowhile.Token.ptr, as3dowhile.Token.sourceFile));
                    lbl_loopend.flag = loopend;
                    env.block.opSteps.Add(lbl_loopend);
                }
            }
        }
    }
}
