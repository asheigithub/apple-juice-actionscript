using ASBinCode;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_object_ctor : NativeFunctionBase
    {
        public system_object_ctor()
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
                return "_system_Object_ctor";
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

            ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value = new object(); 

            return ASBinCode.rtData.rtUndefined.undefined;

        }
    }

    class object_static_equals : NativeFunctionBase
    {
       
        public object_static_equals()
        {
            
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_Object_static_equals";
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
                return RunTimeDataType.rt_boolean;
            }
        }

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.normal_1;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            //base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
            success = true;

            ASBinCode.rtData.rtObject obj = argements[0].getValue() as ASBinCode.rtData.rtObject;
            if (obj == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

            LinkSystemObject o1 = obj.value as LinkSystemObject;
            if (o1 == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }


            ASBinCode.rtData.rtObject obj2 = argements[1].getValue() as ASBinCode.rtData.rtObject;
            if (obj2 == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

            LinkSystemObject o2 = obj.value as LinkSystemObject;
            if (o2 == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

            if (System.Object.Equals(o1, o2))
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                return;
            }
            else
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

        }

    }

    class object_static_referenceEquals : NativeFunctionBase
    {

        public object_static_referenceEquals()
        {

            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_Object_static_referenceEquals";
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
                return RunTimeDataType.rt_boolean;
            }
        }

        public override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.normal_1;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new NotImplementedException();
        }

        public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            //base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
            success = true;

            ASBinCode.rtData.rtObject obj = argements[0].getValue() as ASBinCode.rtData.rtObject;
            if (obj == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

            LinkSystemObject o1 = obj.value as LinkSystemObject;
            if (o1 == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }


            ASBinCode.rtData.rtObject obj2 = argements[1].getValue() as ASBinCode.rtData.rtObject;
            if (obj2 == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

            LinkSystemObject o2 = obj.value as LinkSystemObject;
            if (o2 == null)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

            if (object.ReferenceEquals(o1, o2))
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                return;
            }
            else
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                return;
            }

        }

    }

}
