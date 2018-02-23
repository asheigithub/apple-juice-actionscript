using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class InterfaceCreator : CreatorBase
	{
		private static List<Type> GetExtendInterfaces(Type interfacetype)
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

					if (it.IsGenericType)
					{
						extinterfaces.RemoveAt(i);
						f = true;
						goto research;
					}

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
			Dictionary<Type,CreatorBase> typeCreators ,
			string linkcodenamespace
			):base(interfacetype,as3apidocpath,csharpnativecodepath,linkcodenamespace)
        {
			if (!interfacetype.IsInterface)
			{
				throw new ArgumentException("类型不是接口");
			}

			if (interfacetype.IsGenericType)
			{
				throw new ArgumentException("不支持泛型接口");
			}
			if (IsSkipCreator(interfacetype)
				)
			{
				throw new ArgumentException("接口已配置为跳过");
			}

			if (IsSkipType(interfacetype))
			{
				throw new ArgumentException("接口已配置为不生成");
			}

			name = GetAS3ClassOrInterfaceName(interfacetype);

			var exts = GetExtendInterfaces(interfacetype);

			foreach (var item in exts)
			{
				if (!typeCreators.ContainsKey(item) && !IsSkipCreator(item) && !IsSkipType(item))
				{
					typeCreators.Add(item, null); typeCreators[item]= new InterfaceCreator(item, as3apidocpath, csharpnativecodepath, typeCreators,linkcodenamespace);
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
				if (IsObsolete(item,type))
				{
					continue;
				}

				if (item.IsGenericMethod)
				{
					continue;
				}

				if (item.ReturnType.IsGenericTypeDefinition)
				{
					continue;
				}
				
				var rt = MethodNativeCodeCreator.GetAS3Runtimetype(item.ReturnType);
				if (rt > ASBinCode.RunTimeDataType.unknown )
				{
					if (IsSkipType(item.ReturnType))
					{
						continue;
					}

					MakeCreator(item.ReturnType,typeCreators);
				}

				bool parapass = true;
				var paras = item.GetParameters();
				foreach (var p in paras)
				{
					if (p.IsOut)
					{
						parapass = false;
						break;
					}
					if (p.ParameterType.IsByRef)
					{
						parapass = false;
						break;
					}

					if (p.ParameterType.IsGenericTypeDefinition)
					{
						parapass = false;
						break;
					}

					if (p.IsOptional)
					{
						if (p.RawDefaultValue != null)
						{
							var rrt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
							if (rrt > ASBinCode.RunTimeDataType.unknown)
							{
								parapass = false;
								break;
							}
						}
					}

					if (IsSkipType(p.ParameterType, true))
					{
						parapass = false;
						break;
					}

					var pt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
					if (pt > ASBinCode.RunTimeDataType.unknown )
					{
						var mt = p.ParameterType;
						if (p.ParameterType.IsByRef)
						{
							mt = p.ParameterType.GetElementType();
						}
						MakeCreator(mt, typeCreators);
					}

				}

				if (parapass)
				{
					methodlist.Add(item);
				}

			}


		}


		private List<System.Reflection.MethodInfo> methodlist = new List<System.Reflection.MethodInfo>();

		

		



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

		public static string GetMethodNativeFunctionName(System.Reflection.MethodInfo method,Type type,Dictionary<string,object> staticusenames,Dictionary<string,object> usenames)
		{
			string nativefunName;

			System.Reflection.PropertyInfo pinfo;
			if (MethodNativeCodeCreator.CheckIsIndexerGetter(method, type, out pinfo)

				&& method.GetParameters().Length == 1
				)
			{
				string testname = GetMethodName(method.Name, method, type, staticusenames, usenames);
				if (testname == method.Name)
				{
					testname = "getThisItem";
				}



				nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), testname );// GetMethodName( "getThisItem" ,method,type,staticusenames,usenames ));
				
			}
			else if (MethodNativeCodeCreator.CheckIsGetter(method, type, out pinfo))
			{
				nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), GetMethodName(method.Name, method, type,staticusenames,usenames));

			}
			else if (MethodNativeCodeCreator.CheckIsIndexerSetter(method, type, out pinfo)

				&& method.GetParameters().Length == 2
				)
			{
				//****索引器****
				string testname = GetMethodName(method.Name, method, type, staticusenames, usenames);
				if (testname == method.Name)
				{
					testname = "setThisItem";
				}

				nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), testname );////GetMethodName( "setThisItem",method,type,staticusenames,usenames));
				
			}
			else if (MethodNativeCodeCreator.CheckIsSetter(method, type, out pinfo))
			{
				nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), GetMethodName(method.Name, method, type, staticusenames, usenames));

			}
			else
			{

				nativefunName = string.Format("{0}_{1}", GetNativeFunctionPart1(type), GetMethodName(method.Name, method, type, staticusenames, usenames));
			}

			if (method.IsStatic)
			{
				nativefunName = "static_" + nativefunName;
			}

			return nativefunName;
		}

		public static bool isMethodSkip(System.Reflection.MethodInfo method)
		{
			var item = method;

			if (IsObsolete(item, method.DeclaringType))
			{
				return true;
			}

			if (item.IsGenericMethod)
			{
				return true;
			}

			if (item.ReturnType.IsGenericTypeDefinition)
			{
				return true;
			}

			var rt = MethodNativeCodeCreator.GetAS3Runtimetype(item.ReturnType);
			if (rt > ASBinCode.RunTimeDataType.unknown)
			{
				if (IsSkipType(method.ReturnType))
				{
					return true;
				}
			}

			
			var paras = item.GetParameters();
			foreach (var p in paras)
			{
				if (p.IsOut)
				{
					return true;
				}
				if (p.ParameterType.IsByRef)
				{
					return true;
				}

				if (p.ParameterType.IsGenericTypeDefinition)
				{
					return true;
				}

				if (p.IsOptional)
				{
					if (p.RawDefaultValue != null)
					{
						var rrt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
						if (rrt > ASBinCode.RunTimeDataType.unknown)
						{
							return true;
						}
					}
				}

				if (IsSkipType(p.ParameterType, true))
				{
					return true;
				}
			}

			return false;
			//if (IsObsolete(method,method.DeclaringType))
			//{
			//	return true;
			//}

			//if (method.IsGenericMethod)
			//{
			//	return true;
			//}

			//if (IsSkipType(method.ReturnType))
			//{
			//	return true;
			//}

			//var paras = method.GetParameters();
			//foreach (var para in paras)
			//{
			//	if (para.IsOut)
			//	{
			//		return true;
			//	}
			//	if (para.ParameterType.IsByRef)
			//	{
			//		return true;
			//	}
			//	if (para.ParameterType.IsGenericType)
			//	{
			//		return true;
			//	}

			//	if (para.IsOptional)
			//	{
			//		if (para.RawDefaultValue != null)
			//		{
			//			var rt = MethodNativeCodeCreator.GetAS3Runtimetype(para.ParameterType);
			//			if (rt > ASBinCode.RunTimeDataType.unknown)
			//			{
			//				return true;
			//			}
			//		}
			//	}

			//	if (IsSkipType(para.ParameterType))
			//	{
			//		return true;
			//	}

			//}

			//return false;

		}


		public override string Create()
		{
			Dictionary<Type, string> typeimports = new Dictionary<Type, string>();

			StringBuilder nativefunc = new StringBuilder();
			//GenNativeFuncImport(nativefunc);
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
				"\t\t\tbin.regNativeFunction(LinkSystem_Buildin.getCreator(\""+ GetCreatorNativeFuncName(type) + "\", default("+ NativeCodeCreatorBase.GetTypeFullName(type) + ")));");


			if (super != null)
			{
				as3api.AppendFormat(" extends {0}",GetAS3TypeString(super,typeimports,null,null,null));
			}

			as3api.AppendLine();
			as3api.AppendLine("\t{");

			bool existsindexgetter = false;
			bool existsindexsetter = false;

			foreach (var method in methodlist)
			{
				//if (method.IsGenericMethod)
				//{
				//	Console.WriteLine("跳过泛型方法"+method.ToString());

				//	continue;
				//}

				//if (IsSkipType(method.ReturnType))
				//{
				//	Console.WriteLine(method.ToString() + "返回类型被配置为需要跳过");
				//	continue;
				//}

				
				var paras = method.GetParameters();

				//bool parachecked = true;
				//foreach (var para in paras)
				//{
				//	if (para.IsOut)
				//	{
				//		Console.WriteLine(method.ToString()+"参数"+ para.Position+" " +para+ "为out,跳过");
				//		parachecked=false;
				//		break;
				//	}
				//	if (para.ParameterType.IsByRef)
				//	{
				//		Console.WriteLine(method.ToString() + "参数" + para.Position + " " + para + "为byref,跳过");
				//		parachecked = false;
				//		break;
				//	}
				//	if (para.ParameterType.IsGenericType)
				//	{
				//		Console.WriteLine(method.ToString() + "参数" + para.Position + " " + para + "为泛型,跳过");
				//		parachecked = false;
				//		break;
				//	}

				//	if (para.IsOptional)
				//	{
				//		if (para.RawDefaultValue != null)
				//		{
				//			var rt = MethodNativeCodeCreator.GetAS3Runtimetype(para.ParameterType);
				//			if (rt > ASBinCode.RunTimeDataType.unknown)
				//			{
				//				Console.WriteLine(method.ToString() + "参数" + para.Position + " " + para + "为可选，并且默认值不是基本类型");
				//				parachecked = false;
				//				break;
				//			}
				//		}
				//	}

				//	if (IsSkipType(para.ParameterType))
				//	{
				//		Console.WriteLine(method.ToString() + "参数" + para.Position + " " + para + "被配置为需要跳过");
				//		parachecked = false;
				//		break;
				//	}

				//}

				//if (!parachecked)
				//{
				//	continue;
				//}

				if (isMethodSkip(method))
				{
					Console.WriteLine(method.ToString() + "有参数或返回值被配置为需要跳过");
					continue;
				}
				string returntype = GetAS3TypeString(method.ReturnType, typeimports, type, method, null);

				string nativefunName = GetMethodNativeFunctionName(method,type,null,null);

				System.Reflection.PropertyInfo pinfo;
				if (MethodNativeCodeCreator.CheckIsIndexerGetter(method, type, out pinfo) && !existsindexgetter)
				{
					existsindexgetter = true;
					//****索引器****
					as3api.Append("\t\t");
					as3api.AppendLine("[get_this_item];");

					
					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}];", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					var name = GetMethodName(method.Name, method, type, null, null);
					as3api.Append("function " + name);
				}
				else if (MethodNativeCodeCreator.CheckIsGetter(method, type, out pinfo))
				{

					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}]", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function get ");
					as3api.Append(GetMethodName(pinfo.Name, method,type, null, null));
				}
				else if (MethodNativeCodeCreator.CheckIsIndexerSetter(method, type, out pinfo) && !existsindexsetter)
				{
					existsindexsetter = true;
					//****索引器****
					as3api.Append("\t\t");
					as3api.AppendLine("[set_this_item];");

					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}];", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					var name = GetMethodName(method.Name, method, type, null, null);
					as3api.Append("function " + name);

					var temp = paras[0];
					paras[0] = paras[1];
					paras[1] = temp;

				}
				else if (MethodNativeCodeCreator.CheckIsSetter(method,type,out pinfo))
				{
					
					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}]", nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function set ");
					as3api.Append(GetMethodName(pinfo.Name, method,type, null, null));
				}
				else
				{

					
					as3api.Append("\t\t");
					as3api.AppendFormat("[native,{0}]",nativefunName);
					as3api.AppendLine();

					as3api.Append("\t\t");
					as3api.Append("function ");
					as3api.Append(GetMethodName(method.Name,method,type, null, null));
				}

				as3api.Append("(");

				for (int i = 0; i < paras.Length; i++)
				{
					var para = paras[i];
					as3api.Append(para.Name);
					if (as3keywords.ContainsKey(para.Name))
					{
						as3api.Append("_");
					}
					as3api.Append(":");
					as3api.Append(GetAS3TypeString(para.ParameterType, typeimports,null,method,para));

					if (para.IsOptional)
					{
						as3api.Append("=");

						if (para.RawDefaultValue != null)
						{
							var rt = MethodNativeCodeCreator.GetAS3Runtimetype(para.ParameterType);
							if (rt == ASBinCode.RunTimeDataType.rt_string)
							{
								string str = para.RawDefaultValue.ToString();
								str = str.Replace("\\","\\\\");
								str = str.Replace("\"", "\\\"");

								as3api.Append("\""+str+"\""  );
							}
							else
							{
								as3api.Append(para.RawDefaultValue.ToString());
							}
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
			


			//Console.WriteLine(as3api.ToString());
			//Console.WriteLine(nativefunc.ToString());

			string as3file = "as3api/" + GetPackageName(type).Replace(".", "/") + "/" + name + ".as";
			string nativefunfile = "buildins/" + GetNativeFunctionClassName(type) + ".cs";

			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(as3file));
			System.IO.File.WriteAllText(as3file, as3api.ToString());

			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(nativefunfile));

			StringBuilder usingcode = new StringBuilder();
			GenNativeFuncImport(usingcode);

			System.IO.File.WriteAllText(nativefunfile, usingcode.ToString()+ nativefunc.ToString());

			return nativefunc.ToString();
			
		}

	}
}
