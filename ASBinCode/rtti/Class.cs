using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtti
{
	[Serializable]
    /// <summary>
    /// 类定义
    /// (或者接口定义)接口定义也继承自Class的。
    /// </summary>
    public class Class :IImportable
    {
        /// <summary>
        /// 指明这是否是一个接口定义
        /// </summary>
        public bool isInterface;

		/// <summary>
		/// 指明这是否是一个包级函数
		/// </summary>
		public bool isPackageFunction;

        /// <summary>
        /// 是否文档类
        /// </summary>
        public bool isdocumentclass;

        /// <summary>
        /// 是否是需要非托管类
        /// </summary>
        public bool isUnmanaged;

        /// <summary>
        /// 链接到的系统对象是否是一个结构体
        /// </summary>
        public bool isStruct;
        /// <summary>
        /// 结构体排序序号(从1开始排起)
        /// </summary>
        public int structIndex;

        /// <summary>
        /// 是否链接到系统对象
        /// </summary>
        public bool isLink_System;
        /// <summary>
        /// 系统对象创建器
        /// </summary>
        public ClassMember linkObjCreator;


        /// <summary>
        /// 是否不可实例化
        /// </summary>
        public bool no_constructor;

        public readonly int classid;
        public Class(int id,int blockid,CSWC swc)
        {
            _swc = swc;
            classid = id;
            this.blockid = blockid;
            classMembers = new List<ClassMember>();
            fields = new List<ClassMember>();
            implements = new Dictionary<Class, int[]>();

            implicit_to_type = RunTimeDataType.unknown;
            implicit_to_functionid = -1;
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
        /// 隐式类型转换到原始类型函数
        /// </summary>
        public ClassMember implicit_to;
        public RunTimeDataType implicit_to_type;
        public int implicit_to_functionid;

        /// <summary>
        /// 隐式从原始类型转换过来
        /// </summary>
        public ClassMember implicit_from;
        public int implicit_from_functionid;
        public RunTimeDataType implicit_from_type;

        /// <summary>
        /// 显式类型转换
        /// </summary>
        public ClassMember explicit_from;
        public int explicit_from_functionid;
        public RunTimeDataType explicit_from_type;

        /// <summary>
        /// 索引器 取值
        /// </summary>
        public ClassMember get_this_item;
        /// <summary>
        /// 索引器 赋值
        /// </summary>
        public ClassMember set_this_item;

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

        /// <summary>
        /// 实现的各种接口
        /// 记录了接口的一个方法对应类的哪个成员实现
        /// </summary>
        public Dictionary<Class, int[]> implements;

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
            if (!isInterface)
            {
                return "Class [" + package + (string.IsNullOrEmpty(package) ? "" : ".") + name + "] " + (isdocumentclass ? "doc" : "");  //base.ToString();
            }
            else
            {
                return "Interface [" + package + (string.IsNullOrEmpty(package) ? "" : ".") + name + "] ";  //base.ToString();
            }
        }

    }
}
