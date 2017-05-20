using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3FunctionBuilder
    {
        public void buildAS3YieldReturn(CompileEnv env, ASTool.AS3.AS3YieldReturn  as3yieldreturn, Builder builder)
        {
            if (builder.buildingfunctons.Count > 0)
            {
                ASTool.AS3.AS3Function as3function = builder.buildingfunctons.Peek();

                if (as3function.IsConstructor)
                {
                    throw new BuildException(as3yieldreturn.Token.line, as3yieldreturn.Token.ptr, as3yieldreturn.Token.sourceFile,
                            "构造函数不能用yield");
                }

                var retType = getFunReturnType(as3function, builder);

                if (retType != builder.bin.IEnumeratorInterface.getRtType())
                {
                    if (  !ASRuntime.TypeConverter.testImplicitConvert(retType, builder.bin.IEnumeratorInterface.getRtType(), builder))
                    {
                        throw new BuildException(as3yieldreturn.Token.line, as3yieldreturn.Token.ptr, as3yieldreturn.Token.sourceFile,
                            "yield 函数必须返回IEnumerator");
                    }

                }


                if (as3yieldreturn.ReturnValue != null)
                {
                    builder.buildStmt(env, as3yieldreturn.ReturnValue);

                    ASTool.AS3.AS3Expression expression = as3yieldreturn.ReturnValue.as3exprlist[
                        as3yieldreturn.ReturnValue.as3exprlist.Count - 1
                        ];

                    bool needappendbindscope = false;
                    RightValueBase returnValue = ExpressionBuilder.getRightValue(env, expression.Value, expression.token, builder);
                    if (returnValue.valueType == RunTimeDataType.rt_function)
                    {
                        needappendbindscope = true;
                    }


                    if (needappendbindscope)
                    {
                        //**绑定环境**
                        OpStep stepbind = new OpStep(OpCode.bind_scope, new SourceToken(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile));
                        stepbind.reg = null;
                        stepbind.regType = RunTimeDataType.unknown;
                        stepbind.arg1 = returnValue;
                        stepbind.arg1Type = returnValue.valueType;
                        stepbind.arg2 = null;
                        stepbind.arg2Type = RunTimeDataType.unknown;

                        env.block.opSteps.Add(stepbind);
                    }

                    OpStep opreturn = new OpStep(OpCode.yield_return,
                        new SourceToken(as3yieldreturn.Token.line, as3yieldreturn.Token.ptr, as3yieldreturn.Token.sourceFile));
                    opreturn.arg1 = returnValue;
                    opreturn.arg1Type = returnValue.valueType;

                    env.block.opSteps.Add(opreturn);

                }
                else
                {
                    OpStep opreturn = new OpStep(OpCode.yield_return,
                        new SourceToken(as3yieldreturn.Token.line, as3yieldreturn.Token.ptr, as3yieldreturn.Token.sourceFile));
                    opreturn.arg1 = new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtUndefined.undefined);
                    opreturn.arg1Type = RunTimeDataType.rt_void;

                    env.block.opSteps.Add(opreturn);
                }

                int yield_id = env.getLabelId();
                OpStep yield_flag = new OpStep(OpCode.flag,
                    new SourceToken(as3yieldreturn.Token.line, as3yieldreturn.Token.ptr, as3yieldreturn.Token.sourceFile));

                yield_flag.flag = "YIELD_FLAG_" + yield_id;
                env.block.opSteps.Add(yield_flag);

            }
            else
            {
                throw new BuildException(as3yieldreturn.Token.line, as3yieldreturn.Token.ptr, as3yieldreturn.Token.sourceFile,
                            "yield return 只能在函数中");
            }
        }


        public void buildAS3Return(CompileEnv env, ASTool.AS3.AS3Return as3return, Builder builder)
        {
            if (builder.buildingfunctons.Count > 0)
            {
                ASTool.AS3.AS3Function as3function = builder.buildingfunctons.Peek();

                if (as3function.TypeStr == "void" && as3return.ReturnValue !=null)
                {
                    throw new BuildException(as3return.Token.line, as3return.Token.ptr, as3return.Token.sourceFile,
                            "A return value is not allowed because the return type of this function is 'void'.");
                }
                

                if (as3return.ReturnValue != null)
                {
                    builder.buildStmt(env, as3return.ReturnValue);

                    ASTool.AS3.AS3Expression expression = as3return.ReturnValue.as3exprlist[
                        as3return.ReturnValue.as3exprlist.Count - 1
                        ];

                    bool needappendbindscope = false;
                    RightValueBase returnValue = ExpressionBuilder.getRightValue(env, expression.Value, expression.token, builder);
                    if (returnValue.valueType == RunTimeDataType.rt_function)
                    {
                        needappendbindscope = true;
                    }
                    

                    var retType = getFunReturnType(as3function,builder);

                    if (retType != returnValue.valueType)
                    {
                        if (!ASRuntime.TypeConverter.testImplicitConvert(returnValue.valueType, retType,builder))
                        {
                            throw new BuildException(as3return.Token.line, as3return.Token.ptr, as3return.Token.sourceFile,
                                "函数返回类型不匹配,不能将" + returnValue.valueType +"转化为" + retType );
                        }

                        returnValue= ExpressionBuilder.addCastOpStep(
                            env, returnValue, retType, 
                            new SourceToken(expression.token.line, expression.token.ptr, expression.token.sourceFile),builder);
                    }


                    if (needappendbindscope)
                    {
                        //**绑定环境**
                        OpStep stepbind = new OpStep(OpCode.bind_scope, new SourceToken(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile));
                        stepbind.reg = null;
                        stepbind.regType = RunTimeDataType.unknown;
                        stepbind.arg1 = returnValue;
                        stepbind.arg1Type = returnValue.valueType;
                        stepbind.arg2 = null;
                        stepbind.arg2Type = RunTimeDataType.unknown;

                        env.block.opSteps.Add(stepbind);
                    }

                    OpStep opreturn = new OpStep(OpCode.function_return,
                        new SourceToken(as3return.Token.line, as3return.Token.ptr, as3return.Token.sourceFile));
                    opreturn.arg1 = returnValue;
                    opreturn.arg1Type = returnValue.valueType;

                    env.block.opSteps.Add(opreturn);

                }
                else
                {
                    OpStep opreturn = new OpStep(OpCode.function_return,
                        new SourceToken(as3return.Token.line, as3return.Token.ptr, as3return.Token.sourceFile));
                    opreturn.arg1 = new ASBinCode.rtData.RightValue( ASBinCode.rtData.rtUndefined.undefined);
                    opreturn.arg1Type = RunTimeDataType.rt_void;

                    env.block.opSteps.Add(opreturn);
                }
                
            }
            else
            {
                throw new BuildException(as3return.Token.line, as3return.Token.ptr, as3return.Token.sourceFile,
                            "The return statement cannot be used in global initialization code.");
            }

        }

        private bool existsOperator(CodeBlock block, OpCode operatorCode)
        {
            for (int i = 0; i < block.opSteps.Count; i++)
            {
                if (block.opSteps[i].opCode == operatorCode)
                {
                    return true;
                }
            }
            return false;
        }

        private bool detectingOperator(CodeBlock block,OpCode operatorCode)
        {
            return findReturn(block.opSteps,0,new int[] { },operatorCode);
        }
        private bool findReturn(List<OpStep> commands,int st,int[] hasvisited, OpCode operatorCode)
        {
            Dictionary<int, object> visited = new Dictionary<int, object>();
            for (int i = 0; i < hasvisited.Length; i++)
            {
                visited.Add(hasvisited[i],null);
            }

            for (int i = st; i < commands.Count  ; i++)
            {
                OpStep op = commands[i];

                if (visited.ContainsKey(i))
                {
                    return true;
                }

                visited.Add(i, null);

                if (op.opCode == operatorCode || op.opCode == OpCode.raise_error)
                {
                    return true;
                }
                else if (op.opCode == OpCode.jmp)
                {
                    i += op.jumoffset - 1;
                }
                else if (op.opCode == OpCode.if_jmp)
                {
                    int line = i + op.jumoffset ;

                    int[] add = new int[visited.Keys.Count ];
                    visited.Keys.CopyTo(add, 0);

                    return findReturn(commands, i + 1,add,operatorCode) && findReturn(commands, line,add,operatorCode);
                }
                
            }

            return false;

        }


        private RunTimeDataType getFunReturnType(ASTool.AS3.AS3Function as3function,Builder builder)
        {
            string tofindtypename = as3function.TypeStr;
            if (as3function.TypeDefine != null)
            {
                if (as3function.TypeDefine is ASTool.AS3.AS3Class)
                {
                    tofindtypename = ((ASTool.AS3.AS3Class)as3function.TypeDefine).Name;
                }
                else if (as3function.TypeDefine is ASTool.AS3.AS3Interface)
                {
                    tofindtypename = ((ASTool.AS3.AS3Interface)as3function.TypeDefine).Name;
                }
            }



            var ret= TypeReader.fromSourceCodeStr(tofindtypename,as3function.token,builder);
            if (as3function.TypeStr == "void")
            {
                return RunTimeDataType.fun_void;
            }
            else
            {
                return ret;
            }
        }

        public void buildParameterDefaultValue(ASTool.AS3.AS3Parameter parameter,
            ASBinCode.rtti.FunctionParameter para,List<ASBinCode.rtti.Class> imports,Builder builder)
        {
            RightValueBase defaultv;

            builder._currentImports.Push(imports);

            var testEval = ExpressionEval.Eval(parameter.ValueExpr,builder);
            if (testEval != null) // && !parameter.ValueExpr.Value.IsReg)
            {
                if (testEval.rtType == RunTimeDataType.fun_void ||
                    testEval.rtType == RunTimeDataType.rt_function ||
                    testEval.rtType > RunTimeDataType.unknown
                    )
                {
                    throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                    "Parameter initializer unknown or is not a compile-time constant. An initial value of undefined will be used instead.");
                }


                
                defaultv = new ASBinCode.rtData.RightValue( testEval);
                

                
                para.defaultValue = defaultv;
            }
            else
            {
                throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                    "Parameter initializer unknown or is not a compile-time constant. An initial value of undefined will be used instead.");
            }

            builder._currentImports.Pop();
        }

        private ASBinCode.rtti.FunctionParameter buildSignatureParameter(ASTool.AS3.AS3Parameter parameter,Builder builder)
        {
            {
                ASBinCode.rtti.FunctionParameter para = new ASBinCode.rtti.FunctionParameter();
                para.name = parameter.Name;
                para.type = TypeReader.fromSourceCodeStr(parameter.TypeStr, parameter.token,builder);

                if (parameter.ValueExpr != null)
                {
                    //先临时赋值占位
                    para.defaultValue = new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtUndefined.undefined);

                    List<ASBinCode.rtti.Class> imps = new List<ASBinCode.rtti.Class>();
                    if (builder._currentImports.Count > 0)
                    {
                        imps.AddRange(builder._currentImports.Peek());
                    }

                    builder._toEvalDefaultParameters.Add(parameter, new utils.Tuple<ASBinCode.rtti.FunctionParameter, List<ASBinCode.rtti.Class>>(para,imps));

                    //IRightValue defaultv;

                    //var testEval = ExpressionEval.Eval(parameter.ValueExpr);
                    //if (testEval != null && !parameter.ValueExpr.Value.IsReg)
                    //{
                    //    defaultv = new ASBinCode.rtData.RightValue(testEval);

                    //    para.defaultValue = defaultv;
                    //}
                    //else
                    //{
                    //    throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                    //        "Parameter initializer unknown or is not a compile-time constant. An initial value of undefined will be used instead.");
                    //}
                }
                return para;
            }
        }

        public ASBinCode.rtti.FunctionSignature buildSignature(CompileEnv env, ASTool.AS3.AS3Function as3function, Builder builder)
        {
            bool isdefault = false;
            List<ASBinCode.rtti.FunctionParameter> parameterList = new List<ASBinCode.rtti.FunctionParameter>();
            //***参数***
            for (int i = 0; i < as3function.Parameters.Count; i++)
            {
                ASTool.AS3.AS3Parameter parameter = as3function.Parameters[i];

                if (parameter.IsArrPara)
                {
                    if (i != as3function.Parameters.Count - 1)
                    {
                        throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                            "Rest parameter must be last");
                    }
                    else
                    {
                        ASBinCode.rtti.FunctionParameter para = buildSignatureParameter(parameter,builder);
                        para.isPara = true;
                        parameterList.Add(para);
                    }
                }
                else
                {
                    if (parameter.ValueExpr == null && isdefault)
                    {
                        throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                                "Required parameters are not permitted after optional parameters.");
                    }
                    if (parameter.ValueExpr != null)
                    {
                        isdefault = true;
                    }

                    ASBinCode.rtti.FunctionParameter para = buildSignatureParameter(parameter,builder);

                    for (int j = 0; j < parameterList.Count; j++)
                    {
                        if (parameterList[j].name == para.name)
                        {
                            throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                                "形参名重复");
                        }
                    }

                    parameterList.Add(para);
                }
            }

            ASBinCode.rtti.FunctionSignature signature = new ASBinCode.rtti.FunctionSignature();
            signature.parameters = parameterList;
            signature.returnType = getFunReturnType(as3function,builder);
            return signature;

        }

        public ASBinCode.rtData.rtFunction buildAS3Function(CompileEnv env, ASTool.AS3.AS3Function as3function, Builder builder,ASBinCode.rtti.FunctionSignature signature)
        {
            if (builder.buildoutfunctions.ContainsKey(as3function))
            {
                //return builder.buildoutfunctions[as3function];
                return new ASBinCode.rtData.rtFunction(builder.buildoutfunctions[as3function].functionid
                    , builder.buildoutfunctions[as3function].isMethod

                    );
            }

            builder.buildingfunctons.Push(as3function);

            IScope scope = env.block.scope;
            while (!(scope is ASBinCode.scopes.ObjectInstanceScope || scope is ASBinCode.scopes.OutPackageMemberScope))
            {
                scope = scope.parentScope;
            }

            int defineclassid; bool isoutclass;ASBinCode.rtti.Class refClass = null;
            if (scope is ASBinCode.scopes.ObjectInstanceScope)
            {
                refClass = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                defineclassid = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class.classid;
                isoutclass = false;
            }
            else
            {
                defineclassid = ((ASBinCode.scopes.OutPackageMemberScope)scope).mainclass.classid;
                isoutclass = true;
            }


            int function_codeblock_id = builder.getBlockId();
            ASBinCode.CodeBlock block = new ASBinCode.CodeBlock(function_codeblock_id,
                env.block.name + "::" +
                (as3function.IsAnonymous ? "$" + function_codeblock_id + "anonymous" : as3function.Name)
                ,defineclassid,isoutclass
                );

            if (signature == null)
            {
                signature = buildSignature(env, as3function, builder);
            }

            ASBinCode.rtti.FunctionDefine function = new ASBinCode.rtti.FunctionDefine(builder.getFunctionId());

            function.blockid = block.id;
            function.IsAnonymous = as3function.IsAnonymous;
            function.name = as3function.Name;
            function.signature = signature;
            function.isConstructor = as3function.IsConstructor;
            function.isMethod = as3function.IsMethod;
            function.isStatic = as3function.Access.IsStatic;

            if (function.isConstructor && function.signature.returnType != RunTimeDataType.rt_void)
            {
                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                               "A Constructor cannot specify a return type.");
            }


            //****查找meta***
            if (as3function.Meta != null)
            {
                for (int i = 0; i < as3function.Meta.Count; i++)
                {
                    var m = as3function.Meta[i];
                    if (m.Value.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_array)
                    {
                        List<ASTool.AS3.Expr.AS3DataStackElement> data =
                            (List<ASTool.AS3.Expr.AS3DataStackElement>)m.Value.Data.Value;

                        if (data.Count == 0)
                        {
                            throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                           "Meta 格式错误,至少有1个设置");
                        }
                        else
                        {
                            string meta = data[0].Data.Value.ToString();
                            if (meta == "implicit_to")
                            {
                                #region "implicit_to"

                                if (isoutclass)
                                {
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_to特性不能在包外定义");
                                }
                                else
                                {
                                    ASBinCode.rtti.Class iclass = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                                    if (iclass.instanceClass == null)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_to特性只能定义在静态方法上");
                                    }
                                    if (!as3function.Access.IsPrivate)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_to特性必须为private");
                                    }
                                    if (function.signature.parameters.Count != 1 ||
                                        function.signature.parameters[0].type != RunTimeDataType.rt_void)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit特性函数必须有1个参数,类型为*");
                                    }

                                    if (function.signature.returnType == RunTimeDataType.fun_void ||
                                        function.signature.returnType == RunTimeDataType.rt_void)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_to特性函数必须有确定的返回值");
                                    }
                                    if (function.signature.returnType > RunTimeDataType.unknown)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_to特性函数必须返回原始值");
                                    }

                                    for (int j = 0; j < iclass.classMembers.Count; j++)
                                    {
                                        ASBinCode.rtti.ClassMember member = iclass.classMembers[j];
                                        if (member.valueType == RunTimeDataType.rt_function
                                            &&
                                            member.name == function.name
                                            )
                                        {
                                            ((MethodGetterBase)member.bindField).setNotReadVirtual();
                                            iclass.implicit_to = member;
                                            iclass.implicit_to_functionid = function.functionid;
                                            iclass.implicit_to_type = function.signature.returnType;
                                            break;
                                        }
                                    }

                                }

                                #endregion

                            }
                            else if (meta == "implicit_from")
                            {
                                #region "implicit_from"

                                if (isoutclass)
                                {
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_from特性不能在包外定义");
                                }
                                else
                                {
                                    ASBinCode.rtti.Class iclass = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                                    if (iclass.instanceClass == null)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_from特性只能定义在静态方法上");
                                    }
                                    if (!as3function.Access.IsPrivate)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_from特性必须为private");
                                    }
                                    if (function.signature.parameters.Count != 1 ||
                                        function.signature.parameters[0].type == RunTimeDataType.rt_void
                                        //||
                                        //function.signature.parameters[0].type == RunTimeDataType.rt_function
                                        ||
                                        function.signature.parameters[0].type > RunTimeDataType.unknown
                                        )
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_from特性函数必须有1个参数,且确定为原始数据类型");
                                    }

                                    if (function.signature.returnType != RunTimeDataType.rt_void)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "implicit_from特性函数返回类型必须是void"); //将来做了继承后可能会改为Object
                                    }

                                    for (int j = 0; j < iclass.classMembers.Count; j++)
                                    {
                                        ASBinCode.rtti.ClassMember member = iclass.classMembers[j];
                                        if (member.valueType == RunTimeDataType.rt_function
                                            &&
                                            member.name == function.name
                                            )
                                        {
                                            ((MethodGetterBase)member.bindField).setNotReadVirtual();
                                            iclass.implicit_from = member;
                                            iclass.implicit_from_functionid = function.functionid;
                                            iclass.implicit_from_type = function.signature.parameters[0].type;
                                            function.signature.returnType = iclass.instanceClass.getRtType();

                                            if (builder.bin.primitive_to_class_table[function.signature.parameters[0].type] == null)
                                            {
                                                builder.bin.primitive_to_class_table[function.signature.parameters[0].type] = iclass.instanceClass;
                                            }
                                            else
                                            {
                                                if (iclass.instanceClass.isLink_System
                                                    &&
                                                    iclass.instanceClass.classid > builder.bin.primitive_to_class_table[function.signature.parameters[0].type].classid
                                                    &&
                                                    iclass.final
                                                    )
                                                {
                                                }
                                                else
                                                {
                                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                        function.signature.parameters[0].type.ToString() + "的类型转换不明确"); //将来做了继承后可能会改为Object
                                                }
                                            }
                                            //as3function.TypeStr = iclass.instanceClass.name;
                                            break;
                                        }
                                    }

                                }

                                #endregion

                            }
                            else if (meta == "explicit_from")
                            {
                                #region "explicit_from"
                                if (isoutclass)
                                {
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "explicit_from特性不能在包外定义");
                                }
                                else
                                {
                                    ASBinCode.rtti.Class iclass = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                                    if (iclass.instanceClass == null)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "explicit_from特性只能定义在静态方法上");
                                    }
                                    if (!as3function.Access.IsPrivate)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "explicit_from特性必须为private");
                                    }
                                    //if (function.signature.parameters.Count != 1 ||
                                    //    function.signature.parameters[0].type == RunTimeDataType.rt_void
                                    //    ||
                                    //    function.signature.parameters[0].type == RunTimeDataType.rt_function
                                    //    ||
                                    //    function.signature.parameters[0].type > RunTimeDataType.unknown
                                    //    )
                                    //{
                                    //    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                    //    "explicit_from特性函数必须有1个参数,且确定为原始数据类型");
                                    //}

                                    if (function.signature.returnType != RunTimeDataType.rt_void)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "explicit_from特性函数返回类型必须是*");
                                    }

                                    for (int j = 0; j < iclass.classMembers.Count; j++)
                                    {
                                        ASBinCode.rtti.ClassMember member = iclass.classMembers[j];
                                        if (member.valueType == RunTimeDataType.rt_function
                                            &&
                                            member.name == function.name
                                            )
                                        {
                                            ((MethodGetterBase)member.bindField).setNotReadVirtual();
                                            iclass.explicit_from = member;
                                            iclass.explicit_from_functionid = function.functionid;
                                            iclass.explicit_from_type = function.signature.parameters[0].type;
                                            function.signature.returnType = iclass.instanceClass.getRtType();

                                            break;
                                        }
                                    }

                                }
                                #endregion
                            }
                            else if (meta == "creator")
                            {
                                if (isoutclass)
                                {
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "creator特性不能在包外定义");
                                }
                                else
                                {
                                    ASBinCode.rtti.Class iclass = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                                    if (iclass.instanceClass == null)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "creator特性只能定义在静态方法上");
                                    }
                                    if (!as3function.Access.IsPrivate)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "creator特性必须为private");
                                    }
                                    if (function.signature.parameters.Count != 1
                                        ||
                                        function.signature.parameters[0].type != builder.bin.TypeClass.getRtType()
                                        )
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "creator特性函数的参数必须是要创建的Class");
                                    }

                                    if (function.signature.returnType != RunTimeDataType.rt_void)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "creator特性函数返回类型必须是*");
                                    }

                                    for (int j = 0; j < iclass.classMembers.Count; j++)
                                    {
                                        ASBinCode.rtti.ClassMember member = iclass.classMembers[j];
                                        if (member.valueType == RunTimeDataType.rt_function
                                            &&
                                            member.name == function.name
                                            )
                                        {
                                            ((MethodGetterBase)member.bindField).setNotReadVirtual();
                                            iclass.linkObjCreator = member;
                                            function.signature.returnType = iclass.instanceClass.getRtType();

                                            break;
                                        }
                                    }

                                }
                            }
                            else if (meta == "novisual")
                            {
                                if (isoutclass)
                                {
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "novisual特性不能在包外定义");
                                }
                                else
                                {
                                    ASBinCode.rtti.Class iclass = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                                    for (int j = 0; j < iclass.classMembers.Count; j++)
                                    {
                                        ASBinCode.rtti.ClassMember member = iclass.classMembers[j];
                                        if (member.valueType == RunTimeDataType.rt_function
                                            &&
                                            member.name == function.name
                                            )
                                        {
                                            ((MethodGetterBase)member.bindField).setNotReadVirtual();
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (meta == "native")
                            {
                                if (data.Count == 2)
                                {
                                    string native_to = data[1].Data.Value.ToString();

                                    if (builder.bin.nativefunctionNameIndex.ContainsKey(native_to))
                                    {
                                        bool isvectorScope = false;
                                        RunTimeDataType vt = RunTimeDataType.unknown;
                                        RunTimeDataType vc = RunTimeDataType.unknown;
                                        if (as3function.IsMethod &&
                                            as3function.ParentScope is ASTool.AS3.AS3Class)
                                        {
                                            var ascls = builder.buildingclasses[(ASTool.AS3.AS3Class)as3function.ParentScope];
                                            if (builder.bin.dict_Vector_type.ContainsKey(ascls))
                                            {
                                                vt = builder.bin.dict_Vector_type[ascls];
                                                vc = ascls.getRtType();
                                                isvectorScope = true;
                                            }
                                        }

                                        var nf = builder.bin.nativefunctions[builder.bin.nativefunctionNameIndex[native_to]];
                                        if (as3function.IsMethod == nf.isMethod)
                                        {
                                            if (signature.returnType != nf.returnType
                                                &&
                                                !(isvectorScope && nf.returnType == RunTimeDataType.rt_void
                                                    &&
                                                    signature.returnType == vt
                                                )
                                                &&
                                                !(
                                                isvectorScope && nf.returnType == RunTimeDataType.rt_void
                                                    &&
                                                    signature.returnType == vc
                                                )
                                                &&
                                                !(
                                                    signature.returnType > RunTimeDataType.unknown
                                                    &&
                                                    nf.returnType == RunTimeDataType.rt_void
                                                )
                                                )
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                    "本地函数 " + native_to + " 返回类型不符");
                                            }

                                            if (signature.parameters.Count != nf.parameters.Count)
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                    "本地函数 " + native_to + " 参数数量不符");
                                            }

                                            for (int j = 0; j < signature.parameters.Count; j++)
                                            {


                                                if (signature.parameters[j].type != nf.parameters[j]
                                                    &&
                                                    !(!signature.parameters[j].isPara && isvectorScope && nf.parameters[j] == RunTimeDataType.rt_void)
                                                    &&
                                                    !(signature.parameters[j].isPara && nf.parameters[j] == RunTimeDataType.rt_array)
                                                    &&
                                                    !(signature.parameters[j].type > RunTimeDataType._OBJECT && nf.parameters[j] == RunTimeDataType.rt_void)
                                                    )
                                                {
                                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                        "本地函数 " + native_to + " 参数类型不符");
                                                }
                                            }

                                            function.isNative = true;
                                            function.native_name = native_to;
                                        }
                                        else
                                        {
                                            throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                "本地函数 " + native_to + " isMethod属性不符");
                                        }
                                    }
                                    else
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                            "本地函数 " + native_to + " 未注册");
                                    }
                                }
                                else
                                {
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "native特性需要说明对应的本地函数。用[native,XXXX]格式指定");
                                }
                            }
                            else if (meta == "operator")
                            {
                                if (data.Count == 2)
                                {
                                    string operatorCode = data[1].Data.Value.ToString();
                                    if (isoutclass)
                                    {
                                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                            "operator特性不能在包外定义");
                                    }
                                    else
                                    {
                                        ASBinCode.rtti.Class iclass = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;
                                        if (iclass.instanceClass == null)
                                        {
                                            throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                            "operator特性只能定义在静态方法上");
                                        }
                                        if (!as3function.Access.IsPrivate)
                                        {
                                            throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                            "operator特性必须为private");
                                        }
                                        if (operatorCode == ">")
                                        {
                                            #region >

                                            if (function.signature.returnType != RunTimeDataType.rt_boolean)
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                     "操作符>必须返回Boolean值");
                                            }
                                            if (function.signature.parameters.Count != 2)
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                     "操作符>参数必须是2个");
                                            }
                                            if (function.signature.parameters[0].defaultValue != null
                                                ||
                                                function.signature.parameters[1].defaultValue != null
                                                ||
                                                function.signature.parameters[1].isPara
                                                )
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                     "操作符>参数不能有默认值，也不能是不固定数量");
                                            }
                                            if (function.signature.parameters[0].type == RunTimeDataType.rt_void
                                                ||
                                                function.signature.parameters[1].type == RunTimeDataType.rt_void
                                                )
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                     "操作符>参数类型必须确定");
                                            }
                                            if (function.signature.parameters[0].type < RunTimeDataType.unknown
                                                ||
                                                function.signature.parameters[1].type < RunTimeDataType.unknown
                                                )
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                     "操作符>参数不能都是基本类型");
                                            }
                                            if (function.signature.parameters[0].type > RunTimeDataType.unknown
                                                )
                                            {
                                                var cls = builder.getClassByRunTimeDataType(function.signature.parameters[0].type);
                                                if (cls.staticClass == null || !cls.final)
                                                {
                                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                         "操作符>参数类型必须是final的并且不是Class");
                                                }
                                            }
                                            if (function.signature.parameters[1].type > RunTimeDataType.unknown
                                                )
                                            {
                                                var cls = builder.getClassByRunTimeDataType(function.signature.parameters[1].type);
                                                if (cls.staticClass == null || !cls.final)
                                                {
                                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                         "操作符>参数类型必须是final的并且不是Class");
                                                }
                                            }

                                            if (builder.bin.operatorOverrides.getOperatorFunction(OverrideableOperator.GreatherThan,
                                                function.signature.parameters[0].type,
                                                function.signature.parameters[1].type
                                                ) == null
                                                )
                                            {
                                                builder.bin.operatorOverrides.AddOperatorFunction(OverrideableOperator.GreatherThan, function);
                                            }
                                            else
                                            {
                                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                         "重复的重载操作符>函数.");
                                            }
                                            #endregion

                                        }
                                        else
                                        {
                                            throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                                "操作符"+operatorCode+"不能重载");
                                        }
                                        

                                    }

                                }
                                else
                                {
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "operator需要指定要重载的操作符。用[operator,\"*\"]格式指定");
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                               "Meta 格式错误");
                    }
                }
            }

            if (refClass != null)
            {
                if (refClass.isLink_System)
                {
                    if (!function.isNative)
                    {
                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                   "[link_system]只能包含[native]成员");
                    }
                }
                else if (refClass.instanceClass != null && refClass.instanceClass.isLink_System)
                {
                    if (!function.isNative)
                    {
                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                   "[link_system]只能包含[native]成员");
                    }
                }
            }



            ASBinCode.scopes.FunctionScope funcscope = new ASBinCode.scopes.FunctionScope(function);
            funcscope.parentScope = env.block.scope;
            block.scope = funcscope;

            

            for (int i = 0; i < as3function.Parameters.Count; i++)
            {
                buildParameter(block, as3function.Parameters[i],builder, env);
            }

            bool isinterface = false;
            if (refClass != null && refClass.isInterface)
            {
                //**接口不检查函数返回
                isinterface = true;
            }


            if (!function.isNative && !isinterface)
            {
                builder.buildCodeBlock(as3function.StamentsStack.Peek(), block);

                if (
                    existsOperator(block, OpCode.yield_break)
                    ||
                    existsOperator(block, OpCode.yield_return)
                    )
                {
                    //***如果是yield类函数***
                    if (existsOperator(block, OpCode.function_return))
                    {
                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                   "yield返回和return不能在一起使用");
                    }
                    function.isYield = true;

                    var yieldlineflag = new Variable("@yieldflag", block.scope.members.Count, block.id);
                    yieldlineflag.valueType = RunTimeDataType.rt_int;
                    block.scope.members.Add(yieldlineflag);

                    var yieldismovenext = new Variable("@yieldismovenext", block.scope.members.Count, block.id);
                    yieldismovenext.valueType = RunTimeDataType.rt_boolean;
                    block.scope.members.Add(yieldismovenext);


                    for (int i = 0; i < block.opSteps.Count; i++)
                    {
                        var step = block.opSteps[i];
                        if (step.opCode == OpCode.yield_return)
                        {
                            if (step.tryid != -1)
                            {
                                throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                   "yield无法在try中生成值");
                            }

                            step.arg2 = yieldlineflag;
                            step.arg2Type = RunTimeDataType.rt_int;
                        }
                    }
                    
                }
                else if (function.signature.returnType != RunTimeDataType.fun_void &&
                    function.signature.returnType != RunTimeDataType.rt_void &&
                    !function.IsAnonymous
                    )
                {
                    //查找是否所有分支均有return.
                    if (!detectingOperator(block, OpCode.function_return))
                    {
                        throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                   "Function does not return a value.");
                    }
                }

                if (function.isConstructor)
                {
                    if (!existsOperator(block, OpCode.flag_call_super_constructor))
                    {
                        var pc = ((ASBinCode.scopes.ObjectInstanceScope)funcscope.parentScope)._class.super;
                        if (pc != null)
                        {
                            while (pc !=null && pc.constructor ==null )
                            {
                                pc = pc.super;
                            }

                            if (pc != null)
                            {
                                //有父类构造函数需要调。
                                var sig = builder.dictSignatures[pc.blockid][pc.constructor.bindField];

                                if (sig.parameters.Count != 0
                                    &&
                                    sig.parameters[0].defaultValue == null
                                    )
                                {
                                    //***需要显式调用***
                                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "No default constructor found in base class "+ pc.name +".");
                                }
                                else
                                {
                                    //***自动在第一行加入调用代码****
                                    var c = pc.constructor;
                                    OpStep opMakeArgs = new OpStep(OpCode.make_para_scope, new SourceToken(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile));
                                    opMakeArgs.arg1 = (MethodGetterBase)c.bindField;
                                    opMakeArgs.arg1Type = RunTimeDataType.rt_function;

                                   

                                    OpStep op = new OpStep(OpCode.call_function,
                                            new SourceToken(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile));
                                    var eax = env.getAdditionalRegister();
                                    eax.setEAXTypeWhenCompile(RunTimeDataType.rt_void);
                                    eax.isFuncResult = true;
                                    op.reg = eax;
                                    op.regType = RunTimeDataType.rt_void;

                                    op.arg1 = (MethodGetterBase)c.bindField;
                                    op.arg1Type = RunTimeDataType.rt_function;


                                    env.block.opSteps.Insert(0, op);
                                    env.block.opSteps.Insert(0, opMakeArgs);
                                }
                            }
                        }

                    }
                    else
                    {
                        if (!detectingOperator(block, OpCode.flag_call_super_constructor))  //不是所有路径都调用构造
                        {
                            throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                        "构造函数需要在所有路径调用");
                        }
                    }
                }

            }
            builder.dictfunctionblock.Add(function, block);
            
            ASBinCode.rtData.rtFunction func = 
                new ASBinCode.rtData.rtFunction(
                    function.functionid,
                    function.isMethod);
            
            builder.buildingfunctons.Pop();
            builder.buildoutfunctions.Add(as3function, function);

            return func;
        }

        private void buildParameter(CodeBlock block, ASTool.AS3.AS3Parameter parameter, Builder builder, CompileEnv env)
        {
            for (int i = 0; i < block.scope.members.Count; i++) //scope内查找是否有重复
            {
                IMember member = block.scope.members[i];
                if (member.name == parameter.Name)
                {

                    throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                        "形参名重复");
                }
            }

            VariableBase argement = new Variable(parameter.Name, block.scope.members.Count, block.id);
            block.scope.members.Add(argement);

            if (parameter.IsArrPara)
            {
                argement.valueType = RunTimeDataType.rt_array;
            }
            else
            {
                argement.valueType = TypeReader.fromSourceCodeStr(parameter.TypeStr, parameter.token, builder);
            }

        }





        //public ASBinCode.rtData.rtFunction buildAS3Function(CompileEnv env, ASTool.AS3.AS3Function as3function, Builder builder,object kkk,object mmm)
        //{
        //    if (builder.buildoutfunctions.ContainsKey(as3function))
        //    {
        //        if (addIntoNamedFunction != null)
        //        {
        //            if (!builder.dictNamedFunctions.ContainsKey(env.block.id))
        //            {
        //                builder.dictNamedFunctions.Add(env.block.id, new List<builds.AS3FunctionBuilder.NamedFunction>());
        //            }
        //            builder.dictNamedFunctions[env.block.id].Add(new builds.AS3FunctionBuilder.NamedFunction(
        //                builder.bin.functions[
        //                    builder.buildoutfunctions[as3function].functionId
        //                ]
        //                ,
                        
        //                addIntoNamedFunction));
        //        }


        //        return builder.buildoutfunctions[as3function];
        //    }

        //    builder.buildingfunctons.Push(as3function);

        //    int function_codeblock_id = builder.getBlockId();
        //    ASBinCode.CodeBlock block = new ASBinCode.CodeBlock(function_codeblock_id,
        //        env.block.name +"::"+
        //        (as3function.IsAnonymous?"$"+ function_codeblock_id + "anonymous" : as3function.Name)
        //        );

        //    ASBinCode.scopes.FunctionScope funcscope = new ASBinCode.scopes.FunctionScope();
        //    funcscope.parentScope = env.block.scope;
        //    block.scope = funcscope;

        //    bool isdefault = false;

        //    List<ASBinCode.rtti.FunctionParameter> parameterList = new List<ASBinCode.rtti.FunctionParameter>();
        //    //***参数***
        //    for (int i = 0; i < as3function.Parameters.Count; i++)
        //    {
        //        ASTool.AS3.AS3Parameter parameter = as3function.Parameters[i];

        //        if (parameter.IsArrPara)
        //        {
        //            if (i != as3function.Parameters.Count - 1)
        //            {
        //                throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
        //                    "Rest parameter must be last");
        //            }
        //            else
        //            {
        //                ASBinCode.rtti.FunctionParameter para= buildParameter(block, parameter, builder, env);
        //                para.isPara = true;
        //                parameterList.Add(para);
        //            }
        //        }
        //        else
        //        {
        //            if (parameter.ValueExpr == null && isdefault)
        //            {
        //                throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
        //                        "Required parameters are not permitted after optional parameters.");
        //            }
        //            if (parameter.ValueExpr != null)
        //            {
        //                isdefault = true;
        //            }

        //            ASBinCode.rtti.FunctionParameter para = buildParameter(block, parameter, builder, env);
        //            parameterList.Add(para);
        //        }
        //    }
            

        //    ASBinCode.rtti.FunctionDefine function = new ASBinCode.rtti.FunctionDefine(builder.getFunctionId());
            
        //    function.blockid = block.id;
        //    function.parameters = parameterList;
        //    function.IsAnonymous = as3function.IsAnonymous;
        //    function.name = as3function.Name;
        //    function.returnType = getFunReturnType(as3function);


        //    if (addIntoNamedFunction !=null)
        //    {
        //        if (!builder.dictNamedFunctions.ContainsKey(env.block.id))
        //        {
        //            builder.dictNamedFunctions.Add(env.block.id, new List<builds.AS3FunctionBuilder.NamedFunction>());
        //        }
        //        builder.dictNamedFunctions[env.block.id].Add(new builds.AS3FunctionBuilder.NamedFunction(function, addIntoNamedFunction));
        //    }

        //    builder.buildCodeBlock(as3function.StamentsStack.Peek(), block);

            
        //    if (function.returnType != RunTimeDataType.fun_void &&
        //        function.returnType != RunTimeDataType.rt_void &&
        //        !function.IsAnonymous
        //        )
        //    {
        //        //查找是否所有分支均有return.
        //        if (!detectingReturn(block))
        //        {
        //            throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
        //                       "Function does not return a value.");
        //        }
        //    }

        //    builder.bin.functions[function.functionid]=function;

        //    ASBinCode.rtData.rtFunction func = new ASBinCode.rtData.rtFunction(function.functionid);
        //    //func.returnType = TypeReader.fromSourceCodeStr(as3function.TypeStr, as3function.token); 

        //    builder.buildingfunctons.Pop();

        //    builder.buildoutfunctions.Add(as3function, func);
            
        //    return func;
        //}

        //private ASBinCode.rtti.FunctionParameter buildParameter(CodeBlock block, ASTool.AS3.AS3Parameter parameter, Builder builder,CompileEnv env)
        //{
        //    for (int i = 0; i < block.scope.members.Count; i++) //scope内查找是否有重复
        //    {
        //        IMember member = block.scope.members[i];
        //        if (member.name == parameter.Name)
        //        {

        //            throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
        //                "形参名重复");
        //        }
        //    }

        //    Variable argement = new Variable(parameter.Name, block.scope.members.Count, block.id);
        //    block.scope.members.Add(argement);
        //    argement.valueType = TypeReader.fromSourceCodeStr(parameter.TypeStr, parameter.token);

        //    ASBinCode.rtti.FunctionParameter para = new ASBinCode.rtti.FunctionParameter();
        //    para.name = parameter.Name;
        //    para.type = TypeReader.fromSourceCodeStr(parameter.TypeStr , parameter.token ); 

        //    if (parameter.ValueExpr != null)
        //    {
        //        IRightValue defaultv;

        //        var testEval = ExpressionEval.Eval(parameter.ValueExpr);
        //        if (testEval != null && !parameter.ValueExpr.Value.IsReg)
        //        {
        //            defaultv = new ASBinCode.rtData.RightValue(testEval);

        //            para.defaultValue = defaultv;
        //        }
        //        else
        //        {
        //            throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
        //                "Parameter initializer unknown or is not a compile-time constant. An initial value of undefined will be used instead.");
        //        }
        //    }
        //    return para;
        //}
    }
}
