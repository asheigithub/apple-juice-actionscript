using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
    class BuildException : Exception
    {
        public BuildError error;

        public BuildException(int line, int ptr, string srcFile, string msg)
        {
            error = new BuildError(line,ptr,srcFile,msg);
        }

        public BuildException(BuildError error)
        {
            this.error = error;
        }
    }

    class MemberNotExistsBuildException : BuildException
    {
        public MemberNotExistsBuildException(int line, int ptr, string srcFile, string msg)
            :base(line,ptr,srcFile,msg)
        {

        }
        public MemberNotExistsBuildException(BuildError error)
            : base(error)
        {

        }

    }

    class TooManyBuildErrorException : Exception
    {

    }

}
