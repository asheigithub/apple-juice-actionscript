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

                if (rValue is Register)
                {
                    Register reg = (Register)rValue;
                    if (reg._regMember != null)
                    {
                        build_member_parameterSteps((IRightValue)reg._regMember.bindField,
                            builder, eax, op, step, env, null, rValue);

                        env.block.opSteps.Add(op);
                        return;
                    }

                }
                

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
                #region "查找@__buildin__"
                {
                    string name = step.Arg2.Data.Value.ToString();
                    
                        //***从__buildin__中查找
                        var buildin = TypeReader.findClassFromImports("@__buildin__", builder,step.token);
                        if (buildin.Count == 1)
                        {
                            if (env.isEval)
                            {
                                return;
                            }

                            var bi = buildin[0].staticClass;

                            var member = ClassMemberFinder.find(bi, name, bi);
                            if (member != null)
                            {
                                build_member(member.bindField, step, builder, env, bi, name);
                                return;
                            }

                        }
                    
                }
                #endregion
                //if (step.Arg2.Data.Value.ToString() == "trace")
                //{
                //    if (env.isEval)
                //    {
                //        return;
                //    }

                //    OpStep op = new OpStep(OpCode.native_trace,
                //        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                //    var eax = env.createASTRegister(step.Arg1.Reg.ID);
                //    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                //    op.reg = eax;
                //    op.regType = RunTimeDataType.rt_void;


                //    List<ASTool.AS3.Expr.AS3DataStackElement> args
                //        = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;

                //    if (args.Count > 1)
                //    {
                //        throw new BuildException(
                //            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                //            "trace函数目前只接受1个函数"));
                //    }
                //    else if (args.Count > 0)
                //    {
                //        op.arg1 = ExpressionBuilder.getRightValue(env, args[0], step.token, builder);
                //        op.arg1Type = op.arg1.valueType;
                //    }
                //    else
                //    {
                //        op.arg1 = null;
                //        op.arg1Type = RunTimeDataType.unknown;
                //    }
                //    op.arg2 = null;
                //    op.arg2Type = RunTimeDataType.unknown;

                //    env.block.opSteps.Add(op);
                //}
                //else
                {
                    string name = step.Arg2.Data.Value.ToString();

                    ASBinCode.rtti.Class _cls = null;

                    IMember member = MemberFinder.find(name, env,false,builder,step.token);

                    if (member == null
                        ||
                        (member is ClassMethodGetter
                        &&
                        ((ClassMethodGetter)member).classmember.isConstructor
                        )
                        
                        )     //检查是否强制类型转换
                    {
                        var found= TypeReader.findClassFromImports(name, builder,step.token);
                        if (found.Count == 1)   //说明为强制类型转换
                        {
                            ASBinCode.rtti.Class cls = found[0];
                            if (cls.staticClass != null)
                            {
                                cls = cls.staticClass;

                                List<ASTool.AS3.Expr.AS3DataStackElement> args
                                = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;
                                
                                RunTimeDataType targettype = cls.instanceClass.getRtType();
                                if (cls.implicit_from != null)      //将目标转换为原始对象
                                {
                                    //_cls = cls;
                                    //member = cls.implicit_from.bindField;
                                    //goto memberfinded;

                                    targettype = cls.implicit_from_type;
                                }

                                if (cls.explicit_from != null)
                                {
                                    _cls = cls;
                                    member = cls.explicit_from.bindField;
                                    goto memberfinded;
                                }
                               
                                {
                                    if (args.Count != 1)
                                    {
                                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                                                                    "强制类型转换" + name + "函数必须是1个参数"
                                                                    );
                                    }

                                    IRightValue src = ExpressionBuilder.getRightValue(env, args[0], step.token, builder);
                                    IRightValue ct= ExpressionBuilder.addCastOpStep(env, src, 
                                        targettype, //cls.instanceClass.getRtType(), 
                                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile), builder);

                                    OpStep op = new OpStep(OpCode.assigning,
                                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                                    var eax = env.createASTRegister(step.Arg1.Reg.ID);
                                    eax.setEAXTypeWhenCompile(ct.valueType);
                                    op.reg = eax;
                                    op.regType = ct.valueType;
                                    op.arg1 = ct;
                                    op.arg1Type = ct.valueType;
                                    op.arg2 = null;
                                    op.arg2Type = RunTimeDataType.unknown;

                                    env.block.opSteps.Add(op);

                                    return;
                                }
                            }
                        }
                    }

                memberfinded:

                    if (member == null)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员" + name + "没有找到"
                            );
                    }
                    
                    build_member(member, step, builder, env, _cls, name);


                }
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "目前只实现了trace函数"));
            }
        }






        private void build_member(IMember member, ASTool.AS3.Expr.AS3ExprStep step, Builder builder,CompileEnv env,
            ASBinCode.rtti.Class _cls , string name
            )
        {
            IRightValue rFunc = (IRightValue)member;
            if (ASRuntime.TypeConverter.testImplicitConvert(rFunc.valueType, RunTimeDataType.rt_function, builder))
            {
                OpStep op = new OpStep(OpCode.call_function,
                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                var eax = env.createASTRegister(step.Arg1.Reg.ID);
                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                op.reg = eax;
                op.regType = RunTimeDataType.rt_void;

                build_member_parameterSteps(rFunc, builder, eax, op, step, env, _cls,null);
                
                env.block.opSteps.Add(op);



            }
            else
            {
                throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                    "成员" + name + "不是一个function"
                    );
            }
        }

        private void build_member_parameterSteps(IRightValue rFunc,Builder builder,Register eax,OpStep op,
            ASTool.AS3.Expr.AS3ExprStep step,CompileEnv env,ASBinCode.rtti.Class _cls,
            IRightValue makeParaArg1
            )
        {
            ASBinCode.rtti.FunctionSignature signature = null;

            int blockid = env.block.id;
            if (rFunc is Variable)
            {
                blockid = ((Variable)rFunc).refdefinedinblockid;
            }
            else if (rFunc is ClassMethodGetter)
            {
                blockid = ((ClassMethodGetter)rFunc).refdefinedinblockid;
            }

            if (builder.dictSignatures.ContainsKey(blockid))
            {
                if (builder.dictSignatures[blockid].ContainsKey((IMember)rFunc))
                {

                    signature =
                         builder.dictSignatures[blockid][(IMember)rFunc];
                    var returnvalueType = signature.returnType;

                    op.regType = RunTimeDataType.rt_void;
                    eax.setEAXTypeWhenCompile(returnvalueType);
                }
            }


            op.arg1 = makeParaArg1==null? rFunc : makeParaArg1 ;
            op.arg1Type = RunTimeDataType.rt_function;

            List<ASTool.AS3.Expr.AS3DataStackElement> args
                = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;

            createParaOp(args, signature, step.token, env, rFunc, builder, false, _cls,makeParaArg1);

        }


        public void createParaOp(List<ASTool.AS3.Expr.AS3DataStackElement> args,
            ASBinCode.rtti.FunctionSignature signature, 
            ASTool.Token token, CompileEnv env,
            IRightValue rFunc,Builder builder,bool isConstructor,ASBinCode.rtti.Class cls
            ,

            IRightValue makeParaArg1=null

            )
        {
            
            if (signature != null)
            {
                if (args.Count < signature.parameters.Count)
                {
                    if (signature.parameters[args.Count].defaultValue == null
                        &&
                        !(
                            signature.parameters[signature.parameters.Count-1].isPara
                            &&
                            args.Count >= signature.parameters.Count - 1
                            )
                        )
                    {
                        throw new BuildException(token.line, token.ptr, token.sourceFile,
                            "参数数量不足,需要" + signature.parameters.Count 
                            );
                    }
                }
                else if (args.Count > signature.parameters.Count)
                {
                    if (! (signature.parameters.Count >0 
                        && signature.parameters[signature.parameters.Count - 1].isPara))
                    {
                        throw new BuildException(token.line, token.ptr, token.sourceFile,
                            "参数数量过多,期望" + signature.parameters.Count
                            );
                    }
                }
            }

            List<OpStep> toadd = new List<OpStep>(); //由于这里可能出现嵌套函数调用，所以用临时列表

            if (!isConstructor)
            {
                OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(token.line, token.ptr, token.sourceFile));
                opMakeArgs.arg1 = makeParaArg1==null? rFunc : makeParaArg1 ;
                opMakeArgs.arg1Type = RunTimeDataType.rt_function;
                toadd.Add(opMakeArgs);
            }
            else
            {
                
                OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_argement, new SourceToken(token.line, token.ptr, token.sourceFile));
                opMakeArgs.arg1 = new ASBinCode.rtData.RightValue( new ASBinCode.rtData.rtInt( cls.classid));
                opMakeArgs.arg1Type = RunTimeDataType.rt_int;
                toadd.Add(opMakeArgs);
                
            }
            
            bool hasIntoPara = false;
            for (int i = 0; i < args.Count; i++)
            {
                ASTool.AS3.Expr.AS3DataStackElement argData = args[i];
                IRightValue arg = builds.ExpressionBuilder.getRightValue(env, argData, token, builder);

                if (signature != null) //参数类型检查
                {
                    if (!hasIntoPara)
                    {
                        ASBinCode.rtti.FunctionParameter para = signature.parameters[i];
                        if (para.isPara)
                        {
                            hasIntoPara = true;
                            //throw new BuildException(token.line, token.ptr, token.sourceFile,
                            //    "... (rest) parameter 未实现"
                            //    );
                        }
                    }

                    if (!hasIntoPara)
                    {
                        ASBinCode.rtti.FunctionParameter para = signature.parameters[i];
                        ASBinCode.rtti.FunctionDefine implicit_from_func = null;
                        if (cls != null && cls.implicit_from != null)
                        {
                            foreach (var item in builder.buildoutfunctions)
                            {
                                if (item.Value.functionid == cls.implicit_from_functionid
                                    &&
                                    item.Value.signature == signature
                                    )
                                {
                                    implicit_from_func = item.Value;
                                    break;
                                }
                            }
                        }

                        if (implicit_from_func != null) //如果是隐式类型转换
                        {
                            if (arg.valueType > RunTimeDataType.unknown)
                            {
                                //var pc = builder.getClassByRunTimeDataType(arg.valueType).staticClass;

                                //if (pc ==null || pc.implicit_to == null)
                                //{
                                //    throw new BuildException(token.line, token.ptr, token.sourceFile,
                                //        "参数类型不匹配，不能将" + pc.name + "转换成" + para.type
                                //    );
                                //}
                            }
                        }
                        else
                        {
                            if (!ASRuntime.TypeConverter.testImplicitConvert(arg.valueType, para.type, builder))
                            {
                                throw new BuildException(
                                    new BuildTypeError(
                                    token.line, token.ptr, token.sourceFile,
                                    arg.valueType,para.type,builder)
                                    );
                            }
                        }

                        if (arg.valueType != para.type)
                        {
                            arg = builds.ExpressionBuilder.addCastOpStep(env, arg, para.type,
                                new SourceToken(token.line, token.ptr, token.sourceFile), builder);
                        }

                    }
                }

                //***参数准备***
                OpStep opPushArgs = new OpStep(
                    isConstructor?
                    OpCode.push_parameter_class 
                    :
                    OpCode.push_parameter
                    
                    , new SourceToken(token.line, token.ptr, token.sourceFile));
                opPushArgs.arg1 = arg;
                opPushArgs.arg1Type = arg.valueType;
                opPushArgs.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(i));
                opPushArgs.arg2Type = RunTimeDataType.rt_int;
                toadd.Add(opPushArgs);
                
            }

            env.block.opSteps.AddRange(toadd);

        }


    }
}
