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


        }
    }
}
