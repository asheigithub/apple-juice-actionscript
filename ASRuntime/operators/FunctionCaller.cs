using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class FunctionCaller
    {
        private HeapSlot[] CallFuncHeap;

        public ASBinCode.rtData.rtFunction function;
        public ASBinCode.rtti.FunctionDefine toCallFunc;

        public int pushedArgs;

        public ISLOT returnSlot;

        public ISLOT _tempSlot;

        public IBlockCallBack callbacker;


        public Player player;
        private StackFrame invokerFrame;
        private SourceToken token;

        public FunctionCaller(Player player,StackFrame invokerFrame, SourceToken token)
        {
            this.player = player;
            this.invokerFrame = invokerFrame;
            this.token = token;
        }

        public void loadDefineFromFunction()
        {
            toCallFunc = player.swc.functions[function.functionId];
           
        }

        public void createParaScope()
        {
            
            ASBinCode.rtti.FunctionSignature signature = toCallFunc.signature;

            
            CallFuncHeap =
                player.genHeapFromCodeBlock(player.swc.blocks[toCallFunc.blockid]);

            for (int i = 0; i < signature.parameters.Count; i++)
            {
                if (signature.parameters[i].defaultValue != null)
                {
                    var dt = signature.parameters[i].type;
                    var dv = signature.parameters[i].defaultValue.getValue(null);


                    if (dv.rtType != dt && dt != RunTimeDataType.rt_void)
                    {
                        if (dt == RunTimeDataType.rt_int)
                        {
                            dv = new ASBinCode.rtData.rtInt(TypeConverter.ConvertToInt(dv, invokerFrame, token));
                        }
                        else if (dt == RunTimeDataType.rt_uint)
                        {
                            dv = new ASBinCode.rtData.rtUInt(TypeConverter.ConvertToUInt(dv, invokerFrame, token));
                        }
                        else if (dt == RunTimeDataType.rt_number)
                        {
                            dv = new ASBinCode.rtData.rtNumber(TypeConverter.ConvertToNumber(dv, invokerFrame, token));
                        }
                        else if (dt == RunTimeDataType.rt_string)
                        {
                            dv = new ASBinCode.rtData.rtString(TypeConverter.ConvertToString(dv, invokerFrame, token));
                        }
                        else if (dt == RunTimeDataType.rt_boolean)
                        {
                            dv = TypeConverter.ConvertToBoolean(dv, invokerFrame, token);
                        }
                    }


                    CallFuncHeap[i].directSet(
                        dv

                        );
                }
                else if (signature.parameters[i].isPara)
                {
                    CallFuncHeap[i].directSet(
                       new ASBinCode.rtData.rtArray()
                       );
                }

            }
            
        }

        public void pushParameter(IRunTimeValue argement,int id)
        {
            if (toCallFunc.signature.parameters.Count > 0)
            {
                bool lastIsPara = false;

                if (toCallFunc.signature.parameters[toCallFunc.signature.parameters.Count - 1].isPara)
                {
                    lastIsPara = true;
                }

                if (!lastIsPara || id < toCallFunc.signature.parameters.Count - 1)
                {
                    if (id < toCallFunc.signature.parameters.Count)
                    {
                        CallFuncHeap[id].directSet(argement);
                        pushedArgs++;
                    }
                    //else
                    //{
                    //    //参数数量不匹配
                    //}

                }
                else
                {
                    //***最后一个是参数数组，并且id大于等于最后一个
                    HeapSlot slot = CallFuncHeap[toCallFunc.signature.parameters.Count - 1];
                    if (slot.getValue().rtType == RunTimeDataType.rt_null)
                    {
                        slot.directSet(new ASBinCode.rtData.rtArray());
                    }

                    ASBinCode.rtData.rtArray arr = (ASBinCode.rtData.rtArray)slot.getValue();
                    arr.innerArray.Add((IRunTimeValue)argement.Clone());    //可能从StackSlot中读的数据，因此必须Clone后再传入.

                }
            }
        }

        private int check_para_id = 0;

        BlockCallBackBase cb;
        private void check_para()
        {
            while (check_para_id < pushedArgs)
            {
                IRunTimeValue argement = CallFuncHeap[check_para_id].getValue();
                if (argement.rtType != toCallFunc.signature.parameters[check_para_id].type)
                {
                    
                    cb.args = argement;
                    cb._intArg = check_para_id;
                    cb.setCallBacker(check_para_callbacker);

                    check_para_id++;

                    OpCast.CastValue(argement, toCallFunc.signature.parameters[
                        cb._intArg].type,

                        invokerFrame, token, invokerFrame.scope, _tempSlot,cb,false);

                    
                    return;
                }
                else
                {
                    check_para_id++;
                }
            }
            //***全部参数检查通过***
            _doCall();
        }

        private void check_para_callbacker(BlockCallBackBase sender,object args)
        {
            if (sender.isSuccess)
            {
                CallFuncHeap[sender._intArg].directSet(_tempSlot.getValue());
                check_para();
            }
            else
            {
                invokerFrame.throwCastException(token, ((IRunTimeValue)sender.args).rtType, toCallFunc.signature.parameters[sender._intArg].type);
                return;
            }
        }

        private void _doCall()
        {
            if (pushedArgs < toCallFunc.signature.parameters.Count)
            {
                for (int i = pushedArgs; i < toCallFunc.signature.parameters.Count; i++)
                {
                    if (toCallFunc.signature.parameters[pushedArgs].defaultValue == null
                    &&
                    !toCallFunc.signature.parameters[pushedArgs].isPara
                    &&
                    (
                    toCallFunc.signature.parameters[pushedArgs].type != RunTimeDataType.rt_void
                        ||
                    toCallFunc.isMethod
                    )
                    )
                    {
                        invokerFrame.throwError(
                            token,
                           0,
                            string.Format(
                            "Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
                            player.swc.blocks[toCallFunc.blockid].name, toCallFunc.signature.parameters.Count, pushedArgs
                            )
                            
                            );

                        //***中断本帧本次代码执行进入try catch阶段
                        invokerFrame.endStep();
                        return;
                    }
                }
            }

            
            if (!toCallFunc.isNative)
            {
                returnSlot.directSet(
                    TypeConverter.getDefaultValue(toCallFunc.signature.returnType).getValue(null));
                player.CallBlock(
                    player.swc.blocks[toCallFunc.blockid],
                    CallFuncHeap,
                    returnSlot,
                    function.bindScope,
                    token, callbacker, function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer, RunTimeScopeType.function);
            }
            else
            {
                if (toCallFunc.native_index <0)
                {
                    toCallFunc.native_index = player.swc.nativefunctionNameIndex[toCallFunc.native_name];
                }

                string errormsg;
                int errorno;

                var nf = player.swc.nativefunctions[toCallFunc.native_index];
                nf.bin = player.swc;

                if (!nf.isAsync)
                {
                    var result = nf.execute(
                        function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer,
                        CallFuncHeap,invokerFrame,
                        out errormsg,
                        out errorno
                        );

                    if (errormsg == null)
                    {
                        returnSlot.directSet(result);

                        if (callbacker != null)
                        {
                            callbacker.call(callbacker.args);
                        }
                    }
                    else
                    {
                        invokerFrame.throwError(
                            token,0, errormsg
                            );

                        invokerFrame.endStep();
                    }

                }
                else
                {
                    nf.executeAsync(function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer,
                        CallFuncHeap,
                        returnSlot,
                        callbacker,
                        invokerFrame,
                        token,
                        function.bindScope
                        );
                }
            }
        }


        public void call()
        {
            check_para();
        }

        

    }
}
