using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Date_link
    {
        public DateTime datetime;
        public bool isvalid;
    }


    class Date_constructor : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_constructor()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);
            _paras.Add(RunTimeDataType.rt_number);

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
                return "_Date_constructor";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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
            errormessage = null;
            errorno = 0;


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = new Date_link();

            rtObj.hosted_object = datelink;

            if (argements[0].getValue().rtType == RunTimeDataType.rt_null)
            {
                datelink.datetime = DateTime.Now;
                datelink.isvalid = true;
            }
            else
            {
                double month = ((rtNumber)argements[1].getValue()).value;
                double date = ((rtNumber)argements[2].getValue()).value;
                double hour = ((rtNumber)argements[3].getValue()).value;
                double minute = ((rtNumber)argements[4].getValue()).value;
                double second = ((rtNumber)argements[5].getValue()).value;
                double millisecond = ((rtNumber)argements[6].getValue()).value;

                if (double.IsNaN(month))
                {
                    //***一个参数***
                    var yearOrTimevalue = argements[0].getValue();
                    if (yearOrTimevalue.rtType > RunTimeDataType.unknown)
                    {
                        RunTimeDataType ot;
                        if (TypeConverter.Object_CanImplicit_ToPrimitive(((rtObject)yearOrTimevalue).value._class, out ot))
                        {
                            yearOrTimevalue = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)yearOrTimevalue);
                        }
                    }

                    if (yearOrTimevalue.rtType > RunTimeDataType.unknown
                        ||
                        (
                        yearOrTimevalue.rtType != RunTimeDataType.rt_number
                        &&
                        yearOrTimevalue.rtType != RunTimeDataType.rt_int
                        &&
                        yearOrTimevalue.rtType != RunTimeDataType.rt_uint
                        )
                        )
                    {
                        string str = TypeConverter.ConvertToString(yearOrTimevalue, null, null);

                        DateTime r;
                        if (DateTime.TryParse(str, System.Globalization.CultureInfo.InvariantCulture, 
                            System.Globalization.DateTimeStyles.AssumeLocal, out r))
                        {
                            datelink.isvalid = true;
                            datelink.datetime = r.ToLocalTime();
                        }
                        else
                        {
                            if (DateTime.TryParseExact(str,
                                "ddd MMM d HH:mm:ss 'GMT'zzzzz yyyy",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out r))
                            {
                                datelink.isvalid = true;
                                datelink.datetime = r.ToLocalTime();
                            }
                            else
                            {
                                datelink.isvalid = false;
                            }
                        }
                    }
                    else
                    {
                        double v = TypeConverter.ConvertToNumber(yearOrTimevalue, null, null);
                        if (double.IsNaN(v))
                        {
                            datelink.isvalid = false;
                        }
                        else
                        {
                            //int t = (int)v;

                            DateTime bd = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(v).ToLocalTime();
                            datelink.datetime = bd;
                            datelink.isvalid = true;
                        }
                    }


                }
                else
                {
                    try
                    {
                        int year = TypeConverter.ConvertToInt(argements[0].getValue(), null, null);
                        year = (year-1) % 9999 + 1;

                        

                        DateTime t = new DateTime(year,
                        1,
                        1,
                        ((int)hour % 24),
                        ((int)minute % 60),
                        ((int)second % 60),
                        ((int)millisecond % 999), DateTimeKind.Local
                        );

                        t=t.AddMonths((int)month);
                        t = t.AddDays((int)date - 1);

                        datelink.datetime = t;
                        datelink.isvalid = true;

                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        datelink.isvalid = false;
                    }
                }
            }


            return ASBinCode.rtData.rtUndefined.undefined;
        }
    }

    class Date_tostring : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_tostring()
        {
            _paras = new List<RunTimeDataType>();


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
                return "_date_tostring";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtString(datelink.datetime.ToString("ddd MMM d HH:mm:ss 'GMT'zz00 yyyy",System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                return new rtString("Invalid Date");
            }
        }
    }

    class Date_totimestring : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_totimestring()
        {
            _paras = new List<RunTimeDataType>();


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
                return "_date_totimestring";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtString(datelink.datetime.ToString("HH:mm:ss 'GMT'zz00", System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                return new rtString("Invalid Date");
            }
        }
    }

    class Date_toUTCstring : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_toUTCstring()
        {
            _paras = new List<RunTimeDataType>();


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
                return "_date_toUTCstring";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtString(
                    datelink.datetime.ToUniversalTime().ToString(
                        "ddd MMM d HH:mm:ss yyyy 'UTC'", System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                return new rtString("Invalid Date");
            }
        }
    }

    class Date_toLocalTimeString : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_toLocalTimeString()
        {
            _paras = new List<RunTimeDataType>();


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
                return "_date_tolocaletimestring";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtString(
                    datelink.datetime.ToString(
                        "hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                return new rtString("Invalid Date");
            }
        }
    }

    class Date_toLocalString : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_toLocalString()
        {
            _paras = new List<RunTimeDataType>();


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
                return "_date_tolocalestring";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtString(
                    datelink.datetime.ToString(
                        "ddd MMM d yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                return new rtString("Invalid Date");
            }
        }
    }

    class Date_toLocalDateString : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_toLocalDateString()
        {
            _paras = new List<RunTimeDataType>();


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
                return "_date_tolocaledatestring";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtString(
                    datelink.datetime.ToString(
                        "ddd MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                return new rtString("Invalid Date");
            }
        }
    }



    class Date_valueof : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_valueof()
        {
            _paras = new List<RunTimeDataType>();


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
                return "_date_valueof";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_number;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc)).TotalMilliseconds);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_getdate : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getdate()
        {
            _paras = new List<RunTimeDataType>();
           

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
                return "_date_getdate";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_number;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber(datelink.datetime.Day);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setdate : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setdate()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_number);

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
                return "_date_setdate";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                int d = TypeConverter.ConvertToInt(argements[0].getValue(), null, null);

                try
                {
                    datelink.datetime=datelink.datetime.AddDays(d - datelink.datetime.Day);
                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }
            
            return rtUndefined.undefined;
            
        }
    }

}
