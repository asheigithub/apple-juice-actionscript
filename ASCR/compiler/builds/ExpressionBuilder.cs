using ASBinCode;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    case ASTool.AS3.Expr.OpType.LogicAnd:

                    case ASTool.AS3.Expr.OpType.BitOr:

                    case ASTool.AS3.Expr.OpType.BitXor:

                    case ASTool.AS3.Expr.OpType.BitAnd:

                    case ASTool.AS3.Expr.OpType.LogicEQ:

                    case ASTool.AS3.Expr.OpType.Logic:
                        buildLogic(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.BitShift:

                    case ASTool.AS3.Expr.OpType.Multiply:

                    case ASTool.AS3.Expr.OpType.Unary:
                        buildUnary(env, step);
                        break;
                    case ASTool.AS3.Expr.OpType.Constructor:

                    case ASTool.AS3.Expr.OpType.Access:

                    case ASTool.AS3.Expr.OpType.E4XAccess:

                    case ASTool.AS3.Expr.OpType.E4XFilter:

                    case ASTool.AS3.Expr.OpType.NameSpaceAccess:

                    case ASTool.AS3.Expr.OpType.CallFunc:

                    case ASTool.AS3.Expr.OpType.Suffix:

                    case ASTool.AS3.Expr.OpType.Flag:

                    case ASTool.AS3.Expr.OpType.IF_GotoFlag:

                    case ASTool.AS3.Expr.OpType.GotoFlag:

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



        public void buildAssigning(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step)
        {
            if (step.OpCode == "=")
            {

                if (step.Arg1.IsReg)
                {
                    throw new BuildException(
                                                new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                                "不能成为赋值目标"));
                }
                else if (step.Arg1.Data.FF1Type != ASTool.AS3.Expr.FF1DataValueType.identifier)
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
                    return new ASBinCode.rtData.RightValue<ASBinCode.rtData.rtNumber>(
                        new ASBinCode.rtData.rtNumber(
                            double.Parse((string)data.Data.Value))
                        );
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.const_string)
                {
                    return new ASBinCode.rtData.RightValue<ASBinCode.rtData.rtString>(
                        new ASBinCode.rtData.rtString(
                            (string)data.Data.Value)
                        );
                }
                else if (data.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
                {
                    if (data.Data.Value.ToString() == "null")
                    {
                        return new ASBinCode.rtData.RightValue<ASBinCode.rtData.rtNull>(ASBinCode.rtData.rtNull.nullptr);
                    }
                    else if (data.Data.Value.ToString() == "undefined")
                    {
                        return new ASBinCode.rtData.RightValue<ASBinCode.rtData.rtUndefined>(ASBinCode.rtData.rtUndefined.undefined);
                    }
                    else if (data.Data.Value.ToString() == "true")
                    {
                        return new ASBinCode.rtData.RightValue<ASBinCode.rtData.rtBoolean>(ASBinCode.rtData.rtBoolean.True);
                    }
                    else if (data.Data.Value.ToString() == "false")
                    {
                        return new ASBinCode.rtData.RightValue<ASBinCode.rtData.rtBoolean>(ASBinCode.rtData.rtBoolean.False);
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
            if (step.OpCode == ">")
            {
                ASBinCode.IRightValue v1 = getRightValue(env, step.Arg2, step.token);
                ASBinCode.IRightValue v2 = getRightValue(env, step.Arg3, step.token);
                if (step.Arg1.IsReg)
                {
                    //eax.setEAXTypeWhenCompile(typeTable[(int)v1.valueType, (int)v2.valueType]);
                    
                    ASBinCode.Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_boolean);

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

                    ASBinCode.OpStep op
                        = new ASBinCode.OpStep(ASBinCode.OpCode.gt, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

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
