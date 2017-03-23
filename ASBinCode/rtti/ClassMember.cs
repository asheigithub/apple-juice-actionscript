using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    /// <summary>
    /// 类成员
    /// </summary>
    public class ClassMember
    {

        public bool isPublic;
        public bool isInternal;
        public bool isPrivate;
        public bool isProtectd;

        /// <summary>
        /// 是否属性读取器
        /// </summary>
        public bool isGetter;
        /// <summary>
        /// 是否属性设置器
        /// </summary>
        public bool isSetter;


        /// <summary>
        /// 是否覆盖基类方法
        /// </summary>
        public bool isOverride;

        /// <summary>
        /// 是否是静态成员
        /// </summary>
        public bool isStatic;
        /// <summary>
        /// 是否是常量
        /// </summary>
        public bool isConst;

        /// <summary>
        /// 是否是构造函数
        /// </summary>
        public bool isConstructor;
        /// <summary>
        /// 成员名
        /// </summary>
        public string name;
        /// <summary>
        /// 成员字面值,比如1000,"aabc"等确定的字面值
        /// </summary>
        public IRunTimeValue defaultValue;

        public readonly int index;

        public ClassMember(string name,int index)
        {
            this.index = index;
            this.name = name;
            _type = RunTimeDataType.rt_void;
        }
        public void setTypeWhenCompile(RunTimeDataType t)
        {
            _type = t;
        }

        private RunTimeDataType _type;
        /// <summary>
        /// 成员类型
        /// </summary>
        public RunTimeDataType type
        {
            get
            {
                return _type;
            }
        }

    }
}
