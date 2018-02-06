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
                    builder.buildNamedFunctionSignature(env, (ASTool.AS3.AS3Function)stmt);

                }
                else
                {
                    throw new Exception("异常的成员类型");
                }

                //((Variable)env.block.scope.members[member.index]).valueType = member.valueType;
                if (member.bindField is Field)
                {
                    ((Field)member.bindField).valueType = member.valueType;
                }
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
                if (member.inheritFrom != null)
                {
                    member.setTypeWhenCompile(cls.staticClass.super.classMembers[i].valueType);
                    continue;
                }

                if (member.bindField is ClassPropertyGetter)
                {
                    ClassPropertyGetter pg = (ClassPropertyGetter)member.bindField;
                    if (pg.getter != null)
                    {
                        var g = cls.staticClass.classMembers[pg.getter.indexOfMembers];

                        member.isPublic =member.isPublic || g.isPublic;
                        member.isProtectd = member.isProtectd || g.isProtectd;
                        member.isInternal = member.isInternal || g.isInternal;
                        member.isPrivate = member.isPrivate || g.isPrivate;


                        
                    }
                    if (pg.setter != null)
                    {
                        var g = cls.staticClass.classMembers[pg.setter.indexOfMembers];

                        member.isPublic = member.isPublic || g.isPublic;
                        member.isProtectd = member.isProtectd || g.isProtectd;
                        member.isInternal = member.isInternal || g.isInternal;
                        member.isPrivate = member.isPrivate || g.isPrivate;
                    }
                    continue;
                }
                _doBuildMemberType(member, builder, cls);
            }
            if (builder.buildErrors.Count == 0)
            {
                for (int i = 0; i < cls.staticClass.classMembers.Count; i++) //检查访问器类型
                {
                    ASBinCode.rtti.ClassMember member = cls.staticClass.classMembers[i];
                    if (member.inheritFrom != null)
                    {
                        member.setTypeWhenCompile(cls.staticClass.super.classMembers[i].valueType);
                        continue;
                    }
                    CheckProp(member, builder);
                }
            }

            builder._buildingClass.Pop();

            builder._buildingClass.Push( cls);
            for (int i = 0; i < cls.classMembers.Count; i++)
            {
                ASBinCode.rtti.ClassMember member = cls.classMembers[i];
                if (member.inheritFrom != null)
                {
                    member.setTypeWhenCompile(cls.super.classMembers[i].valueType);
                    continue;
                }
                if (member.bindField is ClassPropertyGetter)
                {
                    ClassPropertyGetter pg = (ClassPropertyGetter)member.bindField;
                    if (pg.getter != null)
                    {
                        var g = cls.classMembers[pg.getter.indexOfMembers];

                        member.isPublic = member.isPublic || g.isPublic;
                        member.isProtectd = member.isProtectd || g.isProtectd;
                        member.isInternal = member.isInternal || g.isInternal;
                        member.isPrivate = member.isPrivate || g.isPrivate;
                    }
                    if (pg.setter != null)
                    {
                        var g = cls.classMembers[pg.setter.indexOfMembers];

                        member.isPublic = member.isPublic || g.isPublic;
                        member.isProtectd = member.isProtectd || g.isProtectd;
                        member.isInternal = member.isInternal || g.isInternal;
                        member.isPrivate = member.isPrivate || g.isPrivate;
                    }
                    continue;
                }
                _doBuildMemberType(member, builder, cls);
            }

            if (builder.buildErrors.Count == 0)
            {
                for (int i = 0; i < cls.classMembers.Count; i++) //检查访问器类型
                {
                    if (cls.classMembers[i].inheritFrom != null)
                    {
                        cls.classMembers[i].setTypeWhenCompile(cls.super.classMembers[i].valueType);
                        continue;
                    }

                    CheckProp( cls.classMembers[i],  builder);
                }
            }
            
            builder._buildingClass.Pop();
        }

        private void CheckProp(ASBinCode.rtti.ClassMember member, Builder builder)
        {
            if (member.bindField is ClassPropertyGetter)
            {
                ClassPropertyGetter pg = (ClassPropertyGetter)member.bindField;

                if (pg.setter != null)
                {
                    var g = member.refClass.classMembers[pg.setter.indexOfMembers];
                    if (g.inheritFrom != null)
                    {
                        return;
                    }

                    var sig =
                        builder.dictSignatures[builder._classbuildingEnv[member.refClass].block.id][g.bindField];

                    if (pg.getter != null)
                    {
                        var gig =
                            builder.dictSignatures[builder._classbuildingEnv[member.refClass].block.id]
                            [pg.getter];


                        if (!ASRuntime.TypeConverter.testImplicitConvert(
                            gig.returnType, sig.parameters[0].type, builder
                            ))
                        {
                            var stmt = builder._buildingmembers[g];

                            throw new BuildException(stmt.Token.line,
                                    stmt.Token.ptr, stmt.Token.sourceFile,
                                                        "访问器类型不匹配");
                        }
                        if (!ASRuntime.TypeConverter.testImplicitConvert(
                             sig.parameters[0].type, gig.returnType, builder
                            ))
                        {
                            var stmt = builder._buildingmembers[g];

                            throw new BuildException(stmt.Token.line,
                                    stmt.Token.ptr, stmt.Token.sourceFile,
                                                        "访问器类型不匹配");
                        }

                        member.setTypeWhenCompile(gig.returnType);
                    }
                    else
                    {
                        member.setTypeWhenCompile(sig.parameters[0].type);
                    }
                }
                else
                {
                    var gig =
                            builder.dictSignatures[builder._classbuildingEnv[member.refClass].block.id]
                            [pg.getter];
                    member.setTypeWhenCompile(gig.returnType);
                }
            }
        }

        /// <summary>
        /// 编译类的类型说明
        /// </summary>
        public ASBinCode.rtti.Class buildClassDefine(ASTool.AS3.AS3Class as3class, 
            Builder builder, ASBinCode.rtti.Class mainClass ,ASTool.AS3.AS3SrcFile as3srcfile,bool isbuildvector,RunTimeDataType vectortype )
        {
			if (builder.buildingclasses.isExistsBuildSuccess(new ASBinCode.rtti.Class(-1, -1, builder.bin, as3srcfile.md5Key + "_" + as3class.Name)
						{
							name= as3class.Name,
							package= as3class.Package.Name
			}
				
				))
			{
				//重复编译，跳过
				return null;
			}

			int classid = builder.getClassId();
            int blockid = builder.getBlockId();

            ASBinCode.rtti.Class cls = new ASBinCode.rtti.Class(classid, blockid,builder.bin,as3srcfile.md5Key + "_" + as3class.Name);
			cls.package = as3class.Package.Name;
			cls.name = as3class.Name;

			
			builder.buildingclasses.Add(as3class, cls);

            if (isbuildvector)
            {
                builder.bin.dict_Vector_type.Add(cls, vectortype);
            }

            
            cls.ispackageout = false;
            cls.isPublic = as3class.Access.IsPublic;
			cls.package = as3class.Package.Name;
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
                foreach (var m in as3class.Meta)
                {
                    if (!m.Value.IsReg)
                    {
                        if (m.Value.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.identifier)
                        {
                            if (m.Value.Data.Value.ToString() == "Doc")
                            {
								foreach (var item in builder.buildingclasses )
								{
									if (item.Value.isdocumentclass)
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
                            else if (m.Value.Data.Value.ToString() == "hosted")
                            {
                                cls.isUnmanaged = true;
                            }
                            else if (m.Value.Data.Value.ToString() == "no_constructor")
                            {
                                if (!cls.ispackageout && cls.isPublic)
                                {
                                    cls.no_constructor = true;
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "struct")
                            {
                                if (!cls.ispackageout && cls.isPublic)
                                {
                                    cls.isStruct = true;
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "link_system")
                            {
                                if (!cls.ispackageout && cls.isPublic)
                                {
                                    cls.isLink_System = true;
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_error_class_")
                            {
                                if (builder.bin.ErrorClass == null)
                                {
                                    builder.bin.ErrorClass = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[_error_class_]只能指定一次");
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_dictionary_")
                            {
                                if (builder.bin.DictionaryClass == null)
                                {
                                    builder.bin.DictionaryClass = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[_dictionary_]只能指定一次");
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_YieldIterator_")
                            {
                                if (builder.bin.YieldIteratorClass == null)
                                {
                                    builder.bin.YieldIteratorClass = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[_YieldIterator_]只能指定一次");
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_function_")
                            {
                                if (builder.bin.FunctionClass == null)
                                {
                                    builder.bin.FunctionClass = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[_function_]只能指定一次");
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_object_")
                            {
                                if (builder.bin.ObjectClass == null)
                                {
                                    builder.bin.ObjectClass = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[_object_]只能指定一次");
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_link_Object_")
                            {
                                if (builder.bin.LinkObjectClass == null)
                                {
                                    builder.bin.LinkObjectClass = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[_link_Object_]只能指定一次");
                                }
                            }
                            else if (m.Value.Data.Value.ToString() == "_class_")
                            {
                                if (builder.bin.TypeClass == null)
                                {
                                    builder.bin.TypeClass = cls;
                                }
                                else
                                {
                                    throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[_class_]只能指定一次");
                                }
                            }
							else if (m.Value.Data.Value.ToString() == "_regexp_")
							{
								if (builder.bin.RegExpClass == null)
								{
									builder.bin.RegExpClass = cls;
								}
								else
								{
									throw new BuildException(as3class.token.line,
											   as3class.token.ptr, as3class.token.sourceFile,
																   "[_regexp_]只能指定一次");
								}
							}
							else if (m.Value.Data.Value.ToString() == "_package_function_")
							{
								cls.isPackageFunction = true;
								cls.no_constructor = true;
							}
							

						}
                    }
                }
                
            }

            if (cls.isUnmanaged && cls.isLink_System)
            {
                throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[hosted]与[link_system]互斥");
            }
            if (cls.isStruct && !cls.isLink_System)
            {
                throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[struct]必须和[link_system]同时设置");
            }
            if (cls.isStruct && !cls.final)
            {
                throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[struct]的类必须是final");
            }


            if (cls.isUnmanaged && !cls.final)
            {
                throw new BuildException(as3class.token.line,
                                               as3class.token.ptr, as3class.token.sourceFile,
                                                                   "[hosted]的类必须是final");
            }


            //****编译metaclass***
            int metaclassid = builder.getClassId();
            int metablockid = builder.getBlockId();

            ASBinCode.rtti.Class metaclass = new ASBinCode.rtti.Class(metaclassid, metablockid,builder.bin,as3srcfile.md5Key+"_metaclass_"+ "$" + as3class.Name);
            metaclass.package = as3class.Package.Name;
            metaclass.ispackageout = false;
            metaclass.isPublic = as3class.Access.IsPublic;
            metaclass.name = "$" + as3class.Name ;
            metaclass.dynamic = true;
            metaclass.final = true;

            cls.staticClass = metaclass;
            metaclass.instanceClass = cls;
			metaclass.isPackageFunction = cls.isPackageFunction;

           
            return cls;
        }

        public void buildClassExtends(ASTool.AS3.AS3Class as3class, Builder builder)
        {
            var cls = builder.buildingclasses[as3class];
            if (cls.getRtType() != RunTimeDataType._OBJECT+2)
            {
                cls.staticClass.super = builder.getClassByRunTimeDataType(RunTimeDataType._OBJECT+2);
            }
            if (as3class.ExtendsNames.Count > 0)
            {
                if (as3class.ExtendsNames.Count > 1)
                {
                    throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                        as3class.Name + " 只能继承1个类"
                        );
                }

                string extendName = as3class.ExtendsNames[0];
                var find= TypeReader.findClassFromImports(extendName, builder, as3class.token);

                if (find.Count == 1)
                {
                    if (find[0].mainClass == cls)
                    {
                        throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                        "Forward reference to base class ." + extendName + "."
                        );
                        //
                    }

                    if (find[0].isInterface)
                    {
                        throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                        "A class can only extend another class, not an interface.");
                    }

                    cls.super = find[0];
                }
                else if (find.Count == 0)
                {
                    throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                        "The definition of base class " + extendName + " was not found."
                        );
                }
                else
                {
                    throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                        "Ambiguous reference to " + extendName
                        );
                }

            }
            else
            {

                var OBJECT = builder.getClassByRunTimeDataType(RunTimeDataType._OBJECT);
                if (cls != OBJECT)
                {
                    cls.super = OBJECT;
                }
            }

			if (cls.isLink_System)
			{
				if (!cls.super.isLink_System
					&&
					cls.super != builder.getClassByRunTimeDataType(RunTimeDataType._OBJECT)
					)
				{
					throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
						"[link_system]的类只能继承自同样[link_system]的类"
						);
				}
			}
			else
			{
				if (cls.super != null && cls.super.isLink_System)
				{
					if (cls.super.isStruct)
					{
						throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
						"actionscript类不能继承自[link_system]的[struct]类"
						);
					}
					cls.isCrossExtend = true;
				}
			}
        }


		public void CheckCrossExtends(ASTool.AS3.AS3ClassInterfaceBase as3class, Builder builder)
		{
			var cls = builder.buildingclasses[as3class];
			if (!cls.isLink_System)
			{
				if (cls.super != null && cls.super.isLink_System)
				{
					if (cls.super.isStruct)
					{
						throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
						"actionscript类不能继承自[link_system]的[struct]类"
						);
					}
					cls.isCrossExtend = true;
				}
				else if (cls.super != null && cls.super.isCrossExtend)
				{
					cls.isCrossExtend = true;
				}

			}
		}


		public void checkCircularReference(ASTool.AS3.AS3Class as3class, Builder builder)
        {
            var cls = builder.buildingclasses[as3class];

            var supser = cls.super;
            while (supser !=null)
            {
                if (ReferenceEquals(cls, supser))
                {
                    throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                        "Circular type reference was detected in " + cls.name);
                }
                supser = supser.super;
            }


        }

        public void buildClassImplements(ASTool.AS3.AS3Class as3class, Builder builder)
        {
            var cls = builder.buildingclasses[as3class];
            if (as3class.ImplementsNames.Count > 0)
            {
                for (int i = 0; i < as3class.ImplementsNames.Count; i++)
                {
                    string extendName = as3class.ImplementsNames[i];
                    var find = TypeReader.findClassFromImports(extendName, builder, as3class.token);

                    if (find.Count == 1)
                    {
                        if (!find[0].isInterface)
                        {
                            throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                                "An interface can only extend other interfaces, but " + extendName + " is a class.");
                        }
                        else
                        {
                            if (cls.implements.ContainsKey(find[0]))
                            {
                                throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                                "class " + cls.name + " implements interface " + find[0].name + " multiple times.");
                            }
                            else if ((!cls.isLink_System) && find[0].isLink_System)
                            {
                                throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                                "非链接到系统的类" + cls.name + " 不能实现链接到系统的接口 " + find[0].name);
                            }
                            else
                            {
                                cls.implements.Add(find[0], null);
                            }
                        }
                    }
                    else if (find.Count == 0)
                    {
                        throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                            "interface " + extendName + " was not found."
                            );
                    }
                    else
                    {
                        throw new BuildException(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                            "Ambiguous reference to " + extendName
                            );
                    }
                }

                List<ASBinCode.rtti.Class> implist = new List<ASBinCode.rtti.Class>();
                foreach(var impl in cls.implements )
                {
                    implist.Add(impl.Key);
                }
                for (int i = 0; i < implist.Count; i++)
                {
                    copyextendImplements(cls, implist[i]);
                }

            }
        }
        private void copyextendImplements(ASBinCode.rtti.Class cls,ASBinCode.rtti.Class impl)
        {
            List<ASBinCode.rtti.Class> implist = new List<ASBinCode.rtti.Class>();
            foreach (var impls in impl.implements)
            {
                if (!cls.implements.ContainsKey(impls.Key))
                {
                    cls.implements.Add(impls.Key,null);
                    implist.Add(impls.Key);
                }
            }
            for (int i = 0; i < implist.Count; i++)
            {
                copyextendImplements(cls, implist[i]);
            }
        }

        private void copyInheritsFromSuper(ASBinCode.rtti.Class cls,CompileEnv env)
        {
            var supercls = cls.super;
            if (supercls == null)
            {
                return;
            }

            for (int i = 0; i < supercls.classMembers.Count; i++)
            {
                var sm = supercls.classMembers[i];
                var bf = sm.bindField;




                ASBinCode.rtti.ClassMember member = new ASBinCode.rtti.ClassMember(sm.name, cls, bf);
                member.defaultValue = sm.defaultValue;
                member.isConst = sm.isConst;
                member.isConstructor = false;
                member.isGetter = sm.isGetter;
                member.isInternal = sm.isInternal;
                member.isOverride = sm.isOverride;
                member.isFinal = sm.isFinal;
                member.isPrivate = sm.isPrivate;
                member.isProtectd = sm.isProtectd;
                member.isPublic = sm.isPublic;
                member.isSetter = sm.isSetter;
                member.isStatic = sm.isStatic;
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

                cls.classMembers.Add(member);

                if (supercls.fields.Contains(sm))
                {
                    env.block.scope.members.Add(bf);

                    cls.fields.Add(member);
                }


            }



        }
        

        public void buildClassDefineMembers(ASTool.AS3.AS3ClassInterfaceBase as3class, Builder builder,bool isstatic)
        {
            List<ASTool.AS3.IAS3Stmt> classstmts = as3class.StamentsStack.Peek();

            var cls = builder.buildingclasses[as3class];
            if (!isstatic)
            {
                ASBinCode.CodeBlock block = new ASBinCode.CodeBlock(cls.blockid, cls.name, cls.classid, false);
                block.scope = new ASBinCode.scopes.ObjectInstanceScope(cls);

                CompileEnv env = new CompileEnv(block, false);
                builder._classbuildingEnv.Add(cls, env);

                copyInheritsFromSuper(cls, env);

                for (int i = 0; i < classstmts.Count; i++)
                {
                    buildClassMember(env, classstmts[i], cls, builder, false);
                }

                if (cls.constructor == null)
                {
                    ASTool.AS3.AS3Function fc = new ASTool.AS3.AS3Function(new ASTool.Token());
                    fc.Access = new ASTool.AS3.AS3Access();
                    fc.Access.IsPublic = true;
                    fc.IsConstructor = true;
                    fc.Name = cls.name;
                    fc.TypeStr = "*";
                    fc.IsMethod = true;

                    fc.Parameters = new List<ASTool.AS3.AS3Parameter>();
                    fc.ParentScope = as3class;
                    fc.StamentsStack.Push(new List<ASTool.AS3.IAS3Stmt>());


                    buildClassMember(env, fc, cls, builder, false);
                }

                if (cls.isLink_System)
                {
                    if (cls.fields.Count > 0)
                    {
                        throw new BuildException(
                                            new BuildError(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                                                                    "[link_system]的类只能包含[native]的成员")
                                            );
                    }
                }

            }
            else
            {
                var metaclass = cls.staticClass;

                ASBinCode.CodeBlock metablock = new ASBinCode.CodeBlock(metaclass.blockid, metaclass.name, metaclass.classid, false);
                metablock.scope = new ASBinCode.scopes.ObjectInstanceScope(metaclass);

                CompileEnv envMeta = new CompileEnv(metablock, false);
                builder._classbuildingEnv.Add(metaclass, envMeta);

                copyInheritsFromSuper(metaclass, envMeta);

                for (int i = 0; i < classstmts.Count; i++)
                {
                    buildClassMember(envMeta, classstmts[i], metaclass, builder, true);
                }

                if (cls.isLink_System)
                {
                    for (int i = 0; i < metaclass.fields.Count; i++)
                    {
                        if (metaclass.fields[i].inheritFrom == null)
                        {
                            throw new BuildException(
                                            new BuildError(as3class.token.line, as3class.token.ptr, as3class.token.sourceFile,
                                                                    "[link_system]的类只能包含[native]的成员")
                                            );
                        }
                    }
                }

            }
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
                        List<ASBinCode.rtti.ClassMember> tooverridefunctions = new List<ASBinCode.rtti.ClassMember>();


                        string funcname = as3function.Name;
                        if (as3function.IsGet || as3function.IsSet)
                        {
                            funcname = "@" + as3function.Name + (as3function.IsGet ? "_get" : "_set");
                        }

                        for (int j = 0; j < cls.classMembers.Count; j++)
                        {
							if (cls.classMembers[j].name == funcname
								&&
								cls.classMembers[j].inheritFrom==null
								)
							{
								
								throw new BuildException(
									new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
															"重复的类成员:" + as3function.Name)
									);
							}


                            if (cls.classMembers[j].name == funcname //as3function.Name
                                &&
                                (
                                    cls.classMembers[j].inheritFrom != null
                                    //||
                                    //(
                                    //    cls.classMembers[j].isPublic 
                                    //    && 
                                    //    as3function.Access.IsPublic
                                    //    &&
                                    //    !cls.classMembers[j].inheritFrom.classMembers[j].isConstructor
                                    //)
									&&
										!cls.classMembers[j].inheritFrom.classMembers[j].isConstructor
								)
                            )
                            {
                                if (as3function.Access.IsOverride
                                    )
                                {
                                    if (!(cls.classMembers[j].bindField is MethodGetterBase)
                                        ||
                                        cls.classMembers[j].inheritFrom.classMembers[j].isConstructor

                                        )
                                    {
                                        throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                    "Method marked override must override another method.")
                                            );
                                    }

									
									if (((as3function.Access.IsPublic) && ( !cls.classMembers[j].isPublic)) || ((!as3function.Access.IsPublic) && (cls.classMembers[j].isPublic)))
									{
										throw new BuildException(
											new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
																	"Method marked override must override another method.")
											);
									}
									if (((as3function.Access.IsInternal) && (!cls.classMembers[j].isInternal)) || ((!as3function.Access.IsInternal) && (cls.classMembers[j].isInternal)))
									{
										throw new BuildException(
											new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
																	"Method marked override must override another method.")
											);
									}
									if (((as3function.Access.IsProtected) && (!cls.classMembers[j].isProtectd)) 
										|| 
										((!as3function.Access.IsProtected) && (cls.classMembers[j].isProtectd))
										||
										cls.classMembers[j].isPrivate
										)
									{
										throw new BuildException(
											new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
																	"Method marked override must override another method.")
											);
									}


									if (cls.classMembers[j].isFinal)
                                    {
                                        throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                    "Cannot redefine a final method.")
                                            );
                                    }


                                    tooverridefunctions.Add(cls.classMembers[j]);
                                    continue;
                                }

                                //if (cls.classMembers[j].inheritFrom == null                                   
                                //    )
                                //{
                                //    throw new BuildException(
                                //        new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                //                                "重复的类成员:" + as3function.Name)
                                //        );
                                //}
                                //else
                                {
                                    throw new BuildException(
                                        new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                "Overriding a function that is not marked for override")
                                        );
                                }
                            }
                        }

                        if (as3function.Access.IsOverride && as3function.Access.IsStatic )
                        {
                            throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                     "Functions cannot be both static and override.")
                                            );
                        }


                        if (as3function.Access.IsOverride && tooverridefunctions.Count == 0)
                        {
                            throw new BuildException(
                                        new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                "Method marked override must override another method.")
                                        );

                        }


                        //***非访问器***
                        if (!as3function.IsGet && !as3function.IsSet)
                        {
                            ASBinCode.rtti.ClassMember member =
                                new ASBinCode.rtti.ClassMember(as3function.Name, cls,
                                new ClassMethodGetter(as3function.Name,
                                cls, cls.classMembers.Count,
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

                            if (member.isConstructor)
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

                            int s = 0;
                            if (member.isPrivate) s++;
                            if (member.isPublic) s++;
                            if (member.isProtectd) s++;
                            if (member.isInternal) s++;

                            if (s == 0)
                            {
                                member.isInternal = true;
                            }

                            if (s > 1)
                            {
                                throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                    "Only one of public, private, protected, or internal can be specified on a definition.")
                                            );
                                
                            }
                            


                            cls.classMembers.Add(member);

                            builder._buildingmembers.Add(member, as3function);

                            if (as3function.Access.IsOverride)
                            {
                                Dictionary<ASBinCode.rtti.ClassMember, List<ASBinCode.rtti.ClassMember>>
                                    dictOvs = new Dictionary<ASBinCode.rtti.ClassMember, List<ASBinCode.rtti.ClassMember>>();
                                if (!builder._overridefunctions.ContainsKey(cls))
                                {
                                    builder._overridefunctions.Add(cls,
                                        dictOvs);
                                }

                                member.virtualLink = tooverridefunctions[tooverridefunctions.Count -1];
                                builder._overridefunctions[cls].Add(
                                    member, tooverridefunctions
                                    );
                            }

                        }
                        else
                        {
                            ASBinCode.rtti.ClassMember member =
                                new ASBinCode.rtti.ClassMember("@" + as3function.Name + (as3function.IsGet?"_get":"_set"), cls,
                                new ClassMethodGetter("@" + as3function.Name + (as3function.IsGet ? "_get" : "_set"),
                                cls, cls.classMembers.Count,
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
                            if (member.isConstructor)
                            {
                                throw new BuildException(
                                             new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                     "访问器不可能是构造函数")
                                             );
                            }

                            if (member.isGetter && member.isSetter)
                            {
                                throw new BuildException(
                                             new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                     "不能同时是getter和setter")
                                             );
                            }

                            if (member.isPrivate)
                            {
                                ((MethodGetterBase)member.bindField).setNotReadVirtual();
                            }

                            int s = 0;
                            if (member.isPrivate) s++;
                            if (member.isPublic) s++;
                            if (member.isProtectd) s++;
                            if (member.isInternal) s++;

                            if (s == 0)
                            {
                                member.isInternal = true;
                            }

                            if (s > 1)
                            {
                                throw new BuildException(
                                            new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                    "Only one of public, private, protected, or internal can be specified on a definition.")
                                            );

                            }

                            



                            cls.classMembers.Add(member);
                            builder._buildingmembers.Add(member, as3function);

                            if (as3function.Access.IsOverride)
                            {
                                Dictionary<ASBinCode.rtti.ClassMember, List<ASBinCode.rtti.ClassMember>>
                                    dictOvs = new Dictionary<ASBinCode.rtti.ClassMember, List<ASBinCode.rtti.ClassMember>>();
                                if (!builder._overridefunctions.ContainsKey(cls))
                                {
                                    builder._overridefunctions.Add(cls,
                                        dictOvs);
                                }

                                member.virtualLink = tooverridefunctions[tooverridefunctions.Count -1];

                                builder._overridefunctions[cls].Add(
                                    member, tooverridefunctions
                                    );
                            }

                            //***查找ClassPropertyGetter****
                            ClassPropertyGetter pg = null;

                            for (int i = 0; i < cls.classMembers.Count; i++)
                            {
                                if (cls.classMembers[i].name== as3function.Name && cls.classMembers[i].inheritFrom==null )
                                {
                                    if (cls.classMembers[i].bindField is ClassPropertyGetter)
                                    {
                                        pg = (ClassPropertyGetter)cls.classMembers[i].bindField;

                                        if (member.isGetter && pg.getter != null 
                                            &&
                                            cls.classMembers[  pg.getter.indexOfMembers ].inheritFrom ==null
                                            
                                            )
                                        {
                                            throw new BuildException(
                                                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                                        "属性访问器重复")
                                                );
                                        }
                                        else if (member.isSetter && pg.setter != null
                                            &&
                                            cls.classMembers[pg.setter.indexOfMembers].inheritFrom==null
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

                                //***从拷贝过来的成员中复制继承的访问器属性***
                                for (int i = cls.classMembers.Count-1; i >=0; i--)
                                {
                                    if (cls.classMembers[i].inheritFrom != null)
                                    {
                                        if (cls.classMembers[i].name == pg.name
                                            &&
                                            cls.classMembers[i].bindField is ClassPropertyGetter
                                            )
                                        {
                                            ClassPropertyGetter copyed =
                                                (ClassPropertyGetter)cls.classMembers[i].bindField;

                                            pg.getter = copyed.getter;
                                            pg.setter = copyed.setter;

                                            m.setTypeWhenCompile(cls.classMembers[i].valueType);

                                            break;
                                        }
                                    }

                                }




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
						bool isthrow = true;
						//***如果是直接被作为参数使用***
						var scope = as3function.ParentScope;
						if (scope != null)
						{
							if (scope.StamentsStack.Count > 0)
							{
								var liststmt = scope.StamentsStack.Peek();
								foreach (var item in liststmt)
								{
									if (item == stmt)
									{
										continue;
									}
									else if(item is ASTool.AS3.AS3Member )
									{
										var member = (ASTool.AS3.AS3Member)item;
										if (member.ValueExpr != null)
										{
											var exprList = member.ValueExpr.exprStepList;
											foreach (var expr in exprList)
											{
												if (expr.Arg3 != null && expr.Arg3.Data !=null && expr.Arg3.Data.FF1Type== ASTool.AS3.Expr.FF1DataValueType.as3_callargements
													)
												{
													List<ASTool.AS3.Expr.AS3DataStackElement> args = expr.Arg3.Data.Value as List<ASTool.AS3.Expr.AS3DataStackElement>;
													if (args != null)
													{
														foreach (var a in args)
														{
															if (a.Data !=null && a.Data.Value == as3function)
															{
																isthrow = false;
																break;
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
						if (isthrow)
						{
							throw new BuildException(
										new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
																"'function' is not allowed here")
										);
						}
					}
                }
            }
            else if (stmt is ASTool.AS3.AS3Variable)
            {
                //字段 数据成员
                ASTool.AS3.AS3Variable variable = (ASTool.AS3.AS3Variable)stmt;
                if (variable.Access.IsStatic == isstatic)
                {
                    if (variable.Access.IsOverride)
                    {
                        throw new BuildException(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                "The override attribute can only be used on a method defined in a class."));
                    }
                    else if (variable.Access.IsFinal)
                    {
                        throw new BuildException(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                "The final attribute can only be used on a method defined in a class."));
                    }

                    for (int j = 0; j < cls.classMembers.Count; j++)
                    {
                        if (cls.classMembers[j].name == variable.Name
                            &&
                                (
                                    cls.classMembers[j].inheritFrom == null
                                    ||
                                    (cls.classMembers[j].isPublic && variable.Access.IsPublic )
                                )

                            )
                        {
                            throw new BuildException(
                                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                        "重复的类成员:" + variable.Name)
                                );
                        }
                    }

					List<FieldMeta> fieldmetalist = new List<FieldMeta>();

                    if (variable.Meta != null)
                    {
                        var metalist = variable.Meta;
                        for (int i = 0; i < metalist.Count; i++)
                        {
                            var m = metalist[i];
                            if (m.Value.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_array)
                            {
                                List<ASTool.AS3.Expr.AS3DataStackElement> data =
                                    (List<ASTool.AS3.Expr.AS3DataStackElement>)m.Value.Data.Value;

								if (data.Count == 0)
								{
									throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
																   "Meta 格式错误,至少有1个设置");
								}
								else if (!data[0].IsReg)
								{
									string meta = data[0].Data.Value.ToString();
									if (meta == "native")
									{
										if (data.Count == 3)
										{
											string native_get = data[1].Data.Value.ToString();

											if (builder.bin.nativefunctionNameIndex.ContainsKey(native_get))
											{
												var nf = builder.bin.nativefunctions[builder.bin.nativefunctionNameIndex[native_get]];
												if (nf.isMethod)
												{
													if (nf.parameters.Count != 0)
													{
														throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
															"本地函数 " + native_get + "不接受参数");
													}
													//if (nf.returnType != RunTimeDataType.rt_void)
													//{
													//	throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
													//		"本地函数 " + native_get + "必须返回*");
													//}

												}
												else
												{
													throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
														"本地函数 " + native_get + " isMethod属性不符");
												}
											}
											else
											{
												throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
													"本地函数 " + native_get + " 未注册");
											}

											string native_set = data[2].Data.Value.ToString();

											if (builder.bin.nativefunctionNameIndex.ContainsKey(native_set))
											{
												var nf = builder.bin.nativefunctions[builder.bin.nativefunctionNameIndex[native_set]];
												if (nf.isMethod)
												{
													if (nf.parameters.Count != 1)
													{
														throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
															"本地函数 " + native_set + "必须只有1个参数");
													}
													//if (nf.parameters[0] != RunTimeDataType.rt_void)
													//{
													//	throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
													//		"本地函数 " + native_set + "参数类型必须是*");
													//}
													if (nf.returnType != RunTimeDataType.fun_void)
													{
														throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
															"本地函数 " + native_set + "必须是void");
													}
												}
												else
												{
													throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
														"本地函数 " + native_get + " isMethod属性不符");
												}
											}
											else
											{
												throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
													"本地函数 " + native_set + " 未注册");
											}

											//***检查通过，创建两个函数作为getter和setter放进去***

											{
												ASTool.AS3.AS3Function fc =
													new ASTool.AS3.AS3Function(new ASTool.Token());
												fc.Access = variable.Access;
												fc.IsConstructor = false;
												fc.Name = variable.Name;
												fc.TypeStr = variable.TypeStr;
												fc.IsMethod = true;
												fc.IsGet = true;
												fc.Access.IsFinal = true;

												fc.Parameters = new List<ASTool.AS3.AS3Parameter>();
												//fc.ParentScope = builder.

												fc.StamentsStack.Push(new List<ASTool.AS3.IAS3Stmt>());
												fc.Meta = new List<ASTool.AS3.AS3Meta>();
												ASTool.AS3.AS3Meta me = new ASTool.AS3.AS3Meta(new ASTool.Token());

												List<ASTool.AS3.Expr.AS3DataStackElement> medata
													= new List<ASTool.AS3.Expr.AS3DataStackElement>();

												me.Value = new ASTool.AS3.Expr.AS3DataStackElement();
												me.Value.Data = new ASTool.AS3.Expr.AS3DataValue();
												me.Value.Data.FF1Type = ASTool.AS3.Expr.FF1DataValueType.as3_array;
												me.Value.Data.Value = medata;

												medata.Add(new ASTool.AS3.Expr.AS3DataStackElement()
												{
													Data = new ASTool.AS3.Expr.AS3DataValue()
													{
														FF1Type = ASTool.AS3.Expr.FF1DataValueType.const_string,
														Value = "native"
													}
												}
												);
												medata.Add(new ASTool.AS3.Expr.AS3DataStackElement()
												{
													Data = new ASTool.AS3.Expr.AS3DataValue()
													{
														FF1Type = ASTool.AS3.Expr.FF1DataValueType.const_string,
														Value = native_get
													}
												}
												);
												fc.Meta.Add(me);

												buildClassMember(env, fc, cls, builder, isstatic);

											}

											{
												ASTool.AS3.AS3Function fc =
													new ASTool.AS3.AS3Function(new ASTool.Token());
												fc.Access = variable.Access;
												fc.IsConstructor = false;
												fc.Name = variable.Name;
												fc.TypeStr = "void";
												fc.IsMethod = true;
												fc.IsSet = true;
												fc.Access.IsFinal = true;

												fc.Parameters = new List<ASTool.AS3.AS3Parameter>();
												fc.Parameters.Add(
													new ASTool.AS3.AS3Parameter(new ASTool.Token())
													{
														Name = "value",
														TypeStr = variable.TypeStr
													}


													);

												//fc.ParentScope = builder.

												fc.StamentsStack.Push(new List<ASTool.AS3.IAS3Stmt>());
												fc.Meta = new List<ASTool.AS3.AS3Meta>();
												ASTool.AS3.AS3Meta me = new ASTool.AS3.AS3Meta(new ASTool.Token());

												List<ASTool.AS3.Expr.AS3DataStackElement> medata
													= new List<ASTool.AS3.Expr.AS3DataStackElement>();

												me.Value = new ASTool.AS3.Expr.AS3DataStackElement();
												me.Value.Data = new ASTool.AS3.Expr.AS3DataValue();
												me.Value.Data.FF1Type = ASTool.AS3.Expr.FF1DataValueType.as3_array;
												me.Value.Data.Value = medata;

												medata.Add(new ASTool.AS3.Expr.AS3DataStackElement()
												{
													Data = new ASTool.AS3.Expr.AS3DataValue()
													{
														FF1Type = ASTool.AS3.Expr.FF1DataValueType.const_string,
														Value = "native"
													}
												}
												);
												medata.Add(new ASTool.AS3.Expr.AS3DataStackElement()
												{
													Data = new ASTool.AS3.Expr.AS3DataValue()
													{
														FF1Type = ASTool.AS3.Expr.FF1DataValueType.const_string,
														Value = native_set
													}
												}
												);
												fc.Meta.Add(me);

												buildClassMember(env, fc, cls, builder, isstatic);

											}

											return;

										}
										else
										{
											throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
												"[native] Field需要指定2个访问函数");
										}
									}
								}
								else if (data[0].IsReg)
								{
									if (variable.TypeStr == "Array")
									{
										if (m.exprStepList.Count == 1)
										{
											var step = m.exprStepList[0];
											if (step.Type == ASTool.AS3.Expr.OpType.CallFunc)
											{
												if (step.Arg2.ToString() == "ArrayElementType")
												{
													if (step.Arg3.Data != null)
													{
														List<ASTool.AS3.Expr.AS3DataStackElement> args =
															step.Arg3.Data.Value as List<ASTool.AS3.Expr.AS3DataStackElement>;

														if (args != null && args.Count == 1)
														{
															if (args[0].Data != null && args[0].Data.Value != null)
															{
																string ArrayElementType = args[0].Data.Value.ToString();

																FieldMeta fieldMeta = new FieldMeta();
																fieldMeta.MetaName = "ArrayElementType";
																fieldMeta.MetaData = ArrayElementType;
																fieldmetalist.Add(fieldMeta);
															}
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
                                throw new BuildException(variable.token.line, variable.token.ptr, variable.token.sourceFile,
                                    "Meta 格式错误");
                            }
                        }

                    }



                    Field field = new Field(variable.Name, env.block.scope.members.Count, env.block.id, false);
					field.metas.AddRange(fieldmetalist);

                    env.block.scope.members.Add(field);
                    field.isInternal = variable.Access.IsInternal;
                    field.isPrivate = variable.Access.IsPrivate;
                    field.isProtected = variable.Access.IsProtected;
                    field.isPublic = variable.Access.IsPublic;
                    field.isStatic = variable.Access.IsStatic;
                    
                    //field.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, stmt.Token);
                    ASBinCode.rtti.ClassMember member = new ASBinCode.rtti.ClassMember(field.name,cls,field);

                    member.isInternal = field.isInternal;
                    member.isPrivate = field.isPrivate;
                    member.isProtectd = field.isProtected;
                    member.isPublic = field.isPublic;
                    member.isStatic = field.isStatic;
                    member.isConst = field.isConst;

                    cls.classMembers.Add(member);

                    cls.fields.Add(member);

                    int s = 0;
                    if (member.isPrivate) s++;
                    if (member.isPublic) s++;
                    if (member.isProtectd) s++;
                    if (member.isInternal) s++;

                    if (s == 0)
                    {
                        member.isInternal = true;
                    }

                    if (s > 1)
                    {
                        throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "Only one of public, private, protected, or internal can be specified on a definition.")
                                    );

                    }

                    builder._buildingmembers.Add(member,variable);
                }
            }
            else if (stmt is ASTool.AS3.AS3Const)
            {
                //字段 数据成员
                ASTool.AS3.AS3Const constant = (ASTool.AS3.AS3Const)stmt;
                if (constant.Access.IsStatic == isstatic)
                {
                    if (constant.Access.IsOverride)
                    {
                        throw new BuildException(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                "The override attribute can only be used on a method defined in a class."));
                    }
                    else if (constant.Access.IsFinal)
                    {
                        throw new BuildException(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                "The final attribute can only be used on a method defined in a class."));
                    }

                    for (int j = 0; j < cls.classMembers.Count; j++)
                    {
                        if (cls.classMembers[j].name == constant.Name
                            &&
                                (
                                    cls.classMembers[j].inheritFrom == null
                                    ||
                                    (cls.classMembers[j].isPublic && constant.Access.IsPublic)
                                )
                            )
                        {
                            throw new BuildException(
                                new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                        "重复的类成员:" + constant.Name)
                                );
                        }
                    }

                    if (constant.Meta != null)
                    {
                        var metalist = constant.Meta;
                        for (int i = 0; i < metalist.Count; i++)
                        {
                            var m = metalist[i];
                            if (m.Value.Data.FF1Type == ASTool.AS3.Expr.FF1DataValueType.as3_array)
                            {
                                List<ASTool.AS3.Expr.AS3DataStackElement> data =
                                    (List<ASTool.AS3.Expr.AS3DataStackElement>)m.Value.Data.Value;

                                if (data.Count == 0)
                                {
                                    throw new BuildException(constant.token.line, constant.token.ptr, constant.token.sourceFile,
                                                                   "Meta 格式错误,至少有1个设置");
                                }
                                else
                                {
                                    string meta = data[0].Data.Value.ToString();
                                    if (meta == "native")
                                    {
                                        if (data.Count == 2)
                                        {
                                            string native_get = data[1].Data.Value.ToString();

                                            if (builder.bin.nativefunctionNameIndex.ContainsKey(native_get))
                                            {
                                                var nf = builder.bin.nativefunctions[builder.bin.nativefunctionNameIndex[native_get]];
                                                if (nf.isMethod)
                                                {
                                                    if (nf.parameters.Count != 0)
                                                    {
                                                        throw new BuildException(constant.token.line, constant.token.ptr, constant.token.sourceFile,
                                                            "本地函数 " + native_get + "不接受参数");
                                                    }
                                                    //if (nf.returnType != RunTimeDataType.rt_void)
                                                    //{
                                                    //    throw new BuildException(constant.token.line, constant.token.ptr, constant.token.sourceFile,
                                                    //        "本地函数 " + native_get + "必须返回*");
                                                    //}

                                                }
                                                else
                                                {
                                                    throw new BuildException(constant.token.line, constant.token.ptr, constant.token.sourceFile,
                                                        "本地函数 " + native_get + " isMethod属性不符");
                                                }
                                            }
                                            else
                                            {
                                                throw new BuildException(constant.token.line, constant.token.ptr, constant.token.sourceFile,
                                                    "本地函数 " + native_get + " 未注册");
                                            }
                                            //***检查通过，创建两个函数作为getter和setter放进去***

                                            {
                                                ASTool.AS3.AS3Function fc =
                                                    new ASTool.AS3.AS3Function(new ASTool.Token());
                                                fc.Access = constant.Access;
                                                fc.IsConstructor = false;
                                                fc.Name = constant.Name;
                                                fc.TypeStr = constant.TypeStr;
                                                fc.IsMethod = true;
                                                fc.IsGet = true;
												fc.Access.IsFinal = true;

                                                fc.Parameters = new List<ASTool.AS3.AS3Parameter>();
                                                //fc.ParentScope = builder.

                                                fc.StamentsStack.Push(new List<ASTool.AS3.IAS3Stmt>());
                                                fc.Meta = new List<ASTool.AS3.AS3Meta>();
                                                ASTool.AS3.AS3Meta me = new ASTool.AS3.AS3Meta(new ASTool.Token());

                                                List<ASTool.AS3.Expr.AS3DataStackElement> medata
                                                    = new List<ASTool.AS3.Expr.AS3DataStackElement>();

                                                me.Value = new ASTool.AS3.Expr.AS3DataStackElement();
                                                me.Value.Data = new ASTool.AS3.Expr.AS3DataValue();
                                                me.Value.Data.FF1Type = ASTool.AS3.Expr.FF1DataValueType.as3_array;
                                                me.Value.Data.Value = medata;

                                                medata.Add(new ASTool.AS3.Expr.AS3DataStackElement()
                                                {
                                                    Data = new ASTool.AS3.Expr.AS3DataValue()
                                                    {
                                                        FF1Type = ASTool.AS3.Expr.FF1DataValueType.const_string,
                                                        Value = "native"
                                                    }
                                                }
                                                );
                                                medata.Add(new ASTool.AS3.Expr.AS3DataStackElement()
                                                {
                                                    Data = new ASTool.AS3.Expr.AS3DataValue()
                                                    {
                                                        FF1Type = ASTool.AS3.Expr.FF1DataValueType.const_string,
                                                        Value = native_get
                                                    }
                                                }
                                                );
                                                fc.Meta.Add(me);

                                                buildClassMember(env, fc, cls, builder, isstatic);

                                            }



                                            return;

                                        }
                                        else
                                        {
                                            throw new BuildException(constant.token.line, constant.token.ptr, constant.token.sourceFile,
                                                "[native] const Field需要指定1个访问函数");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new BuildException(constant.token.line, constant.token.ptr, constant.token.sourceFile,
                                    "Meta 格式错误");
                            }
                        }

                    }


                    Field constfield = new Field(constant.Name, env.block.scope.members.Count, env.block.id, true);
                    env.block.scope.members.Add(constfield);
                    
                    constfield.isInternal = constant.Access.IsInternal;
                    constfield.isPrivate = constant.Access.IsPrivate;
                    constfield.isProtected = constant.Access.IsProtected;
                    constfield.isPublic = constant.Access.IsPublic;
                    constfield.isStatic = constant.Access.IsStatic;

                    ASBinCode.rtti.ClassMember member = new ASBinCode.rtti.ClassMember(constfield.name,cls,constfield);

                    member.isInternal = constfield.isInternal;
                    member.isPrivate = constfield.isPrivate;
                    member.isProtectd = constfield.isProtected;
                    member.isPublic = constfield.isPublic;
                    member.isStatic = constfield.isStatic;
                    member.isConst = constfield.isConst;

                    cls.classMembers.Add(member);

                    cls.fields.Add(member);

                    int s = 0;
                    if (member.isPrivate) s++;
                    if (member.isPublic) s++;
                    if (member.isProtectd) s++;
                    if (member.isInternal) s++;

                    if (s == 0)
                    {
                        member.isInternal = true;
                    }

                    if (s > 1)
                    {
                        throw new BuildException(
                                    new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                            "Only one of public, private, protected, or internal can be specified on a definition.")
                                    );

                    }

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
