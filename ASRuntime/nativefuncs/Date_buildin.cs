using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASRuntime.nativefuncs
{
    class Date_link
    {
        public DateTime datetime;
        public bool isvalid;
    }


    class Date_constructor : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Date_constructor():base(7)
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

        //public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        //{
        //    errormessage = null;
        //    errorno = 0;


        //    ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

        //    Date_link datelink = new Date_link();

        //    rtObj.hosted_object = datelink;

        //    if (argements[0].getValue().rtType == RunTimeDataType.rt_null)
        //    {
        //        datelink.datetime = DateTime.Now;
        //        datelink.isvalid = true;
        //    }
        //    else
        //    {
        //        double month = ((rtNumber)argements[1].getValue()).value;
        //        double date = ((rtNumber)argements[2].getValue()).value;
        //        double hour = ((rtNumber)argements[3].getValue()).value;
        //        double minute = ((rtNumber)argements[4].getValue()).value;
        //        double second = ((rtNumber)argements[5].getValue()).value;
        //        double millisecond = ((rtNumber)argements[6].getValue()).value;

        //        if (double.IsNaN(month))
        //        {
        //            //***一个参数***
        //            var yearOrTimevalue = argements[0].getValue();
        //            if (yearOrTimevalue.rtType > RunTimeDataType.unknown)
        //            {
        //                RunTimeDataType ot;
        //                if (TypeConverter.Object_CanImplicit_ToPrimitive(((rtObject)yearOrTimevalue).value._class, out ot))
        //                {
        //                    yearOrTimevalue = TypeConverter.ObjectImplicit_ToPrimitive((rtObject)yearOrTimevalue);
        //                }
        //            }

        //            if (yearOrTimevalue.rtType > RunTimeDataType.unknown
        //                ||
        //                (
        //                yearOrTimevalue.rtType != RunTimeDataType.rt_number
        //                &&
        //                yearOrTimevalue.rtType != RunTimeDataType.rt_int
        //                &&
        //                yearOrTimevalue.rtType != RunTimeDataType.rt_uint
        //                )
        //                )
        //            {
        //                string str = TypeConverter.ConvertToString(yearOrTimevalue, null, null);

        //                DateTime r;
        //                if (DateTime.TryParse(str, System.Globalization.CultureInfo.InvariantCulture, 
        //                    System.Globalization.DateTimeStyles.AssumeLocal, out r))
        //                {
        //                    datelink.isvalid = true;
        //                    datelink.datetime = r.ToLocalTime();
        //                }
        //                else
        //                {
        //                    if (DateTime.TryParseExact(str,
        //                        "ddd MMM d HH:mm:ss 'GMT'zzzzz yyyy",
        //                        System.Globalization.CultureInfo.InvariantCulture,
        //                        System.Globalization.DateTimeStyles.None, out r))
        //                    {
        //                        datelink.isvalid = true;
        //                        datelink.datetime = r.ToLocalTime();
        //                    }
        //                    else
        //                    {
        //                        datelink.isvalid = false;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                double v = TypeConverter.ConvertToNumber(yearOrTimevalue);
        //                if (double.IsNaN(v))
        //                {
        //                    datelink.isvalid = false;
        //                }
        //                else
        //                {
        //                    //int t = (int)v;

        //                    DateTime bd = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(v).ToLocalTime();
        //                    datelink.datetime = bd;
        //                    datelink.isvalid = true;
        //                }
        //            }


        //        }
        //        else
        //        {
        //            try
        //            {
        //                int year = TypeConverter.ConvertToInt(argements[0].getValue(), null, null);

        //                if (year < 100) { year = 1900 + year; }

        //                year = (year-1) % 9999 + 1;

                        

        //                DateTime t = new DateTime(year,
        //                1,
        //                1,
        //                ((int)hour % 24),
        //                ((int)minute % 60),
        //                ((int)second % 60),
        //                ((int)millisecond % 999), DateTimeKind.Local
        //                );

        //                t=t.AddMonths((int)month);
        //                t = t.AddDays((int)date - 1);

        //                datelink.datetime = t;
        //                datelink.isvalid = true;

        //            }
        //            catch (ArgumentOutOfRangeException)
        //            {
        //                datelink.isvalid = false;
        //            }
        //        }
        //    }


        //    return ASBinCode.rtData.rtUndefined.undefined;
        //}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			
			ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

			Date_link datelink = new Date_link();

			rtObj.hosted_object = datelink;

			if (argements[0].rtType == RunTimeDataType.rt_null)
			{
				datelink.datetime = DateTime.Now;
				datelink.isvalid = true;
			}
			else
			{
				double month = ((rtNumber)argements[1]).value;
				double date = ((rtNumber)argements[2]).value;
				double hour = ((rtNumber)argements[3]).value;
				double minute = ((rtNumber)argements[4]).value;
				double second = ((rtNumber)argements[5]).value;
				double millisecond = ((rtNumber)argements[6]).value;

				if (double.IsNaN(month))
				{
					//***一个参数***
					var yearOrTimevalue = argements[0];
					if (yearOrTimevalue.rtType > RunTimeDataType.unknown)
					{
						RunTimeDataType ot;
						if (TypeConverter.Object_CanImplicit_ToPrimitive(((rtObjectBase)yearOrTimevalue).value._class, out ot))
						{
							yearOrTimevalue = TypeConverter.ObjectImplicit_ToPrimitive((rtObjectBase)yearOrTimevalue);
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
						double v = TypeConverter.ConvertToNumber(yearOrTimevalue);
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
						int year = TypeConverter.ConvertToInt(argements[0]);

						if (year < 100) { year = 1900 + year; }

						year = (year - 1) % 9999 + 1;



						DateTime t = new DateTime(year,
						1,
						1,
						((int)hour % 24),
						((int)minute % 60),
						((int)second % 60),
						((int)millisecond % 999), DateTimeKind.Local
						);

						t = t.AddMonths((int)month);
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

			success = true;
			returnSlot.directSet(rtUndefined.undefined);
		}

	}

    class Date_tostring : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Date_tostring():base(0)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;


		//    ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

		//    Date_link datelink = (Date_link)rtObj.hosted_object;
		//    if (datelink.isvalid)
		//    {
		//        return new rtString(datelink.datetime.ToString("ddd MMM d HH:mm:ss 'GMT'zz00 yyyy",System.Globalization.CultureInfo.InvariantCulture));
		//    }
		//    else
		//    {
		//        return new rtString("Invalid Date");
		//    }
		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;


			ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

			Date_link datelink = (Date_link)rtObj.hosted_object;
			if (datelink.isvalid)
			{
				returnSlot.setValue(datelink.datetime.ToString("ddd MMM d HH:mm:ss 'GMT'zz00 yyyy", System.Globalization.CultureInfo.InvariantCulture));
				//return new rtString(datelink.datetime.ToString("ddd MMM d HH:mm:ss 'GMT'zz00 yyyy", System.Globalization.CultureInfo.InvariantCulture));
			}
			else
			{
				returnSlot.setValue("Invalid Date");
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

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



    class Date_valueof : NativeConstParameterFunction
    {
        private List<RunTimeDataType> _paras;

        public Date_valueof():base(0)
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

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null;
		//    errorno = 0;


		//    ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObject)thisObj).value;

		//    Date_link datelink = (Date_link)rtObj.hosted_object;
		//    if (datelink.isvalid)
		//    {
		//        return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc)).TotalMilliseconds);
		//    }
		//    else
		//    {
		//        return new rtNumber(double.NaN);
		//    }
		//}
		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;


			ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

			Date_link datelink = (Date_link)rtObj.hosted_object;
			if (datelink.isvalid)
			{
				returnSlot.setValue((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
			}
			else
			{
				returnSlot.setValue(double.NaN);
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

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

    class Date_getday : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getday()
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
                return "_date_getday";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber((int)datelink.datetime.DayOfWeek);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getfullyear : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getfullyear()
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
                return "_date_getfullyear";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber((int)datelink.datetime.Year);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_gethours : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_gethours()
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
                return "_date_gethours";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber((int)datelink.datetime.Hour);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getmilliseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getmilliseconds()
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
                return "_date_getmilliseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber((int)datelink.datetime.Millisecond);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_getminutes : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getminutes()
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
                return "_date_getminutes";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber((int)datelink.datetime.Minute);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getmonth : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getmonth()
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
                return "_date_getmonth";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber(datelink.datetime.Month - 1);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getseconds()
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
                return "_date_getseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                return new rtNumber((int)datelink.datetime.Second);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_timezoneoffset : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_timezoneoffset()
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
                return "_date_gettimezoneoffset";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                var d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTimeKind.Local);
                var ud = d.ToUniversalTime();
                d = new DateTime(d.Year,d.Month,d.Day,d.Hour,d.Minute,d.Second, DateTimeKind.Utc);

                return new rtNumber((ud-d).TotalMinutes);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getutcdate : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutcdate()
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
                return "_date_getutcdate";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
               
                return new rtNumber(datelink.datetime.ToUniversalTime().Day);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getutcday : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutcday()
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
                return "_date_getutcday";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {

                return new rtNumber((int)datelink.datetime.ToUniversalTime().DayOfWeek);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getutcfullyear : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutcfullyear()
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
                return "_date_getutcfullyear";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {

                return new rtNumber(datelink.datetime.ToUniversalTime().Year);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_getutchours : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutchours()
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
                return "_date_getutchours";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {

                return new rtNumber(datelink.datetime.ToUniversalTime().Hour);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getutcmilliseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutcmilliseconds()
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
                return "_date_getutcmilliseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {

                return new rtNumber(datelink.datetime.ToUniversalTime().Millisecond);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getutcminutes : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutcminutes()
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
                return "_date_getutcminutes";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {

                return new rtNumber(datelink.datetime.ToUniversalTime().Minute);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getutcmonth : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutcmonth()
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
                return "_date_getutcmonth";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {

                return new rtNumber(datelink.datetime.ToUniversalTime().Month-1);
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_getutcseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_getutcseconds()
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
                return "_date_getutcseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {

                return new rtNumber(datelink.datetime.ToUniversalTime().Second);
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
                return RunTimeDataType.rt_number;
            }
        }

        public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double d = argements[0].getValue().toNumber();

                try
                {
                    datelink.datetime = datelink.datetime.AddDays(d - datelink.datetime.Day);
                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_setfullyear : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setfullyear()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setfullyear";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double y = argements[0].getValue().toNumber();
                if (double.IsNaN(y))
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
                try
                {
                    datelink.datetime = datelink.datetime.AddYears((int)y - datelink.datetime.Year);

                    double m = argements[1].getValue().toNumber();
                    if (!double.IsNaN(m))
                    {
                        
                        datelink.datetime = datelink.datetime.AddMonths((int)m+1 - datelink.datetime.Month);

                        double d = argements[2].getValue().toNumber();
                        if (!double.IsNaN(d))
                        {
                            datelink.datetime = datelink.datetime.AddDays(d - datelink.datetime.Month); 
                        }
                        
                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_sethours : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_sethours()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_sethours";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double h = argements[0].getValue().toNumber();
                if (double.IsNaN(h))
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
                try
                {
                    datelink.datetime = datelink.datetime.AddHours(h - datelink.datetime.Hour);

                    double m = argements[1].getValue().toNumber();
                    if (!double.IsNaN(m))
                    {

                        datelink.datetime = datelink.datetime.AddMinutes(m - datelink.datetime.Minute);

                        double s = argements[2].getValue().toNumber();
                        if (!double.IsNaN(s))
                        {
                            datelink.datetime = datelink.datetime.AddSeconds(s - datelink.datetime.Second);

                            double ms = argements[3].getValue().toNumber();
                            if (!double.IsNaN(ms))
                            {
                                datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.Millisecond);
                            }

                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setmillseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setmillseconds()
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
                return "_date_setmilliseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double ms = argements[0].getValue().toNumber();
                if (double.IsNaN(ms))
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
                try
                {
                    datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.Millisecond);

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setminutes : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setminutes()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setminutes";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                try
                {
                    
                    double m = argements[0].getValue().toNumber();

                    if (double.IsNaN(m))
                    {
                        datelink.isvalid = false;
                        return new rtNumber(double.NaN);
                    }
                    else
                    {

                        datelink.datetime = datelink.datetime.AddMinutes(m - datelink.datetime.Minute);

                        double s = argements[1].getValue().toNumber();
                        if (!double.IsNaN(s))
                        {
                            datelink.datetime = datelink.datetime.AddSeconds(s - datelink.datetime.Second);

                            double ms = argements[2].getValue().toNumber();
                            if (!double.IsNaN(ms))
                            {
                                datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.Millisecond);
                            }

                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_setmonth : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setmonth()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setmonth";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                try
                {

                    double m = argements[0].getValue().toNumber();

                    if (double.IsNaN(m))
                    {
                        datelink.isvalid = false;
                        return new rtNumber(double.NaN);
                    }
                    else
                    {

                        datelink.datetime = datelink.datetime.AddMonths((int)m+1 - datelink.datetime.Month);

                        double d = argements[1].getValue().toNumber();
                        if (!double.IsNaN(d))
                        {
                            datelink.datetime = datelink.datetime.AddDays(d - datelink.datetime.Day);
                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setseconds()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                try
                {

                    double s = argements[0].getValue().toNumber();

                    if (double.IsNaN(s))
                    {
                        datelink.isvalid = false;
                        return new rtNumber(double.NaN);
                    }
                    else
                    {

                        datelink.datetime = datelink.datetime.AddSeconds(s - datelink.datetime.Second);

                        double ms = argements[1].getValue().toNumber();
                        if (!double.IsNaN(ms))
                        {
                            datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.Millisecond);
                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_settime : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_settime()
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
                return "_date_settime";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            
                try
                {

                    double t = argements[0].getValue().toNumber();

                    if (double.IsNaN(t))
                    {
                        datelink.isvalid = false;
                        return new rtNumber(double.NaN);
                    }
                    else
                    {

                        DateTime bt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        bt=bt.AddMilliseconds(t).ToLocalTime();
                        datelink.datetime = bt;
                        datelink.isvalid = true;
                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            
            
        }
    }

    class Date_setutcdate : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setutcdate()
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
                return "_date_setutcdate";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double d = argements[0].getValue().toNumber();

                try
                {
                    datelink.datetime = datelink.datetime.AddDays(d - datelink.datetime.ToUniversalTime().Day);
                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_setutcfullyear : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setutcfullyear()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setutcfullyear";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double y = argements[0].getValue().toNumber();
                if (double.IsNaN(y))
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
                try
                {
                    datelink.datetime = datelink.datetime.AddYears((int)y - datelink.datetime.ToUniversalTime().Year);

                    double m = argements[1].getValue().toNumber();
                    if (!double.IsNaN(m))
                    {

                        datelink.datetime = datelink.datetime.AddMonths((int)m + 1 - datelink.datetime.ToUniversalTime().Month);

                        double d = argements[2].getValue().toNumber();
                        if (!double.IsNaN(d))
                        {
                            datelink.datetime = datelink.datetime.AddDays(d - datelink.datetime.ToUniversalTime().Month);
                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setutchours : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setutchours()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setutchours";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double h = argements[0].getValue().toNumber();
                if (double.IsNaN(h))
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
                try
                {
                    datelink.datetime = datelink.datetime.AddHours(h - datelink.datetime.ToUniversalTime().Hour);

                    double m = argements[1].getValue().toNumber();
                    if (!double.IsNaN(m))
                    {

                        datelink.datetime = datelink.datetime.AddMinutes(m - datelink.datetime.ToUniversalTime().Minute);

                        double s = argements[2].getValue().toNumber();
                        if (!double.IsNaN(s))
                        {
                            datelink.datetime = datelink.datetime.AddSeconds(s - datelink.datetime.ToUniversalTime().Second);

                            double ms = argements[3].getValue().toNumber();
                            if (!double.IsNaN(ms))
                            {
                                datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.ToUniversalTime().Millisecond);
                            }

                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setutcmillseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setutcmillseconds()
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
                return "_date_setutcmilliseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                double ms = argements[0].getValue().toNumber();
                if (double.IsNaN(ms))
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
                try
                {
                    datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.ToUniversalTime().Millisecond);

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setutcminutes : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setutcminutes()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setutcminutes";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                try
                {

                    double m = argements[0].getValue().toNumber();

                    if (double.IsNaN(m))
                    {
                        datelink.isvalid = false;
                        return new rtNumber(double.NaN);
                    }
                    else
                    {

                        datelink.datetime = datelink.datetime.AddMinutes(m - datelink.datetime.ToUniversalTime().Minute);

                        double s = argements[1].getValue().toNumber();
                        if (!double.IsNaN(s))
                        {
                            datelink.datetime = datelink.datetime.AddSeconds(s - datelink.datetime.ToUniversalTime().Second);

                            double ms = argements[2].getValue().toNumber();
                            if (!double.IsNaN(ms))
                            {
                                datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.ToUniversalTime().Millisecond);
                            }

                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_setutcmonth : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setutcmonth()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setutcmonth";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                try
                {

                    double m = argements[0].getValue().toNumber();

                    if (double.IsNaN(m))
                    {
                        datelink.isvalid = false;
                        return new rtNumber(double.NaN);
                    }
                    else
                    {

                        datelink.datetime = datelink.datetime.AddMonths((int)m + 1 - datelink.datetime.ToUniversalTime().Month);

                        double d = argements[1].getValue().toNumber();
                        if (!double.IsNaN(d))
                        {
                            datelink.datetime = datelink.datetime.AddDays(d - datelink.datetime.ToUniversalTime().Day);
                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }

    class Date_setutcseconds : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_setutcseconds()
        {
            _paras = new List<RunTimeDataType>();
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
                return "_date_setutcseconds";
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


            ASBinCode.rtti.HostedDynamicObject rtObj = (ASBinCode.rtti.HostedDynamicObject)((rtObjectBase)thisObj).value;

            Date_link datelink = (Date_link)rtObj.hosted_object;
            if (datelink.isvalid)
            {
                try
                {

                    double s = argements[0].getValue().toNumber();

                    if (double.IsNaN(s))
                    {
                        datelink.isvalid = false;
                        return new rtNumber(double.NaN);
                    }
                    else
                    {

                        datelink.datetime = datelink.datetime.AddSeconds(s - datelink.datetime.ToUniversalTime().Second);

                        double ms = argements[1].getValue().toNumber();
                        if (!double.IsNaN(ms))
                        {
                            datelink.datetime = datelink.datetime.AddMilliseconds(ms - datelink.datetime.ToUniversalTime().Millisecond);
                        }

                    }

                    return new rtNumber((datelink.datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                }
                catch (ArgumentOutOfRangeException)
                {
                    datelink.isvalid = false;
                    return new rtNumber(double.NaN);
                }
            }
            else
            {
                return new rtNumber(double.NaN);
            }
        }
    }


    class Date_utc : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Date_utc()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_number);
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
                return "_date_utc";
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

            {
                double y = ((rtNumber)argements[0].getValue()).value;
                double month = ((rtNumber)argements[1].getValue()).value;
                double date = ((rtNumber)argements[2].getValue()).value;
                double hour = ((rtNumber)argements[3].getValue()).value;
                double minute = ((rtNumber)argements[4].getValue()).value;
                double second = ((rtNumber)argements[5].getValue()).value;
                double millisecond = ((rtNumber)argements[6].getValue()).value;

                if (double.IsNaN(y))
                {
                    return new rtNumber(double.NaN);
                }

                {
                    try
                    {
                        int year = (int)y;
                        if (year < 100) { year = 1900 + year; }
                        year = (year - 1) % 9999 + 1;

                        DateTime t = new DateTime(year,
                        1,
                        1,
                        ((int)hour % 24),
                        ((int)minute % 60),
                        ((int)second % 60),
                        ((int)millisecond % 999), DateTimeKind.Utc
                        );

                        t = t.AddMonths((int)month);
                        t = t.AddDays((int)date - 1);

                        return new rtNumber((t - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);

                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        return new rtNumber(double.NaN);
                    }
                }
            }
            
        }
    }

}
