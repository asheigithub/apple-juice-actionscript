using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtData;

namespace ASBinCode
{
    public class ClassMethodGetter : ILeftValue,IMember
    {
        private readonly ASBinCode.rtti.Class _class;
        private readonly int indexofMember;
        
        private readonly string _name;

        public readonly int refdefinedinblockid;

        private int functionid;
        public void setFunctionId(int functionid)
        {
            this.functionid = functionid;
        }

        public ClassMethodGetter(string name, rtti.Class _class,int indexofMember
            ,int refdefinedinblockid
            )
        {
            this._class = _class;
            this.indexofMember = indexofMember;
            this._name = name;
            this.refdefinedinblockid = refdefinedinblockid;
        }


        public RunTimeDataType valueType
        {
            get
            {
                return RunTimeDataType.rt_function;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public int indexOfMembers
        {
            get
            {
                return indexofMember;
            }
        }

        public ISLOT getISlot(IRunTimeScope scope)
        {
            //if (_tempSlot == null)
            //{
            rtData.rtFunction method = new rtData.rtFunction(functionid, true);
            method.bind(scope);
            method.setThis(scope.this_pointer);
            return new MethodSlot(method);
            //    _tempSlot = new MethodSlot(method);

            //}
            //else if( !
            //    (_tempSlot.method.bindScope.Equals( scope )
            //    &&
            //    _tempSlot.method.this_pointer.Equals( scope.this_pointer )
            //    ) )
            //{
            //    rtData.rtFunction method = new rtData.rtFunction(functionid, true);
            //    method.bind(scope);
            //    method.setThis(scope.this_pointer);
            //    _tempSlot.method = method;
            //}

            //return _tempSlot;

            //Dictionary<rtObject, ISLOT> slots;

            //if (!scope.dictMethods.TryGetValue(this, out slots))
            //{
            //    slots = new Dictionary<rtObject, ISLOT>();
            //    scope.dictMethods.Add(this, slots);
            //}

            //ISLOT slot;
            //if (!slots.TryGetValue(scope.this_pointer, out slot))
            //{
            //    rtData.rtFunction method = new rtData.rtFunction(functionid, true);
            //    method.bind(scope);
            //    method.setThis(scope.this_pointer);
            //    slot = new MethodSlot(method);
            //    slots.Add( scope.this_pointer,slot );

            //}
            //return slot;

            //throw new NotImplementedException();    
        }


        //private MethodSlot _tempSlot;

        public IRunTimeValue getValue(IRunTimeScope scope)
        {
            //throw new NotImplementedException();
            //return getISlot(scope).getValue();
            return getISlot(scope).getValue();
            
        }

        public IMember clone()
        {
            throw new NotImplementedException();
            //return new ClassMethodGetter(_name, _class, indexofMember, refdefinedinblockid);
        }

        public override string ToString()
        {
            return name;
        }

        //public override int GetHashCode()
        //{
        //    return _name.GetHashCode() ^ _class.GetHashCode() ^ indexofMember.GetHashCode() ^ refdefinedinblockid.GetHashCode(); 
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) { return false; }

        //    if (!(obj.GetType().Equals(this.GetType())))
        //    {
        //        return false;
        //    }

        //    ClassMethodGetter other = (ClassMethodGetter)obj;
        //    return _name == other._name &&
        //        _class == other._class &&
        //        indexofMember == other.indexofMember &&
        //        refdefinedinblockid == other.refdefinedinblockid;

        //}

        public class MethodSlot : ISLOT
        {
            private rtFunction method;

            public MethodSlot(rtFunction method)
            {
                
                this.method = method;
            }

            public bool isPropGetterSetter
            {
                get
                {
                    return false;
                }
            }

            public void clear()
            {

            }

            public bool directSet(IRunTimeValue value)
            {
                return false;
            }

            public IRunTimeValue getValue()
            {
                return method;
            }

            public void setValue(rtNull value)
            {
                throw new NotImplementedException();
            }

            public void setValue(rtUndefined value)
            {
                throw new NotImplementedException();
            }

            public void setValue(string value)
            {
                throw new NotImplementedException();
            }

            public void setValue(int value)
            {
                throw new NotImplementedException();
            }

            public void setValue(uint value)
            {
                throw new NotImplementedException();
            }

            public void setValue(double value)
            {
                throw new NotImplementedException();
            }

            public void setValue(rtBoolean value)
            {
                throw new NotImplementedException();
            }
        }
    }
}
