using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
	[Serializable]
    public sealed class rtBoolean : RunTimeValueBase,IEquatable<rtBoolean>
    {
        public static readonly rtBoolean False = new rtBoolean(false);
        public static readonly rtBoolean True = new rtBoolean(true);

        public bool value;
        private rtBoolean(bool v):base(RunTimeDataType.rt_boolean)
        {
            value = v;
        }

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			rtBoolean other = obj as rtBoolean;

			if (other == null)
				return false;
			else
			{
				return value == other.value;
			}

		}

		public bool Equals(rtBoolean other)
		{
			return value == other.value;
		}

		public override double toNumber()
        {
            if (value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public sealed override  object Clone()
        {
            return this;
        }

        public override string ToString()
        {
            if (value)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

		
	}
}
