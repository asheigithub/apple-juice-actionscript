using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtti
{
	
	/// <summary>
	/// 类成员
	/// </summary>
	public sealed class ClassMember :ISWCSerializable
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
        public RunTimeValueBase defaultValue;

        /// <summary>
        /// 所属Class
        /// </summary>
        public Class refClass;

        public IMember bindField;

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

		private RunTimeDataType _type;

		//序列化时用
		private ClassMember() { _type = RunTimeDataType.rt_void; }

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












		public static ClassMember LoadClassMember(BinaryReader reader,CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			///// <summary>
			///// 成员名
			///// </summary>
			//public string name;
			string name = reader.ReadString();

			ClassMember classMember = new ClassMember(); serizlized.Add(key, classMember);
			classMember.name = name;
			Class refClass = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass
				);
			//public readonly IMember bindField;
			IMember bindField = (IMember)serizlizer.DeserializeObject<ISWCSerializable>(reader, ISWCSerializableLoader.LoadIMember
				);


			
			classMember.refClass = refClass;
			classMember.bindField = bindField;


			//public bool isPublic;
			classMember.isPublic = reader.ReadBoolean();
			//public bool isInternal;
			classMember.isInternal = reader.ReadBoolean();
			//public bool isPrivate;
			classMember.isPrivate = reader.ReadBoolean();
			//public bool isProtectd;
			classMember.isProtectd = reader.ReadBoolean();

			///// <summary>
			///// 是否属性读取器
			///// </summary>
			//public bool isGetter;
			classMember.isGetter = reader.ReadBoolean();
			///// <summary>
			///// 是否属性设置器
			///// </summary>
			//public bool isSetter;
			classMember.isSetter = reader.ReadBoolean();

			///// <summary>
			///// 是否覆盖基类方法
			///// </summary>
			//public bool isOverride;
			classMember.isOverride = reader.ReadBoolean();

			//public bool isFinal;
			classMember.isFinal = reader.ReadBoolean();

			///// <summary>
			///// 是否是静态成员
			///// </summary>
			//public bool isStatic;
			classMember.isStatic = reader.ReadBoolean();
			///// <summary>
			///// 是否是常量
			///// </summary>
			//public bool isConst;
			classMember.isConst = reader.ReadBoolean();
			///// <summary>
			///// 是否是构造函数
			///// </summary>
			//public bool isConstructor;
			classMember.isConstructor = reader.ReadBoolean();

			///// <summary>
			///// 成员字面值,比如1000,"aabc"等确定的字面值
			///// </summary>
			//public RunTimeValueBase defaultValue;
			classMember.defaultValue = serizlizer.DeserializeObject<RunTimeValueBase>(reader,RunTimeValueBase.LoadRunTimeValueBase);

			///// <summary>
			///// 从哪个类继承而来
			///// </summary>
			//public Class inheritFrom;
			classMember.inheritFrom = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass);

			///// <summary>
			///// 继承源
			///// </summary>
			//public ClassMember inheritSrcMember;
			classMember.inheritSrcMember = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember);

			//public ClassMember virtualLink;
			classMember.virtualLink = serizlizer.DeserializeObject<ClassMember>(reader, ClassMember.LoadClassMember);
			//public Class virtualLinkFromClass;
			classMember.virtualLinkFromClass = serizlizer.DeserializeObject<Class>(reader, Class.LoadClass);

			//private RunTimeDataType _type;
			classMember._type = reader.ReadInt32();


			return classMember;
		}

		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			///// <summary>
			///// 成员名
			///// </summary>
			//public string name;
			writer.Write(name);
			
			serizlizer.SerializeObject(writer, refClass);

			//public readonly IMember bindField;
			serizlizer.SerializeObject(writer, bindField);

			
			//public bool isPublic;
			writer.Write(isPublic);
			//public bool isInternal;
			writer.Write(isInternal);
			//public bool isPrivate;
			writer.Write(isPrivate);
			//public bool isProtectd;
			writer.Write(isProtectd);

			///// <summary>
			///// 是否属性读取器
			///// </summary>
			//public bool isGetter;
			writer.Write(isGetter);
			///// <summary>
			///// 是否属性设置器
			///// </summary>
			//public bool isSetter;
			writer.Write(isSetter);

			///// <summary>
			///// 是否覆盖基类方法
			///// </summary>
			//public bool isOverride;
			writer.Write(isOverride);

			//public bool isFinal;
			writer.Write(isFinal);

			///// <summary>
			///// 是否是静态成员
			///// </summary>
			//public bool isStatic;
			writer.Write(isStatic);
			///// <summary>
			///// 是否是常量
			///// </summary>
			//public bool isConst;
			writer.Write(isConst);
			///// <summary>
			///// 是否是构造函数
			///// </summary>
			//public bool isConstructor;
			writer.Write(isConstructor);
			
			///// <summary>
			///// 成员字面值,比如1000,"aabc"等确定的字面值
			///// </summary>
			//public RunTimeValueBase defaultValue;
			serizlizer.SerializeObject(writer, defaultValue);

			///// <summary>
			///// 从哪个类继承而来
			///// </summary>
			//public Class inheritFrom;
			serizlizer.SerializeObject(writer, inheritFrom);

			///// <summary>
			///// 继承源
			///// </summary>
			//public ClassMember inheritSrcMember;
			serizlizer.SerializeObject(writer, inheritSrcMember);

			//public ClassMember virtualLink;
			serizlizer.SerializeObject(writer, virtualLink);
			//public Class virtualLinkFromClass;
			serizlizer.SerializeObject(writer, virtualLinkFromClass);

			//private RunTimeDataType _type;
			writer.Write((int)_type);


		}


	}
}
