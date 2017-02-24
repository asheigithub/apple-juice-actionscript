using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class ExpressionBuilder
    {
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
                        fcb.buildFuncCall(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Constructor:

                    case ASTool.AS3.Expr.OpType.Access:

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

        public static IRightValue addCastOpStep(CompileEnv env, IRightValue src, RunTimeDataType dstType, SourceToken token)
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

        public void buildIFGoto(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            IRightValue rv = getRightValue(env, step.Arg1, step.token);

            if (rv.valueType != RunTimeDataType.rt_boolean)
            {
                //插入转型代码
                rv = addCastOpStep(env, rv, RunTimeDataType.rt_boolean,
                    new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)); //op.reg;
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


        public void buildAssigning(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == "=")
            {

                if (step.Arg1.IsReg) //在类似短路操作等编译过程中可能成为赋值目标
                {
                    //throw new BuildException(
                    //                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                    //                            "不能成为赋值目标"));
                    IRightValue rv = getRightValue(env, step.Arg2, step.token);

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(rv.valueType);
                    

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
                    IMember member = MemberFinder.find(step.Arg1.Data.Value.ToString(), env);
                    if (member == null)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员" + step.Arg1.Data.Value + "未找到"));
                    }

                    if (member is ASBinCode.ILeftValue)
                    {
                        ILeftValue lv = (ILeftValue)member;

                        IRightValue rv = getRightValue(env, step.Arg2, step.token);

                        //**加入赋值操作***
                        //**隐式类型转换检查
                        if (!ASRuntime.TypeConverter.testImplicitConvert(rv.valueType, lv.valueType))
                        {
                            throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                                "不能将[" + rv.valueType + "]类型赋值给[" + lv.valueType + "]类型的变量");
                        }

                        if (rv.valueType != lv.valueType)
                        {
                            //插入转型代码
                            rv = addCastOpStep(env, rv, lv.valueType,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)); //op.reg;

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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);

                if (step.Arg1.IsReg)
                {



                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    var rt = ASRuntime.TypeConverter.getImplicitOpType(v1.valueType, v2.valueType, ASBinCode.OpCode.add);
                    if (rt == ASBinCode.RunTimeDataType.unknown)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型" + v1.valueType + "与" + v2.valueType + "的[+]操作未定义."));
                    }


                    //操作类型转换
                    if (v1.valueType != rt)
                    {
                        v1 = addCastOpStep(env, v1, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    }
                    if (v2.valueType != rt)
                    {
                        v2 = addCastOpStep(env, v2, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);



                if (step.Arg1.IsReg)
                {
                    var rt = ASRuntime.TypeConverter.getImplicitOpType(v1.valueType, v2.valueType, ASBinCode.OpCode.sub);
                    if (rt == ASBinCode.RunTimeDataType.unknown)
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型" + v1.valueType + "与" + v2.valueType + "的[-]操作未定义."));
                    }

                    //操作类型转换
                    if (v1.valueType != rt)
                    {
                        v1 = addCastOpStep(env, v1, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    }
                    if (v2.valueType != rt)
                    {
                        v2 = addCastOpStep(env, v2, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
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
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);

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

                var rt = ASRuntime.TypeConverter.getImplicitOpType(v1.valueType, v2.valueType, code);
                if (rt == ASBinCode.RunTimeDataType.unknown)
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        "类型" + v1.valueType + "与" + v2.valueType + "的[" + step.OpCode + "]操作未定义."));
                }

                //操作类型转换
                if (v1.valueType != rt)
                {
                    v1 = addCastOpStep(env, v1, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                }
                if (v2.valueType != rt)
                {
                    v2 = addCastOpStep(env, v2, rt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
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


        public static ASBinCode.IRightValue getRightValue(CompileEnv env, ASTool.AS3.Expr.AS3DataStackElement data, ASTool.Token matchtoken)
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
                return reg;
            }
            else
            {
                if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_expressionlist)
                {
                    List<ASTool.AS3.Expr.AS3DataStackElement> datalist
                        = (List<ASTool.AS3.Expr.AS3DataStackElement>)data.Data.Value;

                    return getRightValue(env, datalist[datalist.Count - 1], matchtoken);
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.const_number)
                {
                    if (((string)data.Data.Value).StartsWith("0x"))
                    {
                        return new ASBinCode.rtData.RightValue(
                            new ASBinCode.rtData.rtNumber(
                                long.Parse(((string)data.Data.Value).Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier )
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
                    else
                    {
                        ASBinCode.IMember member = MemberFinder.find(data.Data.Value.ToString(), env);
                        if (member == null)
                        {
                            throw new BuildException(
                                new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                                "成员" + data.Data.Value + "未找到"));
                        }

                        if (member is ASBinCode.IRightValue)
                        {
                            return (ASBinCode.IRightValue)member;
                        }
                        else
                        {
                            throw new BuildException(
                                new BuildError(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile,
                                "成员" + data.Data.Value + "不是右值"));
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
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
            if (step.Arg1.IsReg)
            {
                if (!(v1 is ILeftValue))
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        step.OpCode + "后缀操作应该是个变量."));
                }

                if (
                        v1.valueType != RunTimeDataType.rt_number &&
                        v1.valueType != RunTimeDataType.rt_int &&
                        v1.valueType != RunTimeDataType.rt_uint &&
                        v1.valueType != RunTimeDataType.rt_void
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_number))
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
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)); //op.reg;

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

                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);

                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_number))
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[-]");
                    }

                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_number);

                    if (v1.valueType != RunTimeDataType.rt_number)
                    {
                        //插入转型代码
                        v1 = addCastOpStep(env, v1, RunTimeDataType.rt_number,
                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)); //op.reg;
                    }

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

                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);

                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_number)
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

                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);

                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_boolean)
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
            else if (step.OpCode == "++" || step.OpCode =="--")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg1, step.token);
                if (!step.Arg1.IsReg)
                {
                    if (!(v1 is ILeftValue))
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            step.OpCode+"操作应该是个变量."));
                    }

                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    if (
                        v1.valueType != RunTimeDataType.rt_number &&
                        v1.valueType != RunTimeDataType.rt_int &&
                        v1.valueType != RunTimeDataType.rt_uint &&
                        v1.valueType != RunTimeDataType.rt_void 
                        )
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型[" + v1.valueType + "]不能进行一元操作[" + step.OpCode  + "]");
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

                    op.reg = null;
                    op.regType = RunTimeDataType.unknown;

                    env.block.opSteps.Add(op);
                    
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
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


                    //如果v1和v2,有任何一个是数值或布尔类型，则转成数字，否则动态计算。
                    if (
                        v1.valueType == RunTimeDataType.rt_boolean ||
                        v1.valueType == RunTimeDataType.rt_int ||
                        v1.valueType == RunTimeDataType.rt_uint ||
                        v1.valueType == RunTimeDataType.rt_number ||
                        v2.valueType == RunTimeDataType.rt_boolean ||
                        v2.valueType == RunTimeDataType.rt_int ||
                        v2.valueType == RunTimeDataType.rt_uint ||
                        v2.valueType == RunTimeDataType.rt_number
                        )
                    {

                        if (v1.valueType != RunTimeDataType.rt_number)
                        {
                            //插入转型代码
                            v1 = addCastOpStep(env, v1, RunTimeDataType.rt_number,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)); //op.reg;
                        }

                        if (v2.valueType != RunTimeDataType.rt_number)
                        {
                            //插入转型代码
                            v2 = addCastOpStep(env, v2, RunTimeDataType.rt_number,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)); //op.reg;
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
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
                        )
                        &&
                        (
                        v2.valueType == RunTimeDataType.rt_int
                        || v2.valueType == RunTimeDataType.rt_uint
                        || v2.valueType == RunTimeDataType.rt_number
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
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
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
            if (step.Arg1.IsReg)
            {
                if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int)
                    ||
                    v1.valueType== RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v1.valueType + "不能执行位与操作"));
                }
                if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int)
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
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
            if (step.Arg1.IsReg)
            {
                if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int)
                    ||
                    v1.valueType == RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v1.valueType + "不能执行位或操作"));
                }
                if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int)
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
            ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
            ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
            if (step.Arg1.IsReg)
            {
                if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int)
                    ||
                    v1.valueType == RunTimeDataType.rt_boolean
                    )
                {
                    throw new BuildException(
                        new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                        v1.valueType + "不能执行位异或操作"));
                }
                if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int)
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
                if (step.Arg1.IsReg)
                {
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int)
                        ||
                        v1.valueType== RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v1.valueType + "不能执行移位操作"));
                    }
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int)
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
                if (step.Arg1.IsReg)
                {
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_int)
                        ||
                        v1.valueType== RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v1.valueType +"不能执行移位操作"));
                    }
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int)
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
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
                if (step.Arg1.IsReg)
                {
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v1.valueType, RunTimeDataType.rt_uint)
                        ||
                        v1.valueType == RunTimeDataType.rt_boolean
                        )
                    {
                        throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            v1.valueType + "不能执行移位操作"));
                    }
                    if (!ASRuntime.TypeConverter.testImplicitConvert(v2.valueType, RunTimeDataType.rt_int)
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
