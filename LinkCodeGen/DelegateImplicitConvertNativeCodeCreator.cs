using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class DelegateImplicitConvertNativeCodeCreator : NativeCodeCreatorBase
	{
		private string classname;

		private Type type;

		public DelegateImplicitConvertNativeCodeCreator(string classname, Type type)
		{
			this.classname = classname;
			this.type = type;
		}

		public string GetCode()
		{
			string funccode = Properties.Resources.DelegateImplicitFrom;

			funccode = funccode.Replace("[classname]", classname);

			string pushparas = string.Empty;


			funccode = funccode.Replace("[delegatetype]", GetTypeFullName(type));

			var method = CreatorBase.GetDelegateMethodInfo(type);

			var param = method.GetParameters();

			if (method.ReturnType == typeof(void))
			{
				funccode = funccode.Replace("[return]", string.Empty);
			}
			else
			{
				funccode = funccode.Replace("[return]", "return (" + GetTypeFullName(method.ReturnType) + ")");
			}

			string invokeparam = string.Empty;
			for (int i = 0; i < param.Length; i++)
			{
				invokeparam += param[i].Name;
				if (i < param.Length - 1)
				{
					invokeparam += ",";
				}
			}

			funccode = funccode.Replace("[invokeparam]", invokeparam);


			return funccode;
		}

	}
}
