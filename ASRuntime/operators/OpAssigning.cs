using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAssigning
    {
        public static void execAssigning(StackFrame frame, ASBinCode.OpStep step,  ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v = step.arg1.getValue(scope);
            ASBinCode.SLOT slot = step.reg.getSlotForAssign(scope);

            //if (!slot.isPropGetterSetter)
            {
                if (!slot.directSet(v))
                {
                    
                    if (!(slot is StackSlot)) //直接赋值时
                    {
                        //ext = "Illegal assignment to function " + ((MethodGetterBase)step.reg).name + ".";
                    }
                    else
                    {
                        StackSlot oslot = (StackSlot)slot;
                        //if (oslot.linktarget != null)
                        {
                            slot = oslot.linktarget;
                        }

                        if (slot is ClassPropertyGetter.PropertySlot)
                        {
                            ClassPropertyGetter.PropertySlot propslot =
                            (ASBinCode.ClassPropertyGetter.PropertySlot)slot;
                            //***调用访问器。***
                            ASBinCode.ClassPropertyGetter prop = oslot.propGetSet; //propslot.property;

                            _doPropAssigning(prop, frame, step, frame.player, scope,
                                //propslot.bindObj
                                oslot.propBindObj
                                , v,
                                oslot
                                );
                            return;
                        }

                        if (slot is OpVector.vectorSLot)    //Vector类型不匹配
                        {
                            BlockCallBackBase cb = new BlockCallBackBase();
                            cb.scope = scope;
                            cb.step = step;
                            cb.args = frame;
                            cb.setCallBacker(_vectorConvertCallBacker);

                            //***调用强制类型转换***
                            OpCast.CastValue(v, ((OpVector.vectorSLot)slot).vector_data.vector_type,
                                frame, step.token, scope, frame._tempSlot1, cb, false);

                            return;
                        }

                    }
                    string ext = String.Empty;

                    if (slot is MethodGetterBase.MethodSlot)
                    {
                        ext = "Cannot assign to a method ";// + ((ASBinCode.ClassMethodGetter.MethodSlot)slot).method;
                    }
                    else if (slot is ObjectMemberSlot)
                    {
                        ext = "Illegal write to read-only property ";
                        //+ ((ObjectMemberSlot)slot).obj.value._class.name
                        //+" on ppp.PPC."
                    }
                    else if (slot is ClassPropertyGetter.PropertySlot)
                    {
                        ext = "Illegal write to read-only property ";
                    }
                    else if (slot is OpAccess_Dot.prototypeSlot)
                    {
                        ext = "Cannot create property "
                            + ((OpAccess_Dot.prototypeSlot)slot)._protoname +
                            " on " + ((OpAccess_Dot.prototypeSlot)slot)._protoRootObj.value._class.name ;
                    }

                    frame.throwError(
                        step.token,0, ext
                        );
                }

                frame.endStep(step);
            }
            //else
            //{
            //    ClassPropertyGetter.PropertySlot propslot= 
            //            (ASBinCode.ClassPropertyGetter.PropertySlot)((StackSlot)slot).linktarget;
            //    //***调用访问器。***
            //    ASBinCode.ClassPropertyGetter prop = ((StackSlot)slot).propGetSet; //propslot.property;

            //    _doPropAssigning(prop, frame, step, player, scope,
            //        //propslot.bindObj
            //        ((StackSlot)slot).propBindObj
            //        , v,
            //        (StackSlot)slot
            //        );

            //}

        }

        internal static void _vectorConvertCallBacker(BlockCallBackBase sender,object args)
        {
            StackFrame frame = (StackFrame)sender.args;
            OpStep step = sender.step;

            ASBinCode.RunTimeValueBase v = frame._tempSlot1.getValue();
            ASBinCode.SLOT slot = step.reg.getSlot(sender.scope);

            if (!slot.directSet(v))
            {
                frame.throwCastException(step.token,
                       step.arg1.getValue(sender.scope).rtType,
                       ((OpVector.vectorSLot)((StackSlot)slot).linktarget).vector_data.vector_type
                        );
            }

            frame.endStep(step);
        }


        public static void _doPropAssigning(ClassPropertyGetter prop,StackFrame frame,
            OpStep step,Player player, RunTimeScope scope,
            ASBinCode.rtData.rtObject bindobj,RunTimeValueBase v , StackSlot sslot )
        {
            do
            {

                
                if (prop.setter == null)
                {
                    frame.throwError(
                        step.token,0, "Illegal write to read-only property "
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

                var setter = ClassMemberFinder.find(prop._class, prop.setter.name, finder);
                if (setter == null || setter.bindField != prop.setter)
                {
                    frame.throwError(
                        step.token,0, "Illegal write to read-only property "
                        );
                    break;
                }

                //***读取setter***
                RunTimeValueBase func;

                if (sslot.superPropBindClass != null)
                {
                    func = ((MethodGetterBase)setter.bindField).getSuperMethod(
                        bindobj.objScope,
                        sslot.superPropBindClass
                        );
                }
                else
                {
                    func = ((MethodGetterBase)setter.bindField).getMethod(
                        bindobj.objScope
                        );
                }
                //***调用设置器***

                var funCaller = new FunctionCaller(player, frame, step.token);
                funCaller.function = (ASBinCode.rtData.rtFunction)func;
                funCaller.loadDefineFromFunction();
                funCaller.createParaScope();

                bool success;
                funCaller.pushParameter(v, 0,out success);
                if (!success)
                {
                    frame.endStep(step);
                    return;
                }

                funCaller._tempSlot = frame._tempSlot1;
                funCaller.returnSlot = frame._tempSlot1;

                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_assign_callbacker);
                cb.step = step;
                cb.args = frame;

                funCaller.callbacker = cb;
                funCaller.call();

                return;

            } while (false);


            frame.endStep(step);
        }


        private static void _assign_callbacker(BlockCallBackBase sender,object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }
        
    }
}
