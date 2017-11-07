using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.scopes
{
	[Serializable]
	/// <summary>
	/// 实例对象Scope
	/// </summary>
	public class ObjectInstanceScope :ScopeBase ,ISWCSerializable
    {
        public ASBinCode.rtti.Class _class;
        public ObjectInstanceScope( rtti.Class _class )
        {
            this._class = _class;
        }


		public static ObjectInstanceScope LoadObjectInstanceScope(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int ,object> serizlized, int key)
		{
			ObjectInstanceScope scope = new ObjectInstanceScope(null);serizlized.Add(key,scope);
			rtti.Class _class = serizlizer.DeserializeObject<rtti.Class>(reader, rtti.Class.LoadClass);
			scope._class = _class;
			
			scope.LoadScopeBase(reader, serizlizer);

			return scope;
		}

		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write("ObjectInstanceScope");
			serizlizer.SerializeObject(writer, _class);
			base.Serialize(writer, serizlizer);
		}
	}
}
