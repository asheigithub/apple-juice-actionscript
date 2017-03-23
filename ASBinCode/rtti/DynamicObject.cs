using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public class DynamicObject : Object
    {
        protected Dictionary<string, ISLOT> propertys;

        public DynamicObject(Class _class):base(_class)
        {
            propertys = new Dictionary<string, ISLOT>();
        }

        public ISLOT this[string name]
        {
            get
            {
                return propertys[name];
            }
        }

        public bool hasproperty(string name)
        {
            return propertys.ContainsKey(name);
        }

        public void createproperty(string name,ISLOT slot)
        {
            if (!propertys.ContainsKey(name))
            {
                propertys.Add(name, slot);
            }
        }

        public void deleteProperty(string name)
        {
            if (propertys.ContainsKey(name))
            {
                propertys.Remove(name);
            }
        }

    }
}
