using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 右值接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRightValue
    {
        RunTimeValueBase getValue(RunTimeScope scope);

        RunTimeDataType valueType { get; }

    }

    /// <summary>
    /// 左值接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILeftValue : IRightValue
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        SLOT getSlot(RunTimeScope scope);

    }


    

}
