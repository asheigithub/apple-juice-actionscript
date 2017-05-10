using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 右值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RightValueBase
    {
        public abstract RunTimeValueBase getValue(RunTimeScope scope);

        //public abstract RunTimeDataType valueType { get; }
        public RunTimeDataType valueType;
    }

    /// <summary>
    /// 左值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LeftValueBase : RightValueBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public abstract SLOT getSlot(RunTimeScope scope);

        public abstract SLOT getSlotForAssign(RunTimeScope scope);
    }


    

}
