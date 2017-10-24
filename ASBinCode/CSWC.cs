using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASBinCode
{
	[Serializable]
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

		
		public readonly List<NativeFunctionBase> nativefunctions;// = new List<NativeFunctionBase>();
		
		public readonly Dictionary<string, int> nativefunctionNameIndex;// = new Dictionary<string, int>();

        public readonly Dictionary<ASBinCode.rtti.Class, RunTimeDataType>
            dict_Vector_type = new Dictionary<ASBinCode.rtti.Class, RunTimeDataType>();

		
		public readonly Dictionary<ILinkSystemObjCreator, Class> creator_Class;
		//= new Dictionary<ILinkSystemObjCreator, Class>();
		
		public readonly Dictionary<Class, ILinkSystemObjCreator> class_Creator;
            //= new Dictionary<Class, ILinkSystemObjCreator>();
		

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

        public CSWC()
        {
			nativefunctions = new List<NativeFunctionBase>();
			nativefunctionNameIndex = new Dictionary<string, int>();
			creator_Class = new Dictionary<ILinkSystemObjCreator, Class>();
			class_Creator = new Dictionary<Class, ILinkSystemObjCreator>();

			for (int i = 0; i < RunTimeDataType.unknown; i++)
            {
                primitive_to_class_table.Add(null);
            }
            
            operatorOverrides = new OperatorFunctions();
        }

		public void unLoadNativeFunctions()
		{
			nativefunctionNameIndex.Clear();
			nativefunctions.Clear();
		}

		public void regNativeFunction(NativeFunctionBase nativefunction)
        {
            if (!nativefunctionNameIndex.ContainsKey(nativefunction.name))
            {
                nativefunctionNameIndex.Add(nativefunction.name, nativefunctions.Count);
                nativefunctions.Add(nativefunction);
            }
            else
            {
                throw new InvalidOperationException("同名函数已存在");
            }
        }

        public NativeFunctionBase getNativeFunction(int funcitonid)
        {
            var define = functions[funcitonid];
            if (define.native_index == -1)
            {
                define.native_index = nativefunctionNameIndex[define.native_name];
            }
            return nativefunctions[define.native_index];
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
					dictionary.Add(item.package + "." + item.name, item);
					dictionary.Add(item.package + "::" + item.name, item);
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
