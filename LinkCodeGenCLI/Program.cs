using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace LinkCodeGenCLI
{
	class Program
	{
		static void Main(string[] args)
		{
			
			bool dirseted = false;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].StartsWith("config="))
				{
					string tempconfig = args[i].Substring(7);
					if (System.IO.File.Exists(tempconfig))
					{
						AppConfig.Change( System.IO.Path.GetFullPath(tempconfig));
						System.Environment.CurrentDirectory = System.IO.Path.GetDirectoryName( System.IO.Path.GetFullPath( tempconfig));
						dirseted = true;
					}
					else
					{
						
						Console.WriteLine("指定的config文件不存在:"+ System.IO.Path.GetFullPath( tempconfig));
						Environment.Exit(1);
						return;
					}
				}
			}
			if (!dirseted)
			{
				System.Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			}

			System.Configuration.AppSettingsReader appSettingsReader = new System.Configuration.AppSettingsReader();

			string outputcode = (string)appSettingsReader.GetValue("combiedcodefile", typeof(string));
			string as3apipath = (string)appSettingsReader.GetValue("as3apipath", typeof(string));
			string csharpcodepath = (string)appSettingsReader.GetValue("csharpcodepath", typeof(string));
			string csharpcodenamespace = (string)appSettingsReader.GetValue("csharpcodenamespace", typeof(string));
			string regfunctioncodenamespace = (string)appSettingsReader.GetValue("regfunctioncodenamespace", typeof(string));
			string regfunctioncode = (string)appSettingsReader.GetValue("regfunctioncodefile", typeof(string));

			bool makemscorelib = (bool)appSettingsReader.GetValue("makemscorlib", typeof(bool));



			LinkCodeGen.Generator generator = new LinkCodeGen.Generator();

			{
				var skipcreatortypes = (StringListSection)System.Configuration.ConfigurationManager.GetSection("skipcreatortypes");
				List<string> configs = new List<string>();
				foreach (AssemblyElement ele in skipcreatortypes.Types)
					configs.Add(ele.StringValue);
				generator.AddSkipCreateTypes(configs);

			}

			{
				var notcreatenamespace = (StringListSection)System.Configuration.ConfigurationManager.GetSection("notcreatenamespace");
				List<string> configs = new List<string>();
				foreach (AssemblyElement ele in notcreatenamespace.Types)
					configs.Add(ele.StringValue);
				generator.AddNotCreateNameSpace(configs);


				generator.AddNotCreateNameSpace(new string[] {
					"ASBinCode","ASRuntime",csharpcodenamespace,regfunctioncodenamespace
				});
			}

			{
				var notcreatetypes = (StringListSection)System.Configuration.ConfigurationManager.GetSection("notcreatetypes");
				List<string> configs = new List<string>();
				foreach (AssemblyElement ele in notcreatetypes.Types)
					configs.Add(ele.StringValue);
				generator.AddNotCreateTypes(configs);
			}
			{
				var notcreatemembers = (StringListSection)System.Configuration.ConfigurationManager.GetSection("notcreatemembers");
				List<string> configs = new List<string>();
				foreach (AssemblyElement ele in notcreatemembers.Types)
					configs.Add(ele.StringValue);
				generator.AddNotCreateMember(configs);
			}


			

			string[] files = null;
			Dictionary<string, string> srcFileProjFile = new Dictionary<string, string>();

			string sdkpath = (string)appSettingsReader.GetValue("sdkpath", typeof(string));
			if (sdkpath == "auto")
			{
				
				sdkpath = System.IO.Path.GetDirectoryName( System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
				Console.WriteLine("SDK路径设置为自动。");

			}


			if (System.IO.Directory.Exists(sdkpath))
			{
				if (System.IO.File.Exists(sdkpath + "/air-sdk-description.xml")
					&&
					System.IO.Directory.Exists(sdkpath + "/as3_commapi/sharpapi/")
					)
				{
					string sharpapi = System.IO.Path.GetFullPath(sdkpath + "/as3_commapi/sharpapi/");

					var linkapi = System.IO.Directory.GetFiles(sharpapi, "*.as", System.IO.SearchOption.AllDirectories);

					foreach (var item in linkapi)
					{
						string projfile = item.Replace("\\", "/").Replace(sharpapi.Replace("\\", "/"), "");
						if (projfile.StartsWith("/"))
							projfile = projfile.Substring(1);
						srcFileProjFile.Add(item, projfile);
					}

					files = new string[linkapi.Length];
					linkapi.CopyTo(files, 0);
				}
				else
				{
					Console.WriteLine("sdk文件夹无效");
					Console.WriteLine("请指定ASRuntimeSDK地址");
					Environment.Exit(1);
					return;
				}
			}
			else
			{
				Console.WriteLine("sdk文件夹没有找到");
				Console.WriteLine("请指定ASRuntimeSDK地址");
				Environment.Exit(1);
				return;
			}

			SDKPATH = sdkpath;

			outputcode = replacePathVariable(outputcode);
			as3apipath = replacePathVariable(as3apipath);
			csharpcodepath = replacePathVariable(csharpcodepath);
			regfunctioncode = replacePathVariable(regfunctioncode);

			//****加载dll***
			List<Type> types = new List<Type>();
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
			if (makemscorelib)
			{
				//加入基础类型
				types.AddRange(typeof(object).Assembly.GetExportedTypes());
			}
			{
				var buildassemblys = (AssemblyListSection)System.Configuration.ConfigurationManager.GetSection("buildassemblys");
				foreach (AssemblyDefineElement  asm in buildassemblys.Assemblys)
				{
					string assembly = asm.StringValue;
					string fullpath = System.IO.Path.GetFullPath(assembly);

					m_rootAssembly = System.IO.Path.GetDirectoryName(fullpath);

					try
					{
						var dll = System.Reflection.Assembly.ReflectionOnlyLoadFrom(fullpath);


						List<string> definetypes = new List<string>();

						List<string> definenamespaces = new List<string>();

						foreach (AssemblyTypeElement type in asm.Types)
						{
							if (type.DefineType == "type")
							{
								definetypes.Add(type.StringValue);
							}
							else if (type.DefineType == "namespace")
							{
								definenamespaces.Add(type.StringValue);
							}
							else
							{
								Console.WriteLine("导出类型配置错误。definetype只能是 type 或者 namespace");

								Environment.Exit(1);
								return;
							}
						}

						foreach (var type in dll.GetExportedTypes())
						{
							if (definetypes.Count == 0 && definenamespaces.Count == 0
								//||
								//definetypes.Contains(type.FullName)
								)
							{
								types.Add(type);
							}
							else
							{
								if (definetypes.Contains(type.FullName))
								{
									types.Add(type);
								}
								else if (definenamespaces.Contains(type.Namespace))
								{
									types.Add(type);
								}
							}
						}

					}
					catch (System.IO.FileLoadException e)
					{
						Console.WriteLine(e.ToString());
						
						Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
						Environment.Exit(1);
						return;
					}
					catch (System.BadImageFormatException e)
					{
						Console.WriteLine(e.ToString());

						Console.WriteLine("可能加载了无法分析的dll。请到Unity的安装目录下/Editor/Data/Managed/目录下加载UnityEngine.dll, /Editor/Data/UnityExtensions/Unity/GUISystem/下加载UnityEngine.UI.dll。");


						Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
						Environment.Exit(1);
					}
					catch (System.Reflection.ReflectionTypeLoadException e)
					{
						Console.WriteLine(e.ToString());
						foreach (var l in e.LoaderExceptions)
						{
							Console.WriteLine(l.ToString());
						}

						Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
						Environment.Exit(1);
						return;
					}
					catch (FileNotFoundException e)
					{
						Console.WriteLine(e.ToString());
						Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
						Environment.Exit(1);
						return;
					}
					catch (System.Security.SecurityException e)
					{
						Console.WriteLine(e.ToString());
						Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
						Environment.Exit(1);
						return;
					}


				}
			}


			generator.AddTypes(types);


			using (System.IO.FileStream fs = new System.IO.FileStream(outputcode, System.IO.FileMode.Create))
			{
				string regcode;
				generator.MakeCode(fs, as3apipath, csharpcodepath, csharpcodenamespace, regfunctioncodenamespace, out regcode);
				System.IO.File.WriteAllText(regfunctioncode, regcode);
			}

			Console.WriteLine("====");

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("开始编译lib");



			//编译刚生成的as3api.
			{
				string apidir = as3apipath;
				if (System.IO.Directory.Exists(apidir))
				{
					var linkapi = System.IO.Directory.GetFiles(apidir, "*.as", System.IO.SearchOption.AllDirectories);

					foreach (var item in linkapi)
					{
						string projfile = item.Replace("\\", "/").Replace(apidir.Replace("\\", "/"), "");
						if (projfile.StartsWith("/"))
							projfile = projfile.Substring(1);
						srcFileProjFile.Add(item, projfile);
					}

					string[] n = new string[files.Length + linkapi.Length];
					linkapi.CopyTo(n, 0);
					files.CopyTo(n, linkapi.Length);
					files = n;
				}
			}
			//***加入其他lib****			
			{
				var includelibcode = (StringListSection)System.Configuration.ConfigurationManager.GetSection("includelibcode");
				List<string> configs = new List<string>();
				foreach (AssemblyElement ele in includelibcode.Types)
					configs.Add( replacePathVariable( ele.StringValue));
				foreach (var apidir in configs)
				{
					
					if (System.IO.Directory.Exists(apidir))
					{
						var linkapi = System.IO.Directory.GetFiles(apidir, "*.as", System.IO.SearchOption.AllDirectories);

						foreach (var item in linkapi)
						{
							string projfile = item.Replace("\\", "/").Replace(apidir.Replace("\\", "/"), "");
							if (projfile.StartsWith("/"))
								projfile = projfile.Substring(1);
							srcFileProjFile.Add(item, projfile);
						}

						string[] n = new string[files.Length + linkapi.Length];
						linkapi.CopyTo(n, 0);
						files.CopyTo(n, linkapi.Length);
						files = n;
					}
				}


			}




			//****开始编译lib*****
			ASTool.Grammar grammar = ASCompiler.Grammar.getGrammar();
			var proj = new ASTool.AS3.AS3Proj();
			var srcout = new ASTool.ConSrcOut();

			for (int i = 0; i < files.Length; i++)
			{
				grammar.hasError = false;
				var teststring = System.IO.File.ReadAllText(files[i]);
				if (string.IsNullOrEmpty(teststring))
				{
					continue;
				}
				var tree = grammar.ParseTree(teststring, ASTool.AS3LexKeywords.LEXKEYWORDS,
							ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, srcFileProjFile[files[i]]);

				if (grammar.hasError)
				{
					Console.WriteLine(files[i]);
					Console.WriteLine("解析语法树失败!");
					
					Environment.Exit(1);
					return;
				}
				
				var analyser = new ASTool.AS3FileGrammarAnalyser(proj, srcFileProjFile[files[i]]);
				if (!analyser.Analyse(tree)) //生成项目的语法树
				{
					Console.WriteLine(analyser.err.ToString());
					Console.WriteLine("语义分析失败!");
					Environment.Exit(1);
					return;
				}
			}

			ASCompiler.compiler.Builder builder = new ASCompiler.compiler.Builder();
			
			builder.options.CheckNativeFunctionSignature = false;
			builder.Build(proj, null);

			if (builder.buildErrors.Count == 0)
			{
				ASBinCode.CSWC swc = builder.getBuildOutSWC();
				byte[] bin = swc.toBytes();

				string as3libfile = (string)appSettingsReader.GetValue("as3libfile", typeof(string));
				System.IO.File.WriteAllBytes(as3libfile, swc.toBytes());

				Console.WriteLine("创建完成.按任意键结束。");
				Console.ReadLine();
			}
			
			
		}

		private static string SDKPATH;

		private static string replacePathVariable(string input)
		{
			return input.Replace("$(sdkpath)", SDKPATH);
		}



		private static string m_rootAssembly;
		private static System.Reflection.Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			AssemblyName name = new AssemblyName(args.Name);
			String asmToCheck = m_rootAssembly + "/" + name.Name + ".dll";
			
			if (File.Exists(asmToCheck))
			{
				return Assembly.ReflectionOnlyLoadFrom(asmToCheck);
			}
			else
			{
				var resolvepath = (StringListSection)System.Configuration.ConfigurationManager.GetSection("resolvepath");

				foreach (AssemblyElement ele in resolvepath.Types)
				{
					asmToCheck = replacePathVariable( ele.StringValue  + "/" + name.Name + ".dll");

					if (File.Exists(asmToCheck))
					{
						return Assembly.ReflectionOnlyLoadFrom(asmToCheck);
					}

				}

			}


			return Assembly.ReflectionOnlyLoad(args.Name);

		}
	}

	#region StringListSection

	class StringListSection : System.Configuration.ConfigurationSection
	{
		

		[ConfigurationProperty("", IsDefaultCollection = true)]
		public StringElementCollection Types
		{
			get
			{
				return (StringElementCollection)base[""];
			}
		}
	}

	[ConfigurationCollection(typeof(AssemblyElement), AddItemName = "item")]
	public class StringElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyElement)element).StringValue;
		}
	}

	public class AssemblyElement : ConfigurationElement
	{
		[ConfigurationProperty("value", IsRequired = true)]
		public string StringValue
		{
			get
			{
				return (string)base["value"];
			}

			set
			{
				base["value"] = value;
			}
		}
	}

	#endregion

	#region AssemblySection
	class AssemblyListSection : System.Configuration.ConfigurationSection
	{
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public AssemblyDefineCollection Assemblys
		{
			get
			{
				return (AssemblyDefineCollection)base[""];
			}
		}
	}

	[ConfigurationCollection(typeof(AssemblyDefineElement), AddItemName = "assembly")]
	public class AssemblyDefineCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyDefineElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyDefineElement)element).StringValue;
		}
	}

	public class AssemblyDefineElement : ConfigurationElement
	{
		[ConfigurationProperty("value", IsRequired = true)]
		public string StringValue
		{
			get
			{
				return (string)base["value"];
			}

			set
			{
				base["value"] = value;
			}
		}

		[ConfigurationProperty("",IsDefaultCollection =true)]
		public AssemblyTypeCollection Types
		{
			get
			{
				return (AssemblyTypeCollection)base[""];
			}
		}

		
	}


	[ConfigurationCollection(typeof(AssemblyDefineElement), AddItemName = "type")]
	public class AssemblyTypeCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyTypeElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyTypeElement)element).StringValue;
		}
	}

	public class AssemblyTypeElement : ConfigurationElement
	{
		[ConfigurationProperty("definetype", IsRequired = true)]
		public string DefineType
		{
			get
			{
				return (string)base["definetype"];
			}

			set
			{
				base["definetype"] = value;
			}
		}


		[ConfigurationProperty("value", IsRequired = true)]
		public string StringValue
		{
			get
			{
				return (string)base["value"];
			}

			set
			{
				base["value"] = value;
			}
		}
	}


	

	#endregion

}
