using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_security_cryptography_x509certificates_X509ContentType_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_security_cryptography_x509certificates_X509ContentType_creator", default(System.Security.Cryptography.X509Certificates.X509ContentType)));
			bin.regNativeFunction(new system_security_cryptography_x509certificates_X509ContentType_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_Unknown_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.Unknown;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_Cert_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.Cert;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_SerializedCert_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_Pfx_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.Pfx;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_Pkcs12_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_SerializedStore_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.SerializedStore;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_Pkcs7_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs7;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_security_cryptography_x509certificates_X509ContentType_Authenticode_getter",()=>{ return System.Security.Cryptography.X509Certificates.X509ContentType.Authenticode;}));
			bin.regNativeFunction(new system_security_cryptography_x509certificates_X509ContentType_operator_bitOr());
		}

		class system_security_cryptography_x509certificates_X509ContentType_ctor : NativeFunctionBase
		{
			public system_security_cryptography_x509certificates_X509ContentType_ctor()
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
					return "system_security_cryptography_x509certificates_X509ContentType_ctor";
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
				return ASBinCode.rtData.rtUndefined.undefined;

			}
		}

		class system_security_cryptography_x509certificates_X509ContentType_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_security_cryptography_x509certificates_X509ContentType_operator_bitOr() : base(2)
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
					return "system_security_cryptography_x509certificates_X509ContentType_operator_bitOr";
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
				System.Security.Cryptography.X509Certificates.X509ContentType ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.Security.Cryptography.X509Certificates.X509ContentType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.Security.Cryptography.X509Certificates.X509ContentType)argObj.value;
				}

				System.Security.Cryptography.X509Certificates.X509ContentType ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.Security.Cryptography.X509Certificates.X509ContentType);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.Security.Cryptography.X509Certificates.X509ContentType)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
