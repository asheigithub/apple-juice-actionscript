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
            
            string _Object = Properties.Resources.Object;
            var tree = grammar.ParseTree(_Object, ASTool.AS3LexKeywords.LEXKEYWORDS,
                        ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, "Object.as3");

            if (grammar.hasError)
            {
                return null;
            }

            var analyser = new ASTool.AS3FileGrammarAnalyser(lib, "Object.as3");
            analyser.Analyse(grammar, tree); //生成项目的语法树

            return lib;
        }

    }
}
