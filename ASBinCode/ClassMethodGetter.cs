using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public class ClassMethodGetter : MethodGetterBase
    {
        public ClassMethodGetter(string name, rtti.Class _class, int indexofMember
            , int refdefinedinblockid
            ):base(name,_class,indexofMember,refdefinedinblockid)
        {
            
        }


        //private MethodSlot _tempSlot;

        public sealed  override  IRunTimeValue getValue(IRunTimeScope scope)
        {
            return getMethod(scope);
            //throw new NotImplementedException();
            //return getMethod(scope);
            //return getISlot(scope).getValue();
            //return getISlot(scope).getValue();
        }






        /// <summary>
        /// 如果此方法是一个构造函数。。在InstanceCreator中调用
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public sealed override  IRunTimeValue getConstructor(IRunTimeScope scope)
        {
            rtData.rtFunction method = new rtData.rtFunction(functionid, true);
            method.bind(scope);
            method.setThis(scope.this_pointer);
            return method;
        }

        public sealed override  IRunTimeValue getMethod(IRunTimeScope scope)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
            }


            if (!isNotReadVirtual)
            {

                var vmember = (ClassMethodGetter)((rtObject)scope.this_pointer).value._class.classMembers[indexofMember].bindField;

                rtData.rtFunction method = new rtData.rtFunction(vmember.functionid, true);
                method.bind(scope);
                method.setThis(scope.this_pointer);
                return method;

            }
            else
            {

                rtData.rtFunction method = new rtData.rtFunction(functionid, true);
                method.bind(scope);
                method.setThis(scope.this_pointer);
                return method;
            }

            //rtData.rtFunction method = new rtData.rtFunction(functionid, true);
            //method.bind(scope);
            //method.setThis(scope.this_pointer);
            //return method;
        }

        public sealed override IRunTimeValue getSuperMethod(IRunTimeScope scope, ASBinCode.rtti.Class superClass)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
            }


            var m = ((rtObject)scope.this_pointer).value._class.classMembers[indexofMember];
            while (!ReferenceEquals(m.virtualLinkFromClass, superClass))
            {
                m = m.virtualLink;
            }


            rtData.rtFunction method = new rtData.rtFunction(((ClassMethodGetter)m.bindField).functionid, true);
            method.bind(scope);
            method.setThis(scope.this_pointer);
            return method;
        }


        public sealed override ISLOT getVirtualSlot(IRunTimeScope scope)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
            }

            if (!isNotReadVirtual)
            {
                var vmember = (ClassMethodGetter)((rtObject)scope.this_pointer).value._class.classMembers[indexofMember].bindField;

                rtData.rtFunction method = new rtData.rtFunction(vmember.functionid, true);
                method.bind(scope);
                method.setThis(scope.this_pointer);
                return new MethodSlot(method);
            }
            else
            {
                rtData.rtFunction method = new rtData.rtFunction(functionid, true);
                method.bind(scope);
                method.setThis(scope.this_pointer);
                return new MethodSlot(method);
            }
        }

        public sealed override ISLOT getSuperSlot(IRunTimeScope scope, ASBinCode.rtti.Class superClass)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
            }

            var m = ((rtObject)scope.this_pointer).value._class.classMembers[indexofMember];
            while (!ReferenceEquals(m.virtualLinkFromClass, superClass))
            {
                m = m.virtualLink;
            }


            rtData.rtFunction method = new rtData.rtFunction(((ClassMethodGetter)m.bindField).functionid, true);
            method.bind(scope);
            method.setThis(scope.this_pointer);
            return new MethodSlot(method);
        }
    }
}
