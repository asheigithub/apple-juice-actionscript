using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
    class Object_toString : NativeFunctionBase
    {
        public override bool isMethod
        {
            get
            {
                return false;
            }
        }

        public override string name
        {
            get
            {
                return "_Object_toString";
            }
        }

        private List<RunTimeDataType> para = new List<RunTimeDataType>();
        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_string;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements,object stackframe,  out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            return  new ASBinCode.rtData.rtString( TypeConverter.ConvertToString(thisObj, null, null));


        }
    }


    class Object_valueOf :NativeConstParameterFunction
    {
        public override bool isMethod
        {
            get
            {
                return false;
            }
        }

        public override string name
        {
            get
            {
                return "_Object_valueOf";
            }
        }

        private List<RunTimeDataType> para = new List<RunTimeDataType>();

        public Object_valueOf() : base(0)
        {
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj,
            FunctionDefine functionDefine, 
            SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            success = true;
            returnSlot.directSet(thisObj);
        }

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null;
        //    errorno = 0;

        //    return new ASBinCode.rtData.rtString(TypeConverter.ConvertToString(thisObj, null, null));


        //}
    }



    class Object_hasOwnProperty : NativeFunctionBase
    {
        public override bool isMethod
        {
            get
            {
                return false;
            }
        }

        public override string name
        {
            get
            {
                return "_Object_hasOwnProperty";
            }
        }

        private List<RunTimeDataType> para = new List<RunTimeDataType>();

        public Object_hasOwnProperty()
        {
            para.Add(RunTimeDataType.rt_string);
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_boolean;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            string name = TypeConverter.ConvertToString( argements[0].getValue(),null,null);
            if (String.IsNullOrEmpty(name))
            {
                return ASBinCode.rtData.rtBoolean.False;
            }

            //固定实例属性 — 对象类定义的非静态变量、常量或方法；
            //继承的固定实例属性 — 对象类继承的变量、常量或方法；
            //动态属性 — 对象实例化后添加到其中的属性（在其类定义之外）。要添加动态属性，必须用 dynamic 关键字声明对象的定义类。

            //return new ASBinCode.rtData.rtString(TypeConverter.ConvertToString(thisObj, null, null));

            ASBinCode.rtData.rtObjectBase obj = (ASBinCode.rtData.rtObjectBase)thisObj;

            if (ClassMemberFinder.find(obj.value._class, name, null) !=null)
            {
                return ASBinCode.rtData.rtBoolean.True;
            }

            if (obj.value is ASBinCode.rtti.DynamicObject)
            {
                ASBinCode.rtti.DynamicObject d = (ASBinCode.rtti.DynamicObject)obj.value;

                if (d.hasproperty(name))
                {
                    return ASBinCode.rtData.rtBoolean.True;
                }
                else
                {
                    return ASBinCode.rtData.rtBoolean.False;
                }
            }
            return ASBinCode.rtData.rtBoolean.False;
        }
    }

    class Object_isPrototypeOf : NativeFunctionBase
    {
        public override bool isMethod
        {
            get
            {
                return false;
            }
        }

        public override string name
        {
            get
            {
                return "_Object_isPrototypeOf";
            }
        }

        private List<RunTimeDataType> para = new List<RunTimeDataType>();

        public Object_isPrototypeOf()
        {
            para.Add(RunTimeDataType.rt_void);
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_boolean;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var theObj = argements[0].getValue();

            if (theObj.rtType < RunTimeDataType.unknown)
            {
                return ASBinCode.rtData.rtBoolean.False;
            }

            //***检查原型链***
            ASBinCode.rtti.Object obj = ((ASBinCode.rtData.rtObjectBase)theObj).value;

            ASBinCode.rtti.DynamicObject _proto = null;

            if (obj is ASBinCode.rtti.DynamicObject)
            {
                ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj;
                _proto = dobj._prototype_;
            }
            else
            {
                _proto = (ASBinCode.rtti.DynamicObject)
                    ((StackFrame)stackframe).player.static_instance[obj._class.staticClass.classid].value;
            }

            var v2obj = ((ASBinCode.rtData.rtObjectBase)thisObj).value;

            bool found = false;
            while (_proto != null)
            {
                if (ReferenceEquals(_proto, v2obj))
                {
                    found = true;
                    break;
                }
                
                if (_proto._class.classid == ((StackFrame)stackframe).player.swc.FunctionClass.classid) //Function 
                {
                    var o =
                        (ASBinCode.rtti.DynamicObject)
                        ((ASBinCode.rtData.rtObjectBase)_proto.memberData[1].getValue()).value;

                    _proto = o;

                }
                else if (_proto._class.classid == 1)
                {
                    _proto = null;
                }
                else
                {
                    break;
                }
            }

            if (found)
            {
                return (ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                return (ASBinCode.rtData.rtBoolean.False);
            }
                    

        }
    }


    class Object_propertyIsEnumerable : NativeFunctionBase
    {
        public override bool isMethod
        {
            get
            {
                return false;
            }
        }

        public override string name
        {
            get
            {
                return "_Object_propertyIsEnumerable";
            }
        }

        private List<RunTimeDataType> para = new List<RunTimeDataType>();

        public Object_propertyIsEnumerable()
        {
            para.Add(RunTimeDataType.rt_string);
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_boolean;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            string name = TypeConverter.ConvertToString( argements[0].getValue(),null,null);

            if (string.IsNullOrEmpty(name))
            {
                return ASBinCode.rtData.rtBoolean.False;
            }

            ASBinCode.rtData.rtObjectBase obj = (ASBinCode.rtData.rtObjectBase)thisObj;
            if (!(obj.value is ASBinCode.rtti.DynamicObject))
            {
                return (ASBinCode.rtData.rtBoolean.False);
            }

            ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;

            if (dobj.hasproperty(name))
            {
                if (dobj.propertyIsEnumerable(name))
                {
                    return (ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    return (ASBinCode.rtData.rtBoolean.False);
                }
            }
            else
            {
                return (ASBinCode.rtData.rtBoolean.False);
            }


        }
    }


    class Object_setPropertyIsEnumerable : NativeFunctionBase
    {
        public override bool isMethod
        {
            get
            {
                return false;
            }
        }

        public override string name
        {
            get
            {
                return "_Object_setPropertyIsEnumerable";
            }
        }

        private List<RunTimeDataType> para = new List<RunTimeDataType>();

        public Object_setPropertyIsEnumerable()
        {
            para.Add(RunTimeDataType.rt_string);
            para.Add(RunTimeDataType.rt_boolean);
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            string name = TypeConverter.ConvertToString(argements[0].getValue(), null, null);
            bool isEnum = TypeConverter.ConvertToBoolean(argements[1].getValue(), null, null).value;

            if (string.IsNullOrEmpty(name))
            {
                return ASBinCode.rtData.rtUndefined.undefined;
            }

            ASBinCode.rtData.rtObjectBase obj = (ASBinCode.rtData.rtObjectBase)thisObj;
            if (!(obj.value is ASBinCode.rtti.DynamicObject))
            {
                return ASBinCode.rtData.rtUndefined.undefined;
            }

            ASBinCode.rtti.DynamicObject dobj = (ASBinCode.rtti.DynamicObject)obj.value;

            if (dobj is ASBinCode.rtti.DictionaryObject)
            {
                return ASBinCode.rtData.rtUndefined.undefined;
            }

            if (dobj.hasproperty(name))
            {
                dobj.setPropertyIsEnumerable(name,isEnum);
                
                
            }
            return ASBinCode.rtData.rtUndefined.undefined;


        }
    }



}
