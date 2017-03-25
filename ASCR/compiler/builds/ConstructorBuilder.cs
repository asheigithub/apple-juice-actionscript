using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class ConstructorBuilder
    {
        

        public void buildConstructor(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step, Builder builder)
        {
            if (step.OpCode == "new")
            {
                if (step.Arg1.IsReg)
                {
                    ASBinCode.rtti.Class _class = null;
                    if (step.Arg2.IsReg)
                    {
                        var cls = ExpressionBuilder.getRightValue(env, step.Arg2, step.token, builder);
                        if (cls is ASBinCode.StaticClassDataGetter)
                        {
                            _class = builder.getClassByRunTimeDataType(cls.valueType).instanceClass;
                            build_class(env, _class, step, builder);
                            return;
                        }
                        else if (cls.valueType == ASBinCode.RunTimeDataType.rt_void)
                        {
                            //从Class对象中new
                            build_void(env, cls, step, builder);
                            return;
                        }
                        else
                        {
                            throw new BuildException(
                                new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                "类型" + cls.valueType + "不能new"));
                        }
                    }
                    else
                    {
                        //***查找对象类型
                        var find = TypeReader.findClassFromImports(step.Arg2.Data.Value.ToString(), builder);

                       

                        if (find.Count == 1)
                        {
                            _class = find[0];
                            build_class(env, _class, step, builder);
                            return;
                        }
                        else
                        {

                            var cls = ExpressionBuilder.getRightValue(env, step.Arg2, step.token, builder);

                            if (cls is ASBinCode.StaticClassDataGetter)
                            {
                                _class = builder.getClassByRunTimeDataType(cls.valueType).instanceClass;
                                build_class(env, _class, step, builder);
                                return;
                            }
                            else if (cls.valueType == ASBinCode.RunTimeDataType.rt_void)
                            {
                                //从Class对象中new
                                build_void(env, cls, step, builder);
                                return;
                            }
                            else
                            {
                                throw new BuildException(
                                    new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                    "类型" + cls.valueType + "不能new"));
                            }
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
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "不支持的操作类型 " + step.Type + " " + step.OpCode));
            }
        }




        private void build_class(CompileEnv env, ASBinCode.rtti.Class _class, ASTool.AS3.Expr.AS3ExprStep step,Builder builder)
        {
            //***查找构造函数**
            if (_class.constructor != null)
            {
                Field field = _class.constructor.bindField; //(Field)builder._classbuildingEnv[_class].block.scope.members[_class.constructor.index];
                int blockid = field.refdefinedinblockid;

                var signature =
                        builder.dictSignatures[blockid][field];
                //***参数检查***
                List<ASTool.AS3.Expr.AS3DataStackElement> args;
                if (step.Arg3 != null)
                {
                    args = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;
                }
                else
                {
                    args = new List<ASTool.AS3.Expr.AS3DataStackElement>();
                }
                FuncCallBuilder funcbuilder = new FuncCallBuilder();
                funcbuilder.createParaOp(args, signature, step, env, field, builder, true, _class);
            }


            OpStep op = new OpStep(OpCode.new_instance, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            op.arg1 = new RightValue(new rtInt(_class.classid));
            op.arg1Type = _class.getRtType();
            Register eax = env.createASTRegister(step.Arg1.Reg.ID);
            eax.setEAXTypeWhenCompile(_class.getRtType());
            op.reg = eax;
            op.regType = _class.getRtType();

            env.block.opSteps.Add(op);

        }



        private void build_void(CompileEnv env, IRightValue cls, ASTool.AS3.Expr.AS3ExprStep step, Builder builder)
        {
            OpStep opnewclass = new OpStep(OpCode.new_instance_class,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            opnewclass.arg1 = cls;
            opnewclass.arg1Type = cls.valueType;
            Register _eax = env.createASTRegister(step.Arg1.Reg.ID);
            _eax.setEAXTypeWhenCompile(cls.valueType);
            opnewclass.reg = _eax;
            opnewclass.regType = cls.valueType;


            OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_class_argement, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            opMakeArgs.arg1 = cls;
            opMakeArgs.arg1Type = cls.valueType;
            env.block.opSteps.Add(opMakeArgs);

            if (step.Arg3 != null)
            {
                List<ASTool.AS3.Expr.AS3DataStackElement> args
                    = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;
                for (int i = 0; i < args.Count; i++)
                {
                    ASTool.AS3.Expr.AS3DataStackElement argData = args[i];
                    IRightValue arg = builds.ExpressionBuilder.getRightValue(env, argData, step.token, builder);
                    //***参数准备***
                    OpStep opPushArgs = new OpStep(OpCode.push_parameter_class, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    opPushArgs.arg1 = arg;
                    opPushArgs.arg1Type = arg.valueType;
                    opPushArgs.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(i));
                    opPushArgs.arg2Type = RunTimeDataType.rt_int;
                    env.block.opSteps.Add(opPushArgs);

                }
            }





            env.block.opSteps.Add(opnewclass);

        }

    }
}
