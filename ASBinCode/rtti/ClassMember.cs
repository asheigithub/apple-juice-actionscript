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

        public bool isFinal;

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

        /// <summary>
        /// 所属Class
        /// </summary>
        public readonly Class refClass;

        public readonly IMember bindField;

        /// <summary>
        /// 从哪个类继承而来
        /// </summary>
        public Class inheritFrom;
        /// <summary>
        /// 继承源
        /// </summary>
        public ClassMember inheritSrcMember;


        public ClassMember virtualLink;
        public Class virtualLinkFromClass;

        public ClassMember(string name,Class refClass, IMember bindField)
        {

            this.name = name;
            this.refClass = refClass;
            this.bindField = bindField;
            _type = RunTimeDataType.rt_void;
        }
        public void setTypeWhenCompile(RunTimeDataType t)
        {
            _type = t;
        }

        //public ISLOT getISlot(IRunTimeScope scope)
        //{
        //    //throw new NotImplementedException();
        //    if (scope.blockId == refClass.blockid)
        //    {
        //        return scope.memberData[index];
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException();
        //    }

        //}

        //public IRunTimeValue getValue(IRunTimeScope scope)
        //{
        //    return getISlot(scope).getValue();
        //}

        private RunTimeDataType _type;
        
        /// <summary>
        /// 成员类型
        /// </summary>
        public RunTimeDataType valueType
        {
            get
            {
                return _type;
            }
        }
    }
}
