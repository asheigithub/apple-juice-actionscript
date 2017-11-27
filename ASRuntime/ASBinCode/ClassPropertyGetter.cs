using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ASBinCode
{
	
	public sealed class ClassPropertyGetter :LeftValueBase, IMember 
    {
        public ASBinCode.rtti.Class _class;
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

       
        public IMember clone()
        {
            throw new NotImplementedException();
        }

        public sealed override  RunTimeValueBase getValue(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            throw new NotImplementedException();
        }


        private PropertySlot _tempSlot;

        public sealed override SLOT getSlotForAssign(RunTimeScope scope, ASRuntime.StackFrame frame)
		{
            return _tempSlot;
        }

        public sealed override  SLOT getSlot(RunTimeScope scope, ASRuntime.StackFrame frame)
		{

            return _tempSlot;
        }

        public override string ToString()
        {
            return name + "{" + (getter!=null?"get;":" ") + (setter !=null?"set;":" ")+ "}";
        }



		public static ClassPropertyGetter LoadClassPropertyGetter(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			
			int indexofMember = reader.ReadInt32();
			string _name = reader.ReadString();

			ClassPropertyGetter cpg = new ClassPropertyGetter(_name, null, indexofMember);
			serizlized.Add(key, cpg);

			rtti.Class _class = serizlizer.DeserializeObject<rtti.Class>(reader, rtti.Class.LoadClass);
			cpg._class = _class;
			

			cpg.getter = serizlizer.DeserializeObject<MethodGetterBase>(reader, ISWCSerializableLoader.LoadIMember);
			cpg.setter = serizlizer.DeserializeObject<MethodGetterBase>(reader, ISWCSerializableLoader.LoadIMember);

			return cpg;
		}


		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(2);
			
			//private readonly int indexofMember;
			writer.Write(indexofMember);
			//private readonly string _name;
			writer.Write(_name);

			//public readonly ASBinCode.rtti.Class _class;
			serizlizer.SerializeObject(writer, _class);
			//public MethodGetterBase getter;
			serizlizer.SerializeObject(writer, getter);
			//public MethodGetterBase setter;
			serizlizer.SerializeObject(writer, setter);

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

			public override SLOT assign(RunTimeValueBase value, out bool success)
			{
				//throw new NotImplementedException();
				success = false;
				return this;
			}

			public sealed override bool directSet(RunTimeValueBase value)
            {
				//return false;
				throw new NotImplementedException();
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
