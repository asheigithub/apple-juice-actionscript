using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASRuntime.nativefuncs;

namespace ASRuntime
{
    partial class extFunctions : INativeFunctionRegister , INativeFunctionFactory
    {
		public float progress=0;
		public NativeFunctionBase Create(string typename)
		{
			//Type ft = this.GetType().Assembly.GetType(typename);
			//return (NativeFunctionBase)System.Activator.CreateInstance(ft);
			//if (types == null)
			//	types = typeof(extFunctions).Assembly.GetTypes();

			//foreach (var item in types)
			//{
			//	if (item.FullName == typename)
			//	{

			//	}
			//}
			
			int dot = typename.LastIndexOf('.');
			string t1 = typename.Substring(0, dot);
			string t2 = typename.Substring(dot + 1);

			Type c = this.GetType().Assembly.GetType(t1);
			Type nt = c.GetNestedType(t2);

			return (NativeFunctionBase)System.Activator.CreateInstance(nt);

		}

		public void registrationFunction(CSWC bin)
        {
			bin.SetNativeFunctionFactory(this);
			regAutoCreateCodes(bin);
        }


		

	}
}
