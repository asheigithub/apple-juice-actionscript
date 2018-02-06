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
		public class Argement
		{
			private StackSlot[] argementslots;

			public Argement(int count)
			{
				argementslots = new StackSlot[count];
			}

			public int Length
			{
				get
				{
					return argementslots.Length;
				}
			}

			public void SetSlot(SLOT slot,int index)
			{
				argementslots[index] = (StackSlot)slot;
			}

			public void ClearSlot()
			{
				for (int i = 0; i < argementslots.Length; i++)
				{
					argementslots[i] = null;
				}
			}


			public RunTimeValueBase this[int index]
			{
				get
				{
					return argementslots[index].getValue();
				}
				set
				{
					argementslots[index].directSet(value);
				}
			}

		}

		private int totalArgs;
		public NativeConstParameterFunction(int totalArgements)
        {
			totalArgs = totalArgements;
			//argements = new RunTimeValueBase[totalArgements];
			//argements = new Argement[totalArgements];
			argements = new Argement(totalArgements);
        }

		public int TotalArgs
		{
			get { return totalArgs; }
		}

		//private bool hasinited;
		//public void initArgements(CSWC swc,Player player)
		//{
		//	if (!hasinited)
		//	{
		//		argements = new Argement(totalArgs,swc,player);
		//		//for (int i = 0; i < argements.Length; i++)
		//		//{
		//		//	argements[i]=new Argement()
		//		//}
		//		hasinited = true;
		//	}
		//}

		//protected RunTimeValueBase[] argements;

		protected Argement argements;

		public sealed override NativeFunctionMode mode
        {
            get
            {
                return NativeFunctionMode.const_parameter_0;
            }
        }

        public sealed override void executeAsync(RunTimeValueBase thisObj, SLOT[] argements, SLOT resultSlot, object callbacker, object stackframe, SourceToken token, RunTimeScope scope)
        {
            throw new EngineException();
        }

        public sealed override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
        {
            throw new EngineException();
        }

        public sealed override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
        {
            throw new EngineException();
        }



        public  void clearParameter()
        {
			argements.ClearSlot();

            //for (int i = 0; i < argements.Length; i++)
            //{
            //    argements[i] = ASBinCode.rtData.rtUndefined.undefined;
            //}
        }

        public  RunTimeValueBase getToCheckParameter(int para_id)
        {
            return argements[para_id];
        }

        public  void setCheckedParameter(int para_id, RunTimeValueBase value)
        {
            argements[para_id]=value;
        }

       
        public  void prepareParameter(FunctionDefine functionDefine,SLOT[] slots,int stidx)
        {
            for (int i = 0; i < argements.Length; i++)
            {
				// argements[i] = ASBinCode.rtData.rtUndefined.undefined;

				argements.SetSlot(slots[stidx + i], i);
				if (functionDefine.signature.parameters[i].defaultValue != null)
				{
					argements[i] = operators.FunctionCaller.getDefaultParameterValue(
						functionDefine.signature, i
						); //functionDefine.signature.parameters[i].defaultValue.getValue(null, null);
				}
				else
				{
					argements[i] =  ASBinCode.rtData.rtUndefined.undefined;
				}
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


		private static int seed=int.MaxValue;

		private static ExecuteToken lastchecktoken;

		public ExecuteToken getExecToken(string functionname)
		{
			lastchecktoken=new ExecuteToken( seed--, functionname);
			return lastchecktoken;
		}
		public static bool checkToken(ExecuteToken token)
		{
			return lastchecktoken.Equals(token);
		}

		public struct ExecuteToken :IEquatable<ExecuteToken>
		{
			public static ExecuteToken nulltoken = new ExecuteToken(int.MinValue,string.Empty);

			public int tokenid;
			public string functionname;

			public ExecuteToken(int id,string fkey)
			{
				tokenid = id;
				functionname = fkey;
			}

			public bool Equals(ExecuteToken other)
			{
				return tokenid == other.tokenid && functionname == other.functionname;
			}

			public override int GetHashCode()
			{
				return tokenid.GetHashCode() ^ functionname.GetHashCode();
			}

		}

    }
}
