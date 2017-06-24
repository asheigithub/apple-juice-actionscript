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

            foreach (var item in ass.GetTypes())
            {
                if (item.IsPublic && item.IsEnum && !item.IsNested && item.Namespace.StartsWith("System"))
                {
                    EnumCreator ec = new EnumCreator(item, "", "");
                    ec.Create();
                    
                }
            }
            

            Console.Read();
        }
    }
}
