using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class FuncCallBuilder
    {
        public void buildFuncCall(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step,Builder builder)
        {
            if (step.Arg2.IsReg || step.Arg2.Data.FF1Type== ASTool.AS3.Expr.FF1DataValueType.as3_function)
            {
                IRightValue rValue = ExpressionBuilder.getRightValue(env, step.Arg2, step.token, builder);

                OpStep op = new OpStep(OpCode.call_function,
                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                var eax = env.createASTRegister(step.Arg1.Reg.ID);
                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                op.reg = eax;
                op.regType = RunTimeDataType.rt_void;


                op.arg1 = rValue;
                op.arg1Type = RunTimeDataType.rt_function;

                List<ASTool.AS3.Expr.AS3DataStackElement> args
                    = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;

                {
                    OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    opMakeArgs.arg1 = rValue;
                    opMakeArgs.arg1Type = RunTimeDataType.rt_function;
                    env.block.opSteps.Add(opMakeArgs);
                }


                for (int i = 0; i < args.Count; i++)
                {
                    ASTool.AS3.Expr.AS3DataStackElement argData = args[i];
                    IRightValue arg = builds.ExpressionBuilder.getRightValue(env, argData, step.token, builder);
                    //***参数准备***
                    OpStep opPushArgs = new OpStep(OpCode.push_parameter, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    opPushArgs.arg1 = arg;
                    opPushArgs.arg1Type = arg.valueType;
                    opPushArgs.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(i));
                    opPushArgs.arg2Type = RunTimeDataType.rt_int;
                    env.block.opSteps.Add(opPushArgs);

                }

                env.block.opSteps.Add(op);

            }
            else if (step.Arg2.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
            {

                if (step.Arg2.Data.Value.ToString() == "trace")
                {
                    if (env.isEval)
                    {
                        return;
                    }

                    OpStep op = new OpStep(OpCode.native_trace,
                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    var eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                    op.reg = eax;
                    op.regType = RunTimeDataType.rt_void;


                    List<ASTool.AS3.Expr.AS3DataStackElement> args
                        = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;

                    if (args.Count > 1)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "trace函数目前只接受1个函数"));
                    }
                    else if (args.Count > 0)
                    {
                        op.arg1 = ExpressionBuilder.getRightValue(env, args[0], step.token, builder);
                        op.arg1Type = op.arg1.valueType;
                    }
                    else
                    {
                        op.arg1 = null;
                        op.arg1Type = RunTimeDataType.unknown;
                    }
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);
                }
                else
                {
                    string name = step.Arg2.Data.Value.ToString();

                    IMember member = MemberFinder.find(name, env,false);
                    if (member == null)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员" + name + "没有找到"
                            );
                    }

                    IRightValue rFunc = (IRightValue)member;
                    if (ASRuntime.TypeConverter.testImplicitConvert( rFunc.valueType, RunTimeDataType.rt_function, builder))
                    {
                        OpStep op = new OpStep(OpCode.call_function,
                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                        var eax = env.createASTRegister(step.Arg1.Reg.ID);
                        eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                        op.reg = eax;
                        op.regType = RunTimeDataType.rt_void;


                        ASBinCode.rtti.FunctionSignature signature = null;

                        int blockid = env.block.id;
                        if (rFunc is Variable)
                        {
                            blockid = ((Variable)rFunc).refdefinedinblockid;
                        }

                        if (builder.dictSignatures.ContainsKey(blockid))
                        {
                            if (builder.dictSignatures[blockid].ContainsKey((Variable)rFunc))
                            {
                                
                                signature =
                                     builder.dictSignatures[blockid][(Variable)rFunc];
                                var returnvalueType = signature.returnType;

                                op.regType = RunTimeDataType.rt_void;
                                eax.setEAXTypeWhenCompile(returnvalueType);
                            }
                        }


                        op.arg1 = rFunc;
                        op.arg1Type = RunTimeDataType.rt_function;

                        List<ASTool.AS3.Expr.AS3DataStackElement> args
                            = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;

                        createParaOp(args, signature, step, env, rFunc, builder,false,null);

                        env.block.opSteps.Add(op);



                    }
                    else
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员" + name + "不是一个function"
                            );
                    }


                }
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "目前只实现了trace函数"));
            }
        }

        public void createParaOp(List<ASTool.AS3.Expr.AS3DataStackElement> args,
            ASBinCode.rtti.FunctionSignature signature, 
            ASTool.AS3.Expr.AS3ExprStep step, CompileEnv env,
            IRightValue rFunc,Builder builder,bool isConstructor,ASBinCode.rtti.Class cls
            )
        {
            
            if (signature != null)
            {
                if (args.Count < signature.parameters.Count)
                {
                    if (signature.parameters[args.Count].defaultValue == null)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "参数数量不足,需要" + signature.parameters.Count 
                            );
                    }
                }
                else if (args.Count > signature.parameters.Count)
                {
                    if (! (signature.parameters.Count >0 
                        && signature.parameters[signature.parameters.Count - 1].isPara))
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "参数数量过多,期望" + signature.parameters.Count
                            );
                    }
                }
            }

            if (!isConstructor)
            {
                OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                opMakeArgs.arg1 = rFunc;
                opMakeArgs.arg1Type = RunTimeDataType.rt_function;
                env.block.opSteps.Add(opMakeArgs);
            }
            else
            {
                
                OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_argement, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                opMakeArgs.arg1 = new ASBinCode.rtData.RightValue( new ASBinCode.rtData.rtInt( cls.classid));
                opMakeArgs.arg1Type = RunTimeDataType.rt_int;
                env.block.opSteps.Add(opMakeArgs);
                
            }


            for (int i = 0; i < args.Count; i++)
            {
                ASTool.AS3.Expr.AS3DataStackElement argData = args[i];
                IRightValue arg = builds.ExpressionBuilder.getRightValue(env, argData, step.token, builder);

                if (signature != null) //参数类型检查
                {
                    ASBinCode.rtti.FunctionParameter para = signature.parameters[i];
                    if (para.isPara)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "... (rest) parameter 未实现"
                            );
                    }

                    if (!ASRuntime.TypeConverter.testImplicitConvert(arg.valueType, para.type, builder))
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "参数类型不匹配，不能将" + arg.valueType + "转换成" + para.type
                            );
                    }

                    if (arg.valueType != para.type)
                    {
                        arg = builds.ExpressionBuilder.addCastOpStep(env, arg, para.type,
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder);
                    }
                }

                //***参数准备***
                OpStep opPushArgs = new OpStep(
                    isConstructor?
                    OpCode.push_parameter_class 
                    :
                    OpCode.push_parameter
                    
                    , new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                opPushArgs.arg1 = arg;
                opPushArgs.arg1Type = arg.valueType;
                opPushArgs.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(i));
                opPushArgs.arg2Type = RunTimeDataType.rt_int;
                env.block.opSteps.Add(opPushArgs);
                
            }
        }


    }
}
