using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
	[Serializable]
	public abstract class NativeFunctionBase
    {
        public enum NativeFunctionMode
        {
            normal_0=0,
            async_0=1,

            normal_1=2,
            const_parameter_0=3,
        }

        public abstract string name { get; }
        /// <summary>
        /// 参数定义
        /// </summary>
        public abstract List<RunTimeDataType> parameters { get; }
        /// <summary>
        /// 返回类型定义
        /// </summary>
        public abstract RunTimeDataType returnType { get; }

        public abstract bool isMethod { get; }
        
        public IClassFinder bin { get; set; }


        /// <summary>
        /// 指示是否同步完成
        /// </summary>
        //public virtual bool isAsync
        //{
        //    get { return false; }
        //}
        public virtual NativeFunctionMode mode
        {
            get { return NativeFunctionMode.normal_0; }
        }


        /// <summary>
        /// 调用函数
        /// </summary>
        /// <param name="thisObj"></param>
        /// <returns></returns>
        public abstract RunTimeValueBase execute(RunTimeValueBase thisObj,SLOT[] argements,object stackframe,out string errormessage,out int errorno);

        public virtual void executeAsync(RunTimeValueBase thisObj,SLOT[] argements,SLOT resultSlot , 
            object callbacker ,object stackframe,
            SourceToken token, RunTimeScope scope)
        {

        }

        public virtual void execute2(RunTimeValueBase thisObj, 
            rtti.FunctionDefine functionDefine,
            SLOT[] argements, 
            SLOT   returnSlot,
            SourceToken token,
            object stackframe, 
            out bool success)
        {
            throw new NotImplementedException();
        }

    }
}
