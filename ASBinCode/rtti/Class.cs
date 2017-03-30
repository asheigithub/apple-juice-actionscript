using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
    /// <summary>
    /// 类定义
    /// </summary>
    public class Class :IImportable
    {
        /// <summary>
        /// 是否文档类
        /// </summary>
        public bool isdocumentclass;

        public readonly int classid;
        public Class(int id,int blockid,CSWC swc)
        {
            _swc = swc;
            classid = id;
            this.blockid = blockid;
            classMembers = new List<ClassMember>();
            fields = new List<ClassMember>();
        }
        /// <summary>
        /// 类定义代码所在blockid
        /// </summary>
        public int blockid;

        /// <summary>
        /// 包外代码所在blockid;
        /// </summary>
        public int outscopeblockid;


        /// <summary>
        /// 类名
        /// </summary>
        public string name;

        public string package;
        public bool dynamic;
        public bool final;

        /// <summary>
        /// 是否公开。如果不是，则只能在相同包内访问
        /// </summary>
        public bool isPublic;

        /// <summary>
        /// 包含此类的静态成员。
        /// </summary>
        public Class staticClass;
        /// <summary>
        /// 如果是静态成员类，则指向类的定义
        /// </summary>
        public Class instanceClass;

        /// <summary>
        /// 是否包外类
        /// </summary>
        public bool ispackageout;
        /// <summary>
        /// 如果是包外类，则显示所从属的主类
        /// </summary>
        public Class mainClass;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClassMember constructor;
        /// <summary>
        /// 构造函数id
        /// </summary>
        public int constructor_functionid;

        /// <summary>
        /// 类成员定义
        /// </summary>
        public List<ClassMember> classMembers;

        /// <summary>
        /// 数据字段表
        /// </summary>
        public List<ClassMember> fields;


        /// <summary>
        /// 父类的定义（如果有）
        /// </summary>
        public Class super;

        private CSWC _swc;
        public CSWC assembly
        {
            get
            {
                return _swc;
            }
        }

        public RunTimeDataType getRtType()
        {
            return classid + RunTimeDataType._OBJECT;
        }

        public override string ToString()
        {
            return "Class [" + package + (string.IsNullOrEmpty(package)?"":".") + name + "] " + (isdocumentclass?"doc":"") ;  //base.ToString();
        }

    }
}
