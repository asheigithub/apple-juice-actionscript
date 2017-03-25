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
                frame.endStep(step);
            }
            else
            {
                var totrace = step.arg1.getValue(scope);

                BlockCallBackBase cb=new BlockCallBackBase();
                cb.args = frame;
                cb.setCallBacker(cast_back);

                operators.OpCast.CastValue(totrace, ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope, frame._tempSlot1, cb
                    );    
            }

            
        }

        private static void cast_back(BlockCallBackBase sender,object args)
        {
            if (sender.isSuccess)
            {
                var rv = ((StackFrame)sender.args)._tempSlot1.getValue();
                if (rv.rtType == ASBinCode.RunTimeDataType.rt_null)
                {
                    Console.WriteLine("null");
                }
                else
                {
                    Console.WriteLine(((ASBinCode.rtData.rtString)rv).valueString());
                }
            }
            else
            {
                Console.WriteLine();
            }
            ((StackFrame)sender.args).endStep();

        }
    }
}
