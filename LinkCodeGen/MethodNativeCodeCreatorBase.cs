using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	abstract class MethodNativeCodeCreatorBase : NativeCodeCreatorBase
	{
		internal class myparainfo : ParameterInfo
		{
			ParameterInfo bp;
			int pos;
			public myparainfo(ParameterInfo bp, int pos)
			{
				this.bp = bp;
				this.pos = pos;
			}

			public override int Position => pos;
			public override string Name => bp.Name;
			public override ParameterAttributes Attributes => bp.Attributes;
			public override object DefaultValue => bp.DefaultValue;
			public override MemberInfo Member => bp.Member;
			public override Type ParameterType => bp.ParameterType;
			public override object RawDefaultValue => bp.RawDefaultValue;

		}

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

		public static string GetOperatorOverrideSummary(MethodInfo method,Type type, ParameterInfo[] paras,out string operatorcode)
		{
			if (method.IsSpecialName && method.Name.StartsWith("op_"))
			{
				string op = method.Name.Substring(3);
				switch (op)
				{
					case "Equality":
						operatorcode = "==";
						//sb.AppendLine(string.Format("{0} == {1};", param[0].Name, param[1].Name));
						return string.Format("计算 {0} == {1}", paras[0].Name, paras[1].Name);
						
					case "Inequality":
						operatorcode = "!=";
						//sb.AppendLine(string.Format("{0} != {1};", param[0].Name, param[1].Name));
						return string.Format("计算 {0} != {1}", paras[0].Name, paras[1].Name);
					case "Addition":
						//sb.AppendLine(string.Format("{0} + {1};", param[0].Name, param[1].Name));
						operatorcode = "+";
						return string.Format("计算 {0} + {1}", paras[0].Name, paras[1].Name);
					case "Subtraction":
						//sb.AppendLine(string.Format("{0} - {1};", param[0].Name, param[1].Name));
						operatorcode = "-";
						return string.Format("计算 {0} - {1}", paras[0].Name, paras[1].Name);
					case "Multiply":
						//sb.AppendLine(string.Format("{0} * {1};", param[0].Name, param[1].Name));
						operatorcode = "*";
						return string.Format("计算 {0} * {1}", paras[0].Name, paras[1].Name);
					case "Division":
						//sb.AppendLine(string.Format("{0} / {1};", param[0].Name, param[1].Name));
						operatorcode = "/";
						return string.Format("计算 {0} / {1}", paras[0].Name, paras[1].Name);
					case "Modulus":
						//sb.AppendLine(string.Format("{0} % {1};", param[0].Name, param[1].Name));
						operatorcode = "%";
						return string.Format("计算 {0} % {1}", paras[0].Name, paras[1].Name);
					case "GreaterThan":
						//sb.AppendLine(string.Format("{0} > {1};", param[0].Name, param[1].Name));
						operatorcode = ">";
						return string.Format("计算 {0} > {1}", paras[0].Name, paras[1].Name);
					case "GreaterThanOrEqual":
						//sb.AppendLine(string.Format("{0} >= {1};", param[0].Name, param[1].Name));
						operatorcode = ">=";
						return string.Format("计算 {0} >= {1}", paras[0].Name, paras[1].Name);
					case "LessThan":
						//sb.AppendLine(string.Format("{0} < {1};", param[0].Name, param[1].Name));
						operatorcode = "<";
						return string.Format("计算 {0} < {1}", paras[0].Name, paras[1].Name);
					case "LessThanOrEqual":
						//sb.AppendLine(string.Format("{0} <= {1};", param[0].Name, param[1].Name));
						operatorcode = "<=";
						return string.Format("计算 {0} <= {1}", paras[0].Name, paras[1].Name);
					case "LeftShift":
						operatorcode = "<<";
						return string.Format("计算 {0} << {1}", paras[0].Name, paras[1].Name);
					case "RightShift":
						operatorcode = ">>";
						return string.Format("计算 {0} >> {1}", paras[0].Name, paras[1].Name);
					case "UnaryPlus":
						operatorcode = "+";
						return string.Format("计算 +{0}", paras[0].Name);
					case "UnaryNegation":
						operatorcode = "-";
						return string.Format("计算 -{0}", paras[0].Name);
					case "Increment":
						operatorcode = "++";
						return string.Format("计算 ++{0}", paras[0].Name);
					case "Decrement":
						operatorcode = "--";
						return string.Format("计算 --{0}", paras[0].Name);
					case "OnesComplement":
						operatorcode = "~";
						return string.Format("计算 ~{0}", paras[0].Name);
					case "ExclusiveOr":
						operatorcode = "^";
						return string.Format("计算 {0} ^ {1}", paras[0].Name, paras[1].Name);
					case "BitwiseOr":
						operatorcode = "|";
						return string.Format("计算 {0} | {1}", paras[0].Name, paras[1].Name);
					case "BitwiseAnd":
						operatorcode = "&";
						return string.Format("计算 {0} & {1}", paras[0].Name, paras[1].Name);
					case "Implicit":
						operatorcode = "Implicit";

						if (method.ReturnType == type && paras[0].ParameterType != type)
						{
							return string.Format("Implicit From {0} ", GetTypeFullName( paras[0].ParameterType));
						}
						else if (method.ReturnType != type && paras[0].ParameterType == type)
						{
							return string.Format("Implicit To {0} ", GetTypeFullName( method.ReturnType));
						}
						else
						{
							return string.Format("Implicit Convert {0} to {1}", GetTypeFullName(paras[0].ParameterType), GetTypeFullName( method.ReturnType));

						}
					case "Explicit":
						
						operatorcode = "Explicit";
						if (method.ReturnType == type && paras[0].ParameterType != type)
						{
							return string.Format("Explicit From {0} ", GetTypeFullName( paras[0].ParameterType));
						}
						else if (method.ReturnType != type && paras[0].ParameterType == type)
						{
							return string.Format("Explicit To {0} ",GetTypeFullName( method.ReturnType));
						}
						else
						{
							return string.Format("Explicit Convert {0} to {1}",GetTypeFullName( paras[0].ParameterType), GetTypeFullName( method.ReturnType));

						}

					default:
						throw new NotImplementedException(op);
				}
			}
			else
			{
				operatorcode = null;
				return "不是操作符重载";
			}
		}


		protected string GetInvokeMethodString(string storeresult, ParameterInfo[] paras)
		{
			if (method.IsSpecialName && method.Name.StartsWith("op_"))
			{
				storeresult = storeresult.Substring(0, storeresult.LastIndexOf( GetTypeFullName( methodAtType)));

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

							storeresult += string.Format("({0})arg0\n", GetTypeFullName(method.ReturnType));

						}
						break;
					default:
						throw new NotImplementedException(op);
				}

				return storeresult;
			}
			else if (method.IsSpecialName && method.Name.StartsWith("add_") && paras.Length == 1 && CreatorBase.IsDelegate(paras[0].ParameterType))
			{
				string eventname = method.Name.Substring(4);

				storeresult += string.Format(".{0} += arg0",eventname);
				return storeresult;
			}
			else if (method.IsSpecialName && method.Name.StartsWith("remove_") && paras.Length == 1 && CreatorBase.IsDelegate(paras[0].ParameterType))
			{
				string eventname = method.Name.Substring(7);
				storeresult += string.Format(".{0} -= arg0",eventname);
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

				storeresult += "[" + string.Format("({0})arg{1}",GetTypeFullName( paras[0].ParameterType), 0) + "]\n";
				return storeresult;
			}
			else if (CheckIsSetter(method, methodAtType, out pinfo))
			{
				//***访问器Setter
				storeresult += "." + pinfo.Name + " = " + string.Format("({0})arg{1}", GetTypeFullName( paras[0].ParameterType), 0) + "\n";
				return storeresult;
			}
			else if (CheckIsIndexerSetter(method, methodAtType, out pinfo))
			{
				//***索引器***

				storeresult += "[" + string.Format("({0})arg{1}",GetTypeFullName( paras[1].ParameterType), 1) + "] = "
					+ string.Format("({0})arg{1}", GetTypeFullName( paras[0].ParameterType), 0) + "\n";
				;
				return storeresult;
			}



			storeresult += ".";
			storeresult += method.Name;
			storeresult += "(";

			for (int i = 0; i < paras.Length; i++)
			{
				if (paras[i].IsOut)
				{
					storeresult += string.Format( CreatorBase.GetOutKeyWord(paras[i],method) + " arg{0}", i);
				}
				else if (paras[i].ParameterType.IsByRef)
				{
					storeresult += string.Format("ref arg{0}", i);
				}
				else
				{
					storeresult += string.Format("({0})arg{1}", GetTypeFullName(paras[i].ParameterType), i);
				}
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
