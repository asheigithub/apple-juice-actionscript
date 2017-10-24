using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
	[Serializable]
	public class SourceToken
    {
        public int line;
        public int ptr;
        public string sourceFile;

        public SourceToken(int line,int ptr,string sourceFile)
        {
            this.line = line;
            this.ptr = ptr;
            this.sourceFile = sourceFile;
        }

        
    }
}
