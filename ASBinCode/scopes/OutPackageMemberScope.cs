using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.scopes
{
	[Serializable]
	/// <summary>
	/// 包外代码的Scope
	/// </summary>
	public class OutPackageMemberScope :ScopeBase , ISWCSerializable
    {
        public rtti.Class mainclass;
        public OutPackageMemberScope(rtti.Class mainclass)
        {
            this.mainclass = mainclass;
        }

		public static OutPackageMemberScope LoadOutPackageMemberScope(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			OutPackageMemberScope scope = new OutPackageMemberScope(null);serizlized.Add(key,scope);
			rtti.Class mainclass = serizlizer.DeserializeObject<rtti.Class>(reader, rtti.Class.LoadClass);
			scope.mainclass = mainclass;
			scope.LoadScopeBase(reader, serizlizer);
			return scope;
		}


		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write("OutPackageMemberScope");
			serizlizer.SerializeObject(writer, mainclass);
			base.Serialize(writer, serizlizer);
			

		}
	}
}
