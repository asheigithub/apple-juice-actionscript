using ASBinCode;
using ASBinCode.rtData;
using ASRuntime.operators;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
    class Function_fill : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Function_fill():base(2)
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_function);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_function_fill";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        //{
            

        //    //throw new NotImplementedException();
        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			((rtObjectBase)argements[0]).value.memberData[0].directSet(argements[1]);
			((rtFunction)argements[1]).objHandle = ((rtObjectBase)argements[0]);

			success = true;
			returnSlot.setValue(rtUndefined.undefined);

			//errormessage = null;
			//errorno = 0;
			//return rtUndefined.undefined;
		}
	}

    class Function_load : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Function_load()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_function);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_function_load";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            if (((rtFunction)argements[0].getValue()).objHandle == null)
            {
                return rtNull.nullptr;
            }
            else
            {
                return ((rtFunction)argements[0].getValue()).objHandle;
            }
        }
    }


    class Function_setPrototype : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Function_setPrototype()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_function_setPrototype";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            if (argements[0].getValue().rtType < RunTimeDataType.unknown)
            {
                errormessage = "Prototype objects must be vanilla Objects.";
                errorno = 1049;
            }
            else
            {
                ((rtObjectBase)thisObj).value.memberData[1].directSet(argements[0].getValue());
            }
            return rtUndefined.undefined;
        }
    }




    class Function_apply : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;
        public Function_apply()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_array);
        }
        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_function_apply";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.async_0;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            throw new ASRunTimeException();
        }

        public override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements, SLOT resultSlot, 
            object callbacker, 
            object stackframe, 
            SourceToken token, RunTimeScope scope)
        {
            //base.executeAsync(thisObj, argements, resultSlot, callbacker, stackframe, token, scope);

            var thisArg = argements[0].getValue();
            

            rtFunction func = (rtFunction)((rtObjectBase)thisObj).value.memberData[0].getValue();
			rtFunction toApply = (rtFunction)func;//.Clone();


			if (!func.ismethod) //方法无法更改this
			{
				if (!(thisArg is rtObjectBase))
				{
					var player = ((StackFrame)stackframe).player;
					var objtype = thisArg.rtType;
					if (objtype < RunTimeDataType.unknown
						&&
						player.swc.primitive_to_class_table[objtype] != null
						)
					{
						FunctionCaller toinsert = ((StackFrame)stackframe).player.funcCallerPool.create( (StackFrame)(stackframe), token);
						toinsert.callbacker = (IBlockCallBack)callbacker;
						toinsert.SetFunction(toApply);
						toinsert.loadDefineFromFunction();
						if (!toinsert.createParaScope()) {  return; }
						toinsert._tempSlot = ((StackFrame)stackframe)._tempSlot1;
						toinsert.returnSlot = resultSlot;
						toinsert.tag = argements;
						stackCallers.Push(toinsert);

						//***转换为对象***

						OpCast.Primitive_to_Object(thisArg, (StackFrame)stackframe, token, scope,
							((StackFrame)stackframe)._tempSlot1,
							null, _primitive_toObj);




						

						return;
					}
					else
					{
						var fd = player.swc.functions[toApply.functionId];
						var block = player.swc.blocks[fd.blockid];
						//if (block.isoutclass )将this复位
						{

							var oc = player.outpackage_runtimescope[block.define_class_id];
							toApply.setThis(oc.this_pointer);
						}

						//caller.function.setThis(caller.function.bindScope.this_pointer);
					}
				}
				else
				{
					toApply.setThis((rtObjectBase)thisArg);
				}
			}




			FunctionCaller caller = ((StackFrame)stackframe).player.funcCallerPool.create((StackFrame)(stackframe), token);
            caller.callbacker = (IBlockCallBack)callbacker;
            caller.SetFunction ( toApply);
            caller.loadDefineFromFunction();
            caller._tempSlot = ((StackFrame)stackframe)._tempSlot1;
            caller.returnSlot = resultSlot;
			if (!caller.createParaScope()) {  return; }



			if (argements[1].getValue().rtType != rtNull.nullptr.rtType)
            {
                rtArray argArray = (rtArray)argements[1].getValue();
                for (int i = 0; i < argArray.innerArray.Count; i++)
                {
                    bool success;
                    caller.pushParameter(argArray.innerArray[i], i,out success);
                }
            }

            caller.call();
            
        }

        private Stack<FunctionCaller> stackCallers = new Stack<FunctionCaller>();

        private  void _primitive_toObj(ASBinCode.RunTimeValueBase v1, 
            ASBinCode.RunTimeValueBase v_temp, 
            StackFrame frame, ASBinCode.OpStep step, 
            ASBinCode.RunTimeScope scope)
        {
            var c = stackCallers.Pop();
            if (v1.rtType < RunTimeDataType.unknown)
            {
                c.SetFunctionThis(null);
            }
            else
            {
                rtObjectBase rtObj = (rtObjectBase)v1;
                c.SetFunctionThis(rtObj);
            }

			if (!c.createParaScope()) { return; }

			SLOT[] argements = (SLOT[])c.tag;

			if (argements[1].getValue().rtType != rtNull.nullptr.rtType)
			{
				rtArray argArray = (rtArray)argements[1].getValue();
				for (int i = 0; i < argArray.innerArray.Count; i++)
				{
					bool success;
					c.pushParameter(argArray.innerArray[i], i, out success);
				}
			}
			c.call();
		}

    }



    class Function_call : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;
        public Function_call()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_array);
        }
        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_function_call";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.async_0;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            throw new ASRunTimeException();
        }

        public override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements, SLOT resultSlot,
            object callbacker,
            object stackframe,
            SourceToken token, RunTimeScope scope)
        {
            //base.executeAsync(thisObj, argements, resultSlot, callbacker, stackframe, token, scope);

            var thisArg = argements[0].getValue();


            rtFunction func = (rtFunction)((rtObjectBase)thisObj).value.memberData[0].getValue();
			rtFunction toApply = (rtFunction)func;//.Clone();


			if (!func.ismethod) //方法无法更改this
			{
				if (!(thisArg is rtObjectBase))
				{
					var player = ((StackFrame)stackframe).player;
					var objtype = thisArg.rtType;
					if (objtype < RunTimeDataType.unknown
						&&
						player.swc.primitive_to_class_table[objtype] != null
						)
					{
						FunctionCaller toInsertStack = ((StackFrame)stackframe).player.funcCallerPool.create( (StackFrame)(stackframe), token);
						toInsertStack.callbacker = (IBlockCallBack)callbacker;
						toInsertStack.SetFunction(  toApply);
						toInsertStack._tempSlot = ((StackFrame)stackframe)._tempSlot1;
						toInsertStack.returnSlot = resultSlot;
						toInsertStack.tag = argements;

						stackCallers.Push(toInsertStack);

						//***转换为对象***
						OpCast.Primitive_to_Object(thisArg, (StackFrame)stackframe, token, scope,
							((StackFrame)stackframe)._tempSlot1,
							null, _primitive_toObj);

						
						return;
					}
					else
					{
						var fd = player.swc.functions[toApply.functionId];
						var block = player.swc.blocks[fd.blockid];
						//if (block.isoutclass )将this复位
						{

							var oc = player.outpackage_runtimescope[block.define_class_id];
							toApply.setThis(oc.this_pointer);
						}
						//else
						//{
						//    caller.function.setThis(null);
						//}


					}
				}
				else
				{
					toApply.setThis((rtObjectBase)thisArg);
				}
			}

			FunctionCaller caller = ((StackFrame)stackframe).player.funcCallerPool.create( (StackFrame)(stackframe), token);
            caller.callbacker = (IBlockCallBack)callbacker;
            caller.SetFunction(toApply);
			caller._tempSlot = ((StackFrame)stackframe)._tempSlot1;
			caller.returnSlot = resultSlot;

			caller.loadDefineFromFunction();
            if (!caller.createParaScope()) {  return; }
            

 

            if (argements[1].getValue().rtType == RunTimeDataType.rt_array)
            {
                rtArray argArray = (rtArray)argements[1].getValue();
                for (int i = 0; i < argArray.innerArray.Count; i++)
                {
                    bool success;
                    caller.pushParameter(argArray.innerArray[i], i,out success);
                }
            }

            
            caller.call();
        }

        private Stack<FunctionCaller> stackCallers = new Stack<FunctionCaller>();

        private void _primitive_toObj(ASBinCode.RunTimeValueBase v1,
            ASBinCode.RunTimeValueBase v_temp,
            StackFrame frame, ASBinCode.OpStep step,
            ASBinCode.RunTimeScope scope)
        {
            var c = stackCallers.Pop();
            if (v1.rtType < RunTimeDataType.unknown)
            {
				//c.function.setThis(null);
				c.SetFunctionThis(null);
            }
            else
            {
                rtObjectBase rtObj = (rtObjectBase)v1;
				//c.function.setThis(rtObj);
				c.SetFunctionThis(rtObj);
            }
			
			c.loadDefineFromFunction();
			if (!c.createParaScope()) { return; }

			SLOT[] argements = (SLOT[])c.tag;

			if (argements[1].getValue().rtType == RunTimeDataType.rt_array)
			{
				rtArray argArray = (rtArray)argements[1].getValue();
				for (int i = 0; i < argArray.innerArray.Count; i++)
				{
					bool success;
					c.pushParameter(argArray.innerArray[i], i, out success);
				}
			}

			c.call();

		}

    }

}
