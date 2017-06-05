using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_datetime_ctor : NativeFunctionBase
    {
        public system_datetime_ctor()
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
                return "_system_DateTime_ctor";
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
            
            ((LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value).value =
                 new DateTime();

            return ASBinCode.rtData.rtUndefined.undefined;

        }


    }

    class system_datetime_static_constructor_ : NativeFunctionBase
    {
        public system_datetime_static_constructor_()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_DateTime_static_constructor_";
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
            int year = ASRuntime.TypeConverter.ConvertToInt(argements[0].getValue(), null, null);
            int month = ASRuntime.TypeConverter.ConvertToInt(argements[1].getValue(), null, null);
            int day = ASRuntime.TypeConverter.ConvertToInt(argements[2].getValue(), null, null);
            
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;
            try
            {
                ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, new DateTime(year, month, day));
                success = true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                frame.throwAneException(token, a.Message);
                success = false;
            }
            
            

            
        }
    }


}
