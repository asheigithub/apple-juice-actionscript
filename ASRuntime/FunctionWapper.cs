using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	/// <summary>
	/// 包装Function,便于外部调用
	/// </summary>
	public class FunctionWapper
	{
		public readonly rtFunction function;
		public readonly Player player;

		/// <summary>
		/// 绑定的委托
		/// </summary>
		public Delegate action;

		internal FunctionWapper(rtFunction func,Player player)
		{
			this.player = player;
			this.function = (rtFunction)func.Clone();

		}

		public object Invoke(params object[] args)
		{
			object v1=null;
			object v2=null;
			object v3=null;
			object v4=null;
			object v5=null;

			int argcount = 0;
			if (args != null)
			{
				argcount= args.Length;
			}
			if (argcount > 0)
			{
				v1 = args[0];
			}
			if (argcount > 1)
			{
				v2 = args[1];
			}
			if (argcount > 2)
			{
				v3 = args[2];
			}
			if (argcount > 3)
			{
				v4 = args[3];
			}
			if (argcount > 4)
			{
				v5 = args[4];
			}

			object[] paraArgs = null;

			if (argcount > 5)
			{
				paraArgs = new object[argcount - 5];
				for (int i = 0; i < paraArgs.Length; i++)
				{
					paraArgs[i] = args[i+5];
				}
			}

			return player.InvokeFunctionWapper(this, argcount, v1, v2, v3, v4, v5, paraArgs);
			
		}

	}
}
