using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace ASRuntime
{
	public class RuntimeLinkTypeMapper
	{
		Type arrayType;
		Dictionary<Type, RunTimeDataType> link_type;
		Dictionary<RunTimeDataType, Type> type_link;

		RunTimeDataType _objectType_;

		RunTimeDataType _OBJECT_LINK = -999;
		RunTimeDataType _DICT_KEY = -998;
		RunTimeDataType _FLOAT = -997;
		RunTimeDataType _SHORT = -996;
		RunTimeDataType _USHORT = -995;

		private CSWC swc;
		public void init(CSWC swc)
		{
			this.swc = swc;
			arrayType = typeof(Array);

			link_type = new Dictionary<Type, RunTimeDataType>();



			link_type.Add(typeof(int), RunTimeDataType.rt_int);
			link_type.Add(typeof(string), RunTimeDataType.rt_string);
			link_type.Add(typeof(double), RunTimeDataType.rt_number);
			link_type.Add(typeof(bool), RunTimeDataType.rt_boolean);
			link_type.Add(typeof(uint), RunTimeDataType.rt_uint);
			link_type.Add(typeof(ASBinCode.rtData.rtArray), RunTimeDataType.rt_array);
			link_type.Add(typeof(ASBinCode.rtData.rtFunction), RunTimeDataType.rt_function);
			link_type.Add(typeof(DictionaryKey), _DICT_KEY);
			link_type.Add(typeof(RunTimeValueBase), _OBJECT_LINK);
			link_type.Add(typeof(float), _FLOAT);
			link_type.Add(typeof(short), _SHORT);
			link_type.Add(typeof(ushort), _USHORT);

			foreach (var item in swc.creator_Class)
			{
				link_type.Add(item.Key.getLinkSystemObjType(), item.Value.getRtType());
				link_type.Add(new AS3Class_Type(item.Value.staticClass.getRtType(), item.Key.getLinkSystemObjType()), item.Value.staticClass.getRtType());

				if (item.Key.getLinkSystemObjType().Equals(typeof(object)))
				{
					_objectType_ = item.Value.getRtType();
				}
			}

			type_link = new Dictionary<RunTimeDataType, Type>();
			type_link.Add(RunTimeDataType.rt_int, typeof(int));
			type_link.Add(RunTimeDataType.rt_string, typeof(string));
			type_link.Add(RunTimeDataType.rt_number, typeof(double));
			type_link.Add(RunTimeDataType.rt_boolean, typeof(bool));
			type_link.Add(RunTimeDataType.rt_uint, typeof(uint));
			type_link.Add(RunTimeDataType.rt_array, typeof(ASBinCode.rtData.rtArray));
			type_link.Add(RunTimeDataType.rt_function, typeof(ASBinCode.rtData.rtFunction));
			type_link.Add(_DICT_KEY, typeof(DictionaryKey));
			type_link.Add(_OBJECT_LINK, typeof(RunTimeValueBase));
			type_link.Add(_FLOAT, typeof(float));
			type_link.Add(_SHORT, typeof(short));
			type_link.Add(_USHORT, typeof(ushort));

			foreach (var item in swc.creator_Class)
			{
				type_link.Add(item.Value.getRtType(), item.Key.getLinkSystemObjType());

				type_link.Add(item.Value.staticClass.getRtType(), new AS3Class_Type(item.Value.staticClass.getRtType(), item.Key.getLinkSystemObjType()));

			}

			if (swc.TypeClass == null)
			{
				return;
			}

			{
				var clsint = swc.getClassDefinitionByName("int").staticClass.getRtType();
				link_type.Add(new AS3Class_Type(clsint, typeof(int)), clsint);
				type_link.Add(clsint, new AS3Class_Type(clsint, typeof(int)));
			}
			{
				var clsuint = swc.getClassDefinitionByName("uint").staticClass.getRtType();
				link_type.Add(new AS3Class_Type(clsuint, typeof(uint)), clsuint);
				type_link.Add(clsuint, new AS3Class_Type(clsuint, typeof(uint)));
			}
			{
				var clsstring = swc.getClassDefinitionByName("String").staticClass.getRtType();
				link_type.Add(new AS3Class_Type(clsstring, typeof(string)), clsstring);
				type_link.Add(clsstring, new AS3Class_Type(clsstring, typeof(string)));
			}
			{
				var clsboolean = swc.getClassDefinitionByName("Boolean").staticClass.getRtType();
				link_type.Add(new AS3Class_Type(clsboolean, typeof(bool)), clsboolean);
				type_link.Add(clsboolean, new AS3Class_Type(clsboolean, typeof(bool)));
			}
			{
				var clsnumber = swc.getClassDefinitionByName("Number").staticClass.getRtType();
				link_type.Add(new AS3Class_Type(clsnumber, typeof(double)), clsnumber);
				type_link.Add(clsnumber, new AS3Class_Type(clsnumber, typeof(double)));
			}
		}

		public Type getLinkType(RunTimeDataType rtType)
		{
			if (rtType == RunTimeDataType._OBJECT)
			{
				return type_link[_OBJECT_LINK];
			}
			else if (rtType == RunTimeDataType.rt_void) //undefined
			{
				return type_link[_OBJECT_LINK];
			}
			else if (rtType == RunTimeDataType.rt_null)
			{
				return type_link[_OBJECT_LINK];
			}
			else
			{
				Type outtype;
				if (type_link.TryGetValue(rtType, out outtype))
				{
					return outtype;
				}
				else
				{
					var cls = swc.getClassByRunTimeDataType(rtType);
					if (cls.staticClass == null && cls.instanceClass.isCrossExtend)
					{
						var scls = cls.instanceClass.super;
						while (!scls.isLink_System)
						{
							scls = scls.super;
						}

						return getLinkType(scls.getRtType());
					}
					else if (cls.staticClass != null && cls.isCrossExtend)
					{
						var scls = cls.super;
						while (!scls.isLink_System)
						{
							scls = scls.super;
						}

						return getLinkType(scls.getRtType());
					}
					else
					{
						throw new TypeLinkClassException(cls + " 不是一个链接到系统对象的类型");
					}
				}

			}
		}

		public RunTimeDataType getRuntimeDataType(Type linkType)
		{
			if (linkType.IsArray)
			{
				return link_type[arrayType];
			}

			RunTimeDataType ot;
			if (link_type.TryGetValue(linkType, out ot))
			{
				return ot;
			}
			else
			{
				if (linkType.IsSubclassOf(type_link[_OBJECT_LINK]))
				{
					return _OBJECT_LINK;
				}
				else
				{
					throw new KeyNotFoundException();
				}
			}
		}


		public void storeLinkObject_ToSlot(object obj, RunTimeDataType defineReturnType, SLOT returnSlot, IClassFinder bin, Player player)
		{
			if (obj is ICrossExtendAdapter)
			{
				obj = ((ICrossExtendAdapter)obj).AS3Object;
			}

			if (obj == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtNull.nullptr);
			}
			else
			{
				RunTimeDataType rt = defineReturnType; //getLinkType(funcDefine.signature.returnType);
													   //RunTimeDataType rt =
													   //    getRuntimeDataType(obj.GetType());
				if (rt == RunTimeDataType.rt_void)
				{
					rt = getRuntimeDataType(obj.GetType());

					if (rt == _FLOAT)
						rt = RunTimeDataType.rt_number;
					else if (rt == _SHORT)
						rt = RunTimeDataType.rt_int;
					else if (rt == _USHORT)
						rt = RunTimeDataType.rt_uint;
				}

				if (rt == _DICT_KEY)
				{
					DictionaryKey key = (DictionaryKey)obj;
					rt = _OBJECT_LINK;
					obj = key.key;

				}

				if (rt == RunTimeDataType._OBJECT)
				{
					rt = _OBJECT_LINK;
				}
				if (rt == _OBJECT_LINK)
				{
					rt = ((RunTimeValueBase)obj).rtType;
					if (rt < RunTimeDataType.unknown)//否则走下面的Object路径
					{
						returnSlot.directSet((RunTimeValueBase)obj);
						return;
					}
				}

				if (rt == RunTimeDataType.rt_int)
				{
					var realObjType = getRuntimeDataType(obj.GetType());
					if (realObjType == RunTimeDataType.rt_uint)
					{
						returnSlot.setValue((int)(uint)obj);
					}
					else if (realObjType == RunTimeDataType.rt_number)
					{
						returnSlot.setValue((int)(double)obj);
					}
					else if (realObjType == _FLOAT)
					{
						returnSlot.setValue((int)(float)obj);
					}
					else if (realObjType == _SHORT)
					{
						returnSlot.setValue((int)(short)obj);
					}
					else if (realObjType == _USHORT)
					{
						returnSlot.setValue((int)(ushort)obj);
					}
					else
					{
						returnSlot.setValue((int)obj);
					}

				}
				else if (rt == RunTimeDataType.rt_uint)
				{
					var realObjType = getRuntimeDataType(obj.GetType());
					if (realObjType == RunTimeDataType.rt_int)
					{
						returnSlot.setValue((uint)(int)obj);
					}
					else if (realObjType == RunTimeDataType.rt_number)
					{
						returnSlot.setValue((uint)(double)obj);
					}
					else if (realObjType == _FLOAT)
					{
						returnSlot.setValue((uint)(float)obj);
					}
					else if (realObjType == _SHORT)
					{
						returnSlot.setValue((uint)(short)obj);
					}
					else if (realObjType == _USHORT)
					{
						returnSlot.setValue((uint)(ushort)obj);
					}
					else
					{
						returnSlot.setValue((uint)obj);
					}
				}
				else if (rt == RunTimeDataType.rt_string)
				{
					var realObjType = getRuntimeDataType(obj.GetType());
					returnSlot.setValue((string)obj);
				}
				else if (rt == RunTimeDataType.rt_number)
				{
					var realObjType = getRuntimeDataType(obj.GetType());
					if (realObjType == RunTimeDataType.rt_uint)
					{
						returnSlot.setValue((double)(uint)obj);
					}
					else if (realObjType == RunTimeDataType.rt_int)
					{
						returnSlot.setValue((double)(int)obj);
					}
					else if (realObjType == _FLOAT)
					{
						returnSlot.setValue((double)(float)obj);
					}
					else if (realObjType == _SHORT)
					{
						returnSlot.setValue((double)(short)obj);
					}
					else if (realObjType == _USHORT)
					{
						returnSlot.setValue((double)(ushort)obj);
					}
					else
					{
						returnSlot.setValue((double)obj);
					}
				}
				else if (rt == RunTimeDataType.rt_boolean)
				{
					if ((bool)obj)
					{
						returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
					}
					else
					{
						returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
					}

				}
				else if (rt == RunTimeDataType.rt_array)
				{
					returnSlot.directSet((ASBinCode.rtData.rtArray)obj);
				}
				else if (rt == RunTimeDataType.rt_function)
				{
					returnSlot.directSet((ASBinCode.rtData.rtFunction)obj);
				}
				else if (rt > RunTimeDataType.unknown)
				{
					Class rtCls;// = ((ASBinCode.rtData.rtObject)obj).value._class; //bin.getClassByRunTimeDataType(rt);
					ASBinCode.rtData.rtObjectBase testObj = obj as ASBinCode.rtData.rtObjectBase;
					if (testObj != null)
					{
						rtCls = ((ASBinCode.rtData.rtObjectBase)obj).value._class;

						if (rtCls.isLink_System)
						{
							var f = ((CSWC)bin).class_Creator[rtCls];
							f.setLinkObjectValueToSlot(returnSlot, player,
								((LinkSystemObject)testObj.value).GetLinkData(), rtCls);
						}
						else
						{
							returnSlot.directSet((ASBinCode.rtData.rtObjectBase)obj);
						}

					}
					else
					{
						rtCls = bin.getClassByRunTimeDataType(rt);

						if (rtCls.classid == player.swc.TypeClass.classid)
						{
							Type type = obj as Type;
							if (type != null)
							{
								try
								{
									var rttype = getRuntimeDataType(type);
									if (rttype < RunTimeDataType.unknown
										&&
										player.swc.primitive_to_class_table[rttype] != null
										)
									{
										rttype = player.swc.primitive_to_class_table[rttype].getRtType();
									}

									var rtcls = bin.getClassByRunTimeDataType(rttype);

									if (player.init_static_class(rtcls, SourceToken.Empty))
									{
										returnSlot.directSet(
										 player.static_instance[rtcls.staticClass.classid]);
									}
									else
									{
										throw new ASRunTimeException("转换Class失败", String.Empty);
									}
								}
								catch (KeyNotFoundException)
								{
									throw new TypeLinkClassException(type.FullName + " 没有链接到脚本");
								}


							}
							else
							{
								throw new ASRunTimeException("没有将Class链接到Type", String.Empty);
							}
						}
						else
						{
							var f = ((CSWC)bin).class_Creator[rtCls];
							f.setLinkObjectValueToSlot(returnSlot, player, obj, rtCls);
						}

					}


				}
				else
				{
					throw new ASRunTimeException("意外的链接类型", String.Empty);
				}
			}
		}



		public bool rtValueToLinkObject
			(RunTimeValueBase value, Type linkType, IClassFinder bin, bool needclone, out object linkobject)
		{
			RunTimeDataType vt = value.rtType;

			if (vt == RunTimeDataType.rt_null)
			{
				linkobject = null;
				return true;
			}

			if (vt > RunTimeDataType.unknown)
			{
				var cls = bin.getClassByRunTimeDataType(vt);

				RunTimeDataType ot;
				if (TypeConverter.Object_CanImplicit_ToPrimitive(
					cls, out ot
					))
				{
					vt = ot;
					value = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)value);
				}
				else if (linkType is AS3Class_Type && cls.staticClass == null)
				{
					if (cls.getRtType() == ((AS3Class_Type)linkType).rttype)
					{
						linkobject = ((AS3Class_Type)linkType).linktype;
						return true;
					}
				}
			}
			RunTimeDataType at =
				getRuntimeDataType(
					linkType);

			if (at == RunTimeDataType.rt_int)
			{
				linkobject = (TypeConverter.ConvertToInt(value));
			}
			else if (at == RunTimeDataType.rt_uint)
			{
				linkobject = (TypeConverter.ConvertToUInt(value, null, null));
			}
			else if (at == RunTimeDataType.rt_string)
			{
				linkobject = (TypeConverter.ConvertToString(value, null, null));
			}
			else if (at == RunTimeDataType.rt_number)
			{
				linkobject = (TypeConverter.ConvertToNumber(value));
			}
			else if (at == RunTimeDataType.rt_boolean)
			{
				var b = TypeConverter.ConvertToBoolean(value, null, null);
				linkobject = b.value;
			}
			else if (at == RunTimeDataType.rt_array)
			{

				linkobject = (ASBinCode.rtData.rtArray)value;
			}
			else if (at == RunTimeDataType.rt_function)
			{
				if (needclone)
				{
					linkobject = (ASBinCode.rtData.rtFunction)value;
				}
				else
				{
					linkobject = ((ASBinCode.rtData.rtFunction)value).Clone();
				}
			}
			else if (at == _OBJECT_LINK)
			{
				if (vt > RunTimeDataType.unknown)
				{
					if (needclone)
					{
						linkobject = ((ASBinCode.rtData.rtObjectBase)value).Clone();
					}
					else
					{
						linkobject = (ASBinCode.rtData.rtObjectBase)value;
					}
				}
				else
				{
					if (needclone)
					{
						linkobject = value.Clone();
					}
					else
					{
						linkobject = value;
					}
				}

			}
			else if (at > RunTimeDataType.unknown)
			{
				if (vt > RunTimeDataType.unknown)
				{
					Class c = bin.getClassByRunTimeDataType(vt);
					if (c.isLink_System)
					{
						LinkSystemObject lo = (LinkSystemObject)((ASBinCode.rtData.rtObjectBase)value).value;
						linkobject = lo.GetLinkData();

					}
					else if (c.isCrossExtend)
					{
						LinkSystemObject lo = (LinkSystemObject)((ASBinCode.rtData.rtObjectBase)value).value;
						linkobject = lo.GetLinkData();
					}
					else if (c.staticClass == null)
					{
						if (c.instanceClass.isCrossExtend)
						{
							//***isCrossExtend Class转换为 基础类型的type
							var sc = c.instanceClass.super;
							while (!sc.isLink_System)
							{
								sc = sc.super;
							}

							var nf = (ICrossExtendAdapterCreator)swc.getNativeFunction(((ClassMethodGetter)sc.crossExtendAdapterCreator.bindField).functionId);

							linkobject = nf.GetAdapterType();
						}
						else
						{
							linkobject = null;
							return false;
						}
					}
					else
					{
						linkobject = null;
						return false;
					}
				}
				else if (at == _objectType_) //托管object
				{
					if (vt == RunTimeDataType.rt_int)
					{
						linkobject = (TypeConverter.ConvertToInt(value));
					}
					else if (vt == RunTimeDataType.rt_uint)
					{
						linkobject = (TypeConverter.ConvertToUInt(value, null, null));
					}
					else if (vt == RunTimeDataType.rt_string)
					{
						linkobject = (TypeConverter.ConvertToString(value, null, null));
					}
					else if (vt == RunTimeDataType.rt_number)
					{
						linkobject = (TypeConverter.ConvertToNumber(value));
					}
					else if (vt == RunTimeDataType.rt_boolean)
					{
						var b = TypeConverter.ConvertToBoolean(value, null, null);
						linkobject = b.value;
					}
					else if (vt == RunTimeDataType.rt_void)
					{
						linkobject = null;
					}
					else
					{
						linkobject = null;
						return false;
					}
				}
				else
				{
					linkobject = null;
					return false;
				}
			}
			else
			{

				linkobject = null;
				return false;
			}

			return true;
		}








		/// <summary>
		/// 定义AS3的Class到Type的链接
		/// </summary>
		public sealed class AS3Class_Type : Type
		{
			public readonly RunTimeDataType rttype;
			public readonly Type linktype;

			public AS3Class_Type(RunTimeDataType rttype, Type linktype)
			{
				this.rttype = rttype;
				this.linktype = linktype;
			}


			public override Guid GUID { get { return linktype.GUID; } }

			public override Module Module { get { return linktype.Module; } }

			public override Assembly Assembly { get { return linktype.Assembly; } }

			public override string FullName { get { return linktype.FullName; } }

			public override string Namespace { get { return linktype.Namespace; } }

			public override string AssemblyQualifiedName { get { return linktype.AssemblyQualifiedName; } }

			public override Type BaseType { get { return null; } }

			public override Type UnderlyingSystemType { get { return linktype.UnderlyingSystemType; } }

			public override string Name { get { return linktype.Name; } }

			public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override object[] GetCustomAttributes(bool inherit)
			{
				throw new NotImplementedException();
			}

			public override object[] GetCustomAttributes(Type attributeType, bool inherit)
			{
				throw new NotImplementedException();
			}

			public override Type GetElementType()
			{
				throw new NotImplementedException();
			}

			public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override EventInfo[] GetEvents(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override FieldInfo GetField(string name, BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override FieldInfo[] GetFields(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override Type GetInterface(string name, bool ignoreCase)
			{
				throw new NotImplementedException();
			}

			public override Type[] GetInterfaces()
			{
				throw new NotImplementedException();
			}

			public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override Type GetNestedType(string name, BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override Type[] GetNestedTypes(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
			{
				throw new NotImplementedException();
			}

			public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
			{
				throw new NotImplementedException();
			}

			public override bool IsDefined(Type attributeType, bool inherit)
			{
				throw new NotImplementedException();
			}

			protected override TypeAttributes GetAttributeFlagsImpl()
			{
				throw new NotImplementedException();
			}

			protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
			{
				throw new NotImplementedException();
			}

			protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
			{
				throw new NotImplementedException();
			}

			protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
			{
				throw new NotImplementedException();
			}

			protected override bool HasElementTypeImpl()
			{
				throw new NotImplementedException();
			}

			protected override bool IsArrayImpl()
			{
				return linktype.IsArray;
				//throw new NotImplementedException();
			}

			protected override bool IsByRefImpl()
			{
				throw new NotImplementedException();
			}

			protected override bool IsCOMObjectImpl()
			{
				throw new NotImplementedException();
			}

			protected override bool IsPointerImpl()
			{
				throw new NotImplementedException();
			}

			protected override bool IsPrimitiveImpl()
			{
				throw new NotImplementedException();
			}



			public override int GetHashCode()
			{
				return rttype.GetHashCode() ^ linktype.GetHashCode();
			}

			public override bool Equals(object o)
			{
				AS3Class_Type other = o as AS3Class_Type;
				if (other == null)
					return false;

				return rttype == other.rttype && Equals(linktype, other.linktype);

			}

		}


		/// <summary>
		/// 类型与Class链接失败
		/// </summary>
		public sealed class TypeLinkClassException : ASRunTimeException
		{
			public TypeLinkClassException(string msg) : base(msg, string.Empty)
			{

			}
		}

	}
}
