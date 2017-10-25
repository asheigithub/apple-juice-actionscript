using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
	[Serializable]
    public sealed class rtFunction : RunTimeValueBase
    {
		[Serializable]
        public class functionObjHandle
        {
			[NonSerialized]
            public rtObject bindFunctionObj;

            //public override int GetHashCode()
            //{
            //    if (bindFunctionObj == null)
            //    {
            //        return 0.GetHashCode();
            //    }
            //    else
            //    {
            //        return bindFunctionObj.GetHashCode();
            //    }
            //}
            //public override bool Equals(object obj)
            //{
            //    functionObjHandle right = obj as functionObjHandle;
            //    if (right == null)
            //    {
            //        return false;
            //    }
            //    return ReferenceEquals(bindFunctionObj, right.bindFunctionObj);
            //}
        }


        private int _objid;
        private static int _seed;
		[NonSerialized]
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
        public rtFunction(int id,bool ismethod):this(id,ismethod,_seed++)
        { 
        }

        private rtFunction(int id, bool ismethod,int objid):base(RunTimeDataType.rt_function)
        {
            _functionid = id; _bindScope = null;
            this._ismethod = ismethod;
            _objid = objid;

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
			//if (obj is rtObject)
			//{
			//	_this_pointer = ((rtObject)obj).getSrcObject();
			//}
			//else
			{
				_this_pointer = obj;
			}
        }

        public override double toNumber()
        {
            return double.NaN;
        }

        public override string ToString()
        {
            return "function " + _functionid;
        }

        public sealed override  object Clone()
        {
            rtFunction result= new rtFunction(_functionid,_ismethod,_objid);
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

            objHandle = right.objHandle;
        }

        public override int GetHashCode()
        {
            //return base.GetHashCode();
            //return _functionid.GetHashCode() ^ _bindScope.GetHashCode() ^
            //    _ismethod.GetHashCode() ^ _this_pointer.GetHashCode() ^ objHandle.GetHashCode();

            if (ismethod)
            {
                return _functionid.GetHashCode() ^ _ismethod.GetHashCode() ^ _this_pointer.GetHashCode();
            }
            else
            {
                return _functionid.GetHashCode() ^ _ismethod.GetHashCode() ^ _objid.GetHashCode(); //objHandle.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            rtFunction right = obj as rtFunction;
            if (right == null)
            {
                return false;
            }

            if (_ismethod == right._ismethod && _functionid==right.functionId)
            {
                if (_ismethod)
                {
                    return this_pointer.Equals(right.this_pointer);
                }
                else
                {
                    return _objid == right._objid; //objHandle.Equals(right.objHandle);
                }
            }
            else
            {
                return false;
            }

            //return _functionid == right._functionid
            //    //&& _bindScope.Equals(right._bindScope) 
            //    && ReferenceEquals(_bindScope,right._bindScope)
            //    && _ismethod == right._ismethod
            //    && //_this_pointer.Equals(right._this_pointer);
            //    ReferenceEquals(_this_pointer, right._this_pointer)
            //    && objHandle.Equals(right.objHandle);
                
            //    ;
        }


        public static bool isFunctionEqual(rtFunction fun1,rtFunction fun2)
        {
            //return fun1.objHandle == fun2.objHandle;

            return fun1.Equals(fun2);

            //return fun1._functionid == fun2._functionid;
            //return fun1.this_pointer == fun2.this_pointer;
            //return fun1.Equals(fun2);
        }
    }
}
