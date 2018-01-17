using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class ValueOfNativeCodeCreator : NativeCodeCreatorBase
	{
		private string classname;
		private System.Reflection.MethodInfo valueofmethod;
		private Type type;

		public ValueOfNativeCodeCreator(string classname, Type classtype, System.Reflection.MethodInfo valueofmethod)
		{
			this.classname = classname;
			this.type = classtype;
			this.valueofmethod = valueofmethod;
		}

		public string GetCode()
		{

			string funccode = Properties.Resources.ValueOf;

			funccode = funccode.Replace("[classname]", classname);
			funccode = funccode.Replace("[returntype]", GetAS3RuntimeTypeString(valueofmethod.ReturnType));
			if (type.IsValueType)
			{
				string loadthis = Properties.Resources.LoadStructThis;
				loadthis = loadthis.Replace("[thisObjtype]", GetTypeFullName( type));
				funccode = funccode.Replace("[loadthis]", loadthis);
			}
			else
			{
				string loadthis = Properties.Resources.LoadThis;
				loadthis = loadthis.Replace("[thisObjtype]", GetTypeFullName(type));
				funccode = funccode.Replace("[loadthis]", loadthis);
			}

			var rettype = GetAS3Runtimetype(valueofmethod.ReturnType);
			if (rettype == ASBinCode.RunTimeDataType.fun_void)
			{
				funccode = funccode.Replace("[storeresult]", "代码生成错误，ValueOf只能是基本类型");
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_int)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "int _result_ = (int)(_this";

				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_uint)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "uint _result_ = (uint)(_this" ;

				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_number)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "double _result_ = (double)(_this";

				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_string)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "string _result_ = (string)(_this";

				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_boolean)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "bool _result_ = (bool)_this";

				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\tif(_result_)\n";
				storeresult += "\t\t\t\t\t{\n";
				storeresult += "\t\t\t\t\t\treturnSlot.setValue(ASBinCode.rtData.rtBoolean.True);\n";
				storeresult += "\t\t\t\t\t}\n";
				storeresult += "\t\t\t\t\telse\n";
				storeresult += "\t\t\t\t\t{\n";
				storeresult += "\t\t\t\t\t\treturnSlot.setValue(ASBinCode.rtData.rtBoolean.False);\n";
				storeresult += "\t\t\t\t\t}\n";


				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype > ASBinCode.RunTimeDataType.unknown)
			{
				funccode = funccode.Replace("[storeresult]", "代码生成错误，ValueOf只能是基本类型");
			}
			else
			{
				funccode = funccode.Replace("[storeresult]", "代码生成错误，不能转换返回类型");
			}


			return funccode;

		}



	}
}
