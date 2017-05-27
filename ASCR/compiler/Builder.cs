using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASCompiler.compiler
{
    public class Builder :IClassFinder 
    {
        public bool isConsoleOut = true;

        private int blockseed = 0;
        internal int getBlockId() { return blockseed++; }
        private int functionseed = 0;
        internal int getFunctionId(){ return functionseed++;}

        private int classseed = 0;
        internal int getClassId() { return classseed++; }

        public List<BuildError> buildErrors = new List<BuildError>();

        internal Dictionary<int, Dictionary<IMember, ASBinCode.rtti.FunctionSignature>>
            dictSignatures = new Dictionary<int, Dictionary<IMember, ASBinCode.rtti.FunctionSignature>>();

        /// <summary>
        /// 记录当前正在编译的function
        /// </summary>
        internal Stack<ASTool.AS3.AS3Function> buildingfunctons = new Stack<ASTool.AS3.AS3Function>();
        internal Dictionary<ASBinCode.rtti.FunctionDefine, CodeBlock> dictfunctionblock = new Dictionary<ASBinCode.rtti.FunctionDefine, CodeBlock>();

        internal Dictionary<ASTool.AS3.AS3Function, ASBinCode.rtti.FunctionDefine>
            buildoutfunctions = new Dictionary<ASTool.AS3.AS3Function, ASBinCode.rtti.FunctionDefine>();


        internal Dictionary<ASTool.AS3.AS3ClassInterfaceBase, ASBinCode.rtti.Class>
            buildingclasses = new Dictionary<ASTool.AS3.AS3ClassInterfaceBase, ASBinCode.rtti.Class>();
        internal Dictionary<ASBinCode.rtti.ClassMember,ASTool.AS3.IAS3Stmt>
            _buildingmembers = new Dictionary< ASBinCode.rtti.ClassMember,ASTool.AS3.IAS3Stmt>();
        internal Dictionary<ASBinCode.rtti.Class, CompileEnv> _classbuildingEnv =
            new Dictionary<ASBinCode.rtti.Class, CompileEnv>();

        internal Dictionary<ASBinCode.rtti.Class, CodeBlock> _class_outScopeBlock 
            = new Dictionary<ASBinCode.rtti.Class, CodeBlock>();



        internal Dictionary<ASTool.AS3.AS3Parameter,utils.Tuple< ASBinCode.rtti.FunctionParameter,List<ASBinCode.rtti.Class>>>
            _toEvalDefaultParameters = new Dictionary<ASTool.AS3.AS3Parameter, utils.Tuple<ASBinCode.rtti.FunctionParameter, List<ASBinCode.rtti.Class>>>();

        internal Dictionary<RightValueBase, List<OpStep>> _propLines = new Dictionary<RightValueBase, List<OpStep>>();

        internal Dictionary<ASBinCode.rtti.Class, Dictionary<ASBinCode.rtti.ClassMember, List<ASBinCode.rtti.ClassMember>>>
            _overridefunctions = new Dictionary<ASBinCode.rtti.Class, Dictionary<ASBinCode.rtti.ClassMember, List<ASBinCode.rtti.ClassMember>>>();

        internal Dictionary<CodeBlock, CompileEnv> blockEnv = new Dictionary<CodeBlock, CompileEnv>();


        internal readonly bool isEval;
        public Builder(bool isEval)
        {
            this.isEval = isEval;
        }
        public Builder():this(false)
        {

        }


        /// <summary>
        /// 根据类型查找的类
        /// </summary>
        /// <param name="rttype"></param>
        /// <returns></returns>
        public ASBinCode.rtti.Class getClassByRunTimeDataType(RunTimeDataType rttype)
        {
            foreach (var item in buildingclasses)
            {
                if (item.Value.classid == rttype - RunTimeDataType._OBJECT)
                {
                    return item.Value;
                }
                else if (item.Value.staticClass != null && item.Value.staticClass.classid == rttype - RunTimeDataType._OBJECT)
                {
                    return item.Value.staticClass;
                }
            }
            return null;
        }


        internal Stack<ASBinCode.rtti.Class> _buildingClass
            = new Stack<ASBinCode.rtti.Class>();

        /// <summary>
        /// 当前的导入环境
        /// </summary>
        internal Stack<List<ASBinCode.rtti.Class>> _currentImports = new Stack<List<ASBinCode.rtti.Class>>();

        private void pushBuildError(BuildError err)
        {
            buildErrors.Add(err);

            if (isConsoleOut)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("file :" + err.srcFile);
                Console.WriteLine("line :" + (err.line+1) + " ptr :" + (err.ptr+1));
                Console.WriteLine(err.errorMsg);

                Console.ResetColor();
            }

            if (buildErrors.Count > 10)
            {
                throw new TooManyBuildErrorException();
            }

        }


        internal ASBinCode.CSWC bin;

        public CSWC getBuildOutSWC()
        {
            if (buildErrors.Count == 0)
            {
                return bin;
            }
            else
            {
                return null;
            }
        }

        public void Build(ASTool.AS3.AS3Proj proj,IList<ASBinCode.INativeFunctionRegister> extfunctions)
        {
            buildErrors.Clear();

            var lib= Grammar.makeLibProj();
            if (lib == null)
            {
                
                if (isConsoleOut)
                {
                    Console.WriteLine("lib库编译失败，编译已终止");
                }
                return;
            }

            bin = new CSWC();
            ASRuntime.nativefuncs.BuildInFunctionLoader.loadBuildInFunctions(bin);
            if (extfunctions != null)
            {
                for (int i = 0; i < extfunctions.Count; i++)
                {
                    extfunctions[i].registrationFunction(bin);
                }
            }

            try
            {
                List<ASTool.AS3.AS3SrcFile> tobuildfiles = new List<ASTool.AS3.AS3SrcFile>();
                tobuildfiles.AddRange(lib.SrcFiles);
                build_srcFiles(tobuildfiles, false, RunTimeDataType.unknown);
                //lib库代码编译

                tobuildfiles.Clear();
                tobuildfiles.AddRange(proj.SrcFiles);
                //ASTool.AS3.AS3SrcFile docfile=null;
                build_srcFiles(tobuildfiles, false, RunTimeDataType.unknown);


                if (isConsoleOut)
                {
                    Console.WriteLine("编译结束");
                }
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
            catch (TooManyBuildErrorException)
            {
                if (isConsoleOut)
                {
                    Console.WriteLine("编译错误过多，编译已终止");
                }
            }
            catch (InvalidOperationException)
            {
                pushBuildError(new BuildError(0, 0, "", "发生编译器内部错误"));

                if (isConsoleOut)
                {
                    Console.WriteLine("编译已终止");
                }
            }


        }

        private void build_srcFiles(List<ASTool.AS3.AS3SrcFile> tobuildfiles,bool isbuildvector,RunTimeDataType vectortype)
        {

            //先编译类说明
            for (int i = 0; i < tobuildfiles.Count; i++)
            {
                ASTool.AS3.AS3SrcFile srcfile = tobuildfiles[i];

                if (!System.IO.Path.GetDirectoryName(srcfile.srcFile)
                    .Replace(System.IO.Path.DirectorySeparatorChar, '.')
                    .EndsWith(srcfile.Package.Name))
                {
                    throw new BuildException(0, 0, srcfile.srcFile,
                        "包名和路径不匹配"
                        );
                }

                if (srcfile.Package.MainClass != null)
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(srcfile.srcFile)
                        != srcfile.Package.MainClass.Name
                        )
                    {
                        throw new BuildException(0, 0, srcfile.srcFile,
                            "文件名和类名不匹配"
                        );
                    }


                    builds.AS3ClassBuilder builder = new builds.AS3ClassBuilder();
                    var c = builder.buildClassDefine(srcfile.Package.MainClass, this, null, srcfile, isbuildvector, vectortype);

                    if (c.isdocumentclass)
                    {
                        //docfile = srcfile;
                    }

                    for (int j = 0; j < srcfile.Package.MainClass.innerClass.Count; j++)
                    {
                        builder.buildClassDefine(
                            srcfile.Package.MainClass.innerClass[j], this, buildingclasses[srcfile.Package.MainClass]
                            , srcfile, isbuildvector, vectortype
                            );
                    }

                    builds.AS3InterfaceBuilder interfacebulder = new builds.AS3InterfaceBuilder();
                    for (int j = 0; j < srcfile.Package.MainClass.innerInterface.Count; j++)
                    {
                        interfacebulder.buildInterfaceDefine(
                            srcfile.Package.MainClass.innerInterface[j],
                            this, buildingclasses[srcfile.Package.MainClass]
                            , srcfile, isbuildvector, vectortype
                            );
                    }

                }
                if (srcfile.Package.MainInterface != null)
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(srcfile.srcFile)
                        != srcfile.Package.MainInterface.Name
                        )
                    {
                        throw new BuildException(0, 0, srcfile.srcFile,
                            "文件名和接口名不匹配"
                        );
                    }
                    builds.AS3InterfaceBuilder builder = new builds.AS3InterfaceBuilder();
                    var c = builder.buildInterfaceDefine(
                        srcfile.Package.MainInterface, this, null, srcfile, isbuildvector, vectortype);

                    builds.AS3ClassBuilder cb = new builds.AS3ClassBuilder();
                    for (int j = 0; j < srcfile.Package.MainInterface.innerClass.Count; j++)
                    {
                        
                        cb.buildClassDefine(
                            srcfile.Package.MainInterface.innerClass[j], 
                            this, buildingclasses[srcfile.Package.MainInterface]
                            , srcfile, isbuildvector, vectortype
                            );
                    }

                    for (int j = 0; j < srcfile.Package.MainInterface.innerInterface.Count; j++)
                    {
                        builder.buildInterfaceDefine(
                            srcfile.Package.MainInterface.innerInterface[j],
                            this, buildingclasses[srcfile.Package.MainInterface]
                            , srcfile, isbuildvector, vectortype
                            );
                    }

                }
            }

            Dictionary<ASBinCode.rtti.Class, List<ASBinCode.rtti.Class>> classImports
                = new Dictionary<ASBinCode.rtti.Class, List<ASBinCode.rtti.Class>>();

            //****确定继承关系****
            for (int i = 0; i < tobuildfiles.Count; i++)
            {
                ASTool.AS3.AS3SrcFile srcfile = tobuildfiles[i];

                if (srcfile.Package.MainClass != null)
                {
                    var iimps = utils.ImportReader.readImports(srcfile, this);
                    classImports.Add(buildingclasses[srcfile.Package.MainClass], iimps);
                    _currentImports.Push(iimps);

                    builds.AS3ClassBuilder builder = new builds.AS3ClassBuilder();
                    builder.buildClassExtends(srcfile.Package.MainClass,this);
                    _currentImports.Pop();

                    for (int j = 0; j < srcfile.Package.MainClass.innerClass.Count; j++)
                    {
                        var imports = utils.ImportReader.readOutPackageImports(srcfile, this);
                        classImports.Add(buildingclasses[srcfile.Package.MainClass.innerClass[j]], imports);
                        _currentImports.Push(imports);

                        builder.buildClassExtends(srcfile.Package.MainClass.innerClass[j], this);

                        _currentImports.Pop();

                    }

                    for (int j = 0; j < srcfile.Package.MainClass.innerInterface.Count; j++)
                    {
                        var imports = utils.ImportReader.readOutPackageImports(srcfile, this);
                        classImports.Add(buildingclasses[srcfile.Package.MainClass.innerInterface[j]], imports);
                        _currentImports.Push(imports);

                        builds.AS3InterfaceBuilder ib = new builds.AS3InterfaceBuilder();
                        ib.buildInterfaceExtends(srcfile.Package.MainClass.innerInterface[j], this);

                        _currentImports.Pop();

                    }
                }

                if (srcfile.Package.MainInterface != null)
                {
                    var iimps = utils.ImportReader.readImports(srcfile, this);
                    classImports.Add(buildingclasses[srcfile.Package.MainInterface], iimps);
                    _currentImports.Push(iimps);

                    builds.AS3InterfaceBuilder builder = new builds.AS3InterfaceBuilder();
                    builder.buildInterfaceExtends(srcfile.Package.MainInterface, this);
                    _currentImports.Pop();

                    for (int j = 0; j < srcfile.Package.MainInterface.innerClass.Count; j++)
                    {
                        var imports = utils.ImportReader.readOutPackageImports(srcfile, this);
                        classImports.Add(buildingclasses[srcfile.Package.MainInterface.innerClass[j]], imports);
                        _currentImports.Push(imports);

                        builds.AS3ClassBuilder cb = new builds.AS3ClassBuilder();
                        cb.buildClassExtends(srcfile.Package.MainInterface.innerClass[j], this);

                        _currentImports.Pop();

                    }

                    for (int j = 0; j < srcfile.Package.MainInterface.innerInterface.Count; j++)
                    {
                        var imports = utils.ImportReader.readOutPackageImports(srcfile, this);
                        classImports.Add(buildingclasses[srcfile.Package.MainInterface.innerInterface[j]], imports);
                        _currentImports.Push(imports);

                        builder.buildInterfaceExtends(srcfile.Package.MainInterface.innerInterface[j], this);

                        _currentImports.Pop();

                    }
                }
            }
            //****查找循环继承***
            #region 循环继承检查
            for (int i = 0; i < tobuildfiles.Count; i++)
            {
                ASTool.AS3.AS3SrcFile srcfile = tobuildfiles[i];

                if (srcfile.Package.MainClass != null)
                {
                   
                    builds.AS3ClassBuilder builder = new builds.AS3ClassBuilder();
                    builder.checkCircularReference(srcfile.Package.MainClass,this);
                    
                    for (int j = 0; j < srcfile.Package.MainClass.innerClass.Count; j++)
                    {
                        builder.checkCircularReference(srcfile.Package.MainClass.innerClass[j], this);
                    }

                    for (int j = 0; j < srcfile.Package.MainClass.innerInterface.Count; j++)
                    {
                        
                        builds.AS3InterfaceBuilder ib = new builds.AS3InterfaceBuilder();
                        ib.checkCircularReference(srcfile.Package.MainClass.innerInterface[j], this);
                        ib.copyImplements(srcfile.Package.MainClass.innerInterface[j], this);

                    }
                }

                if (srcfile.Package.MainInterface != null)
                {
                    
                    builds.AS3InterfaceBuilder builder = new builds.AS3InterfaceBuilder();
                    builder.checkCircularReference(srcfile.Package.MainInterface, this);
                    builder.copyImplements(srcfile.Package.MainInterface, this);

                    for (int j = 0; j < srcfile.Package.MainInterface.innerClass.Count; j++)
                    {
                        
                        builds.AS3ClassBuilder cb = new builds.AS3ClassBuilder();
                        cb.checkCircularReference(srcfile.Package.MainInterface.innerClass[j], this);
                        
                    }

                    for (int j = 0; j < srcfile.Package.MainInterface.innerInterface.Count; j++)
                    {                      
                        builder.checkCircularReference(srcfile.Package.MainInterface.innerInterface[j], this);
                        builder.copyImplements(srcfile.Package.MainInterface.innerInterface[j], this);
                    }
                }
            }
            #endregion
            //***确定接口***
            for (int i = 0; i < tobuildfiles.Count; i++)
            {
                ASTool.AS3.AS3SrcFile srcfile = tobuildfiles[i];

                if (srcfile.Package.MainClass != null)
                {
                    
                    _currentImports.Push(classImports[buildingclasses[srcfile.Package.MainClass]]);

                    builds.AS3ClassBuilder builder = new builds.AS3ClassBuilder();
                    builder.buildClassImplements(srcfile.Package.MainClass, this);
                    _currentImports.Pop();

                    for (int j = 0; j < srcfile.Package.MainClass.innerClass.Count; j++)
                    {
                        _currentImports.Push(classImports[buildingclasses[srcfile.Package.MainClass.innerClass[j]]]);

                        builder.buildClassImplements(srcfile.Package.MainClass.innerClass[j], this);

                        _currentImports.Pop();
                    }
                }

                if (srcfile.Package.MainInterface != null)
                {
                    for (int j = 0; j < srcfile.Package.MainInterface.innerClass.Count; j++)
                    {
                        
                        _currentImports.Push(classImports[buildingclasses[srcfile.Package.MainInterface.innerClass[j]]]);

                        builds.AS3ClassBuilder cb = new builds.AS3ClassBuilder();
                        cb.buildClassImplements(srcfile.Package.MainInterface.innerClass[j], this);
                        _currentImports.Pop();

                    }
                }
            }

            //Dictionary<ASTool.AS3.AS3Class, ASBinCode.rtti.Class> copyclasses = new Dictionary<ASTool.AS3.AS3Class, ASBinCode.rtti.Class>();
            List<KeyValuePair<ASTool.AS3.AS3ClassInterfaceBase, ASBinCode.rtti.Class>> copyclasses = new List<KeyValuePair<ASTool.AS3.AS3ClassInterfaceBase, ASBinCode.rtti.Class>>();
            foreach (var item in buildingclasses)
            {
                if (!tobuildfiles.Contains(item.Key.Package.AS3File))
                {
                    continue;
                }
                copyclasses.Add(item);
            }
            //***按继承深度排序。
            copyclasses.Sort((
                KeyValuePair<ASTool.AS3.AS3ClassInterfaceBase, ASBinCode.rtti.Class> c1,
                KeyValuePair<ASTool.AS3.AS3ClassInterfaceBase, ASBinCode.rtti.Class> c2
                ) =>
            {
                if (c1.Value.isInterface && !c2.Value.isInterface)
                {
                    //if (c2.Value.implements.ContainsKey(c1.Value))
                    {
                        return -1;
                    }
                    //else
                    {
                        //return c1.Value.getRtType() - c2.Value.getRtType();
                    }
                }
                else if (!c1.Value.isInterface && c2.Value.isInterface)
                {
                    //if (c1.Value.implements.ContainsKey(c2.Value))
                    {
                        return 1;
                    }
                    //else
                    {
                        //return c1.Value.getRtType() - c2.Value.getRtType();
                    }
                }
                else if (c1.Value.isInterface && c2.Value.isInterface)
                {
                    if (c1.Value.implements.ContainsKey(c2.Value))
                    {
                        return 1;
                    }
                    else if (c2.Value.implements.ContainsKey(c1.Value))
                    {
                        return -1;
                    }
                }


                {
                    if (ClassMemberFinder.isInherits(c1.Value, c2.Value))
                    {
                        return 1;
                    }
                }
                {
                    if (ClassMemberFinder.isInherits(c2.Value, c1.Value))
                    {
                        return -1;
                    }
                }

                //比较继承深度

                int d1 = 0;
                var ds = c1.Value.super;
                while (ds !=null)
                {
                    d1++;
                    ds = ds.super;
                }

                int d2 = 0;
                var d2s = c2.Value.super;
                while (d2s != null)
                {
                    d2++;
                    d2s = d2s.super;
                }

                if (d1 != d2)
                {
                    return d1 - d2;
                }


                return  c1.Value.getRtType() - c2.Value.getRtType();
            });


            foreach (var item in copyclasses)
            {
                if (!tobuildfiles.Contains(item.Key.Package.AS3File))
                {
                    continue;
                }

                if (item.Key is ASTool.AS3.AS3Class)
                {
                    builds.AS3ClassBuilder builder = new builds.AS3ClassBuilder();
                    builder.buildClassDefineMembers(item.Key, this, false);
                }
                else
                {
                    builds.AS3InterfaceBuilder builder = new builds.AS3InterfaceBuilder();
                    builder.buildInterfaceMembers(item.Key, this);
                }
            }

            foreach (var item in copyclasses)
            {
                if (!tobuildfiles.Contains(item.Key.Package.AS3File))
                {
                    continue;
                }
                builds.AS3ClassBuilder builder = new builds.AS3ClassBuilder();
                builder.buildClassDefineMembers(item.Key, this,true);
            }


            //确定类成员类型 (由于类成员类型也可能是类，所以必须要先编译类说明。)
            foreach (var item in copyclasses)
            {
                if (!tobuildfiles.Contains(item.Key.Package.AS3File))
                {
                    continue;
                }
                _currentImports.Push(classImports[item.Value]);

                if (!buildingclasses[item.Key].isInterface)
                {
                    builds.AS3ClassBuilder builder = new builds.AS3ClassBuilder();
                    builder.buildMemberType(
                        buildingclasses[item.Key]
                        , this, item.Key.Package.AS3File);
                }
                else
                {
                    builds.AS3InterfaceBuilder builder = new builds.AS3InterfaceBuilder();
                    builder.buildInterfaceMemberType(
                        buildingclasses[item.Key]
                        , this, item.Key.Package.AS3File);
                }

                _currentImports.Pop();
            }


            //***编译静态成员赋值代码***
            foreach (var item in copyclasses)
            {
                if (!tobuildfiles.Contains(item.Key.Package.AS3File))
                {
                    continue;
                }

                ASBinCode.rtti.Class scls = item.Value.staticClass;

                _currentImports.Push(classImports[item.Value]);

                CompileEnv env = _classbuildingEnv[scls];
                for (int i = 0; i < scls.classMembers.Count; i++)
                {
                    var m = scls.classMembers[i];
                    if (m.bindField is ClassPropertyGetter || m.inheritFrom != null)
                    {
                        continue;
                    }
                    

                    var expr = _buildingmembers[m];
                    RunTimeValueBase defaultvalue = null;
                    if (expr is ASTool.AS3.AS3Const)
                    {
                        ASTool.AS3.AS3Const as3const = (ASTool.AS3.AS3Const)expr;
                        if (as3const.ValueExpr != null)
                        {
                            defaultvalue = ExpressionEval.Eval(as3const.ValueExpr);
                        }

                    }
                    else if (expr is ASTool.AS3.AS3Variable)
                    {
                        ASTool.AS3.AS3Variable as3var = (ASTool.AS3.AS3Variable)expr;
                        if (as3var.ValueExpr != null)
                        {
                            defaultvalue = ExpressionEval.Eval(as3var.ValueExpr);
                        }

                    }
                    m.defaultValue = defaultvalue;
                    if (defaultvalue != null || expr is ASTool.AS3.AS3Function)
                    {

                        buildStmt(env, expr);   //先给确定值的对象赋值

                    }
                }
                //**再给需要计算的字段赋值
                for (int i = 0; i < scls.classMembers.Count; i++)
                {
                    var m = scls.classMembers[i];
                    if (m.bindField is ClassPropertyGetter || m.inheritFrom !=null)
                    {
                        continue;
                    }
                    var expr = _buildingmembers[m];

                    if (m.defaultValue == null && !(expr is ASTool.AS3.AS3Function))
                    {
                        buildStmt(env, expr);   //先给确定值的对象赋值
                    }
                }

                //block必须有收尾工作
                {
                    env.completSteps();
                    env.block.totalRegisters = env.combieNeedStackSlots();
                }

                _currentImports.Pop();
            }

            Dictionary<ASBinCode.rtti.Class, CodeBlock> outScopeBlock = new Dictionary<ASBinCode.rtti.Class, CodeBlock>();

            //***编译包外代码***
            for (int i = 0; i < tobuildfiles.Count; i++)
            {
                ASTool.AS3.AS3SrcFile srcfile = tobuildfiles[i];
                if (srcfile.Package.MainClass != null)
                {
                    var imports = utils.ImportReader.readOutPackageImports(srcfile, this);
                    _currentImports.Push(imports);


                    List<ASTool.AS3.IAS3Stmt> outstmts = srcfile.OutPackagePrivateScope.StamentsStack.Peek();
                    ASBinCode.CodeBlock block = new ASBinCode.CodeBlock(
                        getBlockId(),
                        srcfile.Package.MainClass.Name + "::privateScope"
                        ,
                        buildingclasses[srcfile.Package.MainClass].classid,
                        true
                        );

                    block.scope = new ASBinCode.scopes.OutPackageMemberScope(buildingclasses[srcfile.Package.MainClass]);
                    //((ASBinCode.scopes.OutPackageMemberScope)block.scope).parentScope
                    //    = _classbuildingEnv[buildingclasses[srcfile.Package.MainClass].staticClass].block.scope;

                    buildingclasses[srcfile.Package.MainClass].outscopeblockid = block.id;
                    buildCodeBlock(outstmts, block);


                    outScopeBlock.Add(buildingclasses[srcfile.Package.MainClass], block);
                    _class_outScopeBlock.Add(buildingclasses[srcfile.Package.MainClass], block);

                    _currentImports.Pop();
                }
            }

            //***编译类实例成员赋值代码
            foreach (var item in copyclasses)
            {
                if (!tobuildfiles.Contains(item.Key.Package.AS3File))
                {
                    continue;
                }

                ASBinCode.rtti.Class cls = item.Value;
                _currentImports.Push(classImports[item.Value]);
                CompileEnv env = _classbuildingEnv[cls];

                //if (cls.mainClass == null)
                //{
                //    ((ASBinCode.scopes.ObjectInstanceScope)env.block.scope).parentScope =
                //        outScopeBlock[cls].scope;
                //}
                //else
                //{
                //    ((ASBinCode.scopes.ObjectInstanceScope)env.block.scope).parentScope =
                //        outScopeBlock[cls.mainClass].scope;
                //}

                for (int i = 0; i < cls.classMembers.Count; i++)
                {
                    var m = cls.classMembers[i];

                    if (m.bindField is ClassPropertyGetter || m.inheritFrom != null)
                    {
                        continue;
                    }

                    var expr = _buildingmembers[m];
                    RunTimeValueBase defaultvalue = null;
                    if (expr is ASTool.AS3.AS3Const)
                    {
                        ASTool.AS3.AS3Const as3const = (ASTool.AS3.AS3Const)expr;
                        if (as3const.ValueExpr != null)
                        {
                            defaultvalue = ExpressionEval.Eval(as3const.ValueExpr);
                        }
                    }
                    else if (expr is ASTool.AS3.AS3Variable)
                    {
                        ASTool.AS3.AS3Variable as3var = (ASTool.AS3.AS3Variable)expr;
                        if (as3var.ValueExpr != null)
                        {
                            defaultvalue = ExpressionEval.Eval(as3var.ValueExpr);
                        }
                    }
                    m.defaultValue = defaultvalue;
                    if (defaultvalue != null || expr is ASTool.AS3.AS3Function)
                    {

                        buildStmt(env, expr);   //先给确定值的对象赋值

                    }
                }
                //**再给需要计算的字段赋值
                for (int i = 0; i < cls.classMembers.Count; i++)
                {
                    var m = cls.classMembers[i];
                    if (m.bindField is ClassPropertyGetter || m.inheritFrom != null)
                    {
                        continue;
                    }

                    var expr = _buildingmembers[m];

                    if (m.defaultValue == null && !(expr is ASTool.AS3.AS3Function))
                    {
                        buildStmt(env, expr);   //先给确定值的对象赋值
                    }
                }

                //***检查Override***
                if (_overridefunctions.ContainsKey(cls) && buildErrors.Count == 0)
                {
                    var ov = _overridefunctions[cls];
                    foreach (var ovt in ov)
                    {
                        var nf = ovt.Key;

                        var nfs = buildoutfunctions[(ASTool.AS3.AS3Function)_buildingmembers[nf]].signature;

                        bool ispass = true;
                        for (int i = 0; i < ovt.Value.Count; i++)
                        {
                            var toovf = ovt.Value[i];   //***要重写的目标***
                            var otovfs = buildoutfunctions[(ASTool.AS3.AS3Function)_buildingmembers[toovf.inheritSrcMember]].signature;

                            if (otovfs.returnType != nfs.returnType)
                            {
                                pushBuildError(
                                    new BuildError(_buildingmembers[nf].Token.line,
                                        _buildingmembers[nf].Token.ptr,
                                        _buildingmembers[nf].Token.sourceFile,
                                        "Incompatible override."
                                    )
                                    );

                            }
                            else if (otovfs.parameters.Count != nfs.parameters.Count)
                            {
                                pushBuildError(
                                    new BuildError(_buildingmembers[nf].Token.line,
                                        _buildingmembers[nf].Token.ptr,
                                        _buildingmembers[nf].Token.sourceFile,
                                        "Incompatible override."
                                    )
                                    );
                            }
                            else
                            {

                                //***比较所有参数***
                                for (int j = 0; j < nfs.parameters.Count; j++)
                                {
                                    if (nfs.parameters[j].type != otovfs.parameters[j].type
                                        ||
                                        nfs.parameters[j].isPara != otovfs.parameters[j].isPara
                                        )
                                    {
                                        ispass = false;
                                        pushBuildError(
                                            new BuildError(_buildingmembers[nf].Token.line,
                                                _buildingmembers[nf].Token.ptr,
                                                _buildingmembers[nf].Token.sourceFile,
                                                "Incompatible override."
                                            )
                                        );
                                    }
                                }
                            }
                        }

                        if (ispass) //检查通过，覆盖原函数
                        {
                            for (int i = 0; i < ovt.Value.Count; i++)
                            {

                                var tooverridefunc = ovt.Value[i];

                                if (cls.classMembers.Contains(tooverridefunc))
                                {
                                    for (int j = 0; j < cls.classMembers.Count; j++)
                                    {
                                        if (cls.classMembers[j] == tooverridefunc)
                                        {
                                            cls.classMembers[j] = nf;

                                        }

                                        if (cls.classMembers[j].bindField is ClassPropertyGetter)
                                        {
                                            ClassPropertyGetter pg = (ClassPropertyGetter)cls.classMembers[j].bindField;
                                            if (pg.getter == tooverridefunc.bindField)
                                            {
                                                pg.getter = (MethodGetterBase)nf.bindField;
                                            }
                                            if (pg.setter == tooverridefunc.bindField)
                                            {
                                                pg.setter = (MethodGetterBase)nf.bindField;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException();
                                }

                            }
                        }


                    }

                }
                //***确定类实现的接口****
                if (!cls.isInterface && buildErrors.Count==0)
                {
                    List<ASBinCode.rtti.Class> implinterfaces = new List<ASBinCode.rtti.Class>();
                    foreach (var impl in cls.implements)
                    {
                        implinterfaces.Add(impl.Key);
                    }

                    foreach (var impls in implinterfaces)
                    {
                        int[] idxlist=new int[ impls.classMembers.Count ];
                        cls.implements[impls] = idxlist;

                        for (int i = 0; i < idxlist.Length; i++)
                        {
                            var implmember = impls.classMembers[i];

                            bool found = false; //查找接口实现
                            for (int j = 0; j < cls.classMembers.Count; j++)
                            {
                                var clsmember = cls.classMembers[j];
                                if (clsmember.name == implmember.name)
                                {
                                    found = true;
                                    if (!clsmember.isPublic)
                                    {
                                        throw new BuildException(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile,
                                            "interface method "
                                            + implmember.name + " in interface " + impls.name +
                                            " not implemented by class " + cls.name 
                                            + ". (实现接口的方法必须是public)");
                                    }

                                    if (clsmember.bindField is ClassPropertyGetter 
                                        || 
                                        implmember.bindField is ClassPropertyGetter)
                                    {
                                        if (!(clsmember.bindField is ClassPropertyGetter
                                        && implmember.bindField is ClassPropertyGetter))
                                        {
                                            throw new BuildException(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile,
                                            "interface method "
                                            + implmember.name + " in interface " + impls.name +
                                            " not implemented by class " + cls.name
                                            + ". (一个是访问器一个不是)");
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    //***比较签名***
                                    var clssig = 
                                        buildoutfunctions[(ASTool.AS3.AS3Function)_buildingmembers[clsmember]].signature;
                                    ASBinCode.rtti.FunctionSignature interfacesig;
                                    if (implmember.inheritFrom == null)
                                    {
                                        interfacesig = buildoutfunctions[(ASTool.AS3.AS3Function)_buildingmembers[implmember]].signature;
                                    }
                                    else
                                    {
                                        interfacesig = buildoutfunctions[(ASTool.AS3.AS3Function)_buildingmembers[implmember.inheritSrcMember]].signature;
                                    }

                                    if (clssig.returnType != interfacesig.returnType)
                                    {
                                        throw new BuildException(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile,
                                            "interface method "
                                            + implmember.name + " in interface " + impls.name +
                                            " not implemented by class " + cls.name
                                            + ". (返回类型不符)");

                                    }
                                    else if (clssig.parameters.Count != interfacesig.parameters.Count)
                                    {
                                        throw new BuildException(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile,
                                            "interface method "
                                            + implmember.name + " in interface " + impls.name +
                                            " not implemented by class " + cls.name
                                            + ". (参数签名不符)");
                                    }
                                    else
                                    {

                                        //***比较所有参数***
                                        for (int k = 0; k < clssig.parameters.Count; k++)
                                        {
                                            if (clssig.parameters[k].type != interfacesig.parameters[k].type
                                                ||
                                                clssig.parameters[k].isPara != interfacesig.parameters[k].isPara
                                                )
                                            {
                                                throw new BuildException(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile,
                                                     "interface method "
                                                     + implmember.name + " in interface " + impls.name +
                                                     " not implemented by class " + cls.name
                                                     + ". (参数签名不符)");
                                            }
                                        }
                                    }

                                    idxlist[i] = j;

                                }
                            }
                            if (!found)
                            {
                                throw new BuildException(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile,
                                    "interface method "
                                    + implmember.name + " in interface " + impls.name +
                                    " not implemented by class " + cls.name);
                            }
                        }

                    }
                }

                if (cls.isLink_System && cls.staticClass.linkObjCreator == null)
                {
                    throw new BuildException(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile,
                                    "[link_system]的类必须有[creator]函数");
                                    
                }

                //block必须有收尾工作
                {
                    env.completSteps();
                    env.block.totalRegisters = env.combieNeedStackSlots();
                    
                }
                _currentImports.Pop();
            }
            
            if (buildErrors.Count == 0)
            {
                foreach (var item in _classbuildingEnv)
                {
                    var block = item.Value.block;
                    while (bin.blocks.Count <= block.id)
                    {
                        bin.blocks.Add(null);
                    }
                    bin.blocks[block.id] = block;
                }
                foreach (var item in outScopeBlock)
                {
                    var block = item.Value;
                    while (bin.blocks.Count <= block.id)
                    {
                        bin.blocks.Add(null);
                    }
                    bin.blocks[block.id] = block;
                }
                foreach (var item in dictfunctionblock)
                {
                    var block = item.Value;
                    while (bin.blocks.Count <= block.id)
                    {
                        bin.blocks.Add(null);
                    }
                    bin.blocks[block.id] = block;
                }
                //bin.blocks.Add(block);
                //bin.blocks.Sort((CodeBlock b1, CodeBlock b2) => { return b1.id - b2.id; });


                foreach (var item in buildoutfunctions.Values)
                {
                    while (bin.functions.Count <= item.functionid)
                    {
                        bin.functions.Add(null);
                    }
                    bin.functions[item.functionid] = item;

                }

                foreach (var item in buildingclasses)
                {
                    if (!tobuildfiles.Contains(item.Key.Package.AS3File))
                    {
                        continue;
                    }

                    while (bin.classes.Count <= item.Value.classid)
                    {
                        bin.classes.Add(null);
                    }
                    bin.classes[item.Value.classid] = item.Value;

                    if (item.Value.constructor != null)
                    {
                        //***查找构造函数id***
                        //Field field = (Field)_classbuildingEnv[item.Value].block.scope.members[
                        //    item.Value.constructor.index];

                        MethodGetterBase field = (MethodGetterBase)item.Value.constructor.bindField;
                        int blockid = field.refdefinedinblockid;

                        var signature =
                                dictSignatures[blockid][field];
                        foreach (var func in buildoutfunctions.Values)
                        {
                            if (func.signature == signature)
                            {
                                item.Value.constructor_functionid = func.functionid;
                                break;
                            }
                        }
                    }

                    while (bin.classes.Count <= item.Value.staticClass.classid)
                    {
                        bin.classes.Add(null);
                    }
                    bin.classes[item.Value.staticClass.classid] = item.Value.staticClass;
                }

            }
            if (buildErrors.Count == 0)
            {
                //***编译参数默认值
                foreach (var item in _toEvalDefaultParameters)
                {
                    builds.AS3FunctionBuilder builder = new builds.AS3FunctionBuilder();
                    builder.buildParameterDefaultValue(item.Key, item.Value.item1, item.Value.item2, this);
                }
                _toEvalDefaultParameters.Clear();
            }
            ASBinCode.rtti.FunctionDefine outf;
            if (!bin.operatorOverrides.Check(out outf))
            {
                foreach (var item in buildoutfunctions)
                {
                    if (item.Value == outf)
                    {
                        pushBuildError(
                            new BuildError(item.Key.token.line, item.Key.token.ptr, item.Key.token.sourceFile, 
                            ">,<,==,!=,>=,<=必须同时重载")
                            );


                        break;
                    }
                }
            }

            //先查找函数私有变量，不考虑参数。如果这个私有变量只在函数体内使用，则转化为1个Register.
            #region 变量查找

            
            foreach (var f in bin.functions)
            {
                if (f == null || f.isYield || f.isNative)
                {
                    continue;
                }
                var fb = bin.blocks[f.blockid];
                if (blockEnv.ContainsKey(fb))
                {
                    blockEnv[fb].convertVarToReg(this, f);
                }
            }

            #endregion

        }


        internal void buildCodeBlock(List<ASTool.AS3.IAS3Stmt> statements, CodeBlock block)
        {
            CompileEnv env = new CompileEnv(block, false);
            blockEnv.Add(block, env);

            //先提取变量定义
            for (int i = 0; i < statements.Count; i++)
            {
                buildVariables(env, statements[i]);
            }

            //***生成所有命名函数的函数签名****
            for (int i = 0; i < env.tobuildNamedfunction.Count; i++)
            {
                buildNamedFunctionSignature(env, env.tobuildNamedfunction[i]);
            }
            //***编译所有命名函数***
            for (int i = 0; i < env.tobuildNamedfunction.Count; i++)
            {
                buildNamedFunctions(env, env.tobuildNamedfunction[i]);
            }
            env.tobuildNamedfunction = null;


            for (int i = 0; i < statements.Count; i++)
            {
                buildStmt(env, statements[i]);
            }
            env.completSteps();
            block.totalRegisters = env.combieNeedStackSlots();
        }

        internal void buildNamedFunctionSignature(CompileEnv env, ASTool.AS3.AS3Function as3function)
        {
            try
            {
                //之前检查过是命名函数所以不需再检查

                ASBinCode.IMember member = MemberFinder.find(as3function.Name, env,as3function.Access.IsStatic,this,as3function.token);

                if (member is MethodGetterBase)
                {

                }
                else if (member is ClassPropertyGetter)
                {
                    ClassPropertyGetter prop = (ClassPropertyGetter)member;
                    if (as3function.IsGet)
                    {
                        member = prop.getter;
                    }
                    else if (as3function.IsSet)
                    {
                        member = prop.setter;
                    }
                    else
                    {
                        pushBuildError(new BuildError(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                        "此处应该是个属性访问器"));
                        return;
                    }
                }
                else if (!(member is VariableBase))
                {
                    pushBuildError(new BuildError(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                        "此处应该是个Variable"));
                    return;
                }

               
                builds.AS3FunctionBuilder builder = new builds.AS3FunctionBuilder();
                ASBinCode.rtti.FunctionSignature signature = builder.buildSignature(
                    env, as3function, this
                    );

                if (as3function.IsGet)
                {
                    if (signature.parameters.Count > 0)
                    {
                        pushBuildError(new BuildError(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                            "getter访问器不能带参数"));
                        return;
                    }
                    if (signature.returnType == RunTimeDataType.fun_void)
                    {
                        pushBuildError(new BuildError(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                            "getter访问器必须有返回值"));
                        return;
                    }
                }
                else if (as3function.IsSet)
                {
                    if (signature.parameters.Count !=1 )
                    {
                        pushBuildError(new BuildError(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                            "setter访问器参数必须是1个"));
                        return;
                    }
                    if (signature.returnType != RunTimeDataType.fun_void
                        &&
                        signature.returnType != RunTimeDataType.rt_void
                        )
                    {
                        pushBuildError(new BuildError(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                            "setter访问器不能有返回值"));
                        return;
                    }

                }

                if (!dictSignatures.ContainsKey(env.block.id))
                {
                    dictSignatures.Add(env.block.id, new Dictionary<IMember, ASBinCode.rtti.FunctionSignature>());
                }
                dictSignatures[env.block.id].Add(member, signature);   
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
        }


        /// <summary>
        /// 编译命名后的闭包函数
        /// </summary>
        /// <param name="env"></param>
        /// <param name="stmt"></param>
        internal void buildNamedFunctions(CompileEnv env, ASTool.AS3.AS3Function as3function)
        {
            try
            {

                
                    if (
                        !as3function.IsMethod) //闭包
                    {
                        if (!as3function.IsAnonymous)
                        {

                            ASBinCode.IMember member = MemberFinder.find(as3function.Name, env,as3function.Access.IsStatic,this,as3function.token);

                            if (!(member is VariableBase))
                            {
                                pushBuildError(new BuildError(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile,
                                    "此处应该是个Variable"));
                                return;
                            }

                            var rtVariable = (VariableBase)member;

                            builds.AS3FunctionBuilder builder = new builds.AS3FunctionBuilder();
                            var func = builder.buildAS3Function(env,
                                as3function, this, dictSignatures[env.block.id][member]);

                            OpStep step = new OpStep(OpCode.assigning, new SourceToken(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile));
                            step.reg = rtVariable;
                            step.regType = rtVariable.valueType;
                            step.arg1 = new ASBinCode.rtData.RightValue(func);
                            step.arg1Type = RunTimeDataType.rt_function;
                            step.arg2 = null;
                            step.arg2Type = RunTimeDataType.unknown;

                            env.block.opSteps.Add(step);

                            OpStep stepbind = new OpStep(OpCode.bind_scope, new SourceToken(as3function.token.line, as3function.token.ptr, as3function.token.sourceFile));
                            stepbind.reg = null;
                            stepbind.regType = RunTimeDataType.unknown;
                            stepbind.arg1 = rtVariable;
                            stepbind.arg1Type = rtVariable.valueType;
                            stepbind.arg2 = null;
                            stepbind.arg2Type = RunTimeDataType.unknown;

                            env.block.opSteps.Add(stepbind);

                        }
                    }


                

            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
        }

        /// <summary>
        /// 先提取代码段中的所有变量定义
        /// </summary>
        /// <param name="env"></param>
        /// <param name="stmt"></param>
        internal void buildVariables(CompileEnv env, ASTool.AS3.IAS3Stmt stmt)
        {
            try
            {
                if (stmt is ASTool.AS3.AS3Block)
                {
                    ASTool.AS3.AS3Block b = (ASTool.AS3.AS3Block)stmt;
                    for (int i = 0; i < b.CodeList.Count; i++)
                    {
                        buildVariables(env, b.CodeList[i]);
                    }
                }
                else if (stmt is ASTool.AS3.AS3Function)
                {
                    if (env.isEval)
                    {
                        pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "当前环境处于表达式求值环境不编译function"));
                        return;
                    }

                    ASTool.AS3.AS3Function as3function = (ASTool.AS3.AS3Function)stmt;
                    //if (as3function.IsMethod)
                    //{
                    //    pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                    //                "类的方法不应该出现在这里"));
                    //    return;
                    //}
                    //else
                    {
                        if (!as3function.IsAnonymous)
                        {
                            if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope
                                &&
                                as3function.IsMethod
                                )
                            {
                                pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "类的方法不应该出现在这里"));
                                return;
                            }

                            if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope
                                &&
                                (as3function.IsGet || as3function.IsSet)
                                )
                            {
                                pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "访问器方法只能是类的方法"));
                                return;
                            }

                            if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope
                                &&
                                as3function.Access.IsStatic
                                )
                            {
                                pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The static attribute may be used only on definitions inside a class."));
                                return;
                            }

                            //***构造一个变量名等于函数名的function数据类型
                            for (int i = 0; i < env.block.scope.members.Count; i++) //scope内查找是否有重复
                            {
                                IMember member = env.block.scope.members[i];
                                if (member.name == as3function.Name)
                                {
                                    {
                                        pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                            "重复声明 " + as3function.Name));
                                        return;
                                    }
                                }
                            }

                            VariableBase rtVariable = new Variable(as3function.Name, env.block.scope.members.Count, env.block.id);
                            env.block.scope.members.Add(rtVariable);
                            rtVariable.valueType = RunTimeDataType.rt_function;

                            env.tobuildNamedfunction.Add(as3function);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3IF)
                {
                    ASTool.AS3.AS3IF as3if = (ASTool.AS3.AS3IF)stmt;

                    buildVariables(env, as3if.Condition);
                    if (as3if.TruePass != null)
                    {
                        for (int i = 0; i < as3if.TruePass.Count; i++)
                        {
                            buildVariables(env, as3if.TruePass[i]);
                        }
                    }
                    if (as3if.FalsePass != null)
                    {
                        for (int i = 0; i < as3if.FalsePass.Count; i++)
                        {
                            buildVariables(env, as3if.FalsePass[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3For)
                {
                    ASTool.AS3.AS3For as3for = (ASTool.AS3.AS3For)stmt;
                    if (as3for.Part2 != null)
                    {
                        buildVariables(env, as3for.Part2);
                    }
                    if (as3for.Part3 != null)
                    {
                        buildVariables(env, as3for.Part3);
                    }
                    if (as3for.Body != null)
                    {
                        for (int i = 0; i < as3for.Body.Count; i++)
                        {
                            buildVariables(env, as3for.Body[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3ForIn)
                {
                    ASTool.AS3.AS3ForIn as3forin = (ASTool.AS3.AS3ForIn)stmt;
                    buildVariables(env, as3forin.ForArg);
                    if (as3forin.Body != null)
                    {
                        for (int i = 0; i < as3forin.Body.Count; i++)
                        {
                            buildVariables(env, as3forin.Body[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3ForEach)
                {
                    ASTool.AS3.AS3ForEach as3foreach = (ASTool.AS3.AS3ForEach)stmt;
                    buildVariables(env, as3foreach.ForArg);
                    if (as3foreach.Body != null)
                    {
                        for (int i = 0; i < as3foreach.Body.Count; i++)
                        {
                            buildVariables(env, as3foreach.Body[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3While)
                {
                    ASTool.AS3.AS3While as3while = (ASTool.AS3.AS3While)stmt;

                    if (as3while.Condition != null)
                    {
                        buildVariables(env, as3while.Condition);
                    }
                    if (as3while.Body != null)
                    {
                        for (int i = 0; i < as3while.Body.Count; i++)
                        {
                            buildVariables(env, as3while.Body[i]);
                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3DoWhile)
                {
                    ASTool.AS3.AS3DoWhile as3dowhile = (ASTool.AS3.AS3DoWhile)stmt;
                    if (as3dowhile.Body != null)
                    {
                        for (int i = 0; i < as3dowhile.Body.Count; i++)
                        {
                            buildVariables(env, as3dowhile.Body[i]);
                        }
                    }
                    if (as3dowhile.Condition != null)
                    {
                        buildVariables(env, as3dowhile.Condition);
                    }
                }
                else if (stmt is ASTool.AS3.AS3Switch)
                {
                    ASTool.AS3.AS3Switch as3switch = (ASTool.AS3.AS3Switch)stmt;
                    for (int i = 0; i < as3switch.CaseList.Count; i++)
                    {
                        var c = as3switch.CaseList[i];
                        if (c.Body != null)
                        {
                            for (int j = 0; j < c.Body.Count; j++)
                            {
                                buildVariables(env, c.Body[j]);
                            }

                        }
                    }
                }
                else if (stmt is ASTool.AS3.AS3Try)
                {
                    ASTool.AS3.AS3Try as3try = (ASTool.AS3.AS3Try)stmt;
                    as3try.holdTryId = env.getLabelId();

                    if (as3try.TryBlock != null)
                    {
                        for (int i = 0; i < as3try.TryBlock.Count; i++)
                        {
                            buildVariables(env, as3try.TryBlock[i]);
                        }
                    }
                    for (int i = 0; i < as3try.CatchList.Count; i++)
                    {
                        var c = as3try.CatchList[i];

                        builds.AS3TryBuilder b = new builds.AS3TryBuilder();
                        b.buildCatchVariable(env, c, i, as3try.holdTryId, this);

                        for (int j = 0; j < c.CatchBlock.Count; j++)
                        {
                            buildVariables(env, c.CatchBlock[j]);
                        }

                    }

                    if (as3try.FinallyBlock != null)
                    {
                        for (int i = 0; i < as3try.FinallyBlock.Count; i++)
                        {
                            buildVariables(env, as3try.FinallyBlock[i]);
                        }
                    }

                }
                else if (stmt is ASTool.AS3.AS3Const)
                {
                    ASTool.AS3.AS3Const variable = (ASTool.AS3.AS3Const)stmt;
                    for (int i = 0; i < env.block.scope.members.Count; i++) //scope内查找是否有重复
                    {
                        IMember member = env.block.scope.members[i];
                        if (member.name == variable.Name)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                "重复声明 " + variable.Name));
                            return;
                        }
                    }
                    if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope
                        &&
                        variable.Access.IsStatic
                        )
                    {
                        pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The static attribute may be used only on definitions inside a class." + variable.Name));
                        return;
                    }
                    if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope)
                    {
                        if (variable.Access.IsInternal || variable.Access.IsPublic || variable.Access.IsPrivate
                            || variable.Access.IsProtected
                            )
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "Access modifier may be used only on class property definitions."));
                        }
                        else if (variable.Access.IsOverride)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The override attribute can only be used on a method defined in a class."));
                        }
                        else if (variable.Access.IsFinal)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The final attribute can only be used on a method defined in a class."));
                        }
                    }
                    if (env.block.scope is ASBinCode.scopes.FunctionScope)
                    {
                        if (variable.Access.IsInternal || variable.Access.IsPublic || variable.Access.IsPrivate
                            || variable.Access.IsProtected
                            )
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "Access modifier not allowed on declarations inside a function."));

                        }
                        else if (variable.Access.IsOverride)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The override attribute can only be used on a method defined in a class."));
                        }
                        else if (variable.Access.IsFinal)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The final attribute can only be used on a method defined in a class."));
                        }
                    }
                    {
                        VariableBase rtVariable = new Variable(variable.Name, env.block.scope.members.Count, env.block.id, true);

                        env.block.scope.members.Add(rtVariable);
                        try
                        {
                            rtVariable.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, stmt.Token, this);
                        }
                        catch (BuildException ex)
                        {
                            pushBuildError(ex.error);
                        }

                    }
                }
                else if (stmt is ASTool.AS3.AS3Variable)
                {
                    ASTool.AS3.AS3Variable variable = (ASTool.AS3.AS3Variable)stmt;

                    if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope
                        &&
                        variable.Access.IsStatic
                        )
                    {
                        pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The static attribute may be used only on definitions inside a class." + variable.Name));
                        return;
                    }
                    if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope)
                    {
                        if (variable.Access.IsInternal || variable.Access.IsPublic || variable.Access.IsPrivate
                            || variable.Access.IsProtected
                            )
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "Access modifier may be used only on class property definitions."));
                        }
                        else if (variable.Access.IsOverride)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The override attribute can only be used on a method defined in a class."));
                        }
                        else if (variable.Access.IsFinal)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The final attribute can only be used on a method defined in a class."));
                        }
                    }
                    if (env.block.scope is ASBinCode.scopes.FunctionScope)
                    {
                        if (variable.Access.IsInternal || variable.Access.IsPublic || variable.Access.IsPrivate
                            || variable.Access.IsProtected
                            )
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "Access modifier not allowed on declarations inside a function."));
                        }
                        else if (variable.Access.IsOverride)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The override attribute can only be used on a method defined in a class."));
                        }
                        else if (variable.Access.IsFinal)
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "The final attribute can only be used on a method defined in a class."));
                        }
                    }

                    for (int i = 0; i < env.block.scope.members.Count; i++) //scope内查找是否有重复
                    {
                        IMember member = env.block.scope.members[i];
                        if (member.name == variable.Name)
                        {
                            if (!(member is VariableBase) || ((VariableBase)member).isConst)
                            {
                                pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "重复声明 " + variable.Name));
                                return;
                            }

                            VariableBase var = (VariableBase)member;

                            var newtype = TypeReader.fromSourceCodeStr(variable.TypeStr, stmt.Token, this);

                            if (newtype == var.valueType)
                            {
                                //和原类型完全相同
                                return;
                            }
                            if (newtype != RunTimeDataType.rt_void && var.valueType != RunTimeDataType.rt_void
                                )
                            {
                                pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                    "类型定义不明确 " + variable.Name));
                                return;
                            }
                            else
                            {
                                if (var.valueType == RunTimeDataType.rt_void)
                                {
                                    //原类型是任意类型
                                    return;
                                }
                                else
                                {
                                    //新类型为*

                                    if (variable.ValueExpr != null)
                                    {
                                        //读取测试

                                        CompileEnv tempEnv = new CompileEnv(new CodeBlock(0, "temp", -65535, true), false);
                                        buildExpression(tempEnv, variable.ValueExpr);
                                        RightValueBase tempRv = builds.ExpressionBuilder.getRightValue(tempEnv, variable.ValueExpr.Value,
                                            stmt.Token, new Builder(true)
                                            );
                                        newtype = tempRv.valueType;
                                    }

                                    if (!ASRuntime.TypeConverter.testImplicitConvert(newtype, var.valueType, this))
                                    {
                                        pushBuildError(new BuildTypeError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                        newtype, var.valueType, this));
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }

                                }


                            }

                        }
                    }

                    {
                        VariableBase rtVariable = new Variable(variable.Name, env.block.scope.members.Count, env.block.id, false);
                        env.block.scope.members.Add(rtVariable);
                        try
                        {
                            rtVariable.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, stmt.Token, this);
                        }
                        catch (BuildException ex)
                        {
                            pushBuildError(ex.error);
                        }

                    }

                }
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
        }




        internal void buildStmt(CompileEnv env, ASTool.AS3.IAS3Stmt stmt)
        {
            try
            {

                if (stmt is ASTool.AS3.AS3Block)
                {
                    ASTool.AS3.AS3Block b = (ASTool.AS3.AS3Block)stmt;

                    string lbl = b.label;
                    int stlen = env.block.opSteps.Count;

                    if (!string.IsNullOrEmpty(lbl))
                    {
                        //***插入Label开始**
                        OpStep opLblSt = new OpStep(OpCode.flag, new SourceToken(b.Token.line, b.Token.ptr, b.Token.sourceFile));
                        opLblSt.flag = "Label_Start_" + b.label;
                        env.block.opSteps.Add(opLblSt);
                    }

                    for (int i = 0; i < b.CodeList.Count; i++)
                    {
                        buildStmt(env, b.CodeList[i]);
                    }

                    if (!string.IsNullOrEmpty(lbl))
                    {
                        //***插入Label结束**
                        OpStep opLblSt = new OpStep(OpCode.flag, new SourceToken(b.Token.line, b.Token.ptr, b.Token.sourceFile));
                        opLblSt.flag = "Label_End_" + b.label;
                        env.block.opSteps.Add(opLblSt);

                        for (int i = stlen; i < env.block.opSteps.Count; i++)
                        {
                            //加入块标记
                            var step = env.block.opSteps[i];
                            if (step.labels == null)
                            {
                                step.labels = new Stack<string>();
                            }

                            if (step.labels.Contains(lbl))
                            {
                                throw new BuildException(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                        "重复定义标签块: [" + lbl + "]");
                            }
                            step.labels.Push(lbl);
                        }
                    }

                }
                else if (stmt is ASTool.AS3.AS3Function)
                {
                    ASTool.AS3.AS3Function as3function = (ASTool.AS3.AS3Function)stmt;

                    {
                        //if (as3function.IsMethod)
                        //{
                        //    pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                        //                "类的方法不应该出现在这里"));
                        //    return;
                        //}
                        if (env.block.scope is ASBinCode.scopes.OutPackageMemberScope
                                &&
                                as3function.IsMethod
                                )
                        {
                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                "类的方法不应该出现在这里"));
                            return;
                        }
                        else
                        {
                            if (as3function.IsAnonymous || as3function.IsMethod)
                            {
                                ASBinCode.rtti.FunctionSignature signature = null;
                                if (as3function.IsMethod)
                                {
                                    if (as3function.IsGet)
                                    {
                                        IMember member = MemberFinder.find(as3function.Name, env, as3function.Access.IsStatic, this, as3function.token);

                                        if (
                                            !dictSignatures.ContainsKey(env.block.id)
                                            ||
                                            !dictSignatures[env.block.id].ContainsKey(((ClassPropertyGetter)member).getter))
                                        {
                                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                "访问器未找到"));
                                            return;
                                        }

                                        signature = dictSignatures[env.block.id][
                                            ((ClassPropertyGetter)member).getter];
                                    }
                                    else if (as3function.IsSet)
                                    {
                                        IMember member = MemberFinder.find(as3function.Name, env, as3function.Access.IsStatic, this, as3function.token);

                                        if (
                                            !dictSignatures.ContainsKey(env.block.id)
                                            ||
                                            !dictSignatures[env.block.id].ContainsKey(
                                                ((ClassPropertyGetter)member).setter))
                                        {
                                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                "访问器未找到"));
                                            return;
                                        }

                                        signature = dictSignatures[env.block.id][
                                            ((ClassPropertyGetter)member).setter];
                                    }
                                    else
                                    {
                                        IMember member = MemberFinder.find(as3function.Name, env, as3function.Access.IsStatic, this, as3function.token);

                                        if (
                                            !dictSignatures.ContainsKey(env.block.id)
                                            ||
                                            !dictSignatures[env.block.id].ContainsKey(
                                                (member)))
                                        {
                                            pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile,
                                                "成员未找到"));
                                            return;
                                        }

                                        signature = dictSignatures[env.block.id][member];
                                    }
                                }

                                builds.AS3FunctionBuilder builder = new builds.AS3FunctionBuilder();
                                var func = builder.buildAS3Function(env,
                                as3function, this, signature);

                                if (as3function.IsMethod)
                                {


                                    ASBinCode.IMember member = MemberFinder.find(as3function.Name, env, as3function.Access.IsStatic, this, as3function.token);

                                    if (as3function.IsGet)
                                    {
                                        ((ClassPropertyGetter)member).getter.setFunctionId(func.functionId);
                                    }
                                    else if (as3function.IsSet)
                                    {
                                        ((ClassPropertyGetter)member).setter.setFunctionId(func.functionId);
                                    }
                                    else
                                    {
                                        ((MethodGetterBase)member).setFunctionId(func.functionId);
                                    }

                                }

                            }
                        }
                    }

                }
                else if (stmt is ASTool.AS3.AS3Break)
                {
                    ASTool.AS3.AS3Break as3Break = (ASTool.AS3.AS3Break)stmt;
                    builds.AS3BreakBuilder builder = new builds.AS3BreakBuilder();
                    builder.buildAS3Break(env, as3Break);
                }
                else if (stmt is ASTool.AS3.AS3YieldBreak)
                {
                    ASTool.AS3.AS3YieldBreak  as3yieldBreak = (ASTool.AS3.AS3YieldBreak)stmt;
                    builds.AS3BreakBuilder builder = new builds.AS3BreakBuilder();
                    builder.buildAS3YieldBreak(env, as3yieldBreak,this);
                }
                else if (stmt is ASTool.AS3.AS3Return)
                {
                    ASTool.AS3.AS3Return as3return = (ASTool.AS3.AS3Return)stmt;
                    builds.AS3FunctionBuilder builder = new builds.AS3FunctionBuilder();
                    builder.buildAS3Return(env, as3return, this);
                }
                else if (stmt is ASTool.AS3.AS3YieldReturn)
                {
                    ASTool.AS3.AS3YieldReturn as3yieldreturn = (ASTool.AS3.AS3YieldReturn)stmt;
                    builds.AS3FunctionBuilder builder = new builds.AS3FunctionBuilder();
                    builder.buildAS3YieldReturn(env, as3yieldreturn, this);
                }
                else if (stmt is ASTool.AS3.AS3Continue)
                {
                    builds.AS3ContinueBuilder builder = new builds.AS3ContinueBuilder();
                    builder.buildAS3Continue(env, (ASTool.AS3.AS3Continue)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3IF)
                {
                    ASTool.AS3.AS3IF as3if = (ASTool.AS3.AS3IF)stmt;
                    builds.AS3IFBuilder builder = new builds.AS3IFBuilder();
                    builder.buildAS3IF(env, as3if, this);
                }
                else if (stmt is ASTool.AS3.AS3Switch)
                {
                    builds.AS3SwitchBuilder builder = new builds.AS3SwitchBuilder();
                    builder.buildAS3Switch(env, (ASTool.AS3.AS3Switch)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3For)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3For(env, (ASTool.AS3.AS3For)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3ForIn)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3ForIn(env, (ASTool.AS3.AS3ForIn)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3ForEach)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3ForEach(env, (ASTool.AS3.AS3ForEach)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3While)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3While(env, (ASTool.AS3.AS3While)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3DoWhile)
                {
                    builds.AS3LoopBuilder builder = new builds.AS3LoopBuilder();
                    builder.buildAS3DoWhile(env, (ASTool.AS3.AS3DoWhile)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3Throw)
                {
                    builds.AS3ThrowBuilder builder = new builds.AS3ThrowBuilder();
                    builder.buildAS3Throw(env, (ASTool.AS3.AS3Throw)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3Try)
                {
                    builds.AS3TryBuilder builder = new builds.AS3TryBuilder();
                    builder.buildAS3Try(env, (ASTool.AS3.AS3Try)stmt, this);
                }
                else if (stmt is ASTool.AS3.AS3StmtExpressions)
                {
                    ASTool.AS3.AS3StmtExpressions expressions = (ASTool.AS3.AS3StmtExpressions)stmt;

                    for (int i = 0; i < expressions.as3exprlist.Count; i++)
                    {
                        buildExpression(env, expressions.as3exprlist[i]);
                    }


                }
                else if (stmt is ASTool.AS3.AS3Const)
                {
                    ASTool.AS3.AS3Const variable = (ASTool.AS3.AS3Const)stmt;
                    {
                        ASBinCode.RightValueBase defaultv = null;
                        try
                        {
                            VariableBase rtVariable = (VariableBase)MemberFinder.find(variable.Name, env, variable.Access.IsStatic, this, variable.token);
                            if (variable.ValueExpr != null) //变量值表达式
                            {
                                var testEval = ExpressionEval.Eval(variable.ValueExpr);
                                if (testEval != null && variable.ValueExpr.Value.IsReg)
                                {
                                    defaultv = new ASBinCode.rtData.RightValue(testEval);
                                }
                                else
                                {
                                    buildExpression(env, variable.ValueExpr);
                                    defaultv = builds.ExpressionBuilder.getRightValue(env, variable.ValueExpr.Value,
                                        stmt.Token, this
                                        );
                                }

                                //**加入赋值操作***
                                if (!ASRuntime.TypeConverter.testImplicitConvert(defaultv.valueType, rtVariable.valueType, this))
                                {
                                    throw new BuildException(new BuildTypeError(stmt.Token.line,
                                        stmt.Token.ptr, stmt.Token.sourceFile,
                                        defaultv.valueType, rtVariable.valueType, this));
                                }

                                bool isbindscope = false;
                                if (defaultv.valueType == RunTimeDataType.rt_function)
                                {
                                    isbindscope = true;
                                }

                                if (defaultv.valueType != rtVariable.valueType)
                                {
                                    //插入类型转换代码
                                    defaultv =
                                        builds.ExpressionBuilder.addCastOpStep(
                                            env, defaultv, rtVariable.valueType,
                                            new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile), this
                                            );

                                }

                                //赋初始值
                                {
                                    OpStep step = new OpStep(OpCode.assigning, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                                    step.reg = rtVariable;
                                    step.regType = rtVariable.valueType;
                                    step.arg1 = defaultv;
                                    step.arg1Type = defaultv.valueType;

                                    step.arg2 = null;
                                    step.arg2Type = RunTimeDataType.unknown;

                                    env.block.opSteps.Add(step);
                                }
                                if (isbindscope && (rtVariable.valueType == RunTimeDataType.rt_function || rtVariable.valueType == RunTimeDataType.rt_void))
                                {
                                    //***需追加绑定scope***
                                    OpStep stepbind = new OpStep(OpCode.bind_scope, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                                    stepbind.reg = null;
                                    stepbind.regType = RunTimeDataType.unknown;
                                    stepbind.arg1 = rtVariable;
                                    stepbind.arg1Type = rtVariable.valueType;
                                    stepbind.arg2 = null;
                                    stepbind.arg2Type = RunTimeDataType.unknown;

                                    env.block.opSteps.Add(stepbind);
                                }
                            }

                        }
                        catch (BuildException ex)
                        {
                            pushBuildError(ex.error);
                        }

                    }
                }
                else if (stmt is ASTool.AS3.AS3Variable)
                {
                    ASTool.AS3.AS3Variable variable = (ASTool.AS3.AS3Variable)stmt;
                    {

                        ASBinCode.RightValueBase defaultv = null;

                        try
                        {
                            VariableBase rtVariable = (VariableBase)MemberFinder.find(variable.Name, env, variable.Access.IsStatic, this, variable.token);
                            //rtVariable.valueType = TypeReader.fromSourceCodeStr(variable.TypeStr, env, stmt.Token);

                            if (variable.ValueExpr != null) //变量值表达式
                            {
                                var testEval = ExpressionEval.Eval(variable.ValueExpr);
                                if (testEval != null && variable.ValueExpr.Value.IsReg)
                                {
                                    defaultv = new ASBinCode.rtData.RightValue(testEval);
                                }
                                else
                                {
                                    buildExpression(env, variable.ValueExpr);
                                    defaultv = builds.ExpressionBuilder.getRightValue(env, variable.ValueExpr.Value,
                                        stmt.Token, this
                                        );
                                }

                                //**加入赋值操作***
                                //**隐式类型转换检查
                                if (rtVariable.ignoreImplicitCast) // catch异常的变量 原行为中似乎忽略编译时类型检查
                                {
                                    //而且只在此处场合忽略检查
                                }
                                else if (!ASRuntime.TypeConverter.testImplicitConvert(defaultv.valueType, rtVariable.valueType, this))
                                {
                                    throw new BuildException(new BuildTypeError(stmt.Token.line,
                                        stmt.Token.ptr, stmt.Token.sourceFile,
                                        defaultv.valueType, rtVariable.valueType, this));
                                }

                                bool isbindscope = false;
                                if (defaultv.valueType == RunTimeDataType.rt_function)
                                {
                                    isbindscope = true;
                                }

                                if (defaultv.valueType != rtVariable.valueType)
                                {
                                    //插入类型转换代码
                                    defaultv =
                                        builds.ExpressionBuilder.addCastOpStep(
                                            env, defaultv, rtVariable.valueType,
                                            new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile), this
                                            );

                                }

                                //赋初始值
                                {
                                    OpStep step = new OpStep(OpCode.assigning, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                                    step.reg = rtVariable;
                                    step.regType = rtVariable.valueType;
                                    step.arg1 = defaultv;
                                    step.arg1Type = defaultv.valueType;


                                    step.arg2 = null;
                                    step.arg2Type = RunTimeDataType.unknown;


                                    env.block.opSteps.Add(step);
                                }
                                if (isbindscope && (rtVariable.valueType == RunTimeDataType.rt_function || rtVariable.valueType == RunTimeDataType.rt_void))
                                {
                                    //***需追加绑定scope***
                                    OpStep stepbind = new OpStep(OpCode.bind_scope, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                                    stepbind.reg = null;
                                    stepbind.regType = RunTimeDataType.unknown;
                                    stepbind.arg1 = rtVariable;
                                    stepbind.arg1Type = rtVariable.valueType;
                                    stepbind.arg2 = new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtBoolean.True);
                                    stepbind.arg2Type = RunTimeDataType.rt_boolean;

                                    env.block.opSteps.Add(stepbind);
                                }
                            }
                            //else
                            //{
                            //    //赋默认值  默认值在运行是自动初始化，所以此处省略
                            //    defaultv = ASRuntime.TypeConverter.getDefaultValue(rtVariable.valueType);
                            //    {
                            //        OpStep step = new OpStep(OpCode.assigning, new SourceToken(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile));
                            //        step.reg = rtVariable;
                            //        step.regType = rtVariable.valueType;
                            //        step.arg1 = defaultv;
                            //        step.arg1Type = defaultv.valueType;
                            //        step.arg2 = null;
                            //        step.arg2Type = RunTimeDataType.unknown;

                            //        env.block.opSteps.Add(step);
                            //    }

                            //}

                        }
                        catch (BuildException ex)
                        {
                            pushBuildError(ex.error);
                        }

                    }

                }
                else
                {
                    pushBuildError(new BuildError(stmt.Token.line, stmt.Token.ptr, stmt.Token.sourceFile, stmt.GetType().Name + "未实现"));

                }
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }

        }


        internal void buildExpression(CompileEnv env, ASTool.AS3.AS3Expression expression)
        {
            ASTool.AS3.AS3Expression testexpr = new ASTool.AS3.AS3Expression(expression.token);
            testexpr.exprStepList = new ASTool.AS3.Expr.AS3ExprStepList();
            for (int i = 0; i < expression.exprStepList.Count; i++)
            {
                if (expression.exprStepList[i].Type != ASTool.AS3.Expr.OpType.Assigning)
                {
                    testexpr.exprStepList.Add(expression.exprStepList[i]);
                }
                else
                {
                    testexpr.Value = expression.exprStepList[i].Arg2;
                    break;
                }
            }

            if (testexpr.Value == null)
            {
                testexpr.Value = expression.Value;
            }

            RunTimeValueBase value = ExpressionEval.Eval(testexpr);

            if (value != null && testexpr.Value.IsReg)
            {
                RightValueBase rv = new ASBinCode.rtData.RightValue(value);

                ASBinCode.Register eax = env.createASTRegister(testexpr.Value.Reg.ID);
                eax.setEAXTypeWhenCompile(rv.valueType);

                OpStep op = new OpStep(OpCode.assigning, new SourceToken(testexpr.token.line, testexpr.token.ptr, testexpr.token.sourceFile));
                op.reg = eax;
                op.regType = eax.valueType;
                op.arg1 = rv;
                op.arg1Type = rv.valueType;
                op.arg2 = null;
                op.arg2Type = RunTimeDataType.unknown;

                env.block.opSteps.Add(op);

                int i = testexpr.exprStepList.Count;
                testexpr.exprStepList.Clear();
                for (int j = i; j < expression.exprStepList.Count; j++)
                {
                    testexpr.exprStepList.Add(expression.exprStepList[j]);
                }
                testexpr.Value = expression.Value;
                buildExpressNotEval(env, testexpr);
            }
            else
            {
                buildExpressNotEval(env, expression);
            }
        }

        internal void buildExpressNotEval(CompileEnv env, ASTool.AS3.AS3Expression expression)
        {
            try
            {
                builds.ExpressionBuilder expressionbuilder =
                            new builds.ExpressionBuilder(this);

                expressionbuilder.buildAS3Expression(env, expression);
            }
            catch (BuildException ex)
            {
                pushBuildError(ex.error);
            }
        }




        
        
        internal string build_vector(string vt, ASTool.Token token,out ASBinCode.rtti.Class vc)
        {
            string vectorType = vt.Substring(8, vt.Length - 9);
            if (vectorType.StartsWith("Vector.<"))
            {
                ASBinCode.rtti.Class tvc;
                build_vector(vectorType,token,out tvc);
            }

            if (vectorType == "null" 
                || vectorType == "undefined"
                || vectorType == "void")
            {
                throw new BuildException(token.line, token.ptr, token.sourceFile,
                    "Syntax error: '" + vectorType + "' is not allowed here");
            }

            var vector_t = TypeReader.fromSourceCodeStr(vectorType, token, this);

            string fulltypename = vector_t.toAS3Name();
            string ot = fulltypename;

            ASBinCode.rtti.Class _Vector_innerClass = null;

            if (vector_t > RunTimeDataType.unknown)
            {
                var rt_class =getClassByRunTimeDataType(vector_t);

                fulltypename = rt_class.package + (String.IsNullOrEmpty( rt_class.package)?String.Empty:".")  +rt_class.name;
                ot = fulltypename;

                if (bin.dict_Vector_type.ContainsKey(rt_class))
                {
                    _Vector_innerClass = rt_class;
                    
                    string temp1 = "C1";

                    while (TypeReader.findClassFromImports(temp1, this, token).Count > 0)
                    {
                        temp1 = temp1 + "1";
                    }

                    ot = temp1;
                }
                
            }

            string finalVectorName = "Vector.<" + fulltypename + ">";

            foreach (var item in buildingclasses)
            {
                if (item.Value.name == finalVectorName)
                {
                    vc = item.Value;
                    return item.Value.name;
                }
            }

            //获取临时类名
            string TC = finalVectorName.Replace(".","").Replace("<","_").Replace(">","_").Replace("@","");
            while (TypeReader.findClassFromImports(TC, this, token).Count > 0)
            {
                TC = TC + "_";
            }
            
            
            string template= Properties.Resources.Vector;
            template= template.Replace("Vector.<&T>", TC);

            
            template = template.Replace("&T", ot);

            string imports = string.Empty;

            if (vector_t > RunTimeDataType.unknown && ot.IndexOf(".")>0)
            {
                imports = "import " + ot;
            }

            template = template.Replace("[imports]", imports);

            if (ot == "*")
            {
                template = template.Replace("private const t = *;", "private const t = undefined;");
            }


            var lib = new ASTool.AS3.AS3Proj();
            var grammar = Grammar.getGrammar();

            List<compiler.utils.Tuple<ASTool.GrammerTree, string>> trees = new List<compiler.utils.Tuple<ASTool.GrammerTree, string>>();

            {
                //***类库源码***
                var tree = grammar.ParseTree(template, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, TC+".as3");

                if (grammar.hasError)
                {
                    throw new BuildException(token.line, token.ptr,token.sourceFile, "Vector.<"+ vt +">编译失败");
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, TC+".as3"));
            }

            foreach (var tree in trees)
            {
                var analyser = new ASTool.AS3FileGrammarAnalyser(lib, tree.item2);
                if (!analyser.Analyse(grammar, tree.item1)) //生成项目的语法树
                {
                    throw new BuildException(token.line, token.ptr, token.sourceFile, "Vector.<" + vt + ">编译失败");
                }
            }


            

            //lib.SrcFiles[0].Package.MainClass.Name = finalVectorName;
            



            //***执行编译。***
            List<ASTool.AS3.AS3SrcFile> listsrcfiles = new List<ASTool.AS3.AS3SrcFile>();
            listsrcfiles.Add(lib.SrcFiles[0]);

            if (_Vector_innerClass != null)
            {
                _Vector_innerClass.name = ot;
            }
            build_srcFiles(listsrcfiles,true,vector_t);

            if (buildingclasses.ContainsKey(lib.SrcFiles[0].Package.MainClass))
            {
                lib.SrcFiles[0].Package.MainClass.Name = finalVectorName;
                buildingclasses[lib.SrcFiles[0].Package.MainClass].name = finalVectorName;
            }
            

            if (_Vector_innerClass != null)
            {
                _Vector_innerClass.name = fulltypename;
            }

            if (_currentImports.Count > 0)
            {
                if (buildingclasses.ContainsKey(lib.SrcFiles[0].Package.MainClass))
                {
                    _currentImports.Peek().Add(buildingclasses[lib.SrcFiles[0].Package.MainClass]);
                }
            }

            vc = buildingclasses[lib.SrcFiles[0].Package.MainClass];

            return lib.SrcFiles[0].Package.MainClass.Name;
        }



    }
}
