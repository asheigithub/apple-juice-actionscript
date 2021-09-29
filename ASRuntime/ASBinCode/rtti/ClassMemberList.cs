using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
	
	public class ClassMemberList : IList<ClassMember>
	{
		private List<ClassMember> list;

		private Dictionary<string, ClassMember> dictionary;

		
		public ClassMemberList()
		{
			list = new List<ClassMember>();
			dictionary = new Dictionary<string, ClassMember>();
			
		}


		public ClassMember this[int index] { get { return list[index]; } set { list[index] = value; } }

		public int Count { get { return list.Count; } }

		public bool IsReadOnly { get { return false; } }

		public void Add(ClassMember item)
		{
			list.Add(item);
			
			if (item.inheritSrcMember != null && item.inheritSrcMember.isConstructor)
			{
				return;
			}

			if (!dictionary.ContainsKey(item.name))
			{
				dictionary.Add(item.name, item);
			}
			else
			{
				if (dictionary[item.name].inheritFrom != null)
				{
					dictionary[item.name] = item;
				}
			}

		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(ClassMember item)
		{
			return list.Contains(item);
		}

		public ClassMember FindByName(string name)
		{
			ClassMember classMember;
			if (dictionary.TryGetValue(name, out classMember))
			{
				return classMember;
			}
			else
			{
				return null;
			}
		}

		public void CopyTo(ClassMember[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<ClassMember> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public int IndexOf(ClassMember item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, ClassMember item)
		{
			throw new NotImplementedException();
		}

		public bool Remove(ClassMember item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}
	}
}
