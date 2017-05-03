using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime
{
    sealed class RunTimeScope : IRunTimeScope
    {
        HeapSlot[] memberDataList;

        private ISLOT[] runtimestack;
        private int _offset;
        private int _blockid;
        public RunTimeScope(
            //IList<IMember> members,
            HeapSlot[] memberDataList,
            ISLOT[] rtStack,
            int offset, int blockid,
            IRunTimeScope parent,
            Dictionary<int, ASBinCode.rtData.rtObject> _static_scope,
            IRunTimeValue this_pointer,
            RunTimeScopeType type
            //,
            //Dictionary<ClassMethodGetter, Dictionary<ASBinCode.rtData.rtObject, ISLOT>> dictMethods
            )
        {
            runtimestack = rtStack;
            this._offset = offset;
            _blockid = blockid;
            _parent = parent;

            this.memberDataList = memberDataList;
            this._static_scope = _static_scope;
            _this_pointer = this_pointer;
            _scopeType = type;
            //this._dictMethods = dictMethods;
        }



        public ISLOT[] memberData
        {
            get
            {
                return memberDataList;
            }
        }

        public int offset
        {
            get
            {
                return _offset;
            }
        }

        private IRunTimeScope _parent;
        public IRunTimeScope parent
        {
            get
            {
                return _parent;
            }
        }

        public ISLOT[] stack
        {
            get
            {
                return runtimestack;
            }
        }

        public int blockId
        {
            get
            {
                return _blockid;
            }
        }

        private Dictionary<int, ASBinCode.rtData.rtObject> _static_scope;
        public Dictionary<int, ASBinCode.rtData.rtObject> static_objects
        {
            get
            {
                return _static_scope;
            }
        }
        private IRunTimeValue _this_pointer;
        public IRunTimeValue this_pointer
        {
            get
            {
                return _this_pointer;
            }
        }

        private RunTimeScopeType _scopeType;
        public RunTimeScopeType scopeType
        {
            get
            {
                return _scopeType;
            }
        }


        
    }

    

}
