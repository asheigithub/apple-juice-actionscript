using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpPropGetSet
    {
        public static void exec_try_read_prop( StackFrame frame, OpStep step, RunTimeScope scope)
        {
            
            ASBinCode.SLOT slot = ((Register)step.arg1).getSlot(scope);

            if (!slot.isPropGetterSetter)
            {
                SLOT regslot = step.reg.getSlot(scope);

                StackSlot d = regslot as StackSlot;
                StackSlot s = slot as StackSlot;
                
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
                _do_prop_read(
                    
                    ((StackSlot)slot).propGetSet,
                    frame,step,frame.player,scope, ((StackSlot)slot).propBindObj, ((StackSlot)slot).superPropBindClass
                    );

                //do
                //{
                //    ClassPropertyGetter.PropertySlot propslot =
                //        (ASBinCode.ClassPropertyGetter.PropertySlot)((StackSlot)slot).linktarget;
                //    //***调用访问器。***
                //    ASBinCode.ClassPropertyGetter prop = ((StackSlot)slot).propGetSet; //propslot.property;
                //    if (prop.getter == null)
                //    {
                //        frame.throwError(
                //            step.token,0, "Illegal read of write-only property"
                //            );
                //        break;
                //    }
                //    //检查访问权限
                //    CodeBlock block = player.swc.blocks[scope.blockId];
                //    Class finder;
                //    if (block.isoutclass)
                //    {
                //        finder = null;
                //    }
                //    else
                //    {
                //        finder = player.swc.classes[block.define_class_id];
                //    }

                //    var getter = ClassMemberFinder.find(prop._class, prop.getter.name, finder);
                //    if (getter == null || getter.bindField != prop.getter)
                //    {
                //        frame.throwError(
                //            step.token,0, "Illegal read of write-only property"
                //            );
                //        break;
                //    }

                //    //***读取getter***

                //    StackSlot sslot = (StackSlot)slot;
                //    RunTimeValueBase func;

                //    if (sslot.superPropBindClass !=null)
                //    {
                //        func = ((MethodGetterBase)getter.bindField).getSuperMethod(
                //        //propslot.bindObj.objScope
                //        ((StackSlot)slot).propBindObj.objScope,
                //        sslot.superPropBindClass

                //        );
                //    }
                //    else
                //    {
                //        func = ((MethodGetterBase)getter.bindField).getMethod(
                //        //propslot.bindObj.objScope
                //        ((StackSlot)slot).propBindObj.objScope
                //        );

                //    }

                //    //***调用设置器***

                //    var funCaller = new FunctionCaller(player, frame, step.token);
                //    funCaller.function = (ASBinCode.rtData.rtFunction)func;
                //    funCaller.loadDefineFromFunction();
                //    funCaller.createParaScope();
                    
                //    funCaller._tempSlot = frame._tempSlot1;
                //    funCaller.returnSlot = step.reg.getSlot(scope);

                //    ((StackSlot)funCaller.returnSlot).propGetSet = prop;
                //    ((StackSlot)funCaller.returnSlot).propBindObj = ((StackSlot)slot).propBindObj;   //propslot.bindObj;


                //    BlockCallBackBase cb = new BlockCallBackBase();
                //    cb.setCallBacker(_getter_callbacker);
                //    cb.step = step;
                //    cb.args = frame;

                //    funCaller.callbacker = cb;
                //    funCaller.call();

                //    return;

                //} while (false);


                //frame.endStep(step);


            }
        }

        public static void _do_prop_read( 
            ClassPropertyGetter prop,StackFrame frame, OpStep step ,Player player ,RunTimeScope scope ,
            ASBinCode.rtData.rtObject propBindObj,Class superPropBindClass
            )
        {
            do
            {
                //***调用访问器。***
                
                if (prop.getter == null)
                {
                    frame.throwError(
                        step.token, 0, "Illegal read of write-only property"
                        );
                    break;
                }
                //检查访问权限
                CodeBlock block = player.swc.blocks[scope.blockId];
                Class finder;
                if (block.isoutclass)
                {
                    var c = prop._class;
                    if (c.instanceClass != null) { c = c.instanceClass; }
                    if (c.mainClass != null) { c = c.mainClass; }

                    if (block.define_class_id == c.classid)
                    {
                        finder = player.swc.classes[block.define_class_id];
                    }
                    else
                    {
                        finder = null;
                    }
                }
                else
                {
                    finder = player.swc.classes[block.define_class_id];
                }

                var getter = ClassMemberFinder.find(prop._class, prop.getter.name, finder);
                if (getter == null || getter.bindField != prop.getter)
                {
                    frame.throwError(
                        step.token, 0, "Illegal read of write-only property"
                        );
                    break;
                }

                //***读取getter***

                
                RunTimeValueBase func;

                if (superPropBindClass != null)
                {
                    func = ((MethodGetterBase)getter.bindField).getSuperMethod(
                    //propslot.bindObj.objScope
                    propBindObj.objScope,
                    superPropBindClass

                    );
                }
                else
                {
                    func = ((MethodGetterBase)getter.bindField).getMethod(
                    //propslot.bindObj.objScope
                    propBindObj.objScope
                    );

                }

                //***调用设置器***

                var funCaller = new FunctionCaller(player, frame, step.token);
                funCaller.function = (ASBinCode.rtData.rtFunction)func;
                funCaller.loadDefineFromFunction();
                funCaller.createParaScope();

                funCaller._tempSlot = frame._tempSlot1;
                funCaller.returnSlot = step.reg.getSlot(scope);

                ((StackSlot)funCaller.returnSlot).propGetSet = prop;
                ((StackSlot)funCaller.returnSlot).propBindObj = propBindObj;   //propslot.bindObj;


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


        private static void _getter_callbacker(BlockCallBackBase sender, object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }


        public static void exec_try_write_prop(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            StackSlot slot = (StackSlot)((Register)step.arg1).getSlot(scope);
            if (slot.propGetSet != null)
            {

                OpAssigning._doPropAssigning(slot.propGetSet, frame, step, frame.player, scope,
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
