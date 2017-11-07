using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
    public enum OverrideableOperator
    {
        /// <summary>
        /// >
        /// </summary>
        GreatherThan=0,
        /// <summary>
        /// &lt;
        /// </summary>
        LessThan=1,
        /// <summary>
        /// ==
        /// </summary>
        Equality=2,
        /// <summary>
        /// >=
        /// </summary>
        GreatherThanOrEqual=3,
        /// <summary>
        /// &lt;=
        /// </summary>
        LessThanOrEqual=4,
        /// <summary>
        /// !=
        /// </summary>
        Inequality=5,

        /// <summary>
        /// +前缀
        /// </summary>
        Unary_plus=6,
        /// <summary>
        /// -前缀
        /// </summary>
        Unary_negation=7,
        /// <summary>
        /// +
        /// </summary>
        addition=9,
        /// <summary>
        /// -
        /// </summary>
        subtraction=10,

        /// <summary>
        /// |
        /// </summary>
        bitOr=11,

    }

    public struct OperatorFunctionKey : IEquatable<OperatorFunctionKey>
    {
        public readonly RunTimeDataType v1;
        public readonly RunTimeDataType v2;

        public OperatorFunctionKey(RunTimeDataType v1, RunTimeDataType v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public bool Equals(OperatorFunctionKey other)
        {
            return v1 == other.v1 && v2 == other.v2;
        }

        public override int GetHashCode()
        {
            return v1.GetHashCode() ^ 1 ^ v2.GetHashCode() ^ 2;
        }

        

    }
	
	class DefineAndFunc
    {
        public rtti.FunctionDefine define;
        public rtData.rtFunction func;
    }

	[Serializable]
    public class OperatorFunctions :ISWCSerializable
    {
        private Dictionary<OperatorFunctionKey, DefineAndFunc>[] operFunctions;

        /// <summary>
        /// 检查是否充分
        /// </summary>
        public bool Check(out rtti.FunctionDefine function)
        {
            for (int i = 0; i <= (int)OverrideableOperator.Inequality; i++)
            {
                foreach (var key in operFunctions[i].Keys)
                {
                    //***查找其他操作符定义***
                    for (int j = 0; j < (int)OverrideableOperator.Inequality; j++)
                    {
                        var dict = operFunctions[j];
                        if (!dict.ContainsKey(key))
                        {
                            function = operFunctions[i][key].define;
                            return false;
                        }
                    }
                }
            }
            function = null;
            return true;
        }

        public OperatorFunctions()
        {
            operFunctions = new Dictionary<OperatorFunctionKey, DefineAndFunc>[(int)OverrideableOperator.bitOr+1];
            for (int i = 0; i < operFunctions.Length; i++)
            {
                operFunctions[i] = new Dictionary<OperatorFunctionKey, DefineAndFunc>();
            }

        }

        public void AddOperatorFunction(OverrideableOperator operCode,rtti.FunctionDefine function)
        {
            var dict = operFunctions[(int)operCode];
            if (function.signature.parameters.Count == 2)
            {
                OperatorFunctionKey key = new OperatorFunctionKey(
                    function.signature.parameters[0].type,
                    function.signature.parameters[1].type
                    );
                dict.Add(key, new DefineAndFunc() { define= function,func=null });
                
            }
            else
            {
                OperatorFunctionKey key = new OperatorFunctionKey(
                   function.signature.parameters[0].type,
                   RunTimeDataType.unknown
                   );
                dict.Add(key, new DefineAndFunc() { define = function, func = null });
            }
        }

        public rtData.rtFunction getOperatorFunction(OverrideableOperator operCode,RunTimeDataType v1,RunTimeDataType v2)
        {
            var dict = operFunctions[(int)operCode];
            DefineAndFunc function;
            if (dict.TryGetValue(new OperatorFunctionKey(v1, v2), out function))
            {
                if (function.func == null)
                {
                    function.func = new rtData.rtFunction(function.define.functionid, function.define.isMethod);
                }

                return function.func;
            }
            else
            {
                return null;
            }
        }

        public rtti.FunctionDefine getOperatorDefine(OverrideableOperator operCode, RunTimeDataType v1, RunTimeDataType v2)
        {
            var dict = operFunctions[(int)operCode];
            DefineAndFunc function;
            if (dict.TryGetValue(new OperatorFunctionKey(v1, v2), out function))
            {
                return function.define;
            }
            else
            {
                return null;
            }
        }






		public static OperatorFunctions LoadOperatorFunctions(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			OperatorFunctions operatorFunctions = new OperatorFunctions(); serizlized.Add(key, operatorFunctions);
			for (int i = 0; i < operatorFunctions.operFunctions.Length; i++)
			{
				var dict = operatorFunctions.operFunctions[i];
				int count = reader.ReadInt32();
				for (int j = 0; j < count; j++)
				{
					RunTimeDataType v1 = reader.ReadInt32();
					RunTimeDataType v2 = reader.ReadInt32();

					OperatorFunctionKey operatorFunctionKey = new OperatorFunctionKey(v1, v2);

					rtti.FunctionDefine define = serizlizer.DeserializeObject<rtti.FunctionDefine>(reader, rtti.FunctionDefine.LoadFunctionDefine);
					rtData.rtFunction function = serizlizer.DeserializeObject<rtData.rtFunction>(reader, RunTimeValueBase.LoadRunTimeValueBase);

					DefineAndFunc defineAndFunc = new DefineAndFunc();
					defineAndFunc.define = define;
					defineAndFunc.func = function;

					dict.Add(operatorFunctionKey, defineAndFunc);

				}
			}

			return operatorFunctions;

		}



		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			
			for (int i = 0; i < operFunctions.Length; i++)
			{
				var dict = operFunctions[i];
				writer.Write(dict.Count);
				foreach (var item in dict)
				{
					writer.Write(item.Key.v1);
					writer.Write(item.Key.v2);

					serizlizer.SerializeObject(writer, item.Value.define);
					serizlizer.SerializeObject(writer, item.Value.func);
				}

			}

		}
	}
}
