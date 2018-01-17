using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class CtorNativeCodeCreator : NativeCodeCreatorBase
	{
		private string classname;
		private System.Reflection.ConstructorInfo constructor;

		private Type type;

		public CtorNativeCodeCreator(string classname, System.Reflection.ConstructorInfo constructor, Type type)
		{
			this.classname = classname;
			this.constructor = constructor;
			this.type = type;
		}

		public string GetCode()
		{
			System.Reflection.ParameterInfo[] paras;
			if (constructor != null)
			{
				paras = constructor.GetParameters();
			}
			else
			{
				paras = new System.Reflection.ParameterInfo[0];
			}

			string funccode = Properties.Resources.CtorFunc;

			funccode = funccode.Replace("[classname]", classname);
			funccode = funccode.Replace("[paracount]", paras.Length.ToString());

			string pushparas = string.Empty;

			for (int i = 0; i < paras.Length; i++)
			{
				var para = paras[i];

				pushparas = pushparas + "\t\t\t\t" + string.Format("para.Add({0});\n", GetAS3RuntimeTypeString(para.ParameterType));

			}

			funccode = funccode.Replace("[pushparas]", pushparas);
			

			string loadargs = string.Empty;

			for (int i = 0; i < paras.Length; i++)
			{
				var argement = paras[i];

				loadargs = loadargs + GetLoadArgementString(argement);
				loadargs = loadargs + "\n";


			}

			funccode = funccode.Replace("[loadargement]", loadargs);


			string setnativeinstance = string.Empty;
			if (type.IsValueType)
			{
				//((LinkObj<UInt64>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = (UInt64)value;
				setnativeinstance = "((LinkObj<"+ GetTypeFullName(type) + ">)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = ";
			}
			else
			{
				setnativeinstance = "((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = ";
			}
			


			string newobj = "new ";
			newobj += NativeCodeCreatorBase.GetTypeFullName(type);
			newobj += "(";

			for (int i = 0; i < paras.Length; i++)
			{
				newobj += string.Format("({0})arg{1}", GetTypeFullName( paras[i].ParameterType), i);
				if (i < paras.Length - 1)
				{
					newobj += ",";
				}
			}

			newobj += ");";

			setnativeinstance += newobj;

			funccode = funccode.Replace("[setnativeinstance]", setnativeinstance);
			return funccode;

		}

	}
}
