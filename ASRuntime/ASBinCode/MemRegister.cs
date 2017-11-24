using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ASBinCode.rtData;
using ASRuntime;

namespace ASBinCode
{
	public interface IMemReg: ISWCSerializable
	{
		int getId();
		void setId(int newid);
	}

	public sealed class MemRegister_Number : LeftValueBase,IMemReg
	{
		sealed class MemSlot_Number : SLOT
		{
			private rtNumber number;

			public MemSlot_Number(rtNumber value)
			{
				number = value;
			}


			public override SLOT assign(RunTimeValueBase value, out bool success)
			{
				number.value = value.toNumber();
				success = true;
				return this;
			}

			public override void clear()
			{
				throw new NotImplementedException();
			}

			public override bool directSet(RunTimeValueBase value)
			{
				number.value = value.toNumber();
				return true;
			}

			public override RunTimeValueBase getValue()
			{
				return number;
			}

			public override void setValue(rtBoolean value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(double value)
			{
				number.value = value;
			}

			public override void setValue(int value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(uint value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(string value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtNull value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtUndefined value)
			{
				throw new NotImplementedException();
			}
		}

		public int Id;

		public int getId() { return Id; }

		public void setId(int newid) { Id = newid; }

		public readonly rtData.rtNumber value;

		MemSlot_Number MemSlot_;

		public MemRegister_Number(int id)
		{
			this.Id = id;
			valueType = RunTimeDataType.rt_number;
			value = new rtNumber(0);

			MemSlot_ = new MemSlot_Number(value);

		}

		public override string ToString()
		{
			return "MEM(" + Id + "\tNumber)";
		}


		public override SLOT getSlot(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return MemSlot_;
		}

		public override SLOT getSlotForAssign(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return MemSlot_;
		}

		public override RunTimeValueBase getValue(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return value;
		}






		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(10);
			base.Serialize(writer, serizlizer);
			writer.Write(Id);
		}

		public static MemRegister_Number LoadRegister(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType valuetype = reader.ReadInt32();
			int id = reader.ReadInt32();
			
			MemRegister_Number register = new MemRegister_Number(id);
			serizlized.Add(key, register);

			return register;
		}

	}

	public sealed class MemRegister_Boolean : LeftValueBase, IMemReg
	{
		sealed class MemSlot_Boolean : SLOT
		{
			MemRegister_Boolean parent;
			public MemSlot_Boolean(MemRegister_Boolean parent)
			{
				this.parent = parent;
			}


			public override SLOT assign(RunTimeValueBase value, out bool success)
			{
				parent.value = (rtBoolean)value;
				success = true;
				return this;
			}

			public override void clear()
			{
				throw new NotImplementedException();
			}

			public override bool directSet(RunTimeValueBase value)
			{
				parent.value = (rtBoolean)value;
				return true;
			}

			public override RunTimeValueBase getValue()
			{
				return parent.value;
			}

			public override void setValue(rtBoolean value)
			{
				parent.value = value;
			}

			public override void setValue(double value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(int value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(uint value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(string value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtNull value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtUndefined value)
			{
				throw new NotImplementedException();
			}
		}

		public int Id;

		public int getId() { return Id; }
		public void setId(int newid) { Id = newid; }

		private MemSlot_Boolean memSlot_;

		private rtBoolean value;
		public MemRegister_Boolean(int id)
		{
			Id = id;
			this.valueType = RunTimeDataType.rt_boolean;
			value = ASBinCode.rtData.rtBoolean.False;
			memSlot_ = new MemSlot_Boolean(this);
		}


		public override SLOT getSlot(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return memSlot_;
		}

		public override SLOT getSlotForAssign(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return memSlot_;
		}

		public override RunTimeValueBase getValue(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return value;
		}

		public override string ToString()
		{
			return "MEM(" + Id + "\tBoolean)";
		}





		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(11);
			base.Serialize(writer, serizlizer);
			writer.Write(Id);
		}

		public static MemRegister_Boolean LoadRegister(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType valuetype = reader.ReadInt32();
			int id = reader.ReadInt32();

			MemRegister_Boolean register = new MemRegister_Boolean(id);
			serizlized.Add(key, register);

			return register;
		}

	}

	public sealed class MemRegister_Int : LeftValueBase, IMemReg
	{
		sealed class MemSlot_Int : SLOT
		{
			private rtInt number;

			public MemSlot_Int(rtInt value)
			{
				number = value;
			}


			public override SLOT assign(RunTimeValueBase value, out bool success)
			{
				number.value = ((rtInt)value).value;
				success = true;
				return this;
			}

			public override void clear()
			{
				throw new NotImplementedException();
			}

			public override bool directSet(RunTimeValueBase value)
			{
				number.value = ((rtInt)value).value;
				return true;
			}

			public override RunTimeValueBase getValue()
			{
				return number;
			}

			public override void setValue(rtBoolean value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(double value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(int value)
			{
				number.value = value;
			}

			public override void setValue(uint value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(string value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtNull value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtUndefined value)
			{
				throw new NotImplementedException();
			}
		}


		public int Id;

		public int getId() { return Id; }
		public void setId(int newid) { Id = newid; }

		public readonly rtData.rtInt value;

		MemSlot_Int MemSlot_;

		public MemRegister_Int(int id)
		{
			this.Id = id;
			valueType = RunTimeDataType.rt_int;
			value = new rtInt(0);

			MemSlot_ = new MemSlot_Int(value);

		}

		public override string ToString()
		{
			return "MEM(" + Id + "\tInt)";
		}





		public override SLOT getSlot(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return MemSlot_;
		}

		public override SLOT getSlotForAssign(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return MemSlot_;
		}

		public override RunTimeValueBase getValue(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return value;
		}



		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(12);
			base.Serialize(writer, serizlizer);
			writer.Write(Id);
		}

		public static MemRegister_Int LoadRegister(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType valuetype = reader.ReadInt32();
			int id = reader.ReadInt32();

			MemRegister_Int register = new MemRegister_Int(id);
			serizlized.Add(key, register);

			return register;
		}

	}

	public sealed class MemRegister_UInt : LeftValueBase, IMemReg
	{
		sealed class MemSlot_UInt : SLOT
		{
			private readonly rtUInt number;

			public MemSlot_UInt(rtUInt value)
			{
				number = value;
			}


			public override SLOT assign(RunTimeValueBase value, out bool success)
			{
				number.value = ((rtUInt)value).value;
				success = true;
				return this;
			}

			public override void clear()
			{
				throw new NotImplementedException();
			}

			public override bool directSet(RunTimeValueBase value)
			{
				number.value = ((rtUInt)value).value;
				return true;
			}

			public override RunTimeValueBase getValue()
			{
				return number;
			}

			public override void setValue(rtBoolean value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(double value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(int value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(uint value)
			{
				number.value = value;
			}

			public override void setValue(string value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtNull value)
			{
				throw new NotImplementedException();
			}

			public override void setValue(rtUndefined value)
			{
				throw new NotImplementedException();
			}
		}


		public int Id;

		public int getId() { return Id; }
		public void setId(int newid) { Id = newid; }

		public readonly rtData.rtUInt value;

		MemSlot_UInt MemSlot_;

		public MemRegister_UInt(int id)
		{
			this.Id = id;
			valueType = RunTimeDataType.rt_uint;
			value = new rtUInt(0);

			MemSlot_ = new MemSlot_UInt(value);

		}

		public override string ToString()
		{
			return "MEM(" + Id + "\tUInt)";
		}





		public override SLOT getSlot(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return MemSlot_;
		}

		public override SLOT getSlotForAssign(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return MemSlot_;
		}

		public override RunTimeValueBase getValue(RunTimeScope scope, StackSlot[] slots, int stoffset)
		{
			return value;
		}



		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(13);
			base.Serialize(writer, serizlizer);
			writer.Write(Id);
		}

		public static MemRegister_UInt LoadRegister(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType valuetype = reader.ReadInt32();
			int id = reader.ReadInt32();

			MemRegister_UInt register = new MemRegister_UInt(id);
			serizlized.Add(key, register);

			return register;
		}

	}

}
