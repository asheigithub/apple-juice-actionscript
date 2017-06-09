using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public abstract class LinkTypeMapper
    {
        public abstract void init(CSWC swc);

        public abstract Type getLinkType(RunTimeDataType rtType);
        

        public abstract RunTimeDataType getRuntimeDataType(Type linkType);
        

        /// <summary>
        /// 将一个系统对象保存到StackSlot中
        /// </summary>
        public abstract void storeLinkObject_ToSlot(object obj, FunctionDefine funcDefine, SLOT returnSlot, IClassFinder bin, object player);
        

        public abstract bool rtValueToLinkObject(RunTimeValueBase value, Type linkType, IClassFinder bin , out object linkobject);

        
    }
}
