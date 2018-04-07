using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASRuntime.nativefuncs;

namespace ASRuntime
{
    public partial class extFunctions : INativeFunctionRegister , INativeFunctionFactory
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

			Type c = typeof(ASRuntime.Player).Assembly.GetType(t1);

			if (c == null)
			{
				c = this.GetType().Assembly.GetType(t1);
			}

			if (c == null)
				throw new ASRunTimeException("nativefunction " + typename + "not found",new InvalidOperationException());
			
			Type nt = c.GetNestedType(t2);

			return (NativeFunctionBase)System.Activator.CreateInstance(nt);

		}

		public System.Collections.IEnumerator registrationFunction(CSWC bin)
        {
			



			bin.SetNativeFunctionFactory(this);

			var method= this.GetType().GetMethod("regAutoCreateCodes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			if (method == null)
			{
				//The AS3 project-related code was not found.Please create the AS3 project first, or export the API in the AS3 project
				throw new InvalidOperationException("没有找到AS3项目相关代码。请先创建AS3项目,或在AS3项目中将API导出");
				
			}
			return (System.Collections.IEnumerator)method.Invoke(this, new object[] { bin });
			
			//return regAutoCreateCodes(bin);
        }


		

	}
}
