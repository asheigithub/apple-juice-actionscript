using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASBinCode;

namespace ASRuntime.nativefuncs
{
    /// <summary>
    /// 本地函数，确定不会修改参数，参数数量确定及类型确定,并且完全本地执行。这样即不需要包装参数，已提高性能
    /// </summary>
    public abstract class NativeConstParameterFunction : NativeFunctionBase
    {
        public NativeConstParameterFunction(int totalArgements)
        {
            argements = new RunTimeValueBase[totalArgements];
        }

        protected RunTimeValueBase[] argements;

        public sealed override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.const_parameter_0;
            }
        }

        public sealed override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements, SLOT resultSlot, object callbacker, object stackframe, SourceToken token, RunTimeScope scope)
        {
            throw new ASRunTimeException();
        }

        public sealed override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new ASRunTimeException();
        }

        public sealed override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            throw new ASRunTimeException();
        }



        public  void clearParameter()
        {
            for (int i = 0; i < argements.Length; i++)
            {
                argements[i] = ASBinCode.rtData.rtUndefined.undefined;
            }
        }

        public  RunTimeValueBase getToCheckParameter(int para_id)
        {
            return argements[para_id];
        }

        public  void setCheckedParameter(int para_id, RunTimeValueBase value)
        {
            argements[para_id]=value;
        }

       
        public  void prepareParameter(FunctionDefine functionDefine)
        {
            for (int i = 0; i < argements.Length; i++)
            {
                argements[i] = ASBinCode.rtData.rtUndefined.undefined;
            }
        }

        public  void pushParameter(FunctionDefine function, int id, RunTimeValueBase value, SourceToken token, object invokeFrame, out bool success)
        {
            success = true;
            if (id < argements.Length)
            {
                argements[id] = value;
            }
            else
            {
                ((StackFrame)invokeFrame).throwArgementException(
                                token,
                                string.Format(
                                "Argument count mismatch on Function/{0}. Expected {1}, got {2}.",
                                function.name, function.signature.parameters.Count, id + 1
                                )

                                );

                success = false;
            }

        }
        

        public abstract void execute3(
            RunTimeValueBase thisObj,
            FunctionDefine functionDefine,
            SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success
            );

    }
}
