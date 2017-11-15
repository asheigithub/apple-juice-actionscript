using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	/// <summary>
	/// 右值
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class RightValueBase :ISWCSerializable
    {
        public abstract RunTimeValueBase getValue(RunTimeScope scope , ASRuntime.StackSlot[] slots ,int stoffset );

		//public abstract RunTimeDataType valueType { get; }
		public RunTimeDataType valueType;






		public virtual void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(valueType);
		}



	}
	
	/// <summary>
	/// 左值
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class LeftValueBase : RightValueBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public abstract SLOT getSlot(RunTimeScope scope, ASRuntime.StackSlot[] slots, int stoffset);

        public abstract SLOT getSlotForAssign(RunTimeScope scope, ASRuntime.StackSlot[] slots, int stoffset);
    }


    

}
