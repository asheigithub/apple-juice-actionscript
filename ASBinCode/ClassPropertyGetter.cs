using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public sealed class ClassPropertyGetter :LeftValueBase, IMember 
    {
        public readonly ASBinCode.rtti.Class _class;
        private readonly int indexofMember;

        private readonly string _name;
        public ClassPropertyGetter(string name, rtti.Class _class, int indexofMember
            )
        {
            this._class = _class;
            this.indexofMember = indexofMember;
            this._name = name;
            this._tempSlot = new PropertySlot();
            this.valueType = RunTimeDataType.unknown;
        }

        
        public MethodGetterBase getter;
        public MethodGetterBase setter;


        public int indexOfMembers
        {
            get
            {
                return indexofMember;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        //public sealed override  RunTimeDataType valueType
        //{
        //    get
        //    {
        //        if (getter == null)
        //        {
        //            throw new NotImplementedException();
        //        }
        //        else
        //        {
        //            throw new NotImplementedException();
        //            //return getter.valueType;
        //        }
                
        //    }
        //}

        public IMember clone()
        {
            throw new NotImplementedException();
        }

        public sealed override  RunTimeValueBase getValue(RunTimeScope scope)
        {
            throw new NotImplementedException();
        }


        private PropertySlot _tempSlot;

        //public PropertySlot propSlot
        //{
        //    get { return _tempSlot; }
        //}

        public sealed override  SLOT getSlot(RunTimeScope scope)
        {
            //if (_tempSlot == null)
            //{
            //    _tempSlot = new PropertySlot(scope.this_pointer, scope, this);

            //}
            //else if (!
            //    (_tempSlot.scope.Equals(scope)
            //    &&
            //    _tempSlot.bindObj.Equals(scope.this_pointer)
            //    ))
            //{
            //    _tempSlot.bindObj = scope.this_pointer;
            //    _tempSlot.scope = scope;

            //}

            return _tempSlot;
        }

        public override string ToString()
        {
            return name + "{" + (getter!=null?"get;":" ") + (setter !=null?"set;":" ")+ "}";
        }

        public sealed class PropertySlot : SLOT
        {
            //public rtObject bindObj;
            //public IRunTimeScope scope;
            //public ClassPropertyGetter property;
            public PropertySlot()// rtObject bindObj, IRunTimeScope scope,ClassPropertyGetter property)
            {
                //this.bindObj = bindObj;
                //this.scope = scope;
                //this.property = property;
            }

            public sealed override bool isPropGetterSetter
            {
                get
                {
                    return true;
                }
            }

            public sealed override void clear()
            {
                throw new NotImplementedException();
            }

            public sealed override bool directSet(RunTimeValueBase value)
            {
                return false;
            }

            public sealed override RunTimeValueBase getValue()
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(rtNull value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(rtUndefined value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(string value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(int value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(uint value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(double value)
            {
                throw new NotImplementedException();
            }

            public sealed override void setValue(rtBoolean value)
            {
                throw new NotImplementedException();
            }
        }

    }
}
