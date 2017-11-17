using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	public class OpStep :ISWCSerializable
    {
        public SourceToken token;

        public OpStep(OpCode op,SourceToken token)
        {
            this.opCode = op;
            this.token = token;
        }

        /// <summary>
        /// 行标
        /// </summary>
        public string flag;
        /// <summary>
        /// 跳转偏移量
        /// </summary>
        public int jumoffset;
        /// <summary>
        /// 所属标签
        /// </summary>
        public Stack<string> labels;
        ///// <summary>
        ///// 当前行在哪些try块里
        ///// </summary>
        //public Stack<int> trys;
        /// <summary>
        /// 当前行在哪个try块里
        /// -1表示不在任何try块里
        /// </summary>
        public int tryid;
        /// <summary>
        /// 当前块所属try类型
        /// 0 try 1 catch 2 finally
        /// </summary>
        public int trytype;

        public OpCode opCode;

        public LeftValueBase reg;
        public RightValueBase arg1;
        public RightValueBase arg2;


        /// <summary>
        /// 输出结果类型
        /// </summary>
        public RunTimeDataType regType;
        /// <summary>
        /// 输入参数1类型
        /// </summary>
        public RunTimeDataType arg1Type;
        /// <summary>
        /// 输入参数2类型
        /// </summary>
        public RunTimeDataType arg2Type;


        public override string ToString()
        {
			if (opCode == OpCode.increment || opCode == OpCode.decrement
				||
				opCode == OpCode.increment_int || opCode == OpCode.increment_number
				||
				opCode == OpCode.increment_uint
				||
				opCode == OpCode.decrement_int || opCode == OpCode.decrement_number || opCode == OpCode.decrement_uint
				)
			{
				return arg1.ToString() + "\t" + opCode.ToString() + "\t";
			}
			else if (opCode == OpCode.if_jmp)
			{
				return opCode.ToString() + "\t" + arg1.ToString() + "\t" + arg2.ToString();
			}
			else if (opCode == OpCode.jmp)
			{
				return opCode.ToString() + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.flag)
			{
				return flag + ":";
			}
			else if (opCode == OpCode.raise_error)
			{
				return "throw" + "\t" + (arg1 != null ? arg1.ToString() : "");
			}
			else if (opCode == OpCode.enter_try)
			{
				return "enter_try" + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.quit_try)
			{
				return "quit_try" + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.enter_catch)
			{
				return "enter_catch" + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.quit_catch)
			{
				return "quit_catch" + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.enter_finally)
			{
				return "enter_finally" + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.quit_finally)
			{
				return "quit_finally" + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.bind_scope)
			{
				return "bind scope" + "\t" + arg1.ToString();
			}
			else if (opCode == OpCode.push_parameter)
			{
				return "push_parameter" + "\t" + arg1.ToString() + "\t" + arg2.ToString();
			}


            if (reg == null)
            {
                return opCode.ToString() + "\t" + (arg1==null?"":arg1.ToString()) + (arg2 == null ? "" : "\t"+arg2.ToString());
            }

            string result = reg.ToString() + "\t" + opCode.ToString();

            if (arg1 != null)
            {
                result += "\t" + arg1.ToString();
            }

            if (arg2 != null)
            {
                result += "\t" + arg2.ToString();
            }

            return result;
        }








		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			//public OpCode opCode;
			writer.Write((byte)opCode);
			//	public SourceToken token;
			serizlizer.SerializeObject(writer, token);
			///// <summary>
			///// 行标
			///// </summary>
			//public string flag;

			//if (flag == null) //行标在运行时无用，不序列化
			{
				writer.Write(false);
			}
			//else
			//{
			//	writer.Write(true);
			//	writer.Write(flag);
			//}


			///// <summary>
			///// 跳转偏移量
			///// </summary>
			//public int jumoffset;
			writer.Write(jumoffset);
			///// <summary>
			///// 所属标签
			///// </summary>
			//public Stack<string> labels;
			//if (labels == null) //运行时labels完全无用
			{
				writer.Write(false);
			}
			//else
			//{
			//	writer.Write(true);
			//	string[] lbls = labels.ToArray();
			//	writer.Write(lbls.Length);
			//	for (int i = 0; i < lbls.Length; i++)
			//	{
			//		writer.Write(lbls[i]);
			//	}
			//}

			///// <summary>
			///// 当前行在哪个try块里
			///// -1表示不在任何try块里
			///// </summary>
			//public int tryid;
			writer.Write(tryid);
			///// <summary>
			///// 当前块所属try类型
			///// 0 try 1 catch 2 finally
			///// </summary>
			//public int trytype;
			writer.Write(trytype);

			//public LeftValueBase reg;
			serizlizer.SerializeObject(writer, reg);
			//public RightValueBase arg1;
			serizlizer.SerializeObject(writer, arg1);
			//public RightValueBase arg2;
			serizlizer.SerializeObject(writer, arg2);

			///// <summary>
			///// 输出结果类型
			///// </summary>
			//public RunTimeDataType regType;
			writer.Write(regType);
			///// <summary>
			///// 输入参数1类型
			///// </summary>
			//public RunTimeDataType arg1Type;
			writer.Write(arg1Type);
			///// <summary>
			///// 输入参数2类型
			///// </summary>
			//public RunTimeDataType arg2Type;
			writer.Write(arg2Type);
		}

		public static OpStep Deserialize(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{
			//public OpCode opCode;
			OpCode opCode = (OpCode)reader.ReadByte();
			//	public SourceToken token;
			SourceToken token = serizlizer.DeserializeObject<SourceToken>(reader, SourceToken.LoadToken);

			OpStep step = new OpStep(opCode, token); serizlized.Add(key, step);

			///// <summary>
			///// 行标
			///// </summary>
			//public string flag;
			if (reader.ReadBoolean())
			{
				step.flag = reader.ReadString();
			}

			///// <summary>
			///// 跳转偏移量
			///// </summary>
			//public int jumoffset;
			step.jumoffset = reader.ReadInt32();
			///// <summary>
			///// 所属标签
			///// </summary>
			//public Stack<string> labels;

			if (reader.ReadBoolean())
			{
				step.labels = new Stack<string>();
				int count = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					step.labels.Push( reader.ReadString() );
				}

			}

			//string[] lbls = labels.ToArray();
			//writer.Write(lbls.Length);
			//for (int i = 0; i < lbls.Length; i++)
			//{
			//	writer.Write(lbls[i]);
			//}




			///// <summary>
			///// 当前行在哪个try块里
			///// -1表示不在任何try块里
			///// </summary>
			//public int tryid;
			step.tryid = reader.ReadInt32();
			///// <summary>
			///// 当前块所属try类型
			///// 0 try 1 catch 2 finally
			///// </summary>
			//public int trytype;
			step.trytype = reader.ReadInt32();

			//public LeftValueBase reg;
			step.reg = (LeftValueBase)serizlizer.DeserializeObject<ISWCSerializable>(reader, ISWCSerializableLoader.LoadIMember);
			//public RightValueBase arg1;
			step.arg1 = (RightValueBase)serizlizer.DeserializeObject<ISWCSerializable>(reader, ISWCSerializableLoader.LoadIMember);
			//public RightValueBase arg2;
			step.arg2 = (RightValueBase)serizlizer.DeserializeObject<ISWCSerializable>(reader, ISWCSerializableLoader.LoadIMember);

			///// <summary>
			///// 输出结果类型
			///// </summary>
			//public RunTimeDataType regType;
			step.regType = reader.ReadInt32();
			///// <summary>
			///// 输入参数1类型
			///// </summary>
			//public RunTimeDataType arg1Type;
			step.arg1Type = reader.ReadInt32();
			///// <summary>
			///// 输入参数2类型
			///// </summary>
			//public RunTimeDataType arg2Type;
			step.arg2Type = reader.ReadInt32();

			return step;
		}

		
	}
}
