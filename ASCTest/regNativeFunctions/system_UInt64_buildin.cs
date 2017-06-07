using ASBinCode;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASRuntime;

namespace ASCTest.regNativeFunctions
{
    class system_uint64_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
               LinkSystem_Buildin.getCreator("_system_UInt64_creator__", default(UInt64)));
            bin.regNativeFunction(new system_uint64_ctor());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<UInt64>("_system_UInt64_MaxValue_getter"
                ,
                () => { return UInt64.MaxValue; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<UInt64>("_system_UInt64_MinValue_getter"
                ,
                () => { return UInt64.MinValue; }
                )
                );
            bin.regNativeFunction(new system_uint64_explicit_from());
            bin.regNativeFunction(new system_uint64_implicit_from());
            bin.regNativeFunction(new system_uint64_static_Parse());
            bin.regNativeFunction(new system_uint64_valueOf());
            bin.regNativeFunction(new system_uint64_toString_());
        }
    }

    class system_uint64_ctor : NativeConstParameterFunction
    {
        public system_uint64_ctor() : base(1)
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
                return "_system_UInt64_ctor";
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
            double value = TypeConverter.ConvertToNumber(argements[0]);

            ((LinkObj<UInt64>)((ASBinCode.rtData.rtObject)thisObj).value).value = (UInt64)value;
            returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
            success = true;
        }


    }
    class system_uint64_explicit_from : NativeConstParameterFunction
    {
        public system_uint64_explicit_from() : base(1)
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
                return "_system_UInt64_explicit_from_";
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
            var v = (stackframe.player.alloc_pureHostedOrLinkedObject(((ASBinCode.rtData.rtObject)thisObj).value._class.instanceClass));

            LinkObj<UInt64> obj =
                (LinkObj<UInt64>)(v.value);

            obj.value = (UInt64)TypeConverter.ConvertToNumber(argements[0]);

            ((StackSlot)returnSlot).setLinkObjectValue(obj._class, stackframe.player, obj.value);

            success = true;
        }


    }
    class system_uint64_implicit_from : NativeConstParameterFunction
    {
        public system_uint64_implicit_from() : base(1)
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
                return "_system_UInt64_implicit_from_";
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
            var v = (stackframe.player.alloc_pureHostedOrLinkedObject(((ASBinCode.rtData.rtObject)thisObj).value._class.instanceClass));

            LinkObj<UInt64> obj =
                (LinkObj<UInt64>)(v.value);

            obj.value = (UInt64)TypeConverter.ConvertToNumber(argements[0]);

            ((StackSlot)returnSlot).setLinkObjectValue(obj._class, stackframe.player, obj.value);

            success = true;
        }


    }
    sealed class system_uint64_static_Parse : NativeConstParameterFunction
    {
        public system_uint64_static_Parse() : base(1)
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
                return "_system_UInt64_static_parse";
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
            UInt64 v = UInt64.Parse(ASRuntime.TypeConverter.ConvertToString(argements[0], null, null));

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
    class system_uint64_valueOf : NativeConstParameterFunction
    {
        public system_uint64_valueOf() : base(0)
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
                return "_system_UInt64_valueOf";
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
            LinkObj<UInt64> obj = ((LinkObj<UInt64>)((ASBinCode.rtData.rtObject)thisObj).value);

            returnSlot.setValue((double)obj.value);
            success = true;
        }

    }
    class system_uint64_toString_ : NativeConstParameterFunction
    {
        public system_uint64_toString_() : base(1)
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
                return "_system_UInt64_toString_";
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
           

            LinkObj<UInt64> obj = ((LinkObj<UInt64>)((ASBinCode.rtData.rtObject)thisObj).value);

            returnSlot.setValue(obj.value.ToString(format));
            success = true;
        }

    }


}
