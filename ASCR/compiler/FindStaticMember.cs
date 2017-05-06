using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASCompiler.compiler.builds;

namespace ASCompiler.compiler
{
    /// <summary>
    /// 指示找到了一个静态成员
    /// </summary>
    class FindStaticMember : ASBinCode.IMember
    {
        /// <summary>
        /// 静态类的成员
        /// </summary>
        public ASBinCode.rtti.ClassMember classMember;
        /// <summary>
        /// 找到的静态类
        /// </summary>
        public StaticClassDataGetter static_class;

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

        public LeftValueBase buildAccessThisMember(ASTool.Token matchtoken , CompileEnv env)
        {
            if (
                !(classMember.bindField is VariableBase)
                &&
                env.block.scope is ASBinCode.scopes.FunctionScope &&
                env.block.scope.parentScope is ASBinCode.scopes.ObjectInstanceScope
                &&
                ((ASBinCode.scopes.ObjectInstanceScope)env.block.scope.parentScope)._class == static_class._class
                )
            {
                return (LeftValueBase)classMember.bindField;
            }
            else
            {
                OpStep stepInitClass = new OpStep(OpCode.init_staticclass,
                                            new SourceToken(matchtoken.line, matchtoken.ptr, matchtoken.sourceFile));
                stepInitClass.arg1 = new ASBinCode.rtData.RightValue(
                    new ASBinCode.rtData.rtInt(static_class._class.instanceClass.classid));
                stepInitClass.arg1Type = static_class._class.getRtType();
                env.block.opSteps.Add(stepInitClass);

                var _buildin_ = static_class;
                var eax_member = env.getAdditionalRegister();
                eax_member.setEAXTypeWhenCompile(classMember.valueType);

                eax_member._regMember = classMember;
                eax_member._regMemberSrcObj = _buildin_;

                AccessBuilder.make_dotStep(env, classMember, matchtoken, eax_member, _buildin_);

                return eax_member;
            }
        }


    }
}
