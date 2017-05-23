using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpUnaryPlus
    {
        public static void execUnaryPlus(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v = step.arg1.getValue(scope);

            var f = scope.swc.operatorOverrides.getOperatorFunction(OverrideableOperator.Unary_plus, 
                v.rtType,RunTimeDataType.unknown);
            if (f != null)
            {

                FunctionCaller fc =  FunctionCaller.create(frame.player, frame, step.token); //fc.releaseAfterCall = true;
                fc.function = f;
                fc.loadDefineFromFunction();
                bool success;
                fc.pushParameter(v, 0, out success);
                fc.returnSlot = step.reg.getSlot(scope);
                fc.callbacker = fc;
                fc.call();

                return;
            }
            else
            {
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.scope = scope;
                cb.step = step;
                cb.args = frame;
                cb.setCallBacker(_tonumber_cb);

                OpCast.CastValue(v, RunTimeDataType.rt_number,
                    frame, step.token, scope, frame._tempSlot1, cb, false);
            }

        }

        private static void _tonumber_cb(BlockCallBackBase sender, object args)
        {
            StackFrame frame = (StackFrame)sender.args;
            OpStep step = sender.step;

            step.reg.getSlot(sender.scope).setValue( frame._tempSlot1.getValue().toNumber() );
            frame.endStep(step);
        }
    }
}
