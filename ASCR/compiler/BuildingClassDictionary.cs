using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASTool.AS3;

namespace ASCompiler.compiler
{
	class BuildingClassDictionary : IDictionary<ASTool.AS3.AS3ClassInterfaceBase, ASBinCode.rtti.Class>
	{
		private Dictionary<AS3ClassInterfaceBase, Class> dictionary;

		public BuildingClassDictionary()
		{
			dictionary = new Dictionary<AS3ClassInterfaceBase, Class>();
		}


		public Class this[AS3ClassInterfaceBase key] { get => dictionary[key]; set => dictionary[key]=value; }

		public ICollection<AS3ClassInterfaceBase> Keys => dictionary.Keys;

		public ICollection<Class> Values => dictionary.Values;

		public int Count => dictionary.Count;

		public bool IsReadOnly => false;

		public bool isExistsBuildSuccess(Class value)
		{
			foreach (var item in dictionary.Values)
			{
				if (item.Equals(value) && item.isbuildSuccess)
				{
					return true;
				}
			}

			return false;
		}

		public void AppendLibClass(Class value)
		{
			if (value.isInterface)
			{
				AS3Interface intface = new AS3Interface(new ASTool.Token(), null);
				intface.Name = value.name;
				intface.Package = new AS3Package(null);
				intface.Package.Name = value.package;

				intface.IsOutPackage = (value.mainClass != null);

				Add(intface, value);
			}
			else
			{
				AS3Class cls = new AS3Class(new ASTool.Token(), null);
				cls.Name = value.name;
				cls.Package = new AS3Package(null);
				cls.Package.Name = value.package;

				cls.IsOutPackage = (value.mainClass != null);

				Add(cls, value);
			}
		}

		public void Add(AS3ClassInterfaceBase key, Class value)
		{
			if (dictionary.ContainsValue(value))
			{
				throw new BuildException(0,0,key.as3SrcFile.srcFile,"重复编译类型"+value.package +":"+ value.name +"。");
			}

			foreach (var item in dictionary.Keys)
			{
				if (item.Name == key.Name && item.Package.Name == key.Package.Name)
				{
					var m1 = item.Package.MainClass;
					var m2 = key.Package.MainClass;
					if (m1 == m2)
					{
						throw new BuildException(0, 0, key.as3SrcFile.srcFile, "重复编译类型" + value.package + ":" + value.name + "。");
					}
				}
			}

			dictionary.Add(key, value);

		}

		public void Add(KeyValuePair<AS3ClassInterfaceBase, Class> item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			dictionary.Clear();
		}

		public bool Contains(KeyValuePair<AS3ClassInterfaceBase, Class> item)
		{
			return dictionary.ContainsKey(item.Key);
		}

		public bool ContainsKey(AS3ClassInterfaceBase key)
		{
			return dictionary.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<AS3ClassInterfaceBase, Class>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<AS3ClassInterfaceBase, Class>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		public bool Remove(AS3ClassInterfaceBase key)
		{
			return dictionary.Remove(key);
		}

		public bool Remove(KeyValuePair<AS3ClassInterfaceBase, Class> item)
		{
			return dictionary.Remove(item.Key);
		}

		public bool TryGetValue(AS3ClassInterfaceBase key, out Class value)
		{
			return dictionary.TryGetValue(key,out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}
	}
}
