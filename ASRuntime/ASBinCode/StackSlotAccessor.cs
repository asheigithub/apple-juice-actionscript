using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	/// <summary>
	/// 表示读写栈上的值
	/// </summary>
	public sealed class StackSlotAccessor : LeftValueBase ,ISWCSerializable
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
        /// 指示是否对其有++,--,delete操作
        /// </summary>
        public bool _hasUnaryOrShuffixOrDelete;
		/// <summary>
		/// 指示是否对其有++,--
		/// </summary>
		public bool _hasUnaryOrShuffix;

		/// <summary>
		/// 只是是否. [] 操作的目标
		/// </summary>
		public bool _isDotAccessTarget;
        
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

		public  int stmtid;

        public StackSlotAccessor(int id,int stmtid)
        {
            this.Id = id;
            _index = id;
			this.stmtid = stmtid;

            //type = RunTimeDataType.unknown;
            valueType = RunTimeDataType.unknown;
        }

        
        public sealed override  RunTimeValueBase getValue(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            return frame.stack[frame.offset + _index].getValue();

        }

        
        public override string ToString()
        {
            return "EAX(" + Id + "("+_index+")(S:"+stmtid+")\t" +valueType+ ")";
        }

        public sealed override  SLOT getSlot(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
			return frame.stack[frame.offset + _index];
        }

        public sealed override SLOT getSlotForAssign(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            return frame.stack[frame.offset + _index];
            
        }













		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(5);
			base.Serialize(writer, serizlizer);

			//public readonly int Id;
			writer.Write(Id);
			//public int _index;
			writer.Write(_index);
			//public readonly int stmtid;
			writer.Write(stmtid);

			///// <summary>
			///// 指示是否delete目标
			///// </summary>
			//public bool _isdeletetarget;
			writer.Write(_isdeletetarget);
			///// <summary>
			///// 指示是否对其有=赋值操作
			///// </summary>
			//public bool _isassigntarget;
			writer.Write(_isassigntarget);
			///// <summary>
			///// 指示是否对其有++,--,delete操作
			///// </summary>
			//public bool _hasUnaryOrShuffixOrDelete;
			writer.Write(_hasUnaryOrShuffixOrDelete);
			///// <summary>
			///// 指示是否对其有++,--
			///// </summary>
			//public bool _hasUnaryOrShuffix;
			writer.Write(_hasUnaryOrShuffix);
			///// <summary>
			///// 只是是否. [] 操作的目标
			///// </summary>
			//public bool _isDotAccessTarget;
			writer.Write(_isDotAccessTarget);

			//public bool isFindByPath;
			writer.Write(isFindByPath);
			//public bool isFuncResult;
			writer.Write(isFuncResult);
			//public bool isConvertFromVariable;
			writer.Write(isConvertFromVariable);

			

		}

		public static StackSlotAccessor LoadRegister(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			RunTimeDataType valuetype = reader.ReadInt32();

			//public readonly int Id;
			int id = reader.ReadInt32();
			//public int _index;
			int _index = reader.ReadInt32();
			//public readonly int stmtid;
			int stmtid = reader.ReadInt32();


			StackSlotAccessor register = new StackSlotAccessor(id,stmtid); serizlized.Add(key, register);
			register._index = _index;
			register.valueType = valuetype;
			///// <summary>
			///// 指示是否delete目标
			///// </summary>
			//public bool _isdeletetarget;
			register._isdeletetarget = reader.ReadBoolean();
			///// <summary>
			///// 指示是否对其有=赋值操作
			///// </summary>
			//public bool _isassigntarget;
			register._isassigntarget = reader.ReadBoolean();
			///// <summary>
			///// 指示是否对其有++,--,delete操作
			///// </summary>
			//public bool _hasUnaryOrShuffixOrDelete;
			register._hasUnaryOrShuffixOrDelete = reader.ReadBoolean();
			///// <summary>
			///// 指示是否对其有++,--
			///// </summary>
			//public bool _hasUnaryOrShuffix;
			register._hasUnaryOrShuffix = reader.ReadBoolean();
			///// <summary>
			///// 只是是否. [] 操作的目标
			///// </summary>
			//public bool _isDotAccessTarget;
			register._isDotAccessTarget = reader.ReadBoolean();

			//public bool isFindByPath;
			register.isFindByPath = reader.ReadBoolean();
			//public bool isFuncResult;
			register.isFuncResult = reader.ReadBoolean();
			//public bool isConvertFromVariable;
			register.isConvertFromVariable = reader.ReadBoolean();

			return register;
			
		}

		
	}
}
