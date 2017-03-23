using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Trace
    {
        public static void exec(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (step.arg1.valueType == ASBinCode.RunTimeDataType.unknown)
            {
                Console.WriteLine();
            }
            else
            {
                var totrace = step.arg1.getValue(scope);
                string toout = TypeConverter.ConvertToString(totrace, frame, step.token);
                Console.WriteLine(
                    toout==null?"null":toout
                    
                    );
            }

            frame.endStep(step);
        }
    }
}
