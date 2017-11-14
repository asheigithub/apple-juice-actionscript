using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    /// <summary>
    /// 位操作
    /// </summary>
    class OpBit
    {
        public static void execBitAnd(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope, frame);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope, frame);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _BitAnd_ValueOf_CallBacker);
        }
        private static void _BitAnd_ValueOf_CallBacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, frame, step.token);

            int r = (int)(n1 & n2);
            step.reg.getSlot(scope, frame).setValue(r);
			//frame.endStep(step);
			frame.endStepNoError();
        }


        public static void execBitOR(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope, frame);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope, frame);

            var f = frame.player.swc.operatorOverrides.getOperatorFunction(OverrideableOperator.bitOr,
                v1.rtType, v2.rtType);
            if (f != null)
            {
                FunctionCaller fc = frame.player.funcCallerPool.create(frame, step.token);
                fc.SetFunction(f);
                fc.loadDefineFromFunction();
				if (!fc.createParaScope())
				{
					return;
				}
                //fc.releaseAfterCall = true;

                bool success;
                fc.pushParameter(v1, 0, out success);
                fc.pushParameter(v2, 1, out success);
                fc.returnSlot = step.reg.getSlot(scope, frame);
                fc.callbacker = fc;
                fc.call();

            }
            else
            {
                OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _BitOR_ValueOf_CallBacker);
            }
        }

        private static void _BitOR_ValueOf_CallBacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, frame, step.token);

            int r = (int)(n1 | n2);
            step.reg.getSlot(scope, frame).setValue(r);
			//frame.endStep(step);
			frame.endStepNoError();
        }


        public static void execBitXOR(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope, frame);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope, frame);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _BitXOR_ValueOf_Callbacker);
        }

        private static void _BitXOR_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, frame, step.token);

            int r = (int)(n1 ^ n2);
            step.reg.getSlot(scope, frame).setValue(r);
			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execBitNot(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope, frame);

            OpCast.InvokeTwoValueOf(v1, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _BitNot_ValueOf_Callbacker);
        }

        private static void _BitNot_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            int n1 = TypeConverter.ConvertToInt(v1, frame, step.token);

            int r = ~n1;
            step.reg.getSlot(scope, frame).setValue(r);
			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execBitLeftShift(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope, frame);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope, frame);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _BitLeftShift_ValueOf_Callbacker);

        }
        private static void _BitLeftShift_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            int n1 = TypeConverter.ConvertToInt(v1, frame, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, frame, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            int r = n1 << n2;
            step.reg.getSlot(scope, frame).setValue(r);

			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execBitRightShift(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope, frame);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope, frame);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _BitRightShift_ValueOf_CallBacker);
        }
        private static void _BitRightShift_ValueOf_CallBacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            int n1 = TypeConverter.ConvertToInt(v1, frame, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, frame, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            int r = n1 >> n2;
            step.reg.getSlot(scope, frame).setValue(r);

			//frame.endStep(step);
			frame.endStepNoError();
        }
        public static void execBitUnSignRightShift(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope, frame);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope, frame);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _BitUnSignRightShift_ValueOf_Callbacker);
        }
        private static void _BitUnSignRightShift_ValueOf_Callbacker(
            ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, frame, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            uint r = n1 >> n2;
            step.reg.getSlot(scope, frame).setValue(r);

			//frame.endStep(step);
			frame.endStepNoError();
        }
    }
}
