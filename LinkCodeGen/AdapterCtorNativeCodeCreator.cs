using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace LinkCodeGen
{
	class AdapterCtorNativeCodeCreator : NativeCodeCreatorBase
	{
		private string classname;
		private System.Reflection.ConstructorInfo constructor;

		private Type type;

		private string adapterclassname;

		public AdapterCtorNativeCodeCreator(string classname, System.Reflection.ConstructorInfo constructor, Type type,string adapterclassname)
		{
			this.classname = classname;
			this.constructor = constructor;
			this.type = type;
			this.adapterclassname = adapterclassname;
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

			string funccode = Properties.Resources.AdapterCtorFunc;

			funccode = funccode.Replace("[adapterclass]",adapterclassname);

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
			
			
			setnativeinstance = "((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = ";
			



			string newobj = "new ";
			newobj += adapterclassname;
			newobj += "(";

			for (int i = 0; i < paras.Length; i++)
			{
				newobj += string.Format("({0})arg{1}", GetTypeFullName(paras[i].ParameterType), i);
				if (i < paras.Length - 1)
				{
					newobj += ",";
				}
			}

			newobj += ");";

			setnativeinstance += newobj;


			setnativeinstance += "\n";
			setnativeinstance += @"
					((ICrossExtendAdapter)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value)
						.SetAS3RuntimeEnvironment(stackframe.player, ((ASBinCode.rtData.rtObjectBase)thisObj).value._class, (ASBinCode.rtData.rtObjectBase)thisObj);

";

			funccode = funccode.Replace("[setnativeinstance]", setnativeinstance);
			return funccode;

		}

	}
}
