using ASBinCode;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASRuntime;

namespace ASCTest.regNativeFunctions
{
    class system_sbyte_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
               LinkSystem_Buildin.getCreator("_system_SByte_creator__", default(SByte)));
            bin.regNativeFunction(new system_sbyte_ctor());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<SByte>("_system_SByte_MaxValue_getter"
                ,
                () => { return SByte.MaxValue; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<SByte>("_system_SByte_MinValue_getter"
                ,
                () => { return SByte.MinValue; }
                )
                );
            bin.regNativeFunction(new system_sbyte_explicit_from());
            bin.regNativeFunction(new system_sbyte_implicit_from());
            bin.regNativeFunction(new system_sbyte_static_Parse());
            bin.regNativeFunction(new system_sbyte_valueOf());
            bin.regNativeFunction(new system_sbyte_toString_());
        }
    }

    class system_sbyte_ctor : NativeConstParameterFunction
    {
        public system_sbyte_ctor() : base(1)
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
                return "_system_SByte_ctor";
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

            ((LinkObj<SByte>)((ASBinCode.rtData.rtObject)thisObj).value).value = (SByte)value;
            returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
            success = true;
        }


    }

    class system_sbyte_explicit_from : NativeConstParameterFunction
    {
        public system_sbyte_explicit_from() : base(1)
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
                return "_system_SByte_explicit_from_";
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

            LinkObj<sbyte> obj =
                (LinkObj<sbyte>)(v.value);

            obj.value = (sbyte)TypeConverter.ConvertToInt(argements[0]);

            ((StackSlot)returnSlot).setLinkObjectValue(obj._class, stackframe.player, obj.value);

            success = true;
        }


    }

    class system_sbyte_implicit_from : NativeConstParameterFunction
    {
        public system_sbyte_implicit_from() : base(1)
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
                return "_system_SByte_implicit_from_";
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

            LinkObj<sbyte> obj =
                (LinkObj<sbyte>)(v.value);

            obj.value = (sbyte)TypeConverter.ConvertToInt(argements[0]);

            ((StackSlot)returnSlot).setLinkObjectValue(obj._class, stackframe.player, obj.value);

            success = true;
        }


    }

    sealed class system_sbyte_static_Parse : NativeConstParameterFunction
    {
        public system_sbyte_static_Parse() : base(1)
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
                return "_system_SByte_static_parse";
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
            sbyte v = sbyte.Parse(ASRuntime.TypeConverter.ConvertToString(argements[0], null, null));

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

    class system_sbyte_valueOf : NativeConstParameterFunction
    {
        public system_sbyte_valueOf() : base(0)
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
                return "_system_SByte_valueOf";
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
            LinkObj<sbyte> obj = ((LinkObj<sbyte>)((ASBinCode.rtData.rtObject)thisObj).value);

            returnSlot.setValue((double)obj.value);
            success = true;
        }

    }
    class system_sbyte_toString_ : NativeConstParameterFunction
    {
        public system_sbyte_toString_() : base(1)
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
                return "_system_SByte_toString_";
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


            LinkObj<sbyte> obj = ((LinkObj<sbyte>)((ASBinCode.rtData.rtObject)thisObj).value);

            returnSlot.setValue(obj.value.ToString(format));
            success = true;
        }

    }

}
