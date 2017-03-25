using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3ClassBuilder
    {
        private void _doBuildMemberType(ASBinCode.rtti.ClassMember member,Builder builder,ASBinCode.rtti.Class cls)
        {
            ASTool.AS3.IAS3Stmt stmt = builder._buildingmembers[member];

            CompileEnv env;// = builder._classbuildingEnv[cls];

            if (member.isStatic)
            {
                env = builder._classbuildingEnv[cls.staticClass];
            }
            else
            {
                env = builder._classbuildingEnv[cls];
            }

            {
                if (stmt is ASTool.AS3.AS3Variable)
                {
                    member.setTypeWhenCompile(TypeReader.fromSourceCodeStr(((ASTool.AS3.AS3Variable)stmt).TypeStr, stmt.Token, builder));
                }
                else if (stmt is ASTool.AS3.AS3Const)
                {
                    member.setTypeWhenCompile(TypeReader.fromSourceCodeStr(((ASTool.AS3.AS3Const)stmt).TypeStr, stmt.Token, builder));
                }
                else if (stmt is ASTool.AS3.AS3Function)
                {
                    member.setTypeWhenCompile(RunTimeDataType.rt_function);
                    //**编译函数签名
                    builder.buildNamedFunctionSignature(builder._classbuildingEnv[cls], (ASTool.AS3.AS3Function)stmt);

                }
                else
                {
                    throw new Exception("异常的成员类型");
                }

                //((Variable)env.block.scope.members[member.index]).valueType = member.valueType;
                member.bindField.valueType = member.valueType;
            }
        }

        /// <summary>
        /// 编译类的成员类型
        /// </summary>
        /// <param name="as3class"></param>
        /// <param name="builder"></param>
        public void buildMemberType(ASBinCode.rtti.Class cls, Builder builder ,ASTool.AS3.AS3SrcFile as3srcfile )
        {
            

            builder._buildingClass.Push( cls.staticClass);

            for (int i = 0; i < cls.staticClass.classMembers.Count; i++)
            {
                ASBinCode.rtti.ClassMember member = cls.staticClass.classMembers[i];
                _doBuildMemberType(member, builder, cls);
            }
            builder._buildingClass.Pop();

            builder._buildingClass.Push( cls);
            for (int i = 0; i < cls.classMembers.Count; i++)
            {
                ASBinCode.rtti.ClassMember member = cls.classMembers[i];
                _doBuildMemberType(member, builder, cls);
            }
            builder._buildingClass.Pop();
        }

        /// <summary>
        /// 编译类的类型说明
        /// </summary>
        public ASBinCode.rtti.Class buildClassDefine(ASTool.AS3.AS3Class as3class, Builder builder, ASBinCode.rtti.Class mainClass ,ASTool.AS3.AS3SrcFile as3srcfile )
        {
            

            int classid = builder.getClassId();
            int blockid = builder.getBlockId();

            ASBinCode.rtti.Class cls = new ASBinCode.rtti.Class(classid, blockid,builder.bin);
            builder.buildingclasses.Add(as3class, cls);

            cls.package = as3class.Package.Name;
            cls.ispackageout = false;
            cls.isPublic = as3class.Access.IsPublic;
            cls.name = as3class.Name;
            cls.dynamic = as3class.Access.IsDynamic;
            cls.final = as3class.Access.IsFinal;

            if (mainClass != null)
            {
                cls.name = as3class.Name;
                cls.mainClass = mainClass;
                cls.ispackageout = true;
            }

            if (as3class.Meta != null)
            {
                if (!as3class.Meta.Value.IsReg)
                {
                    if (as3class.Meta.Value.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
                    {
                        if (as3class.Meta.Value.Data.Value.ToString() == "Doc")
                        {
                            for (int i = 0; i < builder.buildingclasses.Count; i++)
                            {
                                if (builder.buildingclasses[as3class].isdocumentclass)
                                {
                                    throw new BuildException(as3class.token.line,
                                        as3class.token.ptr, as3class.token.sourceFile,
                                                            "只能有1个文档类");
                                }
                            }

                            if (!cls.ispackageout && cls.isPublic)
                            {
                                cls.isdocumentclass = true;
                            }
                        }
                    }
                }
            }



            List<ASTool.AS3.IAS3Stmt> classstmts = as3class.StamentsStack.Peek();

            ASBinCode.CodeBlock block = new ASBinCode.CodeBlock(blockid, cls.name,cls.classid,false);
            block.scope = new ASBinCode.scopes.ObjectInstanceScope(cls);

            CompileEnv env = new CompileEnv(block, false);
            builder._classbuildingEnv.Add(cls, env);

            for (int i = 0; i < classstmts.Count; i++)
            {
                buildClassMember(env, classstmts[i], cls, builder, false);
            }


            //****编译metaclass***
            int metaclassid = builder.getClassId();
            int metablockid = builder.getBlockId();

            ASBinCode.rtti.Class metaclass = new ASBinCode.rtti.Class(metaclassid, metablockid,builder.bin);
            metaclass.package = as3class.Package.Name;
            metaclass.ispackageout = false;
            metaclass.isPublic = as3class.Access.IsPublic;
            metaclass.name = as3class.Name + "_static";
            metaclass.dynamic = true;
            metaclass.final = true;

            cls.staticClass = metaclass;
            metaclass.instanceClass = cls;

            ASBinCode.CodeBlock metablock = new ASBinCode.CodeBlock(metablockid, metaclass.name,metaclass.classid,false);
            metablock.scope = new ASBinCode.scopes.ObjectInstanceScope(metaclass);

            CompileEnv envMeta = new CompileEnv(metablock, false);
            builder._classbuildingEnv.Add(metaclass, envMeta);

            for (int i = 0; i < classstmts.Count; i++)
            {
                buildClassMember(envMeta, classstmts[i], metaclass, builder, true);
            }

            return cls;
        }


        private void buildClassMember(CompileEnv env,
            ASTool.AS3.IAS3Stmt stmt, 
            ASBinCode.rtti.Class cls ,
            Builder builder,
            bool isstatic)
        {
            if (stmt is ASTool.AS3.AS3Block)
            {
                ASTool.AS3.AS3Block as3block = (ASTool.AS3.AS3Block)stmt;
                for (int i = 0; i < as3block.CodeList.Count; i++)
                {
                    buildClassMember(env, as3block.CodeList[i], cls,builder,isstatic);
                }
            }
            else if (stmt is ASTool.AS3.AS3Function)
            {
                ASTool.AS3.AS3Function as3function = (ASTool.AS3.AS3Function)stmt;
                if(as3function.Access.IsStatic == isstatic)
                {
                    if (!as3function.IsAnonymous)
                    {
                        for (int j = 0; j < cls.classMembers.Count; j++)
                        {
                            if (cls.classMembers[j].name == as3function.Name)
                            {
                                if (builder._buildingmembers[cls.classMembers[j]] is ASTool.AS3.AS3Function)
                                {
                                    ASTool.AS3.AS3Function pf = (ASTool.AS3.AS3Function)builder._buildingmembers[cls.classMembers[j]];
                                    if (pf.IsGet != as3function.IsGet
                                        &&
                                        pf.IsSet != as3function.IsSet
                                        )
                                    {
                                        continue;
                                    }

                                }

                                throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "重复的类成员:" + as3function.Name)
                                    );
                            }
                        }

                        Field method = new Field(as3function.Name, env.block.scope.members.Count, env.block.id,true);
                        env.block.scope.members.Add(method);
                        method.valueType = RunTimeDataType.rt_function;


                        method.isInternal = as3function.Access.IsInternal;
                        method.isPrivate = as3function.Access.IsPrivate;
                        method.isProtected = as3function.Access.IsProtected;
                        method.isPublic = as3function.Access.IsPublic;
                        method.isStatic = as3function.Access.IsStatic;
                        
                        //field.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, stmt.Token);
                        ASBinCode.rtti.ClassMember member = new ASBinCode.rtti.ClassMember(method.name,method.indexOfMembers,cls,method);

                        member.setTypeWhenCompile(RunTimeDataType.rt_function);
                        member.isInternal = method.isInternal;
                        member.isPrivate = method.isPrivate;
                        member.isProtectd = method.isProtected;
                        member.isPublic = method.isPublic;
                        member.isStatic = method.isStatic;
                        member.isConst = method.isConst;

                        member.isOverride = as3function.Access.IsOverride;

                        member.isGetter = as3function.IsGet;
                        member.isSetter = as3function.IsSet;

                        member.isConstructor = as3function.IsConstructor;
                        if (member.isConstructor )
                        {
                            if (member.isStatic)
                            {
                                throw new BuildException(
                                        new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                "Constructor functions must be instance methods")
                                        );
                            }

                            if (as3function.Parameters.Count > 0 && cls.isdocumentclass)
                            {
                                throw new BuildException(
                                        new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                "文档类的构造函数不能带参数")
                                        );
                            }

                            cls.constructor = member;
                        }

                        cls.classMembers.Add(member);

                        builder._buildingmembers.Add(member,as3function);
                    }
                    else
                    {
                        throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "'function' is not allowed here")
                                    );
                    }
                }
            }
            else if (stmt is ASTool.AS3.AS3Variable)
            {
                //字段 数据成员
                ASTool.AS3.AS3Variable variable = (ASTool.AS3.AS3Variable)stmt;
                if (variable.Access.IsStatic == isstatic)
                {
                    for (int j = 0; j < cls.classMembers.Count; j++)
                    {
                        if (cls.classMembers[j].name == variable.Name)
                        {
                            throw new BuildException(
                                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                        "重复的类成员:" + variable.Name)
                                );
                        }
                    }

                    Field field = new Field(variable.Name, env.block.scope.members.Count, env.block.id, false);
                    env.block.scope.members.Add(field);
                    field.isInternal = variable.Access.IsInternal;
                    field.isPrivate = variable.Access.IsPrivate;
                    field.isProtected = variable.Access.IsProtected;
                    field.isPublic = variable.Access.IsPublic;
                    field.isStatic = variable.Access.IsStatic;
                    
                    //field.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, stmt.Token);
                    ASBinCode.rtti.ClassMember member = new ASBinCode.rtti.ClassMember(field.name,field.indexOfMembers,cls,field);

                    member.isInternal = field.isInternal;
                    member.isPrivate = field.isPrivate;
                    member.isProtectd = field.isProtected;
                    member.isPublic = field.isPublic;
                    member.isStatic = field.isStatic;
                    member.isConst = field.isConst;

                    cls.classMembers.Add(member);

                    builder._buildingmembers.Add(member,variable);
                }
            }
            else if (stmt is ASTool.AS3.AS3Const)
            {
                //字段 数据成员
                ASTool.AS3.AS3Const constant = (ASTool.AS3.AS3Const)stmt;
                if (constant.Access.IsStatic == isstatic)
                {
                    for (int j = 0; j < cls.classMembers.Count; j++)
                    {
                        if (cls.classMembers[j].name == constant.Name)
                        {
                            throw new BuildException(
                                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                        "重复的类成员:" + constant.Name)
                                );
                        }
                    }

                    Field constfield = new Field(constant.Name, env.block.scope.members.Count, env.block.id, true);
                    env.block.scope.members.Add(constfield);
                    
                    constfield.isInternal = constant.Access.IsInternal;
                    constfield.isPrivate = constant.Access.IsPrivate;
                    constfield.isProtected = constant.Access.IsProtected;
                    constfield.isPublic = constant.Access.IsPublic;
                    constfield.isStatic = constant.Access.IsStatic;

                    ASBinCode.rtti.ClassMember member = new ASBinCode.rtti.ClassMember(constfield.name,constfield.indexOfMembers,cls,constfield);

                    member.isInternal = constfield.isInternal;
                    member.isPrivate = constfield.isPrivate;
                    member.isProtectd = constfield.isProtected;
                    member.isPublic = constfield.isPublic;
                    member.isStatic = constfield.isStatic;
                    member.isConst = constfield.isConst;

                    cls.classMembers.Add(member);

                    builder._buildingmembers.Add(member,constant);
                }
            }
            else
            {
                throw new BuildException(
                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                            "类成员中不能出现" + stmt)
                    );
            }

        }



    }
}
