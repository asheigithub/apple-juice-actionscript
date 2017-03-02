using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
    /// <summary>
    /// 
    /// </summary>
    class MemberFinder
    {
        public static ASBinCode.IMember find(string name, CompileEnv env)
        {

            ASBinCode.IScope scope = env.block.scope;
            int depth = 0;
            while (scope !=null)
            {

                //从后往前找。可解决catch块同名变量e问题
                for (int i = scope.members.Count - 1; i >= 0; i--)
                {
                    if (scope.members[i].name == name
                        )
                    {
                        return scope.members[i].clone();
                    }
                }

                scope = scope.parentScope;
                ++depth;
            }


            return null;
        } 

    }
}
