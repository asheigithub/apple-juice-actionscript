using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
    /// <summary>
    /// 
    /// </summary>
    class MemberFinder
    {
        public static ASBinCode.IMember find(string name, CompileEnv env,bool isStaticMember,Builder builder,ASTool.Token token)
        {
            //List<IScope> findpath = new List<IScope>();
            //List<IScope> outscopes = new List<IScope>();//查找顺序，最后查找包外代码

            //Dictionary<IScope, ASBinCode.rtti.Class> finderclass = new Dictionary<IScope, ASBinCode.rtti.Class>();



            //{
            //    ASBinCode.rtti.Class defineClass = null;
            //    IScope s = env.block.scope;
            //    while (s !=null)
            //    {
            //        if (s is ASBinCode.scopes.ObjectInstanceScope)
            //        {
            //            ASBinCode.rtti.Class cls = ((ASBinCode.scopes.ObjectInstanceScope)s)._class;
            //            if (isStaticMember && cls.staticClass != null)
            //            {
            //                cls = cls.staticClass;
            //            }
            //            if (defineClass == null)
            //            {
            //                defineClass = cls;
            //                if (cls.mainClass != null)
            //                {
            //                    defineClass = cls.mainClass;
            //                }

            //            }
            //        }
            //        finderclass.Add(s,defineClass);

            //        if (s is ASBinCode.scopes.OutPackageMemberScope && !(ReferenceEquals(s,env.block.scope)))
            //        {
            //            outscopes.Add(s);

            //        }
            //        else
            //        {
            //            findpath.Add(s);
            //        }

            //        s = s.parentScope;
            //    }

            //    findpath.AddRange(outscopes);
            //}



            //ASBinCode.IScope scope = null;
            //for (int j = 0; j < findpath.Count; j++)
            //{
            //    scope = findpath[j];

            //    if (scope is ASBinCode.scopes.ObjectInstanceScope)
            //    {
            //        //查找类方法
            //        ASBinCode.rtti.Class cls = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;

            //        var fm = ASBinCode.ClassMemberFinder.find(cls, name, finderclass[scope]);
            //        if (fm != null)
            //        {
            //            return fm.bindField;
            //        }

            //    }

            //    if (!(scope is ASBinCode.scopes.OutPackageMemberScope)
            //        ||
            //        (
            //            finderclass[scope] == null
            //            ||
            //            ((ASBinCode.scopes.OutPackageMemberScope)scope).mainclass == finderclass[scope]
            //        )
            //        )
            //    {
            //        //从后往前找。可解决catch块同名变量e问题
            //        for (int i = scope.members.Count - 1; i >= 0; i--)
            //        {
            //            if (scope.members[i].name == name
            //                )
            //            {
            //                return scope.members[i].clone();
            //            }
            //        }
            //    }


            //}
            bool skipoutpackagescope = false;

            ASBinCode.rtti.Class defineClass = null;

            ASBinCode.IScope scope = env.block.scope;

            while (scope != null)
            {
                if (scope is ASBinCode.scopes.ObjectInstanceScope)
                {
                    //查找类方法
                    ASBinCode.rtti.Class cls = ((ASBinCode.scopes.ObjectInstanceScope)scope)._class;

                    if (defineClass == null)
                    {
                        defineClass = cls;
                        
                    }

                    if (!isStaticMember && cls.staticClass == null)
                    {
                        
                        defineClass = defineClass.instanceClass;
                        skipoutpackagescope = true;
                        break;
                    }
                    else
                    {
                        var fm = ASBinCode.ClassMemberFinder.find(cls, name, defineClass);
                        if (fm != null)
                        {
                            return fm.bindField;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (!(scope is ASBinCode.scopes.OutPackageMemberScope)
                    ||
                    (
                        defineClass == null
                        ||
                        ((ASBinCode.scopes.OutPackageMemberScope)scope).mainclass == defineClass
                    )
                    )
                {
                    //从后往前找。可解决catch块同名变量e问题
                    for (int i = scope.members.Count - 1; i >= 0; i--)
                    {
                        if (scope.members[i].name == name
                            )
                        {
                            return scope.members[i].clone();
                        }
                    }
                }
                scope = scope.parentScope;

            }

            //***如果成员未找到，查找@__buildin__//
            if (!env.isEval)
            {
                var buildin = TypeReader.findClassFromImports("@__buildin__", builder, token);
                if (buildin.Count == 1)
                {
                    var bi = buildin[0].staticClass;

                    var member = ClassMemberFinder.find(bi, name, bi);
                    if (member != null && !(member.bindField is ClassPropertyGetter) && member.inheritFrom==null
                        &&
                        member.name !="Object"
                        )
                    {
                        FindStaticMember sm = new FindStaticMember();
                        sm.classMember = member;
                        sm.static_class = new StaticClassDataGetter(bi);

                        return sm;
                        //return member.bindField;
                    }

                }
            }

            //***在静态成员中查找***
            var findstaticclass = defineClass;


            while (findstaticclass != null && findstaticclass.staticClass !=null)
            {
                var bi = findstaticclass.staticClass;
                var member = ClassMemberFinder.find(bi, name, defineClass.staticClass);
                if (member != null)
                {
                    if (member != null && member.bindField is ClassPropertyGetter && name == "prototype")
                    {
                        throw new BuildException(token.line,token.ptr,token.sourceFile, "Access of possibly undefined property prototype.");
                    }

                    FindStaticMember sm = new FindStaticMember();
                    sm.classMember = member;
                    sm.static_class = new StaticClassDataGetter(bi);
                    

                    return sm;
                    //return member.bindField;
                }
                findstaticclass = findstaticclass.super;
            }

            if (skipoutpackagescope)
            {
                return null;
            }
            

            if (defineClass !=null && defineClass.mainClass != null)
            {
                defineClass = defineClass.mainClass;
            }

            //***在包外代码中查找****
            if (defineClass != null && defineClass.staticClass != null)
            {
                IScope outpackagescope = null;

                if (defineClass.mainClass == null)
                {
                    outpackagescope =
                        builder._class_outScopeBlock[defineClass].scope;
                }
                else
                {
                    outpackagescope =
                        builder._class_outScopeBlock[defineClass.mainClass].scope;
                }

                for (int i = outpackagescope.members.Count - 1; i >= 0; i--)
                {
                    if (outpackagescope.members[i].name == name
                        )
                    {
                        //return outpackagescope.members[i].clone();
                        FindOutPackageScopeMember fo = new FindOutPackageScopeMember();
                        fo.member = outpackagescope.members[i].clone();


                        fo.outscopeclass = ((ASBinCode.scopes.OutPackageMemberScope)outpackagescope).mainclass;

                        return fo;    
                            //return fo;
                        
                        
                    }
                }

            }



            return null;
        }


        public static ASBinCode.rtti.ClassMember findClassMember(ASBinCode.rtti.Class cls,
            string name, 
            CompileEnv env,
            Builder builder
            )
        {
            if (env.block.scope is ASBinCode.scopes.FunctionScope)
            {
                ASBinCode.scopes.FunctionScope funcscope = env.block.scope as ASBinCode.scopes.FunctionScope;

                if (!funcscope.function.IsAnonymous && funcscope.parentScope is ASBinCode.scopes.ObjectInstanceScope)
                {
                    return ASBinCode.ClassMemberFinder.find(
                        cls, name,
                        ((ASBinCode.scopes.ObjectInstanceScope)funcscope.parentScope)._class);
                }
                
            }

            
            return ASBinCode.ClassMemberFinder.find(cls, name,
                builder.getClassByRunTimeDataType(ASBinCode.RunTimeDataType._OBJECT)
                    );
            
        }


    }
}
