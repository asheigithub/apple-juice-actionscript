using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ASBinCode;

namespace ASCompiler.compiler
{
    class FindOutPackageScopeMember : ASBinCode.IMember
    {
        public ASBinCode.rtti.Class outscopeclass;
        public IMember member;

        public Register buildAccessThisMember(ASTool.Token matchtoken, CompileEnv env)
        {
            if (!(member is VariableBase))
            {
                throw new BuildException(new BuildError(
                    matchtoken.line, matchtoken.ptr, 
                    matchtoken.sourceFile, "包外数据期望一个Variable"));
            }

            VariableBase var = (VariableBase)member;

            var eax_member = env.getAdditionalRegister();
            eax_member.setEAXTypeWhenCompile(var.valueType);
            


            OpStep op = new OpStep(OpCode.link_outpackagevairable,
                new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
            op.reg = eax_member;
            op.regType = eax_member.valueType;
            op.arg1 = var;
            op.arg1Type = var.valueType;

            op.arg2 = new ASBinCode.rtData.RightValue( new ASBinCode.rtData.rtInt( outscopeclass.classid ) );
            op.arg2Type = RunTimeDataType.rt_int;


            env.block.opSteps.Add(op);

            

            return eax_member;
        }

        public int indexOfMembers
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IMember clone()
        {
            throw new NotImplementedException();
        }

		public void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			throw new NotImplementedException();
		}
	}
}
