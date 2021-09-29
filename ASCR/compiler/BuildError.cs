using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
    public class BuildError
    {
        public int line;
        public int ptr;
        public string srcFile;
        public string errorMsg;

        public BuildError(int line,int ptr,string srcFile,string msg)
        {
            this.line = line;
            this.ptr = ptr;
            this.srcFile = srcFile;
            this.errorMsg = msg;
        }

    }
}
