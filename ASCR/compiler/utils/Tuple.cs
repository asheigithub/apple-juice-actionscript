using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler.utils
{
    class Tuple<T1,T2>
    {
        public readonly T1 item1;
        public readonly T2 item2;

        public Tuple(T1 item1,T2 item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

    }
}
