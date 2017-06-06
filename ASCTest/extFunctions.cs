using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASCTest.regNativeFunctions;
using ASRuntime.nativefuncs;

namespace ASCTest
{
    class extFunctions : INativeFunctionRegister
    {
        public void registrationFunction(CSWC bin)
        {
            //bin.regNativeFunction(new enumitem_create());
            //bin.regNativeFunction(new enumitem_tostring());
            //bin.regNativeFunction(new enumitem_valueof());

            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_Object_creator__", default(object)));
            bin.regNativeFunction(new system_object_ctor());
            bin.regNativeFunction(LinkSystem_Buildin.getToString("_system_Object_toString"));
            bin.regNativeFunction(LinkSystem_Buildin.getGetHashCode("_system_Object_getHashCode"));
            bin.regNativeFunction(LinkSystem_Buildin.getEquals("_system_Object_equals"));
            bin.regNativeFunction(new object_static_equals());
            bin.regNativeFunction(new object_static_referenceEquals());

            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_DateTimeKind_creator__", default(DateTimeKind)));
            bin.regNativeFunction(new system_DateTimeKind_ctor());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DateTimeKind_Local_getter"
                ,
                () => { return DateTimeKind.Local; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DateTimeKind_Unspecified_getter"
                ,
                () => { return DateTimeKind.Unspecified; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DateTimeKind_Utc_getter"
                ,
                () => { return DateTimeKind.Utc; }
                )
                );

            bin.regNativeFunction(new system_DateTimeKind_operator_bitOr());



            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_DayOfWeek_creator__", default(DayOfWeek)));
            bin.regNativeFunction(new system_dayOfWeek_ctor());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DayOfWeek_Friday_getter"
                ,
                () => { return DayOfWeek.Friday; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DayOfWeek_Monday_getter"
                ,
                () => { return DayOfWeek.Monday; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DayOfWeek_Saturday_getter"
                ,
                () => { return DayOfWeek.Saturday; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DayOfWeek_Sunday_getter"
                ,
                () => { return DayOfWeek.Sunday; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DayOfWeek_Thursday_getter"
                ,
                () => { return DayOfWeek.Thursday; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DayOfWeek_Tuesday_getter"
                ,
                () => { return DayOfWeek.Tuesday; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DayOfWeek_Wednesday_getter"
                ,
                () => { return DayOfWeek.Wednesday; }
                )
                );
            bin.regNativeFunction(new system_dayOfWeek_operator_bitOr());


            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_Int64_creator__",default(long)) );// new system_int64_creator());
            bin.regNativeFunction(new system_int64_explicit_from());
            bin.regNativeFunction(new system_int64_implicit_from());
            bin.regNativeFunction(new system_int64_ctor());
            bin.regNativeFunction(new system_int64_valueOf());

            //bin.regNativeFunction(new system_int64_MaxValue_getter());
            //bin.regNativeFunction(new system_int64_MinValue_getter());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<long>("_system_Int64_MaxValue_getter"
                ,
                () => { return long.MaxValue; }
                )
                );

            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<long>("_system_Int64_MinValue_getter"
                ,
                () => { return long.MinValue; }
                )
                );

            bin.regNativeFunction(new system_int64_static_Parse());




            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_TimeSpan_creator__", default(TimeSpan)));
            bin.regNativeFunction(new system_timespan_ctor());
            bin.regNativeFunction(new system_timespan_add());
            bin.regNativeFunction(new system_timespan_subtract());
            bin.regNativeFunction(new system_timespan_duration());
            bin.regNativeFunction(new system_timespan_negate());
            bin.regNativeFunction(new system_timespan_days());
            bin.regNativeFunction(new system_timespan_hours());
            bin.regNativeFunction(new system_timespan_millseconds());
            bin.regNativeFunction(new system_timespan_minutes());
            bin.regNativeFunction(new system_timespan_seconds());
            bin.regNativeFunction(new system_timespan_ticks());
            bin.regNativeFunction(new system_timespan_totalDays());
            bin.regNativeFunction(new system_timespan_totalHours());
            bin.regNativeFunction(new system_timespan_totalMilliseconds());
            bin.regNativeFunction(new system_timespan_totalMinutes());
            bin.regNativeFunction(new system_timespan_totalSeconds());
            bin.regNativeFunction(LinkSystem_Buildin.getCompareTo<TimeSpan>("_system_TimeSpan_compareTo"));
            bin.regNativeFunction(LinkSystem_Buildin.getCompareTo_Generic<TimeSpan>("_system_TimeSpan_compareTo_TimeSpan"));
            bin.regNativeFunction(new system_timespan_static_compare());
            bin.regNativeFunction(new system_timespan_static_constructor_());
            bin.regNativeFunction(new system_timespan_static_constructor__());
            bin.regNativeFunction(new system_timespan_static_constructor___());
            bin.regNativeFunction(new system_timespan_static_fromDays());
            bin.regNativeFunction(new system_timespan_static_fromHours());
            bin.regNativeFunction(new system_timespan_static_fromMilliseconds());
            bin.regNativeFunction(new system_timespan_static_fromMinutes());
            bin.regNativeFunction(new system_timespan_static_fromSeconds());
            bin.regNativeFunction(new system_timespan_static_parse());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<TimeSpan>("_system_TimeSpan_static_MaxValue_getter"
                ,
                () => { return TimeSpan.MaxValue; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<TimeSpan>("_system_TimeSpan_static_MinValue_getter"
                ,
                () => { return TimeSpan.MinValue; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_TimeSpan_static_TicksPerDay_getter"
                ,
                () => { return TimeSpan.TicksPerDay; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_TimeSpan_static_TicksPerHour_getter"
                ,
                () => { return TimeSpan.TicksPerHour; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_TimeSpan_static_TicksPerMillisecond_getter"
                ,
                () => { return TimeSpan.TicksPerMillisecond; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_TimeSpan_static_TicksPerMinute_getter"
                ,
                () => { return TimeSpan.TicksPerMinute; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_TimeSpan_static_TicksPerSecond_getter"
                ,
                () => { return TimeSpan.TicksPerSecond; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_TimeSpan_static_Zero_getter"
                ,
                () => { return TimeSpan.Zero; }
                )
                );
            bin.regNativeFunction(new system_timespan_operator_greaterThan());
            bin.regNativeFunction(new system_timespan_operator_lessThan());
            bin.regNativeFunction(new system_timespan_operator_equality());
            bin.regNativeFunction(new system_timespan_operator_inequality());
            bin.regNativeFunction(new system_timespan_operator_greatherOrEqual());
            bin.regNativeFunction(new system_timespan_operator_lessThanOrEqual());
            bin.regNativeFunction(new system_timespan_operator_unaryPlus());
            bin.regNativeFunction(new system_timespan_operator_unaryNegation());
            bin.regNativeFunction(new system_timespan_operator_addition());
            bin.regNativeFunction(new system_timespan_operator_subtraction());



            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_DateTime_creator__", default(DateTime)));// new system_int64_creator());
            bin.regNativeFunction(new system_datetime_ctor());
            bin.regNativeFunction(new system_datetime_static_constructor_());
            bin.regNativeFunction(new system_datetime_static_constructor__());
            bin.regNativeFunction(new system_datetime_static_constructor___());
            bin.regNativeFunction(new system_datetime_static_constructor____());
            bin.regNativeFunction(new system_datetime_static_constructor_____());
            bin.regNativeFunction(new system_datetime_static_constructor______());
            bin.regNativeFunction(new system_datetime_static_constructor_______());
            bin.regNativeFunction(new system_datetime_kind());
            bin.regNativeFunction(new system_datetime_date());
            bin.regNativeFunction(new system_datetime_day());
            bin.regNativeFunction(new system_datetime_dayOfWeek());
            bin.regNativeFunction(new system_datetime_dayOfYear());
            bin.regNativeFunction(new system_datetime_hour());
            bin.regNativeFunction(new system_datetime_millsecond());
            bin.regNativeFunction(new system_datetime_minute());
            bin.regNativeFunction(new system_datetime_month());
            bin.regNativeFunction(new system_datetime_now());
            bin.regNativeFunction(new system_datetime_second());
            bin.regNativeFunction(new system_datetime_ticks());
            bin.regNativeFunction(new system_datetime_timeOfDay());
            bin.regNativeFunction(new system_datetime_year());
            bin.regNativeFunction(new system_datetime_toDay());
            bin.regNativeFunction(new system_datetime_utcNow());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DateTime_static_MaxValue_getter"
                ,
                () => { return DateTime.MaxValue; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter("_system_DateTime_static_MinValue_getter"
                ,
                () => { return DateTime.MinValue; }
                )
                );
            bin.regNativeFunction(new system_datetime_add());
            bin.regNativeFunction(new system_datetime_addDays());
            bin.regNativeFunction(new system_datetime_addHours());
            bin.regNativeFunction(new system_datetime_addMilliseconds());
            bin.regNativeFunction(new system_datetime_addMinutes());
            bin.regNativeFunction(new system_datetime_addMonths());
            bin.regNativeFunction(new system_datetime_addSeconds());
            bin.regNativeFunction(new system_datetime_addTicks());
            bin.regNativeFunction(new system_datetime_addYears());
            bin.regNativeFunction(new system_datetime_static_compare());
            bin.regNativeFunction(LinkSystem_Buildin.getCompareTo<DateTime>("_system_DateTime_compareTo"));
            bin.regNativeFunction(LinkSystem_Buildin.getCompareTo_Generic<DateTime>("_system_DateTime_compareTo_DateTime"));
            bin.regNativeFunction(new system_datetime_static_daysinmonth());

        }
    }
}
