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

			var classtype = typeof(System.Data.SqlClient.SqlConnection);
			if (!creators.ContainsKey(classtype))
			{
				creators.Add(classtype, null);
				creators[classtype] = new ClassCreator(classtype, "", "", creators);

			}
			foreach (var item in creators.Values)
			{
				item.Create();
			}

			//System.IO.File.WriteAllText("retNativeCode.txt", regclassSb.ToString());

			Console.Read();
        }

		

	}

	

}
