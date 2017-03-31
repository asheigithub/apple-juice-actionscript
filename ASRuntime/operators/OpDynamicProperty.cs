using ASBinCode;
using ASBinCode.rtData;
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
                    if (link._canDelete)
                    {
                        ((ASBinCode.rtti.DynamicObject)link.obj.value).deleteProperty(link._propname);
                    }
                    else
                    {
                        if (link.backup != null)
                        {
                            link.directSet(link.backup);
                        }
                    }
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


        public static void exec_set_dynamic_prop(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            ASBinCode.rtData.rtObject obj = (ASBinCode.rtData.rtObject)step.reg.getValue(scope);
            ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;

            DynamicPropertySlot heapslot = new DynamicPropertySlot(obj, true);
            heapslot._propname = ((ASBinCode.rtData.rtString)step.arg1.getValue(scope)).value;
            heapslot.directSet( step.arg2.getValue(scope) );


            dobj.createOrReplaceproperty(heapslot._propname,heapslot);



            frame.endStep(step);
        }
    }
}
