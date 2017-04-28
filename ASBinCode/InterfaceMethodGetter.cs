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

        public sealed override IRunTimeValue getConstructor(IRunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public sealed override IRunTimeValue getMethod(IRunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public sealed override IRunTimeValue getSuperMethod(IRunTimeScope scope, Class superClass)
        {
            throw new NotImplementedException();
        }

        public sealed override ISLOT getSuperSlot(IRunTimeScope scope, Class superClass)
        {
            throw new NotImplementedException();
        }

        public sealed override IRunTimeValue getValue(IRunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public sealed override ISLOT getVirtualSlot(IRunTimeScope scope)
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
