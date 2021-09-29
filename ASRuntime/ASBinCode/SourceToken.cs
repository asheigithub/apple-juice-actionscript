using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	public class SourceToken : ISWCSerializable
    {
		public static readonly SourceToken Empty = new SourceToken(0,0,string.Empty);

        public int line;
        public int ptr;
        public string sourceFile;

        public SourceToken(int line,int ptr,string sourceFile)
        {
            this.line = line;
            this.ptr = ptr;
            this.sourceFile = sourceFile;
        }



		public static SourceToken LoadToken(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			int line = reader.ReadInt32();
			int ptr = reader.ReadInt32();
			string srcfile;
			var tag = serizlizer.ReadTag(reader);
			if (tag == CSWCSerizlizer.TagType.NULLINSTANCE)
			{
				srcfile = null;
			}
			else if (tag == CSWCSerizlizer.TagType.INSTANCEATCACHE)
			{
				int idx = reader.ReadInt32();
				srcfile = serizlizer.stringpool[idx];
			}
			else if (tag == CSWCSerizlizer.TagType.WRITEINSTANCEANDCACHEIT)
			{
				int idx = reader.ReadInt32();
				srcfile = reader.ReadString();
				serizlizer.stringpool.Add(idx, srcfile);
			}
			else
			{
				throw new IOException("文件错误");
			}

			SourceToken token = new SourceToken(line, ptr, srcfile);
			serizlized.Add(key, token);

			return token;
		}



		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{

			writer.Write(line);
			writer.Write(ptr);

			if (sourceFile == null)
			{
				serizlizer.WriteTag(writer, CSWCSerizlizer.TagType.NULLINSTANCE);
				return;
			}

			foreach (var item in serizlizer.stringpool)
			{
				if (string.Equals(item.Value, sourceFile))
				{
					serizlizer.WriteTag(writer, CSWCSerizlizer.TagType.INSTANCEATCACHE);
					writer.Write(item.Key);
					return;
				}
			}
			int index = serizlizer.stringpool.Count;
			serizlizer.stringpool.Add(index, sourceFile);

			serizlizer.WriteTag(writer, CSWCSerizlizer.TagType.WRITEINSTANCEANDCACHEIT);
			writer.Write(index);
			writer.Write(sourceFile);

		}
	}
}
