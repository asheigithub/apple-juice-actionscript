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
        public static void exec_dot(Player player, StackFrame frame, OpStep step, RunTimeScope scope)
        {
            RunTimeValueBase obj = step.arg1.getValue(scope);
            if (rtNull.nullptr.Equals(obj))
            {
                frame.throwError(
                    
                        step.token,1009, "Cannot access a property or method of a null object reference."
                            
                        );
                
            }
            else
            {
                rtObject rtObj =
                    (rtObject)obj;
                
                StackSlot slot = step.reg.getISlot(scope) as StackSlot;
                if (slot != null)
                {
                    
                    SLOT lintoslot = ((ILeftValue)step.arg2).getISlot(rtObj.objScope);
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
                        if (step.arg1 is SuperPointer)
                        {
                            slot.superPropBindClass = ((SuperPointer)step.arg1).superClass;
                        }
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

        public static void exec_method(Player player, StackFrame frame, OpStep step, RunTimeScope scope)
        {
            RunTimeValueBase obj = step.arg1.getValue(scope);

            if (rtNull.nullptr.Equals(obj))
            {
                frame.throwError(
                     
                        step.token,1009, "Cannot access a property or method of a null object reference."
                           
                        );

            }
            else
            {
                rtObject rtObj =
                    (rtObject)obj;

                StackSlot slot = step.reg.getISlot(scope) as StackSlot;
                if (slot != null)
                {
                    SLOT lintoslot;// = ((ClassMethodGetter)step.arg2).getISlot(rtObj.objScope);
                    if (step.arg1 is SuperPointer)
                    {
                        lintoslot = ((MethodGetterBase)step.arg2).getSuperSlot(rtObj.objScope, ((SuperPointer)step.arg1).superClass );
                    }
                    else
                    {
                        lintoslot = ((MethodGetterBase)step.arg2).getVirtualSlot(rtObj.objScope );
                    }

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

        public static void exec_dot_byname(Player player, StackFrame frame, OpStep step, RunTimeScope scope)
        {
            RunTimeValueBase obj = step.arg1.getValue(scope);
            if (rtNull.nullptr.Equals(obj))
            {
                frame.throwError(
                    
                        step.token,1009, "Cannot access a property or method of a null object reference."
                        );
                frame.endStep(step);
            }
            else
            {
                if (!(obj is rtObject))
                {
                    var objtype = obj.rtType;
                    if (objtype < RunTimeDataType.unknown
                        &&
                        player.swc.primitive_to_class_table[objtype] != null
                        )
                    {
                        //***转换为对象***

                        OpCast.Primitive_to_Object(obj, frame, step.token, scope, frame._tempSlot1,
                            step, _primitive_toObj);

                        return;
                    }
                    else
                    {
                        frame.throwOpException(step.token, step.opCode);
                        frame.endStep();
                        return;
                    }
                }

                rtObject rtObj = (rtObject)obj;
                //string name = ((rtString)step.arg2.getValue(scope)).value;
                _loadname_dot(rtObj, step, frame, scope, player);
            }

        }

        private static void _loadname_dot(rtObject rtObj,OpStep step,StackFrame frame, RunTimeScope scope,Player player)
        {
            var v2 = step.arg2.getValue(scope);
            
            //**先转换为基础类型***
            if (v2.rtType > RunTimeDataType.unknown)
            {
                var cls = ((rtObject)v2).value._class;
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(cls, out ot))
                {
                    v2 = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)v2);
                }
            }

            if (v2.rtType == RunTimeDataType.rt_string)
            {
                _exec_dot_name(rtObj, ((rtString)v2).value, step, frame, scope, player);
            }
            else
            {
                if (rtObj.value is DictionaryObject)
                {
                    if (v2.rtType == RunTimeDataType.rt_boolean ||
                       v2.rtType == RunTimeDataType.rt_int ||
                       v2.rtType == RunTimeDataType.rt_number ||
                       v2.rtType == RunTimeDataType.rt_uint ||
                       v2.rtType == RunTimeDataType.rt_void ||
                       v2.rtType == RunTimeDataType.rt_null
                        )
                    {
                        _exec_dot_name(rtObj,TypeConverter.ConvertToString(v2,null,null) , step, frame, scope, player);
                        return;
                    }

                    //***访问动态类型
                    DictionaryObject dict = (DictionaryObject)rtObj.value;
                    var key = new DictionaryKey(v2);
                    if (!dict.isContainsKey(key))
                    {
                        if (((Register)step.reg)._isassigntarget)
                        {
                            DictionarySlot dictslot = new DictionarySlot(rtObj);
                            dictslot._key = key;
                            dictslot.directSet(rtUndefined.undefined);
                            dictslot.propertyIsEnumerable = true;
                            dict.createKeyValue(key, dictslot);
                        }
                        else
                        {
                            step.reg.getISlot(scope).directSet(rtUndefined.undefined);
                            frame.endStep(step);
                            return;
                        }
                    }

                    var slot = dict.getValue(key);
                    StackSlot dslot = step.reg.getISlot(scope) as StackSlot;
                    if (dslot != null)
                    {
                        dslot.linkTo(slot);
                    }
                    else
                    {
                        frame.throwError((new error.InternalError(step.token,
                             "dot操作结果必然是一个StackSlot"
                             )));
                    }
                    frame.endStep(step);

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
                        frame._tempSlot1, cb, false
                        );
                }
            }
        }


        private static void _convert_callbacker(BlockCallBackBase sender,object args)
        {
            object[] a = (object[])sender.args;

            StackFrame frame = (StackFrame)a[1];
            var nv = TypeConverter.ConvertToString( frame._tempSlot1.getValue(),frame,sender.step.token);

            _exec_dot_name((rtObject)a[0], nv==null?"null":nv, sender.step, frame, sender.scope, (Player)a[2]);

        }

        /// <summary>
        /// 从原型链中查找对象
        /// </summary>
        /// <returns></returns>
        public static DynamicObject findInProtoType(DynamicObject dobj,string name,StackFrame frame,SourceToken token,out bool haserror)
        {
            haserror = false;
            //***从原型链中查找对象***
            while (dobj != null && !dobj.hasproperty(name))
            {
                if (dobj._prototype_ != null
                    //&&
                    //dobj._prototype_.value is DynamicObject
                    )
                {
                    var protoObj = dobj._prototype_;
                    //****_prototype_的类型，只可能是Function对象或Class对象 Class对象尚未实现
                    if (protoObj._class.classid == frame.player.swc.FunctionClass.classid) //Function 
                    {
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[1].getValue()).value;
                    }
                    else if (protoObj._class.classid == 1) //搜索到根Object
                    {
                        //***根Object有继承自Class的prototype,再没有就没有了
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[0].getValue()).value;
                        if (!dobj.hasproperty(name))
                        {
                            dobj = (DynamicObject)((rtObject)protoObj.memberData[3].getValue()).value;
                            
                            if (!dobj.hasproperty(name))
                            {
                                dobj = null;
                            }
                        }
                        //dobj = null;
                    }
                    else if (protoObj._class.staticClass == null)
                    {
                        //**
                        dobj = (DynamicObject)((rtObject)protoObj.memberData[0].getValue()).value;
                        if (!dobj.hasproperty(name))
                        {
                            dobj = protoObj;
                        }

                    }
                    else
                    {
                        haserror = true;
                        frame.throwError((new error.InternalError(token,
                             "遭遇了异常的_prototype_"
                             )));
                        break;
                    }
                }
                else
                {
                    dobj = null;
                }
            }

            if (dobj == null || haserror)
            {
                return null;
            }
            else
            {
                return dobj;
            }
        }


        private static void _exec_dot_name(rtObject rtObj,string name,OpStep step,StackFrame frame, RunTimeScope scope,Player player)
        {
            do
            {

                if (rtObj.value is Global_Object)
                {
                    Global_Object gobj = (Global_Object)rtObj.value;

                    if (!gobj.hasproperty(name))//propSlot == null)
                    {
                        frame.throwError(
                        
                           step.token,1009, rtObj.ToString() + "找不到" + name);

                        break;
                    }
                    else
                    {
                        SLOT propSlot = gobj[name];
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
                    //*****检查是否是数组*****
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(rtObj.rtType, player.swc, out ot))
                    {
                        if (ot == RunTimeDataType.rt_array)
                        {
                            RunTimeValueBase obj = TypeConverter.ObjectImplicit_ToPrimitive(rtObj);
                            if (_assess_array(obj, frame, step, scope))
                            {
                                return;
                            }
                        }
                    }
                    


                    //***从对象中查找***
                    CodeBlock block = player.swc.blocks[scope.blockId];
                    Class finder;
                    if (block.isoutclass)
                    {
                        finder = player.swc.classes[block.define_class_id];
                        //if (rtObj.value._class.mainClass != null
                        //    &&
                        //    rtObj.value._class.mainClass.outscopeblockid
                        //        == block.id)
                        //{
                        //    finder = rtObj.value._class;
                        //}
                        //else
                        //{
                        //    finder = null;
                        //}
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

                            bool haserror;
                            dobj = findInProtoType(dobj, name, frame, step.token, out haserror);
                            if (haserror)
                            {
                                break;
                            }

                            StackSlot dslot = step.reg.getISlot(scope) as StackSlot;
                            if (dslot != null)
                            {
                                if (dobj == null || !dobj.hasproperty(name))
                                {
                                    if (((Register)step.reg)._isassigntarget)
                                    {

                                        //原型链中也不存在对象
                                        dobj = (DynamicObject)rtObj.value;
                                        DynamicPropertySlot heapslot = new DynamicPropertySlot(rtObj, true);
                                        heapslot._propname = name;
                                        heapslot.directSet(rtUndefined.undefined);
                                        heapslot.propertyIsEnumerable = true;
                                        dobj.createproperty(name, heapslot);
                                    }
                                    else
                                    {
                                        dslot.directSet(rtUndefined.undefined);
                                        break;
                                    }
                                }

                                linkProtoTypeMember(dobj, rtObj, player, dslot, name);
                            }
                            else
                            {
                                frame.throwError((new error.InternalError(step.token,
                                     "dot操作结果必然是一个StackSlot"
                                     )));
                            }


                            break;
                        }
                        else
                        {
                            //***从Class定义的原型链中查找
                            var dobj = (DynamicObject)
                                player.static_instance[ rtObj.value._class.staticClass.classid].value ;

                            dobj = (DynamicObject)((rtObject)dobj.memberData[0].getValue()).value;
                            if (!dobj.hasproperty(name))
                            {

                                dobj = ((DynamicObject)
                                    player.static_instance[rtObj.value._class.staticClass.classid].value);

                                bool haserror;
                                dobj = findInProtoType(dobj, name, frame, step.token, out haserror);
                                if (haserror)
                                {
                                    break;
                                }
                            }
                            
                            if (dobj != null)
                            {
                                StackSlot dslot = step.reg.getISlot(scope) as StackSlot;
                                if (dslot != null)
                                {
                                    linkProtoTypeMember(dobj, rtObj, player, dslot, name);
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
                    }

                    if (member == null)
                    {
                        frame.throwError(
                        
                           step.token,0, rtObj.ToString() + "找不到" + name
                           
                           );

                        break;
                    }

                    if (member.isConstructor)
                    {
                        frame.throwError(
                        
                           step.token,1009, rtObj.ToString() + "找不到" + name
                           );

                        break;
                    }

                    {
                        StackSlot slot = step.reg.getISlot(scope) as StackSlot;
                        if (slot != null)
                        {
                            SLOT linkto;// = ((ILeftValue)member.bindField).getISlot(rtObj.objScope);

                            if (step.arg1 is SuperPointer)
                            {
                                if (member.bindField is MethodGetterBase)
                                {
                                    linkto = ((MethodGetterBase)member.bindField).getSuperSlot(rtObj.objScope, ((SuperPointer)step.arg1).superClass);
                                }
                                else
                                {
                                    linkto = ((ILeftValue)member.bindField).getISlot(rtObj.objScope);
                                }
                            }
                            else
                            {
                                if (member.bindField is MethodGetterBase)
                                {
                                    linkto = ((MethodGetterBase)member.bindField).getVirtualSlot(rtObj.objScope);
                                }
                                else
                                {
                                    linkto = ((ILeftValue)member.bindField).getISlot(rtObj.objScope);
                                }
                            }

                            slot.linkTo(linkto);
                            if (linkto is ClassPropertyGetter.PropertySlot)
                            {
                                slot.propBindObj = rtObj;
                                slot.propGetSet = (ClassPropertyGetter)member.bindField;
                                if (step.arg1 is SuperPointer)
                                {
                                    slot.superPropBindClass = ((SuperPointer)step.arg1).superClass;
                                }
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

        private static void linkProtoTypeMember(DynamicObject dobj,rtObject rtObj,Player player,StackSlot dslot,string name)
        {
            SLOT v = dobj[name];
            if (!ReferenceEquals(dobj, rtObj.value))
            {
                if (v.getValue().rtType == RunTimeDataType.rt_function)
                {
                    ObjectMemberSlot tempslot = new ObjectMemberSlot(rtObj);
                    tempslot.directSet(v.getValue());
                    v = tempslot;
                }
                else if (v.getValue().rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType tout;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(v.getValue().rtType,
                        player.swc, out tout))
                    {
                        if (tout == RunTimeDataType.rt_function)
                        {
                            ObjectMemberSlot tempslot = new ObjectMemberSlot(rtObj);
                            tempslot.directSet(
                                TypeConverter.ObjectImplicit_ToPrimitive(
                                (rtObject)v.getValue()));

                            v = tempslot;
                        }
                    }
                }

                prototypeSlot pslot = dslot._cache_prototypeSlot; //new prototypeSlot(rtObj, name, v);
                pslot._protoRootObj = rtObj;
                pslot._protoname = name;
                pslot.findSlot = v;

                v = pslot;
            }

            dslot.linkTo(v);
        }


        /// <summary>
        /// []访问。需要动态检查是否是数组
        /// </summary>
        /// <param name="player"></param>
        /// <param name="frame"></param>
        /// <param name="step"></param>
        /// <param name="scope"></param>
        public static void exec_bracket_access(Player player, StackFrame frame, OpStep step, RunTimeScope scope)
        {
            RunTimeValueBase obj = step.arg1.getValue(scope);
            if (rtNull.nullptr.Equals(obj))
            {
                frame.throwError(
                    
                        step.token,1009, "Cannot access a property or method of a null object reference."
                        
                        );

            }
            else
            {
                if (obj is rtArray)
                {
                    if (_assess_array(obj, frame, step, scope))
                    {
                        return;
                    }
                }
                else if (obj is rtObject)
                {
                    if (player.swc.dict_Vector_type.ContainsKey(((rtObject)obj).value._class))
                    {
                        //说明是Vector数组
                        OpVector.exec_AccessorBind_ConvertIdx(player, frame, step, scope);
                        return;
                    }
                }
                

                if (!(obj is rtObject))
                {
                    var objtype = obj.rtType;

                    if (objtype < RunTimeDataType.unknown
                        &&
                        player.swc.primitive_to_class_table[objtype] != null
                        )
                    {
                        //***转换为对象***

                        OpCast.Primitive_to_Object(obj, frame, step.token, scope, frame._tempSlot1,
                            step, _primitive_toObj);

                        return;
                    }
                    else if (obj == rtUndefined.undefined)
                    {
                        frame.throwError(step.token,1009,
                            "A term is undefined and has no properties"
                           
                            );
                        frame.endStep();
                        return;
                    }
                    else
                    {
                        frame.throwOpException(step.token, step.opCode);
                        frame.endStep();
                        return;
                    }
                }
                _primitive_toObj(obj, null, frame, step, scope);
            }

        }

        private static void _primitive_toObj(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v_temp, StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (v1.rtType < RunTimeDataType.unknown)
            {
                frame.throwError(
                    new error.InternalError(
                    step.token, "基本数据类型没有转换为引用类型",
                    new rtString("基本数据类型没有转换为引用类型"
                    )
                    )
                    );
                return;
            }

            rtObject rtObj = (rtObject)v1;

            if (true) //****检查如果不是数组
            {
                _loadname_dot(rtObj, step, frame, scope, frame.player);
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


        private static bool _assess_array( RunTimeValueBase obj, StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var idx = step.arg2.getValue(scope);
            var number = TypeConverter.ConvertToNumber(idx, frame, step.token);
            if (!(double.IsNaN(number) || double.IsInfinity(number)))
            {
                if ((int)number >= 0)
                {
                    if ((int)number >= ((rtArray)obj).innerArray.Count)
                    {
                        var toadd = ((rtArray)obj).innerArray;
                        int addnum = (int)number+1 - toadd.Count;

                        while (addnum>0)
                        {
                            addnum--;
                            toadd.Add(rtUndefined.undefined);
                        }
                    }


                    //***访问数组的内容***
                    StackSlot stackSlot= (StackSlot)step.reg.getISlot(scope);
                    //stackSlot.fromArray = (rtArray)obj;
                    //stackSlot.fromArrayIndex = (int)number;

                    //step.reg.getISlot(scope).directSet(((rtArray)obj).innerArray[(int)number]);
                    stackSlot.linkTo(new arraySlot((rtArray)obj, (int)number));

                    frame.endStep();
                    return true;
                }
            }
            return false;
        }

        internal sealed class arraySlot : SLOT
        {
            rtArray array;
            int idx;
            public arraySlot(rtArray array,int idx)
            {
                this.array = array;
                this.idx = idx;
            }

            public sealed override bool isPropGetterSetter
            {
                get
                {
                    return false;
                }
            }

            public sealed override void clear()
            {
                //throw new NotImplementedException();
            }

            public sealed override bool directSet(RunTimeValueBase value)
            {
                array.innerArray[idx] = (RunTimeValueBase)value.Clone(); //对数组的直接赋值，需要Clone
                return true;
                //throw new NotImplementedException();
            }

            public sealed override RunTimeValueBase getValue()
            {
                return array.innerArray[idx];
                //throw new NotImplementedException();
            }

            public sealed override void setValue(rtUndefined value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(rtNull value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(int value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(string value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(uint value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(double value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(rtBoolean value)
            {
                throw new NotImplementedException();
            }
        }


        internal sealed class prototypeSlot : SLOT
        {
            internal rtObject _protoRootObj;
            internal string _protoname;

            internal SLOT findSlot;

            public prototypeSlot(rtObject _protoRootObj, string _protoname,
                SLOT findSlot
                )
            {
                this._protoRootObj = _protoRootObj;
                this._protoname = _protoname;
                this.findSlot = findSlot;
            }


            public sealed override bool isPropGetterSetter
            {
                get
                {
                    return false;
                }
            }

            public sealed override void clear()
            {
                _protoRootObj = null;
                _protoname = null;
                findSlot = null;

                //throw new NotImplementedException();
            }

            public sealed override bool directSet(RunTimeValueBase value)
            {
                //throw new NotImplementedException();
                
                if (_protoRootObj.value._class.dynamic)
                {
                    DynamicPropertySlot heapslot = new DynamicPropertySlot(_protoRootObj, true);
                    heapslot._propname = _protoname;
                    heapslot.directSet(value);

                    ((DynamicObject)_protoRootObj.value).createproperty(_protoname, heapslot);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }

            public sealed override RunTimeValueBase getValue()
            {
                return findSlot.getValue();
                //throw new NotImplementedException();
            }

            public sealed override void setValue(rtUndefined value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(rtNull value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(int value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(string value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(uint value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(double value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(rtBoolean value)
            {
                throw new NotImplementedException();
            }
        }

    }
}
