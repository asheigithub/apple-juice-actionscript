using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASCTest.regNativeFunctions
{
    class EnumItem
    {
        public ASBinCode.rtData.rtInt value;
        public ASBinCode.rtData.rtString str;
    }


    class enumitem_create : ASBinCode.NativeFunctionBase
    {
        public enumitem_create()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_string);
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
                return "_enumitem_create_";
            }
        }

        private List<RunTimeDataType> para;

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
            ASRuntime.StackFrame frame = (ASRuntime.StackFrame)stackframe;

            var cls = argements[2].getValue();

            if (cls.rtType < RunTimeDataType.unknown)
            {
                errormessage = "没有指定枚举类型";
                errorno = 999;
                return ASBinCode.rtData.rtUndefined.undefined;
            }

            var _class = frame.scope.swc.getClassByRunTimeDataType(cls.rtType);
            if (!(_class.staticClass == null))
            {
                errormessage = "第二个参数不是一个Class";
                errorno = 999;
                return ASBinCode.rtData.rtUndefined.undefined;
            }

            var result = frame.player.alloc_pureHostedOrLinkedObject(_class.instanceClass);
            if ( result !=null)
            {
                errormessage = null;
                errorno = 0;

                EnumItem item = new EnumItem();
                item.value = new ASBinCode.rtData.rtInt(ASRuntime.TypeConverter.ConvertToInt(
                        argements[0].getValue(), null, null
                        ));
                item.str = (ASBinCode.rtData.rtString)argements[1].getValue().Clone();

                ((ASBinCode.rtti.IHostedObject)result.value).hostedObject = item;

                return result;
            }
            else
            {
                errormessage = "对象创建失败";
                errorno = 999;
                return ASBinCode.rtData.rtUndefined.undefined;
            }
            //throw new NotImplementedException();
        }
    }


    class enumitem_tostring : ASBinCode.NativeFunctionBase
    {
        public enumitem_tostring()
        {
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
                return "_enumitem_tostring_";
            }
        }

        private List<RunTimeDataType> para;

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
            errormessage = null;
            errorno = 0;

            return ((EnumItem)((ASBinCode.rtti.IHostedObject)((ASBinCode.rtData.rtObject)thisObj).value).hostedObject).str;
        }
    }

    class enumitem_valueof : ASBinCode.NativeFunctionBase
    {
        public enumitem_valueof()
        {
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
                return "_enumitem_valueof_";
            }
        }

        private List<RunTimeDataType> para;

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
                return RunTimeDataType.rt_int;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            return ((EnumItem)((ASBinCode.rtti.IHostedObject)((ASBinCode.rtData.rtObject)thisObj).value).hostedObject).value;
        }
    }


}
