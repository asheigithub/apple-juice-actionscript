using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ASCTest.regNativeFunctions
{
    class system_icomparable_buildin
    {
        public static void regNativeFunctions(CSWC bin)
        {
            bin.regNativeFunction(
                LinkSystem_Buildin.getCreator("system_icomparable_creator_", default(IComparable)));// new system_int64_creator());
            bin.regNativeFunction(new system_icomparable_compareto());
        }
        class system_icomparable_compareto : NativeConstParameterFunction
        {
            public system_icomparable_compareto() : base(1)
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
                    return "_system_icomparable_compareto_";
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

                IComparable icomp =
                    (IComparable)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				
                try
                {
                    object lo;
                    if (!stackframe.player.linktypemapper.rtValueToLinkObject(
                        argements[0],

                        stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                        ,
                        bin, false, out lo
                        ))
                    {
						stackframe.throwCastException(token, argements[0].rtType,

							functionDefine.signature.parameters[0].type
							);
						success = false;
						return;
                    }


					int r = icomp.CompareTo(lo);
					returnSlot.setValue(r);
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

    }
}
