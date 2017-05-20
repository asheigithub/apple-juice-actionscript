using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    interface IBlockCallBack
    {
        object args { get; set; }

        void call(object args);
    }

    class BlockCallBackBase : IBlockCallBack
    {
        public delegate void dgeCallbacker(BlockCallBackBase sender, object args);
        public bool isSuccess;

        public int _intArg;
        public ASBinCode.RunTimeScope scope;
        public ASBinCode.OpStep step;
        private dgeCallbacker callbacker;

        public void setCallBacker(dgeCallbacker callbacker)
        {
            this.callbacker = callbacker;
        }

        public object args
        {
            get
            ;
            set
            ;
        }

        public void call(object args)
        {
            callbacker(this, args);
            callbacker = null;
        }
    }


}
