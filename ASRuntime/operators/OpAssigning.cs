using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAssigning
    {
        public static void execAssigning(Player player, ASBinCode.OpStep step ,StackFrame frame, ASBinCode.IRunTimeScope scope)
        {
            ASBinCode.IRunTimeValue v = step.arg1.getValue(scope);
            ASBinCode.ISLOT slot = step.reg.getISlot(scope);

            if (!slot.isPropGetterSetter)
            {
                if (!slot.directSet(v))
                {
                    if (((StackSlot)slot).linktarget != null)
                    {
                        slot = ((StackSlot)slot).linktarget;
                    }
                    string ext = String.Empty;
                    if (slot is ClassMethodGetter.MethodSlot)
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

                    frame.throwError(
                        new error.InternalError(step.token, ext, new ASBinCode.rtData.rtString(ext))
                        );
                }

                frame.endStep(step);
            }
            else
            {
                ClassPropertyGetter.PropertySlot propslot= 
                        (ASBinCode.ClassPropertyGetter.PropertySlot)((StackSlot)slot).linktarget;
                //***调用访问器。***
                ASBinCode.ClassPropertyGetter prop = ((StackSlot)slot).propGetSet; //propslot.property;

                _doPropAssigning(prop, frame, step, player, scope,
                    //propslot.bindObj
                    ((StackSlot)slot).propBindObj
                    , v);

            }

        }


        public static void _doPropAssigning(ClassPropertyGetter prop,StackFrame frame,
            OpStep step,Player player,IRunTimeScope scope,
            ASBinCode.rtData.rtObject bindobj,IRunTimeValue v)
        {
            do
            {

                
                if (prop.setter == null)
                {
                    frame.throwError(
                        new error.InternalError(step.token, "Illegal write to read-only property ",
                        new ASBinCode.rtData.rtString("Illegal write to read-only property "))
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

                var setter = ClassMemberFinder.find(prop._class, prop.setter.name, finder);
                if (setter == null || setter.bindField != prop.setter)
                {
                    frame.throwError(
                        new error.InternalError(step.token, "Illegal write to read-only property ",
                        new ASBinCode.rtData.rtString("Illegal write to read-only property "))
                        );
                    break;
                }

                //***读取setter***
                var func = ((ClassMethodGetter)setter.bindField).getValue(
                    bindobj.objScope
                    );
                //***调用设置器***

                var funCaller = new FunctionCaller(player, frame, step.token);
                funCaller.function = (ASBinCode.rtData.rtFunction)func;
                funCaller.loadDefineFromFunction();
                funCaller.createParaScope();
                funCaller.pushParameter(v, 0);
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
