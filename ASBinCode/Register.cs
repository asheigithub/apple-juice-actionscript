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

        public IRunTimeValue getValue(IList<IEAX> temporarys, IRunTimeScope scope)
        {
            return temporarys[Id].getValue();
        }

        public void setValue(IRunTimeValue value, IList<IEAX> temporarys, IRunTimeScope scope)
        {
            temporarys[Id].setValue(value);
        }



        public override string ToString()
        {
            return "EAX(" + Id + "\t" +type+ ")";
        }

    }
}
