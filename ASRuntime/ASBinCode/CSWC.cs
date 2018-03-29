using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASBinCode
{

	/// <summary>
	/// 输出的类库
	/// </summary>
	public class CSWC : IClassFinder
	{
		public List<CodeBlock> blocks = new List<CodeBlock>();
		public List<ASBinCode.rtti.FunctionDefine> functions = new List<ASBinCode.rtti.FunctionDefine>();

		public List<rtti.Class> classes = new List<rtti.Class>();

		/// <summary>
		/// 基本类型转对象的类型转换表
		/// </summary>
		public List<rtti.Class> primitive_to_class_table = new List<Class>();

		private Class _arrayClass;
		/// <summary>
		/// Array类
		/// </summary>
		public Class ArrayClass
		{
			get
			{
				if (_arrayClass != null)
					return _arrayClass;

				if (primitive_to_class_table == null)
					return null;

				if (primitive_to_class_table.Count > RunTimeDataType.rt_array
					&&
					primitive_to_class_table[RunTimeDataType.rt_array] != null
					)
				{
					_arrayClass = primitive_to_class_table[RunTimeDataType.rt_array];
					return _arrayClass;
				}

				return null;
			}
		}


		[NonSerialized]
		private List<NativeFunctionBase> nativefunctions;// = new List<NativeFunctionBase>();
		[NonSerialized]
		private Dictionary<string, int> nativefunctionNameIndex;// = new Dictionary<string, int>();

		public readonly Dictionary<ASBinCode.rtti.Class, RunTimeDataType>
			dict_Vector_type = new Dictionary<ASBinCode.rtti.Class, RunTimeDataType>();


		public readonly Dictionary<ILinkSystemObjCreator, Class> creator_Class;

		public readonly Dictionary<Class, ILinkSystemObjCreator> class_Creator;

		internal Dictionary<string, Class> _dictlinkcreatorfunctionname;

		/// <summary>
		/// 链接到系统Object的类型
		/// </summary>
		public Class LinkObjectClass;

		/// <summary>
		/// Class类型
		/// </summary>
		public Class TypeClass;
		/// <summary>
		/// Object类
		/// </summary>
		public Class ObjectClass;
		/// <summary>
		/// IEnumerator接口
		/// </summary>
		public Class IEnumeratorInterface;
		/// <summary>
		/// IEnumerable接口
		/// </summary>
		public Class IEnumerableInterface;

		/// <summary>
		/// yielditerator类
		/// </summary>
		public Class YieldIteratorClass;
		/// <summary>
		/// Function类
		/// </summary>
		public Class FunctionClass;
		/// <summary>
		/// 异常类
		/// </summary>
		public Class ErrorClass;

		/// <summary>
		/// 字典特殊类
		/// </summary>
		public Class DictionaryClass;

		/// <summary>
		/// 正则类
		/// </summary>
		public Class RegExpClass;



		public OperatorFunctions operatorOverrides;

		public int MaxMemNumberCount;
		public int MaxMemIntCount;
		public int MaxMemUIntCount;
		public int MaxMemBooleanCount;

		public List<IMemReg> MemRegList;

		public CSWC()
		{
			nativefunctions = new List<NativeFunctionBase>();
			nativefunctionNameIndex = new Dictionary<string, int>();
			creator_Class = new Dictionary<ILinkSystemObjCreator, Class>();
			class_Creator = new Dictionary<Class, ILinkSystemObjCreator>();
			_dictlinkcreatorfunctionname = new Dictionary<string, Class>();

			for (int i = 0; i < RunTimeDataType.unknown; i++)
			{
				primitive_to_class_table.Add(null);
			}

			operatorOverrides = new OperatorFunctions();

			MemRegList = new List<IMemReg>();

			registedNativeFunctionType = new Dictionary<int, string>();
		}


		private Dictionary<int, string> registedNativeFunctionType;

		public void regNativeFunction(string nativefunctiontype, string nativefunctionname)
		{
			if (!nativefunctionNameIndex.ContainsKey(nativefunctionname))
			{
				nativefunctionNameIndex.Add(nativefunctionname, nativefunctions.Count);
				nativefunctions.Add(null);

				if (_dictlinkcreatorfunctionname.ContainsKey(nativefunctionname))
				{
					//***creator必须立即实例化***
					Type ft = System.Reflection.Assembly.GetExecutingAssembly().GetType(nativefunctionname);
					object nativefunction = System.Activator.CreateInstance(ft);
					nativefunctions[nativefunctionNameIndex[nativefunctionname]] = (NativeFunctionBase)nativefunction;




					if (!class_Creator.ContainsKey(_dictlinkcreatorfunctionname[nativefunctionname]))
					{
						class_Creator.Add(_dictlinkcreatorfunctionname[nativefunctionname], (ILinkSystemObjCreator)nativefunction);
						creator_Class.Add((ILinkSystemObjCreator)nativefunction, _dictlinkcreatorfunctionname[nativefunctionname]);
					}
					else
					{
						class_Creator[_dictlinkcreatorfunctionname[nativefunctionname]] = (ILinkSystemObjCreator)nativefunction;
						creator_Class[(ILinkSystemObjCreator)nativefunction] = _dictlinkcreatorfunctionname[nativefunctionname];
					}

				}
				else
				{
					registedNativeFunctionType.Add(nativefunctionNameIndex[nativefunctionname], nativefunctiontype);
				}
			}
			else
			{
				throw new InvalidOperationException("同名函数已存在");
			}
		}

		public void regNativeFunction(NativeFunctionBase nativefunction, bool isReplace)
		{
			if (!nativefunctionNameIndex.ContainsKey(nativefunction.name))
			{
				nativefunctionNameIndex.Add(nativefunction.name, nativefunctions.Count);
				nativefunctions.Add(nativefunction);

				if (_dictlinkcreatorfunctionname.ContainsKey(nativefunction.name))
				{
					if (!class_Creator.ContainsKey(_dictlinkcreatorfunctionname[nativefunction.name]))
					{
						class_Creator.Add(_dictlinkcreatorfunctionname[nativefunction.name], (ILinkSystemObjCreator)nativefunction);
						creator_Class.Add((ILinkSystemObjCreator)nativefunction, _dictlinkcreatorfunctionname[nativefunction.name]);
					}
					else
					{
						class_Creator[_dictlinkcreatorfunctionname[nativefunction.name]] = (ILinkSystemObjCreator)nativefunction;
						creator_Class[(ILinkSystemObjCreator)nativefunction] = _dictlinkcreatorfunctionname[nativefunction.name];
					}

				}

			}
			else if (isReplace)
			{
				int idx = nativefunctionNameIndex[nativefunction.name];
				nativefunctions[idx] = nativefunction;

				if (_dictlinkcreatorfunctionname.ContainsKey(nativefunction.name))
				{
					if (!class_Creator.ContainsKey(_dictlinkcreatorfunctionname[nativefunction.name]))
					{
						class_Creator.Add(_dictlinkcreatorfunctionname[nativefunction.name], (ILinkSystemObjCreator)nativefunction);
						creator_Class.Add((ILinkSystemObjCreator)nativefunction, _dictlinkcreatorfunctionname[nativefunction.name]);
					}
					else
					{
						class_Creator[_dictlinkcreatorfunctionname[nativefunction.name]] = (ILinkSystemObjCreator)nativefunction;
						creator_Class[(ILinkSystemObjCreator)nativefunction] = _dictlinkcreatorfunctionname[nativefunction.name];
					}

				}
			}
			else
			{
				throw new InvalidOperationException("同名函数已存在");
			}
		}

		public void regNativeFunction(NativeFunctionBase nativefunction)
		{
			regNativeFunction(nativefunction, false);
		}

		public NativeFunctionBase getNativeFunction(int funcitonid)
		{
			var define = functions[funcitonid];
			if (define.native_index == -1)
			{
				int nidx;
				if (nativefunctionNameIndex.TryGetValue(define.native_name, out nidx))
				{
					define.native_index = nidx;
				}
				else
				{
					return null;
				}
			}
			//return nativefunctions[define.native_index]; 
			return LazyLoadNativeFunction(define.native_index);
		}

		public NativeFunctionBase getNativeFunction(FunctionDefine toCallFunc)
		{
			if (toCallFunc.native_index < 0)
			{
				int nidx;
				if (nativefunctionNameIndex.TryGetValue(toCallFunc.native_name, out nidx))
				{
					toCallFunc.native_index = nidx;
					//return nativefunctions[nidx];
					return LazyLoadNativeFunction(nidx);
				}
				else
				{
					return null;
				}
			}
			else
			{
				//return nativefunctions[toCallFunc.native_index];
				return LazyLoadNativeFunction(toCallFunc.native_index);
			}
			//var nf = player.swc.nativefunctions[toCallFunc.native_index];

			//int nidx;
			//if (nativefunctionNameIndex.TryGetValue(nativename, out nidx))
			//{
			//	return nativefunctions[nidx];
			//}
			//else
			//{
			//	return null;
			//}
		}

		public NativeFunctionBase getNativeFunction(string nativename)
		{

			int nidx;
			if (nativefunctionNameIndex.TryGetValue(nativename, out nidx))
			{
				//var f= nativefunctions[nidx];

				//if (f == null)
				//{
				//	Type ft = System.Reflection.Assembly.GetExecutingAssembly().GetType( registedNativeFunctionType[nidx] );
				//	f = (NativeFunctionBase)System.Activator.CreateInstance(ft);
				//	nativefunctions[nidx] = f;
				//}
				//return f;
				return LazyLoadNativeFunction(nidx);
			}
			else
			{
				return null;
			}
		}


		private INativeFunctionFactory nativeFunctionFactory;
		public void SetNativeFunctionFactory(INativeFunctionFactory nativeFunctionFactory)
		{
			this.nativeFunctionFactory = nativeFunctionFactory;
		}

		private NativeFunctionBase LazyLoadNativeFunction(int nidx)
		{
			var f = nativefunctions[nidx];

			if (f == null)
			{
				try
				{
					if (nativeFunctionFactory != null)
					{
						f = nativeFunctionFactory.Create(registedNativeFunctionType[nidx]);
						nativefunctions[nidx] = f;
					}
				}
				catch (ASRunTimeException)
				{
					return null;
				}

				//Type ft = System.Reflection.Assembly.GetEntryAssembly().GetType(registedNativeFunctionType[nidx]);
				//f = (NativeFunctionBase)System.Activator.CreateInstance(ft);
				//nativefunctions[nidx] = f;
			}
			return f;
		}


		public bool ContainsNativeFunction(string nativename)
		{
			int nidx;
			if (nativefunctionNameIndex.TryGetValue(nativename, out nidx))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public int NativeFunctionCount
		{
			get
			{
				return nativefunctions.Count;
			}
		}

		public byte[] toBytes()
		{
			byte[] bin;
			{

				CSWCSerizlizer serizlizer = new CSWCSerizlizer();
				bin = serizlizer.Serialize(this);

			}
			return bin;
		}

		public static CSWC loadFromBytes(byte[] data)
		{

			CSWCSerizlizer serizlizer = new CSWCSerizlizer();
			return serizlizer.Deserialize(data);


		}


		public Class getClassByRunTimeDataType(RunTimeDataType rttype)
		{
			return classes[rttype - RunTimeDataType._OBJECT];
			//throw new NotImplementedException();
		}


		private Dictionary<string, Class> dictionary;

		public Class getClassDefinitionByName(string name)
		{
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, Class>();
				foreach (var item in classes)
				{
					if (string.IsNullOrEmpty(item.package))
					{
						dictionary.Add(item.name, item);
					}
					else
					{
						dictionary.Add(item.package + "." + item.name, item);
						dictionary.Add(item.package + "::" + item.name, item);
					}
				}
			}

			Class r;
			if (dictionary.TryGetValue(name, out r))
			{
				return r;
			}
			else
			{
				return null;
			}

		}
	}
}
