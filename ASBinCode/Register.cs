using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
	[Serializable]
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
		/// 指示是否delete目标
		/// </summary>
		public bool _isdeletetarget;
        /// <summary>
        /// 指示是否对其有=赋值操作
        /// </summary>
        public bool _isassigntarget;
        /// <summary>
        /// 指示是否对其有++,--操作
        /// </summary>
        public bool _hasUnaryOrShuffixOrDelete;

		/// <summary>
		/// 只是是否. [] 操作的目标
		/// </summary>
		public bool _isDotAccessTarget;
        
		[NonSerialized]
        public PackagePathGetter _pathGetter;

		public bool isFindByPath;

        public bool isFuncResult;

        public bool isConvertFromVariable;

        public void setEAXTypeWhenCompile(RunTimeDataType t)
        {
            //type = t;
            valueType = t;
        }

        public readonly int Id;

        public int _index;

		public readonly int stmtid;

        public Register(int id,int stmtid)
        {
            this.Id = id;
            _index = id;
			this.stmtid = stmtid;

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

        public sealed override  RunTimeValueBase getValue(RunTimeScope scope, RunTimeDataHolder holder)
        {
            //var v= getISlot(scope).getValue();
            //return v;

            return holder.stack[holder.offset + _index].getValue();

        }

        
        public override string ToString()
        {
            return "EAX(" + Id + "("+_index+")(S:"+stmtid+")\t" +valueType+ ")";
        }

        public sealed override  SLOT getSlot(RunTimeScope scope, RunTimeDataHolder holder)
        {
            return holder.stack[holder.offset + _index];
            
        }

        public sealed override SLOT getSlotForAssign(RunTimeScope scope, RunTimeDataHolder holder)
        {
            return holder.stack[holder.offset + _index];
            
        }

    }
}
