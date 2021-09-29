using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	public class FieldMeta :ISWCSerializable 
	{
		public string MetaName;
		public string MetaData;



		public static FieldMeta LoadFieldMeta(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			FieldMeta fieldMeta = new FieldMeta(); serizlized.Add(key, fieldMeta);
			if (!reader.ReadBoolean())
			{
				fieldMeta.MetaName = reader.ReadString();
			}
			if (!reader.ReadBoolean())
			{
				fieldMeta.MetaData = reader.ReadString();
			}

			return fieldMeta;
		}


		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(MetaName == null);
			if (MetaName != null)
			{
				writer.Write(MetaName);
			}

			writer.Write(MetaData == null);
			if (MetaData != null)
			{
				writer.Write(MetaData);
			}
		}
	}
}
