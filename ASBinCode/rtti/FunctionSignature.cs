using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtti
{
	[Serializable]
	/// <summary>
	/// 函数签名
	/// </summary>
	public class FunctionSignature : ISWCSerializable
    {
        public List<FunctionParameter> parameters;
        public RunTimeDataType returnType;

        public int onStackParameters;



		public static FunctionSignature LoadSignature(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			FunctionSignature signature = new FunctionSignature();serizlized.Add(key, signature);
			signature.parameters = new List<FunctionParameter>();
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				signature.parameters.Add(serizlizer.DeserializeObject<FunctionParameter>(reader, FunctionParameter.LoadFunctionParameter ));
			}

			signature.returnType = reader.ReadInt32();
			signature.onStackParameters = reader.ReadInt32();

			return signature;
		}


		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			//public List<FunctionParameter> parameters;
			writer.Write(parameters.Count);
			for (int i = 0; i < parameters.Count; i++)
			{
				serizlizer.SerializeObject(writer, parameters[i]);
			}

			//public RunTimeDataType returnType;
			writer.Write(returnType);
			//public int onStackParameters;
			writer.Write(onStackParameters);
		}
	}
}
