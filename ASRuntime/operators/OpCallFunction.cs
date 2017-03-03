using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class OpCallFunction
    {
        public static void bind(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
            var rv = step.arg1.getValue(frame.scope);
            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(new error.InternalError(step.token, "value is not a function",
                    new ASBinCode.rtData.rtString("value is not a function")));
                //return new error.InternalError(step.token, "value is not a function",
                //    new ASBinCode.rtData.rtString("value is not a function"));
                return;
            }
            
            ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;
            
            if (function.bindScope == null
                ||
                function.bindScope.blockId == frame.scope.blockId
                )
            {
                function.bind(frame.scope);
            }
            
        }

        public static void create_paraScope(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
            var rv = step.arg1.getValue(frame.scope);
            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(new error.InternalError(step.token, "value is not a function",
                    new ASBinCode.rtData.rtString("value is not a function")));
                
                return;
            }

            ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;
            ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[function.functionId];
            ASBinCode.rtti.FunctionSignature signature = funcDefine.signature;

            frame.tempCallFuncHeap = //new HeapSlot[ player.swc.blocks[funcDefine.blockid].scope.members.Count];
                player.genHeapFromCodeBlock(player.swc.blocks[funcDefine.blockid]);

            for (int i = 0; i < signature.parameters.Count; i++)
            {
                if (signature.parameters[i].defaultValue != null)
                {
                    frame.tempCallFuncHeap[i].directSet(signature.parameters[i].defaultValue.getValue(null));
                }
            }

            frame._tempSlot = new StackSlot();
            frame._toCallFunc = funcDefine;
        }

        public static void push_parameter(Player player, StackFrame frame, ASBinCode.OpStep step)
        {
            int id = ((ASBinCode.rtData.rtInt)step.arg2.getValue(frame.scope)).value;
            IRunTimeValue arg = step.arg1.getValue(frame.scope);

            if (arg.rtType != frame._toCallFunc.signature.parameters[id].type)
            {
                if (!OpCast.CastValue(arg, frame._toCallFunc.signature.parameters[id].type,
                    frame._tempSlot, frame, step.token, frame.scope
                    ))
                {
                    frame.throwCastException(step.token, arg.rtType, frame._toCallFunc.signature.parameters[id].type);
                    return;
                }
                frame.tempCallFuncHeap[id].directSet(frame._tempSlot.getValue());
            }
            else
            {
                frame.tempCallFuncHeap[id].directSet(arg);
            }
            frame._pushedArgs++;
            //frame.tempCallFuncHeap[id] = OpCast.CastValue()
        }



        public static void exec(Player player,StackFrame frame,ASBinCode.OpStep step)
        {
            var rv= step.arg1.getValue(frame.scope);
            if (rv.rtType != RunTimeDataType.rt_function)
            {
                frame.throwError(new error.InternalError(step.token, "value is not a function",
                    new ASBinCode.rtData.rtString("value is not a function")));

                return;
            }

            ASBinCode.rtData.rtFunction function = (ASBinCode.rtData.rtFunction)rv;
            ASBinCode.rtti.FunctionDefine funcDefine = player.swc.functions[function.functionId];

            if (!frame._toCallFunc.Equals(funcDefine))
            {
                frame.throwError(new error.InternalError( step.token,"运行时异常，调用函数不对" ));
                return;
            }

            if (frame._pushedArgs < funcDefine.signature.parameters.Count)
            {
                for (int i = frame._pushedArgs; i < funcDefine.signature.parameters.Count; i++)
                {
                    if (funcDefine.signature.parameters[frame._pushedArgs].defaultValue == null
                    &&
                    !funcDefine.signature.parameters[frame._pushedArgs].isPara
                    &&
                    funcDefine.signature.parameters[frame._pushedArgs].type != RunTimeDataType.rt_void
                    )
                    {
                        frame.throwError(
                            new error.InternalError(step.token,
                            string.Format(
                            "Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
                            player.swc.blocks[funcDefine.blockid].name, funcDefine.signature.parameters.Count, frame._pushedArgs
                            )
                            )
                            );
                        return;
                    }
                }

                
            }

            step.reg.getISlot(frame.scope).directSet(
                TypeConverter.getDefaultValue(funcDefine.signature.returnType).getValue(null));

            player.CallBlock( 
                player.swc.blocks[ funcDefine.blockid ] ,
                frame.tempCallFuncHeap, 
                step.reg.getISlot(frame.scope),
                function.bindScope, 
                step.token);

            frame.tempCallFuncHeap = null;
            frame._toCallFunc = null;
            frame._pushedArgs = 0;
            frame._tempSlot = null;
        }


        public static void exec_return(Player player, StackFrame frame, ASBinCode.OpStep step)
        {


            frame.returnSlot.directSet(step.arg1.getValue(frame.scope));
            
            
        }

    }
}
