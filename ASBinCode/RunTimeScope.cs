using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASBinCode
{
    public sealed class RunTimeScope //: IRunTimeScope
    {
        HeapSlot[] memberDataList;

        private SLOT[] runtimestack;
        private int _offset;
        private int _blockid;
        public RunTimeScope(
            //IList<IMember> members,
            HeapSlot[] memberDataList,
            SLOT[] rtStack,
            int offset, int blockid,
            RunTimeScope parent,
            Dictionary<int, ASBinCode.rtData.rtObject> _static_scope,
            RunTimeValueBase this_pointer,
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



        public SLOT[] memberData
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

        private RunTimeScope _parent;
        public RunTimeScope parent
        {
            get
            {
                return _parent;
            }
        }

        public SLOT[] stack
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
        private RunTimeValueBase _this_pointer;
        public RunTimeValueBase this_pointer
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
