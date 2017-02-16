using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.scopes
{
    public abstract class ScopeBase : IScope
    {
        private List<IMember> memberlist;

        private IScope _parent;

        public ScopeBase()
        {
            memberlist = new List<IMember>();
            
        }



        public List<IMember> members
        {
            get
            {
                return memberlist;
            }
        }

        public IScope parentScope
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
    }
}
