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
                    
                    ISLOT lintoslot = ((ILeftValue)step.arg2).getISlot(rtObj.objScope);
                    if (lintoslot == null)
                    {
                        frame.throwError((new error.InternalError(step.token,
                            "没有获取到类成员数据"
                            )));
                    }
                    if (lintoslot is ClassPropertyGetter.PropertySlot)
                    {
                        slot.propGetSet = (ClassPropertyGetter)step.arg2;
                        slot.propBindObj = rtObj;
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

        public static void exec_method(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
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
                    ISLOT lintoslot = ((ClassMethodGetter)step.arg2).getISlot(rtObj.objScope);
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
                if (!(obj is rtObject))
                {
                    frame.throwOpException(step.token, step.opCode);
                    frame.endStep();
                    return;
                }
                rtObject rtObj = (rtObject)obj;
                //string name = ((rtString)step.arg2.getValue(scope)).value;

                var v2 = step.arg2.getValue(scope);
                if (v2.rtType == RunTimeDataType.rt_string)
                {
                    _exec_dot_name(rtObj, ((rtString)v2).value, step, frame, scope, player);
                }
                else
                {
                    BlockCallBackBase cb = new BlockCallBackBase();
                    cb.setCallBacker(_convert_callbacker);
                    cb.scope = scope;
                    cb.step = step;

                    object[] args = new object[5];
                    args[0] = rtObj;
                    args[1] = frame;
                    args[2] = player;

                    cb.args = args;

                    //**获取name
                    OpCast.CastValue(step.arg2.getValue(scope), RunTimeDataType.rt_string, frame, step.token, scope
                        ,
                        frame._tempSlot1, cb
                        );

                }
            }

        }

        private static void _convert_callbacker(BlockCallBackBase sender,object args)
        {
            object[] a = (object[])sender.args;

            StackFrame frame = (StackFrame)a[1];
            var nv = TypeConverter.ConvertToString( frame._tempSlot1.getValue(),frame,sender.step.token);

            _exec_dot_name((rtObject)a[0], nv, sender.step, frame, sender.scope, (Player)a[2]);

        }

        private static void _exec_dot_name(rtObject rtObj,string name,OpStep step,StackFrame frame,IRunTimeScope scope,Player player)
        {
            do
            {

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
                                DynamicPropertySlot heapslot = new DynamicPropertySlot(rtObj, true);
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
                            ISLOT linkto = ((ILeftValue)member.bindField).getISlot(rtObj.objScope);
                            slot.linkTo(linkto);
                            if (linkto is ClassPropertyGetter.PropertySlot)
                            {
                                slot.propBindObj = rtObj;
                                slot.propGetSet = (ClassPropertyGetter)member.bindField;
                            }
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




        /// <summary>
        /// []访问。需要动态检查是否是数组
        /// </summary>
        /// <param name="player"></param>
        /// <param name="frame"></param>
        /// <param name="step"></param>
        /// <param name="scope"></param>
        public static void exec_bracket_access(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
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
                if (!(obj is rtObject))
                {
                    frame.throwOpException(step.token, step.opCode);
                    frame.endStep();
                    return;
                }
                rtObject rtObj = (rtObject)obj;

                if (true) //****检查如果不是数组
                {
                    exec_dot_byname(player, frame, step, scope);
                }
                else
                {
                    frame.throwError(
                        new error.InternalError(
                        step.token, "数值未实现",
                        new rtString("数组未实现"
                        )
                        )
                        );
                    frame.endStep();
                }
            }

        }

    }
}
