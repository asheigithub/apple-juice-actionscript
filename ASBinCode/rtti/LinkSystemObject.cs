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

        public override LinkSystemObject Clone()
        {
            return new LinkObj<T>(_class, value);
        }

        public override void CopyData(LinkSystemObject other)
        {
            value = ((LinkObj<T>)other).value;
        }


        public sealed override bool Equals(object obj)
        {
            LinkObj<T> r = obj as LinkObj<T>;
            if (r == null)
            {
                return false;
            }

            return Equals(value, r.value);

        }



        public sealed override int GetHashCode()
        {
            return value.GetHashCode();
        }

    }


    public abstract class LinkSystemObject : Object
    {
        /// <summary>
        /// 是否是刚new出来的。刚new出来的第一次赋值是不需要Clone的
        /// </summary>
        public bool __iscreateout;

        public LinkSystemObject(Class _class):base(_class)
        {
            __iscreateout = true;
        }

        public abstract LinkSystemObject Clone();

        
        public abstract void CopyData(LinkSystemObject other);

        public T getLinkedObject<T>()
        {
            LinkObj<T> t = this as LinkObj<T>;
            if (t == null)
            {
                return default(T);
            }
            return t.value;
        }


    }
}
