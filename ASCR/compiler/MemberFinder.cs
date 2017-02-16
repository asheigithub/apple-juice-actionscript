using System;
using System.Collections.Generic;
using System.Linq;
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
            for (int i = 0; i < env.block.scope.members.Count; i++)
            {
                if (env.block.scope.members[i].name == name)
                {
                    return env.block.scope.members[i];
                }
            }
            return null;
        } 

    }
}
