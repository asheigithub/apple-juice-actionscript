using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{

	public delegate ISWCSerializable DeserializeDelegate(BinaryReader reader, CSWCSerizlizer serizlizer,IDictionary<int,object> serizlized,int key);

	public interface ISWCSerializable
	{
		void Serialize(System.IO.BinaryWriter writer,CSWCSerizlizer serizlizer);
	}

	public class CSWCSerizlizer
	{
		//class objCallBacker
		//{
		//	//public object cacheObj;
		//	public List<Action<object>> callbacker=new List<Action<object>>();
		//}

		//class cacheObjDict : IDictionary<int, object>
		//{
		//	private Dictionary<int, object> dictionary = new Dictionary<int, object>();

		//	public object this[int key]
		//	{
		//		get
		//		{

		//			return dictionary[key];

		//		}
		//		set
		//		{
		//			throw new NotImplementedException();
		//		}

		//	}

		//	public ICollection<int> Keys => dictionary.Keys;

		//	public ICollection<object> Values => dictionary.Values;

		//	public int Count => dictionary.Count;

		//	public bool IsReadOnly => false;

		//	public void Add(int key, object value)
		//	{
		//		if (!dictionary.ContainsKey(key))
		//		{
		//			dictionary.Add(key, value);
		//		}
		//		else
		//		{
		//			objCallBacker cb = (objCallBacker)dictionary[key];

		//			dictionary[key] = value;

		//			foreach (var item in cb.callbacker)
		//			{
		//				item(value);
		//			}
		//		}
		//	}

		//	public void SetCallbacker(int key, Action<object> action)
		//	{
		//		if (!dictionary.ContainsKey(key))
		//		{
		//			objCallBacker cb = new objCallBacker();
		//			cb.callbacker.Add(action);
		//			dictionary.Add(key, cb);
		//		}
		//		else
		//		{
		//			objCallBacker cb = (objCallBacker)dictionary[key];
		//			cb.callbacker.Add(action);
		//		}

		//	}



		//	public void Add(KeyValuePair<int, object> item)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	public void Clear()
		//	{
		//		throw new NotImplementedException();
		//	}

		//	public bool Contains(KeyValuePair<int, object> item)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	public bool ContainsKey(int key)
		//	{
		//		if (dictionary.ContainsKey(key))
		//		{
		//			if (dictionary[key] is objCallBacker)
		//			{
		//				return false;
		//			}
		//			else
		//			{
		//				return true;
		//			}
		//		}
		//		else
		//		{
		//			return false;
		//		}
		//	}

		//	public void CopyTo(KeyValuePair<int, object>[] array, int arrayIndex)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	public IEnumerator<KeyValuePair<int, object>> GetEnumerator()
		//	{
		//		return dictionary.GetEnumerator();
		//	}

		//	public bool Remove(int key)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	public bool Remove(KeyValuePair<int, object> item)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	public bool TryGetValue(int key, out object value)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	IEnumerator IEnumerable.GetEnumerator()
		//	{
		//		throw new NotImplementedException();
		//	}
		//}

		class serizlizedObj
		{
			public object obj;
			public override int GetHashCode()
			{
				return obj.GetHashCode();
			}
			public override bool Equals(object obj)
			{
				serizlizedObj other = obj as serizlizedObj;
				if (other == null)
					return false;

				return ReferenceEquals(other.obj, this.obj);
			}
		}


		internal Dictionary<int, string> stringpool = new Dictionary<int, string>();

		private IDictionary<int,object> serizlizedObjects = new Dictionary<int,object>();
		private Dictionary<serizlizedObj, int> dictSerizlized = new Dictionary<serizlizedObj, int>();

		public enum TagType : byte
		{
			WRITEINSTANCEANDCACHEIT=0,
			INSTANCEATCACHE=1,
			NULLINSTANCE=2
		}

		public T DeserializeObject<T>(BinaryReader reader,DeserializeDelegate deserializeDelegate  ) where T:ISWCSerializable
		{
			var tag = ReadTag(reader);
			switch (tag)
			{
				case TagType.WRITEINSTANCEANDCACHEIT:
					{
						int key = reader.ReadInt32();
						var obj= deserializeDelegate(reader, this, serizlizedObjects,key);
						return (T)obj;
					}
				case TagType.INSTANCEATCACHE:
					{
						int index = reader.ReadInt32();

						//if (serizlizedObjects.ContainsKey(index))
						{
							return (T)serizlizedObjects[index];
						}
						//else
						//{
						//	serizlizedObjects.SetCallbacker(index, action);
						//	return default(T);
						//}
						
					}
				case TagType.NULLINSTANCE:
					return default(T);
				default:
					throw new IOException("读取异常");
			}

		}

		public void SerializeObject<T>(System.IO.BinaryWriter writer,T obj) where T:ISWCSerializable
		{
			if (obj == null)
			{
				WriteTag(writer, TagType.NULLINSTANCE);
				return;
			}
			if (!(obj is RunTimeValueBase))
			{
				//foreach (var item in serizlizedObjects)
				//{
				//	//如果有完全相同的对象
				//	if (ReferenceEquals(item.Value, obj))
				//	{
				//		WriteTag(writer, TagType.INSTANCEATCACHE);
				//		writer.Write(item.Key);
				//		return;
				//	}
				//}

				serizlizedObj test = new serizlizedObj();test.obj = obj;

				if (dictSerizlized.ContainsKey(test))
				{
					int key = dictSerizlized[test];

					WriteTag(writer, TagType.INSTANCEATCACHE);
					writer.Write(key);
					return;
				}

			}
			{
				WriteTag(writer, TagType.WRITEINSTANCEANDCACHEIT);

				writer.Write(serizlizedObjects.Count);

				int key = serizlizedObjects.Count;
				serizlizedObjects.Add(serizlizedObjects.Count, obj);

				if (!(obj is RunTimeValueBase))
				{
					serizlizedObj serizlizedObj = new serizlizedObj();
					serizlizedObj.obj = obj;
					dictSerizlized.Add(serizlizedObj, key);

				}
				obj.Serialize(writer, this);

			}

		}




		internal void WriteTag(System.IO.BinaryWriter writer,TagType tag)
		{
			writer.Write((byte)tag);
		}

		internal TagType ReadTag(System.IO.BinaryReader reader)
		{
			return (TagType)reader.ReadByte();
		}


		public byte[] Serialize(CSWC swc)
		{
			serizlizedObjects.Clear();stringpool.Clear();dictSerizlized.Clear();

			//swc._dictlinkcreatorfunctionname.Clear();
			foreach (var item in swc.class_Creator)
			{
				NativeFunctionBase nativeFunctionBase = (NativeFunctionBase)item.Value;

				if (!swc._dictlinkcreatorfunctionname.ContainsKey(nativeFunctionBase.name))
				{
					swc._dictlinkcreatorfunctionname.Add(nativeFunctionBase.name, item.Key);
				}
				else
				{
					if (
						swc._dictlinkcreatorfunctionname[nativeFunctionBase.name] != item.Key)
					{
						throw new InvalidOperationException("确认ClassCreator失败:"+ nativeFunctionBase.name +"多次对应不同Class");
					}
				}
			}


			using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
			{
				using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms,System.Text.Encoding.UTF8))
				{
					bw.Write(swc.classes.Count);
					for (int i = 0; i < swc.classes.Count; i++)
					{
						SerializeObject<rtti.Class>(bw, swc.classes[i]);
					}

					bw.Write(swc.functions.Count);
					for (int i = 0; i < swc.functions.Count; i++)
					{
						SerializeObject<rtti.FunctionDefine>(bw, swc.functions[i]);
					}

					bw.Write(swc.blocks.Count);
					for (int i = 0; i < swc.blocks.Count; i++)
					{
						SerializeObject<CodeBlock>(bw, swc.blocks[i]);
					}

					bw.Write(swc.primitive_to_class_table.Count);
					for (int i = 0; i < swc.primitive_to_class_table.Count; i++)
					{
						SerializeObject<rtti.Class>(bw, swc.primitive_to_class_table[i]);
					}

					bw.Write(swc.dict_Vector_type.Count);
					foreach (var item in swc.dict_Vector_type)
					{
						SerializeObject<rtti.Class>(bw, item.Key);
						bw.Write(item.Value);
					}

					bw.Write(swc._dictlinkcreatorfunctionname.Count);
					foreach (var item in swc._dictlinkcreatorfunctionname)
					{
						bw.Write(item.Key);
						SerializeObject<rtti.Class>(bw, item.Value);
					}

					SerializeObject<OperatorFunctions>(bw, swc.operatorOverrides);


					///// <summary>
					///// 链接到系统Object的类型
					///// </summary>
					//public Class LinkObjectClass;
					SerializeObject<rtti.Class>(bw, swc.LinkObjectClass);

					///// <summary>
					///// Class类型
					///// </summary>
					//public Class TypeClass;
					SerializeObject<rtti.Class>(bw, swc.TypeClass);
					///// <summary>
					///// Object类
					///// </summary>
					//public Class ObjectClass;
					SerializeObject<rtti.Class>(bw, swc.ObjectClass);
					///// <summary>
					///// IEnumerator接口
					///// </summary>
					//public Class IEnumeratorInterface;
					SerializeObject<rtti.Class>(bw, swc.IEnumeratorInterface);
					///// <summary>
					///// IEnumerable接口
					///// </summary>
					//public Class IEnumerableInterface;
					SerializeObject<rtti.Class>(bw, swc.IEnumerableInterface);
					///// <summary>
					///// yielditerator类
					///// </summary>
					//public Class YieldIteratorClass;
					SerializeObject<rtti.Class>(bw, swc.YieldIteratorClass);
					///// <summary>
					///// Function类
					///// </summary>
					//public Class FunctionClass;
					SerializeObject<rtti.Class>(bw, swc.FunctionClass);
					///// <summary>
					///// 异常类
					///// </summary>
					//public Class ErrorClass;
					SerializeObject<rtti.Class>(bw, swc.ErrorClass);
					///// <summary>
					///// 字典特殊类
					///// </summary>
					//public Class DictionaryClass;
					SerializeObject<rtti.Class>(bw, swc.DictionaryClass);
					///// <summary>
					///// 正则类
					///// </summary>
					//public Class RegExpClass;
					SerializeObject<rtti.Class>(bw, swc.RegExpClass);


					bw.Write(swc.MaxMemNumberCount);
					bw.Write(swc.MaxMemIntCount);
					bw.Write(swc.MaxMemUIntCount);
					bw.Write(swc.MaxMemBooleanCount);
					
					
				}




				serizlizedObjects.Clear(); stringpool.Clear();dictSerizlized.Clear();
				return ms.ToArray();
			}
		}

		internal CSWC loadingSwc;
		public CSWC Deserialize(byte[] bin)
		{
			serizlizedObjects.Clear(); stringpool.Clear();dictSerizlized.Clear();

			CSWC swc = new CSWC();loadingSwc = swc;

			using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bin))
			{
				using (System.IO.BinaryReader br = new System.IO.BinaryReader(ms, System.Text.Encoding.UTF8))
				{
					int classcount = br.ReadInt32();
					for (int i = 0; i < classcount; i++)
					{
						swc.classes.Add(DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass));
					}

					int functioncount = br.ReadInt32();
					for (int i = 0; i < functioncount; i++)
					{
						swc.functions.Add(DeserializeObject<rtti.FunctionDefine>(br,rtti.FunctionDefine.LoadFunctionDefine));
					}

					int blockcount = br.ReadInt32();
					for (int i = 0; i < blockcount; i++)
					{
						swc.blocks.Add(DeserializeObject<CodeBlock>(br, CodeBlock.Deserialize));
					}

					swc.primitive_to_class_table.Clear();
					int tablecount = br.ReadInt32();
					for (int i = 0; i < tablecount; i++)
					{
						swc.primitive_to_class_table.Add(DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass));
					}

					int dict_vector_type_count = br.ReadInt32();
					for (int i = 0; i < dict_vector_type_count; i++)
					{
						rtti.Class _class = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
						RunTimeDataType t = br.ReadInt32();
						swc.dict_Vector_type.Add(_class, t);
					}

					int _dictlinkcreatorcount = br.ReadInt32();
					for (int i = 0; i < _dictlinkcreatorcount; i++)
					{
						string key = br.ReadString();
						rtti.Class _class = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
						swc._dictlinkcreatorfunctionname.Add(key, _class);
					}

					swc.operatorOverrides = DeserializeObject<OperatorFunctions>(br, OperatorFunctions.LoadOperatorFunctions);
					
					/////// <summary>
					/////// 链接到系统Object的类型
					/////// </summary>
					////public Class LinkObjectClass;
					//SerializeObject<rtti.Class>(bw, swc.LinkObjectClass);
					swc.LinkObjectClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);

					/////// <summary>
					/////// Class类型
					/////// </summary>
					////public Class TypeClass;
					//SerializeObject<rtti.Class>(bw, swc.TypeClass);
					swc.TypeClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
					/////// <summary>
					/////// Object类
					/////// </summary>
					////public Class ObjectClass;
					//SerializeObject<rtti.Class>(bw, swc.ObjectClass);
					swc.ObjectClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
					/////// <summary>
					/////// IEnumerator接口
					/////// </summary>
					////public Class IEnumeratorInterface;
					//SerializeObject<rtti.Class>(bw, swc.IEnumeratorInterface);
					swc.IEnumeratorInterface = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);

					/////// <summary>
					/////// IEnumerable接口
					/////// </summary>
					////public Class IEnumerableInterface;
					//SerializeObject<rtti.Class>(bw, swc.IEnumerableInterface);
					swc.IEnumerableInterface = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
					/////// <summary>
					/////// yielditerator类
					/////// </summary>
					////public Class YieldIteratorClass;
					//SerializeObject<rtti.Class>(bw, swc.YieldIteratorClass);
					swc.YieldIteratorClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
					/////// <summary>
					/////// Function类
					/////// </summary>
					////public Class FunctionClass;
					//SerializeObject<rtti.Class>(bw, swc.FunctionClass);
					swc.FunctionClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
					/////// <summary>
					/////// 异常类
					/////// </summary>
					////public Class ErrorClass;
					//SerializeObject<rtti.Class>(bw, swc.ErrorClass);
					swc.ErrorClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
					/////// <summary>
					/////// 字典特殊类
					/////// </summary>
					////public Class DictionaryClass;
					//SerializeObject<rtti.Class>(bw, swc.DictionaryClass);
					swc.DictionaryClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);
					/////// <summary>
					/////// 正则类
					/////// </summary>
					////public Class RegExpClass;
					//SerializeObject<rtti.Class>(bw, swc.RegExpClass);
					swc.RegExpClass = DeserializeObject<rtti.Class>(br, rtti.Class.LoadClass);



					swc.MaxMemNumberCount = br.ReadInt32();
					swc.MaxMemIntCount = br.ReadInt32();
					swc.MaxMemUIntCount = br.ReadInt32();
					swc.MaxMemBooleanCount = br.ReadInt32();
					
					
				}
			}


			loadingSwc = null;
			return swc;
		}

	}
}
