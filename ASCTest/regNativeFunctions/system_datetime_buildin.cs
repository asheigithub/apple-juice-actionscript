using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
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

    class system_datetime_static_constructor__ : NativeConstParameterFunction
    {
        public system_datetime_static_constructor__():base(6)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_DateTime_static_constructor__";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            int year = ASRuntime.TypeConverter.ConvertToInt(argements[0], null, null);
            int month = ASRuntime.TypeConverter.ConvertToInt(argements[1], null, null);
            int days = ASRuntime.TypeConverter.ConvertToInt(argements[2], null, null);
            int hours = ASRuntime.TypeConverter.ConvertToInt(argements[3], null, null);
            int minutes = ASRuntime.TypeConverter.ConvertToInt(argements[4], null, null);
            int seconds = ASRuntime.TypeConverter.ConvertToInt(argements[5], null, null);

            try
            {
                ((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player,
                new DateTime(year, month, days, hours, minutes, seconds)
                );

                success = true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                stackframe.throwAneException(token, a.Message);
                success = false;
            }

            
        }
    }

    class system_datetime_static_constructor___ : NativeConstParameterFunction
    {
        public system_datetime_static_constructor___() : base(7)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_DateTime_static_constructor___";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            int year = ASRuntime.TypeConverter.ConvertToInt(argements[0], null, null);
            int month = ASRuntime.TypeConverter.ConvertToInt(argements[1], null, null);
            int days = ASRuntime.TypeConverter.ConvertToInt(argements[2], null, null);
            int hours = ASRuntime.TypeConverter.ConvertToInt(argements[3], null, null);
            int minutes = ASRuntime.TypeConverter.ConvertToInt(argements[4], null, null);
            int seconds = ASRuntime.TypeConverter.ConvertToInt(argements[5], null, null);

            DateTimeKind kind;
            if (argements[6].rtType > RunTimeDataType.unknown)
            {
                kind = ((LinkObj<DateTimeKind>)((ASBinCode.rtData.rtObject)argements[6]).value).value;
            }
            else
            {
                kind = default(DateTimeKind);
            }
            try
            {
                ((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player,
                new DateTime(year, month, days, hours, minutes, seconds,kind)
                );

                success = true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                stackframe.throwAneException(token, a.Message);
                success = false;
            }


        }
    }

    class system_datetime_static_constructor____ : NativeConstParameterFunction
    {
        public system_datetime_static_constructor____() : base(7)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_DateTime_static_constructor____";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            int year = ASRuntime.TypeConverter.ConvertToInt(argements[0], null, null);
            int month = ASRuntime.TypeConverter.ConvertToInt(argements[1], null, null);
            int days = ASRuntime.TypeConverter.ConvertToInt(argements[2], null, null);
            int hours = ASRuntime.TypeConverter.ConvertToInt(argements[3], null, null);
            int minutes = ASRuntime.TypeConverter.ConvertToInt(argements[4], null, null);
            int seconds = ASRuntime.TypeConverter.ConvertToInt(argements[5], null, null);
            int millseconds = ASRuntime.TypeConverter.ConvertToInt(argements[6], null, null);

            try
            {
                ((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player,
                new DateTime(year, month, days, hours, minutes, seconds, millseconds)
                );
                
                success = true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                stackframe.throwAneException(token, a.Message);
                success = false;
            }


        }
    }

    class system_datetime_static_constructor_____ : NativeConstParameterFunction
    {
        public system_datetime_static_constructor_____() : base(8)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_DateTime_static_constructor_____";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            int year = ASRuntime.TypeConverter.ConvertToInt(argements[0], null, null);
            int month = ASRuntime.TypeConverter.ConvertToInt(argements[1], null, null);
            int days = ASRuntime.TypeConverter.ConvertToInt(argements[2], null, null);
            int hours = ASRuntime.TypeConverter.ConvertToInt(argements[3], null, null);
            int minutes = ASRuntime.TypeConverter.ConvertToInt(argements[4], null, null);
            int seconds = ASRuntime.TypeConverter.ConvertToInt(argements[5], null, null);
            int millsecond = ASRuntime.TypeConverter.ConvertToInt(argements[6], null, null);

            DateTimeKind kind;
            if (argements[7].rtType > RunTimeDataType.unknown)
            {
                kind = ((LinkObj<DateTimeKind>)((ASBinCode.rtData.rtObject)argements[7]).value).value;
            }
            else
            {
                kind = default(DateTimeKind);
            }
            try
            {
                ((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player,
                new DateTime(year, month, days, hours, minutes, seconds,millsecond, kind)
                );

                success = true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                stackframe.throwAneException(token, a.Message);
                success = false;
            }


        }
    }

    class system_datetime_static_constructor______ : NativeConstParameterFunction
    {
        public system_datetime_static_constructor______() : base(1)
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
                return "_system_DateTime_static_constructor______";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            
            long ticks;
            if (argements[0].rtType > RunTimeDataType.unknown)
            {
                ticks = ((LinkObj<long>)((ASBinCode.rtData.rtObject)argements[0]).value).value;
            }
            else
            {
                ticks = default(long);
            }
            try
            {
                ((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player,
                new DateTime(ticks)
                );

                success = true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                stackframe.throwAneException(token, a.Message);
                success = false;
            }


        }
    }

    class system_datetime_static_constructor_______ : NativeConstParameterFunction
    {
        public system_datetime_static_constructor_______() : base(2)
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
                return "_system_DateTime_static_constructor_______";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            long ticks;
            if (argements[0].rtType > RunTimeDataType.unknown)
            {
                ticks = ((LinkObj<long>)((ASBinCode.rtData.rtObject)argements[0]).value).value;
            }
            else
            {
                ticks = default(long);
            }

            DateTimeKind kind;
            if (argements[1].rtType > RunTimeDataType.unknown)
            {
                kind = ((LinkObj<DateTimeKind>)((ASBinCode.rtData.rtObject)argements[1]).value).value;
            }
            else
            {
                kind = default(DateTimeKind);
            }

            try
            {
                ((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player,
                new DateTime(ticks,kind)
                );

                success = true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                stackframe.throwAneException(token, a.Message);
                success = false;
            }


        }
    }


    class system_datetime_kind : NativeConstParameterFunction
    {
        public system_datetime_kind() : base(0)
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
                return "_system_DateTime_kind";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            
            LinkObj<DateTime> dt= (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            if (stackframe.player.init_static_class(cls, token))    // 如果返回对象类型不是自身类型，需要先初始化类型防止出错。
            {
                success = true;

                ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                    dt.value.Kind);
            }
            else
            {
                success = false;
            }

        }
    }

    class system_datetime_date : NativeConstParameterFunction
    {
        public system_datetime_date() : base(0)
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
                return "_system_DateTime_date";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            
            success = true;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                dt.value.Date);
            

        }
    }

    class system_datetime_day : NativeConstParameterFunction
    {
        public system_datetime_day() : base(0)
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
                return "_system_DateTime_day";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;

            
            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.Day);


        }
    }

    class system_datetime_dayOfWeek : NativeConstParameterFunction
    {
        public system_datetime_dayOfWeek() : base(0)
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
                return "_system_DateTime_dayOfWeek";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            if (stackframe.player.init_static_class(cls, token))    // 如果返回对象类型不是自身类型，需要先初始化类型防止出错。
            {
                success = true;

                ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                    dt.value.DayOfWeek);
            }
            else
            {
                success = false;
            }

        }
    }


    class system_datetime_dayOfYear : NativeConstParameterFunction
    {
        public system_datetime_dayOfYear() : base(0)
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
                return "_system_DateTime_dayOfYear";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;


            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.DayOfYear);


        }
    }

    class system_datetime_hour : NativeConstParameterFunction
    {
        public system_datetime_hour() : base(0)
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
                return "_system_DateTime_hour";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;


            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.Hour);


        }
    }

    class system_datetime_millsecond : NativeConstParameterFunction
    {
        public system_datetime_millsecond() : base(0)
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
                return "_system_DateTime_millsecond";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;


            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.Millisecond);


        }
    }


    class system_datetime_minute : NativeConstParameterFunction
    {
        public system_datetime_minute() : base(0)
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
                return "_system_DateTime_minute";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;


            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.Minute);


        }
    }
    class system_datetime_month : NativeConstParameterFunction
    {
        public system_datetime_month() : base(0)
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
                return "_system_DateTime_month";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;


            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.Month);


        }
    }

    class system_datetime_now : NativeConstParameterFunction
    {
        public system_datetime_now() : base(0)
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
                return "_system_DateTime_now";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

           
            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);


            success = true;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                DateTime.Now);


        }
    }


    class system_datetime_second : NativeConstParameterFunction
    {
        public system_datetime_second() : base(0)
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
                return "_system_DateTime_second";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;


            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.Second);


        }
    }

    class system_datetime_ticks : NativeConstParameterFunction
    {
        public system_datetime_ticks() : base(0)
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
                return "_system_DateTime_ticks";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            if (stackframe.player.init_static_class(cls, token))    // 如果返回对象类型不是自身类型，需要先初始化类型防止出错。
            {
                success = true;

                ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                    dt.value.Ticks);
            }
            else
            {
                success = false;
            }

        }
    }

    class system_datetime_timeOfDay : NativeConstParameterFunction
    {
        public system_datetime_timeOfDay() : base(0)
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
                return "_system_DateTime_timeOfDay";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;

            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            if (stackframe.player.init_static_class(cls, token))    // 如果返回对象类型不是自身类型，需要先初始化类型防止出错。
            {
                success = true;

                ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                    dt.value.TimeOfDay);
            }
            else
            {
                success = false;
            }

        }
    }
    class system_datetime_year : NativeConstParameterFunction
    {
        public system_datetime_year() : base(0)
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
                return "_system_DateTime_year";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            LinkObj<DateTime> dt = (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;


            success = true;

            ((StackSlot)returnSlot).setValue(dt.value.Year);


        }
    }
    class system_datetime_toDay : NativeConstParameterFunction
    {
        public system_datetime_toDay() : base(0)
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
                return "_system_DateTime_toDay";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {


            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);


            success = true;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                DateTime.Today);


        }
    }
    class system_datetime_utcNow : NativeConstParameterFunction
    {
        public system_datetime_utcNow() : base(0)
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
                return "_system_DateTime_utcNow";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {


            var cls = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);


            success = true;

            ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player,
                DateTime.UtcNow);

            
        }
    }

    class system_datetime_add :NativeConstParameterFunction
    {
        public system_datetime_add():base(1)
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
                return "_system_DateTime_add";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            LinkObj<DateTime> lobj = 
                (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;
            var arg = argements[0];
            TimeSpan ts;
            if (arg.rtType == RunTimeDataType.rt_null)
            {
                ts = default(TimeSpan);
            }
            else
            {
                LinkObj<TimeSpan> argObj = 
                    (LinkObj<TimeSpan>)((ASBinCode.rtData.rtObject)arg).value;
                ts = argObj.value;
            }

           
            var typeClass = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            ((StackSlot)returnSlot).setLinkObjectValue(
                typeClass, stackframe.player, lobj.value.Add(ts));
                
            success = true;
            
        }
        
    }
    class system_datetime_addDays : NativeConstParameterFunction
    {
        public system_datetime_addDays() : base(1)
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
                return "_system_DateTime_addDays";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            LinkObj<DateTime> lobj =
                (LinkObj<DateTime>)((ASBinCode.rtData.rtObject)thisObj).value;
            double arg = argements[0].toNumber();
            
            var typeClass = bin.getClassByRunTimeDataType(functionDefine.signature.returnType);

            ((StackSlot)returnSlot).setLinkObjectValue(
                typeClass, stackframe.player, lobj.value.AddDays(arg));

            success = true;

        }

    }

}
