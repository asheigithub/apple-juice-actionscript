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

        public readonly int indexOfMembers;

        /// <summary>
        /// 赋值是否忽略编译期类型检查
        /// </summary>
        public readonly bool ignoreImplicitCast;

        public Variable(string name, int index):this(name,index,false)
        {

        }

        public Variable(string name,int index,bool ignoreImplicitCast)
        {
            this._name = name;
            this.indexOfMembers = index;
            this.ignoreImplicitCast = ignoreImplicitCast;

            type = RunTimeDataType.rt_void;
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
            return scope.memberData[indexOfMembers];
        }
        


        public override string ToString()
        {
            return "VAR("+name+"\t"+type +")" ;
        }

        
    }
}
