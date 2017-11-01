using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
	[Serializable]
	/// <summary>
	/// 右值
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class RightValueBase
    {
        public abstract RunTimeValueBase getValue(RunTimeScope scope , RunTimeDataHolder dataHolder );

		
		//public abstract RunTimeDataType valueType { get; }
		public RunTimeDataType valueType;
    }
	[Serializable]
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
        public abstract SLOT getSlot(RunTimeScope scope, RunTimeDataHolder dataHolder);

        public abstract SLOT getSlotForAssign(RunTimeScope scope, RunTimeDataHolder dataHolder);
    }


    

}
