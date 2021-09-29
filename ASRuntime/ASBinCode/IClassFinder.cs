using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public interface IClassFinder
    {
        
        ASBinCode.rtti.Class getClassByRunTimeDataType(RunTimeDataType rttype);


		rtti.Class getClassDefinitionByName(string name);

    }
}
