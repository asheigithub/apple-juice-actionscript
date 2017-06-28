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
            
            bin.regNativeFunction(LinkSystem_Buildin.getCreator("_system_Object_creator__", default(object)));
            bin.regNativeFunction(new system_object_ctor());
            bin.regNativeFunction(LinkSystem_Buildin.getToString("_system_Object_toString"));
            bin.regNativeFunction(LinkSystem_Buildin.getGetHashCode("_system_Object_getHashCode"));
            bin.regNativeFunction(LinkSystem_Buildin.getEquals("_system_Object_equals"));
            bin.regNativeFunction(new object_static_equals());
            bin.regNativeFunction(new object_static_referenceEquals());

            system_collections_interface.regNativeFunctions(bin);

            system_arrays_buildin.regNativeFunctions(bin);

            system_byte_buildin.regNativeFunctions(bin);
            system_char_buildin.regNativeFunctions(bin);
            system_sbyte_buildin.regNativeFunctions(bin);
            system_uint64_buildin.regNativeFunctions(bin);

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
            bin.regNativeFunction(new system_int64_toString_());



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
            bin.regNativeFunction(new system_datetime_getdatetimeformats());
            bin.regNativeFunction(new system_datetime_subtract());
            bin.regNativeFunction(new system_datetime_subtract_timespan());

            system_icomparable_buildin.regNativeFunctions(bin);
            system_collections_hashtable_buildin.regNativeFunctions(bin);
            system_collections_arraylist_buildin.regNativeFunctions(bin);
            system_collections_stack_buildin.regNativeFunctions(bin);
            system_collections_queue_buildin.regNativeFunctions(bin);

            //system_StringSplitOptions_buildin.regNativeFunctions(bin);

			regAutoCreateCodes(bin);

        }


		private void regAutoCreateCodes(CSWC bin)
		{
			system_StringSplitOptions_buildin.regNativeFunctions(bin);
			system_StringComparison_buildin.regNativeFunctions(bin);
			system_DateTimeKind_buildin.regNativeFunctions(bin);
			system_AppDomainManagerInitializationOptions_buildin.regNativeFunctions(bin);
			system_LoaderOptimization_buildin.regNativeFunctions(bin);
			system_AttributeTargets_buildin.regNativeFunctions(bin);
			system_ConsoleColor_buildin.regNativeFunctions(bin);
			system_ConsoleKey_buildin.regNativeFunctions(bin);
			system_ConsoleModifiers_buildin.regNativeFunctions(bin);
			system_ConsoleSpecialKey_buildin.regNativeFunctions(bin);
			system_Base64FormattingOptions_buildin.regNativeFunctions(bin);
			system_DayOfWeek_buildin.regNativeFunctions(bin);
			system_EnvironmentVariableTarget_buildin.regNativeFunctions(bin);
			system_GCCollectionMode_buildin.regNativeFunctions(bin);
			system_GCNotificationStatus_buildin.regNativeFunctions(bin);
			system_MidpointRounding_buildin.regNativeFunctions(bin);
			system_PlatformID_buildin.regNativeFunctions(bin);
			system_TypeCode_buildin.regNativeFunctions(bin);
			system_threading_EventResetMode_buildin.regNativeFunctions(bin);
			system_threading_ThreadPriority_buildin.regNativeFunctions(bin);
			system_threading_ThreadState_buildin.regNativeFunctions(bin);
			system_threading_ApartmentState_buildin.regNativeFunctions(bin);
			system_diagnostics_DebuggerBrowsableState_buildin.regNativeFunctions(bin);
			system_diagnostics_symbolstore_SymAddressKind_buildin.regNativeFunctions(bin);
			system_reflection_AssemblyNameFlags_buildin.regNativeFunctions(bin);
			system_reflection_ProcessorArchitecture_buildin.regNativeFunctions(bin);
			system_reflection_BindingFlags_buildin.regNativeFunctions(bin);
			system_reflection_CallingConventions_buildin.regNativeFunctions(bin);
			system_reflection_EventAttributes_buildin.regNativeFunctions(bin);
			system_reflection_FieldAttributes_buildin.regNativeFunctions(bin);
			system_reflection_GenericParameterAttributes_buildin.regNativeFunctions(bin);
			system_reflection_ResourceLocation_buildin.regNativeFunctions(bin);
			system_reflection_MemberTypes_buildin.regNativeFunctions(bin);
			system_reflection_MethodAttributes_buildin.regNativeFunctions(bin);
			system_reflection_MethodImplAttributes_buildin.regNativeFunctions(bin);
			system_reflection_PortableExecutableKinds_buildin.regNativeFunctions(bin);
			system_reflection_ImageFileMachine_buildin.regNativeFunctions(bin);
			system_reflection_ExceptionHandlingClauseOptions_buildin.regNativeFunctions(bin);
			system_reflection_ParameterAttributes_buildin.regNativeFunctions(bin);
			system_reflection_PropertyAttributes_buildin.regNativeFunctions(bin);
			system_reflection_ResourceAttributes_buildin.regNativeFunctions(bin);
			system_reflection_TypeAttributes_buildin.regNativeFunctions(bin);
			system_runtime_serialization_StreamingContextStates_buildin.regNativeFunctions(bin);
			system_globalization_CalendarAlgorithmType_buildin.regNativeFunctions(bin);
			system_globalization_CalendarWeekRule_buildin.regNativeFunctions(bin);
			system_globalization_CompareOptions_buildin.regNativeFunctions(bin);
			system_globalization_CultureTypes_buildin.regNativeFunctions(bin);
			system_globalization_DateTimeStyles_buildin.regNativeFunctions(bin);
			system_globalization_DigitShapes_buildin.regNativeFunctions(bin);
			system_globalization_GregorianCalendarTypes_buildin.regNativeFunctions(bin);
			system_globalization_NumberStyles_buildin.regNativeFunctions(bin);
			system_globalization_UnicodeCategory_buildin.regNativeFunctions(bin);
			system_text_NormalizationForm_buildin.regNativeFunctions(bin);
			system_resources_UltimateResourceFallbackLocation_buildin.regNativeFunctions(bin);
			system_security_policy_ApplicationVersionMatch_buildin.regNativeFunctions(bin);
			system_security_policy_TrustManagerUIContext_buildin.regNativeFunctions(bin);
			system_security_policy_PolicyStatementAttribute_buildin.regNativeFunctions(bin);
			system_security_principal_PrincipalPolicy_buildin.regNativeFunctions(bin);
			system_security_principal_WindowsAccountType_buildin.regNativeFunctions(bin);
			system_security_principal_TokenImpersonationLevel_buildin.regNativeFunctions(bin);
			system_security_principal_TokenAccessLevels_buildin.regNativeFunctions(bin);
			system_security_principal_WindowsBuiltInRole_buildin.regNativeFunctions(bin);
			system_runtime_constrainedexecution_Consistency_buildin.regNativeFunctions(bin);
			system_runtime_constrainedexecution_Cer_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_ComInterfaceType_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_ClassInterfaceType_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_IDispatchImplType_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_TypeLibTypeFlags_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_TypeLibFuncFlags_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_TypeLibVarFlags_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_VarEnum_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_UnmanagedType_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_CallingConvention_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_CharSet_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_GCHandleType_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_LayoutKind_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_ComMemberType_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_AssemblyRegistrationFlags_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_TypeLibImporterFlags_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_TypeLibExporterFlags_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_ImporterEventKind_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_ExporterEventKind_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_RegistrationClassContext_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_RegistrationConnectionType_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_DESCKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_TYPEKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_TYPEFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_IMPLTYPEFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_IDLFLAG_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_PARAMFLAG_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_FUNCKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_INVOKEKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_CALLCONV_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_FUNCFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_VARFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_SYSKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_LIBFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_DESCKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_TYPEKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_TYPEFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_IMPLTYPEFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_IDLFLAG_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_PARAMFLAG_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_VARKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_FUNCKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_INVOKEKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_CALLCONV_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_FUNCFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_VARFLAGS_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_SYSKIND_buildin.regNativeFunctions(bin);
			system_runtime_interopservices_comtypes_LIBFLAGS_buildin.regNativeFunctions(bin);
			system_io_SearchOption_buildin.regNativeFunctions(bin);
			system_io_DriveType_buildin.regNativeFunctions(bin);
			system_io_FileAccess_buildin.regNativeFunctions(bin);
			system_io_FileMode_buildin.regNativeFunctions(bin);
			system_io_FileOptions_buildin.regNativeFunctions(bin);
			system_io_FileShare_buildin.regNativeFunctions(bin);
			system_io_FileAttributes_buildin.regNativeFunctions(bin);
			system_io_SeekOrigin_buildin.regNativeFunctions(bin);
			system_runtime_compilerservices_CompilationRelaxations_buildin.regNativeFunctions(bin);
			system_runtime_compilerservices_MethodImplOptions_buildin.regNativeFunctions(bin);
			system_runtime_compilerservices_MethodCodeType_buildin.regNativeFunctions(bin);
			system_runtime_compilerservices_LoadHint_buildin.regNativeFunctions(bin);
			system_runtime_GCLatencyMode_buildin.regNativeFunctions(bin);
			system_security_permissions_EnvironmentPermissionAccess_buildin.regNativeFunctions(bin);
			system_security_permissions_FileDialogPermissionAccess_buildin.regNativeFunctions(bin);
			system_security_permissions_FileIOPermissionAccess_buildin.regNativeFunctions(bin);
			system_security_permissions_HostProtectionResource_buildin.regNativeFunctions(bin);
			system_security_permissions_IsolatedStorageContainment_buildin.regNativeFunctions(bin);
			system_security_permissions_PermissionState_buildin.regNativeFunctions(bin);
			system_security_permissions_SecurityAction_buildin.regNativeFunctions(bin);
			system_security_permissions_ReflectionPermissionFlag_buildin.regNativeFunctions(bin);
			system_security_permissions_SecurityPermissionFlag_buildin.regNativeFunctions(bin);
			system_security_permissions_UIPermissionWindow_buildin.regNativeFunctions(bin);
			system_security_permissions_UIPermissionClipboard_buildin.regNativeFunctions(bin);
			system_security_permissions_KeyContainerPermissionFlags_buildin.regNativeFunctions(bin);
			system_security_permissions_RegistryPermissionAccess_buildin.regNativeFunctions(bin);
			system_security_SecurityCriticalScope_buildin.regNativeFunctions(bin);
			system_security_HostSecurityManagerOptions_buildin.regNativeFunctions(bin);
			system_security_PolicyLevelType_buildin.regNativeFunctions(bin);
			system_security_SecurityZone_buildin.regNativeFunctions(bin);
			system_runtime_remoting_WellKnownObjectMode_buildin.regNativeFunctions(bin);
			system_runtime_remoting_activation_ActivatorLevel_buildin.regNativeFunctions(bin);
			system_runtime_remoting_channels_ServerProcessing_buildin.regNativeFunctions(bin);
			system_runtime_remoting_lifetime_LeaseState_buildin.regNativeFunctions(bin);
			system_runtime_remoting_metadata_SoapOption_buildin.regNativeFunctions(bin);
			system_runtime_remoting_metadata_XmlFieldOrderOption_buildin.regNativeFunctions(bin);
			system_runtime_remoting_CustomErrorsModes_buildin.regNativeFunctions(bin);
			system_io_isolatedstorage_IsolatedStorageScope_buildin.regNativeFunctions(bin);
			system_runtime_serialization_formatters_FormatterTypeStyle_buildin.regNativeFunctions(bin);
			system_runtime_serialization_formatters_FormatterAssemblyStyle_buildin.regNativeFunctions(bin);
			system_runtime_serialization_formatters_TypeFilterLevel_buildin.regNativeFunctions(bin);
			system_reflection_emit_AssemblyBuilderAccess_buildin.regNativeFunctions(bin);
			system_reflection_emit_PEFileKinds_buildin.regNativeFunctions(bin);
			system_reflection_emit_OpCodeType_buildin.regNativeFunctions(bin);
			system_reflection_emit_StackBehaviour_buildin.regNativeFunctions(bin);
			system_reflection_emit_OperandType_buildin.regNativeFunctions(bin);
			system_reflection_emit_FlowControl_buildin.regNativeFunctions(bin);
			system_reflection_emit_PackingSize_buildin.regNativeFunctions(bin);
			system_configuration_assemblies_AssemblyHashAlgorithm_buildin.regNativeFunctions(bin);
			system_configuration_assemblies_AssemblyVersionCompatibility_buildin.regNativeFunctions(bin);
			system_security_cryptography_CipherMode_buildin.regNativeFunctions(bin);
			system_security_cryptography_PaddingMode_buildin.regNativeFunctions(bin);
			system_security_cryptography_FromBase64TransformMode_buildin.regNativeFunctions(bin);
			system_security_cryptography_CspProviderFlags_buildin.regNativeFunctions(bin);
			system_security_cryptography_CryptoStreamMode_buildin.regNativeFunctions(bin);
			system_security_cryptography_KeyNumber_buildin.regNativeFunctions(bin);
			system_security_cryptography_x509certificates_X509ContentType_buildin.regNativeFunctions(bin);
			system_security_cryptography_x509certificates_X509KeyStorageFlags_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AceType_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AceFlags_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_CompoundAceType_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AceQualifier_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_ObjectAceFlags_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_CryptoKeyRights_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_InheritanceFlags_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_PropagationFlags_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AuditFlags_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_SecurityInfos_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_ResourceType_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AccessControlSections_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AccessControlActions_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_EventWaitHandleRights_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_FileSystemRights_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_MutexRights_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AccessControlModification_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_RegistryRights_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_AccessControlType_buildin.regNativeFunctions(bin);
			system_security_accesscontrol_ControlFlags_buildin.regNativeFunctions(bin);
			system_security_principal_WellKnownSidType_buildin.regNativeFunctions(bin);
			system_runtime_versioning_ResourceScope_buildin.regNativeFunctions(bin);

		}

	}
}
