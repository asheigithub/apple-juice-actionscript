using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Trace
    {
        public static void exec(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (step.arg1.valueType == ASBinCode.RunTimeDataType.unknown)
            {
                Console.WriteLine();
            }
            else
            {
                var totrace = step.arg1.getValue(scope);
                Console.WriteLine(TypeConverter.ConvertToString(totrace, player, step.token));
            }
        }
    }
}
