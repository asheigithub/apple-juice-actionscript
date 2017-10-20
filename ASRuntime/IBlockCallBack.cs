using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    interface IBlockCallBack
    {
        object args { get; set; }

        void call(object args);

        /// <summary>
        /// 通知代码段无法回调了
        /// </summary>
        void noticeRunFailed();
    }

    class BlockCallBackBase : IBlockCallBack
    {
        //private static List<BlockCallBackBase> oo;

        private static Stack<BlockCallBackBase> pool;
        static BlockCallBackBase()
        {
            pool = new Stack<BlockCallBackBase>();
            for (int i = 0; i < 1024; i++)
            {
                pool.Push(new BlockCallBackBase());
            }

            //oo = new List<BlockCallBackBase>();
        }
        
        public static BlockCallBackBase create()
        {
            BlockCallBackBase cb = pool.Pop();
            cb.hasnoticed = false;
            cb.hasreleased = false;

            //oo.Add(cb);

            return cb;
        }

        private static void ret(BlockCallBackBase c)
        {
            pool.Push(c);
            //oo.Remove(c);
        }

        public static void checkpool()
        {
            if (pool.Count != 1024)
            {
                throw new ASBinCode.ASRunTimeException("缓存池异常");
            }
        }

        public delegate void dgeCallbacker(BlockCallBackBase sender, object args);
        public bool isSuccess;

        public int _intArg;
        public ASBinCode.RunTimeScope scope;
        public ASBinCode.OpStep step;

        public object[] cacheObjects;

        private dgeCallbacker callbacker;

        private dgeCallbacker whenFailed;

        private bool hasreleased;
        private bool hasnoticed;

        private BlockCallBackBase()
        {
            cacheObjects = new object[10];
        }

        public object[] copyFromReceiveArgs(object[] receiveArgs)
        {
            for (int i = 0; i < receiveArgs.Length; i++)
            {
                cacheObjects[i] = receiveArgs[i];
            }
            return cacheObjects;
        }

        public void setCallBacker(dgeCallbacker callbacker)
        {
            this.callbacker = callbacker;
        }

        public void setWhenFailed(dgeCallbacker whenFailed)
        {
            this.whenFailed = whenFailed;
        }

        public object args
        {
            get
            ;
            set
            ;
        }

        /// <summary>
        /// 通知作为参数传递时，需要回收
        /// </summary>
        public void noticeEnd()
        {
            release();
        }

        public void call(object args)
        {
            callbacker(this, args);
            
            release();
        }

        public void noticeRunFailed()
        {
            if (!hasnoticed)
            {
                hasnoticed = true;

                if (args is object[])
                {
                    object[] a = (object[])args;
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i] is IBlockCallBack)
                        {
                            ((IBlockCallBack)a[i]).noticeRunFailed();
                        }
                    }
                }
                else if (args is ASBinCode.rtti.YieldObject)
                {
                    ASBinCode.rtti.YieldObject yobj = (ASBinCode.rtti.YieldObject)args;
                    if (yobj._callbacker !=null)
                    {
                        ((IBlockCallBack)yobj._callbacker).noticeRunFailed();
                        yobj._callbacker = null;
                    }
                }

                if (whenFailed != null)
                {
                    whenFailed(this, args);
                }

                release();

            }
        }

        private void release()
        {
            if (!hasreleased)
            {
                hasreleased = true;

                isSuccess = false;
                _intArg = 0;
                scope = null;
                step = null;
                callbacker = null;
                args = null;
                whenFailed = null;


                cacheObjects[0] = null;
                cacheObjects[1] = null;
                cacheObjects[2] = null;
                cacheObjects[3] = null;
                cacheObjects[4] = null;
                cacheObjects[5] = null;
                cacheObjects[6] = null;
                cacheObjects[7] = null;
                cacheObjects[8] = null;
                cacheObjects[9] = null;

                ret(this);

            }
        }

    }


}
