using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASBinCode
{
    /// <summary>
    /// 输出的类库
    /// </summary>
    public class CSWC:IClassFinder
    {
        public List<CodeBlock> blocks = new List<CodeBlock>();
        public List<ASBinCode.rtti.FunctionDefine> functions = new List<ASBinCode.rtti.FunctionDefine>();

        public List<rtti.Class> classes = new List<rtti.Class>();

        /// <summary>
        /// 基本类型转对象的类型转换表
        /// </summary>
        public List<rtti.Class> primitive_to_class_table = new List<Class>();


        public readonly List<NativeFunctionBase> nativefunctions=new List<NativeFunctionBase>();
        public readonly Dictionary<string, int> nativefunctionNameIndex = new Dictionary<string, int>();

        public readonly Dictionary<ASBinCode.rtti.Class, RunTimeDataType>
            dict_Vector_type = new Dictionary<ASBinCode.rtti.Class, RunTimeDataType>();

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

        public CSWC()
        {
            for (int i = 0; i < RunTimeDataType.unknown; i++)
            {
                primitive_to_class_table.Add(null);
            }

            loadBuildinNativeFunctions();
            
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

        private void loadBuildinNativeFunctions()
        {
               //regNativeFunction(new nativefunctions.Int_toPrecision());
        }




        public Class getClassByRunTimeDataType(RunTimeDataType rttype)
        {
            return classes[rttype - RunTimeDataType._OBJECT];

            //throw new NotImplementedException();
        }

        
    }
}
