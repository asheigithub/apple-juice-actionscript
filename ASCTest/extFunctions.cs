using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASRuntime.nativefuncs;

namespace ASCTest
{
    public partial class extFunctions : INativeFunctionRegister , INativeFunctionFactory
    {
		public float progress = 0;
		public NativeFunctionBase Create(string typename)
		{
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

		public void registrationAllFunction(CSWC bin)
		{
			bin.SetNativeFunctionFactory(this);
			var it= regAutoCreateCodes(bin);
			while (it.MoveNext())
			{
				
			}
		}

		IEnumerator INativeFunctionRegister.registrationFunction(CSWC bin)
		{
			bin.SetNativeFunctionFactory(this);

			throw new NotImplementedException();
		}
	}
}
