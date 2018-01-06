using ASBinCode;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASRuntime;

namespace ASCTest.regNativeFunctions
{
    class system_byte_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator("_system_Byte_creator__", default(Byte)));

            bin.regNativeFunction(new system_byte_ctor());
            bin.regNativeFunction(new system_byte_explicit_from());
            bin.regNativeFunction(new system_byte_implicit_from());
            bin.regNativeFunction(new system_byte_static_Parse());
            bin.regNativeFunction(new system_byte_valueOf());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<byte>("_system_Byte_MaxValue_getter"
                ,
                () => { return byte.MaxValue; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<byte>("_system_Byte_MinValue_getter"
                ,
                () => { return byte.MinValue; }
                )
                );
            bin.regNativeFunction(new system_byte_toString_());

        }
    }

    class system_byte_ctor : NativeConstParameterFunction
    {
        public system_byte_ctor():base(1)
        {
            para = new List<RunTimeDataType>();
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
                return "_system_Byte_ctor";
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
            int value = TypeConverter.ConvertToInt(argements[0]);

            ((LinkObj<Byte>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = (Byte)value;
            returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
            success = true;
        }

        
    }

    class system_byte_explicit_from : NativeConstParameterFunction
    {
        public system_byte_explicit_from():base(1)
        {
            para = new List<RunTimeDataType>();
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
                return "_system_Byte_explicit_from_";
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
			//var v = (stackframe.player.alloc_pureHostedOrLinkedObject(((ASBinCode.rtData.rtObjectBase)thisObj).value._class.instanceClass));
			var cls = ((ASBinCode.rtData.rtObjectBase)thisObj).value._class.instanceClass;

			//LinkObj<byte> obj =
   //             (LinkObj<byte>)(v.value);

            var value = (byte)TypeConverter.ConvertToInt(argements[0]);

            ((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player, value);

            success = true;
        }

        
    }

    class system_byte_implicit_from : NativeConstParameterFunction
    {
        public system_byte_implicit_from() : base(1)
        {
            para = new List<RunTimeDataType>();
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
                return "_system_Byte_implicit_from_";
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
			//var v = (stackframe.player.alloc_pureHostedOrLinkedObject(((ASBinCode.rtData.rtObjectBase)thisObj).value._class.instanceClass));
			var cls = ((ASBinCode.rtData.rtObjectBase)thisObj).value._class.instanceClass;

			//LinkObj<byte> obj =
			//             (LinkObj<byte>)(v.value);

			var value = (byte)TypeConverter.ConvertToInt(argements[0]);

			((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player, value);

			success = true;
		}

        
    }

    sealed class system_byte_static_Parse : NativeConstParameterFunction
    {
        public system_byte_static_Parse():base(1)
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
                return "_system_Byte_static_parse";
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
            byte v = byte.Parse(ASRuntime.TypeConverter.ConvertToString(argements[0], null, null));

            ((ASRuntime.StackSlot)returnSlot)
                .setLinkObjectValue(
                bin.getClassByRunTimeDataType(
                functionDefine.signature.returnType
                )
                ,
                (stackframe).player
                , v);

            success = true;
        }

       

    }

    class system_byte_valueOf : NativeConstParameterFunction
    {
        public system_byte_valueOf():base(0)
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
                return "_system_Byte_valueOf";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            LinkObj<byte> obj = ((LinkObj<byte>)((ASBinCode.rtData.rtObjectBase)thisObj).value);

            returnSlot.setValue((double)obj.value);
            success = true;
        }
        
    }

    class system_byte_toString_ : NativeConstParameterFunction
    {
        public system_byte_toString_() : base(1)
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
                return "_system_Byte_toString_";
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
                return RunTimeDataType.rt_string;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            string format = TypeConverter.ConvertToString(argements[0], stackframe, token);


            LinkObj<byte> obj = ((LinkObj<byte>)((ASBinCode.rtData.rtObjectBase)thisObj).value);

            returnSlot.setValue(obj.value.ToString(format));
            success = true;
        }

    }

}
