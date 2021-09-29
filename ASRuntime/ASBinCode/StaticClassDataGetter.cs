using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	public class StaticClassDataGetter : RightValueBase
    {
        public readonly rtti.Class _class;
        public StaticClassDataGetter(ASBinCode.rtti.Class _class)
        {
            this._class = _class;
            valueType = _class.getRtType();
        }

		public sealed override  RunTimeValueBase getValue(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            return frame.player.static_instance[_class.classid];

        }




		public static StaticClassDataGetter LoadStaticClassDataGetter(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType type = reader.ReadInt32();
			rtti.Class _class = serizlizer.DeserializeObject<rtti.Class>(reader, rtti.Class.LoadClass);

			StaticClassDataGetter sd = new StaticClassDataGetter(_class);serizlized.Add(key, sd);
			sd.valueType = type;

			return sd;
		}



		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(7);
			base.Serialize(writer, serizlizer);
			serizlizer.SerializeObject(writer, _class);

		}

	}
}
