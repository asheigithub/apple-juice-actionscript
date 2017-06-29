using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	class MethodNativeCodeCreator
	{
		private string classname;
		private System.Reflection.MethodInfo method;

		private Type methodAtType;


		/// <summary>
		/// 检查是否索引器的Getter
		/// </summary>
		/// <param name="method"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool CheckIsIndexerGetter(MethodInfo method,Type type,out PropertyInfo propinfo)
		{
			var props = type.GetProperties();

			foreach (var item in props)
			{
				if (Equals(method, item.GetGetMethod()))
				{
					if (item.GetIndexParameters().Length > 0)
					{
						propinfo = item;
						return true;
					}
				}
			}
			propinfo = null;
			return false;
		}
		/// <summary>
		/// 检查是否属性的Getter
		/// </summary>
		/// <param name="method"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool CheckIsGetter(MethodInfo method, Type type, out PropertyInfo propinfo)
		{
			var props = type.GetProperties();

			foreach (var item in props)
			{
				if (Equals(method, item.GetGetMethod()))
				{
					if (item.GetIndexParameters().Length == 0)
					{
						propinfo = item;
						return true;
					}
				}
			}
			propinfo = null;
			return false;
		}

		/// <summary>
		/// 检查是否索引器的Setter
		/// </summary>
		/// <param name="method"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool CheckIsIndexerSetter(MethodInfo method, Type type, out PropertyInfo propinfo)
		{
			var props = type.GetProperties();

			foreach (var item in props)
			{
				if (Equals(method, item.GetSetMethod()))
				{
					if (item.GetIndexParameters().Length > 0)
					{
						propinfo = item;
						return true;
					}
				}
			}
			propinfo = null;
			return false;
		}

		/// <summary>
		/// 检查是否属性的Setter
		/// </summary>
		/// <param name="method"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool CheckIsSetter(MethodInfo method, Type type, out PropertyInfo propinfo)
		{
			var props = type.GetProperties();

			foreach (var item in props)
			{
				if (Equals(method, item.GetSetMethod()))
				{
					if (item.GetIndexParameters().Length == 0)
					{
						propinfo = item;
						return true;
					}
				}
			}
			propinfo = null;
			return false;
		}



		public static string GetAS3RuntimeTypeString(Type type)
		{
			if (type.Equals(typeof(void)))
			{
				return "RunTimeDataType.fun_void";
			}
			else if (
				type.Equals(typeof(double))
				||
				type.Equals(typeof(float))
				)
			{
				//ASBinCode.RunTimeDataType.rt_number
				return "RunTimeDataType.rt_number";
			}
			else if (
				type.Equals(typeof(UInt16))
				||
				type.Equals(typeof(UInt32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_uint
				return "RunTimeDataType.rt_uint";
			}
			else if (
				type.Equals(typeof(Int16))
				||
				type.Equals(typeof(Int32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_int;
				return "RunTimeDataType.rt_int";
			}
			else if (
				type.Equals(typeof(Boolean))
				)
			{
				//ASBinCode.RunTimeDataType.rt_boolean;
				return "RunTimeDataType.rt_boolean";
			}
			else if (
				type.Equals(typeof(string))
				)
			{
				return "RunTimeDataType.rt_string";
			}

			return "RunTimeDataType.rt_void";
		}

		public static ASBinCode.RunTimeDataType GetAS3Runtimetype(Type type)
		{
			if (type.Equals(typeof(void)))
			{
				return ASBinCode.RunTimeDataType.fun_void;
			}
			else if (
				type.Equals(typeof(double))
				||
				type.Equals(typeof(float))
				)
			{
				//ASBinCode.RunTimeDataType.rt_number
				return ASBinCode.RunTimeDataType.rt_number;
			}
			else if (
				type.Equals(typeof(UInt16))
				||
				type.Equals(typeof(UInt32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_uint
				return ASBinCode.RunTimeDataType.rt_uint;
			}
			else if (
				type.Equals(typeof(Int16))
				||
				type.Equals(typeof(Int32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_int;
				return ASBinCode.RunTimeDataType.rt_int;
			}
			else if (
				type.Equals(typeof(Boolean))
				)
			{
				//ASBinCode.RunTimeDataType.rt_boolean;
				return ASBinCode.RunTimeDataType.rt_boolean;
			}
			else if (
				type.Equals(typeof(string))
				)
			{
				return ASBinCode.RunTimeDataType.rt_string;
			}

			return ASBinCode.RunTimeDataType._OBJECT;
		}

		public MethodNativeCodeCreator(string classname,System.Reflection.MethodInfo method,Type methodAtType)
		{
			this.classname = classname;
			this.method = method;
			this.methodAtType = methodAtType;
		}


		private string GetLoadArgementString(ParameterInfo paramenter)
		{
			var rttype = GetAS3Runtimetype(paramenter.ParameterType);

			if (rttype > ASBinCode.RunTimeDataType.unknown)
			{
				string loadargement = Properties.Resources.LoadArgement;

				loadargement = loadargement.Replace("{argindex}", paramenter.Position.ToString());

				return loadargement;
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_int)
			{
				return string.Format("\t\t\t\t\tint arg{0} = TypeConverter.ConvertToInt(argements[{0}], stackframe, token);", paramenter.Position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_uint)
			{
				return string.Format("\t\t\t\t\tuint arg{0} = TypeConverter.ConvertToUInt(argements[{0}], stackframe, token);", paramenter.Position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_number)
			{
				return string.Format("\t\t\t\t\tdouble arg{0} = TypeConverter.ConvertToNumber(argements[{0}]);", paramenter.Position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_boolean)
			{
				return string.Format("\t\t\t\t\tbool arg{0} = TypeConverter.ConvertToBoolean(argements[{0}], stackframe, token).value;", paramenter.Position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_string)
			{
				return string.Format("\t\t\t\t\tstring arg{0} = TypeConverter.ConvertToString(argements[{0}], stackframe, token);", paramenter.Position);
			}
			
			return "代码生成错误，不能转换参数类型";
		}


		private string GetInvokeMethodString(string storeresult,ParameterInfo[] paras)
		{
			PropertyInfo pinfo;
			if (CheckIsGetter(method, methodAtType, out pinfo))
			{
				//***访问器Getter
				storeresult += "." + pinfo.Name + "\n";
				return storeresult;
			}
			else if (CheckIsIndexerGetter(method, methodAtType, out pinfo))
			{
				//***索引器***

				storeresult += "[" + string.Format("({0})arg{1}", paras[0].ParameterType.FullName, 0) + "]\n";
				return storeresult;
			}
			else if (CheckIsSetter(method, methodAtType, out pinfo))
			{
				//***访问器Setter
				storeresult += "." + pinfo.Name + " = " + string.Format("({0})arg{1}", paras[0].ParameterType.FullName, 0) + "\n";
				return storeresult;
			}
			else if (CheckIsIndexerSetter(method, methodAtType, out pinfo))
			{
				//***索引器***

				storeresult += "[" + string.Format("({0})arg{1}", paras[0].ParameterType.FullName, 0) + "] = "
					+ string.Format("({0})arg{1}", paras[1].ParameterType.FullName, 1) + "\n";
				;
				return storeresult;
			}

			

			storeresult += ".";
			storeresult += method.Name;
			storeresult += "(";

			for (int i = 0; i < paras.Length; i++)
			{
				storeresult += string.Format("({0})arg{1}", paras[i].ParameterType.FullName, i);
				if (i < paras.Length - 1)
				{
					storeresult += ",";
				}
			}

			storeresult += ")\n";

			return storeresult;
		}


		public string GetCode()
		{
			if (!method.IsStatic)
			{
				var paras = method.GetParameters();
				string funccode = Properties.Resources.MethodFunc;

				funccode = funccode.Replace("[classname]", classname);
				funccode = funccode.Replace("[paracount]", paras.Length.ToString());

				string pushparas = string.Empty;

				for (int i = 0; i < paras.Length; i++)
				{
					var para = paras[i];

					pushparas = pushparas + "\t\t\t\t" + string.Format("para.Add({0});\n", GetAS3RuntimeTypeString(para.ParameterType));

				}

				funccode = funccode.Replace("[pushparas]", pushparas);
				funccode = funccode.Replace("[returntype]", GetAS3RuntimeTypeString(method.ReturnType));
				funccode = funccode.Replace("[thisObjtype]", methodAtType.FullName);

				string loadargs = string.Empty;

				for (int i = 0; i < paras.Length; i++)
				{
					var argement = paras[i];

					loadargs = loadargs + GetLoadArgementString(argement);
					loadargs = loadargs + "\n";


				}

				funccode = funccode.Replace("[loadargement]", loadargs);

				var rettype = GetAS3Runtimetype(method.ReturnType);
				if (rettype == ASBinCode.RunTimeDataType.fun_void)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t;\n";
					storeresult = storeresult + "\t\t\t\t\treturnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_int)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "int _result_ = (int)(_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t)\n";
					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_uint)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "uint _result_ = (uint)(_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

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
					storeresult = GetInvokeMethodString(storeresult, paras);

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
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t)\n";
					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_boolean)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "bool _result_ = _this";
					storeresult = GetInvokeMethodString(storeresult, paras);

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
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "object _result_ = _this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\tstackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else
				{
					funccode = funccode.Replace("[storeresult]", "代码生成错误，不能转换返回类型");
				}

				return funccode;
			}
			else
			{
				return "静态方法，还没做模板";
			}
		}

	}
}
