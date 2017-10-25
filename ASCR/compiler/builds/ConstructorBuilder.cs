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
						else if (cls is MethodGetterBase)
						{
							throw new BuildException(
								new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
								"Method cannot be used as a constructor."));
						}
						else if (cls.valueType == ASBinCode.RunTimeDataType.rt_void || cls.valueType == ASBinCode.RunTimeDataType.rt_function)
						{
							//从Class对象中new
							build_void(env, cls, step, builder);
							return;
						}
						else if (cls.valueType > ASBinCode.RunTimeDataType.unknown)
						{
							_class = builder.getClassByRunTimeDataType(cls.valueType);

							if (_class.isInterface)
							{
								throw new BuildException(
								new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
								_class.name + " Interfaces cannot be instantiated with the new operator."));
							}
							else if (_class.staticClass != null)
							{
								build_class(env, _class, step, builder);
							}
							else if ( cls is Register && ((Register)cls).isFindByPath && _class.staticClass==null )
							{
								build_class(env, _class.instanceClass, step, builder);
							}
							else
							{
								throw new BuildException(
								new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
								"类型" + cls.valueType + "不能new"));
							}
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
                        var find = TypeReader.findClassFromImports(step.Arg2.Data.Value.ToString(), builder, step.token);

                       

                        if (find.Count == 1)
                        {
                            _class = find[0];

                            if (_class.isInterface)
                            {
                                throw new BuildException(
                                new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                _class.name + " Interfaces cannot be instantiated with the new operator."));
                            }
                            else if (_class.no_constructor)
                            {

                            }
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
                            else if (cls is MethodGetterBase)
                            {
                                throw new BuildException(
                                    new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                    "Method cannot be used as a constructor."));
                            }
                            else if (cls.valueType == ASBinCode.RunTimeDataType.rt_void || cls.valueType == ASBinCode.RunTimeDataType.rt_function)
                            {
                                //从Class对象中new
                                build_void(env, cls, step, builder);

                                return;
                            }
                            else if (cls.valueType > RunTimeDataType.unknown &&
                                (builder.getClassByRunTimeDataType(cls.valueType).classid == 2
                                ||
                                builder.getClassByRunTimeDataType(cls.valueType).classid == 0
                                ))
                            {
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

        public void build_class(CompileEnv env,
            ASBinCode.rtti.Class _class,
            ASTool.Token token, Builder builder,
            Register outeax,
            List<ASTool.AS3.Expr.AS3DataStackElement> args
            )
        {
            //***查找构造函数**
            if (_class.constructor != null)
            {
                MethodGetterBase field = (MethodGetterBase)_class.constructor.bindField; //(Field)builder._classbuildingEnv[_class].block.scope.members[_class.constructor.index];
                int blockid = field.refdefinedinblockid;

                var signature =
                        builder.dictSignatures[blockid][field];
                
                FuncCallBuilder funcbuilder = new FuncCallBuilder();
                funcbuilder.createParaOp(args, signature, token, env, field, builder, true, _class);
            }
            else
            {
                OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_argement,
                    new SourceToken(token.line, token.ptr, token.sourceFile));
                opMakeArgs.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(_class.classid));
                opMakeArgs.arg1Type = RunTimeDataType.rt_int;
                env.block.opSteps.Add(opMakeArgs);
            }

            OpStep op = new OpStep(OpCode.new_instance, new SourceToken(token.line, token.ptr, token.sourceFile));
            op.arg1 = new RightValue(new rtInt(_class.classid));
            op.arg1Type = _class.getRtType();
            
            outeax.setEAXTypeWhenCompile(_class.getRtType());
            op.reg = outeax;
            op.regType = _class.getRtType();

            env.block.opSteps.Add(op);
        }


        private void build_class(CompileEnv env, 
            ASBinCode.rtti.Class _class, 
            ASTool.AS3.Expr.AS3ExprStep step,Builder builder)
        {
            //***参数检查***

            

            List<ASTool.AS3.Expr.AS3DataStackElement> args;
            if (!step.Arg2.IsReg && step.Arg2.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_vector)
            {
                ASTool.AS3.AS3Vector vector = (ASTool.AS3.AS3Vector)step.Arg2.Data.Value;

                
                
                if (vector.Constructor == null)
                {
                    args = new List<ASTool.AS3.Expr.AS3DataStackElement>();
                }
                else
                {
                    if (vector.isInitData)
                    {
                        args = new List<ASTool.AS3.Expr.AS3DataStackElement>();
                    }
                    else
                    {
                        args = (List<ASTool.AS3.Expr.AS3DataStackElement>)vector.Constructor.Data.Value;
                    }
                }
                
            }
            else
            {

                if (step.Arg3 != null)
                {
                    args = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;
                }
                else
                {
                    args = new List<ASTool.AS3.Expr.AS3DataStackElement>();
                }
            }

            Register insEax = env.createASTRegister(step.Arg1.Reg);

            build_class(env, _class, step.token, builder,
                insEax,
                args
                );

            if (!step.Arg2.IsReg && step.Arg2.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_vector)
            {
                ASTool.AS3.AS3Vector vector = (ASTool.AS3.AS3Vector)step.Arg2.Data.Value;
                if (vector.Constructor != null && vector.isInitData)
                {
                    //***追加初始化Vector***
                    var initdata = (List<ASTool.AS3.Expr.AS3DataStackElement>)vector.Constructor.Data.Value;

                    var vt = builder.bin.dict_Vector_type[_class];

                    for (int i = 0; i < initdata.Count; i++)
                    {
                        var d = ExpressionBuilder.getRightValue(env, initdata[i], step.token,builder);

                        if (!ASRuntime.TypeConverter.testImplicitConvert(d.valueType, vt, builder))
                        {
                            throw( new BuildException( new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                        "不能将[" + d.valueType + "]类型存入Vector.<" + vt + ">")));
                            
                        }

                        if (d.valueType != vt)
                        {
                            d=ExpressionBuilder.addCastOpStep(env, d, vt, 
                                new SourceToken(step.token.line,step.token.ptr,step.token.sourceFile)
                                , builder);
                        }

                        OpStep oppush = new OpStep(OpCode.vector_push, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                        oppush.reg = null;
                        oppush.regType = RunTimeDataType.unknown;
                        oppush.arg1 = insEax;
                        oppush.arg1Type = insEax.valueType;
                        oppush.arg2 = d;
                        oppush.arg2Type = d.valueType;

                        env.block.opSteps.Add(oppush);
                    }

                    

                    //throw new NotImplementedException();

                }
            }


                ////***查找构造函数**
                //if (_class.constructor != null)
                //{
                //    ClassMethodGetter field = (ClassMethodGetter)_class.constructor.bindField; //(Field)builder._classbuildingEnv[_class].block.scope.members[_class.constructor.index];
                //    int blockid = field.refdefinedinblockid;

                //    var signature =
                //            builder.dictSignatures[blockid][field];
                //    //***参数检查***
                //    List<ASTool.AS3.Expr.AS3DataStackElement> args;
                //    if (step.Arg3 != null)
                //    {
                //        args = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;
                //    }
                //    else
                //    {
                //        args = new List<ASTool.AS3.Expr.AS3DataStackElement>();
                //    }
                //    FuncCallBuilder funcbuilder = new FuncCallBuilder();
                //    funcbuilder.createParaOp(args, signature, step, env, field, builder, true, _class);
                //}
                //else
                //{
                //    OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_argement,
                //        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                //    opMakeArgs.arg1 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(_class.classid));
                //    opMakeArgs.arg1Type = RunTimeDataType.rt_int;
                //    env.block.opSteps.Add(opMakeArgs);
                //}

                //OpStep op = new OpStep(OpCode.new_instance, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                //op.arg1 = new RightValue(new rtInt(_class.classid));
                //op.arg1Type = _class.getRtType();
                //Register eax = env.createASTRegister(step.Arg1.Reg.ID);
                //eax.setEAXTypeWhenCompile(_class.getRtType());
                //op.reg = eax;
                //op.regType = _class.getRtType();

                //env.block.opSteps.Add(op);

            }



        private void build_void(CompileEnv env, RightValueBase cls, ASTool.AS3.Expr.AS3ExprStep step, Builder builder)
        {
            OpStep opnewclass = new OpStep(OpCode.new_instance_class,
                                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            opnewclass.arg1 = cls;
            opnewclass.arg1Type = RunTimeDataType.rt_void;
            Register _eax = env.createASTRegister(step.Arg1.Reg);
            _eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
            opnewclass.reg = _eax;
            opnewclass.regType = RunTimeDataType.rt_void;


            OpStep opMakeArgs = new OpStep(OpCode.prepare_constructor_class_argement, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
            opMakeArgs.arg1 = cls;
            opMakeArgs.arg1Type = RunTimeDataType.rt_void;
            env.block.opSteps.Add(opMakeArgs);

            if (step.Arg3 != null)
            {
                List<ASTool.AS3.Expr.AS3DataStackElement> args
                    = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;
                for (int i = 0; i < args.Count; i++)
                {
                    ASTool.AS3.Expr.AS3DataStackElement argData = args[i];
                    RightValueBase arg = builds.ExpressionBuilder.getRightValue(env, argData, step.token, builder);
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
