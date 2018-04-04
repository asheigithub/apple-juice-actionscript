using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
    public interface IMember : ISWCSerializable
    {
        string name { get; }

        int indexOfMembers { get; }

        IMember clone();

    }


	internal class ISWCSerializableLoader
	{
		public static ISWCSerializable LoadIMember(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			int membertype = reader.ReadInt32();
			if (membertype == 0)
			{
				return (ClassMethodGetter.LoadClassMethodGetter(reader, serizlizer, serizlized, key));
			}
			else if (membertype == 1)
			{
				return InterfaceMethodGetter.LoadInterfaceMethodGetter(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 2)
			{
				return ClassPropertyGetter.LoadClassPropertyGetter(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 3)
			{
				return Variable.LoadVariable(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 4)
			{
				return Field.LoadField(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 5)
			{
				return StackSlotAccessor.LoadRegister(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 6)
			{
				return rtData.RightValue.LoadRightValue(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 7)
			{
				return StaticClassDataGetter.LoadStaticClassDataGetter(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 8)
			{
				return ThisPointer.LoadThisPointer(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 9)
			{
				return SuperPointer.LoadSuperPointer(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 10)
			{
				return MemRegister_Number.LoadRegister(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 11)
			{
				return MemRegister_Boolean.LoadRegister(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 12)
			{
				return MemRegister_Int.LoadRegister(reader, serizlizer, serizlized, key);
			}
			else if (membertype == 13)
			{
				return MemRegister_UInt.LoadRegister(reader, serizlizer, serizlized, key);
			}
			
			else
			{
				throw new IOException("格式异常");
			}
		}
	}
}
