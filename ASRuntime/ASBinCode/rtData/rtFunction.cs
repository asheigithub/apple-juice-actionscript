using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtData
{
	
    public sealed class rtFunction : RunTimeValueBase
    {
		class WapperContainer
		{
			public Dictionary<RunTimeDataType, ASRuntime.FunctionWapper> dictWappers;
		}


        private int _objid;
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

		
		public rtObjectBase objHandle;

		private WapperContainer wapperContainer;

		public Dictionary<RunTimeDataType, ASRuntime.FunctionWapper> dictWappers
		{
			get
			{
				if (wapperContainer == null)
					return null;
				return wapperContainer.dictWappers;
			}
			set
			{
				if (wapperContainer == null)
				{
					wapperContainer = new WapperContainer();
				}
				wapperContainer.dictWappers = value;
			}
		}


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

			//需要保证所有函数对象一致,因此会被拷贝传下去
			wapperContainer = new WapperContainer();

			objHandle = null;
			
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
			wapperContainer = right.wapperContainer;
        }

		public void SetValue(int functionid, bool ismethod)
		{
			_functionid = functionid;
			_ismethod = ismethod;
		}


		public void Clear()
		{
			//_objid = -1;
			//_functionid = -1;
			//_ismethod = false; //值类型无需清理

			wapperContainer = null;

			_bindScope = null;
			_this_pointer = null;
			objHandle = null;
			
		}

        public override int GetHashCode()
        {
            if (ismethod)
            {
                return _functionid.GetHashCode() ^ _ismethod.GetHashCode() ^ (_this_pointer==null?0:this_pointer.GetHashCode());
            }
            else
            {
                return _functionid.GetHashCode() ^ _ismethod.GetHashCode() ^ _objid.GetHashCode();
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
					return Equals(this_pointer, right.this_pointer); //this_pointer.Equals(right.this_pointer);
                }
                else
                {
                    return _objid == right._objid;
                }
            }
            else
            {
                return false;
            }
        }


        public static bool isFunctionEqual(rtFunction fun1,rtFunction fun2)
        {
            return fun1.Equals(fun2);
        }





		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(rtType);
			writer.Write(_functionid);
			writer.Write(_ismethod);
			
		}




	}
}
