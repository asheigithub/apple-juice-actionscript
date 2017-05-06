﻿using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpLinkOutPackageScope
    {
        public static void exec_link(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            StackSlot l = (StackSlot)step.reg.getSlot(scope);

            int classid = ((ASBinCode.rtData.rtInt)step.arg2.getValue(scope)).value;

            var outscope = frame.player.outpackage_runtimescope[classid];

            SLOT outpackagescopeslot= ((VariableBase)step.arg1).getSlot(outscope);

            Register register = (Register)step.reg;
            if (register._isassigntarget || register._hasUnaryOrShuffixOrDelete)
            {
                l.linkTo(outpackagescopeslot);
            }
            else
            {
                l.directSet( outpackagescopeslot.getValue() );
            }
            frame.endStep(step);
        }
    }
}
