using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtti;

namespace ASRuntime.operators
{
    class OpLogic
    {
        private static bool _exec_is_instance_v1_isprimivate(ASBinCode.rtti.Class cls,ASBinCode.RunTimeValueBase v1,ASBinCode.RunTimeScope scope, ASBinCode.OpStep step)
        {
            ASBinCode.RunTimeDataType ot;
            if (TypeConverter.Object_CanImplicit_ToPrimitive(cls.instanceClass, out ot))
            {
                if (v1.rtType == ot)
                {
                    return true;
                    //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    if (ot == ASBinCode.RunTimeDataType.rt_number)
                    {
                        if (v1.rtType == ASBinCode.RunTimeDataType.rt_int ||
                            v1.rtType == ASBinCode.RunTimeDataType.rt_uint
                            )
                        {
                            return true;
                            //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                        }
                        else
                        {
                            return false;
                            //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                        }
                    }
                    else if (ot == ASBinCode.RunTimeDataType.rt_int)
                    {
                        if (v1.rtType == ASBinCode.RunTimeDataType.rt_number
                            )
                        {
                            double v = TypeConverter.ConvertToNumber(v1, null, null);
                            if (Math.Floor(v) == v && Math.Ceiling(v) == v)
                            {
                                return true;
                                //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                            }
                            else
                            {
                                return false;
                                //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            }
                        }
                        else if (v1.rtType == ASBinCode.RunTimeDataType.rt_uint)
                        {
                            return true;
                            //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                        }
                        else
                        {
                            return false;
                            //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                        }

                    }
                    else if (ot == ASBinCode.RunTimeDataType.rt_uint)
                    {
                        if (v1.rtType == ASBinCode.RunTimeDataType.rt_number
                            )
                        {
                            double v = TypeConverter.ConvertToNumber(v1, null, null);
                            if (Math.Floor(v) == v && Math.Ceiling(v) == v && v >= 0)
                            {
                                return true;
                                //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                            }
                            else
                            {
                                return false;
                                //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            }
                        }
                        else if (v1.rtType == ASBinCode.RunTimeDataType.rt_int)
                        {
                            int v = TypeConverter.ConvertToInt(v1, null, null);
                            if (v >= 0)
                            {
                                return true;
                                //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                            }
                            else
                            {
                                return false;
                                //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            }
                        }
                        else
                        {
                            return false;
                            //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                        }
                    }
                    else
                    {
                        return false;
                        //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                    }
                }

            }
            else
            {
                return false;
                //step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
            }
        }

        private static void _as_is(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope,
            RunTimeValueBase iftrue,RunTimeValueBase iffalse
            )
        {
            var v2 = step.arg2.getValue(scope);
            if (v2.rtType < ASBinCode.RunTimeDataType.unknown ||
                ((ASBinCode.rtData.rtObject)v2).value._class.staticClass != null
                )
            {
                frame.throwError(step.token, 1041, "The right-hand side of operator must be a class.");
            }
            else
            {
                var cls = ((ASBinCode.rtData.rtObject)v2).value._class;

                var v1 = step.arg1.getValue(scope);

                if (v1.rtType > ASBinCode.RunTimeDataType.unknown
                    )
                {
                    ASBinCode.RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v1).value._class, out ot))
                    {
                        v1 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v1);
                    }
                }

                if (v1.rtType < ASBinCode.RunTimeDataType.unknown)
                {
                    if (_exec_is_instance_v1_isprimivate(cls, v1, scope, step))
                    {
                        step.reg.getISlot(scope).directSet(iftrue);
                    }
                    else
                    {
                        step.reg.getISlot(scope).directSet(iffalse);
                    }
                }
                else
                {
                    if (ASBinCode.ClassMemberFinder.isInherits
                        (((ASBinCode.rtData.rtObject)v1).value._class,
                        cls.instanceClass)
                        ||
                        ASBinCode.ClassMemberFinder.isImplements(
                        ((ASBinCode.rtData.rtObject)v1).value._class,
                        cls.instanceClass
                        ))
                    {
                        step.reg.getISlot(scope).directSet(iftrue);
                    }
                    else
                    {
                        step.reg.getISlot(scope).directSet(iffalse);
                    }
                }


            }
        }


        public static void exec_AS(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            _as_is(frame, step, scope, step.arg1.getValue(scope), ASBinCode.rtData.rtNull.nullptr);

            frame.endStep(step);
        }


        public static void exec_IS(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            _as_is(frame, step, scope, ASBinCode.rtData.rtBoolean.True, ASBinCode.rtData.rtBoolean.False);

            frame.endStep(step);
        }

        public static void exec_instanceof(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v2 = step.arg2.getValue(scope);
            if (v2.rtType == ASBinCode.RunTimeDataType.rt_function)
            {
                OpCast.Primitive_to_Object(v2, frame, step.token, scope, frame._tempSlot1,
                    step, _tofunction_callbacker);
            }
            else
            {
                _tofunction_callbacker(v2, null, frame, step, scope);
            }
        }

        private static void _tofunction_callbacker(RunTimeValueBase v11, RunTimeValueBase v21, 
            StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var v2 = v11;

            if (v2.rtType < ASBinCode.RunTimeDataType.unknown ||
                (
                    ((ASBinCode.rtData.rtObject)v2).value._class.staticClass != null
                    &&
                    !ReferenceEquals(((ASBinCode.rtData.rtObject)v2).value._class, frame.player.swc.FunctionClass)
                )
                )
            {
                frame.throwError(step.token, 1041, "The right-hand side of instanceof must be a class or function.");
            }
            else
            {
                var cls = ((ASBinCode.rtData.rtObject)v2).value._class;
                if (cls.isInterface)
                {
                    step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                }
                else
                {
                    var v1 = step.arg1.getValue(scope);
                    if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
                    {
                        ASBinCode.RunTimeDataType ot;
                        if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v1).value._class, out ot))
                        {
                            v1 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v1);
                        }
                    }

                    if (v1.rtType < ASBinCode.RunTimeDataType.unknown)
                    {
                        if (_exec_is_instance_v1_isprimivate(cls, v1, scope, step))
                        {
                            step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                        }
                        else
                        {
                            step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                        }
                    }
                    else
                    {
                        if (!ReferenceEquals(((ASBinCode.rtData.rtObject)v2).value._class, frame.player.swc.FunctionClass))
                        {
                            if (ASBinCode.ClassMemberFinder.isInherits(
                                ((ASBinCode.rtData.rtObject)v1).value._class,
                                cls.instanceClass)
                                )
                            {
                                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                            }
                            else
                            {
                                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            }

                        }
                        else
                        {
                            //***检查原型链***
                            ASBinCode.rtti.Object obj = ((ASBinCode.rtData.rtObject)v1).value;

                            ASBinCode.rtti.DynamicObject _proto = null;

                            if (obj is ASBinCode.rtti.DynamicObject)
                            {
                                ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj;
                                _proto = dobj._prototype_;
                            }
                            else
                            {
                                _proto = (ASBinCode.rtti.DynamicObject)frame.player.static_instance[obj._class.staticClass.classid].value;
                            }

                            ASBinCode.rtti.DynamicObject v2obj = (ASBinCode.rtti.DynamicObject)
                                ((ASBinCode.rtData.rtObject)v2).value;

                            bool found = false;
                            while (_proto != null)
                            {
                                if (ReferenceEquals(_proto, v2obj))
                                {
                                    found = true;
                                    break;
                                }
                                //_proto = _proto._prototype_;

                                if (_proto._class.classid == frame.player.swc.FunctionClass.classid) //Function 
                                {
                                    var o =
                                        (ASBinCode.rtti.DynamicObject)
                                        ((ASBinCode.rtData.rtObject)_proto.memberData[1].getValue()).value;
                                    _proto = o._prototype_;

                                }
                                else if (_proto._class.classid == 1)
                                {
                                    _proto = null;
                                }
                                else
                                {
                                    frame.throwError((new error.InternalError(step.token,
                                         "遭遇了异常的_prototype_"
                                         )));
                                    break;
                                }
                            }

                            if (found)
                            {
                                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                            }
                            else
                            {
                                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            }
                        }
                    }

                }

            }

            frame.endStep(step);
        }



        public static void exec_In(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v1 = step.arg1.getValue(scope);

            var v2 = step.arg2.getValue(scope);

            if (v2.rtType == RunTimeDataType.rt_null)
            {
                frame.throwError(

                        step.token, 1009, "Cannot access a property or method of a null object reference."

                        );
                frame.endStep(step);
            }
            else if (v2.rtType == RunTimeDataType.rt_void)
            {
                frame.throwError(
                        step.token, 1010, "A term is undefined and has no properties."

                        );
                frame.endStep(step);
            }

            if (v2.rtType < RunTimeDataType.unknown)
            {
                OpCast.Primitive_to_Object(v2, frame, step.token, scope, frame._tempSlot1, step, _toObject_callbacker);
            }
            else
            {
                _toObject_callbacker(v2, null, frame, step, scope);
            }
            
        }

        private static void _check_inrange(RunTimeValueBase v1,OpStep step,StackFrame frame, RunTimeScope scope,
            IList<RunTimeValueBase> list
            )
        {
            if (v1.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType it;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v1).value._class, out it))
                {
                    v1 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v1);
                }
            }

            if (v1.rtType == RunTimeDataType.rt_int ||
            v1.rtType == RunTimeDataType.rt_uint ||
            v1.rtType == RunTimeDataType.rt_number ||
            v1.rtType == RunTimeDataType.rt_string
            )
            {
                double idx = TypeConverter.ConvertToNumber(v1, null, null);
                if (double.IsNaN(idx) || double.IsInfinity(idx))
                {
                    step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                    frame.endStep(step);
                    return;
                }
                else
                {
                    int idxx = (int)idx;
                    //ASBinCode.rtData.rtArray arr = 
                    //    (ASBinCode.rtData.rtArray)TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v2);
                    if (idxx >= 0 && idxx < list.Count) //arr.innerArray.Count)
                    {
                        step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                    }
                    else
                    {
                        step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                    }
                    frame.endStep(step);
                    return;
                }
            }
            else if (v1.rtType < RunTimeDataType.unknown)
            {
                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                frame.endStep(step);
                return;
            }
            else
            {
                BlockCallBackBase cb_tostr_arr = new BlockCallBackBase();
                cb_tostr_arr.scope = scope;
                cb_tostr_arr.step = step;
                object[] args = new object[2];
                args[0] = frame;
                args[1] = list; //(ASBinCode.rtData.rtArray)TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v2);
                cb_tostr_arr.args = args;
                cb_tostr_arr.setCallBacker(_cb_tostr_arr);

                //***转字符串***
                OpCast.CastValue(v1, RunTimeDataType.rt_string, frame,
                    step.token, scope, frame._tempSlot1,
                    cb_tostr_arr
                    , false);

                return;
            }
        }


        private static void _toObject_callbacker(RunTimeValueBase v11, RunTimeValueBase v21,
            StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var v2 = v11;
            if (v2.rtType < RunTimeDataType.unknown)
            {
                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                frame.endStep(step);
                return;
            }

            var v1 = step.arg1.getValue(scope);

            RunTimeDataType ot;
            if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v2).value._class, out ot))
            {
                if (ot == RunTimeDataType.rt_array)
                {
                    ASBinCode.rtData.rtArray arr = 
                        (ASBinCode.rtData.rtArray)TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v2);

                    _check_inrange(v1, step, frame, scope, arr.innerArray);
                    return;
                }
            }

            if (frame.player.swc.dict_Vector_type.ContainsKey(((ASBinCode.rtData.rtObject)v2).value._class))
            {
                //和数组同样处理
                var list=((Vector_Data)((HostedObject)((ASBinCode.rtData.rtObject)v2).value).hosted_object).innnerList;
                _check_inrange(v1, step, frame, scope, list);

                return;
            }
            

            if (((ASBinCode.rtData.rtObject)v2).value is ASBinCode.rtti.DictionaryObject)
            {
                if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
                {
                    //***字典对象，允许用Object做Key***
                    DictionaryObject dict = (DictionaryObject)(((ASBinCode.rtData.rtObject)v2).value);
                    var key = new DictionaryKey(v1);
                    if (!dict.isContainsKey(key))
                    {
                        step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                        frame.endStep(step);
                        return;
                    }
                    else
                    {
                        step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                        frame.endStep(step);
                        return;
                    }
                }
            }

            //***将key转为字符串***
            BlockCallBackBase cb_tostr_obj=new BlockCallBackBase();
            cb_tostr_obj.scope = scope;
            cb_tostr_obj.step = step;
            object[] a = new object[2];
            a[0] = frame;
            a[1] = v2;
            cb_tostr_obj.setCallBacker(_cb_tostr_obj);
            cb_tostr_obj.args = a;

            OpCast.CastValue(v1, RunTimeDataType.rt_string, frame, step.token, scope, frame._tempSlot1, cb_tostr_obj, false);


        }

        private static void _cb_tostr_obj(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            StackFrame frame = (StackFrame)a[0];
            ASBinCode.rtData.rtObject v2 = (ASBinCode.rtData.rtObject)a[1];
            RunTimeValueBase vidx = frame._tempSlot1.getValue();
            string name = TypeConverter.ConvertToString(vidx,null,null);
            var step = sender.step;
            var scope = sender.scope;

            if (v2.value is Global_Object)
            {
                Global_Object gobj = (Global_Object)v2.value;
                if (!gobj.hasproperty(name))
                {
                    step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                    frame.endStep(step);
                    return;
                }
                else
                {
                    step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                    frame.endStep(step);
                    return;
                }
            }
            else
            {
                var cls = v2.value._class;
                
                var member = ClassMemberFinder.find(cls, name, null);
                if (member == null)
                {
                    if (v2.value._class.dynamic) //如果是动态类型
                    {
                        DynamicObject dobj = (DynamicObject)v2.value;

                        bool haserror;
                        dobj = OpAccess_Dot.findInProtoType(dobj, name, frame, step.token, out haserror);
                        if (haserror)
                        {
                            step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            frame.endStep(step);
                            return;
                        }

                        if (dobj != null)
                        {
                            step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                            frame.endStep(step);
                            return;
                        }
                        else
                        {
                            step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            frame.endStep(step);
                            return;
                        }

                    }
                    else
                    {
                        var dobj = (DynamicObject)
                                frame.player.static_instance[v2.value._class.staticClass.classid].value;

                        dobj = (DynamicObject)((ASBinCode.rtData.rtObject)dobj.memberData[0].getValue()).value;
                        if (!dobj.hasproperty(name))
                        {

                            dobj = ((DynamicObject)
                                frame.player.static_instance[v2.value._class.staticClass.classid].value);

                            bool haserror;
                            dobj = OpAccess_Dot.findInProtoType(dobj, name, frame, step.token, out haserror);
                            if (haserror)
                            {
                                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                                frame.endStep(step);
                                return;
                            }
                        }

                        if (dobj != null)
                        {
                            step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                            frame.endStep(step);
                            return;
                        }
                        else
                        {
                            step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                            frame.endStep(step);
                            return;
                        }

                    }
                }
                else
                {
                    step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                    frame.endStep(step);
                    return;
                }
            }

        }

        private static void _cb_tostr_arr(BlockCallBackBase sender, object args)
        {
            object[] a = (object[])sender.args;
            StackFrame frame = (StackFrame)a[0];
            IList<RunTimeValueBase> v2 = (IList<RunTimeValueBase>)a[1];
            RunTimeValueBase vidx = frame._tempSlot1.getValue();

            OpStep step = sender.step;
            var scope = sender.scope;

            double idx = TypeConverter.ConvertToNumber(vidx, null, null);
            if (double.IsNaN(idx) || double.IsInfinity(idx))
            {
                step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                frame.endStep(step);
                return;
            }
            else
            {
                int idxx = (int)idx;
                
                if (idxx >= 0 && idxx < v2.Count)
                {
                    step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).directSet(ASBinCode.rtData.rtBoolean.False);
                }
                frame.endStep(step);
                return;
            }

        }

        public static void execNOT(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = TypeConverter.ConvertToBoolean(step.arg1.getValue(scope),frame,step.token);

            if (object.ReferenceEquals(v, ASBinCode.rtData.rtBoolean.True))
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False );
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            frame.endStep(step);
        }

        


        public static void execGT_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {


            //ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            //ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);
            double a1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), null, null);
            double a2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), null, null);

            if (a1 > a2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }

            frame.endStep(step);
            
        }

        public static void execGE_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {


            //ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            //ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);
            double a1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), null, null);
            double a2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), null, null);

            if (a1 >= a2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }


            frame.endStep(step);
        }

        public static void execGT_Void(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _GTVoid_ValueOf_CallBacker);
        }
        private static void _GTVoid_ValueOf_CallBacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (
                (
                v1.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
                )
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)
                ||
                (
                needInvokeToString(v1,v2,frame.player)
                //v1.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
                //    )
                //||
                //(v2.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
                //)
                )
                )
            {

                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_GTVoid_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, cb
                    );

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 > n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }

        private static void _readTwoStringFromCallBacker(BlockCallBackBase sender,out string s1,out string s2)
        {
            
            {
                var rv = ((StackFrame)sender.args)._tempSlot1.getValue();
                if (rv.rtType == ASBinCode.RunTimeDataType.rt_null)
                {
                    s1 = null;
                }
                else
                {
                    s1 = (((ASBinCode.rtData.rtString)rv).valueString());
                }
            }
            
            {
                var rv = ((StackFrame)sender.args)._tempSlot2.getValue();
                if (rv.rtType == ASBinCode.RunTimeDataType.rt_null)
                {
                    s2 = null;
                }
                else
                {
                    s2 = (((ASBinCode.rtData.rtString)rv).valueString());
                }
            }
        }
        private static void _GTVoid_TwoString_Callbacker(BlockCallBackBase sender, object args)
        {
            string s1;
            string s2;
            _readTwoStringFromCallBacker(sender,out s1,out s2);

            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            else if (string.CompareOrdinal(s1, s2) > 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }




        public static void execGE_Void(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _GEVoid_ValueOf_Callbacker);
        }
        private static void _GEVoid_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            if (
                (
                v1.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
                )
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)
                ||
                //(
                needInvokeToString(v1,v2,frame.player)
                //v1.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
                //    )
                //||
                //(v2.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
                //)
                )
            {
                
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_GEVoid_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, cb);

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 >= n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }

        private static void _GEVoid_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1, s2;
            _readTwoStringFromCallBacker(sender, out s1, out s2);
            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else if (string.CompareOrdinal(s1, s2) >= 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }

            ((StackFrame)sender.args).endStep(sender.step);
        }


        //public static void execLT_IntInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.IRunTimeScope scope)
        //{
        //    ASBinCode.rtData.rtInt a1 = (ASBinCode.rtData.rtInt)step.arg1.getValue(scope);
        //    ASBinCode.rtData.rtInt a2 = (ASBinCode.rtData.rtInt)step.arg2.getValue(scope);
        //    if (a1.value < a2.value)
        //    {
        //        step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
        //    }
        //    else
        //    {
        //        step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
        //    }


        //    frame.endStep(step);
        //}

        public static void execLT_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {


            //ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            //ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);
            double a1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), null, null);
            double a2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), null, null);

            if (a1 < a2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }


            frame.endStep(step);
        }

        public static void execLE_NUM(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {


            //ASBinCode.rtData.rtNumber a1 = (ASBinCode.rtData.rtNumber)step.arg1.getValue(scope);
            //ASBinCode.rtData.rtNumber a2 = (ASBinCode.rtData.rtNumber)step.arg2.getValue(scope);
            double a1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), null, null);
            double a2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), null, null);

            if (a1 <= a2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execLT_VOID(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _LTVoid_ValueOf_Callbacker);
        }

        private static void _LTVoid_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (
               (
               v1.rtType == ASBinCode.RunTimeDataType.rt_string
               &&
               (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
               )
               ||
               v2.rtType == ASBinCode.RunTimeDataType.rt_string
               &&
               (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)

               ||
               needInvokeToString(v1, v2, frame.player)
               //(
               //v1.rtType > ASBinCode.RunTimeDataType.unknown
               //    &&
               //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
               //    )
               //||
               //(v2.rtType > ASBinCode.RunTimeDataType.unknown
               //    &&
               //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
               //)

               )
            {
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_LTVoid_TwoString_Callbacker);
                cb.scope = scope;
                cb.args = frame;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string, frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, cb);

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 < n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }

        private static void _LTVoid_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1;
            string s2;
            _readTwoStringFromCallBacker(sender, out s1, out s2);
            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            else if (string.CompareOrdinal(s1, s2) < 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }


        public static void execLE_VOID(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _LEVoid_ValueOf_Callbacker);
        }
        private static void _LEVoid_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (
                (
                v1.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v2.rtType == ASBinCode.RunTimeDataType.rt_string || v2.rtType == ASBinCode.RunTimeDataType.rt_null)
                )
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_string
                &&
                (v1.rtType == ASBinCode.RunTimeDataType.rt_string || v1.rtType == ASBinCode.RunTimeDataType.rt_null)
                ||
                needInvokeToString(v1, v2, frame.player)
                //(
                //v1.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v1.rtType, frame.player.swc)
                //    )
                //||
                //(v2.rtType > ASBinCode.RunTimeDataType.unknown
                //    &&
                //    !TypeConverter.ObjectImplicit_ToNumber(v2.rtType, frame.player.swc)
                //)
                )
            {
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_LEVoid_TwoString_Callbacker);
                cb.args = frame;
                cb.step = step;
                cb.scope = scope;

                OpCast.CastTwoValue(v1, v2, ASBinCode.RunTimeDataType.rt_string, frame, step.token,
                    scope, frame._tempSlot1, frame._tempSlot2, cb);

                return;
            }
            else
            {
                double n1 = TypeConverter.ConvertToNumber(v1, frame, step.token);
                double n2 = TypeConverter.ConvertToNumber(v2, frame, step.token);

                if (n1 <= n2)
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
            }
            frame.endStep(step);
        }
        private static void _LEVoid_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1, s2;
            _readTwoStringFromCallBacker(sender, out s1, out s2);

            if (s1 == null || s2 == null)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else if (string.CompareOrdinal(s1, s2) <= 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }


            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void execEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v1 = step.arg1.getValue(scope);
            var v2 = step.arg2.getValue(scope);

            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _EQ_ValueOf_Callbacker);
        }
        private static void _EQ_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            //if (TypeConverter.ObjectImplicit_ToNumber(v1))
            //{
            //    v1 = new ASBinCode.rtData.rtNumber(
            //        TypeConverter.ConvertToNumber(
                        
            //            TypeConverter.ObjectImplicit_ToPrimitive( (ASBinCode.rtData.rtObject)v1)
                        
            //            , frame, step.token));
            //}
            //if (TypeConverter.ObjectImplicit_ToNumber(v2))
            //{
            //    v2 = new ASBinCode.rtData.rtNumber(
            //        TypeConverter.ConvertToNumber(
            //            TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v2)
            //            , frame, step.token));
            //}


            if (needInvokeToString(v1, v2, frame.player))
            {
                //***转成字符串比较***
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_EQ_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2,
                    ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, cb);


                return;
            }
            else
            {
                if (testEquals(v1, v2, frame, step, scope))
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
                frame.endStep(step);
            }
        }

        private static void _EQ_TwoString_Callbacker(BlockCallBackBase sender,object args)
        {
            string s1, s2;_readTwoStringFromCallBacker(sender, out s1, out s2);

            if (string.CompareOrdinal(s1, s2) == 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void execNotEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v1 = step.arg1.getValue(scope);
            var v2 = step.arg2.getValue(scope);
            OpCast.InvokeTwoValueOf(v1, v2, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _NotEQ_ValueOf_Callbacker);
        }

        private static void _NotEQ_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            //***能转换基本类型的肯定已经转了
            //if (TypeConverter.ObjectImplicit_ToNumber(v1))
            //{
            //    v1 = new ASBinCode.rtData.rtNumber(TypeConverter.ConvertToNumber(
                    
            //        TypeConverter.ObjectImplicit_ToPrimitive( (ASBinCode.rtData.rtObject)v1)
                    
            //        , frame, step.token));
            //}
            //if (TypeConverter.ObjectImplicit_ToNumber(v2))
            //{
            //    v2 = new ASBinCode.rtData.rtNumber(
            //        TypeConverter.ConvertToNumber(
            //            TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v2)

            //            , frame, step.token));
            //}

            if (needInvokeToString(v1, v2, frame.player))//v1.rtType > ASBinCode.RunTimeDataType.unknown || v2.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                //***转成字符串比较***
                BlockCallBackBase cb = new BlockCallBackBase();
                cb.setCallBacker(_NOTEQ_TwoString_Callbacker);
                cb.args = frame;
                cb.scope = scope;
                cb.step = step;

                OpCast.CastTwoValue(v1, v2,
                    ASBinCode.RunTimeDataType.rt_string,
                    frame, step.token, scope,
                    frame._tempSlot1, frame._tempSlot2, cb);


                return;
            }
            else
            {
                if (!testEquals(step.arg1.getValue(scope), step.arg2.getValue(scope), frame, step, scope))
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
                }
                frame.endStep(step);
            }
        }

        private static void _NOTEQ_TwoString_Callbacker(BlockCallBackBase sender, object args)
        {
            string s1, s2; _readTwoStringFromCallBacker(sender, out s1, out s2);

            if (string.CompareOrdinal(s1, s2) != 0)
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                sender.step.reg.getISlot(sender.scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            ((StackFrame)sender.args).endStep(sender.step);
        }

        public static void execEQ_NumNum(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), frame, step.token);
            var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), frame, step.token);

            if (n1==n2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execNotEQ_NumNum(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var n1 = TypeConverter.ConvertToNumber(step.arg1.getValue(scope), frame, step.token);
            var n2 = TypeConverter.ConvertToNumber(step.arg2.getValue(scope), frame, step.token);

            if (n1 != n2)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execEQ_StrStr(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var n1 = ((ASBinCode.rtData.rtString)step.arg1.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg1.getValue(scope), frame, step.token);
            var n2 = ((ASBinCode.rtData.rtString)step.arg2.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg2.getValue(scope), frame, step.token);

            if (string.CompareOrdinal(n1,n2)==0)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static void execNotEQ_StrStr(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var n1 = ((ASBinCode.rtData.rtString)step.arg1.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg1.getValue(scope), frame, step.token);
            var n2 = ((ASBinCode.rtData.rtString)step.arg2.getValue(scope)).value; //TypeConverter.ConvertToString(step.arg2.getValue(scope), frame, step.token);

            if (string.CompareOrdinal(n1, n2) != 0)
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        public static bool StrictEqual(ASBinCode.RunTimeValueBase v1,ASBinCode.RunTimeValueBase v2)
        {
            if (ReferenceEquals(v1, v2))
            {
                return true;
            }

            ASBinCode.RunTimeDataType ot;
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v1).value._class, out ot))
                {
                    v1 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v1);
                }
            }
            if (v2.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v2).value._class, out ot))
                {
                    v2 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v2);
                }
            }
            if ((
                v1.rtType == ASBinCode.RunTimeDataType.rt_number || v1.rtType == ASBinCode.RunTimeDataType.rt_int
                ||
                v1.rtType == ASBinCode.RunTimeDataType.rt_uint
                )

                &&
                (
                v2.rtType == ASBinCode.RunTimeDataType.rt_number || v2.rtType == ASBinCode.RunTimeDataType.rt_int
                ||
                v2.rtType == ASBinCode.RunTimeDataType.rt_uint
                )
                )
            {
                double n1 = TypeConverter.ConvertToNumber(v1, null, null);
                double n2 = TypeConverter.ConvertToNumber(v2, null, null);

                if (n1 == n2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_string && v2.rtType == ASBinCode.RunTimeDataType.rt_string)
            {
                
                string s1 = TypeConverter.ConvertToString(v1, null, null);
                string s2 = TypeConverter.ConvertToString(v2, null, null);

                if (string.CompareOrdinal(s1, s2) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_function
                &&
                v2.rtType == ASBinCode.RunTimeDataType.rt_function
                )
            {
                ASBinCode.rtData.rtFunction obj1 = (ASBinCode.rtData.rtFunction)v1;
                ASBinCode.rtData.rtFunction obj2 = (ASBinCode.rtData.rtFunction)v2;

                return ASBinCode.rtData.rtFunction.isTypeEqual(obj1, obj2);

            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_array
                && v2.rtType == ASBinCode.RunTimeDataType.rt_array)
            {
                return v1.Equals(v2);
            }
            else if (v1.rtType > ASBinCode.RunTimeDataType.unknown
                &&
                v2.rtType > ASBinCode.RunTimeDataType.unknown
                )
            {
                ASBinCode.rtData.rtObject obj1 = (ASBinCode.rtData.rtObject)v1;
                ASBinCode.rtData.rtObject obj2 = (ASBinCode.rtData.rtObject)v2;

                return ReferenceEquals(obj1.value, obj2.value) && ReferenceEquals(obj1.objScope, obj2.objScope);

            }
            else
            {
                if (ReferenceEquals(v1, v2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private static bool  _execStrictEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            //strict equality 运算符仅针对数字类型（Number、int 和 uint）执行自动数据转换
            ASBinCode.RunTimeValueBase v1 = step.arg1.getValue(scope);
            ASBinCode.RunTimeValueBase v2 = step.arg2.getValue(scope);

            return StrictEqual(v1, v2);
        }

        public static void execStrictEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (_execStrictEQ(frame, step, scope))
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }
        public static void execStrictNotEQ(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (!_execStrictEQ(frame, step, scope))
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                step.reg.getISlot(scope).setValue(ASBinCode.rtData.rtBoolean.False);
            }
            frame.endStep(step);
        }

        private static bool needInvokeToString(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,Player player)
        {
            if ((v1.rtType < ASBinCode.RunTimeDataType.unknown && v2.rtType > ASBinCode.RunTimeDataType.unknown)
                ||
                (v1.rtType > ASBinCode.RunTimeDataType.unknown && v2.rtType < ASBinCode.RunTimeDataType.unknown)
                )
            {
                //***如果有任一类型为array或者是vector,则返回false***
                if (v1.rtType == ASBinCode.RunTimeDataType.rt_array
                    ||
                    v2.rtType == ASBinCode.RunTimeDataType.rt_array
                    ||
                    (v1.rtType>ASBinCode.RunTimeDataType.unknown
                        &&
                        player.swc.dict_Vector_type.ContainsKey( player.swc.getClassByRunTimeDataType(v1.rtType) )
                    )
                    ||
                    (v2.rtType > ASBinCode.RunTimeDataType.unknown
                        &&
                        player.swc.dict_Vector_type.ContainsKey(player.swc.getClassByRunTimeDataType(v2.rtType))
                    )
                    )
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        ///测试两个表达式是否相等。如果表达式相等，则结果为 true。 
        ///如果两个操作数的数据类型相匹配，则相等的定义取决于操作数的数据类型：
        ///如果 int、uint 和 Boolean 类型具有相同的值，则将其值视为相等。
        ///值相匹配的两个 Number 被视为相等，除非两个值都为 NaN。
        ///如果两个操作数的值均为 null 或 undefined，则将它们视为相等。
        ///如果字符串表达式具有相同的字符数，而且这些字符都相同，则这些字符串表达式相等。
        ///对于 XML 对象： 
        ///如果一个操作数是文本或属性节点，而另一个操作数具有简单的内容，则使用 toString() 方法可将两个操作数转换为字符串，如果生成的字符串相匹配，则将这两个操作数视为相等。 
        ///否则，仅当两个对象的限定名、特性和子属性都匹配时，才会被视为相等。
        ///如果 XMLList 对象具有相同数目的属性，并且属性的顺序和值都匹配，则可将其视为相等。
        ///对于 Namespace 对象，如果两个对象的 uri 属性相匹配，则其值被视为相等。
        ///对于 QName 对象，如果两个对象的 uri 属性相匹配，并且两个对象的 localName 属性也相匹配，则其值视为相等。
        ///表示对象、数组和函数的变量按引用进行比较。如果两个这样的变量引用同一个对象、数组或函数，则它们相等。而两个单独的数组即使具有相同数量的元素，也永远不会被视为相等。
        ///如果这两个操作数的数据类型不匹配，则结果为 false，但在以下情况下除外： 
        ///操作数的值为 undefined 和 null，在这种情况下结果为 true。
        ///自动数据类型转换将数据类型为 String、Boolean、int、uint 和 Number 的值转换为兼容的类型，并且转换后的值相等，在这种情况下，操作数被视为相等。
        ///一个操作数的类型为 XML，并且包含简单内容(hasSimpleContent() == true)，在使用 toString() 将这两个操作数均转换为字符串后，所生成的字符串相匹配。
        ///一个操作数的类型为 XMLList，并且满足以下任一条件： 
        ///XMLList 对象的 length 属性是 0，而另一个对象为 undefined。
        ///XMLList 对象的 length 属性为 1，XMLList 对象的一个元素与另一个操作数相匹配。
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool testEquals(ASBinCode.RunTimeValueBase v1,ASBinCode.RunTimeValueBase v2, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            
            if (ReferenceEquals(v1, v2))
            {
                return true;
            }

            ASBinCode.RunTimeDataType ot;
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v1).value._class, out ot))
                {
                    v1 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v1);
                }
            }
            if (v2.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                if (TypeConverter.Object_CanImplicit_ToPrimitive(((ASBinCode.rtData.rtObject)v2).value._class, out ot))
                {
                    v2 = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)v2);
                }
            }

            ASBinCode.RunTimeDataType t1 = v1.rtType;
            ASBinCode.RunTimeDataType t2 = v2.rtType;


            if (
                (
                t1 == ASBinCode.RunTimeDataType.rt_int
                || t1 == ASBinCode.RunTimeDataType.rt_uint || t1 == ASBinCode.RunTimeDataType.rt_boolean
                || t1 == ASBinCode.RunTimeDataType.rt_number
                )
                &&
                (t2 == ASBinCode.RunTimeDataType.rt_int
                || t2 == ASBinCode.RunTimeDataType.rt_uint || t2 == ASBinCode.RunTimeDataType.rt_boolean
                || t2 == ASBinCode.RunTimeDataType.rt_number
                )
                )
            {
                return TypeConverter.ConvertToNumber(v1, frame, step.token) == TypeConverter.ConvertToNumber(v2, frame, step.token);
            }
            else if (
                (t1 == ASBinCode.RunTimeDataType.rt_null || t1 == ASBinCode.RunTimeDataType.rt_void)
                &&
                (t2 == ASBinCode.RunTimeDataType.rt_null || t2 == ASBinCode.RunTimeDataType.rt_void)
                )
            {
                return true;
            }
            else if (t1 == ASBinCode.RunTimeDataType.rt_string && t2 == ASBinCode.RunTimeDataType.rt_string)
            {
                return string.CompareOrdinal(
                    ((ASBinCode.rtData.rtString)v1).value
                    ,
                    ((ASBinCode.rtData.rtString)v2).value
                    ) == 0;
            }
            else if (t1 == ASBinCode.RunTimeDataType.rt_string)
            {
                switch (t2)
                {
                    case ASBinCode.RunTimeDataType.rt_boolean:
                        if (v2.Equals(ASBinCode.rtData.rtBoolean.True))
                        {
                            return TypeConverter.ConvertToInt(v1, frame, step.token) == 1;
                        }
                        else
                        {
                            return TypeConverter.ConvertToInt(v1, frame, step.token) != 1;
                        }
                    case ASBinCode.RunTimeDataType.rt_int:
                        return TypeConverter.ConvertToInt(v1, frame, step.token) == TypeConverter.ConvertToInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_uint:
                        return TypeConverter.ConvertToUInt(v1, frame, step.token) == TypeConverter.ConvertToUInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_number:
                        return TypeConverter.ConvertToNumber(v1, frame, step.token) == TypeConverter.ConvertToNumber(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_string:
                        return string.CompareOrdinal(
                            ((ASBinCode.rtData.rtString)v1).value
                            ,
                            ((ASBinCode.rtData.rtString)v2).value
                            ) == 0;
                    case ASBinCode.RunTimeDataType.rt_void:
                        return false;
                    case ASBinCode.RunTimeDataType.rt_null:
                        return false;
                    case ASBinCode.RunTimeDataType.unknown:
                        return false;
                    default:
                        break;
                }
            }
            else if (t2 == ASBinCode.RunTimeDataType.rt_string)
            {
                switch (t1)
                {
                    case ASBinCode.RunTimeDataType.rt_boolean:
                        if (v1.Equals(ASBinCode.rtData.rtBoolean.True))
                        {
                            return TypeConverter.ConvertToInt(v2, frame, step.token) == 1;
                        }
                        else
                        {
                            return TypeConverter.ConvertToInt(v2, frame, step.token) != 1;
                        }
                    case ASBinCode.RunTimeDataType.rt_int:
                        return TypeConverter.ConvertToInt(v1, frame, step.token) == TypeConverter.ConvertToInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_uint:
                        return TypeConverter.ConvertToUInt(v1, frame, step.token) == TypeConverter.ConvertToUInt(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_number:
                        return TypeConverter.ConvertToNumber(v1, frame, step.token) == TypeConverter.ConvertToNumber(v2, frame, step.token);
                    case ASBinCode.RunTimeDataType.rt_string:
                        return string.CompareOrdinal(
                           ((ASBinCode.rtData.rtString)v1).value
                            ,
                            ((ASBinCode.rtData.rtString)v2).value
                            ) == 0;
                    case ASBinCode.RunTimeDataType.rt_void:
                        return false;
                    case ASBinCode.RunTimeDataType.rt_null:
                        return false;
                    case ASBinCode.RunTimeDataType.unknown:
                        return false;
                    default:
                        break;
                }
            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_function
                &&
                v2.rtType == ASBinCode.RunTimeDataType.rt_function
                )
            {
                ASBinCode.rtData.rtFunction obj1 = (ASBinCode.rtData.rtFunction)v1;
                ASBinCode.rtData.rtFunction obj2 = (ASBinCode.rtData.rtFunction)v2;

                return ASBinCode.rtData.rtFunction.isTypeEqual(obj1, obj2);

            }
            else if (v1.rtType == ASBinCode.RunTimeDataType.rt_array 
                && v2.rtType == ASBinCode.RunTimeDataType.rt_array)
            {
                return v1.Equals(v2);
            }
            else if (v1.rtType > ASBinCode.RunTimeDataType.unknown
                &&
                v2.rtType > ASBinCode.RunTimeDataType.unknown
                )
            {
                ASBinCode.rtData.rtObject obj1 = (ASBinCode.rtData.rtObject)v1;
                ASBinCode.rtData.rtObject obj2 = (ASBinCode.rtData.rtObject)v2;

                return ReferenceEquals(obj1.value, obj2.value) && ReferenceEquals(obj1.objScope, obj2.objScope);

            }
            return false;
        }


    }
}
