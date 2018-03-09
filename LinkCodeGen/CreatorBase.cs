using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	abstract class CreatorBase
	{
		public static Dictionary<string, object> as3keywords;//= { "import","extend", "dynamic" };
		static CreatorBase()
		{
			as3keywords = new Dictionary<string, object>();
			as3keywords.Add("import", null);
			as3keywords.Add("extend", null);
			as3keywords.Add("dynamic", null);
			as3keywords.Add("default", null);
			as3keywords.Add("delete", null);
			as3keywords.Add("use", null);
			as3keywords.Add("continue", null);
			as3keywords.Add("namespace", null);
			as3keywords.Add("interface", null);
			as3keywords.Add("class", null);
			as3keywords.Add("implements", null);
			as3keywords.Add("super", null);
			as3keywords.Add("package", null);
			as3keywords.Add("yield", null);
			as3keywords.Add("break", null);
			as3keywords.Add("for", null);
			as3keywords.Add("in", null);
			as3keywords.Add("loop", null);
			as3keywords.Add("do", null);
			as3keywords.Add("while", null);
			as3keywords.Add("return", null);
			as3keywords.Add("protected", null);
			as3keywords.Add("public", null);
			as3keywords.Add("private", null);
			as3keywords.Add("static", null);
			as3keywords.Add("throw", null);
			as3keywords.Add("try", null);
			as3keywords.Add("catch", null);
			as3keywords.Add("finally", null);
			as3keywords.Add("function", null);
			as3keywords.Add("internal", null);
		}

		/// <summary>
		/// 检查是否标记为已过时
		/// </summary>
		/// <param name="member"></param>
		/// <returns></returns>
		public static bool IsObsolete(System.Reflection.MemberInfo member,System.Type definetype)
		{
			var method = member as MethodInfo;
			if (method != null)
			{
				PropertyInfo p;
				if (MethodNativeCodeCreatorBase.CheckIsGetter(method, definetype, out p))
				{
					return IsObsolete(p, definetype);
				}
				if (MethodNativeCodeCreatorBase.CheckIsSetter(method, definetype, out p))
				{
					return IsObsolete(p, definetype);
				}
				if (MethodNativeCodeCreatorBase.CheckIsIndexerGetter(method, definetype, out p))
				{
					return IsObsolete(p, definetype);
				}
				if (MethodNativeCodeCreatorBase.CheckIsIndexerSetter(method, definetype, out p))
				{
					return IsObsolete(p, definetype);
				}
			}

			var c = CustomAttributeData.GetCustomAttributes(member);
			foreach (CustomAttributeData item in c)
			{
				if (item.Constructor.DeclaringType == typeof(System.ObsoleteAttribute))
				{
					return true;
				}
			}
			return false;
			//object[] objs= member.GetCustomAttributes(typeof(System.ObsoleteAttribute),false);

			//if (objs.Length>0)
			//{
			//	return true;
			//}
			//else
			//{
			//	return false;
			//}
		}

		public static String GetOutKeyWord(ParameterInfo parameter,MethodInfo method)
		{
			if (!parameter.ParameterType.IsByRef)
			{
				return "";
			}

			//if (method.Name == "ReadBlock" && method.DeclaringType.FullName == "System.IO.TextReader" && parameter.Name=="buffer")
			//{
			//	return "";
			//}
			//if (method.Name == "Read" && method.DeclaringType.FullName == "System.IO.TextReader" && parameter.Name == "buffer")
			//{
			//	return "";
			//}
			//if (method.Name == "Read" && method.DeclaringType.FullName == "System.IO.Stream" && parameter.Name == "buffer")
			//{
			//	return "";
			//}

			return "out";
		}


		public static bool IsDelegate(Type d)
		{
			if (d.BaseType != typeof(MulticastDelegate))
				return false;

			MethodInfo invoke = d.GetMethod("Invoke");
			if (invoke == null)
				return false;

			return true;
		}

		public static MethodInfo GetDelegateMethodInfo(Type d)
		{
			if (d.BaseType != typeof(MulticastDelegate))
				throw new ApplicationException("Not a delegate.");

			MethodInfo invoke = d.GetMethod("Invoke");
			if (invoke == null)
				throw new ApplicationException("Not a delegate.");
			return invoke;

		}

		public static string GetDelegateSignature(Type d)
		{
			var method = GetDelegateMethodInfo(d);

			string ret = NativeCodeCreatorBase.GetTypeFullName(method.ReturnType) + "(";

			var paras = method.GetParameters();
			for (int i = 0; i < paras.Length; i++)
			{
				ret += NativeCodeCreatorBase.GetTypeFullName(paras[i].ParameterType);

				if (i < paras.Length - 1)
				{
					ret += ",";
				}

			}

			ret = ret + ")";

			return ret;
		}


		protected Type type;
		protected string as3apidocpath;
		protected string csharpnativecodepath;

		public  bool isfinal;
		public  bool isStruct;

		public  string name;

		public Type BuildIngType
		{
			get
			{
				return type;
			}
		}

		public readonly string linkcodenamespace;

		public string LinkCodeNampScapePart
		{
			get
			{
				if (string.IsNullOrEmpty(linkcodenamespace))
					return string.Empty + GetNativeFunctionClassName(type) + ".";
				else
					return linkcodenamespace + "." + GetNativeFunctionClassName(type) + ".";
			}
		}

		public CreatorBase(Type type, string as3apidocpath, string csharpnativecodepath,string linkcodenamespace)
		{
			this.type = type;
			this.as3apidocpath = as3apidocpath;
			this.csharpnativecodepath = csharpnativecodepath;
			this.linkcodenamespace = linkcodenamespace;
		}


		public static void MakeCreator(Type type, Dictionary<Type, CreatorBase> typeCreators, string as3apidocpath,string csharpnativecodepath,string linkcodenamespace)
		{
			if (type.IsInterface)
			{
				if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
				{
					typeCreators.Add(type, null); typeCreators[type] = new InterfaceCreator(type, as3apidocpath, csharpnativecodepath, typeCreators, linkcodenamespace);
				}
			}
			else if (type.IsEnum)
			{
				if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
				{
					typeCreators.Add(type, null); typeCreators[type] = new EnumCreator(type, as3apidocpath, csharpnativecodepath, linkcodenamespace);
				}
			}
			else if (type.IsArray)
			{
				var elementtype = type.GetElementType();
				if (elementtype != null)
				{
					MakeCreator(elementtype, typeCreators,as3apidocpath,csharpnativecodepath,linkcodenamespace);
				}
			}
			else if (type.IsClass || type.IsValueType)
			{
				if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
				{
					typeCreators.Add(type, null); typeCreators[type] = new ClassCreator(type, as3apidocpath, csharpnativecodepath, typeCreators, linkcodenamespace);
				}
			}
		}


		public void MakeCreator(Type type,Dictionary<Type,CreatorBase> typeCreators)
		{
			MakeCreator(type, typeCreators, as3apidocpath, csharpnativecodepath, linkcodenamespace);
			//if (type.IsInterface)
			//{
			//	if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
			//	{
			//		typeCreators.Add(type, null); typeCreators[type] = new InterfaceCreator(type, as3apidocpath, csharpnativecodepath, typeCreators,linkcodenamespace);
			//	}
			//}
			//else if (type.IsEnum)
			//{
			//	if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
			//	{
			//		typeCreators.Add(type, null); typeCreators[type] = new EnumCreator(type, as3apidocpath, csharpnativecodepath,linkcodenamespace);
			//	}
			//}
			//else if (type.IsArray)
			//{
			//	var elementtype = type.GetElementType();
			//	if (elementtype != null)
			//	{
			//		MakeCreator(elementtype, typeCreators);
			//	}
			//}
			//else if (type.IsClass || type.IsValueType)
			//{
			//	if (!typeCreators.ContainsKey(type) && !IsSkipCreator(type))
			//	{
			//		typeCreators.Add(type, null); typeCreators[type] = new ClassCreator(type, as3apidocpath, csharpnativecodepath, typeCreators,linkcodenamespace);
			//	}
			//}
		}
		public static List<string> NoCreateMember = new List<string>();

		public static bool IsSkipMember(MemberInfo memberInfo)
		{
			string key = memberInfo.DeclaringType.FullName + "::"+ memberInfo.ToString();

			if (NoCreateMember.Contains(key))
			{
				return true;
			}
			return false;
		}


		public static List<string> NotCreateNameSpace = new List<string>();

		public static List<string> NotCreateTypes = new List<string>();

		/// <summary>
		/// 返回如果在成员中涉及这种类型是否要跳过
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsSkipType(Type type,bool includeref=false)
		{
			if (type == null)
				return true;
			var c = CustomAttributeData.GetCustomAttributes(type);
			foreach (CustomAttributeData item in c)
			{
				if (item.Constructor.DeclaringType == typeof(System.ObsoleteAttribute))
				{
					return true;
				}
			}

			//object[] objs = type.GetCustomAttributes(typeof(System.ObsoleteAttribute), false);

			//if (objs.Length > 0)
			//{
			//	return true;
			//}
			



			if (Equals(type, typeof(Type))) //Type会转换为Class
			{
				return false;
			}

			if (type.IsCOMObject)
			{
				return true;
			}

			if (Equals(type, typeof(Enum)))
			{
				return true;
			}

			if (type.IsEnum)
			{
				foreach (var item in NotCreateNameSpace)
				{
					if (type.Namespace == item)
						return true;
					if (type.Namespace.StartsWith(item + "."))
						return true;
				}

				if (NotCreateTypes.Contains(type.FullName))
				{
					return true;
				}
				return false;
			}


			if (type.IsByRef)
			{
				if (!includeref)
				{
					return true;
				}
				else if(type.HasElementType)
				{
					return IsSkipType(type.GetElementType());
				}
			}

			if (type.IsNotPublic)
			{
				return true;
			}

			if (type.IsNested)
			{
				if (IsSkipType(type.DeclaringType))
				{
					return true;
				}
			}

			if (type.IsGenericTypeDefinition)
			{
				return true;
			}
			if (type.IsGenericType && type.IsInterface)
			{
				return true;
			}
			

			if (type.IsArray)
			{
				return IsSkipType(type.GetElementType());
			}

			if (type.Namespace == null)
				return true;

			foreach (var item in NotCreateNameSpace)
			{
				if (type.Namespace == item)
					return true;
				if (type.Namespace.StartsWith(item + "."))
					return true;
			}

			if (NotCreateTypes.Contains(type.FullName))
			{
				return true;
			}

			if (IsDelegate(type))
			{

				var m = GetDelegateMethodInfo(type);
				//***如果该委托包含要跳过的类型，则跳过****
				if (m.IsGenericMethod)
				{
					return true;
				}

				if (m.ReturnType.IsGenericTypeDefinition)
				{
					return true;
				}

				var rt = MethodNativeCodeCreator.GetAS3Runtimetype(m.ReturnType);
				if (rt > ASBinCode.RunTimeDataType.unknown)
				{
					if (IsSkipType(m.ReturnType))
					{
						return true;
					}
				}

				var paras = m.GetParameters();
				foreach (var p in paras)
				{
					if (p.IsOut)
					{
						return true;
					}
					if (p.ParameterType.IsByRef)
					{
						return true;
					}
					//if (p.ParameterType.IsGenericType)
					if (p.ParameterType.IsGenericTypeDefinition)
					{
						return true;
					}

					if (p.IsOptional)
					{
						if (p.RawDefaultValue != null)
						{
							var rrt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
							if (rrt > ASBinCode.RunTimeDataType.unknown)
							{
								return true;
							}
						}
					}

					if (IsSkipType(p.ParameterType))
					{
						return true;
					}
				}

				return false;
			}





			if (Equals(type, typeof(TypedReference)))
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

			if (Equals(type, typeof(Type)))
			{
				return true;
			}


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



		public string GetAS3ClassOrInterfaceName(Type type,bool nestedcall=false)
		{
			if (type.Equals(typeof(Object)))
			{
				return "_Object_";
			}

			if (type.FullName == "UnityEngine.Object")
			{
				return "UObject";
			}

			if (type.Equals(typeof(Type)))
			{
				return "Class";
			}

			if (type == typeof(Array))
			{
				return "_Array_";
			}

			if (type.IsArray)
			{
				if (nestedcall)
				{
					return "_ArrayOf_" + GetAS3ClassOrInterfaceName(type.GetElementType(), true);
				}
				else
				{
					return "_Array_";
				}
			}

			if (type.Equals(typeof(System.Collections.IEnumerable)))
			{
				return "_IEnumerable_";
			}

			if (type.Equals(typeof(System.Collections.IEnumerator)))
			{
				return "_IEnumerator_";
			}

			if (type.Equals(typeof(System.Math)))
			{
				return "Math_";
			}

			

			string pre = string.Empty;
			if (type.IsNested)
			{
				pre = GetAS3ClassOrInterfaceName(type.DeclaringType,true) + "_";
			}

			if (type.IsGenericType)
			{
				var defparams = type.GetGenericArguments();

				string ext = string.Empty;
				foreach (var item in defparams)
				{
					ext +="_"+ GetAS3ClassOrInterfaceName(item,true);
				}

				int idx = type.Name.IndexOf("`");
				return pre +  type.Name.Substring(0,idx) + "_Of" + ext;
			}

			return pre+type.Name;
		}




		/// <summary>
		/// 获取转换成as3 package
		/// </summary>
		/// <param name="csharptype"></param>
		/// <returns></returns>
		public static string GetPackageName(Type csharptype)
		{
			string ns = csharptype.Namespace;

			string[] p = ns.Split('.');


			string package = string.Empty;

			for (int i = 0; i < p.Length; i++)
			{
				string s = p[i].ToLower();
				if (as3keywords.ContainsKey(s))
				{
					s = p[i];

					if (as3keywords.ContainsKey(s))
					{
						s = s + "_";
					}
				}

				package += s;

				if (i < p.Length - 1)
				{
					package += ".";
				}
			}


			return package;
		}

		private static string GetSharpTypeName(Type csharptype)
		{
			string pre = string.Empty;
			if (csharptype.IsNested)
			{
				pre = GetSharpTypeName(csharptype.DeclaringType) + "_";
			}

			if (csharptype.IsGenericType)
			{
				var defparams = csharptype.GetGenericArguments();

				string ext = string.Empty;
				foreach (var item in defparams)
				{
					ext += "_" + GetSharpTypeName(item);
				}

				int idx = csharptype.Name.IndexOf("`");
				return pre + csharptype.Name.Substring(0, idx) + "_Of" + ext;
			}
			else if (csharptype.IsArray)
			{
				return pre +  "_ArrayOf" + GetSharpTypeName(csharptype.GetElementType());
			}
			else
			{
				return pre + csharptype.Name;
			}
		}

		public static string GetNativeFunctionPart1(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + GetSharpTypeName(csharptype);
		}

		public static string GetCreatorNativeFuncName(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + GetSharpTypeName(csharptype) + "_creator";
		}

		public static string GetCtorNativeFuncName(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + GetSharpTypeName(csharptype) + "_ctor";
		}

		public static string GetNativeFunctionClassName(Type csharptype)
		{
			return csharptype.Namespace.ToLower().Replace(".", "_") + "_" + GetSharpTypeName(csharptype) + "_buildin";
		}


		private System.Reflection.MethodInfo isMapToInterface(System.Reflection.MethodBase method,System.Type checkinterface)
		{
			
			var intfs= method.DeclaringType.GetInterfaces();
			foreach (var intf in intfs)
			{
				if (intf == checkinterface)
				{
					try
					{
						var map = method.DeclaringType.GetInterfaceMap(checkinterface);

						for (int i = 0; i < map.TargetMethods.Length; i++)
						{
							if (map.TargetMethods[i] == method)
							{
								return map.InterfaceMethods[i];
							}
						}
					}
					catch (ArgumentException)
					{
						continue;
					}

					

					
				}

			}

			
			return null;
		}

		public string GetAS3TypeString(Type type, Dictionary<Type, string> typeimports,Type checktype,System.Reflection.MethodBase checkmethod,System.Reflection.ParameterInfo checkparameter)
		{
			if (type.IsByRef)
			{
				if (!typeimports.ContainsValue("import as3runtime.RefOutStore;"))
				{
					typeimports.Add(typeof(ASRuntime.nativefuncs.linksystem.RefOutStore), "import as3runtime.RefOutStore;");
				}
				return GetAS3TypeString(type.GetElementType(), typeimports, checktype, checkmethod, checkparameter);
			}

			if (checkparameter != null)
			{


				if (checkmethod.DeclaringType == typeof(System.Collections.IList)
					||
					isMapToInterface(checkmethod, typeof(System.Collections.IList)) !=null
					)
				{
					if (isMapToInterface(checkmethod, typeof(System.Collections.IList)) != null)
						checkmethod = isMapToInterface(checkmethod, typeof(System.Collections.IList));


					if (checkmethod.Name == "set_Item" && checkparameter.Name == "value")
					{
						return "*";
					}
					else if (checkmethod.Name == "Add")
					{
						return "*";
					}
					else if (checkmethod.Name == "Contains")
					{
						return "*";
					}
					else if (checkmethod.Name == "IndexOf")
					{
						return "*";
					}
					else if (checkmethod.Name == "Insert" && checkparameter.Name == "value")
					{
						return "*";
					}
					else if (checkmethod.Name == "Remove")
					{
						return "*";
					}
				}

				if (checkmethod.DeclaringType == typeof(System.Collections.IDictionary)
					||
					isMapToInterface(checkmethod,typeof(System.Collections.IDictionary)) !=null
					)
				{
					if (isMapToInterface(checkmethod, typeof(System.Collections.IDictionary)) != null)
						checkmethod = isMapToInterface(checkmethod, typeof(System.Collections.IDictionary));

					if (checkmethod.Name == "get_Item" && checkparameter.Name == "key")
					{
						return "Object";
					}
					else if (checkmethod.Name == "set_Item" && checkparameter.Name == "value")
					{
						return "*";
					}
					else if (checkmethod.Name == "set_Item" && checkparameter.Name == "key")
					{
						return "Object";
					}
					else if (checkmethod.Name == "Add" && checkparameter.Name == "key")
					{
						return "Object";
					}
					else if (checkmethod.Name == "Add" && checkparameter.Name == "value")
					{
						return "*";
					}
					else if (checkmethod.Name == "Contains")
					{
						return "Object";
					}
					else if (checkmethod.Name == "Remove")
					{
						return "Object";
					}
				}

			}


			if (checktype == typeof(System.Collections.IEnumerator))
			{
				if (checkmethod.Name == "System.Collections.IEnumerator.get_Current"
					||
					checkmethod.Name == "get_Current"
					)
				{
					return "*";
				}
			}
			else if (checktype == typeof(System.Collections.IList))
			{
				if (checkmethod.Name == "get_Item"
					||
					checkmethod.Name == "System.Collections.IList.get_Item"
					)
				{
					return "*";
				}
			}
			else if (checktype == typeof(System.Collections.IDictionary))
			{
				if (checkmethod.Name == "get_Item"
					||
					checkmethod.Name == "System.Collections.IDictionary.get_Item"
					)
				{
					return "*";
				}
			}
			else if (checktype == typeof(System.Collections.IDictionaryEnumerator))
			{
				if (checkmethod.Name == "get_Key"
					||
					isMapToInterface(checkmethod, typeof(System.Collections.IDictionaryEnumerator)) != null
					)
				{
					return "Object";
				}
				else if (checkmethod.Name == "get_Value"
					||
					isMapToInterface(checkmethod, typeof(System.Collections.IDictionaryEnumerator)) != null
					)
				{
					return "*";
				}
			}


			if (type.Equals(typeof(ASBinCode.RunTimeValueBase)))
			{
				return "*";
			}

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

			


			do
			{
				if (type == typeof(Type))
				{
					break;
				}

				if (type == typeof(Array) || type.IsArray)
				{
					if (!typeimports.ContainsValue("import system._Array_;"))
					{
						typeimports.Add(typeof(Array), "import system._Array_;");
					}
				}
				else if (this.type.Namespace != type.Namespace)
				{
					if (!typeimports.ContainsKey(type))
					{
						typeimports.Add(type, "import " + type.Namespace.ToLower() + "." + GetAS3ClassOrInterfaceName(type) + ";");
					}
				}


			} while (false);

			
			string name=GetAS3ClassOrInterfaceName(type);

			return name;
		}

		protected static string GetMethodName(string dotName, System.Reflection.MethodInfo method, Type methodAtType, Dictionary<string,object> staticusenames,Dictionary<string,object> usenames )
		{
			if (method.IsSpecialName && method.Name.StartsWith("add_") && method.GetParameters().Length==1 && IsDelegate( method.GetParameters()[0].ParameterType))
			{
				string eventname = method.Name.Substring(4);

				return eventname + "_addEventListener";
			}
			else if (method.IsSpecialName && method.Name.StartsWith("remove_") && method.GetParameters().Length == 1 && IsDelegate(method.GetParameters()[0].ParameterType))
			{
				string eventname = method.Name.Substring(7);

				return eventname + "_removeEventListener";
			}

			
			var members = methodAtType.GetMember(dotName);

			//var props = methodAtType.GetProperties();
			//bool isprop = false;
			//foreach (var item in props)
			//{
			//	if (Equals(method, item.GetGetMethod()))
			//	{
			//		isprop = true;
			//	}
			//	if (Equals(method, item.GetSetMethod()))
			//	{
			//		isprop = true;
			//	}
			//}
			
			//if (!isprop)
			{
				if (Char.IsLower(dotName[0]))
				{
					List<System.Reflection.MemberInfo> temp = new List<System.Reflection.MemberInfo>();

					var ms = methodAtType.GetMember(Char.ToUpper(dotName[0]) + dotName.Substring(1));
					foreach (var item in ms)
					{
						if (item.MemberType != MemberTypes.NestedType)
						{
							temp.Add(item);
						}
					}
					
					if (temp.Count > 0)
					{
						dotName = dotName + "".PadLeft(temp.Count, '_');
					}

				}
			}

			if (methodAtType.IsInterface)
			{
				var extinterface = methodAtType.GetInterfaces();

				List<System.Reflection.MemberInfo> inheritmember = new List<System.Reflection.MemberInfo>();
				foreach (var item in extinterface)
				{
					inheritmember.AddRange(item.GetMember(dotName));
				}

				if (inheritmember.Count > 0)
				{
					inheritmember.AddRange(members);

					members = inheritmember.ToArray();

				}
			}
			else
			{
				var basetype = methodAtType.BaseType;
				List<System.Reflection.MemberInfo> inheritmember = new List<System.Reflection.MemberInfo>();
				if (basetype != null)
				{
					var inherit =basetype.GetMember(dotName);	
					inheritmember.AddRange(inherit);
					basetype = basetype.BaseType;
				}

				if (inheritmember.Count > 0)
				{
					inheritmember.AddRange(members);
					members = inheritmember.ToArray();
				}
			}


			string ext = string.Empty;

			for (int i = 0; i < members.Length; i++)
			{
				var m = members[i];

				if (m is MethodBase)
				{
					if (((MethodBase)m).IsStatic != method.IsStatic)
					{
						continue;
					}
				}
				else if (m is PropertyInfo)
				{
					PropertyInfo p = (PropertyInfo)m;
					if (Equals(p.GetGetMethod(), method))
					{
						break;
					}
					if (Equals(p.GetSetMethod(), method))
					{
						break;
					}
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
			if (dotName.Length > 1)
			{
				if (Char.IsUpper(dotName.Substring(1, 1)[0]))
				{
					v = dotName + ext;
				}
			}



			while (as3keywords.ContainsKey(v))
			{
				v = v + "_";
			}

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

		public abstract string  Create();


		public static void GenNativeFuncImport(StringBuilder nativesb)
		{
			nativesb.AppendLine("using ASBinCode;");
			nativesb.AppendLine("using ASBinCode.rtti;");
			nativesb.AppendLine("using ASRuntime;");
			nativesb.AppendLine("using ASRuntime.nativefuncs;");
			nativesb.AppendLine("using System;");
			nativesb.AppendLine("using System.Collections;");
			nativesb.AppendLine("using System.Collections.Generic;");
		}


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
