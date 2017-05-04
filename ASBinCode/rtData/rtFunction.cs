using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public sealed class rtFunction : RunTimeValueBase
    {
        public class functionObjHandle
        {
            public rtObject bindFunctionObj;

            public override int GetHashCode()
            {
                if (bindFunctionObj == null)
                {
                    return 0.GetHashCode();
                }
                else
                {
                    return bindFunctionObj.GetHashCode();
                }
            }
            public override bool Equals(object obj)
            {
                functionObjHandle right = obj as functionObjHandle;
                if (right == null)
                {
                    return false;
                }
                return ReferenceEquals(bindFunctionObj, right.bindFunctionObj);
            }
        }


        private readonly int _objid;
        private static int _seed;

        private RunTimeScope _bindScope;

        public RunTimeScope bindScope
        {
            get { return _bindScope; }
        }


        private RunTimeValueBase _this_pointer;
        public RunTimeValueBase this_pointer
        {
            get { return _this_pointer; }
        }


        public functionObjHandle objHandle;


        private int _functionid;

        public int functionId
        {
            get { return _functionid; }
        }


        private bool _ismethod;
        public rtFunction(int id,bool ismethod):base(RunTimeDataType.rt_function)
        {
            _functionid = id;_bindScope = null;
            this._ismethod = ismethod;
            _objid = _seed++;

            objHandle = new functionObjHandle();
        }


       

        public bool ismethod
        {
            get
            {
                return _ismethod;
            }
        }

        public void bind(RunTimeScope scope)
        {
            _bindScope = scope;
        }

        public void setThis(RunTimeValueBase obj)
        {
            _this_pointer = obj;
        }


        public override string ToString()
        {
            return "function " + _functionid;
        }

        public sealed override  object Clone()
        {
            rtFunction result= new rtFunction(_functionid,_ismethod);
            result.CopyFrom(this);
            return result;
        }

        public void CopyFrom(rtFunction right)
        {
            //_objid = right._objid;
            _functionid = right._functionid;
            _bindScope = right._bindScope;
            _ismethod = right._ismethod;
            _this_pointer = right._this_pointer;

            objHandle = right.objHandle;
        }

        public override int GetHashCode()
        {
            //return base.GetHashCode();
            return _functionid.GetHashCode() ^ _bindScope.GetHashCode() ^
                _ismethod.GetHashCode() ^ _this_pointer.GetHashCode() ^ objHandle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            rtFunction right = obj as rtFunction;
            if (right == null)
            {
                return false;
            }

            return _functionid == right._functionid
                //&& _bindScope.Equals(right._bindScope) 
                && ReferenceEquals(_bindScope,right._bindScope)
                && _ismethod == right._ismethod
                && //_this_pointer.Equals(right._this_pointer);
                ReferenceEquals(_this_pointer, right._this_pointer)
                && objHandle.Equals(right.objHandle);
                
                ;
        }


        public static bool isTypeEqual(rtFunction fun1,rtFunction fun2)
        {
            return fun1._functionid == fun2._functionid;
        }
    }
}
