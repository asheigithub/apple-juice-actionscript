using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtData
{
	
    public sealed class RightValue : RightValueBase
    {
        private readonly RunTimeValueBase value;

        public RightValue(RunTimeValueBase value)
        {
            this.value = value;
            valueType = value.rtType;
        }

        //public override sealed RunTimeDataType valueType
        //{
        //    get
        //    {
        //        return value.rtType;
        //    }
        //}

        public override sealed RunTimeValueBase getValue(RunTimeScope scope, RunTimeDataHolder holder)
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }







		public static RightValue LoadRightValue(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType vtype = reader.ReadInt32();
			RunTimeValueBase v = serizlizer.DeserializeObject<RunTimeValueBase>(reader, RunTimeValueBase.LoadRunTimeValueBase);

			RightValue rv = new RightValue(v); serizlized.Add(key, rv);
			rv.valueType = vtype;

			return rv;
		}




		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(6);
			base.Serialize(writer, serizlizer);
			serizlizer.SerializeObject(writer, value);
		}

	}

    
}
