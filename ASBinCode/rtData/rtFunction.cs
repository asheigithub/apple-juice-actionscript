using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public class rtFunction : IRunTimeValue
    {
        private int _objid;
        private static int _seed;

        private IRunTimeScope _bindScope;

        public IRunTimeScope bindScope
        {
            get { return _bindScope; }
        }


        private rtObject _this_pointer;
        public rtObject this_pointer
        {
            get { return _this_pointer; }
        }


        private int _functionid;

        public int functionId
        {
            get { return _functionid; }
        }


        private bool _ismethod;
        public rtFunction(int id,bool ismethod)
        {
            _functionid = id;_bindScope = null;
            this._ismethod = ismethod;
            _objid = _seed++;
        }


        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_function;
            }
        }

        public bool ismethod
        {
            get
            {
                return _ismethod;
            }
        }

        public void bind(IRunTimeScope scope)
        {
            _bindScope = scope;
        }

        public void setThis(rtObject obj)
        {
            _this_pointer = obj;
        }


        public override string ToString()
        {
            return "function " + _functionid;
        }

        public object Clone()
        {
            rtFunction result= new rtFunction(_functionid,_ismethod);
            result.CopyFrom(this);
            return result;
        }

        public void CopyFrom(rtFunction right)
        {
            _objid = right._objid;
            _functionid = right._functionid;
            _bindScope = right._bindScope;
            _ismethod = right._ismethod;
            _this_pointer = right._this_pointer;
        }

        public static bool isEqual(rtFunction fun1,rtFunction fun2)
        {
            return fun1._objid == fun2._objid;
        }
    }
}
