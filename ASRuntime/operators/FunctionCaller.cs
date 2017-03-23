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
        private StackFrame frame;
        private SourceToken token;

        public FunctionCaller(Player player,StackFrame frame,SourceToken token)
        {
            this.player = player;
            this.frame = frame;
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
            if (argement.rtType != toCallFunc.signature.parameters[id].type)
            {
                if (!OpCast.CastValue(argement, toCallFunc.signature.parameters[id].type,
                    _tempSlot, frame, token, frame.scope
                    ))
                {
                    frame.throwCastException(token, argement.rtType, toCallFunc.signature.parameters[id].type);
                    return;
                }
                CallFuncHeap[id].directSet(_tempSlot.getValue());
            }
            else
            {
                CallFuncHeap[id].directSet(argement);
            }
            pushedArgs++;
        }

        public void call()
        {
            if (pushedArgs < toCallFunc.signature.parameters.Count)
            {
                for (int i = pushedArgs; i < toCallFunc.signature.parameters.Count; i++)
                {
                    if (toCallFunc.signature.parameters[pushedArgs].defaultValue == null
                    &&
                    !toCallFunc.signature.parameters[pushedArgs].isPara
                    &&
                    toCallFunc.signature.parameters[pushedArgs].type != RunTimeDataType.rt_void
                    )
                    {
                        frame.throwError(
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
                token,callbacker, function.this_pointer != null ? function.this_pointer : frame.scope.this_pointer);

        }

    }
}
