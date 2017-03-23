using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public class StaticClassDataGetter : IRightValue
    {
        public readonly rtti.Class _class;
        public StaticClassDataGetter(ASBinCode.rtti.Class _class)
        {
            this._class = _class;
        }

        public RunTimeDataType valueType
        {
            get
            {
                return _class.getRtType();
            }
        }

        public IRunTimeValue getValue(IRunTimeScope scope)
        {
            return scope.static_objects[_class.classid];

        }
    }
}
