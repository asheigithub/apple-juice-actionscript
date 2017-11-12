using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtData
{
	
    /// <summary>
    /// void 数据类型仅包含一个值：undefined。
    /// 在早期的 ActionScript 版本中，undefined 是 Object 类实例的默认值。
    /// 在 ActionScript 3.0 中，Object 实例的默认值是 null。
    /// 如果您尝试将值 undefined 赋予 Object 类的实例，
    /// 则 Flash Player 或 Adobe AIR 会将该值转换为 null。
    /// 您只能为无类型变量赋予 undefined 这一值。
    /// 无类型变量是指缺乏类型注释或者使用星号 (*) 作为类型注释的变量。您可以将 void 只用作返回类型注释。
    /// </summary>
    public sealed class rtUndefined : RunTimeValueBase,IEquatable<rtUndefined>
    {
        private rtUndefined():base(RunTimeDataType.rt_void) { }

        public static readonly  rtUndefined undefined = new rtUndefined();

		public override bool Equals(object obj)
		{
			rtUndefined other = obj as rtUndefined;

			if (other == null)
				return false;
			else
				return true;

		}

		public override int GetHashCode()
		{
			return rtType.GetHashCode();
		}

		public override double toNumber()
        {
            return double.NaN;
        }

        public override string ToString()
        {
            return "undefined";
        }

        public sealed override  object Clone()
        {
            return undefined;
        }

		public bool Equals(rtUndefined other)
		{
			return true;
		}



		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(rtType);
		}

	}
}
