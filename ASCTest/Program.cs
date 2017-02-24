using System;
using System.Collections.Generic;
using System.Text;



namespace ASCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ASTool.Lex lex = new ASTool.Lex(null);// @"D:\ASTool\ASTool\PG1.txt");
            var words = lex.GetWords(Properties.Resources.PG1);//System.IO.File.ReadAllText(lex.File));
            //words.Write(new ASTool.ConSrcOut(), 0);

            ASTool.Grammar grammar = new ASTool.Grammar(words);

            //string teststring = "package{}var a:String = \"first\";var b:String = \"First\"; var c=a==b;";
            string teststring = "package{}";//System.IO.File.ReadAllText("../../testScript/AS3Testproj/src/Main.as");

            if (args.Length > 0)
            {
                if (System.IO.File.Exists(args[0]))
                {
                    teststring = System.IO.File.ReadAllText(args[0]);
                }
            }
            else
            {
                Console.Write("输入要处理的 *.as文件");
                return;
            }

            var tree = grammar.ParseTree(teststring, ASTool.AS3LexKeywords.LEXKEYWORDS , 
                ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS  , args[0]);

            if (grammar.hasError)
            {
                Console.WriteLine("解析语法树失败!");
                Console.ReadLine();
                return;
            }

            Console.Clear();

            var proj = new ASTool.AS3.AS3Proj();
            var srcout = new ASTool.ConSrcOut();

            var analyser = new ASTool.AS3FileGrammarAnalyser(proj, teststring);
            analyser.Analyse(grammar, tree); //生成项目的语法树

            Console.WriteLine();
            Console.WriteLine("====语法树翻译====");

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


                Console.WriteLine();
                Console.WriteLine("====操作指令====");
                Console.WriteLine();
                Console.WriteLine("total registers:" + block.totalRegisters );
                Console.WriteLine(block.ToString());


                ASRuntime.Player player = new ASRuntime.Player();
                player.loadCode(block);

                Console.WriteLine();
                Console.WriteLine("====程序输出====");

                var scope= player.run();

                Console.WriteLine();

            }


#if DEBUG
            Console.WriteLine("按任意键结束");
            Console.ReadLine();
#endif

        }
    }
}
