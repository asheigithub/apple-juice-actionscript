using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_timespan_ctor : NativeFunctionBase
    {
        public system_timespan_ctor()
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
                return "_system_TimeSpan_ctor";
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
            int hours = ASRuntime.TypeConverter.ConvertToInt(argements[0].getValue(),null,null);
            int minutes = ASRuntime.TypeConverter.ConvertToInt(argements[1].getValue(), null, null);
            int seconds = ASRuntime.TypeConverter.ConvertToInt(argements[2].getValue(), null, null);

            ((LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value).value =
                 new TimeSpan(hours, minutes, seconds);

            return ASBinCode.rtData.rtUndefined.undefined;

        }
    }


    class system_timespan_add : NativeFunctionBase
    {
        public system_timespan_add()
        {
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
                return "_system_TimeSpan_add";
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

        public override void execute2(RunTimeValueBase thisObj, 
            FunctionDefine functionDefine, 
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;

            StackFrame frame = (StackFrame)stackframe;

            var arg = argements[0].getValue();
            if (arg.rtType == RunTimeDataType.rt_null)
            {
                frame.throwArgementException(token,"参数不能为null");
                success = false;
                return;
            }

            LinkObj<TimeSpan> argObj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)arg).value;

            var typeClass = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            ((StackSlot)returnSlot).setLinkObjectValue(typeClass,frame.player, lobj.value.Add(argObj.value));


            success = true;
        }

    }

    class system_timespan_subtract : NativeFunctionBase
    {
        public system_timespan_subtract()
        {
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
                return "_system_TimeSpan_subtract";
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;

            StackFrame frame = (StackFrame)stackframe;

            var arg = argements[0].getValue();
            if (arg.rtType == RunTimeDataType.rt_null)
            {
                frame.throwArgementException(token, "参数不能为null");
                success = false;
                return;
            }

            LinkObj<TimeSpan> argObj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)arg).value;

            var typeClass = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            ((StackSlot)returnSlot).setLinkObjectValue(typeClass, frame.player, lobj.value.Subtract(argObj.value));


            success = true;
        }

    }

    class system_timespan_duration : NativeFunctionBase
    {
        public system_timespan_duration()
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
                return "_system_TimeSpan_duration";
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;

            StackFrame frame = (StackFrame)stackframe;

            var typeClass = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            ((StackSlot)returnSlot).setLinkObjectValue(typeClass, frame.player, lobj.value.Duration());


            success = true;
        }

    }

    class system_timespan_negate : NativeFunctionBase
    {
        public system_timespan_negate()
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
                return "_system_TimeSpan_negate";
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;

            StackFrame frame = (StackFrame)stackframe;

            var typeClass = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            ((StackSlot)returnSlot).setLinkObjectValue(typeClass, frame.player, lobj.value.Negate());


            success = true;
        }

    }


    class system_timespan_days : NativeFunctionBase
    {
        public system_timespan_days()
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
                return "_system_TimeSpan_days";
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
                return RunTimeDataType.rt_int;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.Days);


            success = true;
        }

    }

    class system_timespan_hours : NativeFunctionBase
    {
        public system_timespan_hours()
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
                return "_system_TimeSpan_hours";
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
                return RunTimeDataType.rt_int;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.Hours);


            success = true;
        }

    }

    class system_timespan_millseconds : NativeFunctionBase
    {
        public system_timespan_millseconds()
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
                return "_system_TimeSpan_milliseconds";
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
                return RunTimeDataType.rt_int;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.Milliseconds);


            success = true;
        }

    }

    class system_timespan_minutes : NativeFunctionBase
    {
        public system_timespan_minutes()
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
                return "_system_TimeSpan_minutes";
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
                return RunTimeDataType.rt_int;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.Minutes);


            success = true;
        }

    }

    class system_timespan_seconds : NativeFunctionBase
    {
        public system_timespan_seconds()
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
                return "_system_TimeSpan_seconds";
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
                return RunTimeDataType.rt_int;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.Seconds);


            success = true;
        }

    }


    class system_timespan_ticks : NativeFunctionBase
    {
        public system_timespan_ticks()
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
                return "_system_TimeSpan_ticks";
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, lobj.value.Ticks);


            success = true;
        }

    }

    class system_timespan_totalDays : NativeFunctionBase
    {
        public system_timespan_totalDays()
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
                return "_system_TimeSpan_totalDays";
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
                return RunTimeDataType.rt_number;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.TotalDays);


            success = true;
        }

    }

    class system_timespan_totalHours : NativeFunctionBase
    {
        public system_timespan_totalHours()
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
                return "_system_TimeSpan_totalHours";
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
                return RunTimeDataType.rt_number;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.TotalHours);


            success = true;
        }

    }

    class system_timespan_totalMilliseconds : NativeFunctionBase
    {
        public system_timespan_totalMilliseconds()
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
                return "_system_TimeSpan_totalMilliseconds";
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
                return RunTimeDataType.rt_number;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.TotalMilliseconds);


            success = true;
        }

    }

    class system_timespan_totalMinutes : NativeFunctionBase
    {
        public system_timespan_totalMinutes()
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
                return "_system_TimeSpan_totalMinutes";
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
                return RunTimeDataType.rt_number;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.TotalMinutes);


            success = true;
        }

    }

    class system_timespan_totalSeconds : NativeFunctionBase
    {
        public system_timespan_totalSeconds()
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
                return "_system_TimeSpan_totalSeconds";
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
                return RunTimeDataType.rt_number;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            LinkObj<TimeSpan> lobj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)thisObj).value;


            ((StackSlot)returnSlot).setValue(lobj.value.TotalSeconds);


            success = true;
        }

    }


    class system_timespan_static_compare : NativeFunctionBase
    {
        public system_timespan_static_compare()
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
                return "_system_TimeSpan_static_compare";
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
                return RunTimeDataType.rt_int;
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            
            StackFrame frame = (StackFrame)stackframe;

            var arg1 = argements[0].getValue();
            if (arg1.rtType == RunTimeDataType.rt_null)
            {
                frame.throwArgementException(token, "参数" + functionDefine.signature.parameters[0].name + "不能为null");
                success = false;
                return;
            }
            var arg2 = argements[1].getValue();
            if (arg2.rtType == RunTimeDataType.rt_null)
            {
                frame.throwArgementException(token, "参数" + functionDefine.signature.parameters[1].name + "不能为null");
                success = false;
                return;
            }

            LinkObj<TimeSpan> a1 = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)arg1).value;
            LinkObj<TimeSpan> a2 = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)arg2).value;

            int v=TimeSpan.Compare(a1.value, a2.value);

            returnSlot.setValue(v);

            success = true;
        }

    }



    class system_timespan_static_constructor_ : NativeFunctionBase
    {
        public system_timespan_static_constructor_()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_TimeSpan_static_constructor_";
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
            int days = ASRuntime.TypeConverter.ConvertToInt(argements[0].getValue(), null, null);
            int hours = ASRuntime.TypeConverter.ConvertToInt(argements[1].getValue(), null, null);
            int minutes = ASRuntime.TypeConverter.ConvertToInt(argements[2].getValue(), null, null);
            int seconds = ASRuntime.TypeConverter.ConvertToInt(argements[3].getValue(), null, null);

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, new TimeSpan(days, hours, minutes, seconds));

            success = true;
        }
    }

    class system_timespan_static_constructor__ : NativeFunctionBase
    {
        public system_timespan_static_constructor__()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_TimeSpan_static_constructor__";
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
            int days = ASRuntime.TypeConverter.ConvertToInt(argements[0].getValue(), null, null);
            int hours = ASRuntime.TypeConverter.ConvertToInt(argements[1].getValue(), null, null);
            int minutes = ASRuntime.TypeConverter.ConvertToInt(argements[2].getValue(), null, null);
            int seconds = ASRuntime.TypeConverter.ConvertToInt(argements[3].getValue(), null, null);
            int milliseconds = ASRuntime.TypeConverter.ConvertToInt(argements[4].getValue(), null, null);

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, new TimeSpan(days, hours, minutes, seconds,milliseconds));

            success = true;
        }
    }

    class system_timespan_static_constructor___ : NativeFunctionBase
    {
        public system_timespan_static_constructor___()
        {
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
                return "_system_TimeSpan_static_constructor___";
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
            var arg = argements[0].getValue();

            long ticks;
            if (arg.rtType == RunTimeDataType.rt_null)
            {
                ticks = default(long);
            }
            else
            {
                ticks = ((LinkObj<long>)((ASBinCode.rtData.rtObject)arg).value).value;
            }


            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, new TimeSpan(ticks));

            success = true;
        }
    }

    class system_timespan_static_fromDays : NativeFunctionBase
    {
        public system_timespan_static_fromDays()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_TimeSpan_static_fromDays";
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
            double days = ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, TimeSpan.FromDays(days));

            success = true;
        }
    }

    class system_timespan_static_fromHours : NativeFunctionBase
    {
        public system_timespan_static_fromHours()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_TimeSpan_static_fromHours";
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
            double hours = ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, TimeSpan.FromHours(hours));

            success = true;
        }
    }

    class system_timespan_static_fromMilliseconds : NativeFunctionBase
    {
        public system_timespan_static_fromMilliseconds()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_TimeSpan_static_fromMilliseconds";
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
            double milliseconds = ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, TimeSpan.FromMilliseconds(milliseconds));

            success = true;
        }
    }

    class system_timespan_static_fromMinutes : NativeFunctionBase
    {
        public system_timespan_static_fromMinutes()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_TimeSpan_static_fromMinutes";
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
            double minutes = ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, TimeSpan.FromMinutes(minutes));

            success = true;
        }
    }

    class system_timespan_static_fromSeconds : NativeFunctionBase
    {
        public system_timespan_static_fromSeconds()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_number);
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
                return "_system_TimeSpan_static_fromSeconds";
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
            double seconds = ASRuntime.TypeConverter.ConvertToNumber(argements[0].getValue());
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, TimeSpan.FromSeconds(seconds));

            success = true;
        }
    }

    class system_timespan_static_parse : NativeFunctionBase
    {
        public system_timespan_static_parse()
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_string);
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
                return "_system_TimeSpan_parse";
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
            string s = ASRuntime.TypeConverter.ConvertToString(argements[0].getValue(),null,null);
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);
            StackFrame frame = (StackFrame)stackframe;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, frame.player, TimeSpan.Parse(s));

            success = true;
        }
    }


    class system_timespan_operator_greaterThan : NativeFunctionBase
    {
        public system_timespan_operator_greaterThan()
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
                return "_system_TimeSpan_operator_greaterThan";
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

        public override void execute2(RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT[] argements,
            SLOT returnSlot,
            SourceToken token,
            object stackframe, out bool success)
        {
            StackFrame frame = (StackFrame)stackframe;

            TimeSpan ts1;
            var arg1 = argements[0].getValue();
            if (arg1.rtType == RunTimeDataType.rt_null)
            {
                ts1 = default(TimeSpan);
            }
            else
            {
                LinkObj<TimeSpan> argObj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)arg1).value;
                ts1 = argObj.value;
            }

            TimeSpan ts2;
            var arg2 = argements[1].getValue();
            if (arg2.rtType == RunTimeDataType.rt_null)
            {
                ts2 = default(TimeSpan);
            }
            else
            {
                LinkObj<TimeSpan> argObj = (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)arg2).value;
                ts2 = argObj.value;
            }
            if (ts1 > ts2)
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
            }
            else
            {
                returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
            }
            success = true;
        }

    }





}
