using System;
using System.Collections.Generic;
using System.Text;

namespace ASCompiler
{
    public class Grammar
    {
        public static ASTool.Grammar getGrammar()
        {
            ASTool.Lex lex = new ASTool.Lex(null);
            var words = lex.GetWords(Properties.Resources.PG1);
            
            ASTool.Grammar grammar = new ASTool.Grammar(words);
            return grammar;
        }

        public static ASTool.AS3.AS3Proj makeLibProj()
        {
            var lib = new ASTool.AS3.AS3Proj();
            var grammar = getGrammar();

            List<compiler.utils.Tuple<ASTool.GrammerTree, string>> trees = new List<compiler.utils.Tuple<ASTool.GrammerTree, string>>();

            {
                string _Object = Properties.Resources.Object;
                //***类库源码***
                var tree = grammar.ParseTree(_Object, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Object.as3");
                
                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add ( new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Object.as3"));
            }

            {
                string _Class = Properties.Resources.Class;
                //***类库源码***
                var tree = grammar.ParseTree(_Class, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Class.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Class.as3"));
            }

            {
                string _int = Properties.Resources._int;
                var tree = grammar.ParseTree(_int, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "int.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "int.as3"));
            }

            {
                string _uint = Properties.Resources._uint;
                var tree = grammar.ParseTree(_uint, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "uint.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "uint.as3"));
            }

            {
                string _number = Properties.Resources.Number;
                var tree = grammar.ParseTree(_number, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Number.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Number.as3"));
            }

            {
                string _function = Properties.Resources.Function;
                var tree = grammar.ParseTree(_function, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Function.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Function.as3"));
            }

            {
                string _array = Properties.Resources.Array;
                var tree = grammar.ParseTree(_array, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Array.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Array.as3"));
            }

            {
                string _boolean = Properties.Resources.Boolean;
                var tree = grammar.ParseTree(_boolean, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Boolean.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Boolean.as3"));
            }

            {
                string _buildin_ = Properties.Resources.__buildin__;
                var tree = grammar.ParseTree(_buildin_, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "@__buildin__.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "@__buildin__.as3"));
            }

            {
                string _math = Properties.Resources.Math;
                var tree = grammar.ParseTree(_math, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Math.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Math.as3"));
            }

            {
                string _error = Properties.Resources.Error;
                var tree = grammar.ParseTree(_error, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Error.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "Error.as3"));
            }

            {
                string _error = Properties.Resources.TypeError;
                var tree = grammar.ParseTree(_error, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "TypeError.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "TypeError.as3"));
            }

            {
                string _dictionary = Properties.Resources.Dictionary;
                var tree = grammar.ParseTree(_dictionary, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "flash/utils/Dictionary.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "flash/utils/Dictionary.as3"));
            }

            {
                string _sprite = Properties.Resources.Sprite;
                var tree = grammar.ParseTree(_sprite, ASTool.AS3LexKeywords.LEXKEYWORDS,
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Sprite.as3");

                if (grammar.hasError)
                {
                    return null;
                }
                trees.Add(new compiler.utils.Tuple<ASTool.GrammerTree, string>(tree, "flash/display/Sprite.as3"));
            }


            foreach (var tree in trees)
            {
                var analyser = new ASTool.AS3FileGrammarAnalyser(lib, tree.item2);
                if (!analyser.Analyse(grammar, tree.item1)) //生成项目的语法树
                {
                    return null;
                }
            }


            for (int i = 0; i < lib.SrcFiles.Count; i++)
            {
                if (lib.SrcFiles[i].Package.MainClass.Name == "__buildin__")
                {
                    lib.SrcFiles[i].Package.MainClass.Name = "@__buildin__";
                }
            }

            return lib;
        }

    }
}
