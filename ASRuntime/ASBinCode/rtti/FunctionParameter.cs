using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtti
{
	
	/// <summary>
	/// 函数形参
	/// </summary>
	public class FunctionParameter : ISWCSerializable
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string name;
        /// <summary>
        /// 参数类型
        /// </summary>
        public RunTimeDataType type;
        /// <summary>
        /// 默认值
        /// </summary>
        public RightValueBase defaultValue;
        /// <summary>
        /// 是否是参数数组
        /// </summary>
        public bool isPara;



        /// <summary>
        /// 是否通过栈来传递
        /// </summary>
        public bool isOnStack;
        public LeftValueBase varorreg;











		public static FunctionParameter LoadFunctionParameter(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			FunctionParameter parameter = new FunctionParameter();serizlized.Add(key, parameter);

			/// <summary>
			/// 参数名
			/// </summary>
			//public string name;
			parameter.name = reader.ReadString();
			///// <summary>
			///// 参数类型
			///// </summary>
			//public RunTimeDataType type;
			parameter.type = reader.ReadInt32();
			///// <summary>
			///// 默认值
			///// </summary>
			//public RightValueBase defaultValue;
			parameter.defaultValue = (RightValueBase)serizlizer.DeserializeObject<ISWCSerializable>(reader, ISWCSerializableLoader.LoadIMember);
			///// <summary>
			///// 是否是参数数组
			///// </summary>
			//public bool isPara;
			parameter.isPara = reader.ReadBoolean();


			///// <summary>
			///// 是否通过栈来传递
			///// </summary>
			//public bool isOnStack;
			parameter.isOnStack = reader.ReadBoolean();
			//public LeftValueBase varorreg;
			parameter.varorreg = (LeftValueBase)serizlizer.DeserializeObject<ISWCSerializable>(reader, ISWCSerializableLoader.LoadIMember);

			return parameter;
		}



		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			
			/// <summary>
			/// 参数名
			/// </summary>
			//public string name;
			writer.Write(name);
			///// <summary>
			///// 参数类型
			///// </summary>
			//public RunTimeDataType type;
			writer.Write(type);
			///// <summary>
			///// 默认值
			///// </summary>
			//public RightValueBase defaultValue;
			serizlizer.SerializeObject(writer, defaultValue);
			///// <summary>
			///// 是否是参数数组
			///// </summary>
			//public bool isPara;
			writer.Write(isPara);


			///// <summary>
			///// 是否通过栈来传递
			///// </summary>
			//public bool isOnStack;
			writer.Write(isOnStack);
			//public LeftValueBase varorreg;
			serizlizer.SerializeObject(writer, varorreg);
		}
	}
}
