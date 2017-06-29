using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class InterfaceCreator : CreatorBase
	{
		public static List<Type> GetExtendInterfaces(Type interfacetype)
		{
			List<Type> extinterfaces = new List<Type>(interfacetype.GetInterfaces());

			bool f = true;

		research:


			while (f)
			{
				f = false;

				for (int i = 0; i < extinterfaces.Count; i++)
				{
					var it = extinterfaces[i];

					List<Type> extList = new List<Type>(it.GetInterfaces());

					for (int j = 0; j < extinterfaces.Count; j++)
					{
						if (i != j)
						{
							if (extList.Contains(extinterfaces[j]))
							{
								extinterfaces.RemoveAt(j);

								f = true;
								goto research;
							}
						}
					}


				}

			}

			return extinterfaces;
		}

		private Type super;

		
		public InterfaceCreator(Type interfacetype, string as3apidocpath, string csharpnativecodepath,
			Dictionary<Type,CreatorBase> typeCreators 
			):base(interfacetype,as3apidocpath,csharpnativecodepath)
        {
			if (!interfacetype.IsInterface)
			{
				throw new ArgumentException("类型不是接口");
			}

			if (interfacetype.IsGenericType)
			{
				throw new ArgumentException("不支持泛型接口");
			}

			
			name = GetAS3ClassOrInterfaceName(interfacetype);

			var exts = GetExtendInterfaces(interfacetype);

			foreach (var item in exts)
			{
				if (!typeCreators.ContainsKey(item))
				{
					typeCreators.Add(item, new InterfaceCreator(item, as3apidocpath, csharpnativecodepath, typeCreators));
				}
			}

			if (exts.Count > 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(interfacetype+"检查到接口多继承，这里使用第一个");
				Console.ResetColor();
				super = exts[0];
			}
			else if (exts.Count == 1)
			{
				super = exts[0];
				
			}

			//****分析引用到的类型****
			var methods = interfacetype.GetMethods();
			foreach (var item in methods)
			{
				var rt = MethodNativeCodeCreator.GetAS3Runtimetype(item.ReturnType);
				if (rt > ASBinCode.RunTimeDataType.unknown)
				{
					if (item.ReturnType.IsInterface) //***Class代码还没做****
					{
						if (!typeCreators.ContainsKey(item.ReturnType))
						{
							typeCreators.Add(item.ReturnType, new InterfaceCreator(item.ReturnType, as3apidocpath, csharpnativecodepath, typeCreators));
						}
					}
				}

				var paras = item.GetParameters();
				foreach (var p in paras)
				{
					var pt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
					if (pt > ASBinCode.RunTimeDataType.unknown)
					{
						if (p.ParameterType.IsInterface) //***Class代码还没做****
						{
							if (!typeCreators.ContainsKey(p.ParameterType))
							{
								typeCreators.Add(p.ParameterType, new InterfaceCreator(p.ParameterType, as3apidocpath, csharpnativecodepath, typeCreators));
							}
						}
					}
				}

			}


		}

		private void GenAS3FileHead(StringBuilder as3sb)
		{
			as3sb.AppendLine("package " + GetPackageName(type));
			as3sb.AppendLine("{");
		}

		private void EndAS3File(StringBuilder as3sb)
		{
			as3sb.AppendLine("}");
		}


		private string GetAS3TypeString(Type type,Dictionary<Type,string> typeimports )
		{
			ASBinCode.RunTimeDataType rttype = MethodNativeCodeCreator.GetAS3Runtimetype(type);
			if (rttype == ASBinCode.RunTimeDataType.fun_void)
			{
				return "void";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_number)
			{
				return "Number";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_int)
			{
				return "int";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_uint)
			{
				return "uint";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_boolean)
			{
				return "Boolean";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_string)
			{
				return "String";
			}


			if (this.type.Namespace != type.Namespace)
			{
				if (!typeimports.ContainsKey(type))
				{
					typeimports.Add(type, "import " + type.Namespace.ToLower() + "." + GetAS3ClassOrInterfaceName(type) + ";");
				}
			}

			return GetAS3ClassOrInterfaceName(type);
		}

		private string GetMethodName(string dotName,System.Reflection.MethodInfo method)
		{
			var methods = type.GetMethods();

			string ext = string.Empty;

			for (int i = 0; i < methods.Length; i++)
			{
				if (methods[i].Name == dotName)
				{
					if (methods[i].Equals(method))
					{
						break;
					}
					else
					{
						ext = ext + "_";
					}
				}
			}



			return dotName.Substring(0, 1).ToLower() + dotName.Substring(1) + ext;

			

		}


		private void GenNativeFuncImport(StringBuilder nativesb)
		{
			nativesb.AppendLine("using ASBinCode;");
			nativesb.AppendLine("using ASBinCode.rtti;");
			nativesb.AppendLine("using ASRuntime;");
			nativesb.AppendLine("using ASRuntime.nativefuncs;");
			nativesb.AppendLine("using System;");
			nativesb.AppendLine("using System.Collections;");
			nativesb.AppendLine("using System.Collections.Generic;");
		}

		private void GenNativeFuncNameSpaceAndClass(StringBuilder nativesb)
		{
			nativesb.AppendLine("namespace ASCTest.regNativeFunctions");
			nativesb.AppendLine("{");
			nativesb.Append("\t");
			nativesb.Append("class ");
			nativesb.AppendLine(GetNativeFunctionClassName(type));
			nativesb.Append("\t");
			nativesb.AppendLine("{");
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
		private void EndNativeFuncClass(StringBuilder nativesb)
		{
			nativesb.Append("\t");
			nativesb.AppendLine("}");
			nativesb.AppendLine("}");
		}

		public override void Create()
		{
			Dictionary<Type, string> typeimports = new Dictionary<Type, string>();

			StringBuilder nativefunc = new StringBuilder();
			GenNativeFuncImport(nativefunc);
			GenNativeFuncNameSpaceAndClass(nativefunc);

			BeginRegFunction(nativefunc);

			List<string> regfunctions = new List<string>();
			List<string> nativefuncClasses = new List<string>();





			StringBuilder as3api = new StringBuilder();
			GenAS3FileHead(as3api);

			as3api.AppendLine("@imports");
			as3api.AppendLine();

			as3api.AppendFormat("\t[link_system_interface({0})]",GetCreatorNativeFuncName(type));
			as3api.AppendLine();
			as3api.AppendFormat("\tpublic interface {0}",name);


			regfunctions.Add(
				"\t\t\tbin.regNativeFunction(LinkSystem_Buildin.getCreator(\""+ GetCreatorNativeFuncName(type) + "\", default("+type.FullName+")));");


			if (super != null)
			{
				as3api.AppendFormat(" extends {0}",GetAS3TypeString(super,typeimports));
			}

			as3api.AppendLine();
			as3api.AppendLine("\t{");

			foreach (var method in type.GetMethods())
			{
				if (method.IsGenericMethod)
				{
					Console.WriteLine("跳过泛型方法"+method.ToString());

					continue;
				}

				string returntype = GetAS3TypeString(method.ReturnType,typeimports);

				var paras = method.GetParameters();

				foreach (var para in paras)
				{
					if (para.IsOut)
					{
						Console.WriteLine(method.ToString()+"参数"+ para.Position+" " +para+ "为out,跳过");
						continue;
					}
					if (para.ParameterType.IsByRef)
					{
						Console.WriteLine(method.ToString() + "参数" + para.Position + " " + para + "为byref,跳过");
						continue;
					}
				}


				string nativefunName;

				System.Reflection.PropertyInfo pinfo;
				if (MethodNativeCodeCreator.CheckIsIndexerGetter(method, type, out pinfo))
				{
					//****索引器****
					as3api.Append("\t\t");
					as3api.AppendLine("[get_this_item];");

					nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), "getThisItem");

					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}];", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function getThisItem");
				}
				else if (MethodNativeCodeCreator.CheckIsGetter(method, type, out pinfo))
				{
					nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), GetMethodName(method.Name, method));


					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}]", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function get ");
					as3api.Append(GetMethodName(pinfo.Name, method));
				}
				else if (MethodNativeCodeCreator.CheckIsIndexerSetter(method, type, out pinfo))
				{
					//****索引器****
					as3api.Append("\t\t");
					as3api.AppendLine("[set_this_item];");

					nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), "setThisItem");


					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}];", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function setThisItem");
				}
				else if (MethodNativeCodeCreator.CheckIsSetter(method,type,out pinfo))
				{
					nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), GetMethodName(method.Name, method));


					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}]", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function set ");
					as3api.Append(GetMethodName(pinfo.Name, method));
				}
				else
				{

					nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), GetMethodName(method.Name, method));

					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}]",nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function ");
					as3api.Append(GetMethodName(method.Name,method));
				}

				as3api.Append("(");

				for (int i = 0; i < paras.Length; i++)
				{
					var para = paras[i];
					as3api.Append(para.Name);
					as3api.Append(":");
					as3api.Append(GetAS3TypeString(para.ParameterType, typeimports));

					if (para.IsOptional)
					{
						as3api.Append("=");

						if (para.RawDefaultValue != null)
						{
							as3api.Append(para.RawDefaultValue.ToString());
						}
						else
						{
							as3api.Append("null");
						}
					}

					if (i < paras.Length - 1)
					{
						as3api.Append(",");
					}

				}

				as3api.Append(")");
				as3api.Append(":");
				as3api.Append(returntype);
				as3api.AppendLine(";");
				as3api.AppendLine();

				//***编写方法的本地代码***
				regfunctions.Add( string.Format( "\t\t\tbin.regNativeFunction(new {0}());", nativefunName ));

				MethodNativeCodeCreator mc = new MethodNativeCodeCreator(nativefunName, method,type);
				nativefuncClasses.Add(mc.GetCode());
			}

			as3api.AppendLine("\t}");

			EndAS3File(as3api);

			string imports = string.Empty;
			foreach (var item in typeimports.Values)
			{
				imports += "\t" + item + "\n";
			}
			as3api.Replace("@imports", imports);


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
			


			Console.WriteLine(as3api.ToString());
			Console.WriteLine(nativefunc.ToString());

			string as3file = "as3api/" + GetPackageName(type).Replace(".", "/") + "/" + name + ".as";
			string nativefunfile = "buildins/" + GetNativeFunctionClassName(type) + ".cs";

			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(as3file));
			System.IO.File.WriteAllText(as3file, as3api.ToString());

			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(nativefunfile));
			System.IO.File.WriteAllText(nativefunfile, nativefunc.ToString());



		}

	}
}
