using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
    /// <summary>
    /// 创建枚举的代码
    /// </summary>
    class EnumCreator : CreatorBase
    {
        

        

        public EnumCreator(Type enumtype,string as3apidocpath,string csharpnativecodepath,string linkcodenamespace):base(enumtype,as3apidocpath,csharpnativecodepath,linkcodenamespace)
        {
            if (!enumtype.IsEnum)
            {
                throw new ArgumentException("类型不是枚举");
            }

            isfinal = true;
            isStruct = false;

            this.name = GetAS3ClassOrInterfaceName(enumtype);
		}



		private void GenClassDefine(StringBuilder as3sb)
        {
            as3sb.Append("\t");

            as3sb.Append("public ");
            if (isfinal)
            {
                as3sb.Append("final ");
            }
            as3sb.Append("class ");
            as3sb.Append(name);
            as3sb.AppendLine();
            as3sb.Append("\t");
            as3sb.AppendLine("{");

        }


		private void EndClassDefine(StringBuilder as3sb)
        {
            as3sb.Append("\t");
            as3sb.AppendLine("}");
        }

        private string GetEnumItemNativeFuncName(System.Reflection.FieldInfo fieldinfo)
        {
            //return type.Namespace.ToLower().Replace(".", "_") + "_" +  type.Name + "_" + fieldinfo.Name + "_getter";
			return GetNativeFunctionPart1(type) + "_" + fieldinfo.Name + "_getter";
		}

        private string GetEnumBitOrFuncName()
        {
			//return type.Namespace.ToLower().Replace(".", "_") + "_" + type.Name + "_operator_bitOr";
			return GetNativeFunctionPart1(type) + "_"  +"operator_bitOr";
		}


        private void GenNativeFuncNameSpaceAndClass(StringBuilder nativesb)
        {
            nativesb.AppendLine("namespace " + linkcodenamespace);
            nativesb.AppendLine("{");
            nativesb.Append("\t");
            nativesb.Append("class ");
            nativesb.AppendLine(GetNativeFunctionClassName(type));
            nativesb.Append("\t");
            nativesb.AppendLine("{");
        }

        private void EndNativeFuncClass(StringBuilder nativesb)
        {
            nativesb.Append("\t");
            nativesb.AppendLine("}");
            nativesb.AppendLine("}");
        }

		private void BeginRegFunction(StringBuilder nativesb)
        {
            nativesb.Append("\t\t");
            nativesb.AppendLine("public static void regNativeFunctions(CSWC bin)");
            nativesb.Append("\t\t");
            nativesb.AppendLine("{");
        }

		private void EndRegFunction(StringBuilder nativesb)
        {
            nativesb.Append("\t\t");
            nativesb.AppendLine("}");
            nativesb.AppendLine();

        }


        private string GetCtorClassString()
        {
            return Properties.Resources.EnumCtor.Replace("{0}",GetCtorNativeFuncName(type));
        }


        public override string Create()
        {
            StringBuilder nativefunc = new StringBuilder();
            //GenNativeFuncImport(nativefunc);
            GenNativeFuncNameSpaceAndClass(nativefunc);

            BeginRegFunction(nativefunc);


            List<string> regfunctions = new List<string>();
            List<string> nativefuncClasses = new List<string>();


            StringBuilder as3api = new StringBuilder();
            GenAS3FileHead(as3api);

            as3api.Append('\t');
            as3api.Append("[no_constructor]");
            as3api.AppendLine();

            as3api.Append('\t');
            as3api.Append("[link_system]");
            as3api.AppendLine();

            GenClassDefine(as3api);

            //****枚举的creator***
            as3api.Append("\t\t");
            as3api.Append("[creator];");
            as3api.AppendLine();
            as3api.Append("\t\t");
            as3api.AppendFormat("[native,{0}];",GetCreatorNativeFuncName(type));
            as3api.AppendLine();
            as3api.Append("\t\t");
            as3api.Append("private static function _creator(type:Class):*;");
            as3api.AppendLine();
            as3api.AppendLine();

            regfunctions.Add(
                string.Format("\t\t\tbin.regNativeFunction(LinkSystem_Buildin.getCreator(\"{0}\", default({1})));"
                ,GetCreatorNativeFuncName(type)
                , NativeCodeCreatorBase.GetTypeFullName(type)
				)
                );

            //***枚举的构造函数***
            as3api.Append("\t\t");
            as3api.AppendFormat("[native,{0}];", GetCtorNativeFuncName(type));
            as3api.AppendLine();
            as3api.Append("\t\t");
            as3api.AppendFormat("public function {0}();",name);
            as3api.AppendLine();
            as3api.AppendLine();

			//regfunctions.Add(
			//    string.Format("\t\t\tbin.regNativeFunction(new {0}());"
			//    ,GetCtorNativeFuncName(type)
			//    )
			//    );
			regfunctions.Add(
				string.Format("\t\t\tbin.regNativeFunction(\"{0}\",\"{1}\");"
				, LinkCodeNampScapePart+ GetCtorNativeFuncName(type)
				, GetCtorNativeFuncName(type)
				)
				);
			nativefuncClasses.Add(GetCtorClassString());

            //****创建枚举成员***
            foreach (var item in type.GetFields( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
				if (IsObsolete(item,type))
					continue;


				string enumItemName = item.Name;
				if (enumItemName == "Class") { enumItemName = "Class_"; }
				if (enumItemName == "Object") { enumItemName = "Object_"; }

				//object v = item.GetValue(null);
                as3api.Append("\t\t");
                as3api.AppendLine("/**");
                as3api.AppendLine("\t\t *" + enumItemName+" = "+item.ToString() );
                as3api.AppendLine("\t\t */");

                as3api.Append("\t\t");
                as3api.AppendFormat("[native,{0}];", GetEnumItemNativeFuncName(item) );
                as3api.AppendLine();
                as3api.Append("\t\t");
                as3api.AppendFormat("public static const {0}:{1};", enumItemName, name);
                as3api.AppendLine();
                as3api.AppendLine();

                
                string nf = "\t\t\tbin.regNativeFunction(";
                nf += "LinkSystem_Buildin.getStruct_static_field_getter(\"" +GetEnumItemNativeFuncName(item)+ "\"";
                nf += ",";
                nf += "()=>{ return "+ NativeCodeCreatorBase.GetTypeFullName(type) + "." + item.Name + ";}";
                nf += ")";
                nf += ");";

                regfunctions.Add(nf);

            }

            //***枚举的ValueOf***
            as3api.Append("\t\t");
            as3api.AppendFormat("[native, _system_Enum_valueOf];");
            as3api.AppendLine();
            as3api.Append("\t\t");
            as3api.AppendFormat("public function valueOf():int;");
            as3api.AppendLine();
            as3api.AppendLine();

            //***枚举的bitOr操作****
            as3api.Append("\t\t");
            as3api.Append("[operator,\"|\"];");
            as3api.AppendLine();
            as3api.Append("\t\t");
            as3api.Append("[native,"+ GetEnumBitOrFuncName() +"];");
            as3api.AppendLine();
            as3api.Append("\t\t");
            as3api.AppendFormat("private static function bitOr(t1:{0},t2:{0}):{0};",name);
            as3api.AppendLine();

            string bitorclass = Properties.Resources.EnumItemBitOr;
            bitorclass= bitorclass.Replace("{0}", GetEnumBitOrFuncName());
            bitorclass= bitorclass.Replace("{1}", NativeCodeCreatorBase.GetTypeFullName(type));
            nativefuncClasses.Add( bitorclass);

			//regfunctions.Add("\t\t\tbin.regNativeFunction(new "+ GetEnumBitOrFuncName() +"());");
			regfunctions.Add("\t\t\tbin.regNativeFunction(\"" + LinkCodeNampScapePart + GetEnumBitOrFuncName() + "\",\"" + GetEnumBitOrFuncName() + "\");");


			EndClassDefine(as3api);
            EndAS3File(as3api);


            for (int i = 0; i < regfunctions.Count; i++)
            {
                nativefunc.AppendLine(regfunctions[i]);
            }

            EndRegFunction(nativefunc);

            for (int i = 0; i < nativefuncClasses.Count; i++)
            {
                nativefunc.AppendLine(nativefuncClasses[i]);
            }

            EndNativeFuncClass(nativefunc);

            //Console.WriteLine(as3api.ToString());

            //Console.WriteLine(nativefunc.ToString());

            string as3file = "as3api/" + GetPackageName(type).Replace(".", "/") + "/" + name + ".as";
            string nativefunfile = "buildins/" + GetNativeFunctionClassName(type) + ".cs";

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(as3file));
            System.IO.File.WriteAllText(as3file, as3api.ToString());

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(nativefunfile));

			StringBuilder usingcode = new StringBuilder();
			GenNativeFuncImport(usingcode);

            System.IO.File.WriteAllText(nativefunfile,usingcode.ToString()+  nativefunc.ToString());

			return nativefunc.ToString();
        }



    }
}
