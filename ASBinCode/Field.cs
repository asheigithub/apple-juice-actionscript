using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	[Serializable]
	/// <summary>
	/// 类字段
	/// </summary>
	public sealed class Field : VariableBase
    {
        public bool isPublic;
        public bool isInternal;
        public bool isPrivate;
        public bool isProtected;

        public bool isStatic;

		public List<FieldMeta> metas;

        public Field(string name, int index, int refblockid,bool isConst) : base(name, index, refblockid,isConst)
        {
			metas = new List<FieldMeta>();
        }

        public sealed override SLOT getSlotForAssign(RunTimeScope scope, RunTimeDataHolder holder)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
                if (scope == null)
                {
                    return null;
                }
            }

#if DEBUG
            //***检查类的继承关系***
            rtData.rtObject obj = (rtData.rtObject)scope.this_pointer;
            var cls = obj.value._class;

            while (cls.blockid != refblockid)
            {
                cls = cls.super;
            }
#endif

            return scope.memberData[indexOfMembers];
        }

        public sealed override SLOT getSlot(RunTimeScope scope, RunTimeDataHolder holder)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
                if (scope == null)
                {
                    return null;
                }
            }

#if DEBUG
            //***检查类的继承关系***
            rtData.rtObject obj = (rtData.rtObject)scope.this_pointer;
            var cls = obj.value._class;

            while (cls.blockid != refblockid)
            {
                cls = cls.super;
            }
#endif

            return scope.memberData[indexOfMembers];
        }

        public override RunTimeValueBase getValue(RunTimeScope scope, RunTimeDataHolder holder)
        {
            var slot = getSlot(scope, holder);
            if (slot == null)   //**匿名函数引用了类的成员，但是又在闭包环境下在别的地方被调用，就有可能出现找不到的情况
            {
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            else
            { 
                return slot.getValue();
            }
        }

        protected sealed override IMember _clone()
        {
            Field f = new Field(name, indexOfMembers, refdefinedinblockid, isConst);
            f.valueType = valueType;
            
            f.isPublic = isPublic;
            f.isInternal = isInternal;
            f.isPrivate = isPrivate;
            f.isProtected = isProtected;
            f.isStatic = isStatic;
			f.metas.AddRange(metas);
            return f;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() 
                ^ isPublic.GetHashCode() ^ isInternal.GetHashCode() 
                ^ isPrivate.GetHashCode() ^ isProtected.GetHashCode()
                ^ isStatic.GetHashCode()
                ;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }

            if (!(obj.GetType().Equals(this.GetType())))
            {
                return false;
            }

            Field other = obj as Field;

            return name == other.name
                &&
                indexOfMembers == other.indexOfMembers
                &&
                ignoreImplicitCast == other.ignoreImplicitCast
                &&
                refdefinedinblockid == other.refdefinedinblockid
                &&
                valueType == other.valueType
                &&
                isConst == other.isConst
                &&
                isPublic == other.isPublic
                &&
                isInternal == other.isInternal
                &&
                isPrivate == other.isPrivate
                &&
                isProtected==other.isProtected
                &&
                isStatic== other.isStatic
                

                ;
        }





		public static Field LoadField(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			//	private string _name;
			string _name = reader.ReadString();
			//protected int _indexOfMembers;
			int _indexOfMembers = reader.ReadInt32();
			//protected readonly int refblockid;
			int refblockid = reader.ReadInt32();

			///// <summary>
			///// 赋值是否忽略编译期类型检查
			///// </summary>
			//public readonly bool ignoreImplicitCast;
			bool ignoreImplicitCast = reader.ReadBoolean();
			///// <summary>
			///// 是否不可赋值
			///// </summary>
			//public readonly bool isConst;
			bool isConst = reader.ReadBoolean();

			RunTimeDataType valuetype = reader.ReadInt32();

			Field field = new Field(_name, _indexOfMembers, refblockid, isConst);
			field.valueType = valuetype;

			serizlized.Add(key, field);



			field.isPublic = reader.ReadBoolean();
			field.isInternal = reader.ReadBoolean();
			field.isPrivate = reader.ReadBoolean();
			field.isProtected = reader.ReadBoolean();
			field.isStatic = reader.ReadBoolean();

			int metas = reader.ReadInt32();
			for (int i = 0; i < metas; i++)
			{
				field.metas.Add(serizlizer.DeserializeObject<FieldMeta>(reader, FieldMeta.LoadFieldMeta));
			}

			return field;
		}




		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(4);
			base.Serialize(writer, serizlizer);

			//public bool isPublic;
			writer.Write(isPublic);
			//public bool isInternal;
			writer.Write(isInternal);
			//public bool isPrivate;
			writer.Write(isPrivate);
			//public bool isProtected;
			writer.Write(isProtected);
			//public bool isStatic;
			writer.Write(isStatic);
			//public List<FieldMeta> metas;

			writer.Write(metas.Count);
			for (int i = 0; i < metas.Count; i++)
			{
				serizlizer.SerializeObject(writer, metas[i]);
			}

		}



	}
}
