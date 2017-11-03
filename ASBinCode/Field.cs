using System;
using System.Collections.Generic;
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


    }
}
