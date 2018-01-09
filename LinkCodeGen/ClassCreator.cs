using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	class ClassCreator:CreatorBase
	{
		public ClassCreator(Type classtype, string as3apidocpath, string csharpnativecodepath,
			Dictionary<Type, CreatorBase> typeCreators
			):base(classtype,as3apidocpath,csharpnativecodepath)
        {
			if (!classtype.IsClass && !classtype.IsValueType)
			{
				throw new ArgumentException("类型不是类或结构体");
			}

			if (classtype.IsGenericType)
			{
				throw new ArgumentException("不支持泛型接口");
			}

			if (IsSkipCreator(classtype))
			{
				throw new ArgumentException("类型已配置为跳过");
			}


			name = GetAS3ClassOrInterfaceName(classtype);
			//***分析实现的接口***
			var impls = classtype.GetInterfaces();

			implements = new List<Type>();

			foreach (var intf in impls)
			{
				if (intf.IsGenericType)
				{
					continue;
				}

				implements.Add(intf);

				if (IsSkipCreator(intf))
				{
					continue;
				}

				if (!typeCreators.ContainsKey(intf))
				{
					typeCreators.Add(intf, null);typeCreators[intf]= new InterfaceCreator(intf, as3apidocpath, csharpnativecodepath, typeCreators);
				}

				

			}
			//***链接基类***
			if (classtype.BaseType != null )
			{
				if (!classtype.BaseType.IsGenericType)
				{
					super = classtype.BaseType;

					if (!IsSkipCreator(classtype.BaseType))
					{
						if (!typeCreators.ContainsKey(classtype.BaseType))
						{
							typeCreators.Add(classtype.BaseType, null); typeCreators[classtype.BaseType] = new ClassCreator(classtype.BaseType, as3apidocpath, csharpnativecodepath, typeCreators);
						}
					}
					
				}
			}

		    toimplmethods = new List<methodimplinterface>();
			foreach (var _interface in implements)
			{
				if (type.BaseType != null)
				{
					var baseimpls = type.BaseType.GetInterfaces();
					bool isbaseimpl = false;
					foreach (var bi in baseimpls)
					{
						if (bi.Equals(_interface))
						{
							isbaseimpl = true;
							break;
						}
					}
					if (isbaseimpl)
					{
						continue;
					}

				}


				
				
				if (typeCreators.ContainsKey(_interface))
				{
					var map = type.GetInterfaceMap(_interface);
					foreach (var m in map.TargetMethods)
					{
						if (!InterfaceCreator.isMethodSkip(m))
						{
							methodimplinterface mi = new methodimplinterface();
							mi.method = m;
							mi._interface = _interface;
							toimplmethods.Add(mi);
							
						}
					}
				}
			}


			//****分析方法***

			methodlist = new List<System.Reflection.MethodInfo>();
			opoverrides = new List<System.Reflection.MethodInfo>();

			foreach (var method in type.GetMethods())
			{
				if (!method.DeclaringType.Equals(type)
					)
				{
					continue;
				}

				if (!method.IsPublic)
				{
					continue;
				}

				if (!method.Equals(method.GetBaseDefinition())) //override的，跳过
				{
					continue;
				}

				if (method.IsGenericMethod)
				{
					continue;
				}

				if (method.ReturnType.IsGenericType)
				{
					continue;
				}


				var rt = MethodNativeCodeCreator.GetAS3Runtimetype(method.ReturnType);
				if (rt > ASBinCode.RunTimeDataType.unknown)
				{
					if (IsSkipType(method.ReturnType))
					{
						Console.WriteLine(method.ToString() + "返回类型被配置为需要跳过");
						continue;
					}

					MakeCreator(method.ReturnType, typeCreators);
				}
				bool parachecked = true;
				var paras = method.GetParameters();
				foreach (var p in paras)
				{
					if (p.IsOut)
					{
						Console.WriteLine(method.ToString() + "参数" + p.Position + " " + p + "为out,跳过");
						parachecked = false;
						break;
					}
					if (p.ParameterType.IsByRef)
					{
						Console.WriteLine(method.ToString() + "参数" + p.Position + " " + p + "为byref,跳过");
						parachecked = false;
						break;
					}
					if (p.ParameterType.IsGenericType)
					{
						Console.WriteLine(method.ToString() + "参数" + p.Position + " " + p + "为泛型,跳过");
						parachecked = false;
						break;
					}

					if (p.IsOptional)
					{
						if (p.RawDefaultValue != null)
						{
							var rrt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
							if (rrt > ASBinCode.RunTimeDataType.unknown)
							{
								Console.WriteLine(method.ToString() + "参数" + p.Position + " " + p + "为可选，并且默认值不是基本类型");
								parachecked = false;
								break;
							}
						}
					}

					if (IsSkipType(p.ParameterType))
					{
						Console.WriteLine(method.ToString() + "参数" + p.Position + " " + p + "被配置为需要跳过");
						parachecked = false;
						break;
					}



					var pt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
					if (pt > ASBinCode.RunTimeDataType.unknown && !IsSkipType(p.ParameterType))
					{
						MakeCreator(p.ParameterType, typeCreators);
					}

				}

				if (parachecked)
				{
					if (method.IsSpecialName && method.Name.StartsWith("op_") && method.IsStatic) //操作符重载
					{
						opoverrides.Add(method);
					}
					else
					{
						methodlist.Add(method);
					}
				}
			}

			constructorlist = new List<System.Reflection.ConstructorInfo>();
			var ctors = type.GetConstructors();
			foreach (var ctor in ctors)
			{
				if (!ctor.DeclaringType.Equals(type))
				{
					continue;
				}

				if (ctor.IsGenericMethod)
				{
					continue;
				}

				if (!ctor.IsPublic)
				{
					continue;
				}

				bool parachecked = true;
				var paras = ctor.GetParameters();
				foreach (var p in paras)
				{
					if (p.IsOut)
					{
						Console.WriteLine(ctor.ToString() + "参数" + p.Position + " " + p + "为out,跳过");
						parachecked = false;
						break;
					}
					if (p.ParameterType.IsByRef)
					{
						Console.WriteLine(ctor.ToString() + "参数" + p.Position + " " + p + "为byref,跳过");
						parachecked = false;
						break;
					}
					if (p.ParameterType.IsGenericType)
					{
						Console.WriteLine(ctor.ToString() + "参数" + p.Position + " " + p + "为泛型,跳过");
						parachecked = false;
						break;
					}

					if (p.IsOptional)
					{
						if (p.RawDefaultValue != null)
						{
							var rrt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
							if (rrt > ASBinCode.RunTimeDataType.unknown)
							{
								Console.WriteLine(ctor.ToString() + "参数" + p.Position + " " + p + "为可选，并且默认值不是基本类型");
								parachecked = false;
								break;
							}
						}
					}

					if (IsSkipType(p.ParameterType))
					{
						Console.WriteLine(ctor.ToString() + "参数" + p.Position + " " + p + "被配置为需要跳过");
						parachecked = false;
						break;
					}

					var pt = MethodNativeCodeCreator.GetAS3Runtimetype(p.ParameterType);
					if (pt > ASBinCode.RunTimeDataType.unknown && !IsSkipType(p.ParameterType))
					{
						MakeCreator(p.ParameterType, typeCreators);
					}

				}

				if (parachecked)
				{
					constructorlist.Add(ctor);
				}
			}


			fieldlist = new List<System.Reflection.FieldInfo>();
			foreach (var field in type.GetFields())
			{
				if (!field.DeclaringType.Equals(type))
				{
					continue;
				}

				if (!field.IsPublic)
				{
					continue;
				}

				if (IsSkipType(field.FieldType))
				{
					Console.WriteLine(field.ToString() + " 字段被配置为需要跳过");
					continue;
				}

				fieldlist.Add(field);
			}
			
			
		}

		class methodimplinterface
		{
			public System.Reflection.MethodInfo method;
			public Type _interface;
		}

		List<methodimplinterface> toimplmethods;
		

		List<System.Reflection.MethodInfo> methodlist;
		List<System.Reflection.MethodInfo> opoverrides;


		List<System.Reflection.ConstructorInfo> constructorlist;
		List<System.Reflection.FieldInfo> fieldlist;
		List<Type> implements;

		private System.Reflection.MethodInfo maptointerfacemethod(System.Reflection.MethodInfo method)
		{
			var impls = type.GetInterfaces();
			foreach (var item in impls)
			{
				if (item.IsGenericType)
				{
					continue;
				}

				var map = type.GetInterfaceMap(item);

				for (int i = 0; i < map.TargetMethods.Length; i++)
				{
					if (map.TargetMethods[i].Equals(method))
					{
						return map.InterfaceMethods[i];
					}
				}
				

			}


			return null;
		}

		private Type super;
		public override void Create()
		{
			Dictionary<Type, string> typeimports = new Dictionary<Type, string>();


			StringBuilder nativefunc = new StringBuilder();
			GenNativeFuncImport(nativefunc);
			GenNativeFuncNameSpaceAndClass(nativefunc);

			BeginRegFunction(nativefunc);

			List<string> regfunctions = new List<string>();
			List<string> nativefuncClasses = new List<string>();


			StringBuilder as3api = new StringBuilder();
			GenAS3FileHead(as3api);


			as3api.AppendLine("@imports");
			as3api.AppendLine();

			if (type.IsValueType)
			{
				as3api.AppendLine("\t[struct]");
			}

			if (constructorlist.Count == 0 || type.IsAbstract)
			{
				as3api.AppendLine("\t[no_constructor]");
			}

			as3api.Append("\t[link_system]");
			as3api.AppendLine();
			if (type.IsValueType)
			{
				as3api.AppendFormat("\tpublic final class {0}", name);
			}
			else
			{
				as3api.AppendFormat("\tpublic class {0}", name);
			}

			if (super != null && !type.IsValueType)
			{
				as3api.AppendFormat(" extends {0}", GetAS3TypeString(super, typeimports));
			}
			else
			{
				as3api.AppendFormat(" extends {0}", GetAS3TypeString(typeof(object), typeimports));
			}

			if (implements.Count > 0)
			{
				as3api.Append(" implements ");
				as3api.Append(GetAS3TypeString(implements[0], typeimports));

				for (int i = 1; i < implements.Count; i++)
				{
					as3api.Append(",");
					as3api.Append(GetAS3TypeString(implements[i], typeimports));
				}
			}


			as3api.AppendLine();
			as3api.AppendLine("\t{");


			Dictionary<string, object> dictUseNames=new Dictionary<string, object>();
			Dictionary<string, object> dictStaticUseNames = new Dictionary<string, object>();

			//***加入creator***
			{
				//[creator];
				//[native, _system_ArrayList_creator_]
				//private static function _creator(type:Class):*;

				regfunctions.Add(
				"\t\t\tbin.regNativeFunction(LinkSystem_Buildin.getCreator(\"" + GetCreatorNativeFuncName(type) + "\", default(" + type.FullName + ")));");


				as3api.Append("\t\t");
				as3api.AppendLine("[creator];");

				string nativecreatorname = GetCreatorNativeFuncName(type);

				as3api.Append("\t\t");
				as3api.AppendLine( string.Format( "[native,{0}];",nativecreatorname ));

				as3api.Append("\t\t");
				as3api.AppendLine("private static function _creator(type:Class):*;");

				dictUseNames.Add("_creator", null);
			}

			//***加入构造函数***
			{
				if (constructorlist.Count > 0)
				{
					as3api.AppendLine();
					as3api.AppendLine("\t\t//*********构造函数*******");


					for (int i = 1; i < constructorlist.Count; i++)
					{
						//[native, _system_collections_ArrayList_static_createInstance]
						//public static function createInstance(c:ICollection):ArrayList;
						string createinstancename = GetNativeFunctionPart1(type) + "_constructor" + "".PadRight(i, '_');
						as3api.Append("\t\t");
						as3api.AppendLine(string.Format("[native,{0}];", createinstancename));

						as3api.Append("\t\t");
						as3api.Append("public static function constructor" + "".PadRight(i, '_'));

						appendFunctionParameters(as3api, constructorlist[i], typeimports, name);
						dictStaticUseNames.Add(createinstancename, null);



						//***编写本地方法***
						regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", createinstancename));
						nativefuncClasses.Add(new CtorNativeCodeCreator(createinstancename,constructorlist[i],type).GetCode());


					}

					{
						//[native, _system_ArrayList_ctor_]
						//public function ArrayList();

						string ctorname = GetCtorNativeFuncName(type);

						as3api.Append("\t\t");
						as3api.AppendLine(string.Format("[native,{0}];", ctorname));
						as3api.Append("\t\t");
						as3api.Append("public function " + name);

						appendFunctionParameters(as3api, constructorlist[0], typeimports, null);

						//***编写本地方法***
						regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", ctorname));
						nativefuncClasses.Add(new CtorNativeCodeCreator(ctorname, constructorlist[0], type).GetCode());


					}


				}
			}

			{
				//**字段***
				if (fieldlist.Count > 0)
				{
					as3api.AppendLine();
					as3api.AppendLine("\t\t//*********字段*******");

					foreach (var field in fieldlist)
					{
						//[native, _system_Byte_MaxValue_getter]
						//public static const MaxValue:Byte;

						string fieldname = field.Name;


						as3api.Append("\t\t");
						if (field.IsInitOnly || (field.IsLiteral && !field.IsInitOnly))
						{
							as3api.AppendFormat("[native, {0}];",GetNativeFunctionPart1(this.type)+"_"+fieldname+"_getter");
							as3api.AppendLine();
						}
						else
						{
							as3api.AppendFormat("[native, {0},{1}];", 
								GetNativeFunctionPart1(this.type) + "_" + fieldname + "_getter",
								GetNativeFunctionPart1(this.type) + "_" + fieldname + "_setter");
							as3api.AppendLine();
						}

						//***注册读写器***
						{
							if (field.IsStatic)
							{
								string gettername = GetNativeFunctionPart1(this.type) + "_" + fieldname + "_getter";
								regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", gettername));
								nativefuncClasses.Add(new StaticFieldGetterNativeCodeCreator(gettername, this.type, field).GetCode());

								if (!(field.IsInitOnly || (field.IsLiteral && !field.IsInitOnly)))
								{
									//***注册赋值***
									string funname = GetNativeFunctionPart1(this.type) + "_" + fieldname + "_setter";
									regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", funname));
									nativefuncClasses.Add(new StaticFieldSetterNativeCodeCreator(funname, this.type, field).GetCode());

								}
							}
							else
							{
								string gettername = GetNativeFunctionPart1(this.type) + "_" + fieldname + "_getter";
								regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", gettername));
								nativefuncClasses.Add(new FieldGetterNativeCodeCreator(gettername, this.type, field).GetCode());

								if (!(field.IsInitOnly || (field.IsLiteral && !field.IsInitOnly)))
								{
									//***注册赋值***
									string funname = GetNativeFunctionPart1(this.type) + "_" + fieldname + "_setter";
									regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", funname));
									nativefuncClasses.Add(new FieldSetterNativeCodeCreator(funname, this.type, field).GetCode());

								}

							}
						}



						as3api.Append("\t\t");
						as3api.Append("public ");

						
						if (field.IsStatic)
						{
							as3api.Append("static ");
						}

						if (field.IsInitOnly || (field.IsLiteral && !field.IsInitOnly))
						{
							as3api.Append("const ");
						}
						else
						{
							as3api.Append("var ");
						}
						as3api.Append(field.Name);

						string type = GetAS3TypeString(field.FieldType, typeimports);

						as3api.Append(":");
						as3api.Append(type);
						as3api.Append(";");
						as3api.AppendLine();

						if (field.IsStatic)
						{
							dictStaticUseNames.Add(field.Name, null);
						}
						else
						{
							dictUseNames.Add(field.Name, null);
						}
					}
				}

			}


			bool existsindexgetter=false;
			bool existsindexsetter=false;

			bool existsstaticindexgetter = false;
			bool existsstaticindexsetter = false;

			if (methodlist.Count > 0)
			{
				as3api.AppendLine();
				as3api.AppendLine("\t\t//*********公共方法*******");				
			}

			foreach (var method in methodlist)
			{
				for (int i = 0; i < toimplmethods.Count; i++)
				{
					if (toimplmethods[i].method == method)
					{
						toimplmethods.RemoveAt(i);
						i--;
					}
				}
				
				string returntype = GetAS3TypeString(method.ReturnType, typeimports);

				var mapinterface = maptointerfacemethod(method);
				if (mapinterface != null)
				{
					System.Reflection.PropertyInfo pinfo;
					if (MethodNativeCodeCreator.CheckIsIndexerGetter(mapinterface, mapinterface.DeclaringType, out pinfo) && !existsindexgetter)
					{
						existsindexgetter = true;

						//****索引器****
						as3api.Append("\t\t");
						as3api.AppendLine("[get_this_item];");

						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}];", InterfaceCreator.GetMethodNativeFunctionName(mapinterface, mapinterface.DeclaringType,null,null));
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function getThisItem");
						
						dictUseNames.Add("getThisItem", null);
					}
					else if (MethodNativeCodeCreator.CheckIsGetter(mapinterface, mapinterface.DeclaringType, out pinfo))
					{
						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}]", InterfaceCreator.GetMethodNativeFunctionName(mapinterface, mapinterface.DeclaringType, null, null));
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function get ");

						var mname = GetMethodName(pinfo.Name, method, type,dictStaticUseNames,dictUseNames);
						as3api.Append(mname);
						dictUseNames.Add("get " + mname, null);
					}
					else if (MethodNativeCodeCreator.CheckIsIndexerSetter(mapinterface, mapinterface.DeclaringType, out pinfo) && !existsindexsetter)
					{
						existsindexsetter = true;
						//****索引器****
						as3api.Append("\t\t");
						as3api.AppendLine("[set_this_item];");

						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}];", InterfaceCreator.GetMethodNativeFunctionName(mapinterface, mapinterface.DeclaringType, null, null));
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function setThisItem");

						dictUseNames.Add("setThisItem", null);



					}
					else if (MethodNativeCodeCreator.CheckIsSetter(mapinterface, mapinterface.DeclaringType, out pinfo))
					{
						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}]", InterfaceCreator.GetMethodNativeFunctionName(mapinterface, mapinterface.DeclaringType, null, null));
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function set ");


						var mname = GetMethodName(pinfo.Name, method, type, dictStaticUseNames, dictUseNames);
						as3api.Append(mname);
						dictUseNames.Add("set " + mname, null);

					}
					else
					{
						as3api.AppendLine(string.Format("\t\t[native,{0}];", InterfaceCreator.GetMethodNativeFunctionName(mapinterface, mapinterface.DeclaringType, null, null)));
						as3api.Append("\t\t");
						as3api.Append("public ");
						if (method.IsStatic)
						{
							as3api.Append("static ");
						}

						as3api.Append("function ");


						var mname = GetMethodName(method.Name, method, type, dictStaticUseNames, dictUseNames);
						as3api.Append(mname);
						dictUseNames.Add(mname, null);
					}
				}
				else
				{
					if (method.IsHideBySig && method.DeclaringType == type)
					{
						if (type.BaseType.GetMethod(method.Name) != null)
						{
							dictUseNames.Add(GetMethodName(method.Name, method, type, dictStaticUseNames, dictUseNames), null);
						}
					}

					string nativefunctionname= InterfaceCreator.GetMethodNativeFunctionName(method, type, dictStaticUseNames, dictUseNames);
					System.Reflection.PropertyInfo pinfo;
					if (MethodNativeCodeCreator.CheckIsIndexerGetter(method, type, out pinfo)  && ( ( method.IsStatic && !existsstaticindexgetter ) || (!method.IsStatic && !existsindexgetter) ) )
					{
						//****索引器****
						as3api.Append("\t\t");
						as3api.AppendLine("[get_this_item];");

						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}];",nativefunctionname );
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function getThisItem");


						if (method.IsStatic)
						{
							existsstaticindexgetter = true;
							dictStaticUseNames.Add("getThisItem", null);
						}
						else
						{
							existsindexgetter=true;
							dictUseNames.Add("getThisItem", null);
						}
					}
					else if (MethodNativeCodeCreator.CheckIsGetter(method, type, out pinfo))
					{
						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}]", nativefunctionname);
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function get ");


						var mname = GetMethodName(pinfo.Name, method, type, dictStaticUseNames, dictUseNames);
						as3api.Append(mname);

						if (method.IsStatic)
						{
							dictStaticUseNames.Add("get " + mname, null);
						}
						else
						{
							dictUseNames.Add("get " + mname, null);
						}
					}
					else if (MethodNativeCodeCreator.CheckIsIndexerSetter(method, type, out pinfo) && ((method.IsStatic && !existsstaticindexsetter) || (!method.IsStatic && !existsindexsetter)))
					{
						//****索引器****
						as3api.Append("\t\t");
						as3api.AppendLine("[set_this_item];");

						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}];", nativefunctionname);
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function setThisItem");

						if (method.IsStatic)
						{
							existsstaticindexsetter = true;
							dictStaticUseNames.Add("setThisItem", null);
						}
						else
						{
							existsindexsetter = true;
							dictUseNames.Add("setThisItem", null);
						}
					}
					else if (MethodNativeCodeCreator.CheckIsSetter(method, type, out pinfo))
					{
						as3api.Append("\t\t");
						as3api.AppendFormat("[native,{0}]", nativefunctionname);
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.Append("function set ");

						var mname = GetMethodName(pinfo.Name, method, type, dictStaticUseNames, dictUseNames);
						as3api.Append(mname);

						if (method.IsStatic)
						{
							dictStaticUseNames.Add("set " + mname, null);
						}
						else
						{
							dictUseNames.Add("set " + mname, null);
						}
					}
					else
					{
						as3api.AppendLine(string.Format("\t\t[native,{0}];", nativefunctionname));
						as3api.Append("\t\t");
						as3api.Append("public ");
						if (method.IsStatic)
						{
							as3api.Append("static ");
						}

						as3api.Append("function ");

						

						var mname = GetMethodName(method.Name, method, type, dictStaticUseNames, dictUseNames);
						as3api.Append(mname);

						if (method.IsStatic)
						{
							dictStaticUseNames.Add(mname, null);
						}
						else
						{
							dictUseNames.Add(mname, null);
						}
					}


					//***编写方法的本地代码***
					regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", nativefunctionname));

					if (method.IsStatic)
					{
						StaticMethodNativeCodeCreator mc = new StaticMethodNativeCodeCreator(nativefunctionname, method, type);
						nativefuncClasses.Add(mc.GetCode());
					}
					else
					{
						MethodNativeCodeCreator mc = new MethodNativeCodeCreator(nativefunctionname, method, type);
						nativefuncClasses.Add(mc.GetCode());
					}
				}

				appendFunctionParameters(as3api, method, typeimports, returntype);

				

			}

			if (opoverrides.Count > 0)
			{
				as3api.AppendLine();
				as3api.AppendLine("\t\t//*****操作重载*****");

				foreach (var method in opoverrides)
				{
					string returntype = GetAS3TypeString(method.ReturnType, typeimports);

					string nativefunname = InterfaceCreator.GetMethodNativeFunctionName(method, type, dictStaticUseNames, dictUseNames);

					as3api.AppendLine(string.Format("\t\t[native,{0}];", nativefunname));
					as3api.Append("\t\t");
					as3api.Append("public ");
					
					as3api.Append("static ");
					
					as3api.Append("function ");

					var mname = GetMethodName(method.Name, method, type, dictStaticUseNames, dictUseNames);
					as3api.Append(mname);

					if (method.IsStatic)
					{
						dictStaticUseNames.Add(mname, null);
					}
					else
					{
						dictUseNames.Add(mname, null);
					}


					appendFunctionParameters(as3api, method, typeimports, returntype);

					//***编写方法的本地代码***
					regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", nativefunname));

					StaticMethodNativeCodeCreator mc = new StaticMethodNativeCodeCreator(nativefunname, method, type);
					nativefuncClasses.Add(mc.GetCode());

				}
			}

			if (toimplmethods.Count > 0)
			{
				as3api.AppendLine();
				as3api.AppendLine("\t\t//*****显式接口实现*****");
			}
			foreach (var item in toimplmethods) 
			{
				//添加需隐式接口实现的代码

				string returntype = GetAS3TypeString(item.method.ReturnType, typeimports);

				as3api.AppendLine(string.Format("\t\t[native,{0}];", InterfaceCreator.GetMethodNativeFunctionName(item.method, item._interface,null,null)));
				as3api.Append("\t\t");
				as3api.Append("private ");
				
				as3api.Append("function ");
				as3api.Append(GetMethodName(item.method.Name, item.method, type, dictStaticUseNames, dictUseNames));
				appendFunctionParameters(as3api, item.method, typeimports, returntype);

			}



			as3api.AppendLine();
			as3api.AppendLine("\t}");

			EndAS3File(as3api);

			string imports = string.Empty;
			foreach (var item in typeimports.Values)
			{
				imports += "\t" + item + "\n";
			}
			as3api.Replace("@imports", imports);

			for (int i = 0; i < regfunctions.Count; i++)
			{
				nativefunc.AppendLine(regfunctions[i]);
			}

			EndRegFunction(nativefunc);

			for (int i = 0; i < nativefuncClasses.Count; i++)
			{
				nativefunc.AppendLine(nativefuncClasses[i]);
			}

			EndNativeFuncClass(nativefunc);
			//Console.WriteLine(as3api.ToString());

			string as3file = "as3api/" + GetPackageName(type).Replace(".", "/") + "/" + name + ".as";
			string nativefunfile = "buildins/" + GetNativeFunctionClassName(type) + ".cs";

			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(as3file));
			System.IO.File.WriteAllText(as3file, as3api.ToString());

			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(nativefunfile));
			System.IO.File.WriteAllText(nativefunfile, nativefunc.ToString());

		}


		private void appendFunctionParameters(StringBuilder as3api,System.Reflection.MethodBase method,Dictionary<Type,String> typeimports,string returntype)
		{
			as3api.Append("(");

			var paras = method.GetParameters();
			for (int i = 0; i < paras.Length; i++)
			{
				var para = paras[i];
				as3api.Append(para.Name);
				as3api.Append(":");
				as3api.Append(GetAS3TypeString(para.ParameterType, typeimports));

				if (para.IsOptional)
				{
					as3api.Append("=");

					if (para.RawDefaultValue != null)
					{
						var rt = MethodNativeCodeCreator.GetAS3Runtimetype(para.ParameterType);
						if (rt == ASBinCode.RunTimeDataType.rt_string)
						{
							string str = para.RawDefaultValue.ToString();
							str = str.Replace("\\", "\\\\");
							str = str.Replace("\"", "\\\"");

							as3api.Append("\"" + str + "\"");
						}
						else
						{
							as3api.Append(para.RawDefaultValue.ToString());
						}
					}
					else
					{
						as3api.Append("null");
					}
				}

				if (i < paras.Length - 1)
				{
					as3api.Append(",");
				}

			}

			as3api.Append(")");
			if (!string.IsNullOrEmpty(returntype))
			{
				as3api.Append(":");
				as3api.Append(returntype);
			}
			as3api.AppendLine(";");
			as3api.AppendLine();
		}












		private void GenNativeFuncImport(StringBuilder nativesb)
		{
			nativesb.AppendLine("using ASBinCode;");
			nativesb.AppendLine("using ASBinCode.rtti;");
			nativesb.AppendLine("using ASRuntime;");
			nativesb.AppendLine("using ASRuntime.nativefuncs;");
			nativesb.AppendLine("using System;");
			nativesb.AppendLine("using System.Collections;");
			nativesb.AppendLine("using System.Collections.Generic;");
		}

		private void GenNativeFuncNameSpaceAndClass(StringBuilder nativesb)
		{
			nativesb.AppendLine("namespace ASCAutoGen.regNativeFunctions");
			nativesb.AppendLine("{");
			nativesb.Append("\t");
			nativesb.Append("class ");
			nativesb.AppendLine(GetNativeFunctionClassName(type));
			nativesb.Append("\t");
			nativesb.AppendLine("{");
		}

		private void BeginRegFunction(StringBuilder nativesb)
		{
			nativesb.Append("\t\t");
			nativesb.AppendLine("public static void regNativeFunctions(CSWC bin)");
			nativesb.Append("\t\t");
			nativesb.AppendLine("{");
		}
		private void EndRegFunction(StringBuilder nativesb)
		{
			nativesb.Append("\t\t");
			nativesb.AppendLine("}");
			nativesb.AppendLine();

		}
		private void EndNativeFuncClass(StringBuilder nativesb)
		{
			nativesb.Append("\t");
			nativesb.AppendLine("}");
			nativesb.AppendLine("}");
		}

	}
}
