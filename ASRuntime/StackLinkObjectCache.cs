using ASBinCode;
using ASBinCode.rtData;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    /// <summary>
    /// 缓存链接系统对象
    /// 引用类型编号为0，其他每一种结构体都有一个实例缓存。
    /// </summary>
    class StackLinkObjectCache
    {
		//public rtObject srcObject;

		public sealed class StackCacheObject : rtObjectBase
		{
			private StackLinkObjectCache cache;
			public StackCacheObject(StackLinkObjectCache cache, ASBinCode.rtti.Object v,RunTimeScope s):base(v,s)
			{
				this.cache = cache;
			}

			public sealed override object Clone()
			{				
				LinkSystemObject lobj = (LinkSystemObject)value;

				rtObject clone = new rtObject((lobj).Clone(),
					null
					);

				RunTimeScope scope =
					new RunTimeScope(null, objScope.blockId, null,
					clone, RunTimeScopeType.objectinstance);
				clone.objScope = scope;

				return clone;
			}


			public static StackCacheObject createFrom(StackLinkObjectCache cache,rtObjectBase src)
			{
				LinkSystemObject lobj = (LinkSystemObject)src.value;

				StackCacheObject clone = new StackCacheObject(cache,(lobj).Clone(),
					null
					);

				RunTimeScope scope =
					new RunTimeScope(null, src.objScope.blockId, null,
					clone, RunTimeScopeType.objectinstance);
				clone.objScope = scope;

				return clone;
			}

		}


        private StackCacheObject[] cache;

        private StackLinkObjectCache() { }

		private Player player;

        public StackLinkObjectCache(CSWC bin,Player player)
        {
			this.player = player;
            int maxstructidx = -1;
            foreach (var item in bin.class_Creator)
            {
                var cls = item.Key;
                
                {
                    if (cls.structIndex > maxstructidx)
                    {
                        maxstructidx = cls.structIndex;
                    }
                }
            }

			structuseswitchs = new bool[maxstructidx + 1];
			strunctuseindexs = new int[maxstructidx + 1];
			strunctusecount = 0;

            if (maxstructidx >= 0)
            {
                cache = new StackCacheObject[maxstructidx + 1];
				//cache[0] = player.alloc_pureHostedOrLinkedObject(player.swc.LinkObjectClass);
				cache[0] = StackCacheObject.createFrom(this, player.alloc_pureHostedOrLinkedObject(player.swc.LinkObjectClass));


				foreach (var item in bin.class_Creator)
				{
					var cls = item.Key;
					if (cls.isStruct)
					{
						cache[cls.structIndex] = StackCacheObject.createFrom(this, player.alloc_pureHostedOrLinkedObject(cls));
					}
				}
			}
            
        }

		private bool[] structuseswitchs;
		private int[] strunctuseindexs;
		private int strunctusecount;

        public StackCacheObject getCacheObj(Class cls)
        {
			if (cls.isStruct) //如果是结构体，需要标记回头清除。
			{
				int structindex = cls.structIndex;
				if (!structuseswitchs[structindex])
				{
					structuseswitchs[structindex] = true;
					strunctuseindexs[strunctusecount++] = structindex;
				}
			}

			//if (cls.isStruct)
			{//由于非结构体都是0，所以这里直接使用结构体索引返回即可
				return cache[cls.structIndex];
			}

			//***结构体改为懒加载
			//if (!cls.isStruct)
			//{
			//	return cache[cls.structIndex];
			//}
			//else
			//{
			//	if (cache[cls.structIndex] == null)
			//	{
			//		cache[cls.structIndex] = StackCacheObject.createFrom(this, player.alloc_pureHostedOrLinkedObject(cls));
			//	}
			//	return cache[cls.structIndex];
			//}

		}

		/// <summary>
		/// 清除引用类型对象的引用 
		/// </summary>
		public void clearRefObj()
        {
            if (cache != null)
            {
				cache[0].value._class = null;
                cache[0].rtType = RunTimeDataType.unknown;
                ((LinkObj<object>)cache[0].value).value = null;
            }

			//防止结构体引用了引用对象。。。所以需要清理复位
			while (strunctusecount>0)
			{
				int structindex = strunctuseindexs[strunctusecount];
				structuseswitchs[structindex] = false;
				((LinkSystemObject)cache[structindex].value).ResetLinkData();

				strunctusecount--;
			}

        }

        public StackLinkObjectCache Clone()
        {
            StackLinkObjectCache c = new StackLinkObjectCache();
			c.player = player;
            if (cache != null)
            {
                c.cache = new StackCacheObject[cache.Length];
				c.structuseswitchs = new bool[structuseswitchs.Length];
				c.strunctuseindexs = new int[strunctuseindexs.Length];
				c.strunctusecount = 0;

                for (int i = 0; i < cache.Length; i++)
                {
					//c.cache[i] = (rtObject)cache[i].Clone();

					if (cache[i] != null)
					{
						c.cache[i] = StackCacheObject.createFrom(c, cache[i]);
					}
				}
            }
            return c;
        }

    }
}
