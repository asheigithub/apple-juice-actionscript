using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Catch
    {
        public static bool isCatchError(int tryid,ASBinCode.IRunTimeValue throwValue,ASBinCode.OpStep step,ASBinCode.IRunTimeScope scope)
        {
            int id = ((ASBinCode.rtData.rtInt)((ASBinCode.rtData.RightValue)step.arg1).getValue(null)).value;
            if (tryid == id)
            {
                return TypeConverter.testTypeMatch(throwValue, step.regType);
            }
            return false;
        }
    }
}
