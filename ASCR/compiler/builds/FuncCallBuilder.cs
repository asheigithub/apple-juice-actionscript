using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
	class FuncCallBuilder
	{
		private bool searchbuildin(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step,Builder builder ,string name )
		{
			var util = TypeReader.findClassFromImports("@__buildin__", builder, step.token);
			if (util.Count == 1)
			{
				if (env.isEval)
				{
					return true;
				}

				var bi = util[0].staticClass;

				var member = ClassMemberFinder.find(bi, name, bi);
				if (member != null && member.valueType == RunTimeDataType.rt_function)
				{
					OpStep stepInitClass = new OpStep(OpCode.init_staticclass,
						new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
					stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
						new ASBinCode.rtData.rtInt(bi.instanceClass.classid));
					stepInitClass.arg1Type = bi.getRtType();
					env.block.opSteps.Add(stepInitClass);

					var _buildin_ = new StaticClassDataGetter(bi);
					var eaxfunc = env.getAdditionalRegister();
					{

						eaxfunc.setEAXTypeWhenCompile(RunTimeDataType.rt_function);
						AccessBuilder.make_dotStep(env, member, step.token, eaxfunc, _buildin_);

					}
					{
						OpStep op = new OpStep(OpCode.call_function,
							new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

						var eax = env.createASTRegister(step.Arg1.Reg);
						eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
						op.reg = eax;
						op.regType = RunTimeDataType.rt_void;
						eax.isFuncResult = true;

						op.arg1 = eaxfunc;
						op.arg1Type = RunTimeDataType.rt_function;

						bool isfindsignatrue;ASBinCode.rtti.FunctionSignature signature;
						build_member_parameterSteps((RightValueBase)member.bindField,
							builder, eax, op, step, env, null, eaxfunc, null,out isfindsignatrue,out signature);
						if(isfindsignatrue)
						{
							builder._toOptimizeCallFunctionOpSteps.Add((b)=> {

								if (signature.returnType > RunTimeDataType.unknown || signature.returnType == RunTimeDataType.rt_void)
									op.opCode = OpCode.call_function_notcheck;
								else
								{
									op.opCode = OpCode.call_function_notcheck_notreturnobject;

									bool isfindsuccess;
									if (isNotYieldAndNotNative(signature, builder, out isfindsuccess))
									{
										if (isfindsuccess)
										{
											op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative;
											op.arg2 = ASRuntime.TypeConverter.getDefaultValue(signature.returnType);op.arg2Type = signature.returnType;

											var func = findFunction(signature, builder,out isfindsuccess);
											if (func.isMethod && signature.onStackParameters == signature.parameters.Count && builder.bin.blocks[func.blockid].scope.members.Count==0)
											{
												op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative_method;
											}

										}
									}
								}
							});
						}

						env.block.opSteps.Add(op);

					}

					//build_member(member.bindField, step, builder, env, bi, name);
					return true;
				}

			}

			return false;
		}


		private bool FindPackageFunction(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step, Builder builder)
		{
			
			{
				string name = step.Arg2.Data.Value.ToString();

				var find = TypeReader.findPackageFunctionFromImports(name, builder, step.token);
				if (find.Count == 1 && find[0].isPackageFunction)
				{
					if (searchbuildin(env, step, builder, (find[0].package + "." + find[0].name).Replace(".","_") ))
					{
						return true;
					}
				}
				

			}
			


			return false;
		}

		public void buildFuncCall(CompileEnv env, ASTool.AS3.Expr.AS3ExprStep step,Builder builder)
        {
            if (env.isEval)
            {
                return;
            }

            if (!step.Arg2.IsReg && step.Arg2.Data.FF1Type== ASTool.AS3.Expr.FF1DataValueType.as3_expressionlist )
            {     
                //var f = ExpressionBuilder.getRightValue(env, step.Arg2, step.token, builder);
                var olddata = step.Arg2;

                step.Arg2= ((List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg2.Data.Value)[((List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg2.Data.Value).Count -1];
                

                buildFuncCall(env, step, builder);

                step.Arg2 = olddata;
                return;
            }


            if (step.Arg2.IsReg || step.Arg2.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_function
                
                )
            {
                RightValueBase rValue = ExpressionBuilder.getRightValue(env, step.Arg2, step.token, builder);
                if (rValue is PackagePathGetter)
                {
					PackagePathGetter pf = (PackagePathGetter)rValue;
					var find = TypeReader.findPackageFunction(pf.path, builder, step.token);
					if (find.Count==1)
					{
						if (searchbuildin(env, step, builder, (find[0].package + "." + find[0].name).Replace(".", "_")))
						{
							return;
						}
					}


                    throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "类型" + ((PackagePathGetter)rValue).path + "没有找到."
                        );
                    

                }


                OpStep op = new OpStep(OpCode.call_function,
                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                var eax = env.createASTRegister(step.Arg1.Reg);
                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                op.reg = eax;
                op.regType = RunTimeDataType.rt_void;
                eax.isFuncResult = true;

                op.arg1 = rValue;
                op.arg1Type = RunTimeDataType.rt_function;

                if (rValue is StackSlotAccessor)
                {
                    StackSlotAccessor reg = (StackSlotAccessor)rValue;
                    if (reg._regMember != null)
                    {
                        if (reg._regMember.isConstructor)   //不能直接调用构造函数
                        {
                            throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                                                "Attempted access of inaccessible method " + reg._regMember.name + " through a reference with static type " + reg._regMember.refClass.name + "."
                                                );
                        }
						bool findsignatrue;ASBinCode.rtti.FunctionSignature signature;
                        build_member_parameterSteps((RightValueBase)reg._regMember.bindField,
                            builder, eax, op, step, env, null, rValue, null,out findsignatrue,out signature);
						if (findsignatrue)
						{
							builder._toOptimizeCallFunctionOpSteps.Add((b) => {
								if (signature.returnType > RunTimeDataType.unknown || signature.returnType == RunTimeDataType.rt_void)
									op.opCode = OpCode.call_function_notcheck;
								else
								{
									op.opCode = OpCode.call_function_notcheck_notreturnobject;

									bool isfindsuccess;
									if (isNotYieldAndNotNative(signature, builder, out isfindsuccess))
									{
										if (isfindsuccess)
										{
											op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative;
											op.arg2 = ASRuntime.TypeConverter.getDefaultValue(signature.returnType);op.arg2Type = signature.returnType;

											var func = findFunction(signature, builder, out isfindsuccess);
											if (func.isMethod && signature.onStackParameters == signature.parameters.Count && builder.bin.blocks[func.blockid].scope.members.Count == 0)
											{
												op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative_method;
											}
										}
									}
								}
							});
						}

                        env.block.opSteps.Add(op);
                        return;
                    }

                }


                List<ASTool.AS3.Expr.AS3DataStackElement> args
                    = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;

                List<RightValueBase> argsValue = new List<RightValueBase>();

                for (int i = 0; i < args.Count; i++)
                {
                    ASTool.AS3.Expr.AS3DataStackElement argData = args[i];
                    RightValueBase arg = builds.ExpressionBuilder.getRightValue(env, argData, step.token, builder);
                    argsValue.Add(arg);
                }
                //***
                {
                    OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    opMakeArgs.arg1 = rValue;
                    opMakeArgs.arg1Type = RunTimeDataType.rt_function;
                    env.block.opSteps.Add(opMakeArgs);
                }

                for (int i = 0; i < args.Count; i++)
                {
                    //***参数准备***
                    OpStep opPushArgs = new OpStep(OpCode.push_parameter, new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    opPushArgs.arg1 = argsValue[i];
                    opPushArgs.arg1Type = argsValue[i].valueType;
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

					if (searchbuildin(env, step, builder, name))
					{
						return;
					}

                    ////***从__buildin__中查找
                    //var buildin = TypeReader.findClassFromImports("@__buildin__", builder, step.token);
                    //if (buildin.Count == 1)
                    //{
                    //    if (env.isEval)
                    //    {
                    //        return;
                    //    }

                    //    var bi = buildin[0].staticClass;

                    //    var member = ClassMemberFinder.find(bi, name, bi);
                    //    if (member != null && member.valueType == RunTimeDataType.rt_function)
                    //    {
                    //        OpStep stepInitClass = new OpStep(OpCode.init_staticclass,
                    //            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                    //        stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
                    //            new ASBinCode.rtData.rtInt(bi.instanceClass.classid));
                    //        stepInitClass.arg1Type = bi.getRtType();
                    //        env.block.opSteps.Add(stepInitClass);

                    //        var _buildin_ = new StaticClassDataGetter(bi);
                    //        var eaxfunc = env.getAdditionalRegister();
                    //        {

                    //            eaxfunc.setEAXTypeWhenCompile(RunTimeDataType.rt_function);
                    //            AccessBuilder.make_dotStep(env, member, step.token, eaxfunc, _buildin_);

                    //        }
                    //        {
                    //            OpStep op = new OpStep(OpCode.call_function,
                    //                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                    //            var eax = env.createASTRegister(step.Arg1.Reg.ID);
                    //            eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                    //            op.reg = eax;
                    //            op.regType = RunTimeDataType.rt_void;
                    //            eax.isFuncResult = true;

                    //            op.arg1 = eaxfunc;
                    //            op.arg1Type = RunTimeDataType.rt_function;

                    //            build_member_parameterSteps((RightValueBase)member.bindField,
                    //                builder, eax, op, step, env, null, eaxfunc, null);

                    //            env.block.opSteps.Add(op);

                    //        }

                    //        //build_member(member.bindField, step, builder, env, bi, name);
                    //        return;
                    //    }

                    //}

                }
				#endregion

				#region 查找@_flash.utils.getDefinitionByName

				if (FindPackageFunction(env,step,builder))
				{
					return;
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

                    IMember member = MemberFinder.find(name, env, false, builder, step.token);

                    if (member == null
                        ||
                        (member is MethodGetterBase
                        &&
                        ((MethodGetterBase)member).classmember.isConstructor
                        )

                        )     //检查是否强制类型转换
                    {
                        var found = TypeReader.findClassFromImports(name, builder, step.token);
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

                                    OpStep stepInitClass = new OpStep(OpCode.init_staticclass,
                                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                                    stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
                                        new ASBinCode.rtData.rtInt(cls.instanceClass.classid));
                                    stepInitClass.arg1Type = cls.getRtType();
                                    env.block.opSteps.Add(stepInitClass);

                                    var _buildin_ = new StaticClassDataGetter(cls);
                                    var eaxfunc = env.getAdditionalRegister();
                                    {

                                        eaxfunc.setEAXTypeWhenCompile(RunTimeDataType.rt_function);
                                        AccessBuilder.make_dotStep(env, cls.explicit_from, step.token, eaxfunc, _buildin_);

                                    }
                                    {
                                        OpStep op = new OpStep(OpCode.call_function,
                                            new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                                        var eax = env.createASTRegister(step.Arg1.Reg);
                                        eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                                        op.reg = eax;
                                        op.regType = RunTimeDataType.rt_void;
                                        eax.isFuncResult = true;

                                        op.arg1 = eaxfunc;
                                        op.arg1Type = RunTimeDataType.rt_function;

										bool findsignature;ASBinCode.rtti.FunctionSignature signature;
                                        build_member_parameterSteps((RightValueBase)cls.explicit_from.bindField,
                                            builder, eax, op, step, env, null, eaxfunc, null,out findsignature,out signature);
										if (findsignature)
										{
											builder._toOptimizeCallFunctionOpSteps.Add((b) => {
												if (signature.returnType > RunTimeDataType.unknown || signature.returnType == RunTimeDataType.rt_void)
													op.opCode = OpCode.call_function_notcheck;
												else
												{
													op.opCode = OpCode.call_function_notcheck_notreturnobject;

													bool isfindsuccess;
													if (isNotYieldAndNotNative(signature, builder, out isfindsuccess))
													{
														if (isfindsuccess)
														{
															op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative;
															op.arg2 = ASRuntime.TypeConverter.getDefaultValue(signature.returnType); op.arg2Type = signature.returnType;

															var func = findFunction(signature, builder, out isfindsuccess);
															if (func.isMethod && signature.onStackParameters == signature.parameters.Count && builder.bin.blocks[func.blockid].scope.members.Count == 0)
															{
																op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative_method;
															}
														}
													}
												}

											});
										}
											
                                        env.block.opSteps.Add(op);

                                    }
                                    return;
                                }

                                {
                                    if (args.Count != 1)
                                    {
                                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                                                                    "强制类型转换" + name + "函数必须是1个参数"
                                                                    );
                                    }

                                    RightValueBase src = ExpressionBuilder.getRightValue(env, args[0], step.token, builder);
                                    RightValueBase ct = ExpressionBuilder.addCastOpStep(env, src,
                                        targettype, //cls.instanceClass.getRtType(), 
                                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile), builder);

                                    OpStep op = new OpStep(OpCode.assigning,
                                        new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                                    var eax = env.createASTRegister(step.Arg1.Reg);
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

                    if (member is FindStaticMember)
                    {
                        FindStaticMember fm = (FindStaticMember)member;
                        if (ASRuntime.TypeConverter.testImplicitConvert(fm.classMember.valueType, RunTimeDataType.rt_function, builder))
                        {
                            RightValueBase eaxfunc = fm.buildAccessThisMember(step.token, env);

                            if (eaxfunc.valueType != RunTimeDataType.rt_function)
                            {
                                eaxfunc = ExpressionBuilder.addCastOpStep(env, eaxfunc, RunTimeDataType.rt_function,
                                    new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile)
                                    , builder);
                            }
                            
                            {


                                var oc = getOutPackageClass(env);

                                OpStep clear = new OpStep(OpCode.clear_thispointer,
                                     new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                                clear.arg1 = eaxfunc;
                                clear.arg1Type = RunTimeDataType.rt_function;
                                clear.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(oc.classid));
                                clear.arg2Type = RunTimeDataType.rt_int;


                                eaxfunc = env.getAdditionalRegister();
                                ((StackSlotAccessor)eaxfunc).setEAXTypeWhenCompile(RunTimeDataType.rt_function);
                                clear.reg = (StackSlotAccessor)eaxfunc;
                                clear.regType = eaxfunc.valueType;

                                env.block.opSteps.Add(clear);


                                OpStep op = new OpStep(OpCode.call_function,
                                    new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                                var eax = env.createASTRegister(step.Arg1.Reg);
                                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                                op.reg = eax;
                                op.regType = RunTimeDataType.rt_void;
                                eax.isFuncResult = true;

                                op.arg1 = eaxfunc;
                                op.arg1Type = RunTimeDataType.rt_function;

								bool isfindsignature;ASBinCode.rtti.FunctionSignature signature;
                                build_member_parameterSteps((RightValueBase)fm.classMember.bindField,
                                    builder, eax, op, step, env, null, eaxfunc, null,out isfindsignature,out signature);
								if (isfindsignature)
								{
									builder._toOptimizeCallFunctionOpSteps.Add((b)=> {
										if (signature.returnType > RunTimeDataType.unknown || signature.returnType == RunTimeDataType.rt_void)
											op.opCode = OpCode.call_function_notcheck;
										else
										{
											op.opCode = OpCode.call_function_notcheck_notreturnobject;

											bool isfindsuccess;
											if (isNotYieldAndNotNative(signature, builder, out isfindsuccess))
											{
												if (isfindsuccess)
												{
													op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative;
													op.arg2 = ASRuntime.TypeConverter.getDefaultValue(signature.returnType); op.arg2Type = signature.returnType;

													var func = findFunction(signature, builder, out isfindsuccess);
													if (func.isMethod && signature.onStackParameters == signature.parameters.Count && builder.bin.blocks[func.blockid].scope.members.Count == 0)
													{
														op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative_method;
													}
												}
											}
										}

									});
								}

                                env.block.opSteps.Add(op);

                            }

                            //build_member(member.bindField, step, builder, env, bi, name);
                            return;
                        }
                        else
                        {
                            throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                                fm.static_class._class.name + "." + name + "不是一个function"
                            );
                        }
                    }
					
                    if (member == null)
                    {
                        throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                            "成员" + name + "没有找到"
                            );
                    }

                    build_member(member, step, builder, env, _cls, name);


                }
            }
            else if (step.Arg2.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.super_pointer)
            {
                //***调用父类的构造函数***
                //**首先自己必须是构造函数***
                if (env.block.scope is ASBinCode.scopes.FunctionScope
                    &&
                     ((ASBinCode.scopes.FunctionScope)env.block.scope).function.isConstructor
                    )
                {
                    OpStep f = new OpStep(OpCode.flag_call_super_constructor,new SourceToken(step.token.line,step.token.ptr,step.token.sourceFile));
                    env.block.opSteps.Add(f);

					var testclass = ((ASBinCode.scopes.ObjectInstanceScope)env.block.scope.parentScope)._class;

					var superclass =
						((ASBinCode.scopes.ObjectInstanceScope)env.block.scope.parentScope)._class.super;

					if (testclass.isCrossExtend && superclass.isLink_System )
					{
						//***调用父类的适配器版本****

						if (superclass.crossExtendAdapterCreator == null)
						{
							throw new BuildException(
								new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
								" 没有找到正确的适配器继承适配器."));
						}

						var c = superclass.crossExtendAdapterCreator;
						build_member(c.bindField, step, builder, env, superclass, c.name);
					}
					else
					{						
						while (superclass != null && superclass.constructor == null)
						{
							superclass = superclass.super;
						}

						if (superclass != null)
						{
							var c = superclass.constructor;
							build_member(c.bindField, step, builder, env, superclass, superclass.constructor.name);
						}
					}
                }
                else
                {
                    throw new BuildException(
                                new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                                " A super statement can be used only inside class instance constructors."));
                }
                //throw new BuildException(
                //            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                //            "super_pointer未实现"));
            }
            else
            {
                throw new BuildException(
                            new BuildError(step.token.line, step.token.ptr, step.token.sourceFile,
                            "目前只实现了trace函数"));
            }
        }



        private ASBinCode.rtti.Class getOutPackageClass(CompileEnv env)
        {
            IScope scope = env.block.scope;
            ASBinCode.rtti.Class oc = null;

            while (true)
            {
                if (scope is ASBinCode.scopes.OutPackageMemberScope)
                {
                    oc = ((ASBinCode.scopes.OutPackageMemberScope)scope).mainclass;
                    break;
                }
                else if (scope is ASBinCode.scopes.ObjectInstanceScope)
                {
                    oc = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                    if (oc.staticClass == null)
                    {
                        oc = oc.instanceClass;
                    }
                    if (oc.mainClass != null)
                    {
                        oc = oc.mainClass;
                    }

                    break;
                }
                else if (scope is ASBinCode.scopes.FunctionScope)
                {
                    scope = scope.parentScope;
                }
                else
                {
                    break;
                }

            }
            return oc;
        }


        private void build_member(IMember member, ASTool.AS3.Expr.AS3ExprStep step, Builder builder,CompileEnv env,
            ASBinCode.rtti.Class _cls , string name
            )
        {

            RightValueBase rFunc;

            IMember originMember = member;

            if (member is FindOutPackageScopeMember)
            {
                rFunc=((FindOutPackageScopeMember)member).buildAccessThisMember(step.token, env);

                originMember = ((FindOutPackageScopeMember)member).member;

            }
            else
            {
                rFunc = (RightValueBase)member;
            }
            if (member is VariableBase || member is FindOutPackageScopeMember)
            {

                var oc = getOutPackageClass(env);

                OpStep clear = new OpStep(OpCode.clear_thispointer,
                             new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));
                clear.arg1 = rFunc;
                clear.arg1Type = RunTimeDataType.rt_function;

                clear.arg2 = new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(oc.classid));
                clear.arg2Type = RunTimeDataType.rt_int;

                var eaxfunc = env.getAdditionalRegister();
                ((StackSlotAccessor)eaxfunc).setEAXTypeWhenCompile(RunTimeDataType.rt_function);
                clear.reg = (StackSlotAccessor)eaxfunc;
                clear.regType = eaxfunc.valueType;

                

                env.block.opSteps.Add(clear);

                rFunc = eaxfunc;

            }



            if (ASRuntime.TypeConverter.testImplicitConvert(rFunc.valueType, RunTimeDataType.rt_function, builder))
            {
                OpStep op = new OpStep(OpCode.call_function,
                new SourceToken(step.token.line, step.token.ptr, step.token.sourceFile));

                var eax = env.createASTRegister(step.Arg1.Reg);
                eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                op.reg = eax;
                op.regType = RunTimeDataType.rt_void;
                eax.isFuncResult = true;

				bool findsignature;ASBinCode.rtti.FunctionSignature signature;
                build_member_parameterSteps(rFunc, builder, eax, op, step, env, _cls,null,originMember,out findsignature,out signature);
				if (findsignature)
				{
					builder._toOptimizeCallFunctionOpSteps.Add((bd)=> 
					{

						if (signature.returnType > RunTimeDataType.unknown || signature.returnType == RunTimeDataType.rt_void)
							op.opCode = OpCode.call_function_notcheck;
						else
						{
							op.opCode = OpCode.call_function_notcheck_notreturnobject;

							bool isfindsuccess;
							if (isNotYieldAndNotNative(signature, builder, out isfindsuccess))
							{
								if (isfindsuccess)
								{
									op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative;
									op.arg2 = ASRuntime.TypeConverter.getDefaultValue(signature.returnType); op.arg2Type = signature.returnType;

									var func = findFunction(signature, builder, out isfindsuccess);
									if (func.isMethod && signature.onStackParameters== signature.parameters.Count && builder.bin.blocks[func.blockid].scope.members.Count == 0)
									{
										op.opCode = OpCode.call_function_notcheck_notreturnobject_notnative_method;
									}
								}
							}
						}

					});

					
				}

                env.block.opSteps.Add(op);



            }
            else
            {
                throw new BuildException(step.token.line, step.token.ptr, step.token.sourceFile,
                    "成员" + name + "不是一个function"
                    );
            }
        }

        private void build_member_parameterSteps(RightValueBase rFunc,Builder builder,StackSlotAccessor eax,OpStep op,
            ASTool.AS3.Expr.AS3ExprStep step,CompileEnv env,ASBinCode.rtti.Class _cls,
            RightValueBase makeParaArg1 ,IMember funcOriginMember,out bool findsignature,out ASBinCode.rtti.FunctionSignature sig
            )
        {
			ASBinCode.rtti.FunctionSignature signature = null;
			findsignature = false;sig = null;
            int blockid = env.block.id;
            if (rFunc is VariableBase)
            {
                blockid = ((VariableBase)rFunc).refdefinedinblockid;
            }
            else if (rFunc is MethodGetterBase)
            {
                blockid = ((MethodGetterBase)rFunc).refdefinedinblockid;
            }

            if (funcOriginMember == null)
            {
                funcOriginMember = (IMember)rFunc;
            }
            else
            {
                if (funcOriginMember is VariableBase)
                {
                    blockid = ((VariableBase)funcOriginMember).refdefinedinblockid;
                }
                else if (funcOriginMember is MethodGetterBase)
                {
                    blockid = ((MethodGetterBase)funcOriginMember).refdefinedinblockid;
                }
            }

            if (builder.dictSignatures.ContainsKey(blockid))
            {
                if (builder.dictSignatures[blockid].ContainsKey( funcOriginMember ))
                {
					findsignature = true;
                    signature =
                         builder.dictSignatures[blockid][funcOriginMember];
                    var returnvalueType = signature.returnType;

					sig = signature;

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
            RightValueBase rFunc,Builder builder,bool isConstructor,ASBinCode.rtti.Class cls
            ,

            RightValueBase makeParaArg1=null

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

				if (signature != null)
				{
					builder._toOptimizeCallFunctionOpSteps.Add((b)=> {

						bool findsuccess;
						var func = findFunction(signature, builder, out findsuccess);
						if (findsuccess && checkFunctionNotHasOverridesOrSealed(func,builder))
						{
							if (!func.isConstructor)
							{
								if (func.isMethod && (opMakeArgs.arg1 is MethodGetterBase ))
								{
									opMakeArgs.opCode = OpCode.make_para_scope_method;

									if (isNativeModeConstPara(signature, builder, out findsuccess))
									{
										opMakeArgs.opCode = OpCode.make_para_scope_withsignature_nativeconstpara;
										opMakeArgs.jumoffset = signature.parameters.Count;
									}
									else
									if (isNativeNotModeConstPara(signature, builder, out findsuccess))
									{
										if (signature.onStackParameters == signature.parameters.Count)
										{
											if (signature.parameters.Count == 0)
											{
												opMakeArgs.opCode = OpCode.make_para_scope_method_noparameters;
											}
											else
											{
												opMakeArgs.opCode = OpCode.make_para_scope_method_notnativeconstpara_allparaonstack;
												opMakeArgs.jumoffset = signature.parameters.Count;
												for (int i = 0; i < signature.parameters.Count; i++)
												{
													var p = signature.parameters[i];
													if (p.isPara || p.defaultValue != null)
													{
														opMakeArgs.jumoffset = i;
														break;
													}
												}
											}
										}

									}
									else if (isNotNative(signature, builder, out findsuccess) && builder.bin.blocks[func.blockid].scope.members.Count == 0)
									{
										if (signature.onStackParameters == signature.parameters.Count)
										{
											if (signature.parameters.Count == 0 )
											{
												opMakeArgs.opCode = OpCode.make_para_scope_method_noparameters;
											}
											else
											{
												opMakeArgs.opCode = OpCode.make_para_scope_method_notnativeconstpara_allparaonstack;
												opMakeArgs.jumoffset = signature.parameters.Count;
												for (int i = 0; i < signature.parameters.Count; i++)
												{
													var p = signature.parameters[i];
													if (p.isPara || p.defaultValue != null)
													{
														opMakeArgs.jumoffset = i;
														break;
													}
												}
											}
										}
									}

								}

								else
								{
									opMakeArgs.opCode = OpCode.make_para_scope_withsignature;

									if (isNativeModeConstPara(signature, builder, out findsuccess))
									{
										opMakeArgs.opCode = OpCode.make_para_scope_withsignature_nativeconstpara;
										opMakeArgs.jumoffset = signature.parameters.Count;
									}
									else
									if (isNativeNotModeConstPara(signature, builder, out findsuccess))
									{
										if (signature.onStackParameters == signature.parameters.Count)
										{
											if (signature.parameters.Count == 0)
											{
												opMakeArgs.opCode = OpCode.make_para_scope_withsignature_noparameters;
											}
											else
											{
												opMakeArgs.opCode = OpCode.make_para_scope_withsignature_allparaonstack;
												opMakeArgs.jumoffset = signature.parameters.Count;
												for (int i = 0; i < signature.parameters.Count; i++)
												{
													var p = signature.parameters[i];
													if (p.isPara || p.defaultValue != null)
													{
														opMakeArgs.jumoffset = i;
														break;
													}
												}

											}

										}

									}
									else if (isNotNative(signature, builder, out findsuccess) && builder.bin.blocks[func.blockid].scope.members.Count == 0)
									{
										if (signature.onStackParameters == signature.parameters.Count)
										{
											if (signature.parameters.Count == 0)
											{
												opMakeArgs.opCode = OpCode.make_para_scope_withsignature_noparameters;
											}
											else
											{
												opMakeArgs.opCode = OpCode.make_para_scope_withsignature_allparaonstack;
												opMakeArgs.jumoffset = signature.parameters.Count;
												for (int i = 0; i < signature.parameters.Count; i++)
												{
													var p = signature.parameters[i];
													if (p.isPara || p.defaultValue != null)
													{
														opMakeArgs.jumoffset = i;
														break;
													}
												}
											}
										}
									}

								}
							}
						}
						else
						{
							if (opMakeArgs.arg1 is MethodGetterBase)
							{
								opMakeArgs.opCode = OpCode.make_para_scope_method;
							}
							else
							{
								opMakeArgs.opCode = OpCode.make_para_scope_withsignature;
							}
						}

					});
				}

				

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
                RightValueBase arg = builds.ExpressionBuilder.getRightValue(env, argData, token, builder);

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

				if (signature != null)
				{
					if (opPushArgs.opCode == OpCode.push_parameter)
					{
						if (arg.valueType != RunTimeDataType.rt_void
							&&
							arg.valueType != RunTimeDataType.rt_function 
							&&
							arg.valueType != RunTimeDataType._OBJECT 
							&&
							arg.valueType != builder.bin.FunctionClass.getRtType()
							)
						{
							
							if (!hasIntoPara)
							{
								builder._toOptimizeCallFunctionOpSteps.Add((b)=> {
									//特别注意接口和被继承的情况，接口的函数必须到运行时才能确定。所以检查时必须检查接口
									bool findsuccess;

									var func = findFunction(signature, builder, out findsuccess);

									if (!findsuccess || func == null || !checkFunctionNotHasOverridesOrSealed(func, builder))
									{
										return;
									}
									

									if (isNativeModeConstPara(signature, builder, out findsuccess))
									{
										if (findsuccess)
										{
											opPushArgs.opCode = OpCode.push_parameter_nativeconstpara;
											opPushArgs.jumoffset = signature.parameters.Count -  ((ASBinCode.rtData.rtInt)opPushArgs.arg2.getValue(null,null)).value;
											opPushArgs.memregid1 = (short)((ASBinCode.rtData.rtInt)opPushArgs.arg2.getValue(null, null)).value;
										}
									}
									else if (isNativeNotModeConstPara(signature, builder, out findsuccess))
									{
										if (findsuccess)
										{
											int idx = ((ASBinCode.rtData.rtInt)opPushArgs.arg2.getValue(null, null)).value;
											var parameter = signature.parameters[idx];
											if (signature.onStackParameters > 0)
											{
												if (parameter.isOnStack)
												{
													opPushArgs.opCode = OpCode.push_parameter_skipcheck_storetostack;
													opPushArgs.jumoffset = ((StackSlotAccessor)parameter.varorreg)._index;
												}
												else
												{
													opPushArgs.opCode = OpCode.push_parameter_skipcheck_storetoheap;
													opPushArgs.jumoffset = ((Variable)parameter.varorreg).indexOfMembers;
												}
											}
											else
											{
												opPushArgs.opCode = OpCode.push_parameter_skipcheck_storetoheap;
												opPushArgs.jumoffset = idx;
											}
										}
									}
									else
									{
										bool checkisnotnative;
										if (isNotNative(signature, builder, out checkisnotnative))
										{
											if (checkisnotnative)
											{
												int idx = ((ASBinCode.rtData.rtInt)opPushArgs.arg2.getValue(null, null)).value;
												var parameter = signature.parameters[idx];

												if (signature.onStackParameters > 0)
												{
													if (parameter.isOnStack)
													{
														opPushArgs.opCode = OpCode.push_parameter_skipcheck_storetostack;
														opPushArgs.jumoffset = ((StackSlotAccessor)parameter.varorreg)._index;
													}
													else
													{
														opPushArgs.opCode = OpCode.push_parameter_skipcheck_storetoheap;
														opPushArgs.jumoffset = ((Variable)parameter.varorreg).indexOfMembers;
													}
												}
												else
												{
													opPushArgs.opCode = OpCode.push_parameter_skipcheck_storetoheap;
													opPushArgs.jumoffset = idx;
												}

											}
											else
											{

												opPushArgs.opCode = OpCode.push_parameter_skipcheck_testnative;
											}
										}
										else
										{

											opPushArgs.opCode = OpCode.push_parameter_skipcheck_testnative;
										}

									}

								});
							}
							else
							{
								opPushArgs.opCode = OpCode.push_parameter_para;
							}
						}
					}
				}

            }

            env.block.opSteps.AddRange(toadd);

        }

		private bool checkFunctionNotHasOverridesOrSealed(ASBinCode.rtti.FunctionDefine func, Builder builder)
		{
			
			if (!func.isMethod)
				return true;

			if (func.IsAnonymous)
				return true;

			if (builder._signature_belone.ContainsKey(func.signature))
			{
				var obj = builder._signature_belone[func.signature];
				if (obj == null)
				{
					return false;
				}
				else if (obj is ASBinCode.rtti.Class)
				{
					ASBinCode.rtti.Class cls = (ASBinCode.rtti.Class)obj;

					if (cls.isInterface)
					{
						return false;
					}
					else if (cls.final)
					{
						return true;
					}
					else
					{
						//*** 确定是否含有虚方法***
						foreach (var member in cls.classMembers)
						{
							MethodGetterBase method = member.bindField as MethodGetterBase;
							if (method != null && method.functionId == func.functionid)
							{
								if (member.isPrivate)
								{
									return true;
								}
								if (member.isFinal)
								{
									return true;
								}
								if (member.isConstructor)
									return true;
								//***无法确定***

							}

						}
						//return true;
					}
				}
				else
				{
					return true;
				}
			}
			

			return false;
		}

		private ASBinCode.rtti.FunctionDefine findFunction(ASBinCode.rtti.FunctionSignature signature,Builder builder,out bool findsuccess)
		{
			//if (builder._signature_define.ContainsKey(signature))
			{
				findsuccess = true;


				var func = builder._signature_define[signature];

				if (func != null && func.isNative)
				{
					try
					{
						var nf = builder.bin.getNativeFunction(func.native_name);

						if (nf == null)
						{
							if (builder.options.CheckNativeFunctionSignature)
							{
								throw new NullReferenceException("没有找到对应的本地函数");
							}
							else
							{

								//findsuccess = false;
								//return null;
							}
						}

						//if (nf.mode
						//		== NativeFunctionBase.NativeFunctionMode.const_parameter_0)
						//{
						//	return func;
						//}
					}
					catch (KeyNotFoundException)
					{
						if (builder.options.CheckNativeFunctionSignature)
						{
							throw;
						}
						else
						{
							findsuccess = false;
							return null;
						}
					}


				}

				return func;
			}
			//findsuccess = false;
			//return null;
		}

		private bool isNativeModeConstPara(ASBinCode.rtti.FunctionSignature signature, Builder builder,out bool findsuccess)
		{

			var func = findFunction(signature, builder,out findsuccess);
			if (func != null && func.isNative)
			{
				var nf = builder.bin.getNativeFunction(func.native_name);

				if (nf == null && !builder.options.CheckNativeFunctionSignature)
				{
					//***如果跳过了本地函数检查签名，那么如果是link_system的，肯定是
					if (builder._signature_belone.ContainsKey(signature))
					{
						var obj = builder._signature_belone[signature];
						if (obj == null)
						{
							findsuccess = false;
							return false;
						}
						else if (obj is ASBinCode.rtti.Class)
						{
							ASBinCode.rtti.Class cls = (ASBinCode.rtti.Class)obj;
							if (!cls.isLink_System)
							{
								findsuccess = false;
								return false;
							}
							else
							{
								return true;
							}
						}
						else
						{
							findsuccess = false;
							return false;
						}
					}
					else
					{
						findsuccess = false;
						return false;
					}


				}
				else if (nf.mode== NativeFunctionBase.NativeFunctionMode.const_parameter_0)
				{
					return true;
				}
			}
			return false;
		}

		private bool isNativeNotModeConstPara(ASBinCode.rtti.FunctionSignature signature, Builder builder, out bool findsuccess)
		{
			var func = findFunction(signature, builder, out findsuccess);
			if (func != null && !func.isNative)
			{
				return false;
			}
			else if (func != null)
			{
				#region 不检查本地函数存在的情况

				var nf = builder.bin.getNativeFunction(func.native_name);

				if (nf == null && !builder.options.CheckNativeFunctionSignature)
				{
					findsuccess = false;
					return false;
					
				}



				#endregion


				#region 检查本地函数存在的情况

				if (builder._signature_belone.ContainsKey(signature))
				{
					var obj = builder._signature_belone[signature];
					if (obj == null)
					{
						return false;
					}
					else if (obj is ASBinCode.rtti.Class)
					{
						ASBinCode.rtti.Class cls = (ASBinCode.rtti.Class)obj;
						if (cls.isInterface)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
					else
					{
						return true;
					}
				}
				else
				{
					return false;
				}

				#endregion
			}
			else
			{
				return false;
			}
		}

		private bool isNotNative(ASBinCode.rtti.FunctionSignature signature, Builder builder, out bool findsuccess)
		{
			var func = findFunction(signature, builder, out findsuccess);
			if (func != null && func.isNative)
			{
				return false;
			}
			else if (func != null)
			{
				if (builder._signature_belone.ContainsKey(signature))
				{
					var obj = builder._signature_belone[signature];
					if (obj == null)
					{
						return false;
					}
					else if (obj is ASBinCode.rtti.Class)
					{
						ASBinCode.rtti.Class cls = (ASBinCode.rtti.Class)obj;
						if (cls.isInterface)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
					else
					{
						return true;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private bool isYield(ASBinCode.rtti.FunctionSignature signature, Builder builder, out bool findsuccess)
		{
			var func = findFunction(signature, builder, out findsuccess);
			if (func != null && func.isYield)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool isNotYieldAndNotNative(ASBinCode.rtti.FunctionSignature signature, Builder builder,out bool findsuccess)
		{
			bool checknative;
			if (isNotNative(signature, builder, out checknative))
			{
				if (checknative)
				{
					bool checkyield;

					if (isYield(signature, builder, out checkyield))
					{
						findsuccess = checkyield;
						return false;
					}
					else
					{
						findsuccess = checkyield;
						if (checkyield)
						{
							return true;
						}
						else
						{
							return false;
						}
						
					}

				}
				else
				{
					findsuccess = checknative;
					return false;
				}
			}
			else
			{
				findsuccess = checknative;
				return false;
			}
		}

	}
}
