using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	abstract class MethodNativeCodeCreatorBase : NativeCodeCreatorBase
	{
		protected string classname;
		protected System.Reflection.MethodInfo method;

		protected Type methodAtType;


		/// <summary>
		/// 检查是否索引器的Getter
		/// </summary>
		/// <param name="method"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool CheckIsIndexerGetter(MethodInfo method, Type type, out PropertyInfo propinfo)
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

		protected string GetInvokeMethodString(string storeresult, ParameterInfo[] paras)
		{
			if (method.IsSpecialName && method.Name.StartsWith("op_"))
			{
				storeresult = storeresult.Substring(0, storeresult.LastIndexOf(methodAtType.FullName));

				string op = method.Name.Substring(3);

				switch (op)
				{
					case "Equality":
						//sb.AppendLine(string.Format("{0} == {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 == arg1");
						break;
					case "Inequality":
						//sb.AppendLine(string.Format("{0} != {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 != arg1");
						break;
					case "Addition":
						//sb.AppendLine(string.Format("{0} + {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 + arg1");
						break;
					case "Subtraction":
						//sb.AppendLine(string.Format("{0} - {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 - arg1");
						break;
					case "Multiply":
						//sb.AppendLine(string.Format("{0} * {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 * arg1");
						break;
					case "Division":
						//sb.AppendLine(string.Format("{0} / {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 / arg1");
						break;
					case "Modulus":
						//sb.AppendLine(string.Format("{0} % {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 % arg1");
						break;
					case "GreaterThan":
						//sb.AppendLine(string.Format("{0} > {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 > arg1");
						break;
					case "GreaterThanOrEqual":
						//sb.AppendLine(string.Format("{0} >= {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 >= arg1");
						break;
					case "LessThan":
						//sb.AppendLine(string.Format("{0} < {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 < arg1");
						break;
					case "LessThanOrEqual":
						//sb.AppendLine(string.Format("{0} <= {1};", param[0].Name, param[1].Name));
						storeresult += string.Format("arg0 <= arg1");
						break;
					case "LeftShift":
						storeresult += string.Format("arg0 << arg1");
						break;
					case "RightShift":
						storeresult += string.Format("arg0 >> arg1");
						break;
					case "UnaryPlus":
						storeresult += string.Format("+arg0");
						break;
					case "UnaryNegation":
						//sb.AppendLine(string.Format("-{0};", param[0].Name));
						storeresult += string.Format("-arg0");
						break;
					case "Increment":
						storeresult += string.Format("++arg0");
						break;
					case "Decrement":
						storeresult += string.Format("--arg0");
						break;
					case "OnesComplement":
						storeresult += string.Format("~arg0");
						break;
					case "ExclusiveOr":
						storeresult += string.Format("arg0 ^ arg1");
						break;
					case "BitwiseOr":
						storeresult += string.Format("arg0 | arg1");
						break;
					case "BitwiseAnd":
						storeresult += string.Format("arg0 & arg1");
						break;
					case "Implicit":
					case "Explicit":
						{
							//string tmp, clsName;
							//bool isByRef;
							//method.ReturnType.GetClassName(out tmp, out clsName, out isByRef);
							//sb.AppendLine(string.Format("({1}){0};", param[0].Name, clsName));

							storeresult += string.Format("({0})arg0\n", method.ReturnType.FullName );
							
						}
						break;
					default:
						throw new NotImplementedException(op);
				}

				return storeresult;
			}


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
	}
}
