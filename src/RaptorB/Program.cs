using System;

using RaptorB.Lexer;

namespace RaptorB
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                string source = Console.ReadLine();
                var tokens = new Lexer.Lexer(source).Scan();
                /*foreach (Token token in tokens)
                    Console.WriteLine(token.ToString());*/
                var ast = new Parser.Parser(tokens).Parse();
                var symbolTable = new SemanticAnalysis.SemanticAnalyzer(ast).Analyze();
                Console.WriteLine(new CodeGen.CodeGenerator(ast, symbolTable).Generate());
            }
        }
    }
}
