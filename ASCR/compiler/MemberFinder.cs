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
            //从后往前找。可解决catch块同名变量e问题
            for (int i = env.block.scope.members.Count-1; i >=0; i--)
            {
                if (env.block.scope.members[i].name == name
                    )
                {
                    return env.block.scope.members[i];
                }
            }
            return null;
        } 

    }
}
