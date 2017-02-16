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

        private readonly string _name;

        public readonly int indexOfMembers;

        public Variable(string name,int index)
        {
            this._name = name;
            this.indexOfMembers = index;

            type = RunTimeDataType.rt_void;
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

        public IRunTimeValue getValue(IList<IEAX> temporarys, IRunTimeScope scope)
        {
            //throw new NotImplementedException();
            return scope.memberData[indexOfMembers].getValue();
        }

        public void setValue(IRunTimeValue value, IList<IEAX> temporarys, IRunTimeScope scope)
        {
            scope.memberData[indexOfMembers].setValue(value);
        }


        public override string ToString()
        {
            return "VAR("+name+"\t"+type +")" ;
        }

    }
}
