using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    public static class LinkSystem_Buildin
    {
        public static NativeFunctionBase getCreator<T>(string name, T v)
        {
            return new creator<T>(name, v);
        }
        public static NativeFunctionBase getToString<T>(string name)
        {
            return new toString<T>(name);
        }

        public static NativeFunctionBase getFieldLinkObjGetter<T>(string funcname,string fieldkey,T init_data)
        {
            return new static_field_linkobj_getter<T>(funcname,fieldkey,init_data);
        }
    }

    #region creator

    class creator<T> : NativeFunctionBase
    {
        private T defaultvalue;
        private string funname;

        public creator(string funname, T v)
        {
            this.defaultvalue = v;
            this.funname = funname;
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return funname;
            }
        }

        List<RunTimeDataType> para;
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
                return RunTimeDataType._OBJECT;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            int classid = ((ASBinCode.rtData.rtInt)argements[0].getValue()).value;
            LinkObj<T> obj = new LinkObj<T>((Class)stackframe);

            return new ASBinCode.rtData.rtObject(obj, null);

        }
    }

    #endregion

    #region toString

    class toString<T> : NativeFunctionBase
    {
        string funcname;
        public toString(string name)
        {
            funcname = name;
            para = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return funcname;
            }
        }

        List<RunTimeDataType> para;
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null; errorno = 0;
            LinkObj<T> iv = ((LinkObj<T>)((ASBinCode.rtData.rtObject)thisObj).value);
            return new ASBinCode.rtData.rtString(iv.value.ToString());

        }
    }

    #endregion

    #region static field

    static class static_fieldValues
    {
        public static Dictionary<string, RunTimeValueBase> fieldValues = new Dictionary<string, RunTimeValueBase>();
    }

    class static_field_linkobj_getter<T> : NativeFunctionBase
    {
        private string functionname;
        private string fieldkey;
        private T value;
        public static_field_linkobj_getter(string functionname, string key,T init_value)
        {
            this.functionname = functionname;
            this.fieldkey = key;
            this.value = init_value;

            para = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return functionname;
            }
        }

        List<RunTimeDataType> para;
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

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null; errorno = 0;

            if (!static_fieldValues.fieldValues.ContainsKey(fieldkey))
            {
                static_fieldValues.fieldValues.Add(fieldkey, null);

                StackFrame frame = (ASRuntime.StackFrame)stackframe;

                var obj = frame.player.alloc_pureHostedOrLinkedObject(
                    ((ASBinCode.rtData.rtObject)thisObj).value._class.instanceClass);

                LinkObj<T> i64 = (LinkObj<T>)obj.value;
                i64.value = value;

                static_fieldValues.fieldValues[fieldkey] = obj;

            }
            return static_fieldValues.fieldValues[fieldkey];
        }
    }



    #endregion


}
