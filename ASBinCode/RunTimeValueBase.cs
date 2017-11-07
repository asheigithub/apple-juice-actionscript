using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	[Serializable]
	/// <summary>
	/// 标记是一个运行时的值
	/// </summary>
	public abstract class RunTimeValueBase:ICloneable , ISWCSerializable
    {
        public RunTimeDataType  rtType;

        public RunTimeValueBase(RunTimeDataType rtType)
        {
            this.rtType = rtType;
        }

        public abstract object Clone();

		public abstract double toNumber();


		public abstract void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer);


		public static RunTimeValueBase LoadRunTimeValueBase(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int ,object> list,int key )
		{
			RunTimeDataType type = reader.ReadInt32();

			switch (type)
			{
				case RunTimeDataType.rt_boolean:
					{
						bool b = reader.ReadBoolean();
						if (b)
							return rtData.rtBoolean.True;
						else
							return rtData.rtBoolean.False;
					}
				case RunTimeDataType.rt_array:
					{
						int c = reader.ReadInt32();
						rtData.rtArray array = new rtData.rtArray();
						for (int i = 0; i < c; i++)
						{
							array.innerArray.Add(LoadRunTimeValueBase(reader, serizlizer, list,key));
						}
						return array;
					}
				case RunTimeDataType.rt_function:
					{
						return new ASBinCode.rtData.rtFunction( reader.ReadInt32(),reader.ReadBoolean() );
					}
				case RunTimeDataType.rt_int:
					{
						return new rtData.rtInt( reader.ReadInt32() );
					}
				case RunTimeDataType.rt_null:
					{
						return rtData.rtNull.nullptr;
					}
				case RunTimeDataType.rt_number:
					{
						return new rtData.rtNumber(reader.ReadDouble());
					}
				case RunTimeDataType.rt_string:
					{
						bool isnull = reader.ReadBoolean();
						if (isnull)
						{
							return new rtData.rtString(null);
						}
						else
						{
							return new rtData.rtString(reader.ReadString());
						}
					}
				case RunTimeDataType.rt_uint:
					{
						return new rtData.rtUInt(reader.ReadUInt32());
					}
				case RunTimeDataType.rt_void:
					{
						return rtData.rtUndefined.undefined;
					}
				default:

					throw new IOException("错误数据类型");
			}


		}


	}
}
