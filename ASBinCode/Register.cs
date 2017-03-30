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

        /// <summary>
        /// 指示访问成员的中间状态
        /// </summary>
        public rtti.ClassMember _regMember;
        /// <summary>
        /// 访问成员的所属Object
        /// </summary>
        public IRightValue _regMemberSrcObj;

        public PackagePathGetter _pathGetter;

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
            var v= getISlot(scope).getValue();
            return v;
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
