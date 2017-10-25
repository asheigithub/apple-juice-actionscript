using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.builds
{
    class AS3InterfaceBuilder
    {

        public ASBinCode.rtti.Class buildInterfaceDefine(ASTool.AS3.AS3Interface as3interface,
            Builder builder,
            ASBinCode.rtti.Class mainClass,
            ASTool.AS3.AS3SrcFile as3srcfile, bool isbuildvector, RunTimeDataType vectortype)
        {
			if (builder.buildingclasses.isExistsBuildSuccess(new ASBinCode.rtti.Class(-1, -1, builder.bin, as3srcfile.md5Key + "_" + as3interface.Name) { name=as3interface.Name , package=as3interface.Package.Name   }))
			{
				//重复编译，跳过
				return null;
			}

			int classid = builder.getClassId();
            int blockid = builder.getBlockId();

            ASBinCode.rtti.Class cls = new ASBinCode.rtti.Class(classid, blockid, builder.bin,as3srcfile.md5Key +"_"+ as3interface.Name);
			cls.package = as3interface.Package.Name;
			cls.name = as3interface.Name;

			

			builder.buildingclasses.Add(as3interface, cls);

            if (isbuildvector)
            {
                builder.bin.dict_Vector_type.Add(cls, vectortype);
            }

            
            cls.ispackageout = false;
            cls.isPublic = as3interface.Access.IsPublic;
			cls.package = as3interface.Package.Name;
			cls.name = as3interface.Name;
			cls.no_constructor = true;
            cls.isInterface = true;
            if (as3interface.Access.IsDynamic)
            {
                throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                    "Interface attribute dynamic is invalid.");
            }
            if (as3interface.Access.IsFinal)
            {
                throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                    "Interface attribute final is invalid.");
            }
            //cls.dynamic = as3class.Access.IsDynamic;
            //cls.final = as3class.Access.IsFinal;
            if (mainClass != null)
            {
                cls.mainClass = mainClass;
                cls.ispackageout = true;
            }

            NativeFunctionBase creatorfunction = null;

            if (as3interface.Meta != null)
            {
                foreach (var m in as3interface.Meta)
                {
                    if (!m.Value.IsReg)
                    {
                        if (m.Value.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
                        {
                            if (m.Value.Data.Value.ToString() == "_IEnumerator_")
                            {
                                if (builder.bin.IEnumeratorInterface == null)
                                {
                                    builder.bin.IEnumeratorInterface = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3interface.token.line,
                                               as3interface.token.ptr, as3interface.token.sourceFile,
                                                                   "[_IEnumerator_]只能指定一次");
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_IEnumerable_")
                            {
                                if (builder.bin.IEnumerableInterface == null)
                                {
                                    builder.bin.IEnumerableInterface = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3interface.token.line,
                                               as3interface.token.ptr, as3interface.token.sourceFile,
                                                                   "[_IEnumerable_]只能指定一次");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (m.exprStepList != null && m.exprStepList.Count == 1)
                        {
                            var step = m.exprStepList[0];
                            if (step.Type == ASTool.AS3.Expr.OpType.CallFunc)
                            {
                                if (!step.Arg2.IsReg)
                                {
                                    if (step.Arg2.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
                                    {
                                        if (step.Arg2.Data.Value.ToString() == "link_system_interface")
                                        {
                                            cls.isLink_System = true;

                                            if (step.Arg3.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_callargements)
                                            {
                                                List<ASTool.AS3.Expr.AS3DataStackElement>
                                                    cargs = (List<ASTool.AS3.Expr.AS3DataStackElement>)step.Arg3.Data.Value;
                                                if (cargs.Count == 1)
                                                {
                                                    if (cargs[0].Data.FF1Type 
                                                        == ASTool.AS3.Expr.FF1DataValueType.identifier)
                                                    {
                                                        string creator = cargs[0].Data.Value.ToString();

                                                        if (builder.bin.nativefunctionNameIndex.ContainsKey(creator))
                                                        {
                                                            
                                                            var nf = builder.bin.nativefunctions[builder.bin.nativefunctionNameIndex[creator]];
                                                            if (!(nf is ASBinCode.rtti.ILinkSystemObjCreator))
                                                            {
                                                                throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                                                                "链接接口必须有一个INativeFunctionRegister类型的创建器");
                                                            }

                                                            creatorfunction = nf;
                                                        }
                                                        else
                                                        {
                                                            throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                                                                "本地函数 " + creator + " 未注册");
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }



            //****编译metaclass***
            int metaclassid = builder.getClassId();
            int metablockid = builder.getBlockId();

            ASBinCode.rtti.Class metaclass = new ASBinCode.rtti.Class(metaclassid, metablockid, builder.bin,as3srcfile.md5Key + "_metaclass_" + "$" + as3interface.Name);
            metaclass.package = as3interface.Package.Name;
            metaclass.ispackageout = false;
            metaclass.isPublic = as3interface.Access.IsPublic;
            metaclass.name = "$" + as3interface.Name;
            metaclass.dynamic = true;
            metaclass.final = true;

            cls.staticClass = metaclass;
            metaclass.instanceClass = cls;

            if (cls.isLink_System)
            {
                builder.linkinterfaceCreators.Add(cls, creatorfunction);
            }

            return cls;
        }


        public void buildInterfaceExtends(ASTool.AS3.AS3Interface as3interface, Builder builder)
        {
            var cls = builder.buildingclasses[as3interface];
            
            cls.staticClass.super = builder.getClassByRunTimeDataType(RunTimeDataType._OBJECT + 2);

            if (cls.isLink_System)
            {
                if (builder.bin.LinkObjectClass == null)
                {
                    throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                            "未设置链接对象基类");
                }
                cls.super = builder.bin.LinkObjectClass;
            }
            else
            {
                cls.super = builder.bin.ObjectClass;
            }

            if (as3interface.ExtendsNames.Count > 0)
            {
                for (int i = 0; i < as3interface.ExtendsNames.Count; i++)
                {
                    string extendName = as3interface.ExtendsNames[i];
                    var find = TypeReader.findClassFromImports(extendName, builder, as3interface.token);

                    if (find.Count == 1)
                    {
                        if (!find[0].isInterface)
                        {
                            throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                                "An interface can only extend other interfaces, but " + extendName + " is a class.");
                        }
                        else if ((!cls.isLink_System) && find[0].isLink_System)
                        {
                            throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                            "非链接到系统的接口" + cls.name + " 不能继承链接到系统的接口 " + find[0].name);
                        }
                        else
                        {
                            if (!cls.implements.ContainsKey(find[0]))
                            {
                                cls.implements.Add(find[0], null);
                            }
                        }
                    }
                    else if (find.Count == 0)
                    {
                        throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                            "interface " + extendName + " was not found."
                            );
                    }
                    else
                    {
                        throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                            "Ambiguous reference to " + extendName
                            );
                    }


                }
            }
            
        }

        public void copyImplements(ASTool.AS3.AS3Interface as3interface, Builder builder)
        {
            var cls = builder.buildingclasses[as3interface];
            List<ASBinCode.rtti.Class> implist = new List<ASBinCode.rtti.Class>();
            foreach (var impl in cls.implements)
            {
                implist.Add(impl.Key);
            }
            for (int i = 0; i < implist.Count; i++)
            {
                copyextendImplements(cls, implist[i]);
            }
        }

        private void copyextendImplements(ASBinCode.rtti.Class cls, ASBinCode.rtti.Class impl)
        {
            List<ASBinCode.rtti.Class> implist = new List<ASBinCode.rtti.Class>();
            foreach (var impls in impl.implements)
            {
                if (!cls.implements.ContainsKey(impls.Key))
                {
                    cls.implements.Add(impls.Key, null);
                    implist.Add(impls.Key);
                }
            }
            for (int i = 0; i < implist.Count; i++)
            {
                copyextendImplements(cls, implist[i]);
            }
        }


        public void checkCircularReference(ASTool.AS3.AS3Interface as3interface, Builder builder)
        {
            var cls = builder.buildingclasses[as3interface];

            if (Circularfind(cls, cls.implements))
            {
                throw new BuildException(as3interface.token.line, as3interface.token.ptr, as3interface.token.sourceFile,
                    "Circular type reference was detected in " + cls.name);
            }
        }

        


        private bool Circularfind(ASBinCode.rtti.Class c1, Dictionary<ASBinCode.rtti.Class,int[]> imps)
        {
            foreach (var item in imps)
            {
                if (ReferenceEquals(c1, item.Key))
                {
                    return true;
                }
                else
                {
                    return Circularfind(c1, item.Key.implements);
                }
            }

            return false;

        }



        public void buildInterfaceMembers(ASTool.AS3.AS3ClassInterfaceBase as3interface, Builder builder)
        {
            List<ASTool.AS3.IAS3Stmt> classstmts = as3interface.StamentsStack.Peek();

            var cls = builder.buildingclasses[as3interface];
            
            ASBinCode.CodeBlock block = new ASBinCode.CodeBlock(cls.blockid, cls.name, cls.classid, false);
            block.scope = new ASBinCode.scopes.ObjectInstanceScope(cls);

            CompileEnv env = new CompileEnv(block, false);
            builder._classbuildingEnv.Add(cls, env);

           

            for (int i = 0; i < classstmts.Count; i++)
            {
                buildInterfaceMember(env, classstmts[i], cls, builder);
            }

            //copyInheritsFromImplements(cls, env);
        }


        private void buildInterfaceMember(CompileEnv env,
            ASTool.AS3.IAS3Stmt stmt,
            ASBinCode.rtti.Class cls,
            Builder builder
            )
        {
            if (stmt is ASTool.AS3.AS3Block)
            {
                ASTool.AS3.AS3Block as3block = (ASTool.AS3.AS3Block)stmt;
                for (int i = 0; i < as3block.CodeList.Count; i++)
                {
                    buildInterfaceMember(env, as3block.CodeList[i], cls, builder);
                }
            }
            else if (stmt is ASTool.AS3.AS3Function)
            {
                ASTool.AS3.AS3Function as3function = (ASTool.AS3.AS3Function)stmt;
                
                {
                    if (!as3function.IsAnonymous)
                    {
                        List<ASBinCode.rtti.ClassMember> tooverridefunctions = new List<ASBinCode.rtti.ClassMember>();


                        string funcname = as3function.Name;
                        if (as3function.IsGet || as3function.IsSet)
                        {
                            funcname = "@" + as3function.Name + (as3function.IsGet ? "_get" : "_set");
                        }

                        //for (int j = 0; j < cls.classMembers.Count; j++)
                        //{
                        //    if (cls.classMembers[j].name == funcname //as3function.Name
                        //        &&
                        //        (
                        //            cls.classMembers[j].inheritFrom == null
                        //            ||
                        //            (cls.classMembers[j].isPublic && as3function.Access.IsPublic
                        //            )
                        //        )
                        //        &&
                        //        (
                        //        cls.classMembers[j].inheritFrom != null
                        //        &&
                        //        !cls.classMembers[j].inheritFrom.classMembers[j].isConstructor
                        //        )
                        //        )
                        //    {
                        //        if (as3function.Access.IsOverride
                        //            )
                        //        {
                        //            if (!(cls.classMembers[j].bindField is ClassMethodGetter)
                        //                ||
                        //                cls.classMembers[j].inheritFrom.classMembers[j].isConstructor

                        //                )
                        //            {
                        //                throw new BuildException(
                        //                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                        //                                            "Method marked override must override another method.")
                        //                    );
                        //            }

                        //            if (cls.classMembers[j].isFinal)
                        //            {
                        //                throw new BuildException(
                        //                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                        //                                            "Cannot redefine a final method.")
                        //                    );
                        //            }


                        //            tooverridefunctions.Add(cls.classMembers[j]);
                        //            continue;
                        //        }
                                
                        //        if (cls.classMembers[j].inheritFrom == null
                        //            )
                        //        {
                        //            throw new BuildException(
                        //                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                        //                                        "重复的类成员:" + as3function.Name)
                        //                );
                        //        }
                        //        else
                        //        {
                        //            throw new BuildException(
                        //                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                        //                                        "Overriding a function that is not marked for override")
                        //                );
                        //        }
                        //    }
                        //}

                        if (as3function.Access.IsStatic)
                        {
                            throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                     "The static attribute may be used only on definitions inside a class.")
                                            );
                        }


                        if (as3function.Access.IsOverride)
                        {
                            throw new BuildException(
                                        new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                "The override attribute can only be used on a method defined in a class.")
                                        );

                        }


                        //***非访问器***
                        if (!as3function.IsGet && !as3function.IsSet)
                        {
                            ASBinCode.rtti.ClassMember member =
                                new ASBinCode.rtti.ClassMember(as3function.Name, cls,
                                new InterfaceMethodGetter(as3function.Name,
                                cls,
                                env.block.id)
                                );

                            member.setTypeWhenCompile(RunTimeDataType.rt_function);
                            member.isInternal = as3function.Access.IsInternal;
                            member.isPrivate = as3function.Access.IsPrivate;
                            member.isProtectd = as3function.Access.IsProtected;
                            member.isPublic = as3function.Access.IsPublic;
                            member.isStatic = as3function.Access.IsStatic;
                            member.isConst = true;

                            member.isOverride = as3function.Access.IsOverride;
                            member.isFinal = as3function.Access.IsFinal;

                            member.isGetter = as3function.IsGet;
                            member.isSetter = as3function.IsSet;

                            member.isConstructor = as3function.IsConstructor;

                            if (member.isPrivate)
                            {
                                ((MethodGetterBase)member.bindField).setNotReadVirtual();
                            }

                            int s = 0;
                            if (member.isPrivate) s++;
                            if (member.isPublic) s++;
                            if (member.isProtectd) s++;
                            if (member.isInternal) s++;

                            if (s != 0)
                            {
                                throw new BuildException(
                                           new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                 "Members of an interface cannot be declared public, private, protected, or internal.")
                                           );
                            }

                            member.isPublic = true;

                            if (as3function.FunctionBody.Nodes.Count > 0)
                            {

                                throw new BuildException(
                                           new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                     "Methods defined in an interface must not have a body.")
                                           );
                            }

                            cls.classMembers.Add(member);

                            builder._buildingmembers.Add(member, as3function);

                        }
                        else
                        {
                            ASBinCode.rtti.ClassMember member =
                                new ASBinCode.rtti.ClassMember("@" + as3function.Name + (as3function.IsGet ? "_get" : "_set"), cls,
                                new InterfaceMethodGetter("@" + as3function.Name + (as3function.IsGet ? "_get" : "_set"),
                                cls,
                                env.block.id)
                                );

                            member.setTypeWhenCompile(RunTimeDataType.rt_function);
                            member.isInternal = as3function.Access.IsInternal;
                            member.isPrivate = as3function.Access.IsPrivate;
                            member.isProtectd = as3function.Access.IsProtected;
                            member.isPublic = as3function.Access.IsPublic;
                            member.isStatic = as3function.Access.IsStatic;
                            member.isConst = true;

                            member.isOverride = as3function.Access.IsOverride;
                            member.isFinal = as3function.Access.IsFinal;

                            member.isGetter = as3function.IsGet;
                            member.isSetter = as3function.IsSet;

                            member.isConstructor = as3function.IsConstructor;

                            if (member.isGetter && member.isSetter)
                            {
                                throw new BuildException(
                                             new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                     "不能同时是getter和setter")
                                             );
                            }



                            int s = 0;
                            if (member.isPrivate) s++;
                            if (member.isPublic) s++;
                            if (member.isProtectd) s++;
                            if (member.isInternal) s++;

                            if (s != 0)
                            {
                                throw new BuildException(
                                           new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                 "Members of an interface cannot be declared public, private, protected, or internal.")
                                           );
                            }

                            member.isPublic = true;

                            if (as3function.FunctionBody.Nodes.Count > 0)
                            {

                                throw new BuildException(
                                           new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                     "Methods defined in an interface must not have a body.")
                                           );
                            }

                            cls.classMembers.Add(member);
                            builder._buildingmembers.Add(member, as3function);



                            //***查找ClassPropertyGetter****
                            ClassPropertyGetter pg = null;

                            for (int i = 0; i < cls.classMembers.Count; i++)
                            {
                                if (cls.classMembers[i].name == as3function.Name && cls.classMembers[i].inheritFrom == null)
                                {
                                    if (cls.classMembers[i].bindField is ClassPropertyGetter)
                                    {
                                        pg = (ClassPropertyGetter)cls.classMembers[i].bindField;

                                        if (member.isGetter && pg.getter != null
                                            &&
                                            cls.classMembers[pg.getter.indexOfMembers].inheritFrom == null

                                            )
                                        {
                                            throw new BuildException(
                                                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                        "属性访问器重复")
                                                );
                                        }
                                        else if (member.isSetter && pg.setter != null
                                            &&
                                            cls.classMembers[pg.setter.indexOfMembers].inheritFrom == null
                                            )
                                        {
                                            throw new BuildException(
                                                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                        "属性设置器重复")
                                                );
                                        }

                                    }
                                    else
                                    {
                                        throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                    "期望一个属性")
                                            );
                                    }
                                }
                            }

                            if (pg == null)
                            {
                                pg = new ClassPropertyGetter(as3function.Name, cls, cls.classMembers.Count);
                                ASBinCode.rtti.ClassMember m = new ASBinCode.rtti.ClassMember(as3function.Name, cls, pg);

                                cls.classMembers.Add(m);
                                m.isPublic = true;
                                m.isStatic = member.isStatic;

                            }
                            if (member.isGetter)
                            {
                                pg.getter = (MethodGetterBase)member.bindField;
                            }
                            else
                            {
                                pg.setter = (MethodGetterBase)member.bindField;
                            }


                        }
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
                throw new BuildException(
                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                            "A 'var' declaration is not permitted in an interface.")
                    );
                //
            }
            else if (stmt is ASTool.AS3.AS3Const)
            {
                throw new BuildException(
                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                            "A 'const' declaration is not permitted in an interface.")
                    );
            }
            else
            {
                throw new BuildException(
                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                            "接口成员中不能出现" + stmt)
                    );
            }

        }




        private void copyInheritsFromImplements(ASBinCode.rtti.Class cls,Builder builder)
        {
            foreach (var supercls in cls.implements.Keys)
            {
                for (int i = 0; i < supercls.classMembers.Count; i++)
                {
                    var sm = supercls.classMembers[i];
                    var bf = sm.bindField;
                    if (bf is InterfaceMethodGetter)
                    {
                        var sig = builder.dictSignatures[((InterfaceMethodGetter)bf).refdefinedinblockid][bf];

                        
                        InterfaceMethodGetter ifm =
                            new InterfaceMethodGetter(bf.name, cls, ((InterfaceMethodGetter)bf).refdefinedinblockid);
                        ifm.setLinkMethod((InterfaceMethodGetter)bf);

                        bf = ifm;

                        if (!builder.dictSignatures.ContainsKey(ifm.refdefinedinblockid))
                        {
                            builder.dictSignatures.Add(ifm.refdefinedinblockid, new Dictionary<IMember, ASBinCode.rtti.FunctionSignature>());
                        }
                        
                        builder.dictSignatures[ifm.refdefinedinblockid].Add(ifm, sig);
                    }
                    else if (bf is ClassPropertyGetter)
                    {
                        ClassPropertyGetter cg = new ClassPropertyGetter(bf.name, cls, cls.classMembers.Count);
                        cg.valueType = ((ClassPropertyGetter)bf).valueType;
                        cg.getter = ((ClassPropertyGetter)bf).getter;
                        cg.setter = ((ClassPropertyGetter)bf).setter;
                        bf = cg;
                    }

                    ASBinCode.rtti.ClassMember member = new ASBinCode.rtti.ClassMember(sm.name, cls, bf);
                    member.defaultValue = sm.defaultValue;
                    member.isConst = false;
                    member.isConstructor = false;
                    member.isGetter = sm.isGetter;
                    member.isInternal = false;
                    member.isOverride = false;
                    member.isFinal = false;
                    member.isPrivate = false;
                    member.isProtectd = false;
                    member.isPublic = true;
                    member.isSetter = sm.isSetter;
                    member.isStatic = false;
                    member.setTypeWhenCompile(sm.valueType);

                    member.virtualLink = sm;
                    member.virtualLinkFromClass = supercls;

                    if (sm.inheritFrom == null)
                    {
                        member.inheritFrom = supercls;
                    }
                    else
                    {
                        member.inheritFrom = sm.inheritFrom;
                    }
                    if (sm.inheritSrcMember == null)
                    {
                        member.inheritSrcMember = sm;
                    }
                    else
                    {
                        member.inheritSrcMember = sm.inheritSrcMember;
                    }

                    bool iskip = false;
                    for (int m = 0; m < cls.classMembers.Count; m++)
                    {
                        var om = cls.classMembers[m];

                        ASTool.AS3.IAS3Stmt stmt=null;

                        if (om.name == member.name)
                        {
                            if (om.bindField is ClassPropertyGetter || member.bindField is ClassPropertyGetter)
                            {
                                if (om.bindField is ClassPropertyGetter && member.bindField is ClassPropertyGetter)
                                {
                                    iskip = true;
                                    break;
                                }
                                else
                                {
                                    foreach (var item in builder.buildingclasses)
                                    {
                                        if (item.Value == cls)
                                        {
                                            stmt = item.Key;
                                        }
                                    }

                                    throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "重复的接口成员,一个是访问器一个不是 "+ cls.name + ":" + member.name)
                                    );
                                    
                                }
                            }

                            ASBinCode.rtti.FunctionSignature sig = null;
                            //**检查签名***
                            if (om.inheritFrom == null)
                            {
                                sig = builder.dictSignatures[cls.blockid][om.bindField];
                                stmt = builder._buildingmembers[om];
                            }
                            else
                            {
                                sig = builder.dictSignatures[om.inheritFrom.blockid][om.inheritSrcMember.bindField];
                                stmt = builder._buildingmembers[om.inheritSrcMember];
                            }

                            ASBinCode.rtti.FunctionSignature sig2 = null;
                            if (member.inheritFrom == null)
                            {
                                sig2 = builder.dictSignatures[cls.blockid][member.bindField];
                            }
                            else
                            {
                                sig2 = builder.dictSignatures[member.inheritFrom.blockid][member.inheritSrcMember.bindField];
                            }



                            //***检查2个接口签名完全匹配***
                            if (om.isGetter !=member.isGetter || om.isSetter !=member.isGetter)
                            {
                                throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "重复的接口成员,访问器类型不匹配" + member.name)
                                    );
                            }
                            else if (sig.returnType != sig2.returnType)
                            {
                                throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "重复的接口成员,签名不匹配" + member.name)
                                    );
                            }
                            else if (sig.parameters.Count != sig2.parameters.Count)
                            {
                                throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "重复的接口成员,签名不匹配" + member.name)
                                    );
                            }
                            else
                            {

                                //***比较所有参数***
                                for (int j = 0; j < sig.parameters.Count; j++)
                                {
                                    if (sig.parameters[j].type != sig2.parameters[j].type
                                        ||
                                        sig.parameters[j].isPara != sig2.parameters[j].isPara
                                        )
                                    {
                                        throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                    "重复的接口成员,签名不匹配" + member.name)
                                            );
                                    }
                                }
                            }

                            //***检查通过，跳过这次添加***
                            iskip = true;
                        }
                    }
                    if (!iskip)
                    {
                        cls.classMembers.Add(member);
                    }
                }
            }

            //***拷贝完成，重新确认propertygetersetter,并设定索引***
            for (int i = 0; i < cls.classMembers.Count; i++)
            {
                var m = cls.classMembers[i];
                if (m.bindField is ClassPropertyGetter)
                {
                    ClassPropertyGetter pg = (ClassPropertyGetter)m.bindField;
                    //if (pg._class == cls)
                    {
                        pg.getter = null;
                        pg.setter = null;
                        for (int j = 0; j < cls.classMembers.Count; j++)
                        {
                            if (cls.classMembers[j].name == "@" + pg.name + "_get")
                            {
                                pg.getter = (InterfaceMethodGetter)cls.classMembers[j].bindField;
                            }
                            else if (cls.classMembers[j].name == "@" + pg.name + "_set")
                            {
                                pg.setter = (InterfaceMethodGetter)cls.classMembers[j].bindField;
                            }
                        }
                    }
                }
                else
                {
                    InterfaceMethodGetter ig = (InterfaceMethodGetter)m.bindField;
                    ig.setIndexMember(i,cls);

                }
            }
        }

        public void buildInterfaceMemberType(ASBinCode.rtti.Class cls, Builder builder, ASTool.AS3.AS3SrcFile as3srcfile)
        {

            builder._buildingClass.Push(cls);
            for (int i = 0; i < cls.classMembers.Count; i++)
            {
                ASBinCode.rtti.ClassMember member = cls.classMembers[i];

                if (member.bindField is ClassPropertyGetter)
                {
                    ClassPropertyGetter pg = (ClassPropertyGetter)member.bindField;
                    
                    continue;
                }

                _doBuildMemberType(member, builder, cls);
            }

            if (builder.buildErrors.Count == 0)
            {
                copyInheritsFromImplements(cls,builder);



                for (int i = 0; i < cls.classMembers.Count; i++) //检查访问器类型
                {
                    //if (cls.classMembers[i].inheritFrom != null)
                    //{
                    //    cls.classMembers[i].setTypeWhenCompile(cls.super.classMembers[i].valueType);
                    //    continue;
                    //}

                    //CheckProp(cls.classMembers[i], builder);
                }
            }

            builder._buildingClass.Pop();
        }

        private void _doBuildMemberType(ASBinCode.rtti.ClassMember member, Builder builder, ASBinCode.rtti.Class cls)
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
                if (stmt is ASTool.AS3.AS3Function)
                {
                    member.setTypeWhenCompile(RunTimeDataType.rt_function);

                    //**编译函数签名
                    builder.buildNamedFunctionSignature(env, (ASTool.AS3.AS3Function)stmt);
                    
                }
                else
                {
                    throw new Exception("异常的成员类型");
                }

                //((Variable)env.block.scope.members[member.index]).valueType = member.valueType;
                if (member.bindField is Field)
                {
                    throw new Exception("在接口中异常出现Field");
                }
            }
        }


    }
}
