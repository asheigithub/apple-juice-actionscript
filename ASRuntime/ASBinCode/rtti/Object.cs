using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    /// <summary>
    /// Object Instance
    /// </summary>
    public class Object
    {
        
        public Object(rtti.Class _class)
        {
            this._class = _class;
            memberData = null;
        }
        
        /// <summary>
        /// 指向的类
        /// </summary>
        public rtti.Class _class;
        /// <summary>
        /// 类成员
        /// </summary>
        public SLOT[] memberData;

        public override string ToString()
        {
            if (_class.staticClass == null)
            {
                return "[class " + _class.instanceClass.name + "]";
            }
            else
            {
                return "[object " + _class.name +"]";
            }
        }
    }
}
