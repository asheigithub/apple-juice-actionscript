using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LinkCodeGenCLI
{
	class TypesLoader:System.MarshalByRefObject
	{
		Type[] types;
		public void LoadAllTypes(string fullpath)
		{
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

			m_rootAssembly = System.IO.Path.GetDirectoryName(fullpath);

			List<Type> result = new List<Type>();

			var dll = System.Reflection.Assembly.ReflectionOnlyLoadFrom(fullpath);
			types=(dll.GetExportedTypes());
		}

		public int get_totaltypes()
		{
			return types.Length;
		}

		public Type Get(int index)
		{
			return types[index];
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
					asmToCheck = ele.StringValue + "/" + name.Name + ".dll";

					if (File.Exists(asmToCheck))
					{
						return Assembly.ReflectionOnlyLoadFrom(asmToCheck);
					}

				}

			}


			return Assembly.ReflectionOnlyLoad(args.Name);

		}
	}
}
