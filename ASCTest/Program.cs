using System;
using System.Collections.Generic;
using System.Text;



namespace ASCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ASTool.Lex lex = new ASTool.Lex(@"D:\ASTool\ASTool\PG1.txt");
            var words = lex.GetWords(System.IO.File.ReadAllText(lex.File));
            //words.Write(new ASTool.ConSrcOut(), 0);

            ASTool.Grammar grammar = new ASTool.Grammar(words);

            string teststring = "package{}var l:*= 1+6 ; var j=l; l+=1; ";
            var tree = grammar.ParseTree(teststring, ASTool.AS3LexKeywords.LEXKEYWORDS, teststring);

            if (grammar.hasError)
            {
                Console.WriteLine("解析语法树失败!");
                Console.ReadLine();
                return;
            }

            var proj = new ASTool.AS3.AS3Proj();
            var srcout = new ASTool.ConSrcOut();

            var analyser = new ASTool.AS3FileGrammarAnalyser(proj, teststring);
            analyser.Analyse(grammar, tree); //生成项目的语法树

            //runtimeCompiler rtLoader = new runtimeCompiler();
            foreach (var p in proj.SrcFiles)
            {
                p.Write(0, srcout);
            }

            ASCompiler.compiler.Builder builder = new ASCompiler.compiler.Builder();
            builder.Build(proj);

            


            for (int i = 0; i < builder.blocks.Count; i++)
            {
                var block = builder.blocks[i];

                Console.WriteLine("====");
                Console.WriteLine();
                Console.WriteLine("total registers:" + block.totalRegisters );
                Console.WriteLine(block.ToString());


                ASRuntime.Player player = new ASRuntime.Player();
                player.loadCode(block);

                player.run();

            }

            Console.ReadLine();
        }
    }
}
