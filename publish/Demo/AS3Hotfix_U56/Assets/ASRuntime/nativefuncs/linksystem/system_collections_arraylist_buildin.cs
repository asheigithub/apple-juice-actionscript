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
    class system_collections_arraylist_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator("_system_ArrayList_creator_", default(ArrayList)));// new system_int64_creator());
            bin.regNativeFunction(new system_collections_arraylist_ctor());
            bin.regNativeFunction(new system_collections_arraylist_static_createinstance());
            bin.regNativeFunction(new system_collections_arraylist_static_createinstance_());
            bin.regNativeFunction(new system_collections_arraylist_capcaity());
            bin.regNativeFunction(new system_collections_arraylist_addrange());
            bin.regNativeFunction(new system_collections_arraylist_copyto());
            bin.regNativeFunction(new system_collections_arraylist_copyto_());
            bin.regNativeFunction(new system_collections_arraylist_reverse());
            bin.regNativeFunction(new system_collections_arraylist_sort());
        }

        class system_collections_arraylist_ctor : NativeConstParameterFunction
        {
            public system_collections_arraylist_ctor() : base(0)
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
                    return "_system_ArrayList_ctor_";
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
                    = new ArrayList();
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
                success = true;
            }


        }
        class system_collections_arraylist_static_createinstance : NativeConstParameterFunction
        {
            public system_collections_arraylist_static_createinstance() : base(1)
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
                    return "_system_collections_ArrayList_static_createInstance";
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
               
                if (argements[0].rtType == RunTimeDataType.rt_null)
                {
                    success = false;
                    stackframe.throwArgementException(token, "参数elementType不能为null");

                    return;
                }

                
                Class _arraylist_ = ((ASBinCode.rtData.rtObjectBase)thisObj).value._class;

                var arr = stackframe.player.alloc_pureHostedOrLinkedObject(_arraylist_.instanceClass);

                LinkSystemObject linkobj = (LinkSystemObject)arr.value;


                try
                {
                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],

                        stackframe.player.linktypemapper.getLinkType(
                            functionDefine.signature.parameters[0].type
                            ),

                        bin, true, out lo
                        ))
                    {
                        linkobj.SetLinkData(new ArrayList( (ICollection) lo));

                        returnSlot.directSet(arr);

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
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwArgementException(token, "类型" + argements[0].rtType + "不是一个链接到系统类库的对象，不能创建托管数组");
                }
                catch (ArgumentOutOfRangeException a)
                {
                    success = false;
                    stackframe.throwAneException(token, a.Message);

                }
                catch
                {
                    throw new EngineException();
                }


            }

        }
        class system_collections_arraylist_static_createinstance_ : NativeConstParameterFunction
        {
            public system_collections_arraylist_static_createinstance_() : base(1)
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
                    return "_system_collections_ArrayList_static_createInstance_";
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


                Class _arraylist_ = ((ASBinCode.rtData.rtObjectBase)thisObj).value._class;

                var arr = stackframe.player.alloc_pureHostedOrLinkedObject(_arraylist_.instanceClass);

                LinkSystemObject linkobj = (LinkSystemObject)arr.value;


                try
                {
                    int capacity = TypeConverter.ConvertToInt(argements[0]);

                    linkobj.SetLinkData(new ArrayList(capacity));

                    returnSlot.directSet(arr);

                    success = true;
                   

                }
                //catch (KeyNotFoundException)
                //{
                //    success = false;
                //    stackframe.throwArgementException(token, "类型" + argements[0].rtType + "不是一个链接到系统类库的对象，不能创建托管数组");
                //}
                catch (ArgumentOutOfRangeException a)
                {
                    success = false;
                    stackframe.throwAneException(token, a.Message);

                }
                catch
                {
                    throw new EngineException();
                }


            }

        }
        class system_collections_arraylist_capcaity : NativeConstParameterFunction
        {
            public system_collections_arraylist_capcaity() : base(0)
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
                    return "system_collections_arraylist_capcaity";
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
                    return RunTimeDataType.rt_int;
                }
            }

            public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
            {



                System.Collections.ArrayList arraylist =
                    (System.Collections.ArrayList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    returnSlot.setValue((int)arraylist.Capacity);
                    success = true;
                }
                //catch (KeyNotFoundException)
                //{
                //    success = false;
                //    stackframe.throwAneException(token, arraylist.ToString() + "没有链接到脚本");
                //}
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
        class system_collections_arraylist_addrange : NativeConstParameterFunction
        {
            public system_collections_arraylist_addrange() : base(1)
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
                    return "system_collections_arraylist_addrange";
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



                System.Collections.ArrayList arraylist =
                    (System.Collections.ArrayList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],

                        stackframe.player.linktypemapper.getLinkType(
                            functionDefine.signature.parameters[0].type
                            ),

                        bin, true, out lo
                        ))
                    {
                        arraylist.AddRange((ICollection)lo);

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
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token, functionDefine.signature.parameters[0].type + "没有链接到脚本");
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
        class system_collections_arraylist_copyto : NativeConstParameterFunction
        {
            public system_collections_arraylist_copyto() : base(1)
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
                    return "system_collections_arraylist_copyto";
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



                System.Collections.ArrayList arraylist =
                    (System.Collections.ArrayList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],

                        stackframe.player.linktypemapper.getLinkType(
                            functionDefine.signature.parameters[0].type
                            ),

                        bin, true, out lo
                        ))
                    {
                        arraylist.CopyTo((Array)lo);

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
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token, functionDefine.signature.parameters[0].type + "没有链接到脚本");
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
        class system_collections_arraylist_copyto_ : NativeConstParameterFunction
        {
            public system_collections_arraylist_copyto_() : base(4)
            {
                para = new List<RunTimeDataType>();
                para.Add(RunTimeDataType.rt_int);
                para.Add(RunTimeDataType.rt_void);
                para.Add(RunTimeDataType.rt_int);
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
                    return "system_collections_arraylist_copyto_";
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



                ArrayList arraylist =
                    (ArrayList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    int index = TypeConverter.ConvertToInt(argements[0]);
                    int arrayIndex = TypeConverter.ConvertToInt(argements[2]);
                    int count = TypeConverter.ConvertToInt(argements[3]);


                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[1],

                        stackframe.player.linktypemapper.getLinkType(
                            functionDefine.signature.parameters[1].type
                            ),

                        bin, true, out lo
                        ))
                    {
                        arraylist.CopyTo(index,(Array)lo,arrayIndex,count);

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
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token, functionDefine.signature.parameters[0].type + "没有链接到脚本");
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
        class system_collections_arraylist_reverse : NativeConstParameterFunction
        {
            public system_collections_arraylist_reverse() : base(0)
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
                    return "system_collections_arraylist_reverse";
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



                System.Collections.ArrayList arraylist =
                    (System.Collections.ArrayList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    arraylist.Reverse();
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                //catch (KeyNotFoundException)
                //{
                //    success = false;
                //    stackframe.throwAneException(token, arraylist.ToString() + "没有链接到脚本");
                //}
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
        class system_collections_arraylist_sort : NativeConstParameterFunction
        {
            public system_collections_arraylist_sort() : base(0)
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
                    return "system_collections_arraylist_sort";
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



                System.Collections.ArrayList arraylist =
                    (System.Collections.ArrayList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    arraylist.Sort();
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                //catch (KeyNotFoundException)
                //{
                //    success = false;
                //    stackframe.throwAneException(token, arraylist.ToString() + "没有链接到脚本");
                //}
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
