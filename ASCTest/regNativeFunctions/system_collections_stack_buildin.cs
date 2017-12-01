using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ASCTest.regNativeFunctions
{
    class system_collections_stack_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator("_system_collections_stack_creator_", default(Stack)));// new system_int64_creator());
            bin.regNativeFunction(new system_collections_stack_ctor());
            bin.regNativeFunction(new system_collections_stack_static_createinstance());
            bin.regNativeFunction(new system_collections_stack_static_createinstance_());
            bin.regNativeFunction(new system_collections_stack_clear());
            bin.regNativeFunction(new system_collections_stack_contains());
            bin.regNativeFunction(new system_collections_stack_peek());
            bin.regNativeFunction(new system_collections_stack_pop());
            bin.regNativeFunction(new system_collections_stack_push());
            bin.regNativeFunction(new system_collections_stack_toarray());
        }

        class system_collections_stack_ctor : NativeConstParameterFunction
        {
            public system_collections_stack_ctor() : base(0)
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
                    return "_system_collections_stack_ctor_";
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
                    = new Stack();
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
                success = true;
            }


        }
        class system_collections_stack_static_createinstance : NativeConstParameterFunction
        {
            public system_collections_stack_static_createinstance() : base(1)
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
                    return "_system_collections_stack_static_createInstance";
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
                        linkobj.SetLinkData(new Stack((ICollection)lo));

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
        class system_collections_stack_static_createinstance_ : NativeConstParameterFunction
        {
            public system_collections_stack_static_createinstance_() : base(1)
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
                    return "_system_collections_stack_static_createInstance_";
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
                    int initCapacity = TypeConverter.ConvertToInt(argements[0]);

                    linkobj.SetLinkData(new Stack(initCapacity));

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
        class system_collections_stack_clear : NativeConstParameterFunction
        {
            public system_collections_stack_clear() : base(0)
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
                    return "system_collections_stack_clear";
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



                System.Collections.Stack stack =
                    (System.Collections.Stack)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    stack.Clear();
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
        class system_collections_stack_contains : NativeConstParameterFunction
        {
            public system_collections_stack_contains() : base(1)
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
                    return "_system_collections_stack_contains_";
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

                Stack stack =
                    (System.Collections.Stack)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    object lo;
                    if (stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],
                         
                        stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                        ,
                        bin, false, out lo
                        ))
                    {
                        if (stack.Contains(lo))
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
        class system_collections_stack_peek : NativeConstParameterFunction
        {
            public system_collections_stack_peek() : base(0)
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
                    return "_system_collections_stack_peek_";
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



                System.Collections.Stack stack =
                    (System.Collections.Stack)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    object obj = stack.Peek();

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;
                }
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token, (stack.Peek() != null ? stack.Peek().ToString() : (stack.ToString() + ".peek()的值")) + "没有链接到脚本");
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
        class system_collections_stack_pop : NativeConstParameterFunction
        {
            public system_collections_stack_pop() : base(0)
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
                    return "_system_collections_stack_pop_";
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



                System.Collections.Stack stack =
                    (System.Collections.Stack)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    object obj = stack.Pop();

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;
                }
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token,   "Stack内的值没有链接到脚本");
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
        class system_collections_stack_push : NativeConstParameterFunction
        {
            public system_collections_stack_push() : base(1)
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
                    return "_system_collections_stack_push_";
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

                Stack stack =
                    (System.Collections.Stack)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

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
                        stack.Push(lo);

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
        class system_collections_stack_toarray : NativeConstParameterFunction
        {
            public system_collections_stack_toarray() : base(0)
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
                    return "system_collections_stack_toarray";
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



                System.Collections.Stack stack =
                    (System.Collections.Stack)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

                try
                {
                    object obj = stack.ToArray();

                    stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                    //returnSlot.setValue((int)array.GetValue(index));
                    success = true;
                }
                catch (KeyNotFoundException)
                {
                    success = false;
                    stackframe.throwAneException(token, (stack.Peek() != null ? stack.Peek().ToString() : (stack.ToString() + ".peek()的值")) + "没有链接到脚本");
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

    }
}
