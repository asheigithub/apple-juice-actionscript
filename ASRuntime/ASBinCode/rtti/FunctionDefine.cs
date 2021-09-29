using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtti
{
	
	/// <summary>
	/// 函数定义
	/// </summary>
	public class FunctionDefine :ISWCSerializable
    {
        
        public bool IsAnonymous;

        public string name;

        public bool isMethod;
        
        public bool isStatic;

        public bool isConstructor;

        public FunctionSignature signature;

        

        public int blockid;

        public readonly int functionid;

        /// <summary>
        /// 是否本地函数
        /// </summary>
        public bool isNative;
        public string native_name;
        public int native_index;

        /// <summary>
        /// 是否为yield返回
        /// </summary>
        public bool isYield;

        public FunctionDefine(int id)
        {
            functionid = id;
            native_index = -1;

        }








		public static FunctionDefine LoadFunctionDefine(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			int functionid = reader.ReadInt32();
			FunctionDefine functionDefine = new FunctionDefine(functionid); serizlized.Add(key, functionDefine);

			//	public bool IsAnonymous;
			functionDefine.IsAnonymous = reader.ReadBoolean();
			//public string name;
			functionDefine.name = reader.ReadString();
			//public bool isMethod;
			functionDefine.isMethod = reader.ReadBoolean();
			//public bool isStatic;
			functionDefine.isStatic = reader.ReadBoolean();
			//public bool isConstructor;
			functionDefine.isConstructor = reader.ReadBoolean();

			//public int blockid;
			functionDefine.blockid = reader.ReadInt32();

			///// <summary>
			///// 是否本地函数
			///// </summary>
			//public bool isNative;
			functionDefine.isNative = reader.ReadBoolean();
			//public string native_name;
			if (reader.ReadBoolean())
			{
				functionDefine.native_name = reader.ReadString();
			}
			//public int native_index;
			functionDefine.native_index = reader.ReadInt32();
			///// <summary>
			///// 是否为yield返回
			///// </summary>
			//public bool isYield;
			functionDefine.isYield = reader.ReadBoolean();
			//public FunctionSignature signature;

			functionDefine.signature = serizlizer.DeserializeObject<FunctionSignature>(reader, FunctionSignature.LoadSignature);

			return functionDefine;

		}


		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			//public readonly int functionid;
			writer.Write(functionid);
			//	public bool IsAnonymous;
			writer.Write(IsAnonymous);
			//public string name;
			writer.Write(name);
			//public bool isMethod;
			writer.Write(isMethod);
			//public bool isStatic;
			writer.Write(isStatic);
			//public bool isConstructor;
			writer.Write(isConstructor);

			//public int blockid;
			writer.Write(blockid);
			
			///// <summary>
			///// 是否本地函数
			///// </summary>
			//public bool isNative;
			writer.Write(isNative);
			//public string native_name;
			if (native_name == null)
			{
				writer.Write(false);
			}
			else
			{
				writer.Write(true);
				writer.Write(native_name);
			}
			//public int native_index;
			writer.Write(native_index);
			///// <summary>
			///// 是否为yield返回
			///// </summary>
			//public bool isYield;
			writer.Write(isYield);
			//public FunctionSignature signature;

			serizlizer.SerializeObject(writer, signature);

		}
	}
}
