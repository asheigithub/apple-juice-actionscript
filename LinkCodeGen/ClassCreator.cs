using System;
using System.Collections.Generic;
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

			name = GetAS3ClassOrInterfaceName(classtype);
			//***分析实现的接口***
			var impls = classtype.GetInterfaces();

			foreach (var intf in impls)
			{
				if (intf.IsGenericType)
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
					if (!typeCreators.ContainsKey(classtype.BaseType))
					{
						typeCreators.Add(classtype.BaseType, null); typeCreators[classtype.BaseType] = new ClassCreator(classtype.BaseType, as3apidocpath, csharpnativecodepath, typeCreators);
					}
				}
			}



		}

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

		public override void Create()
		{
			Console.WriteLine(type);

			var methods = type.GetMethods();
			foreach (var method in methods)
			{
				
				if (method.DeclaringType.Equals(type))
				{
					var mapinterface = maptointerfacemethod(method);
					if (mapinterface != null)
					{
						Console.WriteLine("\t[{3}] {0} {1} {2} methodname:{4}", method, method.DeclaringType, GetAS3TypeString(method.ReturnType, new Dictionary<Type, string>()),
							InterfaceCreator.GetMethodNativeFunctionName(mapinterface,mapinterface.DeclaringType),
							GetMethodName(method.Name,method,type)
							);
					}
					else
					{
						Console.WriteLine("\t{0} {1} {2} methodname:{3}", method, method.DeclaringType, GetAS3TypeString(method.ReturnType, new Dictionary<Type, string>()),
							GetMethodName(method.Name, method, type)
							);
					}
				}
			}

			var ctors= type.GetConstructors(  );
			foreach (var ctor in ctors)
			{
				Console.WriteLine("\t{0} {1} ctorname:{2}", ctor, ctor.DeclaringType,
							name
							);
			}

			var fields = type.GetFields();
			foreach (var field in fields)
			{
				Console.WriteLine("\t"+field);
			}

		}
	}
}
