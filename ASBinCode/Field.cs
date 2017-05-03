using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
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
        
        public Field(string name, int index, int refblockid,bool isConst) : base(name, index, refblockid,isConst)
        {

        }

        public sealed override ISLOT getISlot(IRunTimeScope scope)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
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

        protected sealed override IMember _clone()
        {
            Field f = new Field(name, indexOfMembers, refdefinedinblockid, isConst);
            f.valueType = valueType;
            
            f.isPublic = isPublic;
            f.isInternal = isInternal;
            f.isPrivate = isPrivate;
            f.isProtected = isProtected;
            f.isStatic = isStatic;

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
