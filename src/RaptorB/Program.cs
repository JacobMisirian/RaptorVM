using System;
using System.IO;

using RaptorB.Lexer;

namespace RaptorB
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                while (true)
                    runFromString(Console.ReadLine());
            }
            else
                runFromString(File.ReadAllText(args[0]));
        }

        private static void runFromString(string source)
        {
            var tokens = new Lexer.Lexer(source).Scan();
            /*foreach (Token token in tokens)
                    Console.WriteLine(token.ToString());*/
            var ast = new Parser.Parser(tokens).Parse();
            var symbolTable = new SemanticAnalysis.SemanticAnalyzer(ast).Analyze();
            Console.WriteLine(new CodeGen.CodeGenerator(ast, symbolTable).Generate());
        }
    }
}
