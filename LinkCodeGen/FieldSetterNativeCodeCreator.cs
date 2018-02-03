using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class FieldSetterNativeCodeCreator : NativeCodeCreatorBase
	{
		private string classname;
		private System.Reflection.FieldInfo field;
		private Type type;

		public FieldSetterNativeCodeCreator(string classname, Type classtype, System.Reflection.FieldInfo field)
		{
			this.classname = classname;
			this.type = classtype;
			this.field = field;
		}

		public string GetCode()
		{
			string funccode = Properties.Resources.FieldSetter;

			funccode = funccode.Replace("[classname]", classname);

			string pushparas = string.Empty;
			pushparas = pushparas + "\t\t\t\t" + string.Format("para.Add({0});\n", GetAS3RuntimeTypeString(field.FieldType));
			funccode = funccode.Replace("[pushparas]", pushparas);

			if (type.IsValueType)
			{
				string loadthis = Properties.Resources.LoadStructThis;
				loadthis = loadthis.Replace("[thisObjtype]", NativeCodeCreatorBase.GetTypeFullName(type));
				funccode = funccode.Replace("[loadthis]", loadthis);
			}
			else
			{
				string loadthis = Properties.Resources.LoadThis;
				loadthis = loadthis.Replace("[thisObjtype]", NativeCodeCreatorBase.GetTypeFullName(type));
				funccode = funccode.Replace("[loadthis]", loadthis);
			}

			string loadargs = string.Empty;
			loadargs = loadargs + GetLoadArgementString(field.FieldType, 0);
			loadargs = loadargs + "\n";
			funccode = funccode.Replace("[loadargement]", loadargs);

			funccode = funccode.Replace("[setfield]",  "_this." + field.Name + " = " + "(" + GetTypeFullName( field.FieldType) + ")" + "arg0;");

			if (type.IsValueType) //结构体，需要重新赋值回去
			{
				string replacethis = "((LinkObj<"+ GetTypeFullName( type) +">)((ASBinCode.rtData.rtObjectBase) thisObj).value).value = _this;";
				funccode = funccode.Replace("[replacethis]", replacethis);
			}
			else
			{
				funccode = funccode.Replace("[replacethis]", string.Empty);
			}

			return funccode;
		}
	}
}
