using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	public sealed class Variable:VariableBase
    {
        public Variable(string name, int index, int refblockid) : this(name, index, false, refblockid, false)
        {

        }
        public Variable(string name, int index, int refblockid, bool isConst):this(name,index,false,refblockid,isConst)
        {

        }
        public Variable(string name, int index, bool ignoreImplicitCast, int refblockid)
            : this(name, index, ignoreImplicitCast, refblockid, RunTimeDataType.rt_void, false)

        {
        }
        public Variable(string name, int index, bool ignoreImplicitCast, int refblockid, bool isConst)
            :this(name,index,ignoreImplicitCast,refblockid, RunTimeDataType.rt_void,isConst)

        {
        }




        private Variable(string name, int index, bool ignoreImplicitCast, int refblockid, RunTimeDataType type, bool isConst)
            :base(name,index,ignoreImplicitCast,refblockid,type,isConst)
        { }

        /// <summary>
        /// 仅在编译优化阶段重设置成员索引。
        /// </summary>
        public void setIndexMemberWhenCompile(int newindex)
        {
            _indexOfMembers = newindex;
        }


        public sealed override RunTimeValueBase getValue(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            while (refblockid != scope.blockId)
            {
                scope = scope.parent;
            }
            return scope.memberData[indexOfMembers].getValue();
        }

        public sealed override SLOT getSlot(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            while (refblockid != scope.blockId)
            {
                scope = scope.parent;
            }
            return scope.memberData[indexOfMembers];
        }

        public sealed override SLOT getSlotForAssign(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            while (refblockid != scope.blockId)
            {
                scope = scope.parent;
            }
            return scope.memberData[indexOfMembers];
        }

        protected sealed override IMember _clone()
        {
            return new Variable(name, _indexOfMembers, ignoreImplicitCast, refblockid, valueType, isConst);
        }



		public static Variable LoadVariable(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
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

			Variable variable = new Variable(_name, _indexOfMembers, ignoreImplicitCast , refblockid,isConst);
			variable.valueType = valuetype;
			serizlized.Add(key, variable);
			return variable;
		}


		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(3);
			base.Serialize(writer, serizlizer);
		}

	}
}
