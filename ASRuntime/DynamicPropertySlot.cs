using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASRuntime
{
    /// <summary>
    /// 保存动态属性的插槽
    /// </summary>
    class DynamicPropertySlot : ObjectMemberSlot,ILinkSlot
    {
        internal bool _canDelete;
        internal ASBinCode.RunTimeValueBase backup;
        public DynamicPropertySlot(ASBinCode.rtData.rtObject obj,bool _canDelete, RunTimeDataType functionClassRtType) :base(obj,functionClassRtType)
        {
            this._canDelete = _canDelete;
            propertyIsEnumerable = false;
        }

        internal string _propname;

        public ILinkSlot preSlot
        {
            get
            ;

            set
            ;
        }

        public ILinkSlot nextSlot
        {
            get
            ;
            set
            ;
        }

        public bool propertyIsEnumerable
        {
            get
            ;

            set
            ;
        }

        public bool isDeleted
        {
            get;
            set;
        }

        public override bool directSet(RunTimeValueBase value)
        {
            base.directSet(value);
            if (!_canDelete && backup ==null)
            {
                backup = value;
            }
            return true;
        }

    }
}
