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

		public string BuildIngType
		{
			get
			{
				return type.FullName;
			}
		}


		public CreatorBase(Type type, string as3apidocpath, string csharpnativecodepath)
		{
			this.type = type;
			this.as3apidocpath = as3apidocpath;
			this.csharpnativecodepath = csharpnativecodepath;
		}


		public void MakeCreator(Type type,Dictionary<Type,CreatorBase> typeCreators)
		{
			if (type.IsInterface)
			{
				if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
				{
					typeCreators.Add(type, null); typeCreators[type] = new InterfaceCreator(type, as3apidocpath, csharpnativecodepath, typeCreators);
				}
			}
			else if (type.IsEnum)
			{
				if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
				{
					typeCreators.Add(type, null); typeCreators[type] = new EnumCreator(type, as3apidocpath, csharpnativecodepath);
				}
			}
			else if (type.IsArray)
			{
				var elementtype = type.GetElementType();
				if (elementtype != null)
				{
					MakeCreator(elementtype, typeCreators);
				}
			}
			else if (type.IsClass || type.IsValueType)
			{
				if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
				{
					typeCreators.Add(type, null); typeCreators[type] = new ClassCreator(type, as3apidocpath, csharpnativecodepath, typeCreators);
				}
			}
		}

		
		/// <summary>
		/// 返回如果在成员中涉及这种类型是否要跳过
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsSkipType(Type type)
		{
			if (type == null)
				return true;

			if (type.IsByRef)
				return true;

			if (type.IsNotPublic)
			{
				return true;
			}

			if (type.IsGenericType)
			{
				return true;
			}

			if (type.IsArray)
			{
				return IsSkipType(type.GetElementType());
			}

			if (Equals(type, typeof(Type)))
			{
				return true;
			}

			if (Equals(type, typeof(Delegate)))
			{
				return true;
			}

			if (Equals(type, typeof(MulticastDelegate)))
			{
				return true;
			}

			if (type.BaseType != null)
			{
				return IsSkipType(type.BaseType);
			}

			return false;
		}

		public static List<string> SkipCreateTypes = new List<string>();
		/// <summary>
		/// 是否已经手工绑定。
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsSkipCreator(Type type)
		{
			if (type == null)
				return true;

			if (type.IsArray)
			{
				return IsSkipCreator(type.GetElementType());
			}

			ASBinCode.RunTimeDataType rttype = MethodNativeCodeCreator.GetAS3Runtimetype(type);
			if (rttype < ASBinCode.RunTimeDataType._OBJECT)
			{
				return true;
			}

			string fn = type.FullName;

			if (fn == "System.ValueType")
			{
				return true;
			}

			if (SkipCreateTypes.Contains(fn))
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

		protected static string GetMethodName(string dotName, System.Reflection.MethodInfo method, Type methodAtType, Dictionary<string,object> staticusenames,Dictionary<string,object> usenames )
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



			string v= dotName.Substring(0, 1).ToLower() + dotName.Substring(1) + ext;

			if (method.IsStatic)
			{
				if (staticusenames != null)
				{
					while (staticusenames.ContainsKey(v))
					{
						v = v + "_";
					}
				}
			}
			else
			{
				if (usenames != null)
				{
					while (usenames.ContainsKey(v))
					{
						v = v + "_";
					}
				}
			}
			return v;

		}

		public abstract void Create();




		protected void GenAS3FileHead(StringBuilder as3sb)
		{
			as3sb.AppendLine("package " + GetPackageName(type));
			as3sb.AppendLine("{");
		}

		protected void EndAS3File(StringBuilder as3sb)
		{
			as3sb.AppendLine("}");
		}

	}
}
