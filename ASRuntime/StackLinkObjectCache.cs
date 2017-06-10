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
        private rtObject[] cache;

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
                cache = new rtObject[maxstructidx + 1];
                cache[0] = player.alloc_pureHostedOrLinkedObject(player.swc.LinkObjectClass);
                foreach (var item in bin.class_Creator)
                {
                    var cls = item.Key;
                    if (cls.isStruct)
                    {
                        cache[cls.structIndex] = player.alloc_pureHostedOrLinkedObject(cls);
                    }
                }
            }
            
        }

        public rtObject getCacheObj(Class cls)
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
                c.cache = new rtObject[cache.Length];
                for (int i = 0; i < cache.Length; i++)
                {
                    c.cache[i] = (rtObject)cache[i].Clone();
                }
            }
            return c;
        }

    }
}
