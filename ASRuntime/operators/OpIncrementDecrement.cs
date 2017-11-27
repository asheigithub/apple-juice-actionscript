using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpIncrementDecrement
    {
        public static void execIncrement(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);

			StackSlotAccessor register = step.arg1 as StackSlotAccessor;
			if (register != null)
			{
				((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
			}

			switch (v.rtType)
            {
                
                case ASBinCode.RunTimeDataType.rt_int:
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                        iv.value++;
						step.reg.getSlot(scope, frame).setValue(iv.value);
					}
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                        iv.value++;
                        step.reg.getSlot(scope, frame).setValue(iv.value);
						
					}
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                        iv.value++;
                        step.reg.getSlot(scope, frame).setValue(iv.value);
						
					}
                    break;
                
                case ASBinCode.RunTimeDataType.rt_string:

                    {
                        double n = TypeConverter.ConvertToNumber(v);
                        if (string.IsNullOrEmpty( ((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        step.reg.getSlot(scope, frame).directSet(num);

					}
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
                        double n = TypeConverter.ConvertToNumber(v);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).directSet(num);

						
					}
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
						
						OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr, frame, step.token, 
                            scope, frame._tempSlot1, 
                            frame._tempSlot2, step, _execIncrement_ValueOf_Callbacker);
                        return;
                    }
            }

			//frame.endStep(step);
			frame.endStepNoError();
        }
        private static void _execIncrement_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execIncrement_ToString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1);
                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).directSet(num);
                frame.endStep(step);
            }
            
        }
        private static void _execIncrement_ToString_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
           StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
           )
        {
            double n = TypeConverter.ConvertToNumber(v1);
            ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
            ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).directSet(num);
            frame.endStep(step);
        }



        public static void execIncInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);
			
			{
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value++;
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);
				
			}
			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execIncUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);
			
			{
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value++;
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);
				
			}
			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execIncNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);
			
			{
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value++;
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);

			}

			//frame.endStep(step);
			frame.endStepNoError();
        }





        public static void execDecrement(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);
			StackSlotAccessor register = step.arg1 as StackSlotAccessor;
			if (register != null)
			{
				((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
			}
			switch (v.rtType)
            {

                case ASBinCode.RunTimeDataType.rt_int:
                    {
                        ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                        iv.value--;
                        ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);

						
					}
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
                        ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                        iv.value--;
                        ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);

						
					}
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
                        ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                        iv.value--;
                        ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);

						
					}
                    break;

                case ASBinCode.RunTimeDataType.rt_string:

                    {
                        double n = TypeConverter.ConvertToNumber(v);
                        if (string.IsNullOrEmpty(((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).directSet(num);

						
					}
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
                        double n = TypeConverter.ConvertToNumber(v);
                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).directSet(num);

						
					}
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr, frame, step.token,
                            scope, frame._tempSlot1,
                            frame._tempSlot2, step, _execDecrement_ValueOf_Callbacker);
                        return;
                    }
            }


			//frame.endStep(step);
			frame.endStepNoError();
        }

        private static void _execDecrement_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr, frame, step.token, scope, frame._tempSlot1, frame._tempSlot2, step, _execDecrement_toString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1);
                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).directSet(num);
                frame.endStep(step);
            }
        }
        private static void _execDecrement_toString_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope
            )
        {
            double n = TypeConverter.ConvertToNumber(v1);
            ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
            ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).directSet(num);
            frame.endStep(step);
        }



        public static void execDecInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);
			StackSlotAccessor register = step.arg1 as StackSlotAccessor;
			if (register != null)
			{
				((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
			}
			{
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;
                iv.value--;
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);
            }
			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execDecUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);
			StackSlotAccessor register = step.arg1 as StackSlotAccessor;
			if (register != null)
			{
				((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
			}

			{
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;
                iv.value--;
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);

			}
			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execDecNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            var v = step.arg1.getValue(scope, frame);

			StackSlotAccessor register = step.arg1 as StackSlotAccessor;
			if (register != null)
			{
				((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
			}

			{
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;
                iv.value--;
                ((ASBinCode.LeftValueBase)step.reg).getSlot(scope, frame).setValue(iv.value);
            }
			//frame.endStep(step);
			frame.endStepNoError();
        }



        public static void execSuffixInc(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);
			switch (v.rtType)
            {

                case ASBinCode.RunTimeDataType.rt_int:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                        step.reg.getSlot(scope, frame).setValue(iv.value );

                        iv.value++;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                        step.reg.getSlot(scope, frame).setValue(iv.value);

                        iv.value++;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                        step.reg.getSlot(scope, frame).setValue(iv.value);

                        iv.value++;
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_string:

                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						double n = TypeConverter.ConvertToNumber(v);
                        if (string.IsNullOrEmpty(((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        step.reg.getSlot(scope, frame).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						double n = TypeConverter.ConvertToNumber(v);

                        step.reg.getSlot(scope, frame).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
                        ((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
                    }
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixInc_ValueOf_Callbacker);
                        return;
                    }
            }

			//frame.endStep(step);
			frame.endStepNoError();
        }

        private static void _execSuffixInc_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixInc_toString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1);

                step.reg.getSlot(scope, frame).setValue(n);

                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
				
				StackSlotAccessor register = step.arg1 as StackSlotAccessor;
				if (register != null)
				{
					bool issuccess;
					((StackSlot)register.getSlotForAssign(scope, frame)).assign(num, out issuccess);
					if (!issuccess)
					{
						frame.throwError(step.token, 0, "操作失败");
					}

				}
				else
				{
					((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
				}

				frame.endStep(step);
            }
        }

        private static void _execSuffixInc_toString_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double n = TypeConverter.ConvertToNumber(v1);

            step.reg.getSlot(scope, frame).setValue(n);

            ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(++n);
			

			StackSlotAccessor register = step.arg1 as StackSlotAccessor;
			if (register != null)
			{
				bool issuccess;
				((StackSlot)register.getSlotForAssign(scope, frame)).assign(num, out issuccess);
				if (!issuccess)
				{
					frame.throwError(step.token, 0, "操作失败");
				}

			}
			else
			{
				((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
			}

			frame.endStep(step);
        }

        public static void execSuffixIncInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);

			ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

            step.reg.getSlot(scope, frame).setValue(iv.value);

            iv.value++;


			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execSuffixIncUint(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);

			{
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                step.reg.getSlot(scope, frame).setValue(iv.value);

                iv.value++;
            }

			//frame.endStep(step);
			frame.endStepNoError();
        }

        public static void execSuffixIncNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);

			{
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                step.reg.getSlot(scope, frame).setValue(iv.value);

                iv.value++;
            }
			//frame.endStep(step);
			frame.endStepNoError();
        }




        public static void execSuffixDec(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);

            switch (v.rtType)
            {

                case ASBinCode.RunTimeDataType.rt_int:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}


						ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                        step.reg.getSlot(scope, frame).setValue(iv.value);

                        iv.value--;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_uint:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                        step.reg.getSlot(scope, frame).setValue(iv.value);

                        iv.value--;
                    }
                    break;
                case ASBinCode.RunTimeDataType.rt_number:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                        step.reg.getSlot(scope, frame).setValue(iv.value);

                        iv.value--;
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_string:

                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						double n = TypeConverter.ConvertToNumber(v);
                        if (string.IsNullOrEmpty(((ASBinCode.rtData.rtString)v).value))
                        {
                            n = 0;
                        }

                        step.reg.getSlot(scope, frame).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
                    }
                    break;

                case ASBinCode.RunTimeDataType.rt_boolean:
                case ASBinCode.RunTimeDataType.rt_void:
                case ASBinCode.RunTimeDataType.rt_null:
                    {
						StackSlotAccessor register = step.arg1 as StackSlotAccessor;
						if (register != null)
						{
							((StackSlot)register.getSlotForAssign(scope, frame)).linkTo(null);
						}

						double n = TypeConverter.ConvertToNumber(v);

                        step.reg.getSlot(scope, frame).setValue(n);

                        ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
                        ((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
                    }
                    break;
                case ASBinCode.RunTimeDataType.unknown:
                default:
                    {
                        OpCast.InvokeTwoValueOf(v, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixDec_ValueOf_Callbacker);
                        return;
                    }
            }

			//frame.endStep(step);
			frame.endStepNoError();
        }

        private static void _execSuffixDec_ValueOf_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            if (v1.rtType > ASBinCode.RunTimeDataType.unknown)
            {
                OpCast.InvokeTwoToString(v1, ASBinCode.rtData.rtNull.nullptr,
                            frame, step.token, scope, frame._tempSlot1, frame._tempSlot2,
                            step, _execSuffixDec_toString_Callbacker);
            }
            else
            {
                double n = TypeConverter.ConvertToNumber(v1);

                step.reg.getSlot(scope, frame).setValue(n);

                ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
				

				StackSlotAccessor register = step.arg1 as StackSlotAccessor;
				if (register != null)
				{
					bool issuccess;
					register.getSlotForAssign(scope, frame).assign(num, out issuccess);
					if (!issuccess)
					{
						frame.throwError(step.token,0, "操作失败");
					}
				}
				else
				{
					((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
				}



				frame.endStep(step);
            }
        }

        private static void _execSuffixDec_toString_Callbacker(ASBinCode.RunTimeValueBase v1, ASBinCode.RunTimeValueBase v2,
            StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {
            double n = TypeConverter.ConvertToNumber(v1);

            step.reg.getSlot(scope, frame).setValue(n);

			ASBinCode.rtData.rtNumber num = new ASBinCode.rtData.rtNumber(--n);
			StackSlotAccessor register = step.arg1 as StackSlotAccessor;
			if (register != null)
			{
				bool issuccess;
				register.getSlotForAssign(scope, frame).assign(num, out issuccess);
				if (!issuccess)
				{
					frame.throwError(step.token, 0, "操作失败");
				}
			}
			else
			{
				((ASBinCode.LeftValueBase)step.arg1).getSlot(scope, frame).directSet(num);
			}


			frame.endStep(step);
        }

        public static void execSuffixDecInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);

			{
                ASBinCode.rtData.rtInt iv = (ASBinCode.rtData.rtInt)v;

                step.reg.getSlot(scope, frame).setValue(iv.value);

                iv.value--;
            }
            frame.endStep(step);
        }

        public static void execSuffixDecUInt(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);

			{
                ASBinCode.rtData.rtUInt iv = (ASBinCode.rtData.rtUInt)v;

                step.reg.getSlot(scope, frame).setValue(iv.value);

				iv.value--;
            }
            frame.endStep(step);
        }

        public static void execSuffixDecNumber(StackFrame frame, ASBinCode.OpStep step, ASBinCode.RunTimeScope scope)
        {

            var v = step.arg1.getValue(scope, frame);
			
			{
                ASBinCode.rtData.rtNumber iv = (ASBinCode.rtData.rtNumber)v;

                step.reg.getSlot(scope, frame).setValue(iv.value);

                iv.value--;
            }
            frame.endStep(step);
        }


    }


}
