using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_char_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator("_system_Char_creator__", default(Char)));
            bin.regNativeFunction(new system_char_ctor());
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<Char>("_system_Char_MaxValue_getter"
                ,
                () => { return char.MaxValue; }
                )
                );
            bin.regNativeFunction(
                LinkSystem_Buildin.getStruct_static_field_getter<Char>("_system_Char_MinValue_getter"
                ,
                () => { return char.MinValue; }
                )
                );
            bin.regNativeFunction(new system_char_explicit_from());
            bin.regNativeFunction(new system_char_valueOf());
        }
    }


    class system_char_ctor : NativeConstParameterFunction
    {
        public system_char_ctor() : base(1)
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
                return "_system_Char_ctor";
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

            ((LinkObj<Char>)((ASBinCode.rtData.rtObject)thisObj).value).value = (Char)value;
            returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
            success = true;
        }


    }
    class system_char_explicit_from : NativeConstParameterFunction
    {
        public system_char_explicit_from() : base(1)
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
                return "_system_Char_explicit_from_";
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

            LinkObj<Char> obj =
                (LinkObj<Char>)(v.value);

            obj.value = (Char)TypeConverter.ConvertToInt(argements[0]);

            ((StackSlot)returnSlot).setLinkObjectValue(obj._class, stackframe.player, obj.value);

            success = true;
        }


    }
    class system_char_valueOf : NativeConstParameterFunction
    {
        public system_char_valueOf() : base(0)
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
                return "_system_Char_valueOf";
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
            LinkObj<char> obj = ((LinkObj<char>)((ASBinCode.rtData.rtObject)thisObj).value);

            returnSlot.setValue((double)obj.value);
            success = true;
        }

    }


}
