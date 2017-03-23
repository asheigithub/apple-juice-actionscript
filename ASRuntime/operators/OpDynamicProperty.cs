using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    /// <summary>
    /// 动态属性
    /// </summary>
    class OpDynamicProperty
    {
        public static void exec_delete(Player player,StackFrame frame,OpStep step,IRunTimeScope scope)
        {
            {
                StackSlot slot = (StackSlot)((Register)step.arg1).getISlot(scope);
                if (slot.linktarget is DynamicPropertySlot)
                {
                    DynamicPropertySlot link = (DynamicPropertySlot)slot.linktarget;

                    ((ASBinCode.rtti.DynamicObject)link.obj.value).deleteProperty(link._propname); 
                }
                else
                {
                    frame.throwError(
                        new error.InternalError(step.token,
                                    "动态属性才能被delete",
                                    new ASBinCode.rtData.rtString("动态属性才能被delete")
                                    )
                        );
                    
                }

                frame.endStep(step);
            }
        }
    }
}
