using System;

using RaptorB.Lexer;

namespace RaptorB
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Interpreter.Interpreter interpreter = new Interpreter.Interpreter();

            while (true)
            {
                string source = Console.ReadLine();
                var tokens = new Lexer.Lexer(source).Scan();
                Console.WriteLine("Got tokens: " + tokens.Count);
                var ast = new Parser.Parser(tokens).Parse();
                Console.WriteLine("Got ast: " + ast.Children.Count);
                interpreter.Interpret(ast);
            }
        }
    }
}
