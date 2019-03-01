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
            
            ASBinCode.SLOT slot = step.reg.getSlotForAssign(scope, frame);

			ASBinCode.RunTimeValueBase v = step.arg1.getValue(scope, frame);
			bool success;

			var lt= slot.assign(v, out success);

            
			if (!success) //(!slot.directSet(v))
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
						//slot = oslot.linktarget;
						slot = lt;
					}

					if (step.arg1 is IMemReg)
					{
						//将其暂存到临时槽内
						frame._tempSlot2.directSet(v);
						v = frame._tempSlot2.getValue();
					}

					if (slot is SetThisItemSlot)
					{
						_doSetThisItem(((SetThisItemSlot)slot).bindObj, ((SetThisItemSlot)slot).set_this_item ,v, ((SetThisItemSlot)slot).setindex, oslot, frame, step);

						return;
					}

					if (slot is ClassPropertyGetter.PropertySlot)
					{
						ClassPropertyGetter.PropertySlot propslot =
						(ASBinCode.ClassPropertyGetter.PropertySlot)slot;
						//***调用访问器。***
						ASBinCode.ClassPropertyGetter prop = oslot.stackObjects.propGetSet; //propslot.property;

						_doPropAssigning(prop, frame, step, frame.player, scope,
							//propslot.bindObj
							oslot.stackObjects.propBindObj
							, v,
							oslot
							);
						return;
					}

					if (slot is OpVector.vectorSLot)    //Vector类型不匹配
					{
						BlockCallBackBase cb = frame.player.blockCallBackPool.create();
						cb.scope = scope;
						cb.step = step;
						cb.args = frame;
						cb.setCallBacker(D_vectorConvertCallBacker);
						cb.cacheObjects[0] = slot;

						//***调用强制类型转换***
						OpCast.CastValue(v, ((OpVector.vectorSLot)slot).vector_data.vector_type,
							frame, step.token, scope, frame._tempSlot1, cb, false);

						return;
					}

					if (slot is ObjectMemberSlot && !((ObjectMemberSlot)slot).isConstMember)
					{
						BlockCallBackBase cb = frame.player.blockCallBackPool.create();
						cb.scope = scope;
						cb.step = step;
						cb.args = frame;
						cb.setCallBacker(D_objectmemberslotConvertCallbacker);
						cb.cacheObjects[0] = slot;

						//***调用强制类型转换***
						OpCast.CastValue(v, ((ObjectMemberSlot)slot).slottype,
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
						" on " + ((OpAccess_Dot.prototypeSlot)slot)._protoRootObj.value._class.name;
				}

				frame.throwError(
					step.token, 0, ext
					);
				frame.endStep(step);
			}
			else
			{
				frame.endStepNoError();
			}
                
            
            

        }

        private static BlockCallBackBase.dgeCallbacker D_vectorConvertCallBacker = new BlockCallBackBase.dgeCallbacker(_vectorConvertCallBacker);
        internal static void _vectorConvertCallBacker(BlockCallBackBase sender,object args)
        {
            StackFrame frame = (StackFrame)sender.args;
            OpStep step = sender.step;

            ASBinCode.RunTimeValueBase v = frame._tempSlot1.getValue();
			//ASBinCode.SLOT slot = step.reg.getSlot(sender.scope, frame);

			OpVector.vectorSLot slot = (OpVector.vectorSLot)sender.cacheObjects[0];

			bool success;
			slot.assign(v, out success);

			if (!success)
			{
				frame.throwCastException(step.token,
					   step.arg1.getValue(sender.scope, frame).rtType,
					   slot.vector_data.vector_type
						);
				frame.endStep(step);
			}
			else
			{
				frame.endStepNoError();
			}
            
        }
        private static BlockCallBackBase.dgeCallbacker D_objectmemberslotConvertCallbacker = new BlockCallBackBase.dgeCallbacker(_objectmemberslotConvertCallbacker);
        internal static void _objectmemberslotConvertCallbacker(BlockCallBackBase sender, object args)
		{
			StackFrame frame = (StackFrame)sender.args;
			OpStep step = sender.step;

			ASBinCode.RunTimeValueBase v = frame._tempSlot1.getValue();
			//ASBinCode.SLOT slot = step.reg.getSlot(sender.scope, frame);

			ObjectMemberSlot slot = (ObjectMemberSlot)sender.cacheObjects[0];

			bool success;
			slot.assign(v, out success);

			if (!success)
			{
				frame.throwCastException(step.token,
					   step.arg1.getValue(sender.scope, frame).rtType,
					   slot.slottype
						);
				frame.endStep(step);
			}
			else
			{
				frame.endStepNoError();
			}

		}

		public static void _doPropAssigning(ClassPropertyGetter prop,StackFrame frame,
            OpStep step,Player player, RunTimeScope scope,
            ASBinCode.rtData.rtObjectBase bindobj,RunTimeValueBase v , StackSlot sslot )
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

                if (sslot.stackObjects.superPropBindClass != null)
                {
                    func = ((MethodGetterBase)setter.bindField).getSuperMethod(
                        bindobj.objScope,
                        sslot.stackObjects.superPropBindClass
                        );
                }
                else
                {
                    func = ((MethodGetterBase)setter.bindField).getMethod(
                        bindobj
                        );
                }
                //***调用设置器***

                var funCaller = frame.player.funcCallerPool.create(frame, step.token);
                funCaller.SetFunction((ASBinCode.rtData.rtFunction)func); ((ASBinCode.rtData.rtFunction)func).Clear();
                funCaller.loadDefineFromFunction();
                if (!funCaller.createParaScope()) { return; }

                //funCaller.releaseAfterCall = true;

                bool success;
                funCaller.pushParameter(v, 0,out success);
                if (!success)
                {
                    frame.endStep(step);
                    return;
                }

                funCaller._tempSlot = frame._tempSlot1;
                funCaller.returnSlot = frame._tempSlot1;

                BlockCallBackBase cb = frame.player.blockCallBackPool.create();
                cb.setCallBacker(D_assign_callbacker);
                cb.step = step;
                cb.args = frame;

                funCaller.callbacker = cb;
                funCaller.call();

                return;

            } while (false);


            frame.endStep(step);
        }

        private static BlockCallBackBase.dgeCallbacker D_assign_callbacker = new BlockCallBackBase.dgeCallbacker(_assign_callbacker);
        private static void _assign_callbacker(BlockCallBackBase sender,object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }


        public static void _doSetThisItem(ASBinCode.rtData.rtObjectBase thisObj, ASBinCode.rtti.ClassMember set_this_item,
            RunTimeValueBase v,RunTimeValueBase index,StackSlot slot,StackFrame frame,OpStep step
            )
        {
            //***读取setter***
            RunTimeValueBase func;

            func = ((MethodGetterBase)set_this_item.bindField).getMethod(
                thisObj
                );
            
            //***调用设置器***

            var funCaller = frame.player.funcCallerPool.create(frame, step.token);
            funCaller.SetFunction((ASBinCode.rtData.rtFunction)func); ((ASBinCode.rtData.rtFunction)func).Clear();
            funCaller.loadDefineFromFunction();
            if (!funCaller.createParaScope()) { return; }

            //funCaller.releaseAfterCall = true;

            bool success;
            funCaller.pushParameter(v, 0, out success);
            
            if (!success)
            {
                frame.endStep(step);
                return;
            }

            funCaller.pushParameter(index, 1, out success);
            if (!success)
            {
                frame.endStep(step);
                return;
            }

            funCaller._tempSlot = frame._tempSlot1;
            funCaller.returnSlot = frame._tempSlot1;

            BlockCallBackBase cb = frame.player.blockCallBackPool.create();
            cb.setCallBacker(D_setthisitem_callbacker);
            cb.step = step;
            cb.args = frame;
			

            funCaller.callbacker = cb;
            funCaller.call();

            return;
        }
        private static BlockCallBackBase.dgeCallbacker D_setthisitem_callbacker = new BlockCallBackBase.dgeCallbacker(_setthisitem_callbacker);
        private static void _setthisitem_callbacker(BlockCallBackBase sender, object args)
        {
            ((StackFrame)sender.args).endStep(sender.step);
        }
    }
}
