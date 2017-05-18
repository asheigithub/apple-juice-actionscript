using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Yield_movenext : NativeFunctionBase
    {
        public Yield_movenext()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_yield_movenext";
            }
        }

        private List<RunTimeDataType> _paras;

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
                return RunTimeDataType.rt_boolean;
            }
        }

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.async_0;
            }
        }
        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new ASRunTimeException();
        }

        public sealed override void executeAsync(RunTimeValueBase thisObj, 
            SLOT[] argements, SLOT resultSlot, object callbacker, 
            object stackframe, SourceToken token, RunTimeScope scope)
        {
            
            ASBinCode.rtti.YieldObject yieldObj = (ASBinCode.rtti.YieldObject)
                ((ASBinCode.rtData.rtObject)thisObj).value;

            yieldObj.returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

            yieldObj._moveNextResultSlot = resultSlot;
            yieldObj._callbacker = callbacker;


            StackFrame frame = (StackFrame)stackframe;
            var toCallFunc = yieldObj.yield_function;
            var CallFuncHeap = yieldObj.argements;

            CallFuncHeap[CallFuncHeap.Length - 1].directSet(ASBinCode.rtData.rtBoolean.False);


            BlockCallBackBase cb = new BlockCallBackBase();
            cb.args = yieldObj;
            cb.setCallBacker(_movenext_callbacker);


            frame.player.callBlock(
                frame.player.swc.blocks[toCallFunc.blockid],
                CallFuncHeap,
                yieldObj.returnSlot,
                yieldObj.function_bindscope,
                token, cb,yieldObj.thispointer, RunTimeScopeType.function);


        }

        private void _movenext_callbacker(BlockCallBackBase sender, object args)
        {
            ASBinCode.rtti.YieldObject yieldObj = (ASBinCode.rtti.YieldObject)sender.args;

            yieldObj._moveNextResultSlot.directSet(yieldObj.argements[yieldObj.argements.Length-1].getValue());
            yieldObj._moveNextResultSlot = null;
            
            IBlockCallBack cb = ((IBlockCallBack)yieldObj._callbacker);
            yieldObj._callbacker = null;

            cb.call(cb.args);

        }
    }



    class Yield_current : NativeFunctionBase
    {
        public Yield_current()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_yield_current";
            }
        }

        private List<RunTimeDataType> _paras;

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

       
        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;
            ASBinCode.rtti.YieldObject yieldObj = (ASBinCode.rtti.YieldObject)
                ((ASBinCode.rtData.rtObject)thisObj).value;


            var result= yieldObj.returnSlot.getValue();
            //yieldObj.returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
            return result;
        }

        

        
    }
}
