using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3TryBuilder
    {
        private void jumpTo(CompileEnv env, ASTool.AS3.AS3Try as3try, Builder builder,string label)
        {
            ASTool.AS3.AS3StmtExpressions jumptofinally = new ASTool.AS3.AS3StmtExpressions(as3try.Token);
            jumptofinally.as3exprlist = new List<ASTool.AS3.AS3Expression>();

            ASTool.AS3.AS3Expression expression = new ASTool.AS3.AS3Expression(as3try.Token);
            expression.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();

            ASTool.AS3.Expr.AS3ExprStep step = new ASTool.AS3.Expr.AS3ExprStep(as3try.Token);
            step.Type = ASTool.AS3.Expr.OpType.GotoFlag;
            step.OpCode = label;
            expression.exprStepList.Add(step);

            jumptofinally.as3exprlist.Add(expression);
            builder.buildStmt(env, jumptofinally);
        }

        public void buildCatchVariable(CompileEnv env, ASTool.AS3.AS3Catch c,int i,int tryid, Builder builder)
        {
            //Variable rtVariable = 
            //    new Variable( "0"+ c.CatchVariable.Name + "_" + i + "_" + tryid

            //    , env.block.scope.members.Count);

            //rtVariable.valueType = TypeReader.fromSourceCodeStr(c.CatchVariable.TypeStr, env, c.CatchVariable.token);
            //env.block.scope.members.Add(rtVariable);
        }


        //private int trydepth;
        public void buildAS3Try(CompileEnv env, ASTool.AS3.AS3Try as3try,Builder builder)
        {
            //***先编译Try块
            //trydepth++;
            int tryid = as3try.holdTryId;

            //string LBL_BEGIN_TRY = "BEGIN_TRY_" + tryid;
            //string LBL_END_TRY = "END_TRY_" + tryid;
            string LBL_BEGIN_FINALLY = "BEGIN_FINALLY_" + tryid;
            string LBL_END_FINALLY = "END_FINALLY_" + tryid;

            string LBL_CATCH = "BEGIN_CATCH_" + tryid;
            
            //OpStep begintry = new OpStep(OpCode.flag, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
            //begintry.flag = LBL_BEGIN_TRY;
            //env.block.opSteps.Add(begintry);

            //**包装一个block**
            ASTool.AS3.AS3Block tempblock = new ASTool.AS3.AS3Block(as3try.Token);
            tempblock.CodeList = as3try.TryBlock;
            tempblock.label = as3try.label;

            {
                OpStep enter_try = new OpStep(OpCode.enter_try, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
                enter_try.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(tryid));
                enter_try.arg1Type = RunTimeDataType.rt_int;
                env.block.opSteps.Add(enter_try);
            }


            builder.buildStmt(env, tempblock);

            {
                OpStep quit_try = new OpStep(OpCode.quit_try, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
                quit_try.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(tryid));
                quit_try.arg1Type = RunTimeDataType.rt_int;
                env.block.opSteps.Add(quit_try);
            }

            //OpStep endtry= new OpStep(OpCode.flag, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
            //endtry.flag = LBL_END_TRY;
            //env.block.opSteps.Add(endtry);

            //***跳转到Finally块***
            {
                jumpTo(env,as3try,builder,LBL_BEGIN_FINALLY);
            }

            OpStep startCatch = new OpStep(OpCode.flag, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
            startCatch.flag = LBL_CATCH;
            env.block.opSteps.Add(startCatch);

            //***编译Catch块***
            for (int i = 0; i < as3try.CatchList.Count; i++)
            {
                var c = as3try.CatchList[i];
                //c.CatchVariable.Name = "0"+c.CatchVariable.Name + "_" + i;


                Variable rtVariable = new Variable(c.CatchVariable.Name, env.block.scope.members.Count,true);
                rtVariable.valueType = TypeReader.fromSourceCodeStr(c.CatchVariable.TypeStr, env, c.CatchVariable.token);
                env.block.scope.members.Add(rtVariable);

                //builder.buildVariables(env, c.CatchVariable);
                //string catchvariablename = "0" + c.CatchVariable.Name + "_" + i + "_" + tryid;
                //Variable rtVariable = (Variable)MemberFinder.find(catchvariablename, env);
                //rtVariable.resetName(c.CatchVariable.Name);

                //rtVariable.valueType = RunTimeDataType.rt_void;

                OpStep op = new OpStep(OpCode.catch_error, 
                    new SourceToken(c.CatchVariable.token.line,
                    c.CatchVariable.token.ptr, c.CatchVariable.token.sourceFile));
                op.reg = rtVariable;
                op.regType = TypeReader.fromSourceCodeStr( c.CatchVariable.TypeStr,env,c.CatchVariable.token  ) ;
                op.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(tryid));
                op.arg1Type = RunTimeDataType.rt_int;
                op.arg2 = null;
                op.arg2Type = RunTimeDataType.unknown;

                env.block.opSteps.Add(op);

                {
                    OpStep enter_catch = new OpStep(OpCode.enter_catch, new SourceToken(c.CatchVariable.token.line, c.CatchVariable.token.ptr, c.CatchVariable.token.sourceFile));
                    enter_catch.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(tryid));
                    enter_catch.arg1Type = RunTimeDataType.rt_int;
                    env.block.opSteps.Add(enter_catch);
                }


                for (int j = 0; j < c.CatchBlock.Count; j++)
                {
                    builder.buildStmt(env,c.CatchBlock[j]);
                }

                
                rtVariable.resetName( "0"+ rtVariable.name  + "_" +i+"_" + tryid );

                {
                    OpStep quit_catch = new OpStep(OpCode.quit_catch, new SourceToken(c.CatchVariable.token.line, c.CatchVariable.token.ptr, c.CatchVariable.token.sourceFile));
                    quit_catch.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(tryid));
                    quit_catch.arg1Type = RunTimeDataType.rt_int;
                    env.block.opSteps.Add(quit_catch);
                }

                {
                    jumpTo(env, as3try, builder, LBL_BEGIN_FINALLY);
                }
            }

            //FINALLY
            {
                OpStep finallylbl = new OpStep(OpCode.flag, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
                finallylbl.flag = LBL_BEGIN_FINALLY;
                env.block.opSteps.Add(finallylbl);

                {
                    OpStep enter_finally = new OpStep(OpCode.enter_finally, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
                    enter_finally.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(tryid));
                    enter_finally.arg1Type = RunTimeDataType.rt_int;
                    env.block.opSteps.Add(enter_finally);
                }

                if (as3try.FinallyBlock != null)
                {
                    for (int i = 0; i < as3try.FinallyBlock.Count ; i++)
                    {
                        builder.buildStmt(env,as3try.FinallyBlock[i]);
                    }
                }

                {
                    OpStep quit_finally = new OpStep(OpCode.quit_finally, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
                    quit_finally.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(tryid));
                    quit_finally.arg1Type = RunTimeDataType.rt_int;
                    env.block.opSteps.Add(quit_finally);
                }
                
                OpStep endfinally = new OpStep(OpCode.flag, new SourceToken(as3try.Token.line, as3try.Token.ptr, as3try.Token.sourceFile));
                endfinally.flag = LBL_END_FINALLY;
                env.block.opSteps.Add(endfinally);
            }

            //trydepth--;



            //throw new BuildException(as3try.Token.line,as3try.Token.ptr,as3try.Token.sourceFile,"try catch finally编译没实现");
        }
    }
}
