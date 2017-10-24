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
		public rtObject srcObject;

		public class StackCacheObject : rtObject
		{
			private StackLinkObjectCache cache;
			public StackCacheObject(StackLinkObjectCache cache, ASBinCode.rtti.Object v,RunTimeScope s):base(v,s)
			{
				this.cache = cache;
			}

			
			public sealed override rtObject getSrcObject()
			{
				if (cache.srcObject == null
					||
					cache.srcObject.rtType != rtType 
					||
					!(ReferenceEquals(((LinkSystemObject)cache.srcObject.value).GetLinkData(), ((LinkSystemObject)value).GetLinkData()))
					)
				{
					
					//return this;
					//if (srcObject == null)
					//{

					//}

					//出现此种情况，说明时链接对象的中间计算步骤。这时因为不是从变量赋值来的，所以没有srcObject



					return this;
				}
				else
				{
					return cache.srcObject;
				}
				//throw new Exception();
			}

			public static StackCacheObject createFrom(StackLinkObjectCache cache,rtObject src)
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

        public StackLinkObjectCache(CSWC bin,Player player)
        {
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
						//cache[cls.structIndex] = player.alloc_pureHostedOrLinkedObject(cls);
						cache[cls.structIndex] = StackCacheObject.createFrom(this,player.alloc_pureHostedOrLinkedObject(cls));
					}
                }
            }
            
        }

        public StackCacheObject getCacheObj(Class cls)
        {
            //if (cls.isStruct)
            {//由于非结构体都是0，所以这里直接使用结构体索引返回即可
                return cache[cls.structIndex];
            }
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
			
        }

        public StackLinkObjectCache Clone()
        {
            StackLinkObjectCache c = new StackLinkObjectCache();
            if (cache != null)
            {
                c.cache = new StackCacheObject[cache.Length];
                for (int i = 0; i < cache.Length; i++)
                {
					//c.cache[i] = (rtObject)cache[i].Clone();
					c.cache[i] = StackCacheObject.createFrom(c,cache[i]);
				}
            }
            return c;
        }

    }
}
