using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtti
{
	[Serializable]
    /// <summary>
    /// 类定义
    /// (或者接口定义)接口定义也继承自Class的。
    /// </summary>
    public sealed class Class :IImportable , ISWCSerializable
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
		/// 是否已编译成功
		/// </summary>
		public bool isbuildSuccess;
        /// <summary>
        /// 是否不可实例化
        /// </summary>
        public bool no_constructor;

		public readonly string md5key;

        public readonly int classid;
        public Class(int id,int blockid,CSWC swc,string md5key)
        {
            _swc = swc;
			this.md5key = md5key;
            classid = id;
            this.blockid = blockid;
            classMembers = new ClassMemberList();
            fields = new List<ClassMember>();
            implements = new Dictionary<Class, int[]>();

            implicit_to_type = RunTimeDataType.unknown;
            implicit_to_functionid = -1;
        }
        /// <summary>
        /// 类定义代码所在blockid
        /// </summary>
        public readonly int blockid;

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
        public ClassMemberList classMembers;

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

        private readonly CSWC _swc;
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


		public override int GetHashCode()
		{
			return md5key.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Class other = obj as Class;
			if (other == null)
				return false;

			return md5key == other.md5key && name==other.name && package==other.package;

		}










		public static rtti.Class LoadClass(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{

			//	/// <summary>
			//	/// 指明这是否是一个接口定义
			//	/// </summary>
			//public bool isInterface;
			bool isInterface = reader.ReadBoolean();
			///// <summary>
			///// 指明这是否是一个包级函数
			///// </summary>
			//public bool isPackageFunction;
			bool isPackageFunction = reader.ReadBoolean();
			///// <summary>
			///// 是否文档类
			///// </summary>
			//public bool isdocumentclass;
			bool isdocumentclass = reader.ReadBoolean();
			///// <summary>
			///// 是否是需要非托管类
			///// </summary>
			//public bool isUnmanaged;
			bool isUnmanaged = reader.ReadBoolean();
			///// <summary>
			///// 链接到的系统对象是否是一个结构体
			///// </summary>
			//public bool isStruct;
			bool isStruct = reader.ReadBoolean();
			///// <summary>
			///// 结构体排序序号(从1开始排起)
			///// </summary>
			//public int structIndex;
			int structIndex = reader.ReadInt32();
			///// <summary>
			///// 是否链接到系统对象
			///// </summary>
			//public bool isLink_System;
			bool isLink_System = reader.ReadBoolean();

			//public readonly string md5key;
			string md5key = reader.ReadString();
			//public readonly int classid;
			int classid = reader.ReadInt32();
			//	/// <summary>
			//	/// 类定义代码所在blockid
			//	/// </summary>
			//public readonly int blockid;
			int blockid = reader.ReadInt32();

			rtti.Class _class = new Class(classid, blockid, serizlizer.loadingSwc, md5key); serizlized.Add(key,_class);
			_class.isInterface = isInterface;
			_class.isPackageFunction = isPackageFunction;
			_class.isdocumentclass = isdocumentclass;
			_class.isUnmanaged = isUnmanaged;
			_class.isStruct = isStruct;
			_class.structIndex = structIndex;
			_class.isLink_System = isLink_System;

			///// <summary>
			///// 系统对象创建器
			///// </summary>
			//public ClassMember linkObjCreator;
			_class.linkObjCreator = serizlizer.DeserializeObject<ClassMember>(reader,ClassMember.LoadClassMember);


			///// <summary>
			///// 是否已编译成功
			///// </summary>
			//public bool isbuildSuccess;
			_class.isbuildSuccess = reader.ReadBoolean();
			///// <summary>
			///// 是否不可实例化
			///// </summary>
			//public bool no_constructor;
			_class.no_constructor = reader.ReadBoolean();

			///// <summary>
			///// 包外代码所在blockid;
			///// </summary>
			//public int outscopeblockid;
			_class.outscopeblockid = reader.ReadInt32();

			///// <summary>
			///// 类名
			///// </summary>
			//public string name;
			_class.name = reader.ReadString();
			//public string package;
			_class.package = reader.ReadString();
			//public bool dynamic;
			_class.dynamic = reader.ReadBoolean();
			//public bool final;
			_class.final = reader.ReadBoolean();


			///// <summary>
			///// 是否公开。如果不是，则只能在相同包内访问
			///// </summary>
			//public bool isPublic;
			_class.isPublic = reader.ReadBoolean();
			///// <summary>
			///// 包含此类的静态成员。
			///// </summary>
			//public Class staticClass;
			_class.staticClass = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass);
			///// <summary>
			///// 如果是静态成员类，则指向类的定义
			///// </summary>
			//public Class instanceClass;
			_class.instanceClass = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass);
			///// <summary>
			///// 是否包外类
			///// </summary>
			//public bool ispackageout;
			_class.ispackageout = reader.ReadBoolean();
			///// <summary>
			///// 如果是包外类，则显示所从属的主类
			///// </summary>
			//public Class mainClass;
			_class.mainClass = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass);
			///// <summary>
			///// 构造函数
			///// </summary>
			//public ClassMember constructor;
			_class.constructor = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember);
			///// <summary>
			///// 构造函数id
			///// </summary>
			//public int constructor_functionid;
			_class.constructor_functionid = reader.ReadInt32();
			///// <summary>
			///// 隐式类型转换到原始类型函数
			///// </summary>
			//public ClassMember implicit_to;
			_class.implicit_to = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember);
			//public RunTimeDataType implicit_to_type;
			_class.implicit_to_type = reader.ReadInt32();
			//public int implicit_to_functionid;
			_class.implicit_to_functionid = reader.ReadInt32();

			///// <summary>
			///// 隐式从原始类型转换过来
			///// </summary>
			//public ClassMember implicit_from;
			_class.implicit_from = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember);
			//public int implicit_from_functionid;
			_class.implicit_from_functionid = reader.ReadInt32();
			//public RunTimeDataType implicit_from_type;
			_class.implicit_from_type = reader.ReadInt32();

			///// <summary>
			///// 显式类型转换
			///// </summary>
			//public ClassMember explicit_from;
			_class.explicit_from = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember);
			//public int explicit_from_functionid;
			_class.explicit_from_functionid = reader.ReadInt32();
			//public RunTimeDataType explicit_from_type;
			_class.explicit_from_type = reader.ReadInt32();

			///// <summary>
			///// 索引器 取值
			///// </summary>
			//public ClassMember get_this_item;
			_class.get_this_item = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember); 

			///// <summary>
			///// 索引器 赋值
			///// </summary>
			//public ClassMember set_this_item;
			_class.set_this_item = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember);

			///// <summary>
			///// 类成员定义
			///// </summary>
			//public ClassMemberList classMembers;
			int membercount = reader.ReadInt32();
			for (int i = 0; i < membercount; i++)
			{
				_class.classMembers.Add(serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember));
			}

			///// <summary>
			///// 数据字段表
			///// </summary>
			//public List<ClassMember> fields;
			int fieldscount = reader.ReadInt32();
			for (int i = 0; i < fieldscount; i++)
			{
				_class.fields.Add(serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember));
			}

			///// <summary>
			///// 父类的定义（如果有）
			///// </summary>
			//public Class super;
			_class.super = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass);

			///// <summary>
			///// 实现的各种接口
			///// 记录了接口的一个方法对应类的哪个成员实现
			///// </summary>
			//public Dictionary<Class, int[]> implements;
			int implementscount = reader.ReadInt32();
			for (int i = 0; i < implementscount; i++)
			{
				Class keyclass = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass);
				int count = reader.ReadInt32();
				int[] impls = new int[count];
				for (int j = 0; j < count; j++)
				{
					impls[j] = reader.ReadInt32();
				}
				_class.implements.Add(keyclass, impls);
			}

			return _class;
		}


		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			//	/// <summary>
			//	/// 指明这是否是一个接口定义
			//	/// </summary>
			//public bool isInterface;
			writer.Write(isInterface);
			///// <summary>
			///// 指明这是否是一个包级函数
			///// </summary>
			//public bool isPackageFunction;
			writer.Write(isPackageFunction);
			///// <summary>
			///// 是否文档类
			///// </summary>
			//public bool isdocumentclass;
			writer.Write(isdocumentclass);
			///// <summary>
			///// 是否是需要非托管类
			///// </summary>
			//public bool isUnmanaged;
			writer.Write(isUnmanaged);
			///// <summary>
			///// 链接到的系统对象是否是一个结构体
			///// </summary>
			//public bool isStruct;
			writer.Write(isStruct);
			///// <summary>
			///// 结构体排序序号(从1开始排起)
			///// </summary>
			//public int structIndex;
			writer.Write(structIndex);
			///// <summary>
			///// 是否链接到系统对象
			///// </summary>
			//public bool isLink_System;
			writer.Write(isLink_System);

			//public readonly string md5key;
			writer.Write(md5key);
			//public readonly int classid;
			writer.Write(classid);

			//	/// <summary>
			//	/// 类定义代码所在blockid
			//	/// </summary>
			//public readonly int blockid;
			writer.Write(blockid);

			///// <summary>
			///// 系统对象创建器
			///// </summary>
			//public ClassMember linkObjCreator;
			serizlizer.SerializeObject<ClassMember>(writer, linkObjCreator);
			///// <summary>
			///// 是否已编译成功
			///// </summary>
			//public bool isbuildSuccess;
			writer.Write(isbuildSuccess);
			///// <summary>
			///// 是否不可实例化
			///// </summary>
			//public bool no_constructor;
			writer.Write(no_constructor);
			
			///// <summary>
			///// 包外代码所在blockid;
			///// </summary>
			//public int outscopeblockid;
			writer.Write(outscopeblockid);

			///// <summary>
			///// 类名
			///// </summary>
			//public string name;
			writer.Write(name);
			//public string package;
			writer.Write(package);
			//public bool dynamic;
			writer.Write(dynamic);
			//public bool final;
			writer.Write(final);




			///// <summary>
			///// 是否公开。如果不是，则只能在相同包内访问
			///// </summary>
			//public bool isPublic;
			writer.Write(isPublic);
			///// <summary>
			///// 包含此类的静态成员。
			///// </summary>
			//public Class staticClass;
			serizlizer.SerializeObject(writer,staticClass);
			///// <summary>
			///// 如果是静态成员类，则指向类的定义
			///// </summary>
			//public Class instanceClass;
			serizlizer.SerializeObject(writer, instanceClass);
			///// <summary>
			///// 是否包外类
			///// </summary>
			//public bool ispackageout;
			writer.Write(ispackageout);
			///// <summary>
			///// 如果是包外类，则显示所从属的主类
			///// </summary>
			//public Class mainClass;
			serizlizer.SerializeObject(writer, mainClass);
			///// <summary>
			///// 构造函数
			///// </summary>
			//public ClassMember constructor;
			serizlizer.SerializeObject(writer, constructor);
			///// <summary>
			///// 构造函数id
			///// </summary>
			//public int constructor_functionid;
			writer.Write(constructor_functionid);
			///// <summary>
			///// 隐式类型转换到原始类型函数
			///// </summary>
			//public ClassMember implicit_to;
			serizlizer.SerializeObject(writer, implicit_to);
			//public RunTimeDataType implicit_to_type;
			writer.Write((int)implicit_to_type);
			//public int implicit_to_functionid;
			writer.Write((int)implicit_to_functionid);

			///// <summary>
			///// 隐式从原始类型转换过来
			///// </summary>
			//public ClassMember implicit_from;
			serizlizer.SerializeObject(writer, implicit_from);
			//public int implicit_from_functionid;
			writer.Write(implicit_from_functionid);
			//public RunTimeDataType implicit_from_type;
			writer.Write((int)implicit_from_type);

			///// <summary>
			///// 显式类型转换
			///// </summary>
			//public ClassMember explicit_from;
			serizlizer.SerializeObject(writer, explicit_from);
			//public int explicit_from_functionid;
			writer.Write(explicit_from_functionid);
			//public RunTimeDataType explicit_from_type;
			writer.Write((int)explicit_from_type);

			///// <summary>
			///// 索引器 取值
			///// </summary>
			//public ClassMember get_this_item;
			serizlizer.SerializeObject(writer, get_this_item);

			///// <summary>
			///// 索引器 赋值
			///// </summary>
			//public ClassMember set_this_item;
			serizlizer.SerializeObject(writer, set_this_item);

			///// <summary>
			///// 类成员定义
			///// </summary>
			//public ClassMemberList classMembers;
			writer.Write(classMembers.Count);
			for (int i = 0; i < classMembers.Count; i++)
			{
				serizlizer.SerializeObject(writer, classMembers[i]);
			}

			///// <summary>
			///// 数据字段表
			///// </summary>
			//public List<ClassMember> fields;
			writer.Write(fields.Count);
			for (int i = 0; i < fields.Count; i++)
			{
				serizlizer.SerializeObject(writer, fields[i]);
			}

			///// <summary>
			///// 父类的定义（如果有）
			///// </summary>
			//public Class super;
			serizlizer.SerializeObject(writer, super);

			///// <summary>
			///// 实现的各种接口
			///// 记录了接口的一个方法对应类的哪个成员实现
			///// </summary>
			//public Dictionary<Class, int[]> implements;

			if (!isInterface)
			{
				writer.Write(implements.Count);
				foreach (var item in implements)
				{
					serizlizer.SerializeObject(writer, item.Key);
					writer.Write(item.Value.Length);
					for (int i = 0; i < item.Value.Length; i++)
					{
						writer.Write(item.Value[i]);
					}
				}
			}
			else
			{
				writer.Write(0);
			}

		}
	}
}
