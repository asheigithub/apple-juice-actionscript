using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    /// <summary>
    /// 向执行错误栈中追加异常
    /// </summary>
    class Throw
    {
        public static void exec(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (step.arg1 == null)
            {
                //表示重新抛出接住的异常
                
                frame.throwError(new error.InternalError(step.token,
                                "AS3不支持重抛异常"
                                ));
            }
            else
            {
                frame.throwError(new error.InternalError(step.token,
                                step.arg1.getValue(scope)
                                ));
            }

            frame.endStep(step);
        }
    }
}
