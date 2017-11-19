using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	/// <summary>
	/// 代码块
	/// </summary>
	public class CodeBlock : ISWCSerializable
    {
        public readonly int id;
        public readonly string name;

        public readonly int define_class_id;
        public readonly bool isoutclass;

        /// <summary>
        /// 运行环境定义
        /// </summary>
        public IScope scope;
        
        public List<OpStep> opSteps;
		public OpStep[] instructions;
		/// <summary>
		/// 本段代码是否包含TryCatch
		/// </summary>
		public bool hasTryStmt;

        /// <summary>
        /// 本段代码共用了多少个寄存器
        /// </summary>
        public int totalRegisters;

        public List<StackSlotAccessor> regConvFromVar;

        public CodeBlock(int id,string name,int define_class_id,bool isoutclass)
        {
            this.id = id;
            this.name = name;
            this.define_class_id = define_class_id;
            this.isoutclass = isoutclass;

            opSteps = new List<OpStep>();
            regConvFromVar = new List<StackSlotAccessor>();
        }

		public override string ToString()
        {
            string r = string.Empty;
			r += name + "\n";
			if (opSteps != null)
			{
				for (int i = 0; i < opSteps.Count; i++)
				{
					r = r + opSteps[i].ToString() + "\n";
				}
			}
			
            return r;
        }

		public string GetInstruction()
		{
			string r = string.Empty;
			if (instructions != null)
			{
				for (int i = 0; i < instructions.Length; i++)
				{
					r = r + instructions[i].ToString() + "\n";
				}
			}
			return r;
		}





		

		public static CodeBlock Deserialize(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int,object> serizlized,int key)
		{

			var id = reader.ReadInt32();
			var name = reader.ReadString();  //writer.Write(name);
			var define_class_id = reader.ReadInt32();  //writer.Write(define_class_id);
			var isoutclass = reader.ReadBoolean(); //writer.Write(isoutclass);
			var hasTryStmt = reader.ReadBoolean();// writer.Write(hasTryStmt);

			CodeBlock block = new CodeBlock(id, name, define_class_id, isoutclass);
			block.hasTryStmt = hasTryStmt;
			serizlized.Add(key,block);	

			block.totalRegisters = reader.ReadInt32();
			block.scope = serizlizer.DeserializeObject<scopes.ScopeBase>(reader, scopes.ScopeBase.Deserialize);

			int stepscount = reader.ReadInt32();
			block.instructions = new OpStep[stepscount];block.opSteps = null;
			for (int i = 0; i < stepscount; i++)
			{
				OpStep step = serizlizer.DeserializeObject<OpStep>(reader, OpStep.Deserialize);
				//block.opSteps.Add(step);
				block.instructions[i] = step;
			}

			int regconvcount = reader.ReadInt32();
			for (int i = 0; i < regconvcount; i++)
			{
				StackSlotAccessor register = (StackSlotAccessor)serizlizer.DeserializeObject<ISWCSerializable>(reader, ISWCSerializableLoader.LoadIMember);
				block.regConvFromVar.Add(register);
			}

			return block;

			
		}

		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			
			writer.Write(id);
			writer.Write(name);
			writer.Write(define_class_id);
			writer.Write(isoutclass);
			writer.Write(hasTryStmt);
			writer.Write(totalRegisters);

			serizlizer.SerializeObject(writer, (scopes.ScopeBase)scope);

			//writer.Write(opSteps.Count);
			//for (int i = 0; i < opSteps.Count; i++)
			//{
			//	var step = opSteps[i];
			//	serizlizer.SerializeObject(writer, step);
			//}

			writer.Write(instructions.Length);
			for (int i = 0; i < instructions.Length; i++)
			{
				var step = instructions[i];
				serizlizer.SerializeObject(writer, step);
			}

			writer.Write(regConvFromVar.Count);
			for (int i = 0; i < regConvFromVar.Count; i++)
			{
				StackSlotAccessor conv = regConvFromVar[i];
				//conv.Serialize(writer, serizlizer);
				serizlizer.SerializeObject(writer, conv);
			}
		}

		
	}
}
