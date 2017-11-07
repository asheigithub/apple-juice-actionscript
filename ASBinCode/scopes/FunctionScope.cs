using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.scopes
{
	[Serializable]
	public class FunctionScope :ScopeBase ,ISWCSerializable
    {
        public readonly ASBinCode.rtti.FunctionDefine function;
        public FunctionScope(rtti.FunctionDefine function)
        {
            this.function = function;
        }


		public static FunctionScope LoadFunctionScope(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int ,object> serizlized,int key)
		{
			rtti.FunctionDefine function = serizlizer.DeserializeObject<rtti.FunctionDefine>(reader, rtti.FunctionDefine.LoadFunctionDefine);
			FunctionScope scope = new FunctionScope(function);serizlized.Add(key,scope);
			scope.LoadScopeBase(reader, serizlizer);
			return scope;
		}


		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write("FunctionScope");
			serizlizer.SerializeObject(writer, function);
			base.Serialize(writer, serizlizer);
			

		}
	}
}
