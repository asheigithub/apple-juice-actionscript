using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime
{
    class RunTimeScope : IRunTimeScope
    {
        HeapSlot[] memberDataList;

        private IList<ISLOT> runtimestack;
        private int _offset;
        private int _blockid;
        public RunTimeScope(
            //IList<IMember> members,
            HeapSlot[] memberDataList,
            IList<ISLOT> rtStack,
            int offset, int blockid,
            IRunTimeScope parent,
            Dictionary<int, ASBinCode.rtData.rtObject> _static_scope,
            ASBinCode.rtData.rtObject this_pointer
            )
        {
            runtimestack = rtStack;
            this._offset = offset;
            _blockid = blockid;
            _parent = parent;

            this.memberDataList = memberDataList;
            this._static_scope = _static_scope;
            _this_pointer = this_pointer;
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

        public IList<ISLOT> stack
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
        private ASBinCode.rtData.rtObject _this_pointer;
        public ASBinCode.rtData.rtObject this_pointer
        {
            get
            {
                return _this_pointer;
            }
        }
    }

    

}
