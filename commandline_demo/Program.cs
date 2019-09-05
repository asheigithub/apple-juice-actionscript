using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commandline_demo
{
    class Program
    {
        static void Main(string[] args)
        {

            ASTool.Grammar grammar = ASCompiler.Grammar.getGrammar();

            string teststring = 
                @"package{
                    [Doc] //this is document class
                    class Main
                    {}
                }
                var a=1;
                var b=2;
                trace( a,'+',b,'=', a + b);
                ";


            var proj = new ASTool.AS3.AS3Proj();
            var srcout = new ASTool.ConSrcOut();


            //build AST
            grammar.hasError = false;
            var tree = grammar.ParseTree(teststring, ASTool.AS3LexKeywords.LEXKEYWORDS,
                        ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS);           
            if (grammar.hasError)
            {
                Console.WriteLine("解析语法树失败!");
                Console.ReadLine();
                return;
            }

            var analyser = new ASTool.AS3FileGrammarAnalyser(proj, "Main.cs");
            if (!analyser.Analyse(tree)) //生成项目的语法树
            {
                Console.WriteLine(analyser.err.ToString());
                Console.WriteLine("语义分析失败!");
                Console.ReadLine();
                return;
            }

            //build bytecode
            ASCompiler.compiler.Builder builder = new ASCompiler.compiler.Builder();
           
            builder.options.CheckNativeFunctionSignature = false;
            builder.Build(proj, null);

            if (builder.buildErrors.Count == 0)
            {
                ASBinCode.CSWC swc = builder.getBuildOutSWC();

                //save bytecode
                byte[] bin = swc.toBytes();
                swc = ASBinCode.CSWC.loadFromBytes(bin);
                 
                if (swc.blocks.Count > 0)
                {

                    ASRuntime.Player player = new ASRuntime.Player();
                    //load bytecode
                    player.loadCode(swc);
                    

                    Console.WriteLine();
                    Console.WriteLine("========");

                    player.run(null);
                }
                Console.ReadLine();
                

            }



        }
    }
}
