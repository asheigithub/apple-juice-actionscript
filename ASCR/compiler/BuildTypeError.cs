using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler.compiler
{
    class BuildTypeError : BuildError
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="ptr"></param>
        /// <param name="srcFile"></param>
        /// <param name="f"></param>
        /// <param name="t"></param>
        /// <param name="classfinder"></param>
        /// <param name="type">0--变量 1--访问器 2--参数</param>
        public BuildTypeError(int line, int ptr, 
            string srcFile,ASBinCode.RunTimeDataType f,
            ASBinCode.RunTimeDataType t,ASBinCode.IClassFinder classfinder,int type=0):base(line,ptr,srcFile,null)
        {
            string ft = f.toAS3Name();
            string tt = t.toAS3Name();
            if (f > ASBinCode.RunTimeDataType.unknown)
            {
                var fc = classfinder.getClassByRunTimeDataType(f);

                ft = fc.package +( string.IsNullOrEmpty(fc.package)?"":"." ) +fc.name;
            }
            if (t > ASBinCode.RunTimeDataType.unknown)
            {
                var fc = classfinder.getClassByRunTimeDataType(t);

                tt = fc.package + (string.IsNullOrEmpty(fc.package) ? "" : ".") + fc.name;
            }

            if (type == 0)
            {
                errorMsg = "不能将[" + ft + "]类型赋值给[" + tt + "]类型的变量";
            }
            else if (type == 1)
            {
                errorMsg = "不能将[" + ft + "]类型赋值给[" + tt + "]类型的访问器";
            }
            else
            {
                errorMsg = "不能将[" + ft + "]类型传递给[" + tt + "]类型的参数";
            }
            //errorMsg = "不能将[" + ft + "]类型赋值给[" + tt+ "]类型的" + (var_or_gettersetter?"访问器":"变量") ;
        }
    }
}
