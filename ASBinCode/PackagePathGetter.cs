using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public class PackagePathGetter : ILeftValue
    {
        public readonly string path;
        public PackagePathGetter(string path)
        {
            this.path = path;
        }


        public RunTimeDataType valueType
        {
            get
            {
                return RunTimeDataType.unknown;
            }
        }

        public ISLOT getISlot(IRunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public IRunTimeValue getValue(IRunTimeScope scope)
        {
            //***不可能在运行时运行***
            throw new NotImplementedException();
        }
    }
}
