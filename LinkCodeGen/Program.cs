using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	



	class Program
    {
        static void Main(string[] args)
        {

			if (System.IO.File.Exists("skipcreatortypes.txt"))
			{
				var configs = System.IO.File.ReadAllLines("skipcreatortypes.txt");
				CreatorBase.SkipCreateTypes.AddRange(configs);
			}

			if (System.IO.File.Exists("notcreatenamespace.txt"))
			{
				var configs = System.IO.File.ReadAllLines("notcreatenamespace.txt");
				CreatorBase.NotCreateNameSpace.AddRange(configs);
			}

			if (System.IO.File.Exists("notcreatetypes.txt"))
			{
				var configs = System.IO.File.ReadAllLines("notcreatetypes.txt");
				CreatorBase.NotCreateTypes.AddRange(configs);
			}




			var assembly = System.Reflection.Assembly.GetAssembly(typeof(object));
			StringBuilder regclassSb = new StringBuilder();

			//****Enum****
			//       foreach (var item in ass.GetTypes())
			//       {
			//           if (item.IsPublic && item.IsEnum && !item.IsNested && item.Namespace.StartsWith("System"))
			//           {
			//               EnumCreator ec = new EnumCreator(item, "", "");
			//               ec.Create();

			//regclassSb.AppendLine( "\t\t\t"+EnumCreator.GetNativeFunctionClassName(item) + ".regNativeFunctions(bin);");


			//           }
			//       }
			//***********

			//******接口******
			Dictionary<Type, CreatorBase> creators = new Dictionary<Type, CreatorBase>();
			//foreach (var item in assembly.GetTypes())
			//{
			//	if (item.IsPublic && item.IsInterface && !item.IsNested && item.Namespace.StartsWith("System")
			//		&& !item.IsGenericType
			//		&& !item.IsCOMObject
			//		&& !item.Namespace.StartsWith("System.Runtime")
			//		&& !item.Namespace.StartsWith("System.Security")
			//		)
			//	{

			//		if (!creators.ContainsKey(item))
			//		{
			//			creators.Add(item, null);
			//			creators[item]= new InterfaceCreator(item, "", "", creators);


			//		}

			//	}

			//}

			//var interfacetype = typeof(System.Collections.ICollection);
			//if (!creators.ContainsKey(interfacetype))
			//{
			//	creators.Add(interfacetype, null);

			//	creators[interfacetype] = new InterfaceCreator(interfacetype, "", "", creators);


			//}
			//foreach (var item in creators.Values)
			//{
			//	item.Create();
			//}

			//*****类*******

			var types = typeof(object).Assembly.GetTypes();

			foreach (var item in types)
			{
				var classtype = item;

				if (!CreatorBase.IsSkipType(classtype) && !CreatorBase.IsSkipCreator(classtype)

					&& classtype.IsClass && classtype.IsPublic
					)
				{
					if (!creators.ContainsKey(classtype))
					{
						creators.Add(classtype, null);
						creators[classtype] = new ClassCreator(classtype, "", "", creators, "ASCAutoGen.regNativeFunctions");
					}
				}
			}
			//var classtype = typeof(AutoGenCodeLib.Testobj);
			//if (!creators.ContainsKey(classtype))
			//{
			//	creators.Add(classtype, null);
			//	creators[classtype] = new ClassCreator(classtype, "", "", creators, "ASCAutoGen.regNativeFunctions");
			//}
			//var classtype = typeof(ICloneable);
			//if (!creators.ContainsKey(classtype))
			//{
			//	creators.Add(classtype, null);
			//	creators[classtype] = new InterfaceCreator(classtype, "", "", creators, "ASCTest.regNativeFunctions");
			//}

			using (System.IO.FileStream fs=new System.IO.FileStream("codeoutput.cs", System.IO.FileMode.Create))
			{
				using (System.IO.StreamWriter sw=new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8))
				{
					StringBuilder usingcode = new StringBuilder();
					CreatorBase.GenNativeFuncImport(usingcode);

					sw.WriteLine(usingcode.ToString());

					regclassSb.AppendLine("using System;");
					regclassSb.AppendLine("using System.Collections.Generic;");
					regclassSb.AppendLine("using System.Text;");
					regclassSb.AppendLine("using ASBinCode;");
					//regclassSb.AppendLine("using ASCTest.regNativeFunctions;");
					regclassSb.AppendLine("using ASRuntime.nativefuncs;");

					regclassSb.AppendLine("namespace ASCTest");
					regclassSb.AppendLine("{");

					regclassSb.AppendLine("\tpartial class extFunctions : INativeFunctionRegister");
					regclassSb.AppendLine("\t{");
					regclassSb.AppendLine("\t\tprivate void regAutoCreateCodes(CSWC bin)");
					regclassSb.AppendLine("\t\t{");

					foreach (var item in creators.Values)
					{
						Console.WriteLine("building:" + item.BuildIngType);
						string code = item.Create().Replace("\r","").Replace("\n","\r\n");


						regclassSb.AppendLine("\t\t\t" + item.linkcodenamespace + "." + CreatorBase.GetNativeFunctionClassName(item.BuildIngType) + ".regNativeFunctions(bin);");
						sw.WriteLine(code);

					}
					
					regclassSb.AppendLine("\t\t}");
					regclassSb.AppendLine("\t}");
					regclassSb.AppendLine("\t}");
					regclassSb.AppendLine();
				}

				
			}

			

			System.IO.File.WriteAllText("retNativeCode.cs", regclassSb.ToString().Replace("\r", "").Replace("\n", "\r\n"));
			Console.WriteLine("创建完成");
			Console.Read();
        }

		

	}

	

}
