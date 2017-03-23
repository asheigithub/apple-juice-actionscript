using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public class Global_Object:DynamicObject 
    {
        public static Global_Object formCodeBlock(CodeBlock codeblock,ISLOT[] data,Class _class  )
        {
            //throw new NotImplementedException();

            Global_Object global = new Global_Object(_class);

            for (int i = 0; i < codeblock.scope.members.Count; i++)
            {
                global.propertys.Add(codeblock.scope.members[i].name, data[i]);
            }

            return global;
        }

        public Global_Object(rtti.Class _class):base(_class )
        {
            
        }

        public ISLOT findpropertybyname(string name)
        {
            if (propertys.ContainsKey(name))
            {
                return propertys[name];
            }
            return null;
        }

        

        public override string ToString()
        {
            return "[global @" +objectid.ToString("x")+ "]";
        }

    }
}
