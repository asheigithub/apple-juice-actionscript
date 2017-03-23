using System;
using System.Collections.Generic;
using System.Text;



namespace ASCTest
{
    class Program
    {
        static void Main(string[] args)
        {

            ASTool.Grammar grammar = ASCompiler.Grammar.getGrammar();

            //string teststring = "package{}var a:String = \"first\";var b:String = \"First\"; var c=a==b;";
            string teststring = "package{}";//System.IO.File.ReadAllText("../../testScript/AS3Testproj/src/Main.as");

            string[] files = null;

            if (args.Length > 0)
            {
                if (System.IO.Directory.Exists(args[0]))
                {
                    //teststring = System.IO.File.ReadAllText(args[0]);
                    files = System.IO.Directory.GetFiles(args[0], "*.as", System.IO.SearchOption.AllDirectories );
                }
            }
            else
            {
                Console.Write("输入as文件所在路径");
                return;
            }

           

            var proj = new ASTool.AS3.AS3Proj();
            var srcout = new ASTool.ConSrcOut();

            for (int i = 0; i < files.Length; i++)
            {
                grammar.hasError = false;
                teststring = System.IO.File.ReadAllText(files[i]);
                if (string.IsNullOrEmpty(teststring))
                {
                    continue;
                }

                var tree = grammar.ParseTree(teststring, ASTool.AS3LexKeywords.LEXKEYWORDS , 
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS  ,files[i]);

                if (grammar.hasError)
                {
                    Console.WriteLine("解析语法树失败!");
                    Console.ReadLine();
                    return;
                }

                Console.Clear();

                var analyser = new ASTool.AS3FileGrammarAnalyser(proj, files[i]);
                analyser.Analyse(grammar, tree); //生成项目的语法树

            }

            Console.WriteLine();
            Console.WriteLine("====语法树翻译====");

            //runtimeCompiler rtLoader = new runtimeCompiler();
            foreach (var p in proj.SrcFiles)
            {
                p.Write(0, srcout);
            }
            
            ASCompiler.compiler.Builder builder = new ASCompiler.compiler.Builder();

            
            builder.Build(proj);


            if (builder.buildErrors.Count == 0)
            {
                ASBinCode.CSWC swc = builder.bin;

                for (int i = 0; i < swc.blocks.Count ; i++)
                {
                    var block = swc.blocks[i];
                    if (block != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("====操作指令 block " + block.name + " " + block.id + "====");
                        Console.WriteLine();
                        Console.WriteLine("total registers:" + block.totalRegisters);
                        Console.WriteLine(block.ToString());
                    }
                }

                if (swc.blocks.Count > 0)
                {
                    ASRuntime.Player player = new ASRuntime.Player();
                    player.loadCode(swc);


                    Console.WriteLine();
                    Console.WriteLine("====程序输出====");

                    player.run2(null);

                }
                Console.WriteLine();
                

            }

#if DEBUG
            Console.WriteLine("按任意键结束");
            Console.ReadLine();
#endif

        }
    }
}
