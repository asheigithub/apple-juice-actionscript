using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    /// <summary>
    /// 字典类的对象
    /// </summary>
    public sealed class DictionaryObject:DynamicObject
    {
        public interface IDictionaryKey { }
        
        private Dictionary<IDictionaryKey, SLOT> innerDictionary;

        public DictionaryObject(Class _class):base(_class)
        {
            innerDictionary = new Dictionary<IDictionaryKey, SLOT>();
        }

        public bool isContainsKey(IDictionaryKey value)
        {
            return innerDictionary.ContainsKey(value);
        }


        private ILinkSlot rootSlot;

        public IEnumerator<ILinkSlot> eachDictSlot()
        {
            var root = rootSlot;

            while (root != null)
            {
                if (root.propertyIsEnumerable && !root.isDeleted)
                {
                    yield return root;
                }
                root = root.nextSlot;
            }
            yield break;
        }


        public void createKeyValue(IDictionaryKey key, ILinkSlot value)
        {
            if (!isContainsKey(key))
            {
                innerDictionary.Add(key, (SLOT)value);
                if (rootSlot == null)
                {
                    rootSlot = value;
                }
                else
                {
                    rootSlot.preSlot = value;
                    value.nextSlot = rootSlot;
                    rootSlot = value;

                }
            }
        }

        

        public void RemoveKey(IDictionaryKey key)
        {
            
            if (innerDictionary.ContainsKey(key))
            {
                SLOT slot = innerDictionary[key];
                innerDictionary.Remove(key);

                ILinkSlot ls = slot as ILinkSlot;
                if (ls != null)
                {
                    ls.isDeleted = true;
                    if (ls.preSlot != null)
                    {
                        ls.preSlot.nextSlot = ls.nextSlot;

                        if (ls.nextSlot != null)
                        {
                            ls.nextSlot.preSlot = ls.preSlot;
                        }

                        ls.preSlot = null;
                    }
                    else
                    {
                        if (ls.nextSlot != null)
                        {
                            ls.nextSlot.preSlot = null;
                        }
                    }

                }
            }
        }

        public SLOT getValue(IDictionaryKey key)
        {
            return innerDictionary[key];
        }

    }
}
