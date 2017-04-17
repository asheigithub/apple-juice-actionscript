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

            if (idx < 0 || idx >= vector.innnerList.Count )
            {
                frame.throwError(new error.InternalError(step.token,
                    "The index "+idx+" is out of range "+ vector.innnerList.Count +".",
                    new rtString("The index " + idx + " is out of range " + vector.innnerList.Count + ".")
                    ));
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
            else
            {
                idx = TypeConverter.ConvertToNumber(idxvalue, frame, step.token);
            }

            if (double.IsNaN(idx))
            {
                frame.throwError(
                    new error.InternalError(
                        step.token,
                        "索引" + idxvalue + "不能转为int",
                        new rtString("索引" + idxvalue + "不能转为int")
                        )
                    );
            }
            else
            {
                int index = (int)idx;

                if (index < 0 || index >= vector.innnerList.Count)
                {
                    frame.throwError(new error.InternalError(step.token,
                        "The index " + index + " is out of range " + vector.innnerList.Count + ".",
                        new rtString("The index " + index + " is out of range " + vector.innnerList.Count + ".")
                        ));
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

            o.innnerList.Add((IRunTimeValue)step.arg2.getValue(scope).Clone());

            frame.endStep(step);
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
                        vector_data.vector_type > RunTimeDataType.unknown &&
                        value.rtType > RunTimeDataType.unknown &&
                        TypeConverter.testImplicitConvert(value.rtType,vector_data.vector_type,classfinder)

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
