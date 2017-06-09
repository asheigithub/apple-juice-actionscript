using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCTest.regNativeFunctions
{
    class system_linkinterface
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

            bin.regNativeFunction(new system_collectoins_ienumerable_getenumerator_());
            bin.regNativeFunction(new system_collectoins_ienumerator_reset());
            bin.regNativeFunction(new system_collectoins_ienumerator_movenext());
            bin.regNativeFunction(new system_collectoins_ienumerator_current());

        }
    }

    class system_collectoins_ienumerable_getenumerator_ : NativeConstParameterFunction
    {
        public system_collectoins_ienumerable_getenumerator_() : base(0)
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

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj,functionDefine, returnSlot, bin, stackframe.player);
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

    class system_collectoins_ienumerator_reset : NativeConstParameterFunction
    {
        public system_collectoins_ienumerator_reset() : base(0)
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

    class system_collectoins_ienumerator_movenext : NativeConstParameterFunction
    {
        public system_collectoins_ienumerator_movenext() : base(0)
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

    class system_collectoins_ienumerator_current : NativeConstParameterFunction
    {
        public system_collectoins_ienumerator_current() : base(0)
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

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine, returnSlot, bin, stackframe.player);
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

}
