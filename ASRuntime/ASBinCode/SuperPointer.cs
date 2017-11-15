using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	/// <summary>
	/// 访问父类的成员
	/// </summary>
	public sealed class SuperPointer : RightValueBase
    {
        public rtti.Class superClass;

        public rtti.Class thisClass;

        public SuperPointer(ASBinCode.rtti.Class superClass,rtti.Class thisClass)
        {
            this.superClass = superClass;
            this.thisClass = thisClass;
            valueType = superClass.getRtType();
        }


        //public sealed override  RunTimeDataType valueType
        //{
        //    get
        //    {
        //        //throw new NotImplementedException();
        //        return superClass.getRtType();
        //    }
        //}

        public sealed override RunTimeValueBase getValue(RunTimeScope scope, ASRuntime.StackSlot[] slots, int stoffset)
		{
            return scope.this_pointer;
        }

        public override string ToString()
        {
            return "super";
        }






		public static SuperPointer LoadSuperPointer(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType valuetype = reader.ReadInt32();
			rtti.Class superClass = serizlizer.DeserializeObject<rtti.Class>(reader, rtti.Class.LoadClass);
			rtti.Class thisClass = serizlizer.DeserializeObject<rtti.Class>(reader, rtti.Class.LoadClass);

			SuperPointer sp = new SuperPointer(superClass, thisClass); serizlized.Add(key, sp);
			sp.valueType = valuetype;

			return sp;

		}




		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(9);
			base.Serialize(writer, serizlizer);

			serizlizer.SerializeObject(writer, superClass);
			serizlizer.SerializeObject(writer, thisClass);

			//throw new NotImplementedException();
		}

	}
}
