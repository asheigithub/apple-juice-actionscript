using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 表示是变量
    /// </summary>
    public class Variable : ILeftValue,IMember
    {
        private RunTimeDataType type;

        private  string _name;

        private  int _indexOfMembers;

        private readonly int refblockid;

        /// <summary>
        /// 在哪个代码块中定义
        /// </summary>
        public int refdefinedinblockid { get { return refblockid; } }

        public int indexOfMembers
        {
            get
            {
                return _indexOfMembers;
            }
        }

        /// <summary>
        /// 赋值是否忽略编译期类型检查
        /// </summary>
        public readonly bool ignoreImplicitCast;

        public Variable(string name, int index,int refblockid):this(name,index,false,refblockid)
        {

        }

        public Variable(string name, int index, bool ignoreImplicitCast,int refblockid)
            :this(name,index,ignoreImplicitCast,refblockid, RunTimeDataType.rt_void)

        {
        }

        private Variable(string name, int index, bool ignoreImplicitCast, int refblockid,RunTimeDataType type)
        {
            this._name = name;
            this._indexOfMembers = index;
            this.ignoreImplicitCast = ignoreImplicitCast;
            this.refblockid = refblockid;
            this.type = type;
        }

        /// <summary>
        /// 仅用于编译Catch块时
        /// </summary>
        /// <param name="n"></param>
        public void resetName(string n)
        {
            _name = n;
        }

        public RunTimeDataType valueType
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
        }


        public IRunTimeValue getValue(IRunTimeScope scope)
        {
            //throw new NotImplementedException();
            return getISlot(scope).getValue();
        }

        public ISLOT getISlot(IRunTimeScope scope)
        {
            while( refblockid != scope.blockId )
            {
                scope = scope.parent;
            }
            return scope.memberData[indexOfMembers];
        }
        


        public override string ToString()
        {
            return "VAR("+name+"\t"+type +")" ;
        }

        IMember IMember.clone()
        {
            return new  Variable(name, _indexOfMembers, ignoreImplicitCast,refblockid,valueType);
            
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ _indexOfMembers.GetHashCode() ^ ignoreImplicitCast.GetHashCode() ^ refblockid.GetHashCode() ^ valueType.GetHashCode();   
        }

        public override bool Equals(object obj)
        {
            Variable other = obj as Variable;
            if (other == null)
            {
                return false;
            }

            return name == other.name
                &&
                _indexOfMembers == other._indexOfMembers
                &&
                ignoreImplicitCast == other.ignoreImplicitCast
                &&
                refblockid == other.refblockid
                &&
                valueType == other.valueType;

        }
    }
}
