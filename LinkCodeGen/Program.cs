using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var ass = System.Reflection.Assembly.GetAssembly(typeof(object));

			StringBuilder regclassSb = new StringBuilder();

            foreach (var item in ass.GetTypes())
            {
                if (item.IsPublic && item.IsEnum && !item.IsNested && item.Namespace.StartsWith("System"))
                {
                    EnumCreator ec = new EnumCreator(item, "", "");
                    ec.Create();

					regclassSb.AppendLine( "\t\t\t"+EnumCreator.GetNativeFunctionClassName(item) + ".regNativeFunctions(bin);");


                }
            }

			System.IO.File.WriteAllText("retNativeCode.txt", regclassSb.ToString());

			Console.Read();
        }
    }
}
