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
        public static void execBitAnd(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, frame, step.token);

            int r =(int)( n1 & n2);
            step.reg.getISlot(scope).setValue(r);

        }

        public static void execBitOR(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, frame, step.token);

            int r = (int)(n1 | n2);
            step.reg.getISlot(scope).setValue(r);

        }
        public static void execBitXOR(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, frame, step.token);

            int r = (int)(n1 ^ n2);
            step.reg.getISlot(scope).setValue(r);

        }
        public static void execBitNot(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            
            int n1 = TypeConverter.ConvertToInt(v1, frame, step.token);

            int r = ~n1;
            step.reg.getISlot(scope).setValue(r);

        }

        public static void execBitLeftShift(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            int n1 = TypeConverter.ConvertToInt(v1, frame, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, frame, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            int r = n1 << n2;
            step.reg.getISlot(scope).setValue(r);
        }
        public static void execBitRightShift(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            int n1 = TypeConverter.ConvertToInt(v1, frame, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, frame, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            int r = n1 >> n2;
            step.reg.getISlot(scope).setValue(r);
        }

        public static void execBitUnSignRightShift(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, frame, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, frame, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            uint r = n1 >> n2;
            step.reg.getISlot(scope).setValue(r);
        }

    }
}
