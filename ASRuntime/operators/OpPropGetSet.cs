using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpPropGetSet
    {
        public static void exec_try_read_prop(Player player, StackFrame frame, OpStep step, RunTimeScope scope)
        {
            
            ASBinCode.SLOT slot = ((Register)step.arg1).getISlot(scope);

            if (!slot.isPropGetterSetter)
            {
                SLOT regslot = step.reg.getISlot(scope);

                StackSlot d = regslot as StackSlot;
                StackSlot s = slot as StackSlot;
                //if (d != null && s != null)
                //{
                    //d.fromArray = s.fromArray;
                    //d.fromArrayIndex = s.fromArrayIndex;
                //}

                if ( d !=null && s!=null && s.linktarget != null)
                {
                    d.linkTo(s.linktarget);
                }
                else
                {
                    regslot.directSet(slot.getValue());
                }
                frame.endStep(step);
            }
            else
            {
                do
                {
                    ClassPropertyGetter.PropertySlot propslot =
                        (ASBinCode.ClassPropertyGetter.PropertySlot)((StackSlot)slot).linktarget;
                    //***调用访问器。***
                    ASBinCode.ClassPropertyGetter prop = ((StackSlot)slot).propGetSet; //propslot.property;
                    if (prop.getter == null)
                    {
                        frame.throwError(
                            step.token,0, "Illegal read of write-only property"
                            );
                        break;
                    }
                    //检查访问权限
                    CodeBlock block = player.swc.blocks[scope.blockId];
                    Class finder;
                    if (block.isoutclass)
                    {
                        finder = null;
                    }
                    else
                    {
                        finder = player.swc.classes[block.define_class_id];
                    }

                    var getter = ClassMemberFinder.find(prop._class, prop.getter.name, finder);
                    if (getter == null || getter.bindField != prop.getter)
                    {
                        frame.throwError(
                            step.token,0, "Illegal read of write-only property"
                            );
                        break;
                    }

                    //***读取getter***

                    StackSlot sslot = (StackSlot)slot;
                    RunTimeValueBase func;

                    if (sslot.superPropBindClass !=null)
                    {
                        func = ((MethodGetterBase)getter.bindField).getSuperMethod(
                        //propslot.bindObj.objScope
                        ((StackSlot)slot).propBindObj.objScope,
                        sslot.superPropBindClass

                        );
                    }
                    else
                    {
                        func = ((MethodGetterBase)getter.bindField).getMethod(
                        //propslot.bindObj.objScope
                        ((StackSlot)slot).propBindObj.objScope
                        );

                    }

                    //***调用设置器***

                    var funCaller = new FunctionCaller(player, frame, step.token);
                    funCaller.function = (ASBinCode.rtData.rtFunction)func;
                    funCaller.loadDefineFromFunction();
                    funCaller.createParaScope();
                    
                    funCaller._tempSlot = frame._tempSlot1;
                    funCaller.returnSlot = step.reg.getISlot(scope);

                    ((StackSlot)funCaller.returnSlot).propGetSet = prop;
                    ((StackSlot)funCaller.returnSlot).propBindObj = ((StackSlot)slot).propBindObj;   //propslot.bindObj;


                    BlockCallBackBase cb = new BlockCallBackBase();
                    cb.setCallBacker(_getter_callbacker);
                    cb.step = step;
                    cb.args = frame;

                    funCaller.callbacker = cb;
                    funCaller.call();

                    return;

                } while (false);


                frame.endStep(step);


            }
        }

        private static void _getter_callbacker(BlockCallBackBase sender, object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }


        public static void exec_try_write_prop(Player player, StackFrame frame, OpStep step, RunTimeScope scope)
        {
            StackSlot slot = (StackSlot)((Register)step.arg1).getISlot(scope);
            if (slot.propGetSet != null)
            {

                OpAssigning._doPropAssigning(slot.propGetSet, frame, step, player, scope,
                    slot.propBindObj
                    ,
                    slot.getValue()
                    ,
                    slot
                    );
            }
            else
            {
                frame.endStep(step);
            }

        }

    }
}
