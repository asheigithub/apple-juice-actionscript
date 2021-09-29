using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    /// <summary>
    /// 动态属性
    /// </summary>
    class OpDynamicProperty
    {
        public static void exec_delete(StackFrame frame,OpStep step, RunTimeScope scope)
        {
            {
                StackSlot slot = (StackSlot)((StackSlotAccessor)step.arg1).getSlot(scope, frame);

				var ls = slot.getLinkSlot();

                if (ls is DynamicPropertySlot)
                {
                    DynamicPropertySlot link = (DynamicPropertySlot)ls;
                    if (link._canDelete)
                    {
                        ((ASBinCode.rtti.DynamicObject)link.obj.value).deleteProperty(link._propname);
                    }
                    else
                    {
                        if (link.backup != null)
                        {
                            link.directSet(link.backup);
                        }
                    }

					
                }
                else if (ls is DictionarySlot)
                {
                    DictionarySlot link = (DictionarySlot)ls;
                    ((ASBinCode.rtti.DictionaryObject)link.obj.value).RemoveKey(link._key);

					
				}
                else if (ls is OpAccess_Dot.arraySlot) //(slot.fromArray != null)
                {
					//slot.directSet(rtUndefined.undefined);
					//bool success;
					//slot.assign(rtUndefined.undefined,out success);

					((OpAccess_Dot.arraySlot)ls).delete();


					//slot.fromArray.innerArray[slot.fromArrayIndex] = rtUndefined.undefined;
				}
                else if (ls is OpAccess_Dot.prototypeSlot)
                {   //原型链对象，不可删除
					
				}
                else if (ls is OpVector.vectorSLot)
                {
					//数组链接，跳过
					
				}
                else
                {
                    frame.throwError(
                        step.token, 0,
                                    "动态属性才能被delete"
                        );

                }

				slot.linkTo(null);

				frame.endStep(step);
            }
        }


        public static void exec_set_dynamic_prop(StackFrame frame, OpStep step, RunTimeScope scope)
        {
            ASBinCode.rtData.rtObjectBase obj = (ASBinCode.rtData.rtObjectBase)step.reg.getValue(scope, frame);
            ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;

            DynamicPropertySlot heapslot = new DynamicPropertySlot(obj, true,frame.player.swc.FunctionClass.getRtType());
            heapslot._propname = ((ASBinCode.rtData.rtString)step.arg1.getValue(scope, frame)).value;

            if (step.arg2 is MethodGetterBase)
            {
				var sf = ((MethodGetterBase)step.arg2).getMethod(scope);
                heapslot.directSet(sf ); ((rtFunction)sf).Clear();
			}
            else
            {
                heapslot.directSet(step.arg2.getValue(scope, frame));
            }

            //dobj.createOrReplaceproperty(heapslot._propname,heapslot);
            if (dobj.hasproperty(heapslot._propname))
            {
                dobj.deleteProperty(heapslot._propname);
            }
            dobj.createproperty(heapslot._propname, heapslot);
            //直接初始化，可枚举
            heapslot.propertyIsEnumerable = true;
			//frame.endStep(step);
			frame.endStepNoError();
        }
    }
}
