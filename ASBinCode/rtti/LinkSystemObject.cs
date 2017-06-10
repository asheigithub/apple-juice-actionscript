using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    public sealed class LinkObj<T> : LinkSystemObject
    {
        public T value;

        public LinkObj(Class cls) : this(cls, default(T))
        { }
        
        public LinkObj(Class cls, T v) : base(cls)
        {
            value = v;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override LinkSystemObject Clone()
        {
            return new LinkObj<T>(_class, value);
        }

        public override void CopyStructData(LinkSystemObject other)
        {
            value = ((LinkObj<T>)other).value;
        }

        public override void SetLinkData( object linkvalue)
        {
            value = (T)linkvalue;
        }


        
        public override object GetLinkData()
        {
            return value;
        }

        public sealed override bool Equals(object obj)
        {
            
            LinkObj<T> other = obj as LinkObj<T>;
            if (other != null)
            {
                return Equals(other.value, value);
            }
            else
            {
                LinkObj<object> o2 = obj as LinkObj<object>;
                if (o2 != null)
                {
                    return Equals(value,o2.value);
                }
                else
                {
                    return false;
                }
            }

        }

        



        public sealed override int GetHashCode()
        {
            return value.GetHashCode();
        }

    }


    public abstract class LinkSystemObject : Object
    {
        
        public LinkSystemObject(Class _class):base(_class)
        {
            //__iscreateout = true;
        }

        public abstract LinkSystemObject Clone();

        
        public abstract void CopyStructData(LinkSystemObject other);

        public abstract void SetLinkData( object linkvalue);
        /// <summary>
        /// 获取链接的对象
        /// </summary>
        /// <returns></returns>
        public abstract object GetLinkData();
    }
}
