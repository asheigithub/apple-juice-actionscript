using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASBinCode
{
	[Serializable]
    /// <summary>
    /// 数据存储槽接口
    /// </summary>
    public abstract class  SLOT
    {
        //void setValue(IRunTimeValue value);
        public virtual bool isPropGetterSetter { get { return false; } }

        public virtual bool isSetThisItem { get { return false; } }


        public abstract bool directSet(RunTimeValueBase value);

        public abstract RunTimeValueBase getValue();

        public abstract void clear();
        public abstract void setValue(rtBoolean value);
        public abstract void setValue(double value);
        public abstract void setValue(int value);
        public abstract void setValue(uint value);
        public abstract void setValue(string value);
        public abstract void setValue(rtNull value);
        public abstract void setValue(rtUndefined value);
    }

    

}
