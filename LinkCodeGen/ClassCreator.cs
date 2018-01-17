using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	class ClassCreator:CreatorBase
	{
		public ClassCreator(Type classtype, string as3apidocpath, string csharpnativecodepath,
			Dictionary<Type, CreatorBase> typeCreators,
			string linkcodenamespace
			):base(classtype,as3apidocpath,csharpnativecodepath,linkcodenamespace)
        {
			if (!classtype.IsClass && !classtype.IsValueType)
			{
				throw new ArgumentException("类型不是类或结构体");
			}

			//if (classtype.IsGenericType)
			//{
			//	throw new ArgumentException("不支持泛型接口");
			//}

			if (classtype.IsGenericTypeDefinition)
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
				if (IsSkipType(intf))
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
					typeCreators.Add(intf, null);typeCreators[intf]= new InterfaceCreator(intf, as3apidocpath, csharpnativecodepath, typeCreators,linkcodenamespace);
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
							typeCreators.Add(classtype.BaseType, null); typeCreators[classtype.BaseType] = new ClassCreator(classtype.BaseType, as3apidocpath, csharpnativecodepath, typeCreators,linkcodenamespace);
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


				
				
				if (typeCreators.ContainsKey(_interface) || IsSkipCreator(_interface))
				{
					InterfaceMapping map = type.GetInterfaceMap(_interface);

					for (int i = 0; i < map.TargetMethods.Length; i++)
					{
						var m = map.TargetMethods[i];
						if (!InterfaceCreator.isMethodSkip(m))
						{
							methodimplinterface mi = new methodimplinterface();
							mi.method = m;
							mi.interfacemethod = map.InterfaceMethods[i];
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

				//if (method.ReturnType.IsGenericType)
				if(method.ReturnType.IsGenericTypeDefinition)
				{
					continue;
				}

				if (IsObsolete(method))
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
					//if (p.ParameterType.IsGenericType)
					if (p.ParameterType.IsGenericTypeDefinition)
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
					//if (p.ParameterType.IsGenericType)
					if (p.ParameterType.IsGenericTypeDefinition)
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

				if (IsObsolete(field))
				{
					continue;
				}

				MakeCreator(field.FieldType, typeCreators);

				fieldlist.Add(field);
			}
			
			
		}

		class methodimplinterface
		{
			public System.Reflection.MethodInfo method;
			public Type _interface;
			public MethodInfo interfacemethod;
		}

		List<methodimplinterface> toimplmethods;
		

		List<System.Reflection.MethodInfo> methodlist;
		List<System.Reflection.MethodInfo> opoverrides;


		List<System.Reflection.ConstructorInfo> constructorlist;
		List<System.Reflection.FieldInfo> fieldlist;
		List<Type> implements;

		private System.Reflection.MethodInfo maptointerfacemethod(System.Reflection.MethodInfo method)
		{
			var impls = implements; //type.GetInterfaces();
			foreach (var item in impls)
			{
				//if (item.IsGenericType)
				//{
				//	continue;
				//}

				//if (IsSkipType(item))
				//{
				//	continue;
				//}

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
		public override string Create()
		{
			Dictionary<Type, string> typeimports = new Dictionary<Type, string>();


			StringBuilder nativefunc = new StringBuilder();
			//GenNativeFuncImport(nativefunc);
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

			if (!type.IsValueType) //结构体不可能没有构造
			{
				if (constructorlist.Count == 0 || type.IsAbstract)
				{
					as3api.AppendLine("\t[no_constructor]");
				}
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
				as3api.AppendFormat(" extends {0}", GetAS3TypeString(super, typeimports,null,null,null));
			}
			else
			{
				as3api.AppendFormat(" extends {0}", GetAS3TypeString(typeof(object), typeimports,null,null,null));
			}

			if (implements.Count > 0)
			{
				as3api.Append(" implements ");
				as3api.Append(GetAS3TypeString(implements[0], typeimports,null,null,null));

				for (int i = 1; i < implements.Count; i++)
				{
					as3api.Append(",");
					as3api.Append(GetAS3TypeString(implements[i], typeimports,null,null,null));
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
				if (!(type.IsAbstract && type.IsSealed))
				{
					regfunctions.Add(
					"\t\t\tbin.regNativeFunction(LinkSystem_Buildin.getCreator(\"" + GetCreatorNativeFuncName(type) + "\", default(" +NativeCodeCreatorBase.GetTypeFullName( type) + ")));");
				}
				else
				{
					regfunctions.Add(
						"\t\t\tbin.regNativeFunction(LinkSystem_Buildin.getStaticClassCreator(\"" + GetCreatorNativeFuncName(type) + "\", typeof(" + NativeCodeCreatorBase.GetTypeFullName( type) + ")));");
				}

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

						appendFunctionParameters(as3api, constructorlist[i],type, typeimports, name);
						dictStaticUseNames.Add(createinstancename, null);



						//***编写本地方法***
						regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", createinstancename));
						nativefuncClasses.Add(new CreateInstanceNativeCodeCreator(createinstancename, constructorlist[i], type).GetCode());


					}

					{
						//[native, _system_ArrayList_ctor_]
						//public function ArrayList();

						string ctorname = GetCtorNativeFuncName(type);

						as3api.Append("\t\t");
						as3api.AppendLine(string.Format("[native,{0}];", ctorname));
						as3api.Append("\t\t");
						as3api.Append("public function " + name);

						appendFunctionParameters(as3api, constructorlist[0], type,typeimports, null);

						//***编写本地方法***
						regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", ctorname));
						nativefuncClasses.Add(new CtorNativeCodeCreator(ctorname, constructorlist[0], type).GetCode());


					}


				}
				else if (type.IsValueType)
				{
					//编写结构体默认构造函数
					as3api.AppendLine();
					as3api.AppendLine("\t\t//*********构造函数*******");

					string ctorname = GetCtorNativeFuncName(type);


					if (type == typeof(long)) //***long**
					{
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Int64_ctor];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function Int64(v:Number=0);");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[explicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Int64_explicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _explicit_from_value(v:Number):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[implicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Int64_implicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _implicit_from_value(v:Number):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Int64_valueOf];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function valueOf():Number;");
						as3api.AppendLine();
					}
					else if (type == typeof(ulong))
					{
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_UInt64_ctor];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function UInt64(v:Number=0);");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[explicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_UInt64_explicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _explicit_from_value(v:Number):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[implicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_UInt64_implicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _implicit_from_value(v:Number):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_UInt64_valueOf];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function valueOf():Number;");
						as3api.AppendLine();
					}
					else if (type == typeof(char))
					{
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Char_ctor];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function Char(v:int = 0);");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[explicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Char_explicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _explicit_from_value(v:int):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Char_valueOf];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function valueOf():Number;");
						as3api.AppendLine();

					}
					else if (type == typeof(Byte))
					{
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Byte_ctor];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function Byte(v:int = 0);");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[explicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Byte_explicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _explicit_from_value(v:int):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[implicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Byte_implicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _implicit_from_value(v:int):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_Byte_valueOf];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function valueOf():Number;");
						as3api.AppendLine();

					}
					else if (type == typeof(SByte))
					{
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_SByte_ctor];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function SByte(v:int = 0);");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[explicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_SByte_explicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _explicit_from_value(v:int):*;");
						as3api.AppendLine();

						as3api.Append("\t\t");
						as3api.AppendLine("[implicit_from];");
						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_SByte_implicit_from_];");
						as3api.Append("\t\t");
						as3api.AppendLine("private static function _implicit_from_value(v:int):*;");
						as3api.AppendLine();


						as3api.Append("\t\t");
						as3api.AppendLine("[native,_system_SByte_valueOf];");
						as3api.Append("\t\t");
						as3api.AppendLine("public function valueOf():Number;");
						as3api.AppendLine();

					}
					else
					{
						as3api.Append("\t\t");
						as3api.AppendLine(string.Format("[native,{0}];", ctorname));
						as3api.Append("\t\t");
						as3api.Append("public function " + name);

						as3api.Append("();");

						//***编写本地方法***
						regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", ctorname));
						nativefuncClasses.Add(new CtorNativeCodeCreator(ctorname, null, type).GetCode());
					}
				}
				else
				{
					//$$_noctorclass
					as3api.Append("\t\t");
					as3api.AppendLine(string.Format("[native,{0}];", "$$_noctorclass"));
					as3api.Append("\t\t");
					as3api.Append("public function " + name);

					as3api.Append("();");
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

						string type = GetAS3TypeString(field.FieldType, typeimports,null,null,null);

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
						break;
					}
				}
				
				string returntype = GetAS3TypeString(method.ReturnType, typeimports,null,null,null);

				var mapinterface = maptointerfacemethod(method);
				if (mapinterface != null)
				{
					returntype = GetAS3TypeString(method.ReturnType, typeimports, mapinterface.DeclaringType, method,null);

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
						as3api.Append("public ");

						
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
						as3api.Append("public ");
						
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
						try
						{
							if (type.BaseType.GetMethod(method.Name) != null)
							{
								dictUseNames.Add(GetMethodName(method.Name, method, type, dictStaticUseNames, dictUseNames), null);
							}
						}
						catch (System.Reflection.AmbiguousMatchException)
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

						if (method.IsStatic)
						{
							as3api.Append("static ");
						}

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
						as3api.Append("public ");
						if (method.IsStatic)
						{
							as3api.Append("static ");
						}
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
						if (method.IsStatic)
						{
							as3api.Append("static ");
						}
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
						as3api.Append("public ");
						if (method.IsStatic)
						{
							as3api.Append("static ");
						}
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

				appendFunctionParameters(as3api, method,type, typeimports, returntype);

				

			}

			Dictionary<MethodInfo, object[]> implicitfrom = new Dictionary<MethodInfo, object[]>();
			Dictionary<MethodInfo, object[]> implicitto = new Dictionary<MethodInfo, object[]>();
			Dictionary<MethodInfo, object[]> explicitfrom = new Dictionary<MethodInfo, object[]>();
			Dictionary<MethodInfo, object[]> explicitto = new Dictionary<MethodInfo, object[]>();


			if (opoverrides.Count > 0)
			{
				as3api.AppendLine();
				as3api.AppendLine("\t\t//*****操作重载*****");

				foreach (var method in opoverrides)
				{
					string returntype = GetAS3TypeString(method.ReturnType, typeimports,null,null,null);

					string nativefunname = InterfaceCreator.GetMethodNativeFunctionName(method, type, dictStaticUseNames, dictUseNames);

					string opcode;
					string opersummary = MethodNativeCodeCreatorBase.GetOperatorOverrideSummary(method,type, method.GetParameters(), out opcode);
					if (opcode != null)
					{
						string summary = @"/**
		 * "+opersummary+@"
		 */";

						as3api.AppendLine(string.Format("\t\t" + summary));
					}

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


					appendFunctionParameters(as3api, method,type, typeimports, returntype);

					//***编写方法的本地代码***
					regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", nativefunname));

					StaticMethodNativeCodeCreator mc = new StaticMethodNativeCodeCreator(nativefunname, method, type);
					nativefuncClasses.Add(mc.GetCode());


					if (opcode == "+" ||
						opcode == "-" ||
						opcode == "==" ||
						opcode == "!=" ||
						opcode == ">" ||
						opcode == ">=" ||
						opcode == "<" ||
						opcode == "<=" ||
						opcode == "|"
						)
					{
						bool abouttype = false;
						foreach (var item in method.GetParameters())
						{
							if (item.ParameterType.Equals(type))
							{
								abouttype = true;
								break;
							}
						}
						if (method.ReturnType.Equals(type))
						{
							abouttype = true;
						}

						if (abouttype)
						{
							as3api.AppendLine(string.Format("\t\t[operator,\"{0}\"];", opcode));
							as3api.AppendLine(string.Format("\t\t[native,{0}];", nativefunname));
							as3api.Append("\t\t");
							as3api.Append("private ");
							as3api.Append("static ");
							as3api.Append("function ");

							var oname = GetMethodName("_operator_" + method.Name, method, type, dictStaticUseNames, dictUseNames);
							as3api.Append(oname);
							if (method.IsStatic)
							{
								dictStaticUseNames.Add(oname, null);
							}
							else
							{
								dictUseNames.Add(oname, null);
							}

							appendFunctionParameters(as3api, method, type ,typeimports, returntype);

						}

					}
					else if (opcode == "Implicit")
					{
						var param = method.GetParameters();

						if (method.ReturnType == type)
						{
							//implicitfrom;

							var as3type = MethodNativeCodeCreator.GetAS3Runtimetype(param[0].ParameterType);

							if (as3type == ASBinCode.RunTimeDataType.rt_int
								||
								as3type == ASBinCode.RunTimeDataType.rt_number
								||
								as3type == ASBinCode.RunTimeDataType.rt_string
								||
								as3type == ASBinCode.RunTimeDataType.rt_uint
								||
								as3type == ASBinCode.RunTimeDataType.rt_boolean
								)
							{
								implicitfrom.Add(method, new object[] {as3type, nativefunname });
							}
						}
						else if (param[0].ParameterType == type)
						{
							//implicitto;
							var as3type = MethodNativeCodeCreator.GetAS3Runtimetype(method.ReturnType);
							if (as3type == ASBinCode.RunTimeDataType.rt_int
								||
								as3type == ASBinCode.RunTimeDataType.rt_number
								||
								as3type == ASBinCode.RunTimeDataType.rt_string
								||
								as3type == ASBinCode.RunTimeDataType.rt_uint
								||
								as3type == ASBinCode.RunTimeDataType.rt_boolean
								)
							{
								implicitto.Add(method, new object[] { as3type, nativefunname });
							}
						}
					}
					else if (opcode == "Explicit")
					{
						var param = method.GetParameters();

						if (method.ReturnType == type)
						{
							//implicitfrom;

							var as3type = MethodNativeCodeCreator.GetAS3Runtimetype(param[0].ParameterType);

							if (as3type == ASBinCode.RunTimeDataType.rt_int
								||
								as3type == ASBinCode.RunTimeDataType.rt_number
								||
								as3type == ASBinCode.RunTimeDataType.rt_string
								||
								as3type == ASBinCode.RunTimeDataType.rt_uint
								||
								as3type == ASBinCode.RunTimeDataType.rt_boolean
								)
							{
								explicitfrom.Add(method, new object[] { as3type, nativefunname });
							}
						}
						else if (param[0].ParameterType == type)
						{
							//implicitto;
							var as3type = MethodNativeCodeCreator.GetAS3Runtimetype(method.ReturnType);
							if (as3type == ASBinCode.RunTimeDataType.rt_int
								||
								as3type == ASBinCode.RunTimeDataType.rt_number
								||
								as3type == ASBinCode.RunTimeDataType.rt_string
								||
								as3type == ASBinCode.RunTimeDataType.rt_uint
								||
								as3type == ASBinCode.RunTimeDataType.rt_boolean
								)
							{
								explicitto.Add(method, new object[] { as3type, nativefunname });
							}
						}
					}

				}
			}

			Dictionary<ASBinCode.RunTimeDataType, int> as3typesort = new Dictionary<ASBinCode.RunTimeDataType, int>();
			as3typesort.Add(ASBinCode.RunTimeDataType.rt_boolean, 0);
			as3typesort.Add(ASBinCode.RunTimeDataType.rt_string, 1);
			as3typesort.Add(ASBinCode.RunTimeDataType.rt_uint, 2);
			as3typesort.Add(ASBinCode.RunTimeDataType.rt_int, 3);
			as3typesort.Add(ASBinCode.RunTimeDataType.rt_number, 4);

			Dictionary<ASBinCode.RunTimeDataType, string> as3typename = new Dictionary<ASBinCode.RunTimeDataType, string>();
			as3typename.Add(ASBinCode.RunTimeDataType.rt_boolean, "Boolean");
			as3typename.Add(ASBinCode.RunTimeDataType.rt_string, "String");
			as3typename.Add(ASBinCode.RunTimeDataType.rt_uint, "uint");
			as3typename.Add(ASBinCode.RunTimeDataType.rt_int, "int");
			as3typename.Add(ASBinCode.RunTimeDataType.rt_number, "Number");

			if (implicitfrom.Count > 0)
			{
				List<KeyValuePair<MethodInfo, object[]>> list = new List<KeyValuePair<MethodInfo, object[]>>();
				foreach (var item in implicitfrom)
				{
					list.Add(item);
				}
				list.Sort((v1, v2) => 
				{
					return
						as3typesort[(ASBinCode.RunTimeDataType)v2.Value[0]] -
						as3typesort[(ASBinCode.RunTimeDataType)v1.Value[0]];
				});


				var kv = list[0];

				var as3argtype=as3typename[ (ASBinCode.RunTimeDataType)kv.Value[0]];

				var funname = GetMethodName( "_implicit_from_value",kv.Key,type,dictStaticUseNames,dictUseNames);

				as3api.AppendLine(string.Format("\t\t[implicit_from];"));
				as3api.AppendLine(string.Format("\t\t[native,{0}];", (string)kv.Value[1]));
				as3api.AppendLine(string.Format("\t\tprivate static function {0}(v:{1}):*;",funname,as3argtype));
				as3api.AppendLine();
			}

			if (explicitfrom.Count > 0)
			{
				List<KeyValuePair<MethodInfo, object[]>> list = new List<KeyValuePair<MethodInfo, object[]>>();
				foreach (var item in explicitfrom)
				{
					list.Add(item);
				}
				list.Sort((v1, v2) =>
				{
					return
						as3typesort[(ASBinCode.RunTimeDataType)v2.Value[0]] -
						as3typesort[(ASBinCode.RunTimeDataType)v1.Value[0]];
				});

				var kv = list[0];

				var as3argtype = as3typename[(ASBinCode.RunTimeDataType)kv.Value[0]];

				var funname = GetMethodName("_explicit_from_value", kv.Key, type, dictStaticUseNames, dictUseNames);

				as3api.AppendLine(string.Format("\t\t[explicit_from];"));
				as3api.AppendLine(string.Format("\t\t[native,{0}];", (string)kv.Value[1]));
				as3api.AppendLine(string.Format("\t\tprivate static function {0}(v:{1}):*;", funname, as3argtype));
				as3api.AppendLine();
			}

			bool hasvalueof = false;
			if (implicitto.Count > 0)
			{
				List<KeyValuePair<MethodInfo, object[]>> list = new List<KeyValuePair<MethodInfo, object[]>>();
				foreach (var item in implicitto)
				{
					list.Add(item);
				}
				list.Sort((v1, v2) =>
				{
					return
						as3typesort[(ASBinCode.RunTimeDataType)v2.Value[0]] -
						as3typesort[(ASBinCode.RunTimeDataType)v1.Value[0]];
				});

				hasvalueof = true;
				var kv = list[0];
				var as3argtype = as3typename[(ASBinCode.RunTimeDataType)kv.Value[0]];

				
				var funname = GetMethodName("valueOf", kv.Key, type, dictUseNames, dictUseNames);

				string nativefunname = GetNativeFunctionPart1(type) + "_" + funname ;


				as3api.AppendLine(string.Format("\t\t[native,{0}];", (string)kv.Value[1]));
				as3api.AppendLine(string.Format("\t\tpublic function {0}():{1};", funname, as3argtype));
				as3api.AppendLine();


				//***编写方法的本地代码***
				regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", nativefunname));

				ValueOfNativeCodeCreator mc = new ValueOfNativeCodeCreator(nativefunname,type, kv.Key);
				nativefuncClasses.Add(mc.GetCode());

			}
			if (!hasvalueof)
			{
				if (explicitto.Count > 0)
				{
					List<KeyValuePair<MethodInfo, object[]>> list = new List<KeyValuePair<MethodInfo, object[]>>();
					foreach (var item in explicitto)
					{
						list.Add(item);
					}
					list.Sort((v1, v2) =>
					{
						return
							as3typesort[(ASBinCode.RunTimeDataType)v2.Value[0]] -
							as3typesort[(ASBinCode.RunTimeDataType)v1.Value[0]];
					});

					hasvalueof = true;
					var kv = list[0];
					var as3argtype = as3typename[(ASBinCode.RunTimeDataType)kv.Value[0]];

					

					var funname = GetMethodName("valueOf", kv.Key, type, dictUseNames, dictUseNames);

					string nativefunname = GetNativeFunctionPart1(type) + "_" + funname;

					as3api.AppendLine(string.Format("\t\t[native,{0}];",nativefunname));
					as3api.AppendLine(string.Format("\t\tpublic function {0}():{1};", funname, as3argtype));
					as3api.AppendLine();

					//***编写方法的本地代码***
					regfunctions.Add(string.Format("\t\t\tbin.regNativeFunction(new {0}());", nativefunname));

					ValueOfNativeCodeCreator mc = new ValueOfNativeCodeCreator(nativefunname, type, kv.Key);
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
				//添加需显式接口实现的代码

				string returntype = GetAS3TypeString(item.method.ReturnType, typeimports,item._interface,item.method,null);

				



				as3api.AppendLine(string.Format("\t\t[native,{0}];", InterfaceCreator.GetMethodNativeFunctionName(item.interfacemethod, item._interface,null,null)));
				as3api.Append("\t\t");
				as3api.Append("private ");

				as3api.Append("function ");

				string mn = GetMethodName( GetNativeFunctionPart1(item._interface) + "_" +
					item.interfacemethod.Name
					,
					item.method, type, dictStaticUseNames, dictUseNames
					);



				dictUseNames.Add(mn, null);

				as3api.Append( mn);
				appendFunctionParameters(as3api, item.interfacemethod, item._interface, typeimports, returntype);

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


			StringBuilder usingcode = new StringBuilder();
			GenNativeFuncImport(usingcode);
			System.IO.File.WriteAllText(nativefunfile, usingcode.ToString()+ nativefunc.ToString());

			return nativefunc.ToString();

		}


		private void appendFunctionParameters(StringBuilder as3api,System.Reflection.MethodBase method, Type checktype ,Dictionary<Type,String> typeimports,string returntype)
		{
			as3api.Append("(");

			var paras = method.GetParameters();
			if (method is MethodInfo)
			{
				PropertyInfo propertyInfo;
				if (MethodNativeCodeCreator.CheckIsIndexerSetter((MethodInfo)method, checktype, out propertyInfo))
				{
					var temp = paras[0];
					paras[0] = paras[1];
					paras[1] = temp;
				}
			}

			for (int i = 0; i < paras.Length; i++)
			{
				var para = paras[i];
				as3api.Append(para.Name);
				as3api.Append(":");

				as3api.Append(GetAS3TypeString(para.ParameterType, typeimports,null,method,para ));

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







		private void GenNativeFuncNameSpaceAndClass(StringBuilder nativesb)
		{
			nativesb.AppendLine("namespace " + linkcodenamespace);
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
