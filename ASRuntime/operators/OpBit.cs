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
        public static void execBitAnd(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, player, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, player, step.token);

            int r =(int)( n1 & n2);
            step.reg.getISlot(scope).setValue(r);

        }

        public static void execBitOR(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, player, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, player, step.token);

            int r = (int)(n1 | n2);
            step.reg.getISlot(scope).setValue(r);

        }
        public static void execBitXOR(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, player, step.token);
            uint n2 = TypeConverter.ConvertToUInt(v2, player, step.token);

            int r = (int)(n1 ^ n2);
            step.reg.getISlot(scope).setValue(r);

        }
        public static void execBitNot(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            
            int n1 = TypeConverter.ConvertToInt(v1, player, step.token);

            int r = ~n1;
            step.reg.getISlot(scope).setValue(r);

        }

        public static void execBitLeftShift(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            int n1 = TypeConverter.ConvertToInt(v1, player, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, player, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            int r = n1 << n2;
            step.reg.getISlot(scope).setValue(r);
        }
        public static void execBitRightShift(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            int n1 = TypeConverter.ConvertToInt(v1, player, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, player, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            int r = n1 >> n2;
            step.reg.getISlot(scope).setValue(r);
        }

        public static void execBitUnSignRightShift(Player player, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v1 = step.arg1.getValue(scope);
            ASBinCode.IRunTimeValue v2 = step.arg2.getValue(scope);

            uint n1 = TypeConverter.ConvertToUInt(v1, player, step.token);
            int n2 = TypeConverter.ConvertToInt(v2, player, step.token);

            if (n2 < 0) { n2 = 0; } else if (n2 > 31) { n2 = 31; }

            uint r = n1 >> n2;
            step.reg.getISlot(scope).setValue(r);
        }

    }
}
