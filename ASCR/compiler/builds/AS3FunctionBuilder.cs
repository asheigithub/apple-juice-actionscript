using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3FunctionBuilder
    {
        public class NamedFunction
        {
            public NamedFunction(ASBinCode.rtti.FunctionDefine function,
                Variable member
                )
            {
                this.function = function;
                this.member = member;
            }

            public readonly ASBinCode.rtti.FunctionDefine function;
            public readonly Variable member;
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
                    IRightValue returnValue = ExpressionBuilder.getRightValue(env, expression.Value, expression.token, builder);
                    if (returnValue.valueType == RunTimeDataType.rt_function)
                    {
                        needappendbindscope = true;
                    }
                    

                    var retType = getFunReturnType(as3function);

                    if (retType != returnValue.valueType)
                    {
                        if (!ASRuntime.TypeConverter.testImplicitConvert(returnValue.valueType, retType))
                        {
                            throw new BuildException(as3return.Token.line, as3return.Token.ptr, as3return.Token.sourceFile,
                                "函数返回类型不匹配,不能将" + returnValue.valueType +"转化为" + retType );
                        }

                        returnValue= ExpressionBuilder.addCastOpStep(
                            env, returnValue, retType, 
                            new SourceToken(expression.token.line, expression.token.ptr, expression.token.sourceFile));
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

        private bool detectingReturn(CodeBlock block)
        {
            return findReturn(block.opSteps,0,new int[] { });
        }
        private bool findReturn(List<OpStep> commands,int st,int[] hasvisited)
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

                if (op.opCode == OpCode.function_return)
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

                    return findReturn(commands, i + 1,add) && findReturn(commands, line,add);
                }
                
            }

            return false;

        }


        private RunTimeDataType getFunReturnType(ASTool.AS3.AS3Function as3function)
        {
            var ret= TypeReader.fromSourceCodeStr(as3function.TypeStr, as3function.token);
            if (as3function.TypeStr == "void")
            {
                return RunTimeDataType.fun_void;
            }
            else
            {
                return ret;
            }
        }

        public ASBinCode.rtData.rtFunction buildAS3Function(CompileEnv env, ASTool.AS3.AS3Function as3function, Builder builder,Variable addIntoNamedFunction)
        {
            if (builder.buildoutfunctions.ContainsKey(as3function))
            {
                if (addIntoNamedFunction != null)
                {
                    if (!builder.dictNamedFunctions.ContainsKey(env.block.id))
                    {
                        builder.dictNamedFunctions.Add(env.block.id, new List<builds.AS3FunctionBuilder.NamedFunction>());
                    }
                    builder.dictNamedFunctions[env.block.id].Add(new builds.AS3FunctionBuilder.NamedFunction(
                        builder.bin.functions[
                            builder.buildoutfunctions[as3function].functionId
                        ]
                        ,
                        
                        addIntoNamedFunction));
                }


                return builder.buildoutfunctions[as3function];
            }

            builder.buildingfunctons.Push(as3function);

            ASBinCode.CodeBlock block = new ASBinCode.CodeBlock(builder.getBlockId(),env.block.name +"::"+as3function.Name);
            ASBinCode.scopes.FunctionScope funcscope = new ASBinCode.scopes.FunctionScope();
            funcscope.parentScope = env.block.scope;
            block.scope = funcscope;

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
                        ASBinCode.rtti.FunctionParameter para= buildParameter(block, parameter, builder, env);
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

                    ASBinCode.rtti.FunctionParameter para = buildParameter(block, parameter, builder, env);
                    parameterList.Add(para);
                }
            }
            

            ASBinCode.rtti.FunctionDefine function = new ASBinCode.rtti.FunctionDefine(builder.getFunctionId());
            
            function.blockid = block.id;
            function.parameters = parameterList;
            function.IsAnonymous = as3function.IsAnonymous;
            function.name = as3function.Name;
            function.returnType = getFunReturnType(as3function);


            if (addIntoNamedFunction !=null)
            {
                if (!builder.dictNamedFunctions.ContainsKey(env.block.id))
                {
                    builder.dictNamedFunctions.Add(env.block.id, new List<builds.AS3FunctionBuilder.NamedFunction>());
                }
                builder.dictNamedFunctions[env.block.id].Add(new builds.AS3FunctionBuilder.NamedFunction(function, addIntoNamedFunction));
            }

            builder.buildCodeBlock(as3function.StamentsStack.Peek(), block);

            
            if (function.returnType != RunTimeDataType.fun_void &&
                function.returnType != RunTimeDataType.rt_void &&
                !function.IsAnonymous
                )
            {
                //查找是否所有分支均有return.
                if (!detectingReturn(block))
                {
                    throw new BuildException(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                               "Function does not return a value.");
                }
            }

            builder.bin.functions[function.functionid]=function;

            ASBinCode.rtData.rtFunction func = new ASBinCode.rtData.rtFunction(function.functionid);
            //func.returnType = TypeReader.fromSourceCodeStr(as3function.TypeStr, as3function.token); 

            builder.buildingfunctons.Pop();

            builder.buildoutfunctions.Add(as3function, func);
            
            return func;
        }

        private ASBinCode.rtti.FunctionParameter buildParameter(CodeBlock block, ASTool.AS3.AS3Parameter parameter, Builder builder,CompileEnv env)
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

            Variable argement = new Variable(parameter.Name, block.scope.members.Count, block.id);
            block.scope.members.Add(argement);
            argement.valueType = TypeReader.fromSourceCodeStr(parameter.TypeStr, parameter.token);

            ASBinCode.rtti.FunctionParameter para = new ASBinCode.rtti.FunctionParameter();
            para.name = parameter.Name;
            para.type = TypeReader.fromSourceCodeStr(parameter.TypeStr , parameter.token ); 

            if (parameter.ValueExpr != null)
            {
                IRightValue defaultv;

                var testEval = ExpressionEval.Eval(parameter.ValueExpr);
                if (testEval != null && !parameter.ValueExpr.Value.IsReg)
                {
                    defaultv = new ASBinCode.rtData.RightValue(testEval);

                    para.defaultValue = defaultv;
                }
                else
                {
                    throw new BuildException(parameter.token.line, parameter.token.ptr, parameter.token.sourceFile,
                        "Parameter initializer unknown or is not a compile-time constant. An initial value of undefined will be used instead.");
                }
            }
            return para;
        }
    }
}
