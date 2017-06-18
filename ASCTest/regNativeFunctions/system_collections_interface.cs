using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_collections_interface
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_ienumerator_creator_", default(System.Collections.IEnumerator)));

            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_ienumerable_creator_", default(System.Collections.IEnumerable)));
            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_icollectinos_creator_", default(System.Collections.ICollection)));

            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_ilist_creator_", default(System.Collections.IList)));

            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_idictionary_creator_", default(System.Collections.IDictionary)));

            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator(
                    "system_collections_idictionaryenumerator_creator_",
                    default(System.Collections.IDictionaryEnumerator)
                    )
                );

            bin.regNativeFunction(new system_collections_ienumerable_getenumerator_());
            bin.regNativeFunction(new system_collections_ienumerator_reset());
            bin.regNativeFunction(new system_collections_ienumerator_movenext());
            bin.regNativeFunction(new system_collections_ienumerator_current());
            bin.regNativeFunction(new system_collections_icollection_count());
            bin.regNativeFunction(new system_collections_icollection_copyto());
            bin.regNativeFunction(new system_collections_ilist_getthisitem());
            bin.regNativeFunction(new system_collections_ilist_setthisitem());
            bin.regNativeFunction(new system_collections_ilist_isfixedsize());
            bin.regNativeFunction(new system_collections_ilist_isreadonly());
            bin.regNativeFunction(new system_collections_ilist_add());
            bin.regNativeFunction(new system_collections_ilist_clear());
            bin.regNativeFunction(new system_collections_ilist_contains());
            bin.regNativeFunction(new system_collections_ilist_indexof());
            bin.regNativeFunction(new system_collections_ilist_insert());
            bin.regNativeFunction(new system_collections_ilist_remove());
            bin.regNativeFunction(new system_collections_ilist_removeAt());

            bin.regNativeFunction(new system_collections_idictionary_isfixedsize());
            bin.regNativeFunction(new system_collections_idictionary_isreadonly());
            bin.regNativeFunction(new system_collections_idictionary_getthisitem());
            bin.regNativeFunction(new system_collections_idictionary_setthisitem());
            bin.regNativeFunction(new system_collections_idictionary_keys());
            bin.regNativeFunction(new system_collections_idictionary_values());
            bin.regNativeFunction(new system_collections_idictionary_add());
            bin.regNativeFunction(new system_collections_idictionary_clear());
            bin.regNativeFunction(new system_collections_idictionary_contains());
            bin.regNativeFunction(new system_collections_idictionary_remove());
            bin.regNativeFunction(new system_collections_idictionary_getienumerator());

            bin.regNativeFunction(new system_collections_idictonaryenumerator_entry());

        }
    }

    class system_collections_ienumerable_getenumerator_ : NativeConstParameterFunction
    {
        public system_collections_ienumerable_getenumerator_() : base(0)
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
                return "system_collections_ienumerable_getenumerator_";
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



            System.Collections.IEnumerable array =
                (System.Collections.IEnumerable)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object obj = array.GetEnumerator();

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj,functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, array.ToString() + "没有链接到脚本");
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

    class system_collections_ienumerator_reset : NativeConstParameterFunction
    {
        public system_collections_ienumerator_reset() : base(0)
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
                return "system_collections_ienumerator_reset";
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



            System.Collections.IEnumerator enumerator =
                (System.Collections.IEnumerator)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                enumerator.Reset();

                returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, enumerator.ToString() + "没有链接到脚本");
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

    class system_collections_ienumerator_movenext : NativeConstParameterFunction
    {
        public system_collections_ienumerator_movenext() : base(0)
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
                return "system_collections_ienumerator_movenext";
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



            System.Collections.IEnumerator enumerator =
                (System.Collections.IEnumerator)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                bool b= enumerator.MoveNext();

                if (b)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, enumerator.ToString() + "没有链接到脚本");
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

    class system_collections_ienumerator_current : NativeConstParameterFunction
    {
        public system_collections_ienumerator_current() : base(0)
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
                return "system_collections_ienumerator_current";
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



            System.Collections.IEnumerator enumerator =
                (System.Collections.IEnumerator)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object obj = enumerator.Current;

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, (enumerator.Current!=null?enumerator.Current.ToString():(enumerator.ToString()+".current的值")) + "没有链接到脚本");
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

    class system_collections_icollection_count : NativeConstParameterFunction
    {
        public system_collections_icollection_count() : base(0)
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
                return "system_collections_icollection_count";
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



            System.Collections.ICollection enumerator =
                (System.Collections.ICollection)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object obj = enumerator.Count;

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, enumerator.ToString() + "没有链接到脚本");
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

    class system_collections_icollection_copyto : NativeConstParameterFunction
    {
        public system_collections_icollection_copyto() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "system_collections_icollection_copyto";
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
            int index = TypeConverter.ConvertToInt(argements[1], stackframe, token);

            System.Collections.ICollection collection =
                (System.Collections.ICollection)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0], stackframe.player.linktypemapper.getLinkType(functionDefine.signature.parameters[0].type), bin, true, out lo
                    ))
                {
                    collection.CopyTo((Array)lo, index);
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

    class system_collections_ilist_getthisitem : NativeConstParameterFunction
    {
        public system_collections_ilist_getthisitem() : base(1)
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
                return "_system_collections_ilist_getThisItem_";
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


            int index = TypeConverter.ConvertToInt(argements[0], stackframe, token);

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object obj = ilist[index];
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, ilist.ToString() + "没有链接到脚本");
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

    class system_collections_ilist_setthisitem : NativeConstParameterFunction
    {
        public system_collections_ilist_setthisitem() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_collections_ilist_setThisItem_";
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
            int index = TypeConverter.ConvertToInt(argements[1], stackframe, token);

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0], 
                    
                    (ilist is Array) ? ilist.GetType().GetElementType(): 
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    , 
                    
                    bin, true, out lo
                    ))
                {
                    ilist[index]=lo;
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(       
                            ilist.GetType().GetElementType()
                            )
                            :
                            argements[0].rtType
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

    class system_collections_ilist_isfixedsize : NativeConstParameterFunction
    {
        public system_collections_ilist_isfixedsize() : base(0)
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
                return "_system_collections_ilist_isFixedSize_";
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

            

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                if (ilist.IsFixedSize)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, ilist.ToString() + "没有链接到脚本");
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

    class system_collections_ilist_isreadonly : NativeConstParameterFunction
    {
        public system_collections_ilist_isreadonly() : base(0)
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
                return "_system_collections_ilist_isReadOnly_";
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



            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                if (ilist.IsReadOnly)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, ilist.ToString() + "没有链接到脚本");
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

    class system_collections_ilist_add : NativeConstParameterFunction
    {
        public system_collections_ilist_add() : base(1)
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
                return "_system_collections_ilist_add_";
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
            
            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, true, out lo
                    ))
                {

                    returnSlot.setValue(ilist.Add(lo));
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType()):
                        argements[0].rtType
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

    class system_collections_ilist_clear : NativeConstParameterFunction
    {
        public system_collections_ilist_clear() : base(0)
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
                return "_system_collections_ilist_clear_";
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

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                ilist.Clear();
                success = true;
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
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

    class system_collections_ilist_contains : NativeConstParameterFunction
    {
        public system_collections_ilist_contains() : base(1)
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
                return "_system_collections_ilist_contains_";
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

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, true, out lo
                    ))
                {
                    if (ilist.Contains(lo))
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
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
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

    class system_collections_ilist_indexof : NativeConstParameterFunction
    {
        public system_collections_ilist_indexof() : base(1)
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
                return "_system_collections_ilist_indexOf_";
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

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, true, out lo
                    ))
                {
                    returnSlot.setValue(ilist.IndexOf(lo));
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
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

    class system_collections_ilist_insert : NativeConstParameterFunction
    {
        public system_collections_ilist_insert() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "_system_collections_ilist_insert_";
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

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                int index = TypeConverter.ConvertToInt(argements[0], stackframe, token);

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[1],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[1].rtType)
                    ,
                    bin, true, out lo
                    ))
                {
                    ilist.Insert(index, lo);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[1].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
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
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_ilist_remove : NativeConstParameterFunction
    {
        public system_collections_ilist_remove() : base(1)
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
                return "_system_collections_ilist_remove_";
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

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, true, out lo
                    ))
                {
                    ilist.Remove(lo);
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
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

    class system_collections_ilist_removeAt : NativeConstParameterFunction
    {
        public system_collections_ilist_removeAt() : base(1)
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
                return "_system_collections_ilist_removeAt_";
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

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                int index = TypeConverter.ConvertToInt(argements[0], stackframe, token);
                ilist.RemoveAt(index);
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
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_idictionary_isfixedsize : NativeConstParameterFunction
    {
        public system_collections_idictionary_isfixedsize() : base(0)
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
                return "_system_collections_idictionary_isFixedSize_";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                if (idictionary.IsFixedSize)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
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

    class system_collections_idictionary_isreadonly : NativeConstParameterFunction
    {
        public system_collections_idictionary_isreadonly() : base(0)
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
                return "_system_collections_idictionary_isReadOnly_";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                if (idictionary.IsReadOnly)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
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

    class system_collections_idictionary_getthisitem : NativeConstParameterFunction
    {
        public system_collections_idictionary_getthisitem() : base(1)
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
                return "_system_collections_idictionary_getThisItem_";
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


            
            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            RunTimeValueBase kv = argements[0];
            if (kv.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(
                    bin.getClassByRunTimeDataType(kv.rtType), out ot
                    ))
                {
                    kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)kv);
                }
            }
            DictionaryKey key = new DictionaryKey(kv);

            try
            {
                
                
                object obj = idictionary[key];
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
                    
                
                

            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, argements[0] + "没有链接到脚本");
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

    class system_collections_idictionary_setthisitem : NativeConstParameterFunction
    {
        public system_collections_idictionary_setthisitem() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_collections_idictionary_setThisItem_";
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
            RunTimeValueBase kv = argements[1];
            if (kv.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(
                    bin.getClassByRunTimeDataType(kv.rtType), out ot
                    ))
                {
                    kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)kv);
                }
            }
            DictionaryKey key = new DictionaryKey(kv);

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],

                    stackframe.player.linktypemapper.getLinkType( argements[0].rtType), 
                    
                    bin, true, out lo
                    ))
                {
                    idictionary[key] = lo;
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

    class system_collections_idictionary_keys : NativeConstParameterFunction
    {
        public system_collections_idictionary_keys() : base(0)
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
                return "_system_collections_idictionary_keys_";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(
                    idictionary.Keys, 
                    functionDefine.signature.returnType, 
                    returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
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

    class system_collections_idictionary_values : NativeConstParameterFunction
    {
        public system_collections_idictionary_values() : base(0)
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
                return "_system_collections_idictionary_values_";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(
                    idictionary.Values,
                    functionDefine.signature.returnType,
                    returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
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

    class system_collections_idictionary_add : NativeConstParameterFunction
    {
        public system_collections_idictionary_add() : base(2)
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
                return "_system_collections_idictionary_add_";
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
                    kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)kv);
                }
            }
            DictionaryKey key = new DictionaryKey(kv);

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[1],

                    stackframe.player.linktypemapper.getLinkType(argements[1].rtType),

                    bin, true, out lo
                    ))
                {
                    idictionary.Add(key, lo);
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

    class system_collections_idictionary_clear : NativeConstParameterFunction
    {
        public system_collections_idictionary_clear() : base(0)
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
                return "_system_collections_idictionary_clear_";
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

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                idictionary.Clear();
                success = true;
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
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

    class system_collections_idictionary_contains : NativeConstParameterFunction
    {
        public system_collections_idictionary_contains() : base(1)
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
                return "_system_collections_idictionary_contains_";
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

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                RunTimeValueBase kv = argements[0];
                if (kv.rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(
                        bin.getClassByRunTimeDataType(kv.rtType), out ot
                        ))
                    {
                        kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)kv);
                    }
                }
                DictionaryKey key = new DictionaryKey(kv);

                if (idictionary.Contains(key))
                {
                    returnSlot.directSet(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.directSet(ASBinCode.rtData.rtBoolean.False);
                }
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
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_idictionary_remove : NativeConstParameterFunction
    {
        public system_collections_idictionary_remove() : base(1)
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
                return "_system_collections_idictionary_remove_";
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

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                RunTimeValueBase kv = argements[0];
                if (kv.rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(
                        bin.getClassByRunTimeDataType(kv.rtType), out ot
                        ))
                    {
                        kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObject)kv);
                    }
                }
                DictionaryKey key = new DictionaryKey(kv);
                idictionary.Remove(key);

                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

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
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_idictionary_getienumerator : NativeConstParameterFunction
    {
        public system_collections_idictionary_getienumerator() : base(0)
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
                return "system_collections_idictionary_getenumerator_";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            
            try
            {


                object obj = idictionary.GetEnumerator();
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(
                    obj, 
                    stackframe.player.linktypemapper.getRuntimeDataType(typeof(System.Collections.IDictionaryEnumerator))
                    , returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;




            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, argements[0] + "没有链接到脚本");
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



    class system_collections_idictonaryenumerator_entry : NativeConstParameterFunction
    {
        public system_collections_idictonaryenumerator_entry() : base(0)
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
                return "system_collections_idictionaryenumerator_entry";
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



            System.Collections.IDictionaryEnumerator enumerator =
                (System.Collections.IDictionaryEnumerator)((LinkObj<object>)((ASBinCode.rtData.rtObject)thisObj).value).value;

            try
            {
                object obj = enumerator.Entry;

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, (enumerator.Current != null ? enumerator.Current.ToString() : (enumerator.ToString() + ".current的值")) + "没有链接到脚本");
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
