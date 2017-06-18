using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ASCTest.regNativeFunctions
{
    class system_collections_queue_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator("_system_collections_queue_creator_", 
                default(Queue)));// new system_int64_creator());
            bin.regNativeFunction(new system_collections_queue_ctor());
            bin.regNativeFunction(new system_collections_queue_static_createinstance());
            bin.regNativeFunction(new system_collections_queue_static_createinstance_());
            bin.regNativeFunction(new system_collections_queue_clear());
            bin.regNativeFunction(new system_collections_queue_contains());
            bin.regNativeFunction(new system_collections_queue_dequeue());
            bin.regNativeFunction(new system_collections_queue_enqueue());
            bin.regNativeFunction(new system_collections_queue_peek());
            bin.regNativeFunction(new system_collections_queue_toarray());
            bin.regNativeFunction(new system_collections_queue_trimtosize());
        }


        class system_collections_queue_ctor : NativeConstParameterFunction
        {
            public system_collections_queue_ctor() : base(0)
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
                    return "_system_collections_queue_ctor_";
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
                ((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value
                    = new Queue();
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
                success = true;
            }


        }
        class system_collections_queue_static_createinstance : NativeConstParameterFunction
        {
            public system_collections_queue_static_createinstance() : base(1)
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
                    return "_system_collections_queue_static_createInstance";
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


                Class _arraylist_ = ((ASBinCode.rtData.rtObject)thisObj).value._class;

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
                        linkobj.SetLinkData(new Queue((ICollection)lo));

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
                    throw new ASRunTimeException();
                }


            }

        }
        class system_collections_queue_static_createinstance_ : NativeConstParameterFunction
        {
            public system_collections_queue_static_createinstance_() : base(1)
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
                    return "_system_collections_queue_static_createInstance_";
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


                Class _arraylist_ = ((ASBinCode.rtData.rtObject)thisObj).value._class;

                var arr = stackframe.player.alloc_pureHostedOrLinkedObject(_arraylist_.instanceClass);

                LinkSystemObject linkobj = (LinkSystemObject)arr.value;


                try
                {
                    int initCapacity = TypeConverter.ConvertToInt(argements[0], stackframe, token);

                    linkobj.SetLinkData(new Queue(initCapacity));

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
                    throw new ASRunTimeException();
                }


            }

        }
        class system_collections_queue_clear : NativeConstParameterFunction
        {
            public system_collections_queue_clear() : base(0)
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
                    return "system_collections_queue_clear";
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



                System.Collections.Queue queue =
                    (System.Collections.Queue)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

                try
                {
                    queue.Clear();
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
        class system_collections_queue_contains : NativeConstParameterFunction
        {
            public system_collections_queue_contains() : base(1)
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
                    return "_system_collections_queue_contains_";
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
                    return RunTimeDataType.rt_boolean;
                }
            }

            public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
            {

                Queue queue =
                    (System.Collections.Queue)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

                try
                {
                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],

                        stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                        ,
                        bin, true, out lo
                        ))
                    {
                        if (queue.Contains(lo))
                        {
                            returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                        }
                        else
                        {
                            returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                        }
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
                catch (NotSupportedException n)
                {
                    success = false;
                    stackframe.throwAneException(token, n.Message);
                }

            }
        }
        class system_collections_queue_dequeue : NativeConstParameterFunction
        {
            public system_collections_queue_dequeue() : base(0)
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
                    return "_system_collections_queue_dequeue_";
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



                System.Collections.Queue queue =
                    (System.Collections.Queue)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

                try
                {
                    object obj = queue.Dequeue();

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;
                }
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token,  "Queue内的值没有链接到脚本");
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
                catch (InvalidOperationException io)
                {
                    success = false;
                    stackframe.throwAneException(token, io.Message);
                }

            }


        }
        class system_collections_queue_enqueue : NativeConstParameterFunction
        {
            public system_collections_queue_enqueue() : base(1)
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
                    return "_system_collections_queue_enqueue_";
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

                Queue queue =
                    (System.Collections.Queue)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

                try
                {
                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],

                        stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                        ,
                        bin, true, out lo
                        ))
                    {
                        queue.Enqueue(lo);

                        returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

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
                catch (NotSupportedException n)
                {
                    success = false;
                    stackframe.throwAneException(token, n.Message);
                }

            }
        }
        class system_collections_queue_peek : NativeConstParameterFunction
        {
            public system_collections_queue_peek() : base(0)
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
                    return "_system_collections_queue_peek_";
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



                System.Collections.Queue queue =
                    (System.Collections.Queue)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

                try
                {
                    object obj = queue.Peek();

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;
                }
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token,   "Queue内的值没有链接到脚本");
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
                catch (InvalidOperationException io)
                {
                    success = false;
                    stackframe.throwAneException(token, io.Message);
                }

            }


        }
        class system_collections_queue_toarray : NativeConstParameterFunction
        {
            public system_collections_queue_toarray() : base(0)
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
                    return "system_collections_queue_toarray";
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



                System.Collections.Queue queue =
                    (System.Collections.Queue)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

                try
                {
                    object obj = queue.ToArray();

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;
                }
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token,  "Queue.toArray()的结果没有链接到脚本");
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
                catch (InvalidOperationException io)
                {
                    success = false;
                    stackframe.throwAneException(token, io.Message);
                }

            }


        }
        class system_collections_queue_trimtosize : NativeConstParameterFunction
        {
            public system_collections_queue_trimtosize() : base(0)
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
                    return "system_collections_queue_trimtosize";
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



                System.Collections.Queue queue =
                    (System.Collections.Queue)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

                try
                {
                    queue.TrimToSize();
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
