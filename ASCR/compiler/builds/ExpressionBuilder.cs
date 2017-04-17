using ASBinCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class ExpressionBuilder
    {
        Builder builder;
        
        public ExpressionBuilder(Builder builder)
        {
            this.builder = builder;
            
        }

        public void buildAS3Expression(CompileEnv env, ASTool.AS3.AS3Expression expression)
        {
            for (int i = 0; i < expression.exprStepList.Count; i++)
            {
                ASTool.AS3.Expr.AS3ExprStep step = expression.exprStepList[i];

                switch (step.Type)
                {
                    case ASTool.AS3.Expr.OpType.Plus:
                        buildPlus(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Assigning:
                        buildAssigning(env, step);
                        break;      
                    case ASTool.AS3.Expr.OpType.LogicEQ:
                        buildLogicEQ(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Logic:
                        buildLogic(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.BitShift:
                        buildBitShift(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.BitAnd:
                        buildBitAnd(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Unary:
                        buildUnary(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.BitOr:
                        buildBitOr(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.BitXor:
                        buildBitXor(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Multiply:
                        buildMultipy(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Suffix:
                        buildSuffix(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.IF_GotoFlag:
                        buildIFGoto(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.GotoFlag:
                        buildGoto(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Flag:
                        buildFlag(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.CallFunc:
                        FuncCallBuilder fcb = new FuncCallBuilder();
                        fcb.buildFuncCall(env, step,builder);
                        break;
                    
                    case ASTool.AS3.Expr.OpType.Access:
                        AccessBuilder acb = new AccessBuilder();
                        acb.buildAccess(env, step, builder);
                        break;
                    case ASTool.AS3.Expr.OpType.Constructor:
                        ConstructorBuilder cb = new ConstructorBuilder();
                        cb.buildConstructor(env, step, builder);
                        break;
                    case ASTool.AS3.Expr.OpType.E4XAccess:

                    case ASTool.AS3.Expr.OpType.E4XFilter:

                    case ASTool.AS3.Expr.OpType.NameSpaceAccess:

                    default:
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));

                }
            }


        }

        public static IRightValue addCastOpStep(CompileEnv env, IRightValue src, RunTimeDataType dstType, 
            SourceToken token,Builder builder)
        {
            if (dstType < RunTimeDataType.unknown && dstType != RunTimeDataType.rt_void)
            {
                src = addCastToPrimitive(env, src,
                            token,builder
                            );
            }

            if (dstType > RunTimeDataType.unknown && src.valueType < RunTimeDataType.unknown)
            {
                if (builder.bin.primitive_to_class_table[src.valueType] != null)
                {
                    var cls = builder.bin.primitive_to_class_table[src.valueType];
                    if (cls.getRtType() == dstType)
                    {
                        OpStep stepInitClass = new OpStep(OpCode.init_staticclass,
                                    token);
                        stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
                            new ASBinCode.rtData.rtInt(cls.classid));
                        stepInitClass.arg1Type = cls.staticClass.getRtType();
                        env.block.opSteps.Add(stepInitClass);

                        //***调用执行隐式类型转换方法,先获取方法***
                        var eax_dot = env.getAdditionalRegister();
                        {
                            OpStep op_dot = new OpStep(OpCode.access_method, token);
                            eax_dot.setEAXTypeWhenCompile(ASBinCode.RunTimeDataType.rt_function);
                            op_dot.reg = eax_dot;
                            op_dot.regType = eax_dot.valueType;
                            op_dot.arg1 = new StaticClassDataGetter(cls.staticClass);
                            op_dot.arg1Type = cls.staticClass.getRtType();
                            op_dot.arg2 = (ClassMethodGetter)cls.staticClass.implicit_from.bindField; //new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(member.index));
                            op_dot.arg2Type = ASBinCode.RunTimeDataType.rt_function; //RunTimeDataType.rt_int;
                            env.block.opSteps.Add(op_dot);
                        }
                        {
                            OpStep op = new OpStep(OpCode.call_function,
                            new SourceToken(token.line, token.ptr, token.sourceFile));

                            OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(token.line, token.ptr, token.sourceFile));
                            opMakeArgs.arg1 = eax_dot;
                            opMakeArgs.arg1Type = RunTimeDataType.rt_function;
                            env.block.opSteps.Add(opMakeArgs);


                            OpStep opPushArgs = new OpStep(
                                OpCode.push_parameter
                            , new SourceToken(token.line, token.ptr, token.sourceFile));
                                    opPushArgs.arg1 = src;
                                    opPushArgs.arg1Type = src.valueType;
                                    opPushArgs.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(0));
                                    opPushArgs.arg2Type = RunTimeDataType.rt_int;
                                    env.block.opSteps.Add(opPushArgs);


                            var eax = env.getAdditionalRegister();
                            op.reg = eax;
                            op.regType = dstType;
                            eax.setEAXTypeWhenCompile(dstType);

                            op.arg1 = eax_dot;
                            op.arg1Type = RunTimeDataType.rt_function;
                            env.block.opSteps.Add(op);

                            return eax;
                        }


                    }
                }

            }


            if (src.valueType != dstType)
            {
                OpStep op =
                    new OpStep(OpCode.cast, token);

                Register tempreg = env.getAdditionalRegister();
                tempreg.setEAXTypeWhenCompile(dstType);
                op.reg = tempreg;
                op.regType = dstType;

                op.arg1 = src;
                op.arg1Type = src.valueType;
                env.block.opSteps.Add(op);

                return tempreg;
            }
            else
            {
                return src;
            }
        }

        public static IRightValue addCastToPrimitive(CompileEnv env, IRightValue src, SourceToken token,Builder builder)
        {
            RunTimeDataType dstType;
            if (ASRuntime.TypeConverter.Object_CanImplicit_ToPrimitive(src.valueType, builder,out dstType))
            {
                OpStep op =
                    new OpStep(OpCode.cast_primitive, token);

                Register tempreg = env.getAdditionalRegister();
                tempreg.setEAXTypeWhenCompile(dstType);
                op.reg = tempreg;
                op.regType = dstType;

                op.arg1 = src;
                op.arg1Type = src.valueType;
                env.block.opSteps.Add(op);

                return tempreg;
            }
            else
            {
                return src;
            }
        }

        public static IRightValue addValueOfOpStep(CompileEnv env, 
            IRightValue src,  
            SourceToken token,Builder builder)
        {
            if (src.valueType > RunTimeDataType.unknown )
            {
                ASBinCode.rtti.Class _class = (builder.getClassByRunTimeDataType(src.valueType));
                var valueOf= ClassMemberFinder.find(_class, "valueOf", _class);
                ASBinCode.rtti.FunctionSignature signature = null;
                if (valueOf != null && valueOf.bindField is ClassMethodGetter)
                {
                    signature = builder.dictSignatures[
                        ((ClassMethodGetter)valueOf.bindField).refdefinedinblockid][(ClassMethodGetter)valueOf.bindField];
                }
                else
                {
                    valueOf = null;
                }

                if (valueOf != null
                    && valueOf.valueType == RunTimeDataType.rt_function
                        && !valueOf.isStatic
                        && valueOf.isPublic
                        && !valueOf.isConstructor
                        && !valueOf.isGetter
                        && !valueOf.isSetter
                        && signature.returnType<RunTimeDataType.unknown
                        && signature.returnType != RunTimeDataType.rt_void
                        && signature.returnType != RunTimeDataType.fun_void
                    )
                {
                    Register dot_eax = null;
                    {
                        OpStep op = new OpStep(OpCode.access_method, token);
                        var eax = env.getAdditionalRegister();
                        eax.setEAXTypeWhenCompile(valueOf.valueType);

                        dot_eax = eax;

                        op.reg = eax;
                        op.regType = eax.valueType;
                        op.arg1 = src;
                        op.arg1Type = src.valueType;
                        op.arg2 = (ClassMethodGetter)valueOf.bindField; //new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(member.index));
                        op.arg2Type = valueOf.valueType; //RunTimeDataType.rt_int;

                        env.block.opSteps.Add(op);
                    }
                    {
                        OpStep op = new OpStep(OpCode.call_function,
                        new SourceToken(token.line, token.ptr, token.sourceFile));

                        OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(token.line, token.ptr, token.sourceFile));
                        opMakeArgs.arg1 = dot_eax;
                        opMakeArgs.arg1Type = RunTimeDataType.rt_function;
                        env.block.opSteps.Add(opMakeArgs);


                        var eax = env.getAdditionalRegister();
                        eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                        op.reg = eax;
                        op.regType = RunTimeDataType.rt_void;

                        //***调用valueof***
                        
                        var returnvalueType = signature.returnType;
                        op.regType = RunTimeDataType.rt_void;
                        eax.setEAXTypeWhenCompile(returnvalueType);

                        op.arg1 = dot_eax;
                        op.arg1Type = RunTimeDataType.rt_function;
                        env.block.opSteps.Add(op);

                        return eax;
                    }
                }
                else
                {
                    return src;
                }
            }
            else
            {
                return src;
            }
            
        }

        public void buildIFGoto(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            IRightValue rv = getRightValue(env, step.Arg1, step.token,builder);

            if (rv.valueType != RunTimeDataType.rt_boolean)
            {
                //插入转型代码
                rv = addCastOpStep(env, rv, RunTimeDataType.rt_boolean,
                    new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder); //op.reg;
            }

            OpStep op = new OpStep(OpCode.if_jmp, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            op.reg = null;
            op.regType = RunTimeDataType.unknown;
            op.arg1 = rv;
            op.arg1Type = rv.valueType;
            op.arg2 = new ASBinCode.rtData.RightValue( 
                new ASBinCode.rtData.rtString( step.OpCode  )) ;
            op.arg2Type = RunTimeDataType.rt_string;

            env.block.opSteps.Add(op);
        }

        public void buildGoto(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            

            OpStep op = new OpStep(OpCode.jmp, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            op.reg = null;
            op.regType = RunTimeDataType.unknown;
            op.arg1 = new ASBinCode.rtData.RightValue(
                new ASBinCode.rtData.rtString(step.OpCode));
            op.arg1Type = RunTimeDataType.rt_string;
            op.arg2 = null;
            op.arg2Type = RunTimeDataType.unknown;

            env.block.opSteps.Add(op);
        }

        public void buildFlag(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            OpStep op = new OpStep(OpCode.flag, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            op.reg = null;
            op.regType = RunTimeDataType.unknown;
            op.arg1 = null;
            op.arg1Type = RunTimeDataType.unknown;
            op.arg2 = null;
            op.arg2Type = RunTimeDataType.unknown;

            op.flag = step.OpCode;

            env.block.opSteps.Add(op);

        }


        private void buildPropSet(ClassPropertyGetter prop,
            ASTool.AS3.Expr.AS3ExprStep step,string name,CompileEnv env,
            ASBinCode.rtti.Class refClass,IRightValue rv, IRightValue setterbindobj
            ,Register eax
            )
        {
            var member = refClass.classMembers[prop.indexOfMembers];

            if (prop.setter == null)
            {
                throw new BuildException(
                    new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                    "Property " + name + " is read - only."));

            }
            //***检查可见性***
            var f = MemberFinder.findClassMember(refClass, prop.setter.name, env, builder);
            if (f == null)
            {
                throw new BuildException(
                    new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                    "Property " + name + " is read - only."));
            }

            if (prop.getter != null)
            {
                var g = MemberFinder.findClassMember(refClass, prop.getter.name, env, builder);
                if (g != null)
                {
                    //如果2个访问器访问级别不同,却又都可以访问到
                    if (f.isInternal != g.isInternal || f.isPublic != g.isPublic || f.isPrivate != g.isPrivate)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "Ambiguous reference to " + name));
                    }
                }
            }

            //**提取function**

            var signature =
                         builder.dictSignatures[prop.setter.refdefinedinblockid][prop.setter];

            //**隐式类型转换检查
            if (!ASRuntime.TypeConverter.testImplicitConvert(rv.valueType, member.valueType, builder))
            {
                throw new BuildException( new BuildTypeError( step.token.line, step.token.ptr, step.token.sourceFile,
                    rv.valueType ,member.valueType ,builder,1));
            }
            if (rv.valueType != signature.parameters[0].type)
            {
                //插入转型代码
                rv = addCastOpStep(env, rv, signature.parameters[0].type,
                    new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile), builder); //op.reg;
            }
            
            //***调用setter访问器**

            IRightValue func;
            if (setterbindobj != null)
            {
                //先从对象中取出setter
                var eaxDotSetter = env.getAdditionalRegister();
                eaxDotSetter.setEAXTypeWhenCompile(RunTimeDataType.rt_function);
                AccessBuilder.make_dotStep(env,
                    refClass.classMembers[prop.setter.indexOfMembers],
                    step.token, eaxDotSetter, setterbindobj);
                func = eaxDotSetter;
            }
            else
            {
                func = prop.setter;
            }

            //***再访问***
            OpStep opInvokeSetter = new OpStep(OpCode.call_function,
            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            opInvokeSetter.reg = eax;
            opInvokeSetter.regType = eax.valueType;
            opInvokeSetter.arg1 = func;
            opInvokeSetter.arg1Type = func.valueType;

            {
                OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                opMakeArgs.arg1 = func;
                opMakeArgs.arg1Type = func.valueType;
                env.block.opSteps.Add(opMakeArgs);
            }

            {
                //***参数准备***
                OpStep opPushArgs = new OpStep(OpCode.push_parameter, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                opPushArgs.arg1 = rv;
                opPushArgs.arg1Type = rv.valueType;
                opPushArgs.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(0));
                opPushArgs.arg2Type = RunTimeDataType.rt_int;
                env.block.opSteps.Add(opPushArgs);
            }

            env.block.opSteps.Add(opInvokeSetter);

        }


        public void buildAssigning(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == "=")
            {

                if (step.Arg1.IsReg) //在类似短路操作等编译过程中可能成为赋值目标
                {
                    
                    IRightValue rv = getRightValue(env, step.Arg2, step.token,builder);

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    
                    //当是暂存成员访问中间结果时
                    if (eax._regMember == null)
                    {
                        if (eax.valueType == RunTimeDataType.unknown)   //只在创建时设置类型
                        {
                            eax.setEAXTypeWhenCompile(rv.valueType);
                        }
                    }
                    else if (eax._regMember.bindField is ClassPropertyGetter)
                    {
                        //****检查属性存取器***
                        ClassPropertyGetter prop = (ClassPropertyGetter)eax._regMember.bindField;
                        buildPropSet(prop, step, prop.name, env, eax._regMember.refClass, rv, eax._regMemberSrcObj, eax);
                        return;
                    }
                    else if (eax._regMember.isConst)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "const成员" + eax._regMember.name + "不能在此赋值"));
                    }

                    ////***如果是成员访问的中间缓存，则需要转型
                    if (rv.valueType != eax.valueType)
                    {
                        //插入转型代码
                        rv = addCastOpStep(env, rv, eax.valueType,
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile), builder); //op.reg;

                    }

                    OpStep op = new OpStep(OpCode.assigning, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    op.reg = eax;
                    op.regType = eax.valueType;
                    op.arg1 = rv;
                    op.arg1Type = rv.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);
                    
                    

                   

                }
                else if (
                    step.Arg1.Data.FF1Type != ASTool.AS3.Expr.FF1DataValueType.identifier
                    
                    )
                {
                    throw new BuildException(
                                new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                "不能成为赋值目标"));
                }
                else
                {
                    IMember member = MemberFinder.find(step.Arg1.Data.Value.ToString(), env,false,builder,step.token);

                    if (member == null)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员" + step.Arg1.Data.Value + "未找到"));
                    }

                    if (member is Variable && ((Variable)member).isConst)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "const成员" + step.Arg1.Data.Value + "不能在此赋值"));
                    }

                    if (member is ASBinCode.ILeftValue)
                    {
                        ILeftValue lv = (ILeftValue)member;

                        IRightValue rv = getRightValue(env, step.Arg2, step.token,builder);

                        //**加入赋值操作***
                        //**隐式类型转换检查
                        if (!ASRuntime.TypeConverter.testImplicitConvert(rv.valueType, lv.valueType,builder))
                        {
                            throw new BuildException( new BuildTypeError( step.token.line, step.token.ptr, step.token.sourceFile,
                                rv.valueType ,lv.valueType ,builder));
                        }

                        if (rv.valueType != lv.valueType)
                        {
                            //插入转型代码
                            rv = addCastOpStep(env, rv, lv.valueType,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder); //op.reg;

                        }

                        //赋初始值
                        {
                            OpStep op = new OpStep(OpCode.assigning, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                            op.reg = lv;
                            op.regType = lv.valueType;
                            op.arg1 = rv;
                            op.arg1Type = rv.valueType;
                            op.arg2 = null;
                            op.arg2Type = RunTimeDataType.unknown;

                            env.block.opSteps.Add(op);
                        }

                    }
                    else
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员" + step.Arg1.Data.Value + "不能被赋值"));
                    }


                }
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));
            }
        }


        /// <summary>
        /// + -
        /// </summary>
        private void buildPlus(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == "+")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token,builder);

                v1 = addValueOfOpStep(env, v1, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile), builder);
                v2 = addValueOfOpStep(env, v2, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile), builder);


                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    var rt = ASRuntime.TypeConverter.getImplicitOpType(v1.valueType, v2.valueType, ASBinCode.OpCode.add,builder);
                    if (rt == ASBinCode.RunTimeDataType.unknown)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型" + v1.valueType + "与" + v2.valueType + "的[+]操作未定义."));
                    }


                    //操作类型转换
                    if (v1.valueType != rt)
                    {
                        v1 = addCastOpStep(env, v1, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder);
                    }
                    if (v2.valueType != rt)
                    {
                        v2 = addCastOpStep(env, v2, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder);
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(rt);

                    ASBinCode.OpStep op;// = new ASBinCode.OpStep(ASBinCode.OpCode.add,new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    if (v1.valueType == RunTimeDataType.rt_number && v2.valueType == RunTimeDataType.rt_number)
                    {
                        op = new ASBinCode.OpStep(ASBinCode.OpCode.add_number, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    }
                    else if (v1.valueType == RunTimeDataType.rt_string && v2.valueType == RunTimeDataType.rt_string)
                    {
                        op = new ASBinCode.OpStep(ASBinCode.OpCode.add_string, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    }
                    else
                    {
                        op = new ASBinCode.OpStep(ASBinCode.OpCode.add, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    }


                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }

            }
            else
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token,builder);
                
                if (step.Arg1.IsReg)
                {
                    var rt = ASRuntime.TypeConverter.getImplicitOpType(v1.valueType, v2.valueType, ASBinCode.OpCode.sub,builder);
                    if (rt == ASBinCode.RunTimeDataType.unknown)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型" + v1.valueType + "与" + v2.valueType + "的[-]操作未定义."));
                    }

                    //操作类型转换
                    if (v1.valueType != rt)
                    {
                        v1 = addCastOpStep(env, v1, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder);
                    }
                    if (v2.valueType != rt)
                    {
                        v2 = addCastOpStep(env, v2, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder);
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(rt);

                    //ASBinCode.OpStep op = new ASBinCode.OpStep(ASBinCode.OpCode.sub,
                    //    new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)
                    //    );
                    ASBinCode.OpStep op;
                    if (v1.valueType == RunTimeDataType.rt_number && v2.valueType == RunTimeDataType.rt_number)
                    {
                        op = new ASBinCode.OpStep(ASBinCode.OpCode.sub_number,
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)
                            );
                    }
                    else
                    {
                        op = new ASBinCode.OpStep(ASBinCode.OpCode.sub,
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)
                            );
                    }


                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
        }

        /// <summary>
        /// * / %
        /// </summary>
        private void buildMultipy(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token,builder);

            if (step.Arg1.IsReg)
            {
                OpCode code;
                if (step.OpCode == "/")
                {
                    code = OpCode.div;
                }
                else if (step.OpCode == "%")
                {
                    code = OpCode.mod;
                }
                else if (step.OpCode == "*")
                {
                    code = OpCode.multi;
                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));
                }

                var rt = ASRuntime.TypeConverter.getImplicitOpType(v1.valueType, v2.valueType, code,builder);
                if (rt == ASBinCode.RunTimeDataType.unknown)
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        "类型" + v1.valueType + "与" + v2.valueType + "的[" + step.OpCode + "]操作未定义."));
                }

                //操作类型转换
                if (v1.valueType != rt)
                {
                    v1 = addCastOpStep(env, v1, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder);
                }
                if (v2.valueType != rt)
                {
                    v2 = addCastOpStep(env, v2, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder);
                }

                ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                eax.setEAXTypeWhenCompile(rt);

                
                ASBinCode.OpStep op
                    = new ASBinCode.OpStep(code,
                    new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)
                    );
                
                op.arg1 = v1;
                op.arg1Type = v1.valueType;
                op.arg2 = v2;
                op.arg2Type = v2.valueType;

                op.reg = eax;
                op.regType = eax.valueType;

                env.block.opSteps.Add(op);

            }
            else
            {
                throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        "编译异常 此处应该是个寄存器"));
            }
        }


        private static IRightValue addOpPropGet(ClassPropertyGetter prop, ASTool.Token matchtoken ,string propname ,
            ASBinCode.rtti.Class refClass , IRightValue rvObj ,CompileEnv env, Builder builder
            )
        {
            
            if (prop.getter == null)
            {
                throw new BuildException(
                    new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                    "Property " + propname + " is write-only."));

            }
            //***检查可见性***
            var f = MemberFinder.findClassMember(refClass, prop.getter.name, env, builder);
            if (f == null)
            {
                throw new BuildException(
                    new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                    "Property " + propname + " is write-only."));
            }

            if (prop.setter != null)
            {
                var g = MemberFinder.findClassMember(refClass, prop.setter.name, env, builder);
                if (g != null)
                {
                    //如果2个访问器访问级别不同,却又都可以访问到
                    if (f.isInternal != g.isInternal || f.isPublic != g.isPublic || f.isPrivate != g.isPrivate)
                    {
                        throw new BuildException(
                            new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                            "Ambiguous reference to " + propname));
                    }
                }
            }

            //**提取function**

            var signature =
                         builder.dictSignatures[prop.getter.refdefinedinblockid][prop.getter];

            int idx = env.block.opSteps.Count;

            IRightValue func;

            if (rvObj != null)
            {
                var eaxDotGetter = env.getAdditionalRegister();
                eaxDotGetter.setEAXTypeWhenCompile(RunTimeDataType.rt_function);
                //***调用getter访问器**
                //先从对象中取出setter
                func = eaxDotGetter;
                AccessBuilder.make_dotStep(env,
                    refClass.classMembers[prop.getter.indexOfMembers],
                    matchtoken, eaxDotGetter, rvObj);
            }
            else
            {
                func = prop.getter;
            }

            //***再访问***
            Register gv = env.getAdditionalRegister();
            gv.setEAXTypeWhenCompile(signature.returnType);

            OpStep opInvokeGetter = new OpStep(OpCode.call_function,
            new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
            opInvokeGetter.reg = gv;
            opInvokeGetter.regType = gv.valueType;
            opInvokeGetter.arg1 = func;
            opInvokeGetter.arg1Type = func.valueType;

            {
                OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                opMakeArgs.arg1 = func;
                opMakeArgs.arg1Type = func.valueType;
                env.block.opSteps.Add(opMakeArgs);
            }

            env.block.opSteps.Add(opInvokeGetter);


            List<OpStep> addlines = new List<OpStep>();
            for (int i = idx; i < env.block.opSteps.Count ; i++)
            {
                addlines.Add(env.block.opSteps[i]);
            }
            builder._propLines.Add(gv, addlines);

            gv._regMember = refClass.classMembers[prop.indexOfMembers];
            gv._regMemberSrcObj = rvObj;
            return gv;
        }
        
        public static ASBinCode.IRightValue getRightValue(CompileEnv env, ASTool.AS3.Expr.AS3DataStackElement data, ASTool.Token matchtoken,Builder builder)
        {
            if (data.IsReg)
            {
                //if (env.tempEaxList.Count <= data.Reg.ID)

                Register reg = env.loadRegisterByAST(data.Reg.ID);
                
                if (reg == null)
                {
                    throw new BuildException(
                            new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                            "编译异常 无法获得临时变量类型"));
                }

                if (reg._regMember != null && 
                    reg._regMember.bindField is ClassPropertyGetter) //属性访问器
                {
                    ClassPropertyGetter prop = (ClassPropertyGetter)reg._regMember.bindField;

                    return addOpPropGet(prop, matchtoken, prop.name, reg._regMember.refClass, reg._regMemberSrcObj, env, builder);
                }
                else if (reg._pathGetter != null)
                {
                    return reg._pathGetter;
                }              
                else
                {
                    if (reg.valueType == RunTimeDataType.rt_void)
                    {
                        //***有可能是个访问器，
                        OpStep opgettest = new OpStep(OpCode.try_read_getter, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));

                        opgettest.arg1 = reg;
                        opgettest.arg1Type = reg.valueType;

                        reg = env.getAdditionalRegister();
                        reg.setEAXTypeWhenCompile(RunTimeDataType.rt_void);

                        opgettest.reg = reg;
                        opgettest.regType = reg.valueType;

                        env.block.opSteps.Add(opgettest);

                        List<OpStep> opline = new List<OpStep>();
                        opline.Add(opgettest);
                        builder._propLines.Add(reg, opline);
                    }

                    return reg;
                }
            }
            else
            {
                if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_expressionlist)
                {
                    List<ASTool.AS3.Expr.AS3DataStackElement> datalist
                        = (List<ASTool.AS3.Expr.AS3DataStackElement>)data.Data.Value;

                    return getRightValue(env, datalist[datalist.Count - 1], matchtoken, builder);
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.const_number)
                {
                    if (((string)data.Data.Value).StartsWith("0x"))
                    {
                        return new ASBinCode.rtData.RightValue(
                            new ASBinCode.rtData.rtNumber(
                                long.Parse(((string)data.Data.Value).Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier)
                                )
                            );
                    }
                    else
                    {
                        return new ASBinCode.rtData.RightValue(
                            new ASBinCode.rtData.rtNumber(
                                double.Parse((string)data.Data.Value)
                                )
                            );
                    }
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.const_string)
                {
                    return new ASBinCode.rtData.RightValue(
                        new ASBinCode.rtData.rtString(
                            (string)data.Data.Value)
                        );
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
                {
                    if (data.Data.Value.ToString() == "null")
                    {
                        return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtNull.nullptr);
                    }
                    else if (data.Data.Value.ToString() == "undefined")
                    {
                        return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtUndefined.undefined);
                    }
                    else if (data.Data.Value.ToString() == "true")
                    {
                        return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtBoolean.True);
                    }
                    else if (data.Data.Value.ToString() == "false")
                    {
                        return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtBoolean.False);
                    }
                    else if (data.Data.Value.ToString() == "Infinity")
                    {
                        return new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtNumber(double.PositiveInfinity));
                    }
                    else if (data.Data.Value.ToString() == "NaN")
                    {
                        return new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtNumber(double.NaN));
                    }
                    else
                    {
                        IMember member = MemberFinder.find(data.Data.Value.ToString(), env, false, builder,matchtoken);

                        if (member == null && builder._currentImports.Count > 0
                            ||
                            (member is ClassMethodGetter && ((ClassMethodGetter)member).classmember.isConstructor)
                            )
                        {
                            string t = data.Data.Value.ToString();
                            //查找导入的类
                            var found = TypeReader.findClassFromImports(t, builder, matchtoken);
                            if (found.Count == 1)
                            {
                                var item = found[0];

                                OpStep stepInitClass = new OpStep(OpCode.init_staticclass, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                                stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
                                    new ASBinCode.rtData.rtInt(item.classid));
                                stepInitClass.arg1Type = item.staticClass.getRtType();
                                env.block.opSteps.Add(stepInitClass);


                                return new StaticClassDataGetter(item.staticClass);
                            }
                            else if (found.Count > 1)
                            {
                                throw new BuildException(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                                        "类型" + t + "不明确."
                                    );
                            }
                        }

                        if (member == null && builder._currentImports.Count > 0)
                        {
                            string packagepath = data.Data.Value.ToString();

                            List<ASBinCode.rtti.Class> classlist = builder._currentImports.Peek();

                            //***查找包路径***
                            for (int i = 0; i < classlist.Count; i++)
                            {
                                if (classlist[i].package == packagepath
                                    ||
                                    classlist[i].package.StartsWith(packagepath + ".")
                                    )
                                {
                                    return new PackagePathGetter(packagepath);
                                }
                            }

                        }

                        if (member == null)
                        {
                            throw new BuildException(
                                new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                                "成员" + data.Data.Value + "未找到"));
                        }

                        if (member is ASBinCode.IRightValue)
                        {
                            if (member is ClassPropertyGetter) //需要加上测试读取
                            {
                                return
                                    addOpPropGet((ClassPropertyGetter)member, matchtoken, member.name, ((ClassPropertyGetter)member)._class,
                                        null, env, builder
                                    );

                            }
                            else
                            {
                                return (ASBinCode.IRightValue)member;
                            }
                        }
                        else
                        {
                            throw new BuildException(
                                new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                                "成员" + data.Data.Value + "不是右值"));
                        }
                    }
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.this_pointer)
                {
                    if (env.isEval)
                    {
                        throw new BuildException(
                            new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                            "表达式求值时不考虑this "));
                    }

                    if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope
                        ||
                        env.block.scope is ASBinCode.scopes.FunctionScope
                        )
                    {
                        return new ThisPointer(env.block.scope);
                    }
                    else
                    {
                        throw new BuildException(
                            new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                            "this不能出现在这里"));
                    }

                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_function)
                {
                    if (env.isEval)
                    {
                        throw new BuildException(
                            new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                            "表达式求值时不考虑function " + data.Data.FF1Type));
                    }
                    ASTool.AS3.AS3Function as3func = (ASTool.AS3.AS3Function)data.Data.Value;


                    builds.AS3FunctionBuilder fb = new AS3FunctionBuilder();
                    var fc = fb.buildAS3Function(env, as3func, builder, null);

                    return new ASBinCode.rtData.RightValue(fc);
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.dynamicobj && !env.isEval)
                {
                    //***创建一个Object***//

                    Register eax = env.getAdditionalRegister();
                    //***创建对象实例
                    {
                        var found = TypeReader.findClassFromImports("Object", builder,matchtoken);
                        var item = found[0];
                        OpStep stepInitClass = new OpStep(OpCode.init_staticclass, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                        stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
                            new ASBinCode.rtData.rtInt(item.classid));
                        stepInitClass.arg1Type = item.staticClass.getRtType();
                        env.block.opSteps.Add(stepInitClass);

                        eax.setEAXTypeWhenCompile(item.getRtType());

                        ConstructorBuilder cb = new ConstructorBuilder();
                        cb.build_class(env, item, matchtoken, builder, eax, new List<ASTool.AS3.Expr.AS3DataStackElement>());

                    }
                    //***对象赋初始值

                    Hashtable objData = (Hashtable)data.Data.Value;
                    foreach (var item in objData.Keys)
                    {
                        ASTool.Token key = (ASTool.Token)item;
                        ASTool.AS3.Expr.AS3DataStackElement value = (ASTool.AS3.Expr.AS3DataStackElement)objData[key];

                        IRightValue rv = getRightValue(env, value, matchtoken, builder);

                        OpStep setprop = new OpStep(OpCode.set_dynamic_prop, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                        setprop.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtString(key.StringValue));
                        setprop.arg1Type = RunTimeDataType.rt_string;
                        setprop.arg2 = rv;
                        setprop.arg2Type = rv.valueType;

                        setprop.reg = eax;
                        setprop.regType = eax.valueType;

                        env.block.opSteps.Add(setprop);

                    }


                    return eax;
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_array && !env.isEval)
                {
                    Register eax = env.getAdditionalRegister();
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_array);

                    //***创建原始数组对象***
                    OpStep opCreateArray = new OpStep(OpCode.array_create, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                    opCreateArray.reg = eax;
                    opCreateArray.regType = eax.valueType;
                    opCreateArray.arg1 = null;
                    opCreateArray.arg1Type = RunTimeDataType.unknown;

                    env.block.opSteps.Add(opCreateArray);

                    List<ASTool.AS3.Expr.AS3DataStackElement> list = (List<ASTool.AS3.Expr.AS3DataStackElement>)data.Data.Value;
                    for (int i = 0; i < list.Count; i++)
                    {
                        ASTool.AS3.Expr.AS3DataStackElement value = list[i];
                        IRightValue rv = getRightValue(env, value, matchtoken, builder);

                        OpStep setprop = new OpStep(OpCode.array_push, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                        setprop.arg1 = rv;
                        setprop.arg1Type = rv.valueType;
                        setprop.arg2 = null;
                        setprop.arg2Type = RunTimeDataType.unknown;

                        setprop.reg = eax;
                        setprop.regType = eax.valueType;

                        env.block.opSteps.Add(setprop);
                    }



                    return eax;
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_vector && !env.isEval)
                {
                    ASTool.AS3.AS3Vector vector = (ASTool.AS3.AS3Vector)data.Data.Value;
                    string typeStr = vector.VectorTypeStr;

                    var _vector_= TypeReader.fromSourceCodeStr( "Vector.<" + typeStr + ">", matchtoken, builder);

                    var _class = builder.getClassByRunTimeDataType(_vector_);

                    

                    if (vector.Constructor == null)
                    {
                        OpStep stepInitClass = new OpStep(OpCode.init_staticclass, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                        stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
                            new ASBinCode.rtData.rtInt(_class.classid));
                        stepInitClass.arg1Type = _class.staticClass.getRtType();
                        env.block.opSteps.Add(stepInitClass);

                        return new StaticClassDataGetter(_class.staticClass);
                    }
                    else
                    {

                        //OpStep stepinit_vector = new OpStep(OpCode.init_vector, new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                        //stepinit_vector.arg1 = new ASBinCode.rtData.RightValue(
                        //    new ASBinCode.rtData.rtInt(_class.classid));
                        //stepinit_vector.arg1Type = _class.getRtType();
                        //env.block.opSteps.Add(stepinit_vector);

                        List<ASTool.AS3.Expr.AS3DataStackElement> args =
                            (List<ASTool.AS3.Expr.AS3DataStackElement>)vector.Constructor.Data.Value;

                        if (args.Count != 1)
                        {
                            throw new BuildException(new BuildError(
                                matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                                "Argument count mismatch on class coercion.  Expected 1, got " + args.Count + "."
                                ));
                        }

                        var init_data = getRightValue(env, args[0], matchtoken, builder);

                        if (init_data.valueType != RunTimeDataType.rt_array)
                        {
                            //***强制类型转换
                            if (ASRuntime.TypeConverter.testImplicitConvert(init_data.valueType, _class.getRtType(), builder))
                            {
                                return addCastOpStep(env, init_data, _class.getRtType(), 
                                    new SourceToken(matchtoken.line,matchtoken.ptr,matchtoken.sourceFile)
                                    , builder);
                            }
                            else
                            {
                                throw new BuildException(
                                    new BuildTypeError(
                                        matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                                        init_data.valueType, _class.getRtType(),
                                        builder
                                        )
                                    );
                            }

                        }
                        else
                        {
                            Register eax = env.getAdditionalRegister();
                            eax.setEAXTypeWhenCompile(_class.getRtType());
                            //stepinit_vector.reg = eax;
                            //stepinit_vector.regType = eax.valueType;

                            ConstructorBuilder cb = new ConstructorBuilder();
                            cb.build_class(env, _class, matchtoken, builder, eax, new List<ASTool.AS3.Expr.AS3DataStackElement>());

                            //****追加初始值****
                            var vt = builder.bin.dict_Vector_type[_class];

                            List<ASTool.AS3.Expr.AS3DataStackElement> initdata = (List<ASTool.AS3.Expr.AS3DataStackElement>)args[0].Data.Value;

                            for (int i = 0; i < initdata.Count; i++)
                            {
                                var d = ExpressionBuilder.getRightValue(env, initdata[i], matchtoken, builder);

                                if (!ASRuntime.TypeConverter.testImplicitConvert(d.valueType, vt, builder))
                                {
                                    string vtstr = vt.toAS3Name();
                                    if (vt > RunTimeDataType.unknown)
                                    {
                                        vtstr = builder.getClassByRunTimeDataType(vt).name;
                                    }

                                    throw (new BuildException(new BuildError(matchtoken.line, 
                                        matchtoken.ptr, matchtoken.sourceFile,
                                                "不能将[" + d.valueType + "]类型存入Vector.<" + vtstr + ">")));

                                }

                                if (d.valueType != vt)
                                {
                                    d = addCastOpStep(env, d, vt,
                                        new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile)
                                        , builder);
                                }

                                OpStep oppush = new OpStep(OpCode.vector_push, 
                                    new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                                oppush.reg = null;
                                oppush.regType = RunTimeDataType.unknown;
                                oppush.arg1 = eax;
                                oppush.arg1Type = eax.valueType;
                                oppush.arg2 = d;
                                oppush.arg2Type = d.valueType;

                                env.block.opSteps.Add(oppush);
                            }



                            return eax;
                        }
                    }

                }
                else
                {
                    throw new BuildException(
                            new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                            "数据类型未实现 " + data.Data.FF1Type));
                }
            }
        }



        private void buildSuffix(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
            if (step.Arg1.IsReg)
            {
                if (!(v1 is ILeftValue))
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        step.OpCode + "后缀操作应该是个变量."));
                }

                if ((v1 is Register && ((Register)v1)._regMember != null))
                {
                    var m = ((Register)v1)._regMember;
                    if (m.isConst)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "const成员" + m.name + "不能在此赋值"));
                    }
                }

                if (
                        v1.valueType != RunTimeDataType.rt_number &&
                        v1.valueType != RunTimeDataType.rt_int &&
                        v1.valueType != RunTimeDataType.rt_uint &&
                        v1.valueType != RunTimeDataType.rt_void &&
                        !ASRuntime.TypeConverter.ObjectImplicit_ToNumber(v1.valueType, builder)
                        )
                {
                    throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                        "类型[" + v1.valueType + "]不能进行后缀操作[" + step.OpCode + "]");
                }

                ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                eax.setEAXTypeWhenCompile(v1.valueType);


                OpCode code;
                if (step.OpCode == "++")
                {
                    if (v1.valueType == RunTimeDataType.rt_number)
                    {
                        code = OpCode.suffix_inc_number;
                    }
                    else if (v1.valueType == RunTimeDataType.rt_int)
                    {
                        code = OpCode.suffix_inc_int;
                    }
                    else if (v1.valueType == RunTimeDataType.rt_uint)
                    {
                        code = OpCode.suffix_inc_uint;
                    }
                    else
                    {
                        code = OpCode.suffix_inc;
                    }
                }
                else
                {
                    if (v1.valueType == RunTimeDataType.rt_number)
                    {
                        code = OpCode.suffix_dec_number;
                    }
                    else if (v1.valueType == RunTimeDataType.rt_int)
                    {
                        code = OpCode.suffix_dec_int;
                    }
                    else if (v1.valueType == RunTimeDataType.rt_uint)
                    {
                        code = OpCode.suffix_dec_uint;
                    }
                    else
                    {
                        code = OpCode.suffix_dec;
                    }
                }



                ASBinCode.OpStep op
                        = new ASBinCode.OpStep(
                            code
                            , 
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                op.arg1 = v1;
                op.arg1Type = v1.valueType;
                op.arg2 = null;
                op.arg2Type = RunTimeDataType.unknown;

                op.reg = eax;
                op.regType = eax.valueType;

                env.block.opSteps.Add(op);



                //***检查是否是动态属性
                if (builder._propLines.ContainsKey(v1))
                {
                    var addlines = builder._propLines[v1];
                    if (addlines.Count == 1 && addlines[0].opCode == OpCode.try_read_getter)
                    {
                        //***将值赋值回v1***
                        OpStep opwriteback = new OpStep(OpCode.try_write_setter, 
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                        opwriteback.reg = null;
                        opwriteback.regType = RunTimeDataType.unknown;
                        opwriteback.arg1 = v1;
                        opwriteback.arg1Type = v1.valueType;
                        opwriteback.arg2 = v1;
                        opwriteback.arg2Type = v1.valueType;

                        env.block.opSteps.Add(opwriteback);
                    }
                    else
                    {
                        Register addeax = env.getAdditionalRegister();

                        Register t=(Register)v1;
                        ClassPropertyGetter prop = (ClassPropertyGetter)t._regMember.bindField;
                        //****将值赋值回去****
                        buildPropSet(prop,
                            step, prop.name, env, prop._class, v1 ,t._regMemberSrcObj, addeax
                            );
                        
                    }
                }



            }
            else
            {
                throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        "编译异常 此处应该是个寄存器"));
            }
        }


        /// <summary>
        /// 一元操作符包括 负号 ，自增等
        /// <Unary> ::=  "+"  | "-"  | "~" | "!" | "delete" | "typeof"| "++"  | "--" ;  //单目运算
        /// </summary>
        /// <param name="env"></param>
        /// <param name="step"></param>
        private void buildUnary(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == "+")
            {
                #region +

                //正号。检查是否可转换成Number;
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);
                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_number, builder))
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[+]");
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_number);

                    if (v1.valueType != RunTimeDataType.rt_number)
                    {
                        //相当于转型成Number后赋值
                        v1 = addCastOpStep(env, v1, RunTimeDataType.rt_number,
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder); //op.reg;

                        ASBinCode.OpStep op
                        = new ASBinCode.OpStep(ASBinCode.OpCode.assigning, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                        op.arg1 = v1;
                        op.arg1Type = v1.valueType;
                        op.arg2 = null;
                        op.arg2Type = RunTimeDataType.unknown;

                        op.reg = eax;
                        op.regType = eax.valueType;

                        env.block.opSteps.Add(op);

                    }
                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }

                #endregion

            }
            else if (step.OpCode == "-")
            {
                #region "-"

                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);

                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_number, builder))
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[-]");
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_number);

                    
                    //if (v1.valueType != RunTimeDataType.rt_number)
                    //{
                    //    //插入转型代码
                    //    v1 = addCastOpStep(env, v1, RunTimeDataType.rt_number,
                    //        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder); //op.reg;
                    //}

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(ASBinCode.OpCode.neg, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
                #endregion
            }
            else if (step.OpCode == "~")
            {
                #region ~

                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);

                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_number, builder)
                        ||
                        v1.valueType == RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[~]");
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_int);


                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(ASBinCode.OpCode.bitNot, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }

                #endregion

            }
            else if (step.OpCode == "!")
            {
                #region "!"

                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);

                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_boolean, builder)
                        )
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[!]");
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_boolean);


                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(ASBinCode.OpCode.logic_not, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }

                #endregion
            }
            else if (step.OpCode == "delete")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);

                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!
                        (
                            v1.valueType == RunTimeDataType.rt_void ||
                            v1.valueType > RunTimeDataType.unknown   
                        )
                        )
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[delete]");
                    }

                    if (v1 is Variable)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员[" + ((Variable)v1).name + "]不能进行一元操作[delete]");
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(ASBinCode.OpCode.delete_prop, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else if (step.OpCode == "void" && !env.isEval)
            {
                if (step.Arg1.IsReg)
                {

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(ASBinCode.OpCode.assigning, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtUndefined.undefined);
                    op.arg1Type = RunTimeDataType.rt_void;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else if (step.OpCode == "++" || step.OpCode == "--")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);
                if (!step.Arg1.IsReg || (v1 is Register && ((Register)v1)._regMember != null)
                    ||
                    builder._propLines.ContainsKey(v1)
                    )
                {
                    if (!(v1 is ILeftValue))
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            step.OpCode + "操作应该是个变量."));
                    }

                    if ((v1 is Register && ((Register)v1)._regMember != null))
                    {
                        var m = ((Register)v1)._regMember;
                        if (m.isConst)
                        {
                            throw new BuildException(
                                new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                "const成员" + m.name + "不能在此赋值"));
                        }
                    }


                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (
                        v1.valueType != RunTimeDataType.rt_number &&
                        v1.valueType != RunTimeDataType.rt_int &&
                        v1.valueType != RunTimeDataType.rt_uint &&
                        v1.valueType != RunTimeDataType.rt_void &&
                        !ASRuntime.TypeConverter.ObjectImplicit_ToNumber(v1.valueType, builder)
                        )
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[" + step.OpCode + "]");
                    }

                    //ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    //eax.setEAXTypeWhenCompile(RunTimeDataType.rt_number);

                    OpCode code;
                    if (step.OpCode == "++")
                    {
                        if (v1.valueType == RunTimeDataType.rt_number)
                        {
                            code = OpCode.increment_number;
                        }
                        else if (v1.valueType == RunTimeDataType.rt_int)
                        {
                            code = OpCode.increment_int;
                        }
                        else if (v1.valueType == RunTimeDataType.rt_uint)
                        {
                            code = OpCode.increment_uint;
                        }
                        else
                        {
                            code = OpCode.increment;
                        }
                    }
                    else
                    {
                        if (v1.valueType == RunTimeDataType.rt_number)
                        {
                            code = OpCode.decrement_number;
                        }
                        else if (v1.valueType == RunTimeDataType.rt_int)
                        {
                            code = OpCode.decrement_int;
                        }
                        else if (v1.valueType == RunTimeDataType.rt_uint)
                        {
                            code = OpCode.decrement_uint;
                        }
                        else
                        {
                            code = OpCode.decrement;
                        }
                    }

                    ASBinCode.OpStep op
                    = new ASBinCode.OpStep(
                        code
                        ,
                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = null;
                    op.arg2Type = RunTimeDataType.unknown;

                    Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(v1.valueType);

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                    //***检查是否是动态属性
                    if (builder._propLines.ContainsKey(v1))
                    {
                        var addlines = builder._propLines[v1];
                        if (addlines.Count == 1 && addlines[0].opCode == OpCode.try_read_getter)
                        {
                            //***将值赋值回v1***
                            OpStep opwriteback = new OpStep(OpCode.try_write_setter,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                            opwriteback.reg = null;
                            opwriteback.regType = RunTimeDataType.unknown;
                            opwriteback.arg1 = v1;
                            opwriteback.arg1Type = v1.valueType;
                            opwriteback.arg2 = eax;
                            opwriteback.arg2Type = eax.valueType;

                            env.block.opSteps.Add(opwriteback);
                        }
                        else
                        {
                            Register addeax = env.getAdditionalRegister();

                            Register t = (Register)v1;
                            ClassPropertyGetter prop = (ClassPropertyGetter)t._regMember.bindField;
                            //****将值赋值回去****
                            buildPropSet(prop,
                                step, prop.name, env, prop._class, eax, t._regMemberSrcObj, addeax
                                );

                        }
                    }


                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个变量."));
                }
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));
            }

        }

        /// <summary>
        /// 逻辑比较 
        /// &lt;  | >  | &lt;= | >= | as | in  | instanceof | is
        /// </summary>
        /// <param name="env"></param>
        /// <param name="step"></param>
        private void buildLogic(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == ">" || step.OpCode =="<" || step.OpCode == ">=" || step.OpCode == "<=")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token,builder);
                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    
                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_boolean);

                    OpCode opcode = OpCode.gt_void;
                    switch (step.OpCode)
                    {
                        case ">":
                            break;
                        case "<":
                            opcode = OpCode.lt_void;
                            break;
                        case ">=":
                            opcode = OpCode.ge_void;
                            break;
                        case "<=":
                        default:
                            opcode = OpCode.le_void;
                            break;
                    }

                    //比较两个表达式，确定 expression1 是否大于 expression2；如果是，则结果为 true。如果 expression1 小于等于 expression2，则结果为 false。 
                    //如果两个操作数的类型都为 String，则使用字母顺序比较操作数；所有大写字母都排在小写字母的前面。否则，首先将操作数转换为数字，然后进行比较。

                    RunTimeDataType t1;ASRuntime.TypeConverter.Object_CanImplicit_ToPrimitive(v1.valueType, builder, out t1);
                    RunTimeDataType t2;ASRuntime.TypeConverter.Object_CanImplicit_ToPrimitive(v1.valueType, builder, out t2);

                    //如果v1和v2,有任何一个是数值或布尔类型，则转成数字，否则动态计算。
                    if (
                        v1.valueType == RunTimeDataType.rt_boolean ||
                        v1.valueType == RunTimeDataType.rt_int ||
                        v1.valueType == RunTimeDataType.rt_uint ||
                        v1.valueType == RunTimeDataType.rt_number ||
                        v2.valueType == RunTimeDataType.rt_boolean ||
                        v2.valueType == RunTimeDataType.rt_int ||
                        v2.valueType == RunTimeDataType.rt_uint ||
                        v2.valueType == RunTimeDataType.rt_number ||
                        ASRuntime.TypeConverter.ObjectImplicit_ToNumber(v1.valueType,builder)
                        ||
                        ASRuntime.TypeConverter.ObjectImplicit_ToNumber(v2.valueType, builder)
                        ||
                        t1== RunTimeDataType.rt_boolean
                        ||
                        t2 == RunTimeDataType.rt_boolean
                        )
                    {

                        if (v1.valueType != RunTimeDataType.rt_number)
                        {
                            //插入转型代码
                            v1 = addCastOpStep(env, v1, RunTimeDataType.rt_number,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder); //op.reg;
                        }

                        if (v2.valueType != RunTimeDataType.rt_number)
                        {
                            //插入转型代码
                            v2 = addCastOpStep(env, v2, RunTimeDataType.rt_number,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile),builder); //op.reg;
                        }
                        //opcode = step.OpCode ==">"? OpCode.gt : OpCode.lt;

                        switch (step.OpCode)
                        {
                            case ">":
                                opcode = OpCode.gt_num;
                                break;
                            case "<":
                                opcode = OpCode.lt_num;
                                break;
                            case ">=":
                                opcode = OpCode.ge_num;
                                break;
                            case "<=":
                            default:
                                opcode = OpCode.le_num;
                                break;
                        }
                    }

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(opcode, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else
            {

                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));
            }
        }


        private void buildLogicEQ(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == "==" || step.OpCode=="!=")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token,builder);
                if (step.Arg1.IsReg)
                {

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_boolean);

                    OpCode code = step.OpCode=="==" ? OpCode.equality: OpCode.not_equality ;
                    if (
                        (
                        v1.valueType == RunTimeDataType.rt_int
                        || v1.valueType == RunTimeDataType.rt_uint
                        || v1.valueType == RunTimeDataType.rt_number
                        || ASRuntime.TypeConverter.ObjectImplicit_ToNumber(v1.valueType, builder)
                        )
                        &&
                        (
                        v2.valueType == RunTimeDataType.rt_int
                        || v2.valueType == RunTimeDataType.rt_uint
                        || v2.valueType == RunTimeDataType.rt_number
                        || ASRuntime.TypeConverter.ObjectImplicit_ToNumber(v2.valueType, builder)
                        )
                        )
                    {
                        code = step.OpCode == "==" ? OpCode.equality_num_num : OpCode.not_equality_num_num  ;
                    }
                    else if (v1.valueType == RunTimeDataType.rt_string && v2.valueType == RunTimeDataType.rt_string)
                    {
                        code = step.OpCode == "==" ? OpCode.equality_str_str : OpCode.not_equality_str_str  ;
                    }

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(code, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else if (step.OpCode == "===" || step.OpCode =="!==")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token,builder);
                if (step.Arg1.IsReg)
                {

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_boolean);

                    OpCode code = step.OpCode == "===" ? OpCode.strict_equality : OpCode.not_strict_equality ;

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(code, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));
            }
        }

        private void buildBitAnd(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token,builder);
            if (step.Arg1.IsReg)
            {
                if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int,builder)
                    ||
                    v1.valueType== RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v1.valueType + "不能执行位与操作"));
                }
                if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int,builder)
                    ||
                    v2.valueType== RunTimeDataType.rt_boolean 
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v2.valueType + "不能执行位与操作"));
                }

                
                ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_int);

                ASBinCode.OpStep op
                    = new ASBinCode.OpStep( OpCode.bitAnd, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                op.arg1 = v1;
                op.arg1Type = v1.valueType;
                op.arg2 = v2;
                op.arg2Type = v2.valueType;

                op.reg = eax;
                op.regType = eax.valueType;

                env.block.opSteps.Add(op);

            }
            else
            {
                throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        "编译异常 此处应该是个寄存器"));
            }

        }

        private void buildBitOr(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token,builder);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token, builder);
            if (step.Arg1.IsReg)
            {
                if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int,builder)
                    ||
                    v1.valueType == RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v1.valueType + "不能执行位或操作"));
                }
                if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int,builder)
                    ||
                    v2.valueType == RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v2.valueType + "不能执行位或操作"));
                }


                ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_int);

                ASBinCode.OpStep op
                    = new ASBinCode.OpStep(OpCode.bitOr, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                op.arg1 = v1;
                op.arg1Type = v1.valueType;
                op.arg2 = v2;
                op.arg2Type = v2.valueType;

                op.reg = eax;
                op.regType = eax.valueType;

                env.block.opSteps.Add(op);

            }
            else
            {
                throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        "编译异常 此处应该是个寄存器"));
            }
        }

        private void buildBitXor(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token, builder);
            if (step.Arg1.IsReg)
            {
                if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int,builder)
                    ||
                    v1.valueType == RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v1.valueType + "不能执行位异或操作"));
                }
                if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int,builder)
                    ||
                    v2.valueType == RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v2.valueType + "不能执行位异或操作"));
                }


                ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_int);

                ASBinCode.OpStep op
                    = new ASBinCode.OpStep(OpCode.bitXOR, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                op.arg1 = v1;
                op.arg1Type = v1.valueType;
                op.arg2 = v2;
                op.arg2Type = v2.valueType;

                op.reg = eax;
                op.regType = eax.valueType;

                env.block.opSteps.Add(op);

            }
            else
            {
                throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        "编译异常 此处应该是个寄存器"));
            }
        }


        private void buildBitShift(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == "<<")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token, builder);
                if (step.Arg1.IsReg)
                {
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int,builder)
                        ||
                        v1.valueType== RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v1.valueType + "不能执行移位操作"));
                    }
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int,builder)
                        ||
                        v2.valueType== RunTimeDataType.rt_boolean 
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v2.valueType + "不能执行移位操作"));
                    }


                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_int);

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(OpCode.bitLeftShift, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }
                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else if (step.OpCode == ">>")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token, builder);
                if (step.Arg1.IsReg)
                {
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int,builder)
                        ||
                        v1.valueType== RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v1.valueType +"不能执行移位操作"));
                    }
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int,builder)
                        ||
                        v2.valueType== RunTimeDataType.rt_boolean 
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v2.valueType + "不能执行移位操作"));
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_int);

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(OpCode.bitRightShift, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }

                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else if (step.OpCode == ">>>")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token, builder);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token, builder);
                if (step.Arg1.IsReg)
                {
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_uint,builder)
                        ||
                        v1.valueType == RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v1.valueType + "不能执行移位操作"));
                    }
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int,builder)
                        ||
                        v2.valueType == RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v2.valueType + "不能执行移位操作"));
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_uint);

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(OpCode.bitUnsignedRightShift, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    op.arg1 = v1;
                    op.arg1Type = v1.valueType;
                    op.arg2 = v2;
                    op.arg2Type = v2.valueType;

                    op.reg = eax;
                    op.regType = eax.valueType;

                    env.block.opSteps.Add(op);

                }

                else
                {
                    throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "编译异常 此处应该是个寄存器"));
                }
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));
            }
        }

    }
}
