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
			LinkCodeGen.Generator generator = new LinkCodeGen.Generator();

			{
				var skipcreatortypes = (StringListSection)System.Configuration.ConfigurationManager.GetSection("skipcreatortypes");
				List<string> configs = new List<string>();
				foreach (StringElement ele in skipcreatortypes.Types)
					configs.Add(ele.StringValue);
				generator.AddSkipCreateTypes(configs);

			}

			{
				var notcreatenamespace = (StringListSection)System.Configuration.ConfigurationManager.GetSection("notcreatenamespace");
				List<string> configs = new List<string>();
				foreach (StringElement ele in notcreatenamespace.Types)
					configs.Add(ele.StringValue);
				generator.AddNotCreateNameSpace(configs);
			}

			{
				var notcreatetypes = (StringListSection)System.Configuration.ConfigurationManager.GetSection("notcreatetypes");
				List<string> configs = new List<string>();
				foreach (StringElement ele in notcreatetypes.Types)
					configs.Add(ele.StringValue);
				generator.AddNotCreateTypes(configs);
			}

			
			
			System.Configuration.AppSettingsReader appSettingsReader = new System.Configuration.AppSettingsReader();

			string outputcode = (string)appSettingsReader.GetValue("combiedcodefile", typeof(string));
			string as3apipath = (string)appSettingsReader.GetValue("as3apipath", typeof(string));
			string csharpcodepath = (string)appSettingsReader.GetValue("csharpcodepath", typeof(string));
			string csharpcodenamespace = (string)appSettingsReader.GetValue("csharpcodenamespace", typeof(string));
			string regfunctioncodenamespace = (string)appSettingsReader.GetValue("regfunctioncodenamespace", typeof(string));
			string regfunctioncode = (string)appSettingsReader.GetValue("regfunctioncodefile", typeof(string));

			string libdir = (string)appSettingsReader.GetValue("libdir", typeof(string));

			if (string.IsNullOrEmpty(libdir))
			{
				Console.WriteLine("需要配置要处理的dll所在目录");
				return;
			}

			if (!System.IO.Directory.Exists(libdir))
			{
				Console.WriteLine("dll所在目录不存在");
				return;
			}

			var dlllist = System.IO.Directory.GetFiles(libdir, "*.dll", System.IO.SearchOption.TopDirectoryOnly);

			if (dlllist.Length == 0)
			{
				Console.WriteLine("没有要处理的dll");
				return;
			}
			m_rootAssembly = System.IO.Path.GetFullPath( libdir);
			List<Type> types = new List<Type>();
			{
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

				//加入基础类型
				types.AddRange(typeof(object).Assembly.GetExportedTypes());

				foreach (var item in dlllist)
				{
					string fullpath = System.IO.Path.GetFullPath(item);

					try
					{
						var dll = System.Reflection.Assembly.ReflectionOnlyLoadFrom(fullpath);
						
						types.AddRange(dll.GetExportedTypes());

					}
					catch (System.Reflection.ReflectionTypeLoadException e)
					{
						Console.WriteLine(e.ToString());
						foreach (var l in e.LoaderExceptions)
						{
							Console.WriteLine(l.ToString());
						}
						
						Console.WriteLine( System.IO.Path.GetFileName(fullpath) + "读取失败");
						return;
					}
					//catch (BadImageFormatException e)
					//{
					//	Console.WriteLine(e.ToString());
					//	Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
					//	return;
					//}
					catch (FileNotFoundException e)
					{
						Console.WriteLine(e.ToString());
						Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
						return;
					}
					catch (System.Security.SecurityException e)
					{
						Console.WriteLine(e.ToString());
						Console.WriteLine(System.IO.Path.GetFileName(fullpath) + "读取失败");
						return;
					}
				}


				//var dll = System.Reflection.Assembly.LoadFrom(@"E:\Manju-pc\blacksmith\blacksmith\Library\UnityAssemblies\UnityEngine.dll");
				//types.AddRange(dll.GetTypes());
				//dll = System.Reflection.Assembly.LoadFrom(@"E:\Manju-pc\blacksmith\blacksmith\Library\UnityAssemblies\UnityEngine.UI.dll");

				//types.AddRange(dll.GetTypes());
			}



			generator.AddTypes(types);


			using (System.IO.FileStream fs = new System.IO.FileStream(outputcode, System.IO.FileMode.Create))
			{
				string regcode;
				generator.MakeCode(fs, as3apipath, csharpcodepath, csharpcodenamespace, regfunctioncodenamespace, out regcode);
				System.IO.File.WriteAllText(regfunctioncode, regcode);
			}


			Console.WriteLine("创建完成");
			
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			throw new NotImplementedException();
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

			return Assembly.ReflectionOnlyLoad(args.Name);

		}
	}



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

	[ConfigurationCollection(typeof(StringElement), AddItemName = "item")]
	public class StringElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new StringElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((StringElement)element).StringValue;
		}
	}

	public class StringElement : ConfigurationElement
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

}
