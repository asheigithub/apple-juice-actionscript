using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASCompiler.compiler
{
    public class Builder
    {
        public bool isConsoleOut = true;


        public List<BuildError> buildErrors=new List<BuildError>();

        private void pushBuildError(BuildError err)
        {
            buildErrors.Add(err);

            if (isConsoleOut)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("file :" + err.srcFile);
                Console.WriteLine("line :" + err.line + " ptr :" + err.ptr);
                Console.WriteLine(err.errorMsg);

                Console.ResetColor();
            }

            if (buildErrors.Count > 10)
            {
                throw new InvalidOperationException();
            }

        }


        public List<CodeBlock> blocks = new List<CodeBlock>();
        public void Build(ASTool.AS3.AS3Proj proj)
        {
            buildErrors.Clear();

            try
            {
                for (int i = 0; i < proj.SrcFiles.Count; i++)
                {
                    ASTool.AS3.AS3SrcFile srcfile = proj.SrcFiles[i];

                    buildFile(srcfile);

                }

                if (isConsoleOut)
                {
                    Console.WriteLine("编译结束");
                }
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
            catch (InvalidOperationException)
            {
                if (isConsoleOut)
                {
                    Console.WriteLine("编译已终止");
                }
            }

            
        }

        private void buildFile(ASTool.AS3.AS3SrcFile srcfile)
        {
            //***分析包外代码***
            List<ASTool.AS3.IAS3Stmt> outstmts = srcfile.OutPackagePrivateScope.StamentsStack.Peek();
            ASBinCode.CodeBlock block = new ASBinCode.CodeBlock();
            block.scope = new ASBinCode.scopes.OutPackageMemberScope();
            
            CompileEnv env = new CompileEnv(block);
            //先提取变量定义
            for (int i = 0; i < outstmts.Count; i++)
            {
                buildVariables(env, outstmts[i]);
            }

            
            for (int i = 0; i < outstmts.Count; i++)
            {
                buildStmt(env, outstmts[i]);
            }
            env.completSteps();
            block.totalRegisters = env.combieRegisters();



            if (buildErrors.Count == 0)
            {
                blocks.Add(block);
            }
        }

        /// <summary>
        /// 先提取代码段中的所有变量定义
        /// </summary>
        /// <param name="env"></param>
        /// <param name="stmt"></param>
        private void buildVariables(CompileEnv env, ASTool.AS3.IAS3Stmt stmt)
        {
            try
            {
                if (stmt is ASTool.AS3.AS3Block)
                {
                    ASTool.AS3.AS3Block b = (ASTool.AS3.AS3Block)stmt;
                    for (int i = 0; i < b.CodeList.Count; i++)
                    {
                        buildVariables(env, b.CodeList[i]);
                    }
                }
                else if (stmt is ASTool.AS3.AS3IF)
                {
                    ASTool.AS3.AS3IF as3if = (ASTool.AS3.AS3IF)stmt;

                    buildVariables(env, as3if.Condition);
                    if (as3if.TruePass != null)
                    {
                        for (int i = 0; i < as3if.TruePass.Count; i++)
                        {
                            buildVariables(env, as3if.TruePass[i]);
                        }
                    }
                    if (as3if.FalsePass != null)
                    {
                        for (int i = 0; i < as3if.FalsePass.Count; i++)
                        {
                            buildVariables(env, as3if.FalsePass[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3For)
                {
                    ASTool.AS3.AS3For as3for = (ASTool.AS3.AS3For)stmt;
                    if (as3for.Part2 != null)
                    {
                        buildVariables(env, as3for.Part2);
                    }
                    if (as3for.Part3 != null)
                    {
                        buildVariables(env, as3for.Part3);
                    }
                    if (as3for.Body != null)
                    {
                        for (int i = 0; i < as3for.Body.Count; i++)
                        {
                            buildVariables(env, as3for.Body[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3While)
                {
                    ASTool.AS3.AS3While as3while = (ASTool.AS3.AS3While)stmt;

                    if (as3while.Condition != null)
                    {
                        buildVariables(env, as3while.Condition);
                    }
                    if (as3while.Body != null)
                    {
                        for (int i = 0; i < as3while.Body.Count; i++)
                        {
                            buildVariables(env, as3while.Body[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3DoWhile)
                {
                    ASTool.AS3.AS3DoWhile as3dowhile = (ASTool.AS3.AS3DoWhile)stmt;
                    if (as3dowhile.Body != null)
                    {
                        for (int i = 0; i < as3dowhile.Body.Count; i++)
                        {
                            buildVariables(env, as3dowhile.Body[i]);
                        }
                    }
                    if (as3dowhile.Condition != null)
                    {
                        buildVariables(env, as3dowhile.Condition);
                    }
                }
                else if (stmt is ASTool.AS3.AS3Switch)
                {
                    ASTool.AS3.AS3Switch as3switch = (ASTool.AS3.AS3Switch)stmt;
                    for (int i = 0; i < as3switch.CaseList.Count; i++)
                    {
                        var c = as3switch.CaseList[i];
                        if (c.Body != null)
                        {
                            for (int j = 0; j < c.Body.Count; j++)
                            {
                                buildVariables(env, c.Body[j]);
                            }

                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3Variable)
                {
                    ASTool.AS3.AS3Variable variable = (ASTool.AS3.AS3Variable)stmt;

                    for (int i = 0; i < env.block.scope.members.Count; i++) //scope内查找是否有重复
                    {
                        IMember member = env.block.scope.members[i];
                        if (member.name == variable.Name)
                        {
                            if (!(member is Variable))
                            {
                                pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "重复声明 " + variable.Name));
                                return;
                            }

                            Variable var = (Variable)member;

                            var newtype = TypeReader.fromSourceCodeStr(variable.TypeStr, env, stmt.Token);

                            if (newtype == var.valueType)
                            {
                                //和原类型完全相同
                                return;
                            }
                            if (newtype != RunTimeDataType.rt_void && var.valueType != RunTimeDataType.rt_void
                                )
                            {
                                pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "类型定义不明确 " + variable.Name));
                                return;
                            }
                            else
                            {
                                if (var.valueType == RunTimeDataType.rt_void)
                                {
                                    //原类型是任意类型
                                    return;
                                }
                                else
                                {
                                    //新类型为*

                                    if (variable.ValueExpr != null)
                                    {
                                        //读取测试

                                        CompileEnv tempEnv = new CompileEnv(new CodeBlock());
                                        buildExpression(tempEnv, variable.ValueExpr);
                                        IRightValue tempRv = builds.ExpressionBuilder.getRightValue(env, variable.ValueExpr.Value,
                                            stmt.Token
                                            );
                                        newtype = tempRv.valueType;
                                    }

                                    if (!ASRuntime.TypeConverter.testImplicitConvert(newtype, var.valueType))
                                    {
                                        pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                        "不能将[" + newtype + "]类型赋值给[" + var.valueType + "]类型的变量"));
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }

                                }


                            }

                        }
                    }

                    {
                        Variable rtVariable = new Variable(variable.Name, env.block.scope.members.Count);
                        env.block.scope.members.Add(rtVariable);

                        //ASBinCode.IRightValue defaultv = null;


                        try
                        {

                            rtVariable.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, env, stmt.Token);

                            if (variable.ValueExpr != null) //变量值表达式
                            {

                                //留到执行到时赋值
                            }
                            else
                            {
                                //赋默认值
                                //defaultv = ASRuntime.TypeConverter.getDefaultValue(rtVariable.valueType);
                                //{
                                //    OpStep step = new OpStep(OpCode.assigning, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                                //    step.reg = rtVariable;
                                //    step.regType = rtVariable.valueType;
                                //    step.arg1 = defaultv;
                                //    step.arg1Type = defaultv.valueType;
                                //    step.arg2 = null;
                                //    step.arg2Type = RunTimeDataType.unknown;

                                //    env.block.opSteps.Add(step);
                                //}
                                //***默认值在运行时初始化环境时自动赋值，因此此处省略代码
                            }

                        }
                        catch (BuildException ex)
                        {
                            pushBuildError(ex.error);
                        }

                    }

                }
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
        }


        

        internal void buildStmt(CompileEnv env,ASTool.AS3.IAS3Stmt stmt)
        {
            try
            {

                if (stmt is ASTool.AS3.AS3Block)
                {
                    ASTool.AS3.AS3Block b = (ASTool.AS3.AS3Block)stmt;

                    string lbl = b.label;
                    int stlen = env.block.opSteps.Count;

                    if (!string.IsNullOrEmpty(lbl))
                    {
                        //***插入Label开始**
                        OpStep opLblSt = new OpStep(OpCode.flag, new SourceToken(b.Token.line, b.Token.ptr, b.Token.sourceFile));
                        opLblSt.flag = "Label_Start_" + b.label;
                        env.block.opSteps.Add(opLblSt);
                    }

                    for (int i = 0; i < b.CodeList.Count; i++)
                    {
                        buildStmt(env, b.CodeList[i]);
                    }

                    if (!string.IsNullOrEmpty(lbl))
                    {
                        //***插入Label结束**
                        OpStep opLblSt = new OpStep(OpCode.flag, new SourceToken(b.Token.line, b.Token.ptr, b.Token.sourceFile));
                        opLblSt.flag = "Label_End_" + b.label;
                        env.block.opSteps.Add(opLblSt);

                        for (int i = stlen; i < env.block.opSteps.Count; i++)
                        {
                            //加入块标记
                            var step = env.block.opSteps[i];
                            if (step.labels == null)
                            {
                                step.labels = new Stack<string>();
                            }

                            if (step.labels.Contains(lbl))
                            {
                                throw new BuildException(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                        "重复定义标签块: [" + lbl + "]");
                            }
                            step.labels.Push(lbl);
                        }
                    }

                }
                else if (stmt is ASTool.AS3.AS3Break)
                {
                    ASTool.AS3.AS3Break as3Break = (ASTool.AS3.AS3Break)stmt;
                    builds.AS3BreakBuilder builder = new builds.AS3BreakBuilder();
                    builder.buildAS3Break(env, as3Break);

                }
                else if (stmt is ASTool.AS3.AS3Continue)
                {
                    builds.AS3ContinueBuilder builder = new builds.AS3ContinueBuilder();
                    builder.buildAS3Continue(env, (ASTool.AS3.AS3Continue)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3IF)
                {
                    ASTool.AS3.AS3IF as3if = (ASTool.AS3.AS3IF)stmt;
                    builds.AS3IFBuilder builder = new builds.AS3IFBuilder();
                    builder.buildAS3IF(env, as3if, this);
                }
                else if (stmt is ASTool.AS3.AS3Switch)
                {
                    builds.AS3SwitchBuilder builder = new builds.AS3SwitchBuilder();
                    builder.buildAS3Switch(env, (ASTool.AS3.AS3Switch)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3For)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3For(env, (ASTool.AS3.AS3For)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3While)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3While(env, (ASTool.AS3.AS3While)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3DoWhile)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3DoWhile(env, (ASTool.AS3.AS3DoWhile)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3Throw)
                {
                    builds.AS3ThrowBuilder builder = new builds.AS3ThrowBuilder();
                    builder.buildAS3Throw(env, (ASTool.AS3.AS3Throw)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3StmtExpressions)
                {
                    ASTool.AS3.AS3StmtExpressions expressions = (ASTool.AS3.AS3StmtExpressions)stmt;


                    for (int i = 0; i < expressions.as3exprlist.Count; i++)
                    {
                        buildExpression(env, expressions.as3exprlist[i]);
                    }


                }
                else if (stmt is ASTool.AS3.AS3Variable)
                {
                    ASTool.AS3.AS3Variable variable = (ASTool.AS3.AS3Variable)stmt;

                    //if ( MemberFinder.find(variable.Name,env ) !=null )//env.tempVariable.ContainsKey(variable.Name))
                    //{
                    //    pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile, 
                    //        "重复声明 " + variable.Name ));
                    //}
                    //else
                    {
                        //Variable rtVariable = new Variable(variable.Name , env.block.scope.members.Count);
                        //env.block.scope.members.Add(rtVariable);

                        ASBinCode.IRightValue defaultv = null;


                        try
                        {
                            Variable rtVariable = (Variable)MemberFinder.find(variable.Name, env);
                            //rtVariable.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, env, stmt.Token);

                            if (variable.ValueExpr != null) //变量值表达式
                            {
                                var testEval = ExpressionEval.Eval(variable.ValueExpr);
                                if (testEval != null && variable.ValueExpr.Value.IsReg)
                                {
                                    defaultv = new ASBinCode.rtData.RightValue(testEval);
                                }
                                else
                                {
                                    buildExpression(env, variable.ValueExpr);
                                    defaultv = builds.ExpressionBuilder.getRightValue(env, variable.ValueExpr.Value,
                                        stmt.Token
                                        );
                                }

                                //**加入赋值操作***
                                //**隐式类型转换检查
                                if (!ASRuntime.TypeConverter.testImplicitConvert(defaultv.valueType, rtVariable.valueType))
                                {
                                    throw new BuildException(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                        "不能将[" + defaultv.valueType + "]类型赋值给[" + rtVariable.valueType + "]类型的变量");
                                }



                                if (defaultv.valueType != rtVariable.valueType)
                                {
                                    //插入类型转换代码
                                    defaultv =
                                        builds.ExpressionBuilder.addCastOpStep(
                                            env, defaultv, rtVariable.valueType,
                                            new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile)
                                            );

                                }

                                //赋初始值
                                {
                                    OpStep step = new OpStep(OpCode.assigning, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                                    step.reg = rtVariable;
                                    step.regType = rtVariable.valueType;
                                    step.arg1 = defaultv;
                                    step.arg1Type = defaultv.valueType;
                                    step.arg2 = null;
                                    step.arg2Type = RunTimeDataType.unknown;

                                    env.block.opSteps.Add(step);
                                }

                            }
                            //else
                            //{
                            //    //赋默认值  默认值在运行是自动初始化，所以此处省略
                            //    defaultv = ASRuntime.TypeConverter.getDefaultValue(rtVariable.valueType);
                            //    {
                            //        OpStep step = new OpStep(OpCode.assigning, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                            //        step.reg = rtVariable;
                            //        step.regType = rtVariable.valueType;
                            //        step.arg1 = defaultv;
                            //        step.arg1Type = defaultv.valueType;
                            //        step.arg2 = null;
                            //        step.arg2Type = RunTimeDataType.unknown;

                            //        env.block.opSteps.Add(step);
                            //    }

                            //}

                        }
                        catch (BuildException ex)
                        {
                            pushBuildError(ex.error);
                        }

                    }

                }
                else
                {
                    pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile, stmt.GetType().Name + "未实现"));

                }
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }

        }


        internal void buildExpression(CompileEnv env,ASTool.AS3.AS3Expression expression)
        {

            try
            {
                builds.ExpressionBuilder expressionbuilder =
                            new builds.ExpressionBuilder();

                expressionbuilder.buildAS3Expression(env, expression);
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
            
        }

    }
}
