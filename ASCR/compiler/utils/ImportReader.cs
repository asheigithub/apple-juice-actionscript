using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.utils
{
    class ImportReader
    {
        private static void fillimports(List<ASBinCode.rtti.Class> imports, 
            List<ASTool.AS3.AS3Import> imps , Builder builder,ASTool.AS3.AS3SrcFile srcfile)
        {
            string thispackage = srcfile.Package.Name;
            //导入相同包下所有主类
            foreach (var item in builder.buildingclasses)
            {
                if (item.Value.package.Equals(thispackage, StringComparison.Ordinal)
                    &&
                    item.Value.mainClass==null
                    )
                {
                    imports.Add(item.Value);
                }
            }
            //导入顶级包的所有主类
            foreach (var item in builder.buildingclasses)
            {
                if ( string.IsNullOrEmpty(item.Value.package) && item.Value.mainClass ==null)
                {
                    if (!imports.Contains(item.Value))
                    {
                        imports.Add(item.Value);
                    }
                }
            }

            //***导入当前类的包外类***
            {
                var _class = builder.buildingclasses[srcfile.Package.MainClassOrInterface];
                foreach (var item in builder.buildingclasses)
                {
                    if (item.Value.mainClass == _class)
                    {
                        if (!imports.Contains(item.Value))
                        {
                            imports.Add(item.Value);
                        }
                    }
                }

                //***如果当前在编译Vector
                if (builder.bin.dict_Vector_type.ContainsKey(_class))
                {
                    var c = builder.bin.dict_Vector_type[_class];
                    if (c > ASBinCode.RunTimeDataType.unknown)
                    {
                        var oc= builder.getClassByRunTimeDataType(c);
                        if (!imports.Contains(oc))
                        {
                            imports.Add(oc);
                        }

                    }
                }

            }
            for (int i = 0; i < imps.Count; i++)
            {
                var imp = imps[i];
                if (imp.Name.EndsWith(".*"))
                {
                    //导入包内所有主类
                    string packagename = imp.Name.Substring(0, imp.Name.Length - 2);
                    foreach (var item in builder.buildingclasses)
                    {
                        if (packagename.Equals(item.Value.package, StringComparison.Ordinal))
                        {
                            if (!imports.Contains(item.Value) && item.Value.mainClass==null)
                            {
                                imports.Add(item.Value);
                            }
                        }
                    }
                }
                else
                {
                    bool found = false;
                    //导入主类
                    foreach (var item in builder.buildingclasses)
                    {
                        if (imp.Name.Equals(item.Value.package + "." + item.Value.name, StringComparison.Ordinal)
                            &&
                            item.Value.mainClass==null
                            )
                        {
                            found = true;
                            if (!imports.Contains(item.Value))
                            {
                                imports.Add(item.Value);
                            }
                            break;
                        }
                    }

                    if (!found)
                    {
                        if (builder.options.isConsoleOut)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Definition " + imp.Name + " could not be found.");
                            Console.ResetColor();
                        }

                        //throw new BuildException(imp.)
                    }
                }
            }
        }


        /// <summary>
        /// 读导入
        /// </summary>
        /// <param name="srcfile"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static List<ASBinCode.rtti.Class> readImports(ASTool.AS3.AS3SrcFile srcfile,Builder builder)
        {
            List<ASBinCode.rtti.Class> imports = new List<ASBinCode.rtti.Class>();
            fillimports(imports, srcfile.Package.Import,builder,srcfile);
            return imports;
        }

        /// <summary>
        /// 读包外导入
        /// </summary>
        /// <param name="srcfile"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static List<ASBinCode.rtti.Class> readOutPackageImports(ASTool.AS3.AS3SrcFile srcfile, Builder builder)
        {
            List<ASBinCode.rtti.Class> imports = new List<ASBinCode.rtti.Class>();

            fillimports(imports, srcfile.OutPackageImports, builder,srcfile);

            //**加入主类***

            if (!imports.Contains(builder.buildingclasses[srcfile.Package.MainClassOrInterface]))
            {
                imports.Add(builder.buildingclasses[srcfile.Package.MainClassOrInterface]);
            }


            return imports;
        }
    }
}
