using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class StaticFieldSetterNativeCodeCreator : NativeCodeCreatorBase
	{
		private string classname;
		private System.Reflection.FieldInfo field;
		private Type type;

		public StaticFieldSetterNativeCodeCreator(string classname, Type classtype, System.Reflection.FieldInfo field)
		{
			this.classname = classname;
			this.type = classtype;
			this.field = field;
		}


		public string GetCode()
		{
			string funccode = Properties.Resources.StaticFieldSetter;

			funccode = funccode.Replace("[classname]", classname);

			string pushparas = string.Empty;
			pushparas = pushparas + "\t\t\t\t" + string.Format("para.Add({0});\n", GetAS3RuntimeTypeString(field.FieldType));


			funccode = funccode.Replace("[pushparas]", pushparas);


			string loadargs = string.Empty;


			loadargs = loadargs + GetLoadArgementString(field.FieldType, 0);
			loadargs = loadargs + "\n";

			funccode = funccode.Replace("[loadargement]", loadargs);


			funccode = funccode.Replace("[setstaticfield]", type.FullName + "." + field.Name + " = " + "(" + field.FieldType.FullName + ")" + "arg0;");


			return funccode;

		}

	}
}
