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
        IRunTimeValue getValue(IList<IEAX> temporarys,IRunTimeScope scope);

        RunTimeDataType valueType { get; }

    }

    /// <summary>
    /// 左值接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILeftValue : IRightValue
    {
        void setValue(IRunTimeValue value ,IList<IEAX> temporary, IRunTimeScope scope);
    }
}
