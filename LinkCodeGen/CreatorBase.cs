using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	abstract class CreatorBase
	{
		protected Type type;
		protected string as3apidocpath;
		protected string csharpnativecodepath;

		public  bool isfinal;
		public  bool isStruct;

		public  string name;

		public CreatorBase(Type type, string as3apidocpath, string csharpnativecodepath)
		{
			this.type = type;
			this.as3apidocpath = as3apidocpath;
			this.csharpnativecodepath = csharpnativecodepath;
		}

		/// <summary>
		/// 返回如果在成员中涉及这种类型是否要跳过
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsSkipType(Type type)
		{
			if (Equals(type, typeof(Type)))
			{
				return true;
			}



			return false;
		}



		public string GetAS3ClassOrInterfaceName(Type type)
		{
			if (type.Equals(typeof(Object)))
			{
				return "_Object_";
			}

			if (type.IsArray)
			{
				return "_Array_";
			}

			if (type.Equals(typeof(System.Collections.IEnumerable)))
			{
				return "_IEnumerable_";
			}

			if (type.Equals(typeof(System.Collections.IEnumerator)))
			{
				return "_IEnumerator_";
			}

			return type.Name;
		}




		/// <summary>
		/// 获取转换成as3 package
		/// </summary>
		/// <param name="csharptype"></param>
		/// <returns></returns>
		public static string GetPackageName(Type csharptype)
		{
			string ns = csharptype.Namespace;
			return ns.ToLower();
		}

		public static string GetNativeFunctionPart1(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + csharptype.Name;
		}

		public static string GetCreatorNativeFuncName(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + csharptype.Name + "_creator";
		}

		public static string GetCtorNativeFuncName(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + csharptype.Name + "_ctor";
		}

		public static string GetNativeFunctionClassName(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + csharptype.Name + "_buildin";
		}

		public string GetAS3TypeString(Type type, Dictionary<Type, string> typeimports)
		{
			ASBinCode.RunTimeDataType rttype = MethodNativeCodeCreator.GetAS3Runtimetype(type);
			if (rttype == ASBinCode.RunTimeDataType.fun_void)
			{
				return "void";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_number)
			{
				return "Number";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_int)
			{
				return "int";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_uint)
			{
				return "uint";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_boolean)
			{
				return "Boolean";
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_string)
			{
				return "String";
			}


			if (this.type.Namespace != type.Namespace)
			{
				if (!typeimports.ContainsKey(type))
				{
					typeimports.Add(type, "import " + type.Namespace.ToLower() + "." + GetAS3ClassOrInterfaceName(type) + ";");
				}
			}

			return GetAS3ClassOrInterfaceName(type);
		}

		protected static string GetMethodName(string dotName, System.Reflection.MethodInfo method, Type methodAtType)
		{

			var members = methodAtType.GetMember(dotName);

			string ext = string.Empty;

			for (int i = 0; i < members.Length; i++)
			{
				System.Reflection.MethodBase m = members[i] as System.Reflection.MethodBase;
				if (m == null)
				{
					continue;
				}
				if (m.IsStatic != method.IsStatic)
				{
					continue;
				}

				if (members[i].Equals(method))
				{
					break;
				}
				else
				{
					ext = ext + "_";
				}
			}



			return dotName.Substring(0, 1).ToLower() + dotName.Substring(1) + ext;



		}

		public abstract void Create();

	}
}
