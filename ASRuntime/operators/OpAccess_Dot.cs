using ASBinCode;
using ASBinCode.rtData;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpAccess_Dot
    {
        public static void exec_dot(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            IRunTimeValue obj = step.arg1.getValue(scope);

            if (rtNull.nullptr.Equals(obj))
            {
                frame.throwError(
                    new error.InternalError(
                        step.token, "Cannot access a property or method of a null object reference.",
                            new ASBinCode.rtData.rtString("Cannot access a property or method of a null object reference.")
                        ));
                
            }
            else
            {
                rtObject rtObj =
                    (rtObject)obj;
                
                StackSlot slot = step.reg.getISlot(scope) as StackSlot;
                if (slot != null)
                {
                    ISLOT lintoslot = ((Field)step.arg2).getISlot(rtObj.objScope);
                    if (lintoslot == null)
                    {
                        frame.throwError((new error.InternalError(step.token,
                         "没有获取到类成员数据"
                         )));
                    }

                    slot.linkTo(lintoslot);
                }
                else
                {
                    frame.throwError((new error.InternalError(step.token,
                         "dot操作结果必然是一个StackSlot"
                         )));
                }

            }

            frame.endStep(step);
        }


        public static void exec_dot_byname(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            IRunTimeValue obj = step.arg1.getValue(scope);
            if (rtNull.nullptr.Equals(obj))
            {
                frame.throwError(
                    new error.InternalError(
                        step.token, "Cannot access a property or method of a null object reference.",
                        new rtString("Cannot access a property or method of a null object reference."
                        )
                        )
                        );

            }
            else
            {
                do
                {
                    rtObject rtObj = (rtObject)obj;
                    string name = ((rtString)step.arg2.getValue(scope)).value;

                    if (rtObj.value is Global_Object)
                    {
                        Global_Object gobj = (Global_Object)rtObj.value;
                        
                        if (!gobj.hasproperty(name))//propSlot == null)
                        {
                            frame.throwError(
                            new error.InternalError(
                               step.token, rtObj.ToString() + "找不到" + name,
                               new rtString(rtObj.ToString() + "找不到" + name)
                               ));

                            break;
                        }
                        else
                        {
                            ISLOT propSlot = gobj[name];
                            StackSlot slot = step.reg.getISlot(scope) as StackSlot;
                            if (slot != null)
                            {
                                slot.linkTo(propSlot);
                            }
                            else
                            {
                                frame.throwError((new error.InternalError(step.token,
                                     "dot操作结果必然是一个StackSlot"
                                     )));
                            }
                        }
                    }
                    else
                    {
                        //***从对象中查找***
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

                        var member = ClassMemberFinder.find(rtObj.value._class, name, finder);

                        if (member == null)
                        {
                            if (rtObj.value._class.dynamic) //如果是动态类型
                            {
                                DynamicObject dobj = (DynamicObject)rtObj.value;

                                if (!dobj.hasproperty(name))
                                {
                                    DynamicPropertySlot heapslot = new DynamicPropertySlot(rtObj,true);
                                    heapslot._propname = name;
                                    heapslot.directSet(rtUndefined.undefined);
                                    dobj.createproperty(name, heapslot);
                                }

                                StackSlot dslot = step.reg.getISlot(scope) as StackSlot;
                                if (dslot != null)
                                {
                                    dslot.linkTo(dobj[name]);
                                }
                                else
                                {
                                    frame.throwError((new error.InternalError(step.token,
                                         "dot操作结果必然是一个StackSlot"
                                         )));
                                }


                                break;
                            }
                        }

                        if (member == null)
                        {
                            frame.throwError(
                            new error.InternalError(
                               step.token, rtObj.ToString() + "找不到" + name,
                               new rtString(rtObj.ToString() + "找不到" + name)
                               ));

                            break;
                        }

                        if (member.isConstructor)
                        {
                            frame.throwError(
                            new error.InternalError(
                               step.token, rtObj.ToString() + "找不到" + name,
                               new rtString(rtObj.ToString() + "找不到" + name)
                               ));

                            break;
                        }

                        {
                            StackSlot slot = step.reg.getISlot(scope) as StackSlot;
                            if (slot != null)
                            {
                                slot.linkTo( member.bindField.getISlot(rtObj.objScope) );
                            }
                            else
                            {
                                frame.throwError((new error.InternalError(step.token,
                                     "dot操作结果必然是一个StackSlot"
                                     )));
                            }
                        }

                    }

                }
                while (false);

                frame.endStep(step);
            }

        }

    }
}
