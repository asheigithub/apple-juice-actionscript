using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpVector
    {
        public static void exec_AccessorBind(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            ASBinCode.rtti.Vector_Data vector =
                (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((ASBinCode.rtData.rtObject)step.arg1.getValue(scope)).value).hosted_object;

            int idx = TypeConverter.ConvertToInt(step.arg2.getValue(scope), frame, step.token);

            if (idx < 0 || idx > vector.innnerList.Count )
            {
                frame.throwError(step.token,1125,
                    "The index "+idx+" is out of range "+ vector.innnerList.Count +".");
            }
            if (idx == vector.innnerList.Count)
            {
                if (vector.isFixed || !((Register)step.reg)._isassigntarget)
                {
                    frame.throwError(step.token,1125,
                    "The index " + idx + " is out of range " + vector.innnerList.Count + "."
                    );
                }
                else
                {
                    vector.innnerList.Add(TypeConverter.getDefaultValue(vector.vector_type).getValue(null));
                }
            }



            StackSlot slot = (StackSlot)step.reg.getISlot(scope);
            slot._cache_vectorSlot.idx = idx;
            slot._cache_vectorSlot.vector_data = vector;

            slot.linkTo(slot._cache_vectorSlot);


            frame.endStep(step);
        }


        public static void exec_AccessorBind_ConvertIdx(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            ASBinCode.rtti.Vector_Data vector =
                (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((ASBinCode.rtData.rtObject)step.arg1.getValue(scope)).value).hosted_object;


            var idxvalue = step.arg2.getValue(scope);

            double idx = double.NaN;

            if (idxvalue.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(idxvalue.rtType, player.swc, out ot))
                {
                    var v = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)idxvalue);
                    idx = TypeConverter.ConvertToNumber(v, frame, step.token);
                }
            }
            else if(idxvalue.rtType == RunTimeDataType.rt_string 
                || idxvalue.rtType==RunTimeDataType.rt_int 
                || idxvalue.rtType == RunTimeDataType.rt_number
                || idxvalue.rtType == RunTimeDataType.rt_uint
                )
            {
                idx = TypeConverter.ConvertToNumber(idxvalue, frame, step.token);
            }

            if (double.IsNaN(idx))
            {
                frame.throwError(
                    
                        step.token,0,
                        "索引" + idxvalue + "不能转为int"
                        
                    );
            }
            else
            {
                int index = (int)idx;

                if (index < 0 || index > vector.innnerList.Count)
                {
                    frame.throwError(step.token,1125,
                        "The index " + index + " is out of range " + vector.innnerList.Count + ".");
                }
                if (idx == vector.innnerList.Count)
                {
                    if (vector.isFixed || !((Register)step.reg)._isassigntarget)
                    {
                        frame.throwError(step.token,
                            1125,
                        "The index " + idx + " is out of range " + vector.innnerList.Count + ".");
                    }
                    else
                    {
                        vector.innnerList.Add(TypeConverter.getDefaultValue(vector.vector_type).getValue(null));
                    }
                }


                StackSlot slot = (StackSlot)step.reg.getISlot(scope);
                slot._cache_vectorSlot.idx = index;
                slot._cache_vectorSlot.vector_data = vector;

                slot.linkTo(slot._cache_vectorSlot);
            }


            frame.endStep(step);

        }

        public static void exec_push(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            var o = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)step.arg1.getValue(scope)).value).hosted_object;

            o.innnerList.Add((IRunTimeValue)step.arg2.getValue(scope).Clone());//直接对容器赋值，必须Clone

            frame.endStep(step);
        }


        public static void exec_initfromdata(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            IRunTimeValue initdata = step.arg2.getValue(scope);
            int classrttype = ((rtInt)step.arg1.getValue(scope)).value;
            while (true)
            {
                if (initdata.rtType == classrttype)
                {
                    step.reg.getISlot(scope).directSet(initdata);
                    frame.endStep(step);
                    return;
                }
                else if (initdata.rtType == RunTimeDataType.rt_array)
                {
                    break;
                }
                else if (
                    initdata.rtType > RunTimeDataType.unknown
                    )
                {
                    var cls = ((rtObject)initdata).value._class;
                    if (player.swc.dict_Vector_type.ContainsKey(cls))
                    {
                        break;
                    }
                }

                frame.throwCastException(step.token, initdata.rtType, classrttype);
                frame.endStep(step);
                return;


            }



            var _class = player.swc.getClassByRunTimeDataType(classrttype);
            frame.instanceCreator = new InstanceCreator(player, frame, step, step.token, _class);
            frame.instanceCreator.step = step;
            frame.instanceCreator.token = step.token;

            if (_class.constructor != null)
            {
                frame.instanceCreator.prepareConstructorArgements();
            }

            BlockCallBackBase cb = new BlockCallBackBase();
            cb.args = frame;
            cb.setCallBacker(objcreated);
            cb.scope = scope;
            cb.step = step;

            frame.instanceCreator.callbacker = cb;
            frame.instanceCreator.createInstance();

            return;

        }

        private static void objcreated(BlockCallBackBase sender, object args)
        {
            var vector = ((StackFrame)sender.args).instanceCreator.objectResult;


            sender.step.reg.getISlot(sender.scope).directSet(
                vector);

            var step = sender.step;
            StackFrame frame = (StackFrame)sender.args;
            IRunTimeScope scope = sender.scope;

            IRunTimeValue initdata = step.arg2.getValue(scope);

            if (initdata.rtType == RunTimeDataType.rt_array)
            {
                _pusharray(
                    (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)vector).value).hosted_object,
                    frame.player, frame, step, scope
                    );
            }
            else
            {
                _pushVector(   
                    (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)vector).value).hosted_object
                    , frame.player, frame, step, scope);
            }

        }

        public static void exec_pushVector(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            var o = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)step.arg1.getValue(scope)).value).hosted_object;

            _pushVector(o, player, frame, step, scope);
        }

        public static void _pushVector(ASBinCode.rtti.Vector_Data o, Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            var arr = step.arg2.getValue(scope);
            if (arr.rtType == RunTimeDataType.rt_null)
            {
                frame.throwCastException(step.token, RunTimeDataType.rt_null,
                    step.arg1.getValue(scope).rtType
                    );
                frame.endStep(step);
            }
            else
            {
                ASBinCode.rtti.Vector_Data array = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)step.arg2.getValue(scope)).value).hosted_object;
                if (array.innnerList.Count == 0)
                {
                    frame.endStep(step);

                }
                else
                {
                    BlockCallBackBase cb = new BlockCallBackBase();
                    cb.step = step;
                    cb.args = frame;
                    cb.setCallBacker(_allpushed);

                    pushAllElementToVector(o, array.innnerList, frame, step.token, scope, cb);
                }
            }
        }


        public static void exec_pusharray(Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {
            var o = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObject)step.arg1.getValue(scope)).value).hosted_object;
            _pusharray(o, player, frame, step, scope);
        }

        public static void _pusharray( ASBinCode.rtti.Vector_Data o ,Player player, StackFrame frame, OpStep step, IRunTimeScope scope)
        {

            var arr = step.arg2.getValue(scope);
            if (arr.rtType == RunTimeDataType.rt_null)
            {
                frame.throwCastException(step.token, RunTimeDataType.rt_null,
                    step.arg1.getValue(scope).rtType
                    );
                frame.endStep(step);
            }
            else
            {
                rtArray array = (rtArray)arr;
                if (array.innerArray.Count == 0)
                {
                    frame.endStep(step);

                }
                else
                {
                    BlockCallBackBase cb = new BlockCallBackBase();
                    cb.step = step;
                    cb.args = frame;
                    cb.setCallBacker(_allpushed);

                    pushAllElementToVector(o, array.innerArray, frame, step.token, scope, cb);
                }
            }
        }

        private static void _allpushed(BlockCallBackBase sender,object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        } 


        public static void pushAllElementToVector(ASBinCode.rtti.Vector_Data vd,
            List<IRunTimeValue> datalist, StackFrame frame, SourceToken token, 
            IRunTimeScope scope,
            IBlockCallBack callbacker
            )
        {
            BlockCallBackBase convertCb = new BlockCallBackBase();
            convertCb._intArg = 0;
            convertCb.setCallBacker(_convertCB);

            object[] args = new object[6];
            args[0] = vd;
            args[1] = datalist;
            args[2] = token;
            args[3] = scope;
            args[4] = callbacker;
            args[5] = frame;
            convertCb.args = args;

            OpCast.CastValue(datalist[convertCb._intArg ], 
                vd.vector_type, frame, token, scope, frame._tempSlot1, 
                convertCb, false);
        }

        private static void _convertCB(BlockCallBackBase sender,object args)
        {
            object[] a = (object[])sender.args;

            if (sender.isSuccess)
            {
                ASBinCode.rtti.Vector_Data vd = (ASBinCode.rtti.Vector_Data)a[0];
                var v = ((StackFrame)a[5])._tempSlot1.getValue().Clone();//必须Clone
                var list = (List<IRunTimeValue>)a[1];
                vd.innnerList.Add((IRunTimeValue)v); 

                sender._intArg = sender._intArg + 1;

                if (sender._intArg >= list.Count)
                {
                    IBlockCallBack cb = (IBlockCallBack)a[4];
                    cb.call(cb.args);
                }
                else
                {
                    OpCast.CastValue(list[sender._intArg],
                        vd.vector_type, (StackFrame)a[5], (SourceToken)a[2],
                        (IRunTimeScope)a[3], ((StackFrame)a[5])._tempSlot1,
                        sender, false);
                }
            }

            
            
        }


        public class vectorSLot : ISLOT
        {
            public IClassFinder classfinder;
            public ASBinCode.rtti.Vector_Data vector_data;
            public int idx;
            public vectorSLot(ASBinCode.rtti.Vector_Data vector_data, int idx,IClassFinder classfinder)
            {
                this.vector_data = vector_data;
                this.idx = idx;
                this.classfinder = classfinder;
            }

            public bool isPropGetterSetter
            {
                get
                {
                    return false;
                }
            }

            public void clear()
            {
                vector_data = null;
                idx = 0;
            }

            public bool directSet(IRunTimeValue value)
            {
                if (vector_data.vector_type != value.rtType
                    &&
                    !
                    (
                        //***检查子类关系****
                        (vector_data.vector_type > RunTimeDataType.unknown &&
                        value.rtType > RunTimeDataType.unknown &&
                        (
                        ClassMemberFinder.check_isinherits(value,vector_data.vector_type, classfinder)
                        ||
                        ClassMemberFinder.check_isImplements(value, vector_data.vector_type, classfinder)
                        )
                        )
                        ||
                        (
                            vector_data.vector_type > RunTimeDataType.unknown &&
                            value.rtType == RunTimeDataType.rt_null
                        )

                    )
                    )
                {
                    return false;
                }

                vector_data.innnerList[idx] = (IRunTimeValue)value.Clone(); //对容器的直接赋值，需要Clone
                return true;
                //throw new NotImplementedException();
            }

            public IRunTimeValue getValue()
            {
                return vector_data.innnerList[idx];
                //throw new NotImplementedException();
            }

            public void setValue(rtUndefined value)
            {
                throw new NotImplementedException();
            }

            public void setValue(rtNull value)
            {
                throw new NotImplementedException();
            }

            public void setValue(int value)
            {
                throw new NotImplementedException();
            }

            public void setValue(string value)
            {
                throw new NotImplementedException();
            }

            public void setValue(uint value)
            {
                throw new NotImplementedException();
            }

            public void setValue(double value)
            {
                throw new NotImplementedException();
            }

            public void setValue(rtBoolean value)
            {
                throw new NotImplementedException();
            }
        }

    }
}
