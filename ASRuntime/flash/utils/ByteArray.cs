using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;


namespace ASRuntime.flash.utils
{
	public class Endian
	{
		public static readonly rtString bigEndian = new rtString("bigEndian");
		public static readonly rtString littleEndian = new rtString("littleEndian");
	}

	class EOFException : Exception
	{

	}


	class BigEndianBinaryReader : System.IO.BinaryReader
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the BigEndianBinaryReader class based on the supplied stream and using UTF8Encoding.
		/// </summary>
		/// <param name="stream"></param>
		public BigEndianBinaryReader(System.IO.Stream stream)
			: base(stream)
		{
		}
		/// <summary>
		/// Initializes a new instance of the BigEndianBinaryReader class based on the supplied stream and a specific character encoding.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="encoding"></param>
		public BigEndianBinaryReader(System.IO.Stream input, Encoding encoding)
			: base(input, encoding)
		{
		}
		#endregion

		#region Methods
		/// <summary>
		/// Reads a 4-byte signed integer using the big-endian layout from the current stream and advances the current position of the stream by two bytes.
		/// </summary>
		/// <returns></returns>
		public int ReadInt32BE()
		{
			// big endian
			byte[] byteArray = new byte[4];
			int iBytesRead = this.Read(byteArray, 0, 4);

			int i = byteArray[0 + 0];
			i = i << 8;
			i = i | byteArray[0 + 1];
			i = i << 8;
			i = i | byteArray[0 + 2];
			i = i << 8;
			i = i | byteArray[0 + 3];
			return i;
		}

		public uint ReadUInt32BE()
		{
			// big endian
			byte[] byteArray = new byte[4];
			int iBytesRead = this.Read(byteArray, 0, 4);

			uint i = byteArray[0 + 0];
			i = i << 8;
			i = i | byteArray[0 + 1];
			i = i << 8;
			i = i | byteArray[0 + 2];
			i = i << 8;
			i = i | byteArray[0 + 3];
			return i;
		}

		public short ReadInt16BE()
		{
			// big endian
			byte[] byteArray = new byte[2];
			int iBytesRead = this.Read(byteArray, 0, 2);

			int i = byteArray[0 + 0];
			i = i << 8;
			i = i | byteArray[0 + 1];

			return (short)i;
		}

		public ushort ReadUInt16BE()
		{
			// big endian
			byte[] byteArray = new byte[2];
			int iBytesRead = this.Read(byteArray, 0, 2);

			uint i = byteArray[0 + 0];
			i = i << 8;
			i = i | byteArray[0 + 1];

			return (ushort)i;
		}

		public double ReadDoubleBE()
		{
			// big endian
			byte[] byteArray = new byte[16];
			int iBytesRead = this.Read(byteArray, 0, 8);

			byteArray[8] = byteArray[7];
			byteArray[9] = byteArray[6];
			byteArray[10] = byteArray[5];
			byteArray[11] = byteArray[4];
			byteArray[12] = byteArray[3];
			byteArray[13] = byteArray[2];
			byteArray[14] = byteArray[1];
			byteArray[15] = byteArray[0];

			using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray, 8, 8))
			{
				using (System.IO.BinaryReader br = new System.IO.BinaryReader(ms))
				{
					return br.ReadDouble();
				}
			}

		}

		internal float ReadSingleBE()
		{
			byte[] byteArray = new byte[8];
			int iBytesRead = this.Read(byteArray, 0, 4);

			byteArray[4] = byteArray[3];
			byteArray[5] = byteArray[2];
			byteArray[6] = byteArray[1];
			byteArray[7] = byteArray[0];


			using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray, 4, 4))
			{
				using (System.IO.BinaryReader br = new System.IO.BinaryReader(ms))
				{
					return br.ReadSingle();
				}
			}
		}


		#endregion
	}

	class BigEndianBinaryWriter : System.IO.BinaryWriter
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the BigEndianBinaryWriter class.
		/// </summary>
		public BigEndianBinaryWriter() : base()
		{

		}
		/// <summary>
		/// Initializes a new instance of the BigEndianBinaryWriter class based on the supplied stream and using UTF-8 as the encoding for strings.
		/// </summary>
		/// <param name="output">The supplied stream.</param>
		public BigEndianBinaryWriter(System.IO.Stream output) : base(output)
		{
		}
		/// <summary>
		/// Initializes a new instance of the BigEndianBinaryWriter class based on the supplied stream and a specific character encoding.
		/// </summary>
		/// <param name="output">The supplied stream.</param>
		/// <param name="encoding">The character encoding.</param>
		public BigEndianBinaryWriter(System.IO.Stream output, Encoding encoding) : base(output, encoding)
		{
		}
		#endregion

		#region Properties
		#endregion

		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="integer">The four-byte signed integer to write.</param>
		public void WriteInt32BE(int integer)
		{
			byte i1 = (byte)(integer >> 24);
			byte i2 = (byte)(integer >> 16);
			byte i3 = (byte)(integer >> 8);
			byte i4 = (byte)(integer & 255);

			this.Write(i1);
			this.Write(i2);
			this.Write(i3);
			this.Write(i4);
		}
		public void WriteUInt32BE(uint integer)
		{
			byte i1 = (byte)(integer >> 24);
			byte i2 = (byte)(integer >> 16);
			byte i3 = (byte)(integer >> 8);
			byte i4 = (byte)(integer & 255);

			this.Write(i1);
			this.Write(i2);
			this.Write(i3);
			this.Write(i4);
		}

		public void WriteInt16BE(short integer)
		{
			//byte i1 = (byte)(integer >> 24);
			//byte i2 = (byte)(integer >> 16);
			byte i3 = (byte)(integer >> 8);
			byte i4 = (byte)(integer & 255);

			//this.Write(i1);
			//this.Write(i2);
			this.Write(i3);
			this.Write(i4);
		}

		public void WriteDoubleBE(double v)
		{
			if (BitConverter.IsLittleEndian)
			{
				var buff = BitConverter.GetBytes(v);

				for (int i = 0; i < 8; i++)
				{
					Write(buff[7 - i]);
				}
			}
			else
			{
				Write(v);
			}
		}

		public void WriteSingleBE(float v)
		{
			if (BitConverter.IsLittleEndian)
			{
				var buff = BitConverter.GetBytes(v);

				for (int i = 0; i < 4; i++)
				{
					Write(buff[3 - i]);
				}
			}
			else
			{
				Write(v);
			}
		}

		#endregion

	}

	public class ByteArray : IDisposable,IDataInput,IDataOutput
	{
		#region IDisposable Support
		private bool disposedValue = false; // 要检测冗余调用

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 释放托管状态(托管对象)。
					br.Close();
					bw.Close();
					ms.Dispose();
					ms = null;
					br = null;
					bw = null;
				}

				// TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
				// TODO: 将大型字段设置为 null。

				disposedValue = true;
			}
		}

		// TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
		// ~ByteArray() {
		//   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
		//   Dispose(false);
		// }

		// 添加此代码以正确实现可处置模式。
		public void Dispose()
		{
			// 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
			Dispose(true);
			// TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
			// GC.SuppressFinalize(this);
		}






		#endregion

		public rtObject bindAS3Object;

		private System.IO.MemoryStream ms;
		private BigEndianBinaryReader br;
		private BigEndianBinaryWriter bw;

		public bool isbig;

		private bool isclosed;

		internal ByteArray()
		{
			ms = new System.IO.MemoryStream();
			br = new BigEndianBinaryReader(ms);
			bw = new BigEndianBinaryWriter(ms);

			isbig = true;
			isclosed = false;
		}

		
		public void AttachMemoryStream(System.IO.MemoryStream stream,bool isClearOld)
		{
			if (isClearOld)
			{
				clear();
			}

			ms = stream;
			br = new BigEndianBinaryReader(ms);
			bw = new BigEndianBinaryWriter(ms);

			isclosed = false;
			position = (uint)ms.Position;
		}

		public void DetachMemoryStream()
		{
			isclosed = true;
			position = 0;
			br = null;
			bw = null;
			ms = null;
		}

		public void WriteInto(byte[] bytes)
		{
			createMs();

			ms.Write(bytes, 0, bytes.Length);
			position = (uint)ms.Position;

		}

		public byte[] ToArray()
		{
			return ms.ToArray();
		}

		private void createMs()
		{
			if (isclosed)
			{
				ms = new System.IO.MemoryStream();
				br = new BigEndianBinaryReader(ms);
				bw = new BigEndianBinaryWriter(ms);

				isclosed = false;
			}
		}

		public void clear()
		{
			if (!isclosed)
			{
				isclosed = true;
				position = 0;
				br.Close();
				br = null;
				bw.Close();
				bw = null;


				ms.Close();
			}
		}

		public uint bytesAvailable
		{
			get
			{
				if (isclosed)
				{
					return 0;
				}
				else
				{
					var a = ms.Length - (long)position;
					if (a < 0)
					{
						return 0;
					}
					else
					{
						return (uint)a;
					}
				}
			}
		}



		public uint length
		{
			get
			{
				if (isclosed)
				{
					return 0;
				}
				else
				{
					return (uint)ms.Length;
				}
			}
			set
			{

				try
				{
					createMs();

					ms.SetLength(value);

					if (ms.Length < position)
					{
						position = (uint)ms.Length;
					}

				}
				catch (OutOfMemoryException)
				{
					throw;
				}
				catch (ArgumentOutOfRangeException)
				{
					throw;
				}
			}
		}

		private uint _pos;
		public uint position
		{
			get { return _pos; }
			set
			{
				_pos = value;
			}
		}


		public void compress()
		{
			//未执行压缩，就当不能压缩。。
			if (!isclosed)
			{
				position = (uint)ms.Length;
			}
		}

		public void uncompress()
		{
			position = 0;
		}

		public bool readBoolean()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + 1 > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;
			int b = br.ReadByte();
			position = (uint)ms.Position;

			if (b == 0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}

		public int readByte()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + 1 > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			int b = br.ReadSByte();
			position = (uint)ms.Position;

			return b;
		}

		public void readBytes(ByteArray byteArray, uint offset, uint length)
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			byteArray.createMs();
			if (byteArray.ms.Length < offset)
			{
				byteArray.ms.SetLength(offset);
			}
			byteArray.ms.Position = offset;

			if (length == 0)
			{
				length = bytesAvailable;
			}
			else if (length > bytesAvailable)
			{
				throw new EOFException();
			}

			while (ms.Position < ms.Length)
			{
				byteArray.ms.WriteByte((byte)ms.ReadByte());
			}

			position = (uint)ms.Position;
		}

		public double readDouble()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(double) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			double b = isbig ? br.ReadDoubleBE() : br.ReadDouble();

			position = (uint)ms.Position;

			return b;
		}

		public double readFloat()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(float) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			float b = isbig ? br.ReadSingleBE() : br.ReadSingle();

			position = (uint)ms.Position;

			return b;
		}

		public int readInt()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(int) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			var b = isbig ? br.ReadInt32BE() : br.ReadInt32();

			position = (uint)ms.Position;

			return b;
		}

		public string readMultiByte(uint length, string charSet)
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + length > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;
			var buffer = br.ReadBytes((int)length);
			position = (uint)ms.Position;

			try
			{
				var encoding = Encoding.GetEncoding(charSet);
				return encoding.GetString(buffer);
			}
			catch (ArgumentException)
			{
				return Encoding.Default.GetString(buffer);
			}

		}

		public int readShort()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(short) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			var b = isbig ? br.ReadInt16BE() : br.ReadInt16();

			position = (uint)ms.Position;

			return b;
		}

		public uint readUnsignedByte()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(byte) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			var b = br.ReadByte();
			position = (uint)ms.Position;

			return b;
		}

		public uint readUnsignedInt()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(uint) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			var b = isbig ? br.ReadUInt32BE() : br.ReadUInt32();
			position = (uint)ms.Position;

			return b;
		}

		public uint readUnsignedShort()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(uint) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			var b = isbig ? br.ReadUInt16BE() : br.ReadUInt16();
			position = (uint)ms.Position;

			return b;
		}

		public string readUTF()
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + sizeof(uint) > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			var len = isbig ? br.ReadUInt16BE() : br.ReadUInt16();
			position = (uint)ms.Position;


			if (position + len > ms.Length)
			{
				throw new EOFException();
			}

			var buffer = br.ReadBytes(len);
			position = (uint)ms.Position;

			return System.Text.Encoding.UTF8.GetString(buffer);

		}

		public string readUTFBytes(uint len)
		{
			if (isclosed)
			{
				throw new EOFException();
			}

			if (position + len > ms.Length)
			{
				throw new EOFException();
			}

			ms.Position = position;

			var buffer = br.ReadBytes((int)len);
			position = (uint)ms.Position;

			return System.Text.Encoding.UTF8.GetString(buffer);
		}


		public override string ToString()
		{
			if (isclosed)
			{
				return string.Empty;
			}
			else
			{
				var buff = ms.ToArray();

				return BitConverter.ToString(buff);

			}
		}


		public void writeBoolean(bool v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			if (v)
			{
				ms.WriteByte(1);
			}
			else
			{
				ms.WriteByte(0);
			}

			position = (uint)ms.Position;


		}

		public void writeByte(int v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			bw.Write((sbyte)v);

			position = (uint)ms.Position;
		}

		public void writeBytes(ByteArray target, uint offset, uint length)
		{
			if (target.isclosed)
			{
				return;
			}

			if (offset > target.length)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (length != 0)
			{
				if (offset + length > target.length)
				{
					length = target.length - offset;
				}
			}
			else
			{
				length = target.length - offset;
			}

			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;


			target.ms.Position = offset;
			for (int i = 0; i < length; i++)
			{
				bw.Write((byte)target.ms.ReadByte());
			}


			position = (uint)ms.Position;
		}

		public void writeBytes(byte[] buffer, int offset, int length)
		{
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (length != 0)
			{
				if (offset + length > buffer.Length)
				{
					length = buffer.Length - offset;
				}
			}
			else
			{
				length = buffer.Length - offset;
			}

			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			ms.Write(buffer, offset, length);

			position = (uint)ms.Position;
		}


		public void writeDouble(double v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			if (isbig)
			{
				bw.WriteDoubleBE(v);
			}
			else
			{
				bw.Write(v);
			}
			position = (uint)ms.Position;
		}

		public void writeFloat(double v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			if (isbig)
			{
				bw.WriteSingleBE((float)v);
			}
			else
			{
				bw.Write((float)v);
			}
			position = (uint)ms.Position;
		}

		public void writeInt(int v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			if (isbig)
			{
				bw.WriteInt32BE(v);
			}
			else
			{
				bw.Write(v);
			}
			position = (uint)ms.Position;
		}

		public void writeMultiByte(string value, string charset)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;


			try
			{
				var encoding = Encoding.GetEncoding(charset);

				var buff = encoding.GetBytes(value);

				for (int i = 0; i < buff.Length; i++)
				{
					ms.WriteByte(buff[i]);
				}


			}
			catch (ArgumentException)
			{
				var buff = Encoding.Default.GetBytes(value);

				for (int i = 0; i < buff.Length; i++)
				{
					ms.WriteByte(buff[i]);
				}
			}

			position = (uint)ms.Position;
		}

		public void writeShort(int v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			if (isbig)
			{
				bw.WriteInt16BE((short)v);
			}
			else
			{
				bw.Write((short)v);
			}
			position = (uint)ms.Position;
		}

		public void writeUnsignedInt(uint v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			if (isbig)
			{
				bw.WriteUInt32BE(v);
			}
			else
			{
				bw.Write(v);
			}
			position = (uint)ms.Position;
		}

		public void writeUTF(string v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			byte[] buff = Encoding.UTF8.GetBytes(v);

			if (isbig)
			{
				bw.WriteInt16BE((short)buff.Length);
			}
			else
			{
				bw.Write((short)buff.Length);
			}

			for (int i = 0; i < buff.Length; i++)
			{
				bw.Write(buff[i]);
			}


			position = (uint)ms.Position;
		}

		internal void writeUTFBytes(string v)
		{
			createMs();
			if (position > ms.Length)
			{
				ms.SetLength(position);
			}
			ms.Position = position;

			byte[] buff = Encoding.UTF8.GetBytes(v);

			for (int i = 0; i < buff.Length; i++)
			{
				bw.Write(buff[i]);
			}

			position = (uint)ms.Position;
		}
	}

}
