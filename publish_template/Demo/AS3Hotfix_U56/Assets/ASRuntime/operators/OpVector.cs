using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpVector
    {
        public static void exec_AccessorBind(StackFrame frame, OpStep step, RunTimeScope scope)
        {
			RunTimeValueBase obj = step.arg1.getValue(scope, frame);
			if (rtNull.nullptr.Equals(obj))
			{
				frame.throwError(

						step.token, 1009, "Cannot access a property or method of a null object reference."

						);
				frame.endStep();
				return;
			}


			ASBinCode.rtti.Vector_Data vector =
                (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)obj).value).hosted_object;

            int idx = TypeConverter.ConvertToInt(step.arg2.getValue(scope, frame));

            if (idx < 0 || idx > vector.innnerList.Count )
            {
                frame.throwError(step.token,1125,
                    "The index "+idx+" is out of range "+ vector.innnerList.Count +".");
            }

            StackSlotAccessor reg = (StackSlotAccessor)step.reg;

            if (idx == vector.innnerList.Count)
            {
                if (vector.isFixed || !reg._isassigntarget)
                {
                    frame.throwError(step.token,1125,
                    "The index " + idx + " is out of range " + vector.innnerList.Count + "."
                    );
                    frame.endStep(step);
                    return;
                }
                else
                {
                    vector.innnerList.Add(TypeConverter.getDefaultValue(vector.vector_type).getValue(null,null));
                }
            }

            

            StackSlot slot = (StackSlot)step.reg.getSlot(scope, frame);
            if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete)
            {
                slot._cache_vectorSlot.idx = idx;
                slot._cache_vectorSlot.vector_data = vector;

                slot.linkTo(slot._cache_vectorSlot);
            }
            else
            {
                slot.directSet(vector.innnerList[idx]);
            }

            frame.endStep(step);
        }


		public static void exec_GetValue(StackFrame frame, OpStep step, RunTimeScope scope)
		{
			ASBinCode.rtti.Vector_Data vector =
			   (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)step.arg1.getValue(scope, frame)).value).hosted_object;

			int idx = TypeConverter.ConvertToInt(step.arg2.getValue(scope, frame));

			if (idx < 0 || idx >= vector.innnerList.Count)
			{
				frame.throwError(step.token, 1125,
					"The index " + idx + " is out of range " + vector.innnerList.Count + ".");
				frame.endStep(step);
			}
			else
			{
				step.reg.getSlot(scope, frame).directSet(vector.innnerList[idx]);
				frame.endStepNoError();
			}
		}


        public static void exec_AccessorBind_ConvertIdx(StackFrame frame, OpStep step, RunTimeScope scope)
        {
			RunTimeValueBase obj = step.arg1.getValue(scope, frame);
			if (rtNull.nullptr.Equals(obj))
			{
				frame.throwError(

						step.token, 1009, "Cannot access a property or method of a null object reference."

						);
				frame.endStep();
				return;
			}
			ASBinCode.rtti.Vector_Data vector =
                (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((ASBinCode.rtData.rtObjectBase)obj).value).hosted_object;


            var idxvalue = step.arg2.getValue(scope, frame);

            double idx = double.NaN;

            if (idxvalue.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(idxvalue.rtType, frame.player.swc, out ot))
                {
                    var v = TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)idxvalue);
                    idx = TypeConverter.ConvertToNumber(v);
                }
            }
            else if(idxvalue.rtType == RunTimeDataType.rt_string 
                || idxvalue.rtType==RunTimeDataType.rt_int 
                || idxvalue.rtType == RunTimeDataType.rt_number
                || idxvalue.rtType == RunTimeDataType.rt_uint
                )
            {
                idx = TypeConverter.ConvertToNumber(idxvalue);
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

                StackSlotAccessor reg = (StackSlotAccessor)step.reg;

                if (idx == vector.innnerList.Count)
                {
                    if (vector.isFixed || !reg._isassigntarget)
                    {
                        frame.throwError(step.token,
                            1125,
                        "The index " + idx + " is out of range " + vector.innnerList.Count + ".");
                        frame.endStep(step);
                        return;
                    }
                    else
                    {
                        vector.innnerList.Add(TypeConverter.getDefaultValue(vector.vector_type).getValue(null,null));
                    }
                }


                StackSlot slot = (StackSlot)step.reg.getSlot(scope, frame);

                if (reg._isassigntarget || reg._hasUnaryOrShuffixOrDelete)
                {
                    slot._cache_vectorSlot.idx = index;
                    slot._cache_vectorSlot.vector_data = vector;

                    slot.linkTo(slot._cache_vectorSlot);
                }
                else
                {
                    slot.directSet(vector.innnerList[index]);
                }
            }


            frame.endStep(step);

        }

        public static void exec_push(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var o = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)step.arg1.getValue(scope, frame)).value).hosted_object;

            o.innnerList.Add((RunTimeValueBase)step.arg2.getValue(scope, frame).Clone());//直接对容器赋值，必须Clone

			//frame.endStep(step);
			frame.endStepNoError();
        }


        public static void exec_initfromdata(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var player = frame.player;
            RunTimeValueBase initdata = step.arg2.getValue(scope, frame);
            int classrttype = ((rtInt)step.arg1.getValue(scope, frame)).value;
            while (true)
            {
                if (initdata.rtType == classrttype)
                {
                    step.reg.getSlot(scope, frame).directSet(initdata);
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
                    var cls = ((rtObjectBase)initdata).value._class;
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
			//frame.instanceCreator = new InstanceCreator(player, frame, step.token, _class);
			frame.activeInstanceCreator(step.token, _class);
            frame.instanceCreator.step = step;
            frame.instanceCreator.token = step.token;

            if (_class.constructor != null)
            {
                if (!frame.instanceCreator.prepareConstructorArgements())
                {
                    return;
                }
            }

            BlockCallBackBase cb = frame.player.blockCallBackPool.create();
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

            StackFrame frame = (StackFrame)sender.args;
            sender.step.reg.getSlot(sender.scope, frame).directSet(
                vector);

            var step = sender.step;
            
            RunTimeScope scope = sender.scope;

            RunTimeValueBase initdata = step.arg2.getValue(scope, frame);

            if (initdata.rtType == RunTimeDataType.rt_array)
            {
                _pusharray(
                    (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)vector).value).hosted_object,
                     frame, step, scope
                    );
            }
            else
            {
                _pushVector(   
                    (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)vector).value).hosted_object
                    , frame, step, scope);
            }

        }

        public static void exec_pushVector(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var o = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)step.arg1.getValue(scope, frame)).value).hosted_object;

            _pushVector(o, frame, step, scope);
        }

        public static void _pushVector(ASBinCode.rtti.Vector_Data o,  StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var arr = step.arg2.getValue(scope, frame);
            if (arr.rtType == RunTimeDataType.rt_null)
            {
                frame.throwCastException(step.token, RunTimeDataType.rt_null,
                    step.arg1.getValue(scope, frame).rtType
                    );
                frame.endStep(step);
            }
            else
            {
                ASBinCode.rtti.Vector_Data array = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)step.arg2.getValue(scope, frame)).value).hosted_object;
                if (array.innnerList.Count == 0)
                {
                    frame.endStep(step);

                }
                else
                {
                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                    cb.step = step;
                    cb.args = frame;
                    cb.setCallBacker(_allpushed);

                    pushAllElementToVector(o, array.innnerList, frame, step.token, scope, cb);
                }
            }
        }


        public static void exec_pusharray(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            var o = (ASBinCode.rtti.Vector_Data)((ASBinCode.rtti.HostedObject)((rtObjectBase)step.arg1.getValue(scope, frame)).value).hosted_object;
            _pusharray(o, frame, step, scope);
        }

        public static void _pusharray( ASBinCode.rtti.Vector_Data o , StackFrame frame, OpStep step, RunTimeScope scope)
        {

            var arr = step.arg2.getValue(scope, frame);
            if (arr.rtType == RunTimeDataType.rt_null)
            {
                frame.throwCastException(step.token, RunTimeDataType.rt_null,
                    step.arg1.getValue(scope, frame).rtType
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
                    BlockCallBackBase cb = frame.player.blockCallBackPool.create();
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
            List<RunTimeValueBase> datalist, StackFrame frame, SourceToken token,
            RunTimeScope scope,
            IBlockCallBack callbacker
            )
        {
            BlockCallBackBase convertCb = frame.player.blockCallBackPool.create();
            convertCb._intArg = 0;
            convertCb.setCallBacker(_convertCB);

            object[] args = convertCb.cacheObjects; //new object[6];
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
                var list = (List<RunTimeValueBase>)a[1];
                vd.innnerList.Add((RunTimeValueBase)v); 

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
                        (RunTimeScope)a[3], ((StackFrame)a[5])._tempSlot1,
                        sender, false);
                }
            }

            
            
        }


        public sealed class vectorSLot : SLOT
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

            //public sealed override bool isPropGetterSetter
            //{
            //    get
            //    {
            //        return false;
            //    }
            //}

            public sealed override void clear()
            {
                vector_data = null;
                idx = 0;
            }

			public override SLOT assign(RunTimeValueBase value, out bool success)
			{
				if (vector_data.vector_type != value.rtType
					&&
					!
					(
						//***检查子类关系****
						(vector_data.vector_type > RunTimeDataType.unknown &&
						value.rtType > RunTimeDataType.unknown &&
						(
						ClassMemberFinder.check_isinherits(value, vector_data.vector_type, classfinder)
						||
						ClassMemberFinder.check_isImplements(value, vector_data.vector_type, classfinder)
						)
						)
						||
						(
							vector_data.vector_type > RunTimeDataType.unknown &&
							value.rtType == RunTimeDataType.rt_null
						)
						||
						(
							vector_data.vector_type == RunTimeDataType.rt_array &&
							value.rtType == RunTimeDataType.rt_null
						)
						||
						(
							vector_data.vector_type == RunTimeDataType.rt_function &&
							value.rtType == RunTimeDataType.rt_null
						)
						||
						(
							vector_data.vector_type == RunTimeDataType.rt_string &&
							value.rtType == RunTimeDataType.rt_null
						)
					)
					)
				{
					//return false;
					success = false;
					return this;
				}
				else
				{
					switch (value.rtType)
					{
						case RunTimeDataType.rt_int:
							{
								((rtInt)vector_data.innnerList[idx]).value = ((rtInt)value).value;
								success = true;
								return this;
							}
						case RunTimeDataType.rt_uint:
							{
								((rtUInt)vector_data.innnerList[idx]).value = ((rtUInt)value).value;
								success = true;
								return this;
							}
						case RunTimeDataType.rt_number:
							{
								((rtNumber)vector_data.innnerList[idx]).value = ((rtNumber)value).value;
								success = true;
								return this;
							}
						case RunTimeDataType.rt_string:
							{
								((rtString)vector_data.innnerList[idx]).value = ((rtString)value).value;
								success = true;
								return this;
							}
						case RunTimeDataType.rt_function:
							{
								((rtFunction)vector_data.innnerList[idx]).CopyFrom((rtFunction)value);
								success = true;
								return this;
							}
						default:
							break;
					}

					if (value.rtType > RunTimeDataType.unknown)
					{
						var vd = vector_data.innnerList[idx];
						if (vd != null)
						{
							var cls = classfinder.getClassByRunTimeDataType(value.rtType);
							if (cls.isLink_System)
							{
								ASBinCode.rtti.LinkSystemObject link = (ASBinCode.rtti.LinkSystemObject)((rtObjectBase)vd).value;

								if (cls.isStruct)
								{
									link.CopyStructData((ASBinCode.rtti.LinkSystemObject)((rtObjectBase)value).value);
								}
								else
								{
									((rtObjectBase)vd).value = ((rtObjectBase)value).value;
								}
								success = true;
								return this;
							}
						}
					}

					vector_data.innnerList[idx] = (RunTimeValueBase)value.Clone(); //对容器的直接赋值，需要Clone																				   //return true;
					success = true;
					return this;
				}

			}

			public sealed override bool directSet(RunTimeValueBase value)
            {
				//如果是++,--等可能会调用到这个,直接返回false即可
				return false;
                //throw new NotImplementedException();
            }

            public sealed override RunTimeValueBase getValue()
            {
                return vector_data.innnerList[idx];
                //throw new NotImplementedException();
            }

            public sealed override void setValue(rtUndefined value)
            {
                throw new EngineException();
            }

            public sealed override void setValue(rtNull value)
            {
                throw new EngineException();
            }

            public sealed override void setValue(int value)
            {
                throw new EngineException();
            }

            public sealed override void setValue(string value)
            {
                throw new EngineException();
            }

            public sealed override void setValue(uint value)
            {
                throw new EngineException();
            }

            public sealed override void setValue(double value)
            {
                throw new EngineException();
            }

            public sealed override void setValue(rtBoolean value)
            {
                throw new EngineException();
            }
        }

    }
}
