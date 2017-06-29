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



		public abstract void Create();

	}
}
