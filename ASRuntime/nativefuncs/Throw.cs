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
        public static void exec(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            if (step.arg1 == null)
            {
                //表示重新抛出接住的异常

                player.runtimeErrors.Push(new error.InternalError(step.token,
                                "重抛异常还未实现"
                                ));
            }
            else
            {
                player.runtimeErrors.Push(new error.InternalError(step.token,
                                step.arg1.getValue(scope)
                                ));
            }
        }
    }
}
