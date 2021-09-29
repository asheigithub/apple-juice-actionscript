using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpNeg
    {
        public static void execNeg(StackFrame frame, ASBinCode.OpStep step,ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v = step.arg1.getValue(scope, frame);

            if (v.rtType != ASBinCode.RunTimeDataType.rt_number)
            {
                var f = frame.player.swc.operatorOverrides.getOperatorFunction(OverrideableOperator.Unary_negation,
                v.rtType, RunTimeDataType.unknown);
                if (f != null)
                {
                    FunctionCaller fc = frame.player.funcCallerPool.create( frame, step.token); //fc.releaseAfterCall = true;
                    fc.SetFunction(f);
                    fc.loadDefineFromFunction();
					if (!fc.createParaScope()) { return; }
					bool success;
                    fc.pushParameter(v, 0, out success);
                    fc.returnSlot = step.reg.getSlot(scope, frame);
                    fc.callbacker = fc;
                    fc.call();

                    return;
                }
                else
                {
                    OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execNeg_ValueOf_Callbacker);
                }
            }
            else
            {
                step.reg.getSlot(scope, frame).setValue(-((ASBinCode.rtData.rtNumber)v).value);//new ASBinCode.rtData.rtNumber( -((ASBinCode.rtData.rtNumber)v).value));
																							   //frame.endStep(step);
				frame.endStepNoError();
            }
        }

        private static void _execNeg_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execNeg_ToString_Callbacker);
            }
            else
            {

                step.reg.getSlot(scope, frame).setValue(
                    -TypeConverter.ConvertToNumber(v1)  
                    );
				//frame.endStep(step);
				frame.endStepNoError();
            }
        }
        private static void _execNeg_ToString_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            step.reg.getSlot(scope, frame).setValue(
                    -TypeConverter.ConvertToNumber(v1)
                    //-((ASBinCode.rtData.rtNumber)v1).value
                    );//new ASBinCode.rtData.rtNumber( -((ASBinCode.rtData.rtNumber)v).value));
					  //frame.endStep(step);
			frame.endStepNoError();
        }
    }
}
