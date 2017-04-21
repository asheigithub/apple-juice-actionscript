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
        IRunTimeValue getValue(IRunTimeScope scope);

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
        ISLOT getISlot(IRunTimeScope scope);

    }


    public interface IValueSetter
    {
        void setValue(rtData.rtBoolean value);
        void setValue(double value);
        void setValue(int value);
        void setValue(uint value);
        void setValue(string value);
        void setValue(rtData.rtNull value);
        void setValue(rtData.rtUndefined value);
    }

}
