using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 访问父类的成员
    /// </summary>
    public class SuperPointer : IRightValue
    {
        public rtti.Class superClass;

        public rtti.Class thisClass;

        public SuperPointer(ASBinCode.rtti.Class superClass,rtti.Class thisClass)
        {
            this.superClass = superClass;
            this.thisClass = thisClass;
        }


        public RunTimeDataType valueType
        {
            get
            {
                //throw new NotImplementedException();
                return superClass.getRtType();
            }
        }

        public RunTimeValueBase getValue(RunTimeScope scope)
        {
            return scope.this_pointer;
        }

        public override string ToString()
        {
            return "super";
        }

    }
}
