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

    sealed class BlockCallBackBase : IBlockCallBack
    {
		internal class BlockCallBackBasePool : PoolBase<BlockCallBackBase>
		{
			Player player;
			public BlockCallBackBasePool(Player player):base(1024)
			{
				this.player = player;
			}

			public override BlockCallBackBase create()
			{
				BlockCallBackBase cb = base.create();
				cb.hasnoticed = false;
				cb.hasreleased = false;
				cb.player = player;
				cb.isSuccess = false;
				cb._intArg = 0; cb._intArg2 = 0;
				//oo.Add(cb);

				return cb;
			}
			public override void ret(BlockCallBackBase c)
			{
				c.player = null;
				base.ret(c);
			}
		}


		public delegate void dgeCallbacker(BlockCallBackBase sender, object args);
        public bool isSuccess;

		public int _intArg2;
        public int _intArg;
        public ASBinCode.RunTimeScope scope;
        public ASBinCode.OpStep step;

        public object[] cacheObjects;

        private dgeCallbacker callbacker;

        private dgeCallbacker whenFailed;

        private bool hasreleased;
        private bool hasnoticed;

		private Player player;

        public BlockCallBackBase()
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
				else if (args is IBlockCallBack)
				{
					((IBlockCallBack)args).noticeRunFailed();
				}
				else if (args is ASBinCode.rtti.YieldObject)
				{
					ASBinCode.rtti.YieldObject yobj = (ASBinCode.rtti.YieldObject)args;
					if (yobj._callbacker != null)
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

                
                scope = null;
                step = null;
                callbacker = null;
                args = null;
                whenFailed = null;
				isSuccess = false;
				_intArg = 0; _intArg2 = 0;

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

                player.blockCallBackPool.ret(this);

            }
        }

    }


}
