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

        public sealed override void init(CSWC swc)
        {
            arrayType = typeof(Array);

            link_type = new Dictionary<Type, RunTimeDataType>();
            link_type.Add(typeof(int), RunTimeDataType.rt_int);
            link_type.Add(typeof(string), RunTimeDataType.rt_string);
            link_type.Add(typeof(double), RunTimeDataType.rt_number);
            link_type.Add(typeof(bool), RunTimeDataType.rt_boolean);
            link_type.Add(typeof(uint), RunTimeDataType.rt_uint);

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

            foreach (var item in swc.creator_Class)
            {
                type_link.Add(item.Value.getRtType(), item.Key.getLinkSystemObjType());
            }
        }

        public sealed override Type getLinkType(RunTimeDataType rtType)
        {
            return type_link[rtType];
        }

        public sealed override RunTimeDataType getRuntimeDataType(Type linkType)
        {
            if (linkType.IsArray)
            {
                return link_type[arrayType];
            }
            
            return link_type[linkType];
        }


        public sealed override void storeLinkObject_ToSlot(object obj,FunctionDefine funcDefine ,SLOT returnSlot, IClassFinder bin, object player)
        {
            if (obj == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtNull.nullptr);
            }
            else
            {
                RunTimeDataType rt = funcDefine.signature.returnType; //getLinkType(funcDefine.signature.returnType);
                //RunTimeDataType rt =
                //    getRuntimeDataType(obj.GetType());
                if (rt == RunTimeDataType.rt_void)
                {
                    rt = getRuntimeDataType(obj.GetType());
                }

                if (rt == RunTimeDataType.rt_int)
                {
                    returnSlot.setValue((int)obj);
                }
                else if (rt == RunTimeDataType.rt_uint)
                {
                    returnSlot.setValue((uint)obj);
                }
                else if (rt == RunTimeDataType.rt_string)
                {
                    returnSlot.setValue((string)obj);
                }
                else if (rt == RunTimeDataType.rt_number)
                {
                    returnSlot.setValue((double)obj);
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
                else if (rt > RunTimeDataType.unknown)
                {
                    Class rtCls = bin.getClassByRunTimeDataType(rt);

                    //var funCreator = (ClassMethodGetter)rtCls.staticClass.linkObjCreator.bindField;
                    //var f = (ILinkSystemObjCreator)((CSWC)bin).getNativeFunction(funCreator.functionId);

                    var f = ((CSWC)bin).class_Creator[rtCls];

                    f.setLinkObjectValueToSlot(returnSlot, player,  obj, rtCls);

                }
                else
                {
                    throw new ASRunTimeException("意外的链接类型");
                }
            }
        }



        public sealed override bool rtValueToLinkObject
            (RunTimeValueBase value, Type linkType,IClassFinder bin, out object linkobject)
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
                linkobject=(TypeConverter.ConvertToInt(value, null, null));
            }
            else if (at == RunTimeDataType.rt_uint)
            {
                linkobject=(TypeConverter.ConvertToUInt(value, null, null));
            }
            else if (at == RunTimeDataType.rt_string)
            {
                linkobject=(TypeConverter.ConvertToString(value, null, null));
            }
            else if (at == RunTimeDataType.rt_number)
            {
                linkobject=(TypeConverter.ConvertToNumber(value));
            }
            else if (at == RunTimeDataType.rt_boolean)
            {
                var b = TypeConverter.ConvertToBoolean(value,null,null);
                linkobject = b.value;
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
                    else if (at == RunTimeDataType.rt_number)
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
