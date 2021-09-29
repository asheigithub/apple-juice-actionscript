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
			_dictDelegateWappers = null;
        }
        
        /// <summary>
        /// 指向的类
        /// </summary>
        public rtti.Class _class;
        /// <summary>
        /// 类成员
        /// </summary>
        public SLOT[] memberData;

		
		private Dictionary<int, object> _dictDelegateWappers;
		/// <summary>
		/// 已缓存的方法到委托的包装
		/// </summary>
		internal virtual Dictionary<int, object> dictDelegateWappers
		{
			get
			{
				if (_dictDelegateWappers == null)
				{
					_dictDelegateWappers = new Dictionary<int, object>();
				}

				return _dictDelegateWappers;
			}
		}



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
