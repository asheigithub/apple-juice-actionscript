using System;
using System.Collections.Generic;
using System.Text;


namespace ASCompiler.compiler
{
    using rt = ASBinCode.RunTimeDataType;
    
    class TypeReader
    {
        /// <summary>
        /// 从源代码的类型字符串中转换成运行时类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ASBinCode.RunTimeDataType fromSourceCodeStr(string t,CompileEnv env,ASTool.Token token)
        {
            if (t == "*")
            {
                return rt.rt_void;
            }
            else if (t == "Boolean")
            {
                return rt.rt_boolean;
            }
            else if (t == "int")
            {
                return ASBinCode.RunTimeDataType.rt_int;
            }
            else if (t == "uint")
            {
                return ASBinCode.RunTimeDataType.rt_uint;
            }
            else if (t == "Number")
            {
                return ASBinCode.RunTimeDataType.rt_number;
            }
            else if (t == "String")
            {
                return ASBinCode.RunTimeDataType.rt_string;
            }
            else
            {
                throw new BuildException(token.line, token.ptr, token.sourceFile,
                    "类型" + t + "未实现"
                    );
            }
        }
       
    }
}
