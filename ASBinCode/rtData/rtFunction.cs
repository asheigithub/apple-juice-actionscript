using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtFunction : IRunTimeValue
    {

        private IRunTimeScope _bindScope;

        public IRunTimeScope bindScope
        {
            get { return _bindScope; }
        }

        private int _functionid;

        public int functionId
        {
            get { return _functionid; }
        }

        public rtFunction(int id)
        {
            _functionid = id;_bindScope = null;
            
        }


        

        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_function;
            }
        }

        public void bind(IRunTimeScope scope)
        {
            _bindScope = scope;
        }


        public override string ToString()
        {
            return "function " + _functionid;
        }

        public object Clone()
        {
            rtFunction result= new rtFunction(_functionid);
            result.CopyFrom(this);
            return result;
        }

        public void CopyFrom(rtFunction right)
        {
            _functionid = right._functionid;
            _bindScope = right._bindScope;
        }

    }
}
