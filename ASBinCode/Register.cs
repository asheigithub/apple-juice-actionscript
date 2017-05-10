using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 表示需对寄存器操作
    /// </summary>
    public sealed class Register : LeftValueBase
    {
        

        /// <summary>
        /// 指示访问成员的中间状态
        /// </summary>
        public rtti.ClassMember _regMember;
        /// <summary>
        /// 访问成员的所属Object
        /// </summary>
        public RightValueBase _regMemberSrcObj;

        
        
        /// <summary>
        /// 指示是否对其有=赋值操作
        /// </summary>
        public bool _isassigntarget;
        /// <summary>
        /// 指示是否对其有++,--操作
        /// </summary>
        public bool _hasUnaryOrShuffixOrDelete;
        
        

        public PackagePathGetter _pathGetter;

        public bool isFuncResult;


        public void setEAXTypeWhenCompile(RunTimeDataType t)
        {
            //type = t;
            valueType = t;
        }

        public readonly int Id;

        public int _index;

        public Register(int id)
        {
            this.Id = id;
            _index = id;
            //type = RunTimeDataType.unknown;
            valueType = RunTimeDataType.unknown;
        }

        //public sealed override  RunTimeDataType valueType
        //{
        //    get
        //    {
        //        return type;
        //    }
        //}

        public sealed override  RunTimeValueBase getValue(RunTimeScope scope)
        {
            //var v= getISlot(scope).getValue();
            //return v;

            return scope.stack[scope.offset + _index].getValue();

        }

        
        public override string ToString()
        {
            return "EAX(" + Id + "\t" +valueType+ ")";
        }

        public sealed override  SLOT getSlot(RunTimeScope scope)
        {
            return scope.stack[scope.offset + _index];
        }

        public sealed override SLOT getSlotForAssign(RunTimeScope scope)
        {
            return scope.stack[scope.offset + _index];
        }

    }
}
