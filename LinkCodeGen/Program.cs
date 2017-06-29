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
			//			InterfaceCreator itc = new InterfaceCreator(item, "", "", creators);

			//			creators.Add(item, itc);
			//		}

			//	}

			//}

			var interfacetype = typeof(ICloneable);
			if (!creators.ContainsKey(interfacetype))
			{
				InterfaceCreator itc = new InterfaceCreator(interfacetype, "", "", creators);

				creators.Add(interfacetype, itc);
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
