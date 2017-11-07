using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtData
{
	[Serializable]
    public sealed  class rtUInt : RunTimeValueBase
    {
        
        public uint value;
        public rtUInt(uint v):base(RunTimeDataType.rt_uint)
        {
            value = v;
        }

        public override double toNumber()
        {
            return value;
        }

        public sealed override  object Clone()
        {
            return new rtUInt(value);
        }

        public override string ToString()
        {
            return value.ToString();
        }



		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(rtType);
			writer.Write(value);
		}

	}
}
