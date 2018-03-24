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
            
            ASBinCode.SLOT slot = ((StackSlotAccessor)step.arg1).getSlot(scope, frame);

            if (slot.isPropGetterSetter)
            {
				((StackSlot)slot).linkTo(null);	
                _do_prop_read(

                    ((StackSlot)slot).stackObjects.propGetSet,
                    frame, step, frame.player, scope, ((StackSlot)slot).stackObjects.propBindObj, ((StackSlot)slot).stackObjects.superPropBindClass
                    );
            }
            else if (slot.isSetThisItem)
            {
                SetThisItemSlot sslot = (SetThisItemSlot)((StackSlot)slot).getLinkSlot();

				((StackSlot)slot).linkTo(null);

				//***调用索引器get***
				RunTimeValueBase func;
                var rtObj = sslot.bindObj;
                var player = frame.player;
                var v2 = sslot.setindex;
                func = ((MethodGetterBase)sslot.get_this_item.bindField).getMethod(
                    rtObj
                    );

                var funCaller = player.funcCallerPool.create(frame, step.token);
                funCaller.SetFunction((ASBinCode.rtData.rtFunction)func);((ASBinCode.rtData.rtFunction)func).Clear();
                funCaller.loadDefineFromFunction();
                if (!funCaller.createParaScope()) { return; }

                //funCaller.releaseAfterCall = true;

                bool success;
                funCaller.pushParameter(v2, 0, out success);
                if (!success)
                {
                    frame.endStep(step);
                    return;
                }

                funCaller._tempSlot = frame._tempSlot1;
                funCaller.returnSlot = step.reg.getSlot(scope, frame);

                StackSlot ret = (StackSlot)funCaller.returnSlot;
                ret.stackObjects._temp_try_write_setthisitem = ret._cache_setthisslot;ret.refPropChanged = true;
                ret._cache_setthisslot.bindObj = rtObj;
                ret._cache_setthisslot.setindex = v2;
				ret._cache_setthisslot.set_this_item = sslot.set_this_item;

                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                cb.setCallBacker(_get_this_item_callbacker);
                cb.step = step;
                cb.args = frame;

                funCaller.callbacker = cb;
                funCaller.call();

                return;


            }
            else
            {
                SLOT regslot = step.reg.getSlot(scope, frame);

                StackSlot d = regslot as StackSlot;
                StackSlot s = slot as StackSlot;

                if (d != null && s != null && s.getLinkSlot() != null)
                {
                    d.linkTo(s.getLinkSlot());
					s.linkTo(null);
                }
                else
                {
                    regslot.directSet(slot.getValue());
                }
				//frame.endStep(step);
				frame.endStepNoError();
            }

           
        }

        private static void _get_this_item_callbacker(BlockCallBackBase sender, object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void _do_prop_read( 
            ClassPropertyGetter prop,StackFrame frame, OpStep step ,Player player ,RunTimeScope scope ,
            ASBinCode.rtData.rtObjectBase propBindObj,Class superPropBindClass
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
                    propBindObj
                    );

                }

                //***调用访问器***

                var funCaller = player.funcCallerPool.create(frame, step.token);
                //funCaller.releaseAfterCall = true;
                funCaller.SetFunction( (ASBinCode.rtData.rtFunction)func); ((ASBinCode.rtData.rtFunction)func).Clear();

				funCaller.loadDefineFromFunction();
                if (!funCaller.createParaScope()) { return; }

                funCaller._tempSlot = frame._tempSlot1;
                funCaller.returnSlot = step.reg.getSlot(scope, frame);

                ((StackSlot)funCaller.returnSlot).stackObjects.propGetSet = prop; ((StackSlot)funCaller.returnSlot).refPropChanged = true;
				((StackSlot)funCaller.returnSlot).stackObjects.propBindObj = propBindObj; 


                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
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
            StackSlot slot = (StackSlot)((StackSlotAccessor)step.arg1).getSlot(scope, frame);
            if (slot.stackObjects.propGetSet != null)
            {

                OpAssigning._doPropAssigning(slot.stackObjects.propGetSet, frame, step, frame.player, scope,
                    slot.stackObjects.propBindObj
                    ,
                    slot.getValue()
                    ,
                    slot
                    );
            }
            else if (slot.stackObjects._temp_try_write_setthisitem !=null)
            {
                SetThisItemSlot sslot = (SetThisItemSlot)slot.stackObjects._temp_try_write_setthisitem;
                slot.stackObjects._temp_try_write_setthisitem = null;

                OpAssigning._doSetThisItem(
                    sslot.bindObj,sslot.set_this_item,
                    slot.getValue(),
                    sslot.setindex,
                    slot, frame, step
                    );

            }
            else
            {
				//frame.endStep(step);
				frame.endStepNoError();
            }

        }

    }
}
