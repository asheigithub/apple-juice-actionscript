using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public sealed class PackagePathGetter : LeftValueBase
    {
        public readonly string path;
        public PackagePathGetter(string path)
        {
            this.path = path;
        }


        //public sealed override  RunTimeDataType valueType
        //{
        //    get
        //    {
        //        return RunTimeDataType.unknown;
        //    }
        //}

        public override sealed SLOT getSlot(RunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public override sealed RunTimeValueBase getValue(RunTimeScope scope)
        {
            //***不可能在运行时运行***
            throw new NotImplementedException();
        }
    }
}
