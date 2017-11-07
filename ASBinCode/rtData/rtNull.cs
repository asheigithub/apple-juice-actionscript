using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtData
{
	[Serializable]
    public sealed class rtNull : RunTimeValueBase,IEquatable<rtNull>
    {
        public static rtNull nullptr = new rtNull();

        private rtNull():base(RunTimeDataType.rt_null) { }

        public override double toNumber()
        {
            return 0;
        }

        public sealed override  object Clone()
        {
            return nullptr;
        }

        public override string ToString()
        {
            return "rtNull";
        }

		public override int GetHashCode()
		{
			return this.rtType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			rtNull other = obj as rtNull;
			if (other == null)
				return false;
			else
				return true;
		}

		public bool Equals(rtNull other)
		{
			return true;
		}


		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(rtType);
		}

	}
}
