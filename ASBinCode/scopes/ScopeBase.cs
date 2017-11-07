using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.scopes
{
	[Serializable]
	public abstract class ScopeBase : IScope, ISWCSerializable
	{
        private List<IMember> memberlist;

        private IScope _parent;

        public ScopeBase()
        {
            memberlist = new List<IMember>();
            
        }

        public List<IMember> members
        {
            get
            {
                return memberlist;
            }
        }

        public IScope parentScope
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }


		public virtual void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			serizlizer.SerializeObject(writer, (ScopeBase)_parent);

			writer.Write(memberlist.Count);
			for (int i = 0; i < memberlist.Count; i++)
			{
				//((ISWCSerializable)memberlist[i]).Serialize(writer, serizlizer);
				serizlizer.SerializeObject(writer, (ISWCSerializable)memberlist[i]);
			}
		}

		protected void LoadScopeBase(BinaryReader reader, CSWCSerizlizer serizlizer)
		{
			_parent = serizlizer.DeserializeObject<ScopeBase>(reader, Deserialize);

			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				memberlist.Add( (IMember)serizlizer.DeserializeObject<ISWCSerializable>(reader,ISWCSerializableLoader.LoadIMember) );
			}

		}


		public static ScopeBase Deserialize(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			string type = reader.ReadString();
			if (type == "FunctionScope")
			{
				return FunctionScope.LoadFunctionScope(reader, serizlizer,serizlized,key);
			}
			else if (type == "ObjectInstanceScope")
			{
				return ObjectInstanceScope.LoadObjectInstanceScope(reader, serizlizer,serizlized,key);
			}
			else if (type == "OutPackageMemberScope")
			{
				return OutPackageMemberScope.LoadOutPackageMemberScope(reader, serizlizer,serizlized,key);
			}
			else
			{
				throw new IOException("异常的Scope类型");
			}
		}

		
	}
}
