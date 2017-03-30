using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.operators
{
    class FunctionCaller
    {
        public HeapSlot[] CallFuncHeap;

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
                    CallFuncHeap[i].directSet(signature.parameters[i].defaultValue.getValue(null));
                }
            }
        }

        public void pushParameter(IRunTimeValue argement,int id)
        {
            //if (argement.rtType != toCallFunc.signature.parameters[id].type)
            //{
            //    if (!OpCast.CastValue(argement, toCallFunc.signature.parameters[id].type,
            //        _tempSlot, invokerFrame, token, invokerFrame.scope
            //        ))
            //    {
            //        invokerFrame.throwCastException(token, argement.rtType, toCallFunc.signature.parameters[id].type);
            //        return;
            //    }
            //    CallFuncHeap[id].directSet(_tempSlot.getValue());
            //}
            //else
            //{
            //    CallFuncHeap[id].directSet(argement);
            //}
            if (id < CallFuncHeap.Length)
            {
                CallFuncHeap[id].directSet(argement);
                pushedArgs++;
            }
            else
            {
                //参数数量不匹配
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

                        invokerFrame, token, invokerFrame.scope, _tempSlot,cb);

                    
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
                            new error.InternalError(token,
                            string.Format(
                            "Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
                            player.swc.blocks[toCallFunc.blockid].name, toCallFunc.signature.parameters.Count, pushedArgs
                            )
                            ,
                            new ASBinCode.rtData.rtString(
                                string.Format(
                                    "Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
                                    player.swc.blocks[toCallFunc.blockid].name, toCallFunc.signature.parameters.Count, pushedArgs
                                    )
                                )

                            )
                            );

                        //***中断本帧本次代码执行进入try catch阶段
                        invokerFrame.endStep();
                        return;
                    }
                }


            }

            returnSlot.directSet(
                TypeConverter.getDefaultValue(toCallFunc.signature.returnType).getValue(null));

            player.CallBlock(
                player.swc.blocks[toCallFunc.blockid],
                CallFuncHeap,
                returnSlot,
                function.bindScope,
                token, callbacker, function.this_pointer != null ? function.this_pointer : invokerFrame.scope.this_pointer);

        }


        public void call()
        {
            check_para();
        }

        

    }
}
