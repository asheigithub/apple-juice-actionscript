using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public class StaticClassDataGetter : RightValueBase
    {
        public readonly rtti.Class _class;
        public StaticClassDataGetter(ASBinCode.rtti.Class _class)
        {
            this._class = _class;
            valueType = _class.getRtType();
        }
        
        public sealed override  RunTimeValueBase getValue(RunTimeScope scope, RunTimeDataHolder holder)
        {
            return holder.static_objects[_class.classid];

        }
    }
}
