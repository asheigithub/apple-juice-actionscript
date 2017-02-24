using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 表示需对寄存器操作
    /// </summary>
    public class Register : ILeftValue
    {
        private  RunTimeDataType type;

        public void setEAXTypeWhenCompile(RunTimeDataType t)
        {
            type = t;
        }

        public int Id;

        public Register(int id)
        {
            this.Id = id;
            type = RunTimeDataType.unknown;
        }

        public RunTimeDataType valueType
        {
            get
            {
                return type;
            }
        }

        public IRunTimeValue getValue( IRunTimeScope scope)
        {
            return getISlot(scope).getValue();
        }

        
        public override string ToString()
        {
            return "EAX(" + Id + "\t" +type+ ")";
        }

        public ISLOT getISlot(IRunTimeScope scope)
        {
            return scope.stack[scope.offset + Id];
        }
    }
}
