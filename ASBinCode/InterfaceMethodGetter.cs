using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASBinCode.rtData;

namespace ASBinCode
{
    public class InterfaceMethodGetter : MethodGetterBase
    {
        public InterfaceMethodGetter(string name, rtti.Class _class
            , int refdefinedinblockid
            ):base(name,_class,0,refdefinedinblockid)
        {
            
        }

        public void setIndexMember(int value,Class setter)
        {
            if (!ReferenceEquals(setter, _class))
            {
                throw new InvalidOperationException();
            }

            indexofMember = value;
        }

        public sealed override RunTimeValueBase getConstructor(RunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public sealed override RunTimeValueBase getMethod(RunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public sealed override RunTimeValueBase getSuperMethod(RunTimeScope scope, Class superClass)
        {
            throw new NotImplementedException();
        }

        public sealed override SLOT getSuperSlot(RunTimeScope scope, Class superClass)
        {
            throw new NotImplementedException();
        }

        public sealed override RunTimeValueBase getValue(RunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public sealed override SLOT getVirtualSlot(RunTimeScope scope)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
            }

            var instance_class = ((rtObject)scope.this_pointer).value._class;
            var vmember = (ClassMethodGetter)instance_class.classMembers[instance_class.implements[_class][indexofMember]].bindField;



            rtData.rtFunction method = new rtData.rtFunction(vmember.functionId, true);
            method.bind(scope);
            method.setThis(scope.this_pointer);
            return new MethodSlot(method);

            
        }
    }
}
