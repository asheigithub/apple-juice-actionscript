using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs.linksystem
{
    class system_collections_hashtable_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator("_system_Hashtable_creator_", default(Hashtable)));// new system_int64_creator());
            bin.regNativeFunction(new system_collections_hashtable_ctor());

            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator(
                    "_system_DictionaryEntry_creator__", 
                    default(DictionaryEntry)));// new system_int64_creator());
            bin.regNativeFunction(new system_collections_dictionaryentry_ctor());
            bin.regNativeFunction(new system_collections_dictionaryentry_getkey());
            bin.regNativeFunction(new system_collections_dictionaryentry_setkey());
            bin.regNativeFunction(new system_collections_dictionaryentry_getvalue());
            bin.regNativeFunction(new system_collections_dictionaryentry_setvalue());
        }


        class system_collections_hashtable_ctor : NativeConstParameterFunction
        {
            public system_collections_hashtable_ctor() : base(0)
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
                    return "_system_Hashtable_ctor_";
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
                ((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value
                    = new Hashtable();
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
                success = true;
            }


        }

        class system_collections_dictionaryentry_ctor : NativeConstParameterFunction
        {
            public system_collections_dictionaryentry_ctor() : base(2)
            {
                para = new List<RunTimeDataType>();
                para.Add(RunTimeDataType._OBJECT);
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
                    return "_system_DictionaryEntry_ctor_";
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
                

                RunTimeValueBase kv = argements[0];
                if (kv.rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(
                        bin.getClassByRunTimeDataType(kv.rtType), out ot
                        ))
                    {
                        kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)kv);
                    }
                }
                
                try
                {

                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[1],

                        stackframe.player.linktypemapper.getLinkType(argements[1].rtType),

                        bin, true, out lo
                        ))
                    {
                        ((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value
                            = new DictionaryEntry(kv,lo);

                        returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                        success = true;
                    }
                    else
                    {
                        stackframe.throwCastException(token, argements[1].rtType,
                            functionDefine.signature.parameters[1].type
                            );
                        success = false;
                    }

                }
                catch (InvalidCastException ic)
                {
                    success = false;
                    stackframe.throwAneException(token, ic.Message);
                }
                catch (ArgumentException a)
                {
                    success = false;
                    stackframe.throwAneException(token, a.Message);
                }
                catch (IndexOutOfRangeException i)
                {
                    success = false;
                    stackframe.throwAneException(token, i.Message);
                }

                
            }


        }

        class system_collections_dictionaryentry_getkey : NativeConstParameterFunction
        {
            public system_collections_dictionaryentry_getkey() : base(0)
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
                    return "_system_DictionaryEntry_getkey_";
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
                DictionaryEntry dictionaryEntry =
                ((LinkObj<DictionaryEntry>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;


                try
                {

                    object obj = dictionaryEntry.Key;

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;

                }
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
                {
                    success = false;
                    stackframe.throwAneException(token, ic.Message);
                }
                catch (ArgumentException a)
                {
                    success = false;
                    stackframe.throwAneException(token, a.Message);
                }
                catch (IndexOutOfRangeException i)
                {
                    success = false;
                    stackframe.throwAneException(token, i.Message);
                }
                
            }


        }

        class system_collections_dictionaryentry_setkey : NativeConstParameterFunction
        {
            public system_collections_dictionaryentry_setkey() : base(1)
            {
                para = new List<RunTimeDataType>();
                para.Add(RunTimeDataType._OBJECT);
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
                    return "_system_DictionaryEntry_setkey_";
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
                    return RunTimeDataType.fun_void;
                }
            }

            public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
            {
                
                RunTimeValueBase kv = argements[0];
                if (kv.rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(
                        bin.getClassByRunTimeDataType(kv.rtType), out ot
                        ))
                    {
                        kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)kv);
                    }
                }
                DictionaryKey key = new DictionaryKey(kv,true);

                try
                {

                  //链接结构体，不能缓存到变量，必须直接对其赋值....
                    (((LinkObj<DictionaryEntry>)
                        ((ASBinCode.rtData.rtObjectBase)thisObj).value).value).Key=key;


                    returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;

                }
                catch (InvalidCastException ic)
                {
                    success = false;
                    stackframe.throwAneException(token, ic.Message);
                }
                catch (ArgumentException a)
                {
                    success = false;
                    stackframe.throwAneException(token, a.Message);
                }
                catch (IndexOutOfRangeException i)
                {
                    success = false;
                    stackframe.throwAneException(token, i.Message);
                }
                
            }


        }

        class system_collections_dictionaryentry_getvalue : NativeConstParameterFunction
        {
            public system_collections_dictionaryentry_getvalue() : base(0)
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
                    return "_system_DictionaryEntry_getvalue_";
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
                DictionaryEntry dictionaryEntry =
                ((LinkObj<DictionaryEntry>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;


                try
                {

                    object obj = dictionaryEntry.Value;

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;

                }
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
                {
                    success = false;
                    stackframe.throwAneException(token, ic.Message);
                }
                catch (ArgumentException a)
                {
                    success = false;
                    stackframe.throwAneException(token, a.Message);
                }
                catch (IndexOutOfRangeException i)
                {
                    success = false;
                    stackframe.throwAneException(token, i.Message);
                }

            }


        }

        class system_collections_dictionaryentry_setvalue : NativeConstParameterFunction
        {
            public system_collections_dictionaryentry_setvalue() : base(1)
            {
                para = new List<RunTimeDataType>();
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
                    return "_system_DictionaryEntry_setvalue_";
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
                    return RunTimeDataType.fun_void;
                }
            }

            public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
            {

                try
                {
                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],

                        stackframe.player.linktypemapper.getLinkType(argements[0].rtType),

                        bin, true, out lo
                        ))
                    {

                        //链接结构体，不能缓存到变量，必须直接对其赋值....
                        (((LinkObj<DictionaryEntry>)
                            ((ASBinCode.rtData.rtObjectBase)thisObj).value).value).Value = lo;


                        returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                        success = true;
                    }
                    else
                    {
                        stackframe.throwCastException(token, argements[0].rtType,
                            functionDefine.signature.parameters[0].type
                            );
                        success = false;
                    }


                }
                catch (InvalidCastException ic)
                {
                    success = false;
                    stackframe.throwAneException(token, ic.Message);
                }
                catch (ArgumentException a)
                {
                    success = false;
                    stackframe.throwAneException(token, a.Message);
                }
                catch (IndexOutOfRangeException i)
                {
                    success = false;
                    stackframe.throwAneException(token, i.Message);
                }

            }


        }


    }
}
