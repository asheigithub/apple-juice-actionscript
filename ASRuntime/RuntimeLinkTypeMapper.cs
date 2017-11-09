using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    class RuntimeLinkTypeMapper : LinkTypeMapper
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

        public sealed override void init(CSWC swc)
        {
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
            }
        }

        public sealed override Type getLinkType(RunTimeDataType rtType)
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
                return type_link[rtType];
            }
        }

        public sealed override RunTimeDataType getRuntimeDataType(Type linkType)
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


        public sealed override void storeLinkObject_ToSlot(object obj,RunTimeDataType defineReturnType ,SLOT returnSlot, IClassFinder bin, object player)
        {
            if (obj == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtNull.nullptr);
            }
            else
            {
                RunTimeDataType rt =  defineReturnType; //getLinkType(funcDefine.signature.returnType);
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
                    ASBinCode.rtData.rtObject testObj = obj as ASBinCode.rtData.rtObject;
                    if (testObj != null)
                    {
                        rtCls = ((ASBinCode.rtData.rtObject)obj).value._class;

                        if (rtCls.isLink_System)
                        {
                            var f = ((CSWC)bin).class_Creator[rtCls];
                            f.setLinkObjectValueToSlot(returnSlot, player,
                                ((LinkSystemObject)testObj.value).GetLinkData(), rtCls);
                        }
                        else
                        {
                            returnSlot.directSet((ASBinCode.rtData.rtObject)obj);
                        }

                    }
                    else
                    {
                        rtCls = bin.getClassByRunTimeDataType(rt);
                        var f = ((CSWC)bin).class_Creator[rtCls];
                        f.setLinkObjectValueToSlot(returnSlot, player, obj, rtCls);
                    }


                }
                else
                {
                    throw new ASRunTimeException("意外的链接类型", String.Empty);
                }
            }
        }



        public sealed override bool rtValueToLinkObject
            (RunTimeValueBase value, Type linkType,IClassFinder bin,bool needclone, out object linkobject)
        {
            RunTimeDataType vt = value.rtType;

            if (vt == RunTimeDataType.rt_null)
            {
                linkobject = null;
                return true;
            }

            if (vt > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(
                    bin.getClassByRunTimeDataType(vt), out ot
                    ))
                {
                    vt = ot;
                    value = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)value);
                }
            }
            RunTimeDataType at =
                getRuntimeDataType(
                    linkType);

            if (at == RunTimeDataType.rt_int)
            {
                linkobject = (TypeConverter.ConvertToInt(value, null, null));
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
                    linkobject = ((ASBinCode.rtData.rtObject)value).Clone();
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
                        LinkSystemObject lo = (LinkSystemObject)((ASBinCode.rtData.rtObject)value).value;
                        linkobject = lo.GetLinkData();

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
                        linkobject = (TypeConverter.ConvertToInt(value, null, null));
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
    }
}
